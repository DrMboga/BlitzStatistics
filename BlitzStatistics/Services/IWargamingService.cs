using BlitzStatistics.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlitzStatistics.Services
{
	public interface IWargamingService
	{
		Task<List<WotBlitzStatician.Model.AccountInfo>> FindAccounts(string searchString);

		Task<AccountInfoDto> GetAccountInfo(long accountId);
	}
}
