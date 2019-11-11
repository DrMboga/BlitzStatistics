using Autofac;
using System.Collections.Generic;

namespace BlitzStatistics.Messaging
{
	public class ApplicationMessagesDispatcher : IApplicationMessagesDispatcher
	{
		private readonly IComponentContext _componentContext;

		public ApplicationMessagesDispatcher(IComponentContext componentContext)
		{
			_componentContext = componentContext;
		}

		public void Publish<TMessage>(TMessage message) where TMessage : IApplicationMessage
		{
			var listeners = _componentContext.Resolve<IEnumerable<IApplicationMessagesListener<TMessage>>>();
			if(listeners != null)
			{
				foreach (var listener in listeners)
				{
					listener.ListenTo(message).ContinueWith(result => {});
				}
			}
		}
	}
}
