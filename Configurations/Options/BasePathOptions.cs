namespace MyFirstReqnroll.Configurations.Options
{
    public class BasePathOptions
    {
        public const string Key = "BasePath";

        public string Screenshots { get; set; } = string.Empty;
        public string TestData { get; set; } = string.Empty;
        public string Uploads { get; set; } = string.Empty;
        public string Downloads { get; set; } = string.Empty;

         public static string GetFullPath(string relative, params string[] additionalSubPaths)
        {
            if (additionalSubPaths.Length == 0) return Path.GetFullPath(relative);
            return Path.GetFullPath(CombinePath(relative, additionalSubPaths));
        }

        public static string CombinePath(string first, params string[] additionalSubPaths)
        {
            string finalPath = first;
            foreach (string second in additionalSubPaths)
            {
                finalPath = Path.Combine(first, second);
            }
            return finalPath;
        }
    }
    
}