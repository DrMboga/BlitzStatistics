using BlitzStatistics.AccountsSearchHistory;
using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Services;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace BlitzStatistics.Tests.AccountsSearchHistory
{
	public class AccountsSearchHistoryViewModelTests
	{
		private Mock<IApplicationMessagesDispatcher> _applicationMessageDicpatcherMock;
		private IAccountsSearchHistoryViewModel _accountsSearchHistoryViewModel;
		private Mock<ISearchHisoryDataAdapter> _searchHistoryDataAdapterMock;

		public AccountsSearchHistoryViewModelTests()
		{
			_applicationMessageDicpatcherMock = new Mock<IApplicationMessagesDispatcher>();
			_searchHistoryDataAdapterMock = new Mock<ISearchHisoryDataAdapter>();
			_accountsSearchHistoryViewModel = new AccountsSearchHistoryViewModel(
				_applicationMessageDicpatcherMock.Object,
				_searchHistoryDataAdapterMock.Object);
		}

		[Fact]
		public void ShouldAddAccountToListWhenOlListenMessageFired()
		{
			var newAccount = new WotBlitzStatician.Model.AccountInfo
			{
				AccountId = 42,
				NickName = "Yeah"
			};

			Assert.Empty(_accountsSearchHistoryViewModel.Accounts);

			_accountsSearchHistoryViewModel.ListenTo(new ShowFoundAccountInfoMessage(newAccount));

			Assert.NotEmpty(_accountsSearchHistoryViewModel.Accounts);
			Assert.Collection(_accountsSearchHistoryViewModel.Accounts,
				(item) =>
				{
					Assert.Equal(newAccount.AccountId, item.AccountId);
					Assert.Equal(newAccount.NickName, item.NickName);
				});
		}

		[Fact]
		public void ShouldAddAccountToListWhenOlListenMessageFiredOnlyDistinctAccounts()
		{
			var newAccount = new WotBlitzStatician.Model.AccountInfo
			{
				AccountId = 42,
				NickName = "Yeah"
			};

			Assert.Empty(_accountsSearchHistoryViewModel.Accounts);

			_accountsSearchHistoryViewModel.ListenTo(new ShowFoundAccountInfoMessage(newAccount));
			_accountsSearchHistoryViewModel.ListenTo(new ShowFoundAccountInfoMessage(newAccount));

			Assert.NotEmpty(_accountsSearchHistoryViewModel.Accounts);
			Assert.Single(_accountsSearchHistoryViewModel.Accounts);
		}

		[Fact]
		public void ShouldFireEventWhenAccountSelected()
		{
			var accountInfo = new WotBlitzStatician.Model.AccountInfo
			{
				AccountId = 42,
				NickName = "Yeah"
			};

			long accountIdFromMessage = 0;
			_applicationMessageDicpatcherMock.Setup(m => m.Publish(It.IsAny<ShowHistoricalAccountInfoMessage>()))
				.Callback<ShowHistoricalAccountInfoMessage>(message => accountIdFromMessage = message.AccountInfo.AccountId);

			_accountsSearchHistoryViewModel.Accounts = new ObservableCollection<WotBlitzStatician.Model.AccountInfo>
			{
				new WotBlitzStatician.Model.AccountInfo(),
				accountInfo
			};

			_accountsSearchHistoryViewModel.SelectedAccount = accountInfo;

			_applicationMessageDicpatcherMock.Verify(m => m.Publish(It.IsAny<ShowHistoricalAccountInfoMessage>()), Times.Once);
			Assert.Equal(accountInfo.AccountId, accountIdFromMessage);
		}

		[Fact]
		public void ShouldLoadAccountsOnInitializationMessage()
		{
			var accountInfosFromStore = new List<WotBlitzStatician.Model.AccountInfo>
			{
				new WotBlitzStatician.Model.AccountInfo {AccountId = 42, NickName = "Yeah"},
				new WotBlitzStatician.Model.AccountInfo {AccountId = 12, NickName = "Oyeah"}
			};
			_searchHistoryDataAdapterMock.Setup(d => d.LoadAccountsHistory())
				.ReturnsAsync(accountInfosFromStore);

			_accountsSearchHistoryViewModel.ListenTo(new InitializeMessage());

			_searchHistoryDataAdapterMock.Verify(d => d.LoadAccountsHistory(), Times.Once);

			Assert.NotNull(_accountsSearchHistoryViewModel.Accounts);
			Assert.Equal(2, _accountsSearchHistoryViewModel.Accounts.Count);
			Assert.Collection(_accountsSearchHistoryViewModel.Accounts,
				(item) =>
				{
					Assert.Equal(accountInfosFromStore[0].AccountId, item.AccountId);
					Assert.Equal(accountInfosFromStore[0].NickName, item.NickName);
				},
				(item) =>
				{
					Assert.Equal(accountInfosFromStore[1].AccountId, item.AccountId);
					Assert.Equal(accountInfosFromStore[1].NickName, item.NickName);
				});
		}

		[Fact]
		public void ShouldRewriteAccountsInStoreWhenNewAccountAdded()
		{
			var accountToSave = new WotBlitzStatician.Model.AccountInfo { AccountId = 42, NickName = "Yeah" };

			Assert.Empty(_accountsSearchHistoryViewModel.Accounts);

			_accountsSearchHistoryViewModel.ListenTo(new ShowFoundAccountInfoMessage(accountToSave));

			_searchHistoryDataAdapterMock.Verify(
				d => d.RewriteAccountsHistory(It.IsAny<List<WotBlitzStatician.Model.AccountInfo>>()),
				Times.Once);
		}

		// ToDo: Errors

	}
}
