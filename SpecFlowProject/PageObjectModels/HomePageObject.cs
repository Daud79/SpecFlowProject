using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SpecFlowProject.PageObjectModels;

/// <summary>
/// Captures the elements and operations for the homepage
/// </summary>
public class HomePageObject
{
    // Properties
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    // Constructor
    public HomePageObject(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
    }

    #region Elements
    // No elements here as the selectors have been parameterized to reduce lines
    #endregion

    #region Operations
    /// <summary>
    /// Hovers over a navigation menu
    /// Then clicks the respective item
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="item"></param>
    public void ClickMenuItem(string innerMenuItem, string outerMenuItem)
    {
        // Needed to hover over the menu item
        var action = new Actions(_driver);

        // Hover over the element
        action.MoveToElement(_driver.FindElement(By.XPath($"(//div[contains(text(), '{outerMenuItem}')])[1]"))).Perform();

        // Wait until the element is clickable
        _wait.Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement(By.XPath($"//nav[@class = 'nav-wrap']//a[text() = ' {innerMenuItem}']"))));

        // Click the item
        _driver.FindElement(By.LinkText(innerMenuItem)).Click();

        // The name of the item should be in the title of the page
        // I.e. 'Contacts'
        _wait.Until(ExpectedConditions.TitleContains(innerMenuItem));
    }
    #endregion
}