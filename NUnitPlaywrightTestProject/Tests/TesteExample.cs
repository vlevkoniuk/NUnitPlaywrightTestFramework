using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnitSeleniumTestProjectExample.Helpers;
using NUnitSeleniumTestProjectExample.Models;
using Framework.PageObjects;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Playwright;
using System;

namespace NUnitSeleniumTestProjectExample.Tests
{
    [TestFixture]
    public class TesteExample
    {
        private IBrowser browser;
        private Login _loginInfo;
        public static string BrowserName => (Environment.GetEnvironmentVariable("BROWSER") ?? Microsoft.Playwright.BrowserType.Chromium).ToLower();
        public static string HeadlessEnv => (Environment.GetEnvironmentVariable("HEADLESS") ?? "true");
        private static readonly Task<IPlaywright> _playwrightTask = Microsoft.Playwright.Playwright.CreateAsync();

        public IPlaywright? Playwright { get; private set; }

        public IBrowserType? BrowserType { get; private set; }
        private IPage page;

        [SetUp]
        public async Task SetupAsync()
        {
            Playwright = await _playwrightTask;
            BrowserType = Playwright[BrowserName];
            browser = await BrowserType.LaunchAsync(new BrowserTypeLaunchOptions {Headless=Boolean.Parse(HeadlessEnv)});
            page = await browser.NewPageAsync();
            Logger.logLevel = 0; // log all messages

            //sample of reading data from JSON file
            _loginInfo = new Login();
            _loginInfo = LocalTestDataReader.LoadTestData<Login>(_loginInfo.GetType().Name);

        }

        [Test, Description("Some sample test")]
        public async Task SearchForViacheslav()
        {
            Logger.LogDebug("Debug message");
            Logger.LogInfo("Info message");
            Logger.LogWarning("Warning message");
            Logger.LogError("Error message");
            Logger.LogFatal("Fatal message");
            
            Logger.LogDebug(page.Url);
            GoogleSearchPage gPage = new GoogleSearchPage(page);
            Logger.LogDebug(page.Url);
            await gPage.OpenPageAsync();
            await gPage.SearchAsync("viacheslav levkoniuk");

            Logger.LogDebug(page.Url);
            GoogleSearchResult resPage = new GoogleSearchResult(page);
            ILocator firstLink = resPage.GetLink(0);
            string linkHref = await firstLink.GetAttributeAsync("href");
            Logger.LogDebug("linkHref = " + linkHref);
            Assert.IsTrue(linkHref.Contains("linkedin") && linkHref.Contains("viacheslav-levkoniuk"), "The search result does not contain requested search result");
            Logger.LogInfo("clicking the first link");
            await firstLink.ClickAsync();
            Logger.LogDebug(page.Url);
            Assert.IsTrue(page.Url.Contains("linkedin"));
            
        }

        [TearDown]
        public void TearDown()
        {
            //driver.Close();
            Playwright.Dispose();
        }
        
    }
}