using System.Threading.Tasks;
using TestFramework.Common;
using TestFramework.PageObjects;
using Microsoft.Playwright;
using NUnit.Framework;
using TestFramework.Helpers;

namespace TestFramework.Tests
{
    [TestFixture]
    public class MainTestExampleWithPage : BaseTestClass
    {
        [SetUp]
        public override async Task SetUpAsync()
        {
            await base.SetUpAsync();

        }

        [Test]
        public async Task PlaywrightPOMtest()
        {
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
        public override void TearDown()
        {
            base.TearDown();
        }
        
    }
}