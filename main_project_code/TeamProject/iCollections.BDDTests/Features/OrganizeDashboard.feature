Feature: Organize dashboard events
	This feature is setting up a dashboard page that organizes the events
    (events are his followees and friends' icollections posted, secondhand friends, and who his followees have followed)
    so that the most recent events are towards the top of the page and the earlier ones are towards the bottom
    of the page. Baltazar Ortiz #177922126

Scenario Outline: When a user does not follow nor is friends with another user, then there will be no events to show.
    Given a user does not follow anybody 
    And is not friends with another app user (no events)
    When he goes to the dashboard
    Then the dashboard will say "No events yet"

Scenario Outline: Users that have activity on their dashboard get it displayed in an organized manner.
    Given a user has events
    When he goes to the dashboard
    Then the dashboard will display at most 15 events in chronological order descending