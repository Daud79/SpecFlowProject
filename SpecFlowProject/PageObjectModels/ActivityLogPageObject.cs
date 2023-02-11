using System.Security.Policy;
using System.Xml.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V107.DOM;
using OpenQA.Selenium.DevTools.V107.SystemInfo;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowProject.Support;
using TechTalk.SpecFlow;

namespace SpecFlowProject.PageObjectModels;

/// <summary>
/// Captures the elements and operations for the activity log page
/// </summary>
public class ActivityLogPageObject
{
    // Properties
    private readonly IWebDriver _driver;
    private readonly ScenarioContext _scenarioContext;
    private readonly WebDriverWait _wait;

    // Constructor
    public ActivityLogPageObject(IWebDriver driver, ScenarioContext scenarioContext)
    {
        _driver = driver;
        _scenarioContext = scenarioContext;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
    }

    #region Elements
    private IWebElement ListViewTable => _driver.FindElement(By.XPath("//table[contains(@id, 'listView')]"));
    private IWebElement ActionsButton => _driver.FindElement(By.XPath("(//button[@class = 'input-button menu-source'])[1]"));
    #endregion

    #region Operations
    /// <summary>
    /// Selects x number of items from the top of the list down
    /// </summary>
    /// <param name="numberOfItemsToSelect"></param>
    public void SelectItemsFromList(int numberOfItemsToSelect)
    {
        // A list of the text for each row we selected
        var selectedItemList = new List<string>();

        // Loops through x number of items
        // And selects that # of items from the retrieved list
        for (var i = 0; i < numberOfItemsToSelect; i++)
        {
            var rowXpath = $"(//table[contains(@id, 'listView')]//tr)[{i + 2}]";

            // i+2 as we don't care about the headers
            // This will click each individual checkbox
            ListViewTable.FindElement(By.XPath($"(.//tr)[{i + 2}]//input")).Click();

            // Wait for the row to update
            // The tr element updates with 'markRow' when it has been selected
            _wait.Until(d => d.FindElement(By.XPath(rowXpath)).GetAttribute("class").Contains("markRow"));

            // Add the element to an array, so we can assert it's deleted later
            selectedItemList.Add(_driver.FindElement(By.XPath(rowXpath)).Text);
        }

        // Add the array of web elements to the scenario context
        _scenarioContext["selectedItemList"] = selectedItemList;
    }

    /// <summary>
    /// Delete/Export/Print the selected items from the list
    /// </summary>
    public void PerformActionOnSelectedItems(string action)
    {
        // Create the XPath using the name of the action
        var actionButtonXPath = $"//div[text() = '{action}']";

        // Expand actions 
        ActionsButton.Click();

        // Wait for the action button to become clickable
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(actionButtonXPath)));

        // Click the respective button
        _driver.FindElement(By.XPath(actionButtonXPath)).Click();

        switch (action)
        {
            // An alert will popup in the case of record deletion
            case "Delete":
                // Accept the alert
                _driver.SwitchTo().Alert().Accept();
                break;

            default:
                throw new Exception("Others actions not implemented yet!");
        }
    }

    /// <summary>
    /// Asserts that the items no longer exist in the list
    /// </summary>
    public void AssertItemDeletion()
    {
        // Load the items from the scenario context
        var deletedItemList = (List<string>)_scenarioContext["selectedItemList"];

        // Wait for the table to update
        _wait.Until(d => d.FindElement(By.XPath("//table[contains(@id, 'listView')]")).Text.Contains(deletedItemList[0]) == false);

        foreach (var webElementText in deletedItemList)
        {
            // Grab all the text in the table
            var tableText = ListViewTable.Text;

            // Assert that the records we removed don't exist in the table
            Assert.False(tableText.Contains(webElementText));
        }
    }
    #endregion
}