using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
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


        [SetUp]
        public virtual async Task SetUpAsync()
        {
            logger.LogInfo("initializing BaseTest browser and page");
            Playwright = await _playwrightTask;
            BrowserType = Playwright[BrowserName];
            browser = await BrowserType.LaunchAsync(new BrowserTypeLaunchOptions {Headless=Boolean.Parse(HeadlessEnv)});
            page = await browser.NewPageAsync();
            context = await browser.NewContextAsync();
        }

        [TearDown]
        public virtual void TearDown()
        {
            logger.LogInfo("Disposing playwright");
            Playwright.Dispose();
        }
        
    }
}