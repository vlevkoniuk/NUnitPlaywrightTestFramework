using System.Threading.Tasks;
using TestFramework.Common;
using TestFramework.PageObjects;
using Microsoft.Playwright;
using NUnit.Framework;
using TestFramework.Helpers;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace TestFramework.Tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSubSuite("Page Example")]
    [AllureSeverity(Allure.Commons.SeverityLevel.critical)]
    [Parallelizable]
    public class MainTestExampleWithPage : BaseTestClass
    {
        
        [SetUp]
        public override async Task SetUpAsync()
        {
            await base.SetUpAsync();
            logger.logLevel = 0;

        }

        [Test]
        [AllureTag("NUnit","Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureFeature("Core")]
        public async Task PlaywrightPOMtest()
        {
            logger.LogDebug(page.Url);
            GoogleSearchPage gPage = new GoogleSearchPage(page);
            logger.LogDebug(page.Url);
            await gPage.OpenPageAsync();
            await gPage.SearchAsync("viacheslav levkoniuk");

            logger.LogDebug(page.Url);
            GoogleSearchResult resPage = new GoogleSearchResult(page);
            ILocator firstLink = resPage.GetLink(0);
            string linkHref = await firstLink.GetAttributeAsync("href");
            logger.LogDebug("linkHref = " + linkHref);
            Assert.IsTrue(linkHref.Contains("linkedin") && linkHref.Contains("viacheslav-levkoniuk"), "The search result does not contain requested search result");
            logger.LogInfo("clicking the first link");
            await firstLink.ClickAsync();
            logger.LogDebug(page.Url);
            Assert.IsTrue(page.Url.Contains("linkedin"));
        }

        [Test]
        [AllureTag("NUnit","Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureFeature("Core")]
        public async Task PlaywrightPOMtestFail()
        {
            logger.LogDebug(page.Url);
            GoogleSearchPage gPage = new GoogleSearchPage(page);
            logger.LogDebug(page.Url);
            await gPage.OpenPageAsync();
            await gPage.SearchAsync("viacheslav levkoniuk");

            logger.LogDebug(page.Url);
            GoogleSearchResult resPage = new GoogleSearchResult(page);
            ILocator firstLink = resPage.GetLink(0);
            string linkHref = await firstLink.GetAttributeAsync("href");
            logger.LogDebug("linkHref = " + linkHref);
            Assert.IsTrue(linkHref.Contains("linkedin") && linkHref.Contains("viacheslav-levkoniuk"), "The search result does not contain requested search result");
            logger.LogInfo("clicking the first link");
            await firstLink.ClickAsync();
            logger.LogDebug(page.Url);
            await Task.Delay(5000);
            Assert.IsTrue(page.Url.Contains("blablabla"));
        }

        [TearDown]
        public async override Task TearDown()
        {
            await base.TearDown();
        }
        
    }
}