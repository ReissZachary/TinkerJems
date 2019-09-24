Feature: AddItemFeatures

A short summary of the feature

@tag1
Scenario: NameNotNull
	Given the name box is not empty
	When they click "Add Item" button
	Then the item is added
