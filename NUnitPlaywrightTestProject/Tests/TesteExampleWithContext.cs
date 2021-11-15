using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using TestFramework.Helpers;
using TestFramework.Models;
using TestFramework.PageObjects;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Playwright;
using System;
using TestFramework.Common;

namespace TestFramework.Tests
{
    [TestFixture]
    public class TesteExampleWithContext : BaseTestClass
    {
        // private IBrowser browser;
        private Login _loginInfo;
        // public static string BrowserName => (Environment.GetEnvironmentVariable("BROWSER") ?? Microsoft.Playwright.BrowserType.Chromium).ToLower();
        // public static string HeadlessEnv => (Environment.GetEnvironmentVariable("HEADLESS") ?? "true");
        // private static readonly Task<IPlaywright> _playwrightTask = Microsoft.Playwright.Playwright.CreateAsync();

        // public IPlaywright Playwright { get; private set; }

        // public IBrowserType BrowserType { get; private set; }
        // private IPage page;
        // private IBrowserContext context;

        [SetUp]
        public async Task SetupAsync()
        {
            await base.SetUpAsync();
            Logger.logLevel = 0; // log all messages

            //sample of reading data from JSON file
            _loginInfo = new Login();
            _loginInfo = LocalTestDataReader.LoadTestData<Login>(_loginInfo.GetType().Name);

        }

        [Test, Description("Some sample test")]
        public async Task SearchForViacheslav()
        {
            //Logger.LogDebug("Debug message");
            //Logger.LogInfo("Info message");
            //Logger.LogWarning("Warning message");
            //Logger.LogError("Error message");
            //Logger.LogFatal("Fatal message");
            
            Logger.LogDebug(page.Url);
            GoogleSearchPageContext gPage = new GoogleSearchPageContext(context);
            Logger.LogDebug(context.Pages[0].Url);
            await gPage.OpenPageAsync();
            await gPage.SearchAsync("viacheslav levkoniuk");

            Logger.LogDebug(context.Pages[0].Url);
            GoogleSearchResultContext resPage = new GoogleSearchResultContext(context);
            ILocator firstLink = resPage.GetLink(0);
            Logger.LogDebug(firstLink.ToString());
            string linkHref = await firstLink.GetAttributeAsync("href");
            Logger.LogDebug("linkHref = " + linkHref);
            Assert.IsTrue(linkHref.Contains("linkedin") && linkHref.Contains("viacheslav-levkoniuk"), "The search result does not contain requested search result");
            Logger.LogInfo("clicking the first link");
            await firstLink.ClickAsync();
            Logger.LogDebug(context.Pages[0].Url);
            Assert.IsTrue(context.Pages[0].Url.Contains("linkedin"));
            
        }

        [TearDown]
        public override void TearDown()
        {
            //driver.Close();
            base.TearDown();
        }
        
    }
}