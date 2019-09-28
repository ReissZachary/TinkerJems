Feature: AddItemEntries

A short summary of the feature

@tag1
Scenario: NotNullName
	Given the name textbox contains a "name"
	When the user clicks the add item button
	Then The item with "name" is added

#Scenario Outline: Successful adding of item
#	Given the user is at the Add Item page
#	When the user enters valid credentials
#	And clicks the AddItem button
#	Then Item should be added
#
#	Examples:	
#	| Name     | Price | Short_Description | Long_Description          |
#	| Necklace | 5.00  | Beautiful thing   | Best necklace in the west |
#	| Ring     | 10.00 | Small but great   | Best ring in the west     |
#	| Earring  | 5.00  | Hangs low         | Best earrings in the west |
#	| test     | 75.00 | Wow...just wow    | Best test in the west     |


 