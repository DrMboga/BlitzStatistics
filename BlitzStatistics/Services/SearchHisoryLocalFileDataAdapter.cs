using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlitzStatistics.Services
{
	public class SearchHisoryLocalFileDataAdapter : ISearchHisoryDataAdapter
	{
		private readonly string _tempFileName;

		public SearchHisoryLocalFileDataAdapter()
		{
			_tempFileName = Path.Combine(Path.GetTempPath(), "BlitzStatisticsAccountsHistory.json");
		}

		public async Task<List<WotBlitzStatician.Model.AccountInfo>> LoadAccountsHistory()
		{
			if(File.Exists(_tempFileName))
			{
				var accountsString = await File.ReadAllTextAsync(_tempFileName).ConfigureAwait(false);
				return JsonConvert.DeserializeObject<List<WotBlitzStatician.Model.AccountInfo>>(accountsString);
			}
			return null;
		}

		public async Task RewriteAccountsHistory(List<WotBlitzStatician.Model.AccountInfo> accounts)
		{
			var accountsString = JsonConvert.SerializeObject(accounts);
			await File.WriteAllTextAsync(_tempFileName, accountsString).ConfigureAwait(false);
		}
	}
}
