Feature: OrangeHRM login feature

Logging into OrangeHRM

@Login
Scenario: Login With Valid Credentials
	Given Navigate to the Login Page URL
	When Enter Valid Credentials
	Then HomePage must be displayed

@Login
Scenario: Login With Invalid Credentials
	Given Navigate to the Login Page URL
	When Enter Invalid Credentials
	Then Error Message must be displayed
