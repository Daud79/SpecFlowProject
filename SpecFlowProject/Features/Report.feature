Feature: Report

Aims to test the functionality around reporting

@report
Scenario: Producing a report
	Given I have navigated to 1CRM
	And I login as "admin"
	When I navigate to the "Reports" page under "Reports & Settings"
	And I search for the "Project Profitability" report
	When I run the "Project Profitability" report
	Then I can view the respective accounts