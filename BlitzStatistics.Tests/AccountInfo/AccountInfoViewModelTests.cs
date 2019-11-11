using BlitzStatistics.AccountInfo;
using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlitzStatistics.Tests.AccountInfo
{
	public class AccountInfoViewModelTests
	{
		IAccountInfoViewModel _accountInfoViewModel;
		private Mock<IWargamingService> _wargamingServiceMock;
		private Mock<IApplicationMessagesDispatcher> _applicationMessageDicpatcherMock;

		public AccountInfoViewModelTests()
		{
			_wargamingServiceMock = new Mock<IWargamingService>();
			_applicationMessageDicpatcherMock = new Mock<IApplicationMessagesDispatcher>();
			_accountInfoViewModel = new AccountInfoViewModel(
				_wargamingServiceMock.Object,
				_applicationMessageDicpatcherMock.Object);
		}

		[Fact]
		public void ShouldSetInitialAccountViewStateBeforeLoad()
		{
			Assert.Equal(AccountInfoViewState.Initial, _accountInfoViewModel.AccountInfoViewState);
		}

		[Fact]
		public void ShouldSetLoadingInitialViewStateWhileAccountDataIsLoading()
		{
			// Delaying for a second
			_wargamingServiceMock.Setup(s => s.GetAccountInfo(It.IsAny<long>()))
				.ReturnsAsync(new Model.AccountInfoDto(), new TimeSpan(0, 0, 1));

			_accountInfoViewModel.ListenTo(new ShowFoundAccountInfoMessage(new WotBlitzStatician.Model.AccountInfo()));

			// While task executes, state should be 'Loading'
			Assert.Equal(AccountInfoViewState.Loading, _accountInfoViewModel.AccountInfoViewState);
		}

		[Fact]
		public void ShouldSetShowAccountViewStateWhenAccountDataHasBeenLoaded()
		{
			_wargamingServiceMock.Setup(s => s.GetAccountInfo(It.IsAny<long>()))
				.ReturnsAsync(new Model.AccountInfoDto());

			_accountInfoViewModel.ListenTo(new ShowFoundAccountInfoMessage(new WotBlitzStatician.Model.AccountInfo()));

			_wargamingServiceMock.Verify(s => s.GetAccountInfo(It.IsAny<long>()), Times.Once);

			Assert.Equal(AccountInfoViewState.ShowingAccountInfo, _accountInfoViewModel.AccountInfoViewState);
		}

		[Fact]
		public void ShouldLoadAccountDataOnShowFoundAcountMessage()
		{
			_wargamingServiceMock.Setup(s => s.GetAccountInfo(It.IsAny<long>()))
				.ReturnsAsync(new Model.AccountInfoDto());

			_accountInfoViewModel.ListenTo(new ShowFoundAccountInfoMessage(new WotBlitzStatician.Model.AccountInfo()));

			_wargamingServiceMock.Verify(s => s.GetAccountInfo(It.IsAny<long>()), Times.Once);
		}

		[Fact]
		public void ShouldLoadAccountDataOnShowHistoricalAccountMessage()
		{
			_wargamingServiceMock.Setup(s => s.GetAccountInfo(It.IsAny<long>()))
				.ReturnsAsync(new Model.AccountInfoDto());

			_accountInfoViewModel.ListenTo(new ShowHistoricalAccountInfoMessage(new WotBlitzStatician.Model.AccountInfo()));

			_wargamingServiceMock.Verify(s => s.GetAccountInfo(It.IsAny<long>()), Times.Once);
		}

		[Fact]
		public void ShoudDispatchApplicationErrorWhenWargamingErrorOccurs()
		{
			_wargamingServiceMock.Setup(s => s.GetAccountInfo(It.IsAny<long>()))
				.ThrowsAsync(new Exception());

			_accountInfoViewModel.ListenTo(new ShowFoundAccountInfoMessage(new WotBlitzStatician.Model.AccountInfo()));

			_applicationMessageDicpatcherMock.Verify(m => m.Publish(It.IsAny<ApplicationErrorMessage>()), Times.Once);
		}
	}
}
