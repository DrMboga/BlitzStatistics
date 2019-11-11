using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using System.Threading.Tasks;
using System.Windows;

namespace BlitzStatistics.DialogsProvider
{
	public class DialogMessageProvider : IApplicationMessagesListener<ApplicationErrorMessage>
	{
		public Task ListenTo(ApplicationErrorMessage message)
		{
			MessageBox.Show(message.ErrorMessage, message.Message, MessageBoxButton.OK, MessageBoxImage.Error);
			return Task.CompletedTask;
		}
	}
}
