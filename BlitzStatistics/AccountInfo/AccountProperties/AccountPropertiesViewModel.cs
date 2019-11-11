using BlitzStatistics.Model;
using BlitzStatistics.WpfHelpers;
using System.Collections.ObjectModel;

namespace BlitzStatistics.AccountInfo.AccountProperties
{
	public class AccountPropertiesViewModel : ViewModelBase
	{
		private ObservableCollection<AccountViewProperty> _accountViewProperties;
		public ObservableCollection<AccountViewProperty> AccountProperties
		{
			get { return _accountViewProperties; }
			set
			{
				_accountViewProperties = value;
				OnPropertyChanged();
			}
		}

	}
}
