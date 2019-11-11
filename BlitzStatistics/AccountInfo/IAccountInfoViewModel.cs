using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Model;

namespace BlitzStatistics.AccountInfo
{
	public interface IAccountInfoViewModel :
		IApplicationMessagesListener<ShowFoundAccountInfoMessage>,
		IApplicationMessagesListener<ShowHistoricalAccountInfoMessage>
	{
		AccountInfoViewState AccountInfoViewState { get; set; }
		AccountInfoDto AccountInfo { get; set; }
	}
}
