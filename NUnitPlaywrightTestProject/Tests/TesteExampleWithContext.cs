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
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using NUnitPlaywrightTestProject.Utils;

namespace TestFramework.Tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSubSuite("Context Example")]
    [AllureSeverity(Allure.Commons.SeverityLevel.critical)]
    [Parallelizable]
    public class TesteExampleWithContext : BaseTestContextClass
    {
        private Login _loginInfo;

        public TesteExampleWithContext()
        {
            logger.logLevel = 0; // log all messages

            //sample of reading data from JSON file
            _loginInfo = new Login();
            _loginInfo = LocalTestDataReader.LoadTestData<Login>(_loginInfo.GetType().Name);
        }

        [Test] 
        [Description("Some sample test")]
        [AllureTag("NUnit","Debug")]
        [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
        [AllureFeature("Core")]
        public async Task SearchForViacheslav()
        {
            //Logger.LogDebug("Debug message");
            //Logger.LogInfo("Info message");
            //Logger.LogWarning("Warning message");
            //Logger.LogError("Error message");
            //Logger.LogFatal("Fatal message");
            
            GoogleSearchPageContext gPage = new GoogleSearchPageContext(context);
            logger.LogDebug(context.Pages[0].Url);
            await gPage.OpenPageAsync();
            await gPage.SearchAsync("viacheslav levkoniuk");

            logger.LogDebug(context.Pages[0].Url);
            GoogleSearchResultContext resPage = new GoogleSearchResultContext(context);
            ILocator firstLink = resPage.GetLink(0);
            logger.LogDebug(firstLink.ToString());
            string linkHref = await firstLink.GetAttributeAsync("href");
            logger.LogDebug("linkHref = " + linkHref);
            Assert.IsTrue(linkHref.Contains("linkedin") && linkHref.Contains("viacheslav-levkoniuk"), "The search result does not contain requested search result");
            logger.LogInfo("clicking the first link");
            await firstLink.ClickAsync();
            logger.LogDebug(context.Pages[0].Url);
            Assert.IsTrue(context.Pages[0].Url.Contains("linkedin"));
            //ReportLog.Pass("Search vor the author and redirecting to the Linked In page was successfull");
        }
        
    }
}