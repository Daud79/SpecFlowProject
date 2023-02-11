using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace SpecFlowProject.PageObjectModels;

/// <summary>
/// Captures the elements and operations for the contacts page
/// </summary>
public class ContactsPageObject
{
    // Properties
    private readonly IWebDriver _driver;
    private readonly ScenarioContext _scenarioContext;
    private readonly WebDriverWait _wait;

    // Constructor
    public ContactsPageObject(IWebDriver driver, ScenarioContext scenarioContext)
    {
        _driver = driver;
        _scenarioContext = scenarioContext;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
    }

    #region Elements
    private IWebElement CreateContactLink => _driver.FindElement(By.XPath("//span[text() = 'Create Contact']//parent::a"));

    // Create contact form
    private IWebElement CreateContactFirstNameInput => _driver.FindElement(By.Id("DetailFormfirst_name-input"));
    private IWebElement CreateContactLastNameInput => _driver.FindElement(By.Id("DetailFormlast_name-input"));
    private IWebElement CreateContactCategoriesInput => _driver.FindElement(By.Id("DetailFormcategories-input"));
    private IWebElement CreateContactBusinessRole => _driver.FindElement(By.Id("DetailFormbusiness_role-input-label"));
    private IWebElement CreateContactSaveButton => _driver.FindElement(By.Id("DetailForm_save2-label"));
    private IWebElement ContactSummary => _driver.FindElement(By.Id("le_section__summary"));
    #endregion

    #region Operations
    /// <summary>
    /// Accepts a table
    /// Creates however many users are in the table
    /// </summary>
    /// <param name="contactDetailsTable"></param>
    public void CreateContact(Table contactDetailsTable)
    {
        foreach (var row in contactDetailsTable.Rows)
        {
            // Grab the data from the row
            var firstName = row[0];
            var lastName = row[1];
            var categories = row[2].Split(",");
            var role = row[3];

            // Save the parsed data from table into the scenario context
            // So that we can assert the details later on
            _scenarioContext["firstName"] = firstName;
            _scenarioContext["lastName"] = lastName;
            _scenarioContext["categories"] = categories;
            _scenarioContext["role"] = role;

            // Open the form
            CreateContactLink.Click();

            // Wait for an element from the form to become clickable
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("DetailFormfirst_name-input")));

            // Enter contact details
            CreateContactFirstNameInput.SendKeys(firstName);
            CreateContactLastNameInput.SendKeys(lastName);

            // Set the role

            // Not happy about using this, but after trying several other things for a good 1-2 hours
            // Including waiting for the element to be clickable, enabled etc.
            // I've had to resort to this (AKA Selenium sucks)
            Thread.Sleep(500);
            CreateContactBusinessRole.Click();
            _driver.FindElement(By.XPath($"//div[contains(@class, 'option-cell') and text() = '{role}']")).Click();

            // Select categories
            var actions = new Actions(_driver);
            foreach (var category in categories)
            {
                Thread.Sleep(500);
                CreateContactCategoriesInput.Click();

                // Similar issue with clicking the items inside the categories once opened
                var xPathForElement = $"//div[text() = '{category}']";
                actions.MoveToElement(_driver.FindElement(By.XPath(xPathForElement))).Perform();
                _driver.FindElement(By.XPath(xPathForElement)).Click();
            }

            // Finally, save
            CreateContactSaveButton.Click();
        }
    }

    /// <summary>
    /// Pulls the details for the latest contact
    /// From the scenario context
    /// </summary>
    public void AssertContactDetails()
    {
        // Grab the contact's details from the scenario context
        var fullName = (string)_scenarioContext["firstName"] + " " + _scenarioContext["lastName"];
        var categories = (string[])_scenarioContext["categories"];
        var role = (string)_scenarioContext["role"];

        // If we're not on the contact page (i.e. one already exists in the db)
        // Then navigate into the one that has been created
        // I may have had to run this a few times!
        if (!_driver.Title.Contains(fullName))
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(fullName)));
            _driver.FindElement(By.LinkText(fullName)).Click();
            _wait.Until(ExpectedConditions.TitleContains(fullName));
        }

        // Assert that the contact details are present
        _wait.Until(ExpectedConditions.TextToBePresentInElement(ContactSummary, fullName));

        // The role for whatever reason on this page is always in lower case
        _wait.Until(ExpectedConditions.TextToBePresentInElement(ContactSummary, role.ToLower()));

        // Assert that the categories match what we input in the earlier step
        foreach (var category in categories)
        {
            _wait.Until(ExpectedConditions.TextToBePresentInElement(ContactSummary, category));
        }
    }
    #endregion
}