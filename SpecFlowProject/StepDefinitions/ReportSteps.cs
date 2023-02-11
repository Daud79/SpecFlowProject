using SpecFlowProject.PageObjectModels;
using SpecFlowProject.Support;
using TechTalk.SpecFlow;

namespace SpecFlowProject.StepDefinitions;

[Binding]
public class ReportSteps
{
    // Relevant POMs
    private readonly ReportsPageObject _reportsPageObject;

    public ReportSteps(Driver driver, ScenarioContext scenarioContext)
    {
        _reportsPageObject = new ReportsPageObject(driver.Current, scenarioContext);
    }

    [When(@"I search for the ""([^""]*)"" report")]
    public void WhenISearchForTheReport(string reportType)
    {
        _reportsPageObject.SearchForReport(reportType);
    }

    [When(@"I run the ""([^""]*)"" report")]
    public void WhenIRunTheReport(string p0)
    {
       _reportsPageObject.RunReport();
    }

    [Then(@"I can view the respective accounts")]
    public void ThenICanViewTheRespectiveAccounts()
    {
        _reportsPageObject.AssertReportResultsPresent();
    }
}