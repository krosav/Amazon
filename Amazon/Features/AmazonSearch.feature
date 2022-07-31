Feature: QA Challenge for automating book searching on Amazon.co.uk

@qaChallenge
Scenario Outline: Search Amazon for a book and verify the search page
	Given I have navigated to the Amazon website <url>
	And I have entered <book> in the search type is <type>
	Then we have an item with the correct title
	And the item does not have a badge
	And we have a type <type> 

Examples: 
        | url          | book                              | type      |
        | amazon.co.uk | Harry Potter and the Cursed Child | Paperback |

@qaChallenge
Scenario Outline: Verify the details page for the book found
	Given I have navigated to the Amazon website <url>
	And I have entered <book> in the search type is <type>
	And I have navigated to the detail of the item
	Then the detailed item has the correct title
	And the detailed item has the correct type
	And the detailed item does not have a best-seller badge
	And the price is the same as the one from the search

Examples: 
        | url          | book                              | type      |
        | amazon.co.uk | Harry Potter and the Cursed Child | Paperback |

@qaChallenge
Scenario Outline: Verify the addition to the basket
	Given I have navigated to the Amazon website <url>
	And I have entered <book> in the search type is <type>
	And I have navigated to the detail of the item
	And I have added the item to the basket
	Then the notification is shown
	And there is one item in the basket

Examples: 
        | url          | book                              | type      |
        | amazon.co.uk | Harry Potter and the Cursed Child | Paperback |

@qaChallenge
Scenario Outline: Verify the content to the basket
	Given I have navigated to the Amazon website <url>
	And I have entered <book> in the search type is <type>
	And I have navigated to the detail of the item
	And I have added the item to the basket
    And I have clicked the basket
	Then the title of book is correct
	And the type is correct
	And the quantity is one
	And the price is the same as the one from the searchAnd 
	And the total price is the same as the price

Examples: 
        | url          | book                              | type      |
        | amazon.co.uk | Harry Potter and the Cursed Child | Paperback |