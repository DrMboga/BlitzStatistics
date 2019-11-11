using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlitzStatistics.AccountInfo
{
	public class AccountInfoStateToVosibilityConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var viewState = (AccountInfoViewState)value;
			string panel = parameter == null ? string.Empty : parameter.ToString();
			switch(viewState)
			{
				case AccountInfoViewState.Initial:
					if(panel == "InitialPanel")
					{
						return Visibility.Visible;
					}
					return Visibility.Collapsed;
				case AccountInfoViewState.Loading:
					if(panel == "LoadingPanel")
					{
						return Visibility.Visible;
					}
					return Visibility.Collapsed;
				case AccountInfoViewState.ShowingAccountInfo:
					if(panel == "AccountInfoPanel")
					{
						return Visibility.Visible;
					}
					return Visibility.Collapsed;
				default:
					return Visibility.Visible;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
