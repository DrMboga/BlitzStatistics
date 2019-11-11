using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BlitzStatistics.AccountsSearch
{
	public interface IAccountsSearchViewModel
	{
		string SearchInput { get; set; }
		ObservableCollection<WotBlitzStatician.Model.AccountInfo> Accounts { get; set; }
		WotBlitzStatician.Model.AccountInfo SelectedAccount { get; set; }
		ICommand ClearSearchCommand { get; }
	}
}
