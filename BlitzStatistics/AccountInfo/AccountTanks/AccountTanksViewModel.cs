using BlitzStatistics.Model;
using BlitzStatistics.WpfHelpers;
using System.Collections.ObjectModel;

namespace BlitzStatistics.AccountInfo.AccountTanks
{
	public class AccountTanksViewModel : ViewModelBase
	{
		private ObservableCollection<AccountTankDto> _accountTanks;

		public ObservableCollection<AccountTankDto> AccountTanks
		{
			get { return _accountTanks; }
			set
			{
				_accountTanks = value;
				OnPropertyChanged();
			}
		}

	}
}
