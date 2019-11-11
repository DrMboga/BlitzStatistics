using BlitzStatistics.Messaging;

namespace BlitzStatistics.Messages
{
	public class ShowFoundAccountInfoMessage : IApplicationMessage
	{
		public ShowFoundAccountInfoMessage(WotBlitzStatician.Model.AccountInfo accountInfo)
		{
			AccountInfo = accountInfo;
		}

		public WotBlitzStatician.Model.AccountInfo AccountInfo { get; }
	}
}
