using SpecFlowProject.PageObjectModels;
using SpecFlowProject.Support;
using TechTalk.SpecFlow;

namespace SpecFlowProject.StepDefinitions;

[Binding]
public class ContactSteps
{
    // Relevant POMs
    private readonly LoginPageObject _loginPageObject;
    private readonly ContactsPageObject _contactsPageObject;

    public ContactSteps(Driver driver, ScenarioContext scenarioContext)
    {
        _loginPageObject = new LoginPageObject(driver.Current);
        _contactsPageObject = new ContactsPageObject(driver.Current, scenarioContext);
    }

    [Given(@"I login as ""(.*)""")]
    public void GivenIAmLoggedInToCrmAs(string user)
    {
        _loginPageObject.LoginToCrm(user);
    }

    [When(@"I create a new contact")]
    public void WhenICreateANewContact(Table detailsTable)
    {
        _contactsPageObject.CreateContact(detailsTable);
    }

    [Then(@"the new contact has been successfully created")]
    public void ThenTheNewContactHasBeenSuccessfullyCreated()
    {
        _contactsPageObject.AssertContactDetails();
    }
}
