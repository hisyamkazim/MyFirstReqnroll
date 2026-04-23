using MyFirstReqnroll.Configurations.Options;
using System.Diagnostics;

namespace MyFirstReqnroll.Helpers
{
    public class DownloadHelper
    {
        private readonly string _downloadDirectory;

        public DownloadHelper(string directory)
        {
            _downloadDirectory = directory;
        }

        public string WaitForNewDownload(int timeoutInSeconds = 60, int downloadStartInSeconds = 15)
        {
            var timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            var pollInterval = TimeSpan.FromMilliseconds(500);
            var stopwatch = Stopwatch.StartNew();

            string fullPath = BasePathOptions.GetFullPath(_downloadDirectory);
            Console.WriteLine($"Starting download check in: {fullPath}");

            if (!Directory.Exists(fullPath))
                throw new DirectoryNotFoundException($"Download directory not found: {fullPath}");

            // Capture download start time with tolerance
            // By dedault 15 seconds earlier but can be adjusted in parameter "downloadStartInSeconds"
            DateTime downloadStart = DateTime.UtcNow.AddSeconds(-downloadStartInSeconds);

            while (stopwatch.Elapsed < timeout)
            {
                try
                {
                    var newestFile = Directory.GetFiles(fullPath)
                        .Where(f => !IsPartialDownload(f))
                        .OrderByDescending(f => new FileInfo(f).LastWriteTimeUtc)
                        .FirstOrDefault();

                    if (newestFile != null)
                    {
                        var fileInfo = new FileInfo(newestFile);

                        // Accept files created or modified after (scenarioStart - 15s)
                        if (fileInfo.LastWriteTimeUtc >= downloadStart && IsFileStable(newestFile))
                        {
                            Console.WriteLine("New downloaded file detected: " + newestFile);
                            return newestFile;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking for downloads (ignored until timeout): {ex.Message}");
                }

                Thread.Sleep(pollInterval);
            }

            throw new TimeoutException($"No valid file was downloaded within {timeoutInSeconds} seconds");
        }


        private bool IsFileStable(string filePath)
        {
            try
            {
                var fi = new FileInfo(filePath);
                if (!fi.Exists || fi.Length == 0)
                    return false;

                long previousSize = fi.Length;
                const int checks = 5;           // do 5 checks
                const int delayMs = 1000;       // 1 second between checks

                for (int i = 0; i < checks; i++)
                {
                    Thread.Sleep(delayMs);
                    fi.Refresh();

                    if (!fi.Exists)
                    {
                        Console.WriteLine($"[IsFileStable] File disappeared: {filePath}");
                        return false;
                    }

                    if (fi.Length != previousSize)
                    {
                        Console.WriteLine($"[IsFileStable] File still growing: {filePath} (old={previousSize}, new={fi.Length})");
                        previousSize = fi.Length;
                        i = 0;
                    }
                }

                Console.WriteLine($"[IsFileStable] File is stable: {filePath} (size={fi.Length})");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[IsFileStable] Exception for {filePath}: {ex.Message}");
                return false;
            }
        }

        private bool IsPartialDownload(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filePath) ?? "";

            return ext.Equals(".crdownload", StringComparison.OrdinalIgnoreCase)
                || ext.Equals(".part", StringComparison.OrdinalIgnoreCase)
                || ext.Equals(".tmp", StringComparison.OrdinalIgnoreCase)
                || fileName.StartsWith(".com.google.Chrome", StringComparison.OrdinalIgnoreCase);
        }
    }
}
