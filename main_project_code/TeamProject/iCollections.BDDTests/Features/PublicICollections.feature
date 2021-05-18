Feature: PublicICollections

Baltazar Ortiz #178115988
Being able to see registered users public iCollections.
Public iCollections can be viewed via the profile page. Note: I am not logged in.

Scenario Outline: General public can view iCollections for a registered user with iCollections via iCollections link
    Given I am on '<User>' profile page
	And the user has iCollections
    When I click on the <Link>
    Then I can see a registered user's public iCollections
    Examples:
    | User          | Link          |
    | DavilaH       | iCollections  |
    | TaliaK        | iCollections  |
    | ZaydenC       | iCollections  |
    | KrzysztofP    | iCollections  |

Scenario Outline: General public can view iCollections for a registered user with iCollections via Show all link
    Given I am on '<User>' profile page
    And the user has iCollections
    When I click on the <Link>
    Then I can see a registered user's public iCollections
    Examples:
    | User          | Link          |
    | DavilaH       | Show all      |
    | TaliaK        | Show all      |
    | ZaydenC       | Show all      |
    | KrzysztofP    | Show all      |

