using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using System.Collections.ObjectModel;

namespace BlitzStatistics.AccountsSearchHistory
{
	public interface IAccountsSearchHistoryViewModel :
		IApplicationMessagesListener<ShowFoundAccountInfoMessage>,
		IApplicationMessagesListener<InitializeMessage>
	{
		ObservableCollection<WotBlitzStatician.Model.AccountInfo> Accounts { get; set; }
		WotBlitzStatician.Model.AccountInfo SelectedAccount { get; set; }
	}
}
