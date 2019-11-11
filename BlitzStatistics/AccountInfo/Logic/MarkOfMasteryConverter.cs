using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WotBlitzStatician.Model;

namespace BlitzStatistics.AccountInfo.Logic
{
	public class MarkOfMasteryConverter : IValueConverter
	{
		private const string MasteryPath = "/Assets/Mastery/vehicle.mark.master.scale-200.png";
		private const string FirstClassPath = "/Assets/Mastery/vehicle.mark.first.scale-200.png";
		private const string SecondClassPath = "/Assets/Mastery/vehicle.mark.second.scale-200.png";
		private const string ThirdClassPath = "/Assets/Mastery/vehicle.mark.third.scale-200.png";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string path = string.Empty;
			if(value is MarkOfMastery mastery)
			{
				switch (mastery)
				{
					case MarkOfMastery.Rank3:
						path = ThirdClassPath;
						break;
					case MarkOfMastery.Rank2:
						path = SecondClassPath;
						break;
					case MarkOfMastery.Rank1:
						path = FirstClassPath;
						break;
					case MarkOfMastery.Master:
						path = MasteryPath;
						break;
				}
			}
			return string.IsNullOrWhiteSpace(path) ? null :
				new BitmapImage(new Uri($"pack://application:,,,{path}"));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
