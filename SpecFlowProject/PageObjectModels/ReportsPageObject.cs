using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace SpecFlowProject.PageObjectModels;

/// <summary>
/// Captures the elements and operations for the reportspage
/// </summary>
public class ReportsPageObject
{
    // Properties
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly ScenarioContext _scenarioContext;

    // Constructor
    public ReportsPageObject(IWebDriver driver, ScenarioContext scenarioContext)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        _scenarioContext = scenarioContext;
    }

    #region Elements
    private IWebElement ReportSearchInput => _driver.FindElement(By.XPath("//div[@class = 'form-entry']//input[contains(@autosave, 'search.Reports')]"));
    private IWebElement RunReportButton => _driver.FindElement(By.XPath("//button[@name = 'FilterForm_applyButton']"));
    private IWebElement ReportResultsTable => _driver.FindElement(By.XPath("//table[contains(@id, 'listView')]"));
    #endregion

    #region Operations
    /// <summary>
    /// Searches for the report, and clicks in to it
    /// </summary>
    /// <param name="reportType"></param>
    public void SearchForReport(string reportType)
    {
        // Search for the report
        // Wait for the element to become displayed
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class = 'form-entry']//input[contains(@autosave, 'search.Reports')]")));

        // Enter report type + hit enter to navigate
        ReportSearchInput.SendKeys(reportType);
        ReportSearchInput.SendKeys(Keys.Enter);

        // Wait for the element to become interactable
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(reportType)));

        // Select the report from the refined list
        _scenarioContext["reportType"] = reportType;
        _driver.FindElement(By.LinkText(reportType)).Click();
    }

    /// <summary>
    /// Finds and clicks the 'Run Report' button
    /// </summary>
    public void RunReport()
    {
        // Wait for the table to load
        _wait.Until(
            ExpectedConditions.TextToBePresentInElement(ReportResultsTable, (string)_scenarioContext["reportType"]));

        // Take note of the # of rows (so we can assert whether records were returned later)
        _scenarioContext["tableRowsBeforeReportRun"] = ReportResultsTable.FindElements(By.TagName("tr")).Count;
        
        // Wait for the button to appear
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@name = 'FilterForm_applyButton']")));

        // Run the report
        RunReportButton.Click();
    }

    /// <summary>
    /// Asserts that the report has run
    /// By comparing the # of rows in the listview table
    /// Before and after running the report
    /// </summary>
    public void AssertReportResultsPresent()
    {
        // Wait for the table to load
        _wait.Until(d => d.FindElements(By.XPath("//table[contains(@id, 'listView')]//tr")).Count >= 2);

        // Assert that the row count has updated (i.e. the run report has successfully run)
        Assert.True(ReportResultsTable.FindElements(By.TagName("tr")).Count > (int)_scenarioContext["tableRowsBeforeReportRun"]);
    }
    #endregion
}