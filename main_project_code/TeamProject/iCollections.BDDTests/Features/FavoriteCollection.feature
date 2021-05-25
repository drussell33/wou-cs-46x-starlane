Feature: FavoriteCollection
	Favoriting A Collection 

	* If a user is viewing a collection, then there must be an associated "Favorite Button" associated with the collection. 
	* If a user is clicks on the favorite button, then the collection must be added to their favorites list.
	* If a user is clicks on the favorite button and the collection is already in their favorites list, then the user must be notified of it. 

 The process of adding a collection to 'My Favorites'

Background:

	Given the following collections exist
	  | Id | Name                       | Visibility | UserId | Route               |
	  | 10 | First Collection Fish      | 1          | 8      | Ocean_environment   |
	  | 13 | Second Collection Dogs     | 1          | 64     | gallery_environment |
	  | 16 | Third Collection Shoes     | 1          | 3      | Ocean_environment   |
	  | 45 | Forth Collection Puzzels   | 1          | 3      | gallery_environment |
	
	And the following collections do not exist
	  | Id | Name                       | Visibility | UserId | Route               |
	  | 0  | Bad Collection 1           | 1          | 8      | Ocean_environment   |
	  | 11 | Worse Collection 2         | 1          | 0      | gallery_environment |
	  | 15 | HORRIBLE Collection 3      | 1          | 3      | IMLOST!             |
	  | $0 | kityy Collection 4 | 9001       | 666    | WTFisHAPPENING!!!1! |

Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Hello123# |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Hello123# |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Hello123# |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Hello123# |

	And the following users do not exist
	  | UserName | Email               | FirstName | LastName | Password  |
	  | AndreC   | colea@example.com   | Andre     | Cole     | 0a9dfi3.a |
	  | JoannaV  | valdezJ@example.com | Joanna    | Valdez   | d9u(*dsF4 |

Given the following FavoriteCollections exist
	  | Name			| userid	| collectionid	| 
	  | My Favorites	| 1			| 16				| 
	  | My Favorites    | 2			| 45				| 
	  | My Favorites    | 3			| 45				| 
	  | My Favorites	| 4			| 13				|


	And the following FavoriteCollections do not exist
	  | Name				| userid	| collectionid	| 
	  | my not so favorites	| 6			| 2				| 
	  | i hate these		| 7			| 9				| 
	  | null				| 3			| 8				| 
	  | i forgot			| 4			| 3				|

	

@ignore
Scenario Outline: Clicking on the favorites button, the collection will be added to the users list if it is not already in my favorites
	Given I am a logged in user with user name '<username>'
	When I click on add to favorites button
	Then the corresonping '<collections>' will be added to '<favoritescollection>'
	  And I will be notified of it 
	Examples:
	| FirstName  | Collections  |favoritescollection |
	| Taliak     | 10			|My Favorites|
	| Zaydenc    | 16			|My Favorites|
	| Davilah    | 45			|My Favorites|
	| Krzysztofp | 16			|My Favorites|

@ignore
Scenario Outline: Clicking on the favorites button, if collection is already in a user's favorites list, the user must be notified of it and the collection will not be added.
	Given I am a logged in user with user name '<username>'
	When I click on add to favorites button
		And the collection is already in '<favoritecollections>'
	Then the '<collections>' will not be added
		And I will be notified
	Examples:
	| FirstName  | Collections  |favoritescollection |
	| Taliak     | 10			|My Favorites|
	| Zaydenc    | 16			|My Favorites|
	| Davilah    | 45			|My Favorites|
	| Krzysztofp | 16			|My Favorites|

