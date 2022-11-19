# NUnitPlaywrightTestFramework
Test framework based on the Playwright and NUnit frameworks

To be able to run it you have to make sure that the Playwright CLI is installed
> dotnet tool install --global Microsoft.Playwright.CLI --version 1.2.0

And Playwright browsers are also installed
> playwright install

To see Allure report, use this instruction to create report from the test data
https://docs.qameta.io/allure/
>allure serve

Too see Extent report just open index.html from the report folder after tests completed
