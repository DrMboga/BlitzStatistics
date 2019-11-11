using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BlitzStatistics.AccountInfo.Logic
{
	public class VehicleTypeToIconPathConverter : IMultiValueConverter
	{
		private const string AtIconPath = "/Assets/Vehicles/vehicle.class.atspg.small.scale-200.png";
		private const string HeavyIconPath = "/Assets/Vehicles/vehicle.class.heavy.small.scale-200.png";
		private const string MediomIconPath = "/Assets/Vehicles/vehicle.class.medium.small.scale-200.png";
		private const string LightIconPath = "/Assets/Vehicles/vehicle.class.light.small.scale-200.png";
		private const string PremAtIconPath = "/Assets/Vehicles/vehicle.class.atspg.premium.small.scale-200.png";
		private const string PremHeavyIconPath = "/Assets/Vehicles/vehicle.class.heavy.premium.small.scale-200.png";
		private const string PremMediomIconPath = "/Assets/Vehicles/vehicle.class.medium.premium.small.scale-200.png";
		private const string PremLightIconPath = "/Assets/Vehicles/vehicle.class.light.premium.small.scale-200.png";

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var path = string.Empty;

			if (values?.Length == 2 &&
				values[0] is string tankType &&
				values[1] is bool isPrem)
			{
				switch (tankType)
				{
					case "AT-SPG" when isPrem:
						path = PremAtIconPath;
						break;
					case "AT-SPG" when !isPrem:
						path = AtIconPath;
						break;
					case "heavyTank" when isPrem:
						path = PremHeavyIconPath;
						break;
					case "heavyTank" when !isPrem:
						path = HeavyIconPath;
						break;
					case "mediumTank" when isPrem:
						path = PremMediomIconPath;
						break;
					case "mediumTank" when !isPrem:
						path = MediomIconPath;
						break;
					case "lightTank" when isPrem:
						path = PremLightIconPath;
						break;
					case "lightTank" when !isPrem:
						path = LightIconPath;
						break;
				}
			}
			return string.IsNullOrWhiteSpace(path) ? null : 
				new BitmapImage(new Uri($"pack://application:,,,{path}"));
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
