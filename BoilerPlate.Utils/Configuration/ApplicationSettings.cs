namespace BoilerPlate.Utils
{
    public class ApplicationSettings
    {
        public CountryConfiguration Hosts { get; set; }

        public string HostingEnvironmentPath { get; set; }

        public string Environment { get; set; }

        public bool IsProduction()
        {
            return Environment == "production";
        }

        public bool IsStaging()
        {
            return Environment == "staging";
        }

        public bool IsDevelopment()
        {
            return Environment == "development";
        }

        public bool IsLocalhost()
        {
            return Environment == "localhost";
        }

        public bool IsTest()
        {
            return Environment == "test";
        }
    }
}
