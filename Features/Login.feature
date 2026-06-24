Feature: Login
Feature to login into the app

  @InitDatabase @CleanUpDatabase
  Scenario Outline: Login Data Driven
    Given User goes to Login page
    When User enters username "<username>" and password "<password>"
    Then User should be logged in successfully
    And User should see welcome message

    Examples:
      | username | password |
      | john     | demo     |
      | jane     | demo     |
