using System;
using System.Diagnostics;
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
    public  class BaseTestClass
    {
              
        public IBrowser browser {get; set;}
        public static string BrowserName => (Environment.GetEnvironmentVariable("BROWSER") ?? Microsoft.Playwright.BrowserType.Chromium).ToLower();
        public static string HeadlessEnv => (Environment.GetEnvironmentVariable("HEADLESS") ?? "true");
        private  readonly Task<IPlaywright> _playwrightTask = Microsoft.Playwright.Playwright.CreateAsync();

        public  IPlaywright Playwright { get; set; }

        public  IBrowserType BrowserType { get; set; }
        public  IPage page {get; set; }
        public  IBrowserContext context {get; set; }
        public Logger logger = new Logger();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [SetUp]
        public virtual async Task SetUpAsync()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            logger.LogInfo("initializing BaseTest browser and page");
            Playwright = await _playwrightTask;
            BrowserType = Playwright[BrowserName];
            browser = await BrowserType.LaunchAsync(new BrowserTypeLaunchOptions {Headless=Boolean.Parse(HeadlessEnv)});
            page = await browser.NewPageAsync();
            context = await browser.NewContextAsync();
        }

        [TearDown]
        public async virtual Task TearDown()
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
                        ReportLog.Fail("Screenchot", CaptureScreenshot(TestContext.CurrentContext.Test.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")).Result);
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
                        ReportLog.Warning("Screenchot", await CaptureScreenshot(TestContext.CurrentContext.Test.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")));
                        break;
                }
            }
            
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                logger.LogInfo("Disposing playwright");
                
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentService.GetExtent().Flush();
            Playwright.Dispose();
        }

        public async Task<MediaEntityModelProvider> CaptureScreenshot(string name)
        {
            var screenshot  = await page.ScreenshotAsync();
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot), name).Build();
        }
        
    }
}