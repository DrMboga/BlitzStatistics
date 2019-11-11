using System.Collections.Generic;
using WotBlitzStatician.Model;

namespace BlitzStatistics.Services
{
	public class WargamingDictionaries : IWargamingDictionaries
	{
		public bool AreDictionariesCashed { get; private set; } = false;

		public IEnumerable<DictionaryNations> DictionaryNations { get; private set; }

		public IEnumerable<DictionaryVehicleType> DictionaryVehicleType { get; private set; }

		public IEnumerable<VehicleEncyclopedia> VehicleEncyclopedia { get; private set; }

		public IEnumerable<Achievement> DictionaryAchievement { get; private set; }

		public void SaveToCache(
			IEnumerable<DictionaryNations> dictionaryNations,
			IEnumerable<DictionaryVehicleType> dictionaryVehicleType,
			IEnumerable<Achievement> dictionaryAchievement,
			IEnumerable<VehicleEncyclopedia> vehicleEncyclopedia)
		{
			DictionaryNations = dictionaryNations;
			DictionaryVehicleType = dictionaryVehicleType;
			DictionaryAchievement = dictionaryAchievement;
			VehicleEncyclopedia = vehicleEncyclopedia;
			AreDictionariesCashed = true;
		}
	}
}
