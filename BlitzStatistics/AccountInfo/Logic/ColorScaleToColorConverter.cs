using BlitzStatistics.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BlitzStatistics.AccountInfo.Logic
{
	public class ColorScaleToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string defaultColor = "#405365";
			if (value is ParameterScale scale)
			{
				switch (scale)
				{
					case ParameterScale.VeryBad:
						return "#000000";
					case ParameterScale.Bad:
						return "#cd3333";
					case ParameterScale.BelowAverage:
						return "#d77900";
					case ParameterScale.Average:
						return "#d7b600";
					case ParameterScale.Good:
						return "#6d9521";
					case ParameterScale.VeryGood:
						return "#4c762e";
					case ParameterScale.Great:
						return "#4a92b7";
					case ParameterScale.Unicum:
						return "#83579d";
					case ParameterScale.SuperUnicum:
						return "#5a3175";
					default:
						return defaultColor;
				}
			}

			return defaultColor;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
