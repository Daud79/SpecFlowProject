Feature: Contact

Aims to test the functionality around contacts

@contact @create
Scenario: Creating a contact
	Given I have navigated to 1CRM
	And I login as "admin"
	When I navigate to the "Contacts" page under "Sales & Marketing"
	And I create a new contact
		| firstName | lastName | categories          | role  |
		| Daud      | Abbas    | Customers,Suppliers | Admin |
	Then the new contact has been successfully created