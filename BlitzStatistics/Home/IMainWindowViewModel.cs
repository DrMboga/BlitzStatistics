using BlitzStatistics.AccountInfo;
using BlitzStatistics.AccountsSearch;
using BlitzStatistics.AccountsSearchHistory;

namespace BlitzStatistics.Home
{
	public interface IMainWindowViewModel
	{
		IAccountsSearchViewModel AccountsSearchViewModel { get; }
		IAccountsSearchHistoryViewModel AccountsSearchHistoryViewModel { get; }
		IAccountInfoViewModel AccountInfoViewModel { get; }

	}
}
