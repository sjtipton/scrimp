namespace scrimp.Helpers
{
    public class AppSettingsProvider
    {
        private static AppSettingsConfiguration _configuration = new AppSettingsConfiguration();

        public static string GetGreenlitApiUrl() => _configuration.GreenlitApiUrl;

        public static void SetGreenlitApiUrl(AppSettingsConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
