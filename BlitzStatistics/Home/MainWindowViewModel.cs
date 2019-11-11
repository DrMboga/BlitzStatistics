using BlitzStatistics.AccountInfo;
using BlitzStatistics.AccountsSearch;
using BlitzStatistics.AccountsSearchHistory;
using BlitzStatistics.WpfHelpers;

namespace BlitzStatistics.Home
{
	public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
	{
		public MainWindowViewModel(
			IAccountsSearchViewModel accountsSearchViewModel,
			IAccountsSearchHistoryViewModel accountsSearchHistoryViewModel,
			IAccountInfoViewModel accountInfoViewModel
			)
		{
			AccountsSearchViewModel = accountsSearchViewModel;
			AccountsSearchHistoryViewModel = accountsSearchHistoryViewModel;
			AccountInfoViewModel = accountInfoViewModel;
		}

		public IAccountsSearchViewModel AccountsSearchViewModel { get; }
		public IAccountsSearchHistoryViewModel AccountsSearchHistoryViewModel { get; }
		public IAccountInfoViewModel AccountInfoViewModel { get; }
	}
}
