using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
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
        public virtual async Task OneTimeSetUpAsync()
        {
            logger.LogInfo($"initializing BaseTestContext browser and context with browser {BrowserName}");
            Playwright = await _playwrightTask;
            BrowserType = Playwright[BrowserName];
            browser = await BrowserType.LaunchAsync(new BrowserTypeLaunchOptions {Headless=Boolean.Parse(HeadlessEnv)});
            context = await browser.NewContextAsync();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            logger.LogInfo("Disposing playwright");
            Playwright.Dispose();
        }
        
    }
}