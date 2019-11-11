using BlitzStatistics.Model;
using System.Collections.Generic;

namespace BlitzStatistics.AccountInfo.Logic
{
	public static class AccountInfoHelper
	{
		public static List<AccountViewProperty> GetAccountProperties(this AccountInfoDto accountInfo)
		{
			var accountProperties = new List<AccountViewProperty>();
			// BattlesCount
			accountProperties.Add(new AccountViewProperty
			{
				Caption = "Battles",
				IconRelativePath = "/Assets/battles.scale-200.png",
				Scale = ParameterScale.Undefined,
				Value = accountInfo.Battles.ToString("N0")
			});
			// WinRate
			accountProperties.Add(new AccountViewProperty
			{
				Caption = "Winrate",
				IconRelativePath = "/Assets/wins.scale-200.png",
				Scale = accountInfo.Winrate.WinrateScale(),
				Value = $"{accountInfo.Winrate:N2} %"
			});
			// AvgDamage
			accountProperties.Add(new AccountViewProperty
			{
				Caption = "Average damage",
				IconRelativePath = "/Assets/damage.scale-200.png",
				Scale = ParameterScale.Undefined,
				Value = accountInfo.AvgDamage.ToString("N0")
			});
			// Wn7
			accountProperties.Add(new AccountViewProperty
			{
				Caption = "Wn7",
				IconRelativePath = "/Assets/details.global.rating.scale-200.png",
				Scale = accountInfo.Wn7.Wn7Scale(),
				Value = accountInfo.Wn7.ToString("N0")
			});
			// AvgXP
			accountProperties.Add(new AccountViewProperty
			{
				Caption = "Average XP",
				IconRelativePath = "/Assets/xp.avg.scale-200.png",
				Scale = ParameterScale.Undefined,
				Value = accountInfo.AvgXp.ToString("N0")
			});

			return accountProperties;
		}

	}
}
