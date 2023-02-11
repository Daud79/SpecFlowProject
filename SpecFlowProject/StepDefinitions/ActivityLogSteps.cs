using SpecFlowProject.PageObjectModels;
using SpecFlowProject.Support;
using TechTalk.SpecFlow;

namespace SpecFlowProject.StepDefinitions;

[Binding]
public class ActivityLogSteps
{
    // Relevant POMs
    private readonly ActivityLogPageObject _activityLogPageObject;

    public ActivityLogSteps(Driver driver, ScenarioContext scenarioContext)
    {
        _activityLogPageObject = new ActivityLogPageObject(driver.Current, scenarioContext);
    }

    [When(@"I ""(.*)"" the first (.*) items in the list")]
    public void WhenIRemoveTheFirstItemsInTheList(string action, int numberOfItemsToRemove)
    {
        _activityLogPageObject.SelectItemsFromList(numberOfItemsToRemove);
        _activityLogPageObject.PerformActionOnSelectedItems(action);
    }

    [Then(@"the removed items are deleted from the system")]
    public void ThenTheRemovedItemsAreDeletedFromTheSystem()
    {
        _activityLogPageObject.AssertItemDeletion();
    }
}