using System.Threading.Tasks;
using Framework.Common;
using Framework.PageObjects;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnitSeleniumTestProjectExample.Helpers;

namespace NUnitPlaywrightTestProject.Tests
{
    [TestFixture]
    public class SecondTest : BaseTestClass
    {
        [SetUp]
        public override async Task SetUp()
        {
            await base.SetUp();

        }

        [Test]
        public async Task Test1()
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