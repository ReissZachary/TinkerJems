Feature: CreateItem

	A short summary of the feature


Background: Correct page
	Given I am at the start page
	When I click on Add Item button
	Then  I can see the add item page



@tag1
Scenario: Test data
	Given I entered the following data into boxes
	| Field             | Value                 |
	| Name              | Ring                  |
	| Price             | 10.00                 |
	| Short_Description | Has hearts on it      |
	| Long_Description  | Best ring you'll find |
	Then My binding should have the following data
	| Field             | Value                 |
	| Name              | Ring                  |
	| Price             | 10.00                 |
	| Short_Description | Has hearts on it      |
	| Long_Description  | Best ring you'll find |

Scenario Outline: Successful adding of item
	Given the user is at the Add Item page
	When the user enters valid credentials
	And clicks the AddItem button
	Then Item should be added

	Examples:	
	| Name     | Price | Short_Description | Long_Description          |
	| Necklace | 5.00  | Beautiful thing   | Best necklace in the west |
	| Ring     | 10.00 | Small but great   | Best ring in the west     |

