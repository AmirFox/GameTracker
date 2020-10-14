Feature: Game List
  As a user
  I want to visit the game list page
  So that I can view available games

  Scenario: Open Game List page
    Given I am on the game-list
    Then I should see a "navbar"
    And I should see a "new game" link