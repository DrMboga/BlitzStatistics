using BlitzStatistics.AccountInfo.AccountProperties;
using BlitzStatistics.AccountInfo.AccountTanks;
using BlitzStatistics.AccountInfo.Achievements;
using BlitzStatistics.AccountInfo.Logic;
using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Model;
using BlitzStatistics.Services;
using BlitzStatistics.WpfHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlitzStatistics.AccountInfo
{
	public class AccountInfoViewModel : ViewModelBase, IAccountInfoViewModel
	{
		private readonly IWargamingService _wargamingService;
		private readonly IApplicationMessagesDispatcher _applicationMessagesDispatcher;
		private AccountInfoDto _accountInfo;
		private AccountInfoViewState _accountInfoViewState;
		private AccountPropertiesViewModel _accountPropertiesViewModel;
		private AccountTanksViewModel _accountTanksViewModel;
		private AchievementsViewModel _achievementsViewModel;

		public AccountInfoViewModel(
			IWargamingService wargamingService,
			IApplicationMessagesDispatcher applicationMessagesDispatcher)
		{
			_wargamingService = wargamingService;
			_applicationMessagesDispatcher = applicationMessagesDispatcher;
			AccountInfoViewState = AccountInfoViewState.Initial;
			AccountPropertiesViewModel = new AccountPropertiesViewModel();
			AccountTanksViewModel = new AccountTanksViewModel();
			AchievementsViewModel = new AchievementsViewModel();
		}

		public AccountInfoDto AccountInfo
		{
			get => _accountInfo;
			set
			{
				_accountInfo = value;
				OnPropertyChanged();
			}
		}

		public AccountInfoViewState AccountInfoViewState
		{
			get => _accountInfoViewState;
			set
			{
				_accountInfoViewState = value;
				OnPropertyChanged();
			}
		}

		public AccountPropertiesViewModel AccountPropertiesViewModel
		{
			get { return _accountPropertiesViewModel; }
			set
			{
				_accountPropertiesViewModel = value;
				OnPropertyChanged();
			}
		}

		public AccountTanksViewModel AccountTanksViewModel
		{
			get { return _accountTanksViewModel; }
			set
			{
				_accountTanksViewModel = value;
				OnPropertyChanged();
			}
		}

		public AchievementsViewModel AchievementsViewModel
		{
			get { return _achievementsViewModel; }
			set
			{
				_achievementsViewModel = value;
				OnPropertyChanged();
			}
		}


		public Task ListenTo(ShowFoundAccountInfoMessage message)
		{
			if(message?.AccountInfo == null)
			{
				return Task.CompletedTask;
			}
			return LoadAccount(message.AccountInfo.AccountId);
		}

		public Task ListenTo(ShowHistoricalAccountInfoMessage message)
		{
			if (message?.AccountInfo == null)
			{
				return Task.CompletedTask;
			}
			return LoadAccount(message.AccountInfo.AccountId);
		}

		private async Task LoadAccount(long accountId)
		{
			AccountInfoViewState = AccountInfoViewState.Loading;
			try
			{
				AccountInfo = await _wargamingService.GetAccountInfo(accountId);
				AccountPropertiesViewModel.AccountProperties = new ObservableCollection<AccountViewProperty>(AccountInfo.GetAccountProperties());
				AccountTanksViewModel.AccountTanks = new ObservableCollection<AccountTankDto>(AccountInfo.TopTanks);
				AchievementsViewModel.Achievements = new ObservableCollection<AchievementDto>(AccountInfo.Achievements);
				AccountInfoViewState = AccountInfoViewState.ShowingAccountInfo;
			}
			catch (Exception e)
			{
				_applicationMessagesDispatcher.Publish(new ApplicationErrorMessage($"Unable to load accountId {accountId}", e));
			}
		}
	}
}
