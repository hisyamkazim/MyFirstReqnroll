Feature: Register
Feature to Register Account

@CleanUpDatabase
Scenario: Successful Register
User successfully registered
Given User choose to Register
When User fills Registration Form as follows
| FirstName         | LastName         | Address         | City            | State   | ZipCode | Phone               | SSN            | Username         | Password | ConfirmPassword |
| {{FakeFirstName}} | {{FakeLastName}} | {{FakeAddress}} | Jakarta Selatan | Jakarta | 13120   | {{FakePhoneNumber}} | {{FakeNumber}} | {{FakeUsername}} | demo     | demo            |
Then User should get success message "Your account was created successfully"

Scenario: Username already existed
Username that user choose already taken by other user
Given User choose to Register
When User fills Registration Form as follows
| FirstName | LastName         | Address         | City          | State   | ZipCode | Phone               | SSN            | Username | Password | ConfirmPassword |
| John      | {{FakeLastName}} | {{FakeAddress}} | Jakarta Timur | Jakarta | 13120   | {{FakePhoneNumber}} | {{FakeNumber}} | john     | demo     | demo            |
Then User should get error message "This username already exists"
