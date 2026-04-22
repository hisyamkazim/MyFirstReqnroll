Feature: Login
Feature to login into the app

@InitDatabase @CleanUpDatabase
Scenario Outline: 1. Successful Login
Given User goes to Login page
When User enters username "<username>" and password "<password>"
Then User should be logged in successfully
And User should see welcome message "<name>"

Examples:
| username | password | name              |
| john     | demo     | John Smith |