using BlitzStatistics.AccountsSearch;
using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Xunit;

namespace BlitzStatistics.Tests.AccountsSearch
{
	public class AccountsSearchViewModelTests
	{
		const int minSymbolsToSearch = 5;
		private IAccountsSearchViewModel _accountsSearchViewModel;
		private Mock<IWargamingService> _wargamingServiceMock;
		private Mock<IApplicationMessagesDispatcher> _applicationMessageDicpatcherMock;

		public AccountsSearchViewModelTests()
		{
			_wargamingServiceMock = new Mock<IWargamingService>();
			_applicationMessageDicpatcherMock = new Mock<IApplicationMessagesDispatcher>();
			_accountsSearchViewModel = new AccountsSearchViewModel(
				_wargamingServiceMock.Object,
				_applicationMessageDicpatcherMock.Object);
			_wargamingServiceMock.Setup(s => s.FindAccounts(It.IsAny<string>()))
				.ReturnsAsync(new List<WotBlitzStatician.Model.AccountInfo>
				{
					new WotBlitzStatician.Model.AccountInfo {AccountId = 1, NickName = "First"},
					new WotBlitzStatician.Model.AccountInfo {AccountId = 2, NickName = "Second"}
				});
		}

		[Fact]
		public void ShouldLoadAccountsWhenSearchStringGreaterThanFiveSymbols()
		{
			string searchString = new string('a', minSymbolsToSearch);
			_accountsSearchViewModel.SearchInput = searchString;

			_wargamingServiceMock.Verify(s => s.FindAccounts(searchString), Times.Once);

			// ToDo: Accounts list fills asyncronously, so wait it for a while
			Thread.Sleep(500);

			Assert.NotNull(_accountsSearchViewModel.Accounts);
			Assert.Collection(_accountsSearchViewModel.Accounts,
				(item) =>
					{
						Assert.Equal(1, item.AccountId);
						Assert.Equal("First", item.NickName);
					},
				(item) =>
					{
						Assert.Equal(2, item.AccountId);
						Assert.Equal("Second", item.NickName);
					}
				);
		}

		[Fact]
		public void ShouldLoadAccountsWhenSearchStringLessThanFiveSymbols()
		{
			string searchString = new string('a', minSymbolsToSearch - 1);

			_accountsSearchViewModel.Accounts = new ObservableCollection<WotBlitzStatician.Model.AccountInfo>
			{
				new WotBlitzStatician.Model.AccountInfo()
			};

			_accountsSearchViewModel.SearchInput = searchString;

			_wargamingServiceMock.Verify(s => s.FindAccounts(searchString), Times.Never);

			Assert.Null(_accountsSearchViewModel.Accounts);
		}

		[Fact]
		public void ShouldClearSearchTextWhenClearButtonPushed()
		{
			_accountsSearchViewModel.SearchInput = new string('a', minSymbolsToSearch);

			_accountsSearchViewModel.ClearSearchCommand.Execute(null);

			Assert.Null(_accountsSearchViewModel.SearchInput);
		}

		[Fact]
		public void ShouldClearItemsWhenClearButtonPushed()
		{
			_accountsSearchViewModel.SearchInput = new string('a', minSymbolsToSearch);

			_wargamingServiceMock.Verify(s => s.FindAccounts(_accountsSearchViewModel.SearchInput), Times.Once);

			// ToDo: Accounts list fills asyncronously, so wait it for a while
			Thread.Sleep(500);

			Assert.NotNull(_accountsSearchViewModel.Accounts);
			Assert.NotEmpty(_accountsSearchViewModel.Accounts);

			_accountsSearchViewModel.ClearSearchCommand.Execute(null);
			_wargamingServiceMock.Verify(s => s.FindAccounts(_accountsSearchViewModel.SearchInput), Times.Never);

			Assert.Null(_accountsSearchViewModel.Accounts);
		}

		[Fact]
		public void ShouldFireEventWhenAccountSelected()
		{
			var secondAccountInfo = new WotBlitzStatician.Model.AccountInfo
			{
				AccountId = 42,
				NickName = "Yeah"
			};

			long accountIdFromMessage = 0;
			_applicationMessageDicpatcherMock.Setup(m => m.Publish(It.IsAny<ShowFoundAccountInfoMessage>()))
				.Callback<ShowFoundAccountInfoMessage>(message => accountIdFromMessage = message.AccountInfo.AccountId);

			_accountsSearchViewModel.Accounts = new ObservableCollection<WotBlitzStatician.Model.AccountInfo>
			{
				new WotBlitzStatician.Model.AccountInfo(),
				secondAccountInfo
			};

			_accountsSearchViewModel.SelectedAccount = secondAccountInfo;

			_applicationMessageDicpatcherMock.Verify(m => m.Publish(It.IsAny<ShowFoundAccountInfoMessage>()), Times.Once);
			Assert.Equal(secondAccountInfo.AccountId, accountIdFromMessage);
		}

		[Fact]
		public void ShoudDispatchApplicationErrorWhenWargamingSearchErrorOccurs()
		{
			string searchString = new string('a', minSymbolsToSearch);
			_wargamingServiceMock.Setup(s => s.FindAccounts(searchString))
				.ThrowsAsync(new Exception());

			_accountsSearchViewModel.SearchInput = searchString;

			Thread.Sleep(500);

			_applicationMessageDicpatcherMock.Verify(m => m.Publish(It.IsAny<ApplicationErrorMessage>()), Times.Once);
		}
	}
}
