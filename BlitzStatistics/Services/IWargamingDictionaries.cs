using System.Collections.Generic;
using WotBlitzStatician.Model;

namespace BlitzStatistics.Services
{
	public interface IWargamingDictionaries
	{
		bool AreDictionariesCashed { get; }

		IEnumerable<DictionaryNations> DictionaryNations { get; }

		IEnumerable<DictionaryVehicleType> DictionaryVehicleType { get; }

		IEnumerable<VehicleEncyclopedia> VehicleEncyclopedia { get; }

		IEnumerable<Achievement> DictionaryAchievement { get; }

		void SaveToCache(
			IEnumerable<DictionaryNations> dictionaryNations,
			IEnumerable<DictionaryVehicleType> dictionaryVehicleType,
			IEnumerable<Achievement> dictionaryAchievement,
			IEnumerable<VehicleEncyclopedia> vehicleEncyclopedia);
	}
}
