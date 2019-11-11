using System.Windows;

namespace BlitzStatistics.Home
{
	public partial class MainWindow : Window
	{
		public MainWindow(IMainWindowViewModel mainWindowViewModel)
		{
			InitializeComponent();
			DataContext = mainWindowViewModel;
		}
	}
}
