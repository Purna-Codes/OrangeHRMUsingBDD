Feature: DataDrivenTestingLogin

Logging into OrangeHRM

@Login
Scenario: Login With Valid Credentials
	Given Navigate to the Login Page using URL 'https://opensource-demo.orangehrmlive.com/web/index.php/auth/login'
	When username 'Admin' and password as 'admin123'
	Then HomePage must be displayed

@Login
Scenario: Login With Invalid Credentials
	Given Navigate to the Login Page using URL 'https://opensource-demo.orangehrmlive.com/web/index.php/auth/login'
	When Enter password as 'admin123' and username as 'purna' 
	Then Error Message must be displayed