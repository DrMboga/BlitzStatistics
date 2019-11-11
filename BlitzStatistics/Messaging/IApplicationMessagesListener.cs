using System.Threading.Tasks;

namespace BlitzStatistics.Messaging
{
	public interface IApplicationMessagesListener<in TMessage> where TMessage : IApplicationMessage
	{
		Task ListenTo(TMessage message);
	}
}
