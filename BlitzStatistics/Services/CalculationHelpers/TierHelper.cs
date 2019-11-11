using System;
using System.Collections.Generic;
using System.Linq;
using WotBlitzStatician.Model;

namespace BlitzStatistics.Services.CalculationHelpers
{
	public static class TierHelper
	{
		public static void CalculateMiddleTier(this AccountInfoStatistics account, IList<AccountTankStatistics> allTanks, Dictionary<long, double> tankTires)
		{
			double x = 0d;
			foreach (var tank in allTanks)
			{
				if (tankTires.ContainsKey(tank.TankId) && tank.Battles > 0)
				{
					x += tankTires[tank.TankId] * tank.Battles;
				}
			}
			if (account.Battles > 0)
				x /= account.Battles;
			account.AvgTier = x;
		}

		public static Dictionary<long, double> GetTanksTires(this IEnumerable<VehicleEncyclopedia> vehicles)
		{
			return vehicles.ToDictionary(v => v.TankId, v => Convert.ToDouble(v.Tier));
		}
	}
}
