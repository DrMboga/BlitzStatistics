using Autofac;
using BlitzStatistics.Bootstrap;
using BlitzStatistics.Home;
using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using System.Windows;

namespace BlitzStatistics
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var bootstrapper = new Bootstrapper();
			var container = bootstrapper.BootStrap();

			container.Resolve<IApplicationMessagesDispatcher>()
				.Publish(new InitializeMessage());

			var mainWindow = container.Resolve<MainWindow>();

			mainWindow.Show();
		}
	}
}
