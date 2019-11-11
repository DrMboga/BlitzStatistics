using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlitzStatistics.Services
{
	public interface ISearchHisoryDataAdapter
	{
		Task<List<WotBlitzStatician.Model.AccountInfo>> LoadAccountsHistory();
		Task RewriteAccountsHistory(List<WotBlitzStatician.Model.AccountInfo> accounts);
	}
}
