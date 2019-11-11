using System;
using System.Globalization;
using System.Windows.Data;

namespace BlitzStatistics.AccountInfo.Logic
{
	public class NationToFlagPathConverter : IValueConverter
	{
		private const string DefaultNationPath = "/Assets/Flags/encyclopedia.nation.unknown.scale-200.png";
		private const string BritainNationPath = "/Assets/Flags/encyclopedia.nation.britain.scale-200.png";
		private const string ChinaNationPath = "/Assets/Flags/encyclopedia.nation.china.scale-200.png";
		private const string FranceNationPath = "/Assets/Flags/encyclopedia.nation.france.scale-200.png";
		private const string GermanyNationPath = "/Assets/Flags/encyclopedia.nation.germany.scale-200.png";
		private const string JapanNationPath = "/Assets/Flags/encyclopedia.nation.japan.scale-200.png";
		private const string UsaNationPath = "/Assets/Flags/encyclopedia.nation.usa.scale-200.png";
		private const string UssrNationPath = "/Assets/Flags/encyclopedia.nation.ussr.scale-200.png";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string nation)
			{
				switch (nation)
				{
					case "uk":
						return BritainNationPath;
					case "china":
						return ChinaNationPath;
					case "france":
						return FranceNationPath;
					case "germany":
						return GermanyNationPath;
					case "japan":
						return JapanNationPath;
					case "usa":
						return UsaNationPath;
					case "ussr":
						return UssrNationPath;
					case "european":
					default:
						return DefaultNationPath;
				}
			}
			return DefaultNationPath;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
