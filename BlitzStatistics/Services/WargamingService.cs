using BlitzStatistics.Model;
using BlitzStatistics.Services.CalculationHelpers;
using BlitzStatistics.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WotBlitzStatician.Model;
using WotBlitzStatician.WotApiClient;

namespace BlitzStatistics.Services
{
	public class WargamingService : IWargamingService
	{
		/// <summary>
		/// For simplicity, I have taken the Wargaming API interaction stuff as a nuget from my other project
		/// The source code of these nugets is here: https://github.com/DrMboga/WotBlitzStatician
		/// </summary>
		private readonly IWargamingApiClient _wargamingApiClient;
		private readonly IWargamingDictionaries _wargamingDictionaries;

		public WargamingService(
			IWargamingApiClient wargamingApiClient,
			IWargamingDictionaries wargamingDictionaries)
		{
			_wargamingApiClient = wargamingApiClient;
			_wargamingDictionaries = wargamingDictionaries;
		}

		public Task<List<WotBlitzStatician.Model.AccountInfo>> FindAccounts(string searchString)
		{
			return _wargamingApiClient.FindAccountAsync(searchString);
		}

		public async Task<AccountInfoDto> GetAccountInfo(long accountId)
		{
			// For simplicity I have put the whole logic of getting and calculating account information here.
			// This method need to be refactored. It should be separated by small steps, and each step should be tied into the pipe, 
			// And here shold be called just one method of that piped operation. Same operation is implemented here https://github.com/DrMboga/WotBlitzStatician
			await LoadDictionaries();
			var accountInfoMapper = new AccountInfoToDtoMapper();
			var accountStatistics = await _wargamingApiClient.GetAccountInfoAllStatisticsAsync(accountId, null);

			if (accountStatistics?.AccountInfoStatistics == null
				|| accountStatistics.AccountInfoStatistics.Count == 0
				|| accountStatistics.AccountInfoStatistics.Single().Battles == 0)
			{
				throw new ArgumentException("This player haven't played World Of Tanks Blitz yet");
			}

			// Getting Clan info step
			accountStatistics.AccountClanInfo = await _wargamingApiClient.GetAccountClanInfoAsync(accountId);

			// Getting Achevements step
			var accountInfoAchievements = await _wargamingApiClient.GetAccountAchievementsAsync(accountId);

			// Getting player's tanks list step
			var tanks = await _wargamingApiClient.GetTanksStatisticsAsync(accountId, null);

			// Calculating middle tier step
			accountStatistics.AccountInfoStatistics.Single().CalculateMiddleTier(
				tanks,
				_wargamingDictionaries.VehicleEncyclopedia.GetTanksTires());

			// Calculating Wn7 effectivity parameter
			accountStatistics.AccountInfoStatistics.Single().CalculateWn7();

			// Mapping result to the viewmodel dto
			var returningAccount = accountInfoMapper.Map(accountStatistics);

			// Getting meaningful achievements for the view
			returningAccount.Achievements = MapMeaningfulAchievements(accountInfoAchievements);

			// Getting top 3 tanks
			returningAccount.TopTanks = GetTopTanks(tanks, 3);

				return returningAccount;
		}

		private async Task LoadDictionaries()
		{
			if (_wargamingDictionaries.AreDictionariesCashed)
			{
				return;
			}
			var (languages, nations, vehicleTypes, achievemntSection, clanRoles) = await _wargamingApiClient.GetStaticDictionariesAsync().ConfigureAwait(false);

			var achievementsDictionary = await _wargamingApiClient.GetAchievementsDictionaryAsync().ConfigureAwait(false);

			var vehicles = await _wargamingApiClient.GetWotEncyclopediaVehiclesAsync().ConfigureAwait(false);

			_wargamingDictionaries.SaveToCache(
				nations,
				vehicleTypes,
				achievementsDictionary,
				vehicles);
		}

		private List<AchievementDto> MapMeaningfulAchievements(List<AccountInfoAchievement> allAccountAchievenments)
		{
			return _wargamingDictionaries
				.DictionaryAchievement
				.Where(a => a.Section == "epic" || a.AchievementId == "markOfMastery")
				.Join(allAccountAchievenments, d => d.AchievementId, a => a.AchievementId,
					(d, a) => new AchievementDto
					{
						AchievementId = d.AchievementId,
						Section = d.Section,
						Name = $"{d.Name}{Environment.NewLine}{d.Description}",
						Image = d.Image,
						Count = a.Count
					})
				.ToList();
		}
		private List<AccountTankDto> GetTopTanks(List<AccountTankStatistics> tanks, int topCount)
		{
			return tanks
				.OrderByDescending(t => t.Battles)
				.ThenByDescending(t => t.Wins)
				.ThenByDescending(t => t.DamageDealt)
				.ThenByDescending(t => t.Xp)
				.Take(topCount)
				.Join(_wargamingDictionaries.VehicleEncyclopedia, t => t.TankId, v => v.TankId,
					(t, v) => new AccountTankDto
					{
						TankId = t.TankId,
						Battles = t.Battles,
						MarkOfMastery = t.MarkOfMastery,
						WinRate = t.Battles == 0 ? 0m : Convert.ToDecimal(100 * t.Wins) / t.Battles,
						AvgDamage = t.Battles == 0 ? 0m : Convert.ToDecimal(t.DamageDealt) / t.Battles,
						AvgXp = t.Battles == 0 ? 0m : Convert.ToDecimal(t.Xp) / t.Battles,
						IsPremium = v.IsPremium,
						Name = v.Name,
						Nation = v.Nation,
						Type = v.Type,
						PreviewImageUrl = v.PreviewImageUrl,
						Tier = Convert.ToInt32(v.Tier),
						TierRoman = v.Tier.ToRomanNumeral()
					})
				.ToList();
		}
	}
}
