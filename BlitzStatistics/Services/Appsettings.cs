﻿using WotBlitzStatician.WotApiClient;

namespace BlitzStatistics.Services
{
	public class Appsettings : IWgApiConfiguration
	{
		public string ApplicationId { get; set; }
		public string BaseAddress { get; set; }
		public string WotBaseAddress { get; set; }
		public string Language { get; set; }
		public int DictionariesUpdateFrequencyInDays { get; set; }
		public IProxySettings ProxySettings { get; set; }
		public int HttpTimeoutInMinutes { get; set; }
	}

	public class ProxySettings : IProxySettings
	{
		public bool UseProxy { get; set; }
		public string ProxyAddress { get; set; }
		public string Domain { get; set; }
		public string User { get; set; }
		public string PwdHash { get; set; }
	}
}
