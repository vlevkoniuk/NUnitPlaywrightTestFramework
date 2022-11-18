using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace NUnitPlaywrightTestProject.Utils
{
    public class ExtentService
    {
        public static ExtentReports extent;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                extent = new ExtentReports();
                string reportDir = Path.Combine(Utility.GetProjectRooDirectory(), "Report");
                if (!Directory.Exists(reportDir))
                    Directory.CreateDirectory(reportDir);

                string path = Path.Combine(reportDir, "index.html");
                var reporter = new ExtentHtmlReporter(path);
                reporter.Config.DocumentTitle = "Extent Report for Playwrite";
                reporter.Config.ReportName = "Extent Report for Playwrite tests";
                reporter.Config.Theme = Theme.Dark;
                extent.AttachReporter(reporter);
            }
            return extent;
        }
    }
}