Feature: Follow
**As a registered user I would like to be able to follow and unfollow other users.**

This feature ensures that registered users can follow other users they do not follow and can unfollow users that
they do follow. Users cannot follow themselves. Users cannot follow or unfollow users that do not exist. It also 
*defines* a set of seeded users for future software test engineers to use when performing other kinds of tests.

To generate living documentation, create a Documentation folder and then run one of these from the project dir: 
    `livingdoc test-assembly -t bin\Debug\net5.0\TestExecution.json -o Documentation bin\Debug\net5.0\Fuji.BDDNunitTests.dll`
    `livingdoc feature-folder -t bin\Debug\net5.0\TestExecution.json -o Documentation .`

Background:
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

Scenario Outline: Logged-in user can follow a user they do not follow.
	Given I am a user with username '<UserName1>'
		And there is another user with username '<UserName2>' who I do not follow
	When I follow that user
	Then I now follow that user

	Examples:
	| UserName1 | UserName2 |
	| TaliaK    | ZaydenC   |
	| ZaydenC   | DavilaH   |
	| DavilaH	| TaliaK	|

Scenario Outline: Logged-in user can unfollow a user they follow.
	Given I am a user with username '<UserName1>'
		And there is another user with username '<UserName2>' who I follow
	When I unfollow that user
	Then I no longer follow that user

	Examples:
	| UserName1 | UserName2 |
	| TaliaK    | ZaydenC   |
	| ZaydenC   | DavilaH   |
	| DavilaH	| TaliaK	|

Scenario Outline: Logged-in user cannot follow themself.
	Given I am a user with username '<UserName>'
	When I follow myself
	Then that follow attempt fails
		And it returns a JSON message describing the failure

	Examples:
	| UserName   |
	| TaliaK     |
	| ZaydenC    |

Scenario Outline: Logged-in user cannot follow a user that does not exist.
	Given I am a user with username '<UserName1>'
		And there is not a user with ussername '<UserName2>'
	When I follow that user
	Then that follow attempt fails
		And it returns a JSON message describing the failure

	Examples:
	| UserName1 | UserName2 |
	| TaliaK    | AndreC    |
	| ZaydenC   | JoannaV   |