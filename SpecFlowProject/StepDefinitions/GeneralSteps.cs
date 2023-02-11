using SpecFlowProject.PageObjectModels;
using SpecFlowProject.Support;
using TechTalk.SpecFlow;

namespace SpecFlowProject.StepDefinitions;

[Binding]
public class GeneralSteps
{
    // Relevant POMs
    private readonly LoginPageObject _loginPageObject;
    private readonly HomePageObject _homePageObject;

    public GeneralSteps(Driver driver)
    {
        _loginPageObject = new LoginPageObject(driver.Current);
        _homePageObject = new HomePageObject(driver.Current);
    }

    [Given(@"I have navigated to 1CRM")]
    public void GivenIHaveNavigatedTo1Crm()
    {
        _loginPageObject.NavigateToCrm();
    }

    [Given(@"I navigate to the ""(.*)"" page under ""(.*)""")]
    [When(@"I navigate to the ""(.*)"" page under ""(.*)""")]
    public void WhenINavigateToThePage(string innerMenuItem, string outerMenuItem)
    {
        _homePageObject.ClickMenuItem(innerMenuItem, outerMenuItem);

    }
}