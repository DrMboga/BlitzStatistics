using System;
using System.Collections.Generic;

namespace BlitzStatistics.Model
{
	public class AccountInfoDto
	{
		public long AccountId { get; set; }
		public string NickName { get; set; }
		public DateTime? AccountCreatedAt { get; set; }
		public DateTime? LastBattleTime { get; set; }
		public long Battles { get; set; }
		public double Wn7 { get; set; }
		public double AvgTier { get; set; }
		public double Winrate { get; set; }
		public double AvgDamage { get; set; }
		public double AvgXp { get; set; }
		public IEnumerable<AchievementDto> Achievements { get; set; }
		public IEnumerable<AccountTankDto> TopTanks { get; set; }
	}
}
