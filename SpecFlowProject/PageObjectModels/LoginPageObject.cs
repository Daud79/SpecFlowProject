using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SpecFlowProject.PageObjectModels;

/// <summary>
/// Captures the elements and operations for the login page
/// </summary>
public class LoginPageObject
{
    // Properties
    private const string CrmUrl = "https://demo.1crmcloud.com/";
    private const string HomePageTitle = "1CRM: Home Dashboard";
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    // Constructor
    public LoginPageObject(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
    }

    #region Elements

    private IWebElement UserNameInput => _driver.FindElement(By.Id("login_user"));
    private IWebElement PasswordInput => _driver.FindElement(By.Id("login_pass"));
    private IWebElement LoginButton => _driver.FindElement(By.Id("login_button"));

    #endregion

    #region Operations

    /// <summary>
    /// Navigates to the SUT
    /// </summary>
    public void NavigateToCrm()
    {
        if (_driver.Url != CrmUrl)
        {
            // Set the url
            _driver.Url = CrmUrl;
        }
        _wait.Until(ExpectedConditions.UrlToBe("https://demo.1crmcloud.com/login.php?login_module=Home&login_action=index"));
    }

    /// <summary>
    /// Logs in to the CRM as a given user
    /// </summary>
    /// <param name="user"></param>
    public void LoginToCrm(string user)
    {
        // Input the username field
        UserNameInput.SendKeys(user);

        switch (user)
        {
            case "admin":
                PasswordInput.SendKeys("admin");
                LoginButton.Click();
                break;

            default:
                throw new Exception("User not accounted for. Please check!");

                // Others users can be implemented here as/when needed
        }
        _wait.Until(ExpectedConditions.TitleIs(HomePageTitle));
    }

    #endregion
}