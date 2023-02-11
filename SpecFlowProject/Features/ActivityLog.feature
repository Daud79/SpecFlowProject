Feature: Activity Log

Aims to test the functionality around the activity log

@activity-log @delete
Scenario: Remove events from activity log
	Given I have navigated to 1CRM
	And I login as "admin"
	And I navigate to the "Activity Log" page under "Reports & Settings"
	When I "Delete" the first 3 items in the list
	Then the removed items are deleted from the system