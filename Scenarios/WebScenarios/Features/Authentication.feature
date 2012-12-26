Feature: AuthenticationFeature
	In order to access the system
	As an administrator
	I want to be able to logon using forms authentication

@session
Scenario: Logon with forms authentication
	Given I am registered
	And I am not logged on
	And I enter valid credentials into the logon form
	When I press logon
	Then the ItsA user page should be shown

@session
Scenario: Register user
	Given I do not exist as a user
	And I enter valid details into the registration form
	When I press register
	Then the ItsA user page should be shown

Scenario: Unregister user
	Given I am registered
	And I am logged on
	When I press unregister
	#Then I should be asked to confirm the deregistration
	Then  the ItsA register page should be shown

