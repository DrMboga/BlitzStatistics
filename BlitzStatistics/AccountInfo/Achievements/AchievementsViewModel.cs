using BlitzStatistics.Model;
using BlitzStatistics.WpfHelpers;
using System.Collections.ObjectModel;

namespace BlitzStatistics.AccountInfo.Achievements
{
	public class AchievementsViewModel : ViewModelBase
	{
		private ObservableCollection<AchievementDto> _achievements;

		public ObservableCollection<AchievementDto> Achievements
		{
			get { return _achievements; }
			set
			{
				_achievements = value;
				OnPropertyChanged();
			}
		}
	}
}
