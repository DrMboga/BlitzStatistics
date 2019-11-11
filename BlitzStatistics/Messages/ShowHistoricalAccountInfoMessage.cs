using BlitzStatistics.Messaging;

namespace BlitzStatistics.Messages
{
	public class ShowHistoricalAccountInfoMessage : IApplicationMessage
	{
		public ShowHistoricalAccountInfoMessage(WotBlitzStatician.Model.AccountInfo accountInfo)
		{
			AccountInfo = accountInfo;
		}

		public WotBlitzStatician.Model.AccountInfo AccountInfo { get; }
	}
}
