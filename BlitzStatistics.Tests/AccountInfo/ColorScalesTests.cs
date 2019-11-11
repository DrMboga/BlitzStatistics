using BlitzStatistics.AccountInfo.Logic;
using BlitzStatistics.Model;
using Xunit;

namespace BlitzStatistics.Tests.AccountInfo
{
	public class ColorScalesTests
	{

		[Theory]
		[InlineData(44, ParameterScale.VeryBad)]
		[InlineData(46, ParameterScale.Bad)]
		[InlineData(48, ParameterScale.BelowAverage)]
		[InlineData(51, ParameterScale.Average)]
		[InlineData(53, ParameterScale.Good)]
		[InlineData(55, ParameterScale.VeryGood)]
		[InlineData(58, ParameterScale.Great)]
		[InlineData(63, ParameterScale.Unicum)]
		[InlineData(66, ParameterScale.SuperUnicum)]
		public void ShouldCalculateRightWinrateScales(double winrate, ParameterScale expectedScale)
		{
			Assert.Equal(expectedScale, winrate.WinrateScale());
		}

		[Theory]
		[InlineData(499, ParameterScale.VeryBad)]
		[InlineData(600, ParameterScale.Bad)]
		[InlineData(800, ParameterScale.BelowAverage)]
		[InlineData(1000, ParameterScale.Average)]
		[InlineData(1300, ParameterScale.Good)]
		[InlineData(1500, ParameterScale.VeryGood)]
		[InlineData(1800, ParameterScale.Great)]
		[InlineData(2000, ParameterScale.Unicum)]
		[InlineData(3000, ParameterScale.SuperUnicum)]
		public void ShouldCalculateRightWn7Scales(double wn7, ParameterScale expectedScale)
		{
			Assert.Equal(expectedScale, wn7.Wn7Scale());
		}
	}
}
