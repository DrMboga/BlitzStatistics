namespace BlitzStatistics.Messaging
{
	public interface IApplicationMessagesDispatcher
	{
		void Publish<TMessage>(TMessage message) where TMessage : IApplicationMessage;
	}
}
