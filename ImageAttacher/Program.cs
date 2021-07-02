using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using ImageAttacher.Runs;
using ImageAttacher.RunResultClasses;
using System.Threading;

namespace ImageAttacher
{
    public class Program
    {
        public static HttpClient hc = new HttpClient();
#if RELEASE

        public static string username = Environment.GetEnvironmentVariable("S_USERNAME");
        public static string pat = Environment.GetEnvironmentVariable("S_PAT");
        public static string e2eRootDir = Environment.GetEnvironmentVariable("S_E2EROOTDIR");
        public static string runTitle = Environment.GetEnvironmentVariable("S_RUNTITLE");
        public static string orgName = Environment.GetEnvironmentVariable("S_ORGNAME");
        public static string projName = Environment.GetEnvironmentVariable("S_PROJNAME");
#else

        public static string username = "__USERNAME__";
        public static string pat = "__PAT WITH ACCESS TO UPLOAD IN TESTS__";
        public static string e2eRootDir = @"__PATH TO E2E TEST RESULTS__";
        public static string runTitle = "E2E Tests( upload name of tests)";
        public static string orgName = "__ YOUR ORG NAME __";
        public static string projName = "__PROJECT NAME__";

#endif

        private static async Task Main(string[] args)
        {
            // use this to simulate execution from e2e folder.
            Directory.SetCurrentDirectory(e2eRootDir);
            Console.WriteLine($"Current dir now is:{Directory.GetCurrentDirectory()}");

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{pat}"));
            hc.DefaultRequestHeaders.Add("Authorization", $"Basic {authToken}");

            bool hasRuns = false;
            TestRuns runs = default;
            byte passes = 0;

            do
            {
                runs = await GetTestRunsAsync();

                if (runs.value.Any())
                {
                    hasRuns = true;
                    break;
                }

                passes++;
                Thread.Sleep(30000);

                if (passes >= 10)
                {
                    break;
                }
            } while (!hasRuns);

            var firstRunId = runs.value[0].id;

            // get failed test cases for this run

            var failedCasesUrl = $"https://dev.azure.com/{orgName}/{projName}/_apis/test/Runs/{firstRunId}/results?api-version=5.1-preview&outcomes=Failed";
            Console.WriteLine($"Failed Cases Url:{failedCasesUrl}");

            var failedCases = JsonSerializer.Deserialize<RunResults>(await hc.GetStringAsync(failedCasesUrl));

            var currentDir = Directory.GetCurrentDirectory();
            Console.WriteLine($"Failed Cases:{failedCases.count}");

            for (int i = 0; i < failedCases.count; i++)
            {
                var failedCase = failedCases.value[i];
                Console.WriteLine($"=== Failed Case {failedCase.id}");
                Console.WriteLine($"Title:{failedCase.testCaseTitle}");

                string name = failedCase.testCaseTitle;

                string[] casePath = failedCase.automatedTestStorage.Split('.');

                var pathList = new List<string>() { currentDir, "screenshots" };
                pathList.AddRange(casePath);

                var expectedImgDir = Path.Join(pathList.ToArray());
                Console.WriteLine($"expectedImgDir:{expectedImgDir}");

                try
                {
                    var imagesList = Directory.GetFiles(expectedImgDir, "*.png");
                    var escapedName = name.Replace(' ', '-');
                    Console.WriteLine($"escapedName:{escapedName}");
                    Console.WriteLine($"--- Images ---");

                    Console.WriteLine(string.Join('\n', imagesList.ToArray()));
                    Console.WriteLine($"--- End Images ---");

                    foreach (var imagePath in imagesList.Where(x => x.StartsWith(Path.Combine(expectedImgDir, escapedName))))
                    {
                        Console.WriteLine($"Found imagePath:{imagePath}");

                        byte[] bytes = File.ReadAllBytes(imagePath);
                        var fileInBase64 = Convert.ToBase64String(bytes);

                        var json = JsonSerializer.Serialize(new
                        {
                            stream = fileInBase64,
                            fileName = $"{escapedName}.png",
                            attachmentType = "GeneralAttachment"
                        });

                        var attachmentURl = $"https://dev.azure.com/{orgName}/{projName}/_apis/test/Runs/{firstRunId}/Results/{failedCase.id}/attachments?api-version=5.1-preview";
                        Console.WriteLine($"Attachment URL: {attachmentURl}");

                        var uploadResult = await hc.PostAsync(attachmentURl, new StringContent(json, Encoding.UTF8, "application/json"));
                        Console.WriteLine("---------");
                        Console.WriteLine(JsonSerializer.Serialize(uploadResult));
                        Console.WriteLine("---------");
                        Console.WriteLine($"uploaded {escapedName} to case {failedCase.id}");
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine("cannot find directory:" + expectedImgDir);
                }
            }
        }

        private static async Task<TestRuns> GetTestRunsAsync()
        {
            //var minDateFormated = DateTime.UtcNow.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss");
            var minDateFormated = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss");

            var maxDateFormated = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            Console.WriteLine($"minDate:{minDateFormated},maxDate:{maxDateFormated}");

            // get list of runs
            var runsUrl = $"https://dev.azure.com/{orgName}/{projName}/_apis/test/runs?api-version=5.1-preview&maxLastUpdatedDate={maxDateFormated}&minLastUpdatedDate={minDateFormated}&runTitle={runTitle}";
            Console.WriteLine($"runs URL : {runsUrl}");

            var runs = JsonSerializer.Deserialize<TestRuns>(await hc.GetStringAsync(runsUrl));
            return runs;
        }
    }
}