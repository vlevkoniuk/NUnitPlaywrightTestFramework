using System;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnitPlaywrightTestProject.Utils;
using TestFramework.Helpers;

namespace TestFramework.Common
{
    [TestFixture]
    public  class BaseTestContextClass
    {
              
        public IBrowser browser {get; set;}
        public static string BrowserName => (Environment.GetEnvironmentVariable("BROWSER") ?? Microsoft.Playwright.BrowserType.Chromium).ToLower();
        public static string HeadlessEnv => (Environment.GetEnvironmentVariable("HEADLESS") ?? "true");
        private  readonly Task<IPlaywright> _playwrightTask = Microsoft.Playwright.Playwright.CreateAsync();

        public  IPlaywright Playwright { get; set; }

        public  IBrowserType BrowserType { get; set; }
        public  IBrowserContext context {get; set; }
        public Logger logger = new Logger();


        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            logger.LogInfo($"initializing BaseTestContext browser and context with browser {BrowserName}");
            Playwright = _playwrightTask.Result;
            BrowserType = Playwright[BrowserName];
            browser = BrowserType.LaunchAsync(new BrowserTypeLaunchOptions {Headless=Boolean.Parse(HeadlessEnv)}).Result;
            context =  browser.NewContextAsync().Result;
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            logger.LogInfo("Disposing playwright");
            Playwright.Dispose();
            ExtentService.GetExtent().Flush();
        }

        [SetUp]
        public async virtual Task SetUpAsync()
        {
            logger.LogInfo("Runnint global single test SetUp");
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public async virtual Task TearDownAsync()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var errorMessage = string .IsNullOrWhiteSpace(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : string.Format($"<pre>{TestContext.CurrentContext.Result.Message}</ore>");
                var stackTrace = string .IsNullOrWhiteSpace(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format($"<pre>{TestContext.CurrentContext.Result.StackTrace}</ore>");
                switch (status)
                {
                    case TestStatus.Failed:
                        ReportLog.Fail("Test failed!");
                        ReportLog.Fail(errorMessage);
                        ReportLog.Fail(stackTrace);
                        ReportLog.Fail("Screenchot",  CaptureScreenshot(TestContext.CurrentContext.Test.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")).Result);
                        break;
                    case TestStatus.Skipped:
                        ReportLog.Skip("Test skipped!");
                        break;
                    case TestStatus.Passed:
                        ReportLog.Pass("Test passed!");
                        break;
                    case TestStatus.Warning:
                        ReportLog.Warning("Test generated warning!");
                        ReportLog.Warning(errorMessage);
                        ReportLog.Warning("Screenchot", CaptureScreenshot(TestContext.CurrentContext.Test.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")).Result);
                        break;
                }
            }
            
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
            }
        }

        public async Task<MediaEntityModelProvider> CaptureScreenshot(string name)
        {
            var screenshot  = await context.Pages[0].ScreenshotAsync();
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot), name).Build();
        }
        
    }
}