namespace MyFirstReqnroll.Configurations.Options
{
    public class DriverOptions
    {
        public const string Key = "Driver";

        public string Name { get; set; } = "chrome";
        public bool Headless { get; set; } = false;
        public int ImplicitWaitSeconds { get; set; } = 10;
        public int PageLoadTimeoutSeconds { get; set; } = 30;
        public List<string> arguments { get; set; } = new List<string>();
    }
}