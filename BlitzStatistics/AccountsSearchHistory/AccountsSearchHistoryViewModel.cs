using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Services;
using BlitzStatistics.WpfHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzStatistics.AccountsSearchHistory
{
	public class AccountsSearchHistoryViewModel : ViewModelBase, IAccountsSearchHistoryViewModel
	{
		private readonly IApplicationMessagesDispatcher _applicationMessagesDispatcher;
		private readonly ISearchHisoryDataAdapter _searchHisoryDataAdapter;
		private WotBlitzStatician.Model.AccountInfo _selectedAccount;
		private ObservableCollection<WotBlitzStatician.Model.AccountInfo> _accounts;

		public AccountsSearchHistoryViewModel(
			IApplicationMessagesDispatcher applicationMessagesDispatcher,
			ISearchHisoryDataAdapter searchHisoryDataAdapter)
		{
			_applicationMessagesDispatcher = applicationMessagesDispatcher;
			_searchHisoryDataAdapter = searchHisoryDataAdapter;
			Accounts = new ObservableCollection<WotBlitzStatician.Model.AccountInfo>();
		}

		public ObservableCollection<WotBlitzStatician.Model.AccountInfo> Accounts
		{
			get { return _accounts; }
			set
			{
				_accounts = value;
				OnPropertyChanged();
			}
		}

		public WotBlitzStatician.Model.AccountInfo SelectedAccount
		{
			get { return _selectedAccount; }
			set
			{
				_selectedAccount = value;
				OnPropertyChanged();
				_applicationMessagesDispatcher.Publish(new ShowHistoricalAccountInfoMessage(
					_selectedAccount));
			}
		}

		public async Task ListenTo(ShowFoundAccountInfoMessage message)
		{
			if (Accounts.Any(a => a.AccountId == message.AccountInfo.AccountId))
			{
				return;
			}
			Accounts.Add(message.AccountInfo);
			try
			{
				await _searchHisoryDataAdapter.RewriteAccountsHistory(Accounts.ToList()).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				_applicationMessagesDispatcher.Publish(new ApplicationErrorMessage("Could not save accounts history to local storage", e));
			}
		}

		public async Task ListenTo(InitializeMessage message)
		{
			Accounts.Clear();
			try
			{
				(await _searchHisoryDataAdapter.LoadAccountsHistory().ConfigureAwait(false))
					?.ForEach(a => Accounts.Add(a));
			}
			catch (Exception e)
			{
				_applicationMessagesDispatcher.Publish(new ApplicationErrorMessage("Could not load accounts history from local storage", e));
			}
		}
	}
}
