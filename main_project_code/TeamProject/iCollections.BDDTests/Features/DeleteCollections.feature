Feature: Delete Collections
	Baltazar Ortiz #177895376
    This is going to test deleting collections. That the collections controller correctly chooses
    whether a collection gets deleted.

@ignore
Scenario Outline: User with no collections plans gets shown that he does not have collections.
    Given when a user has no iCollections 
    When he goes to the page showing the list of his collections
    Then it will say something like "No collections for this user"

@ignore
Scenario Outline: The user who has published collections has a delete button to choose which collection he or she wants to delete
    Given when a user has iCollection(s)
    When he goes to the page showing the list of his collection(s)
    Then the collection names will be shown in a table and each row will have a red delete button called "Delete"

@ignore
Scenario Outline: The user clicks on the delete button when viewing his collections and goes to a confirmation page.
    Given the user has iCollections
    And is in the page showing his list of icollections
    When he clicks the delete button
    Then he will go to a confirmation page showing the icollection information of the icollection he wants to delete along with one button that says "Cancel" and another that says "Delete this iCollection"

@ignore
Scenario Outline: The user clicks the cancel button when on the delete [collection name] confirmation page.
    Given the user is in the confirmation page of his icollection he wants to delete
    When he presses the "cancel" button
    Then he goes to the page showing the list of his collection(s)

@ignore
Scenario Outline: The user confirms they want to delete their collection and getting redirected.
    Given the user is in the confirmation page of his icollection he wants to delete 
    When he presses the "Delete this iCollection" button 
    Then the user will be directed back to the display iCollections page and there will be a message that says, "[name of deleted collection] deleted", the selected iCollection will be deleted by the app and will not show up on display page anymore

@ignore
Scenario Outline: Another user gets a 404 error when trying to delete another user's icollection.
    Given a user is not the owner of a certain iCollection 
    And is on the page showing another user's icollections
    When he presses the Delete button
    Then he will get redirected to an error page showing 404 error

