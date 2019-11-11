using Autofac;
using BlitzStatistics.AccountInfo;
using BlitzStatistics.AccountsSearch;
using BlitzStatistics.AccountsSearchHistory;
using BlitzStatistics.DialogsProvider;
using BlitzStatistics.Home;
using BlitzStatistics.Messages;
using BlitzStatistics.Messaging;
using BlitzStatistics.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using WotBlitzStatician.WotApiClient;

namespace BlitzStatistics.Bootstrap
{
	public class Bootstrapper
	{
		public IContainer BootStrap()
		{
			var builder = new ContainerBuilder();

			// Configuration
			var configuration = GetWargamingApiCinfiguration();
			builder.RegisterInstance(configuration)
				.As<IWgApiConfiguration>()
				.SingleInstance();
			builder.RegisterInstance(configuration.ProxySettings)
				.As<IProxySettings>()
				.SingleInstance();

			// Wargaming api service client
			ConfigureWargamingServiceClient(builder);

			builder.RegisterType<ApplicationMessagesDispatcher>()
				.As<IApplicationMessagesDispatcher>()
				.SingleInstance();

			builder.RegisterType<DialogMessageProvider>()
				.As<IApplicationMessagesListener<ApplicationErrorMessage>>();

			builder.RegisterType<SearchHisoryLocalFileDataAdapter>()
				.As<ISearchHisoryDataAdapter>();

			// ViewModels
			builder.RegisterType<AccountInfoViewModel>()
				.As<IAccountInfoViewModel>()
				.As<IApplicationMessagesListener<ShowFoundAccountInfoMessage>>()
				.As<IApplicationMessagesListener<ShowHistoricalAccountInfoMessage>>()
				.SingleInstance();
			builder.RegisterType<AccountsSearchViewModel>()
				.As<IAccountsSearchViewModel>()
				.SingleInstance();
			builder.RegisterType<AccountsSearchHistoryViewModel>()
				.As<IAccountsSearchHistoryViewModel>()
				.As<IApplicationMessagesListener<ShowFoundAccountInfoMessage>>()
				.As<IApplicationMessagesListener<InitializeMessage>>()
				.SingleInstance();
			builder.RegisterType<MainWindowViewModel>()
				.As<IMainWindowViewModel>()
				.SingleInstance();

			builder.RegisterType<MainWindow>().AsSelf();

			return builder.Build();
		}

		private static void ConfigureWargamingServiceClient(ContainerBuilder builder)
		{
			// Trick with logger relates with the internal implementation of Wargaming api service from nuget
			// This nuget need to be refactored. But so long, here we can add some logger and see the web requests log
			ILoggerFactory loggerFactory = new LoggerFactory();
			builder.RegisterInstance(loggerFactory.CreateLogger<WebApiClient>())
				.As<ILogger<WebApiClient>>();

			builder.ConfigureWargamingApi();
			builder.RegisterType<WargamingDictionaries>()
				.As<IWargamingDictionaries>()
				.SingleInstance();
			builder.RegisterType<WargamingService>()
				.As<IWargamingService>();
		}

		private static IWgApiConfiguration GetWargamingApiCinfiguration()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			var wgApiConfig = new Appsettings
			{
				ProxySettings = new ProxySettings()
			};
			config.GetSection("WgApi").Bind(wgApiConfig);
			config.GetSection("ProxySettings").Bind(wgApiConfig.ProxySettings);
			return wgApiConfig;
		}
	}
}
