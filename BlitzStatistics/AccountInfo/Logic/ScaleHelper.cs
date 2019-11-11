using BlitzStatistics.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzStatistics.AccountInfo.Logic
{
	public static class ScaleHelper
	{
		public static ParameterScale WinrateScale(this double winrate)
		{
			if (winrate <= 44.99)
				return ParameterScale.VeryBad;
			if (winrate <= 46.99)
				return ParameterScale.Bad;
			if (winrate <= 48.99)
				return ParameterScale.BelowAverage;
			if (winrate <= 51.99)
				return ParameterScale.Average;
			if (winrate <= 53.99)
				return ParameterScale.Good;
			if (winrate <= 55.99)
				return ParameterScale.VeryGood;
			if (winrate <= 59.99)
				return ParameterScale.Great;
			if (winrate <= 64.99)
				return ParameterScale.Unicum;
			return ParameterScale.SuperUnicum;
		}

		public static ParameterScale Wn7Scale(this double wn7)
		{
			if (wn7 <= 499.99)
				return ParameterScale.VeryBad;
			if (wn7 <= 699.99)
				return ParameterScale.Bad;
			if (wn7 <= 899.99)
				return ParameterScale.BelowAverage;
			if (wn7 <= 1099.99)
				return ParameterScale.Average;
			if (wn7 <= 1349.99)
				return ParameterScale.Good;
			if (wn7 <= 1549.99)
				return ParameterScale.VeryGood;
			if (wn7 <= 1849.99)
				return ParameterScale.Great;
			if (wn7 <= 2049.99)
				return ParameterScale.Unicum;
			return ParameterScale.SuperUnicum;
		}
	}
}
