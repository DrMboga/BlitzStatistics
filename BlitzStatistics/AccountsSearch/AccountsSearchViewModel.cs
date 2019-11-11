using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Services;
using BlitzStatistics.WpfHelpers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BlitzStatistics.AccountsSearch
{
	public class AccountsSearchViewModel : ViewModelBase, IAccountsSearchViewModel
	{
		const int _minSymbolsToSearch = 5;
		private string _searchInput;
		private readonly IWargamingService _wargamingService;
		private readonly IApplicationMessagesDispatcher _applicationMessagesDispatcher;

		private ObservableCollection<WotBlitzStatician.Model.AccountInfo> _accounts;
		private WotBlitzStatician.Model.AccountInfo _selectedAccount;

		public AccountsSearchViewModel(
			IWargamingService wargamingService,
			IApplicationMessagesDispatcher applicationMessagesDispatcher)
		{
			ClearSearchCommand = new RelayCommand(ClearSearch);
			_wargamingService = wargamingService;
			_applicationMessagesDispatcher = applicationMessagesDispatcher;
		}

		public string SearchInput
		{
			get { return _searchInput; }
			set
			{
				_searchInput = value;
				OnPropertyChanged();
				FindAccounts();
			}
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

		public ICommand ClearSearchCommand { get; }

		public WotBlitzStatician.Model.AccountInfo SelectedAccount
		{
			get { return _selectedAccount; }
			set
			{
				_selectedAccount = value;
				OnPropertyChanged();
				_applicationMessagesDispatcher.Publish(new ShowFoundAccountInfoMessage(
					_selectedAccount));
			}
		}


		private void FindAccounts()
		{
			ClearAccountsList();

			if (SearchInput == null || SearchInput.Length < _minSymbolsToSearch)
			{
				return;
			}
			// ToDo: Maybe for each typed letter add to the dictionary pair with word as key and cancellation token as value,
			// And cancel the searching of previous word
			_wargamingService.FindAccounts(SearchInput).ContinueWith((result) =>
			{
				if (result.IsCompleted)
				{
					try
					{
						Accounts = new ObservableCollection<WotBlitzStatician.Model.AccountInfo>(result.GetAwaiter().GetResult());
					}
					catch (Exception ex)
					{
						_applicationMessagesDispatcher.Publish(new ApplicationErrorMessage("Unable to find Wargaming accounts", ex));
					}
				}
			});
		}

		private void ClearSearch(object obj)
		{
			SearchInput = null;
			ClearAccountsList();
		}

		private void ClearAccountsList()
		{
			Accounts = null;
		}


	}
}
