namespace NotifyService.Common.Library.Configurations
{
	public class ConfigurationReader
	{
		public static AppConfiguration Current { get; private set; }

		public static void SetCurrent(AppConfiguration configuration)
			=> Current = configuration;
	}
}