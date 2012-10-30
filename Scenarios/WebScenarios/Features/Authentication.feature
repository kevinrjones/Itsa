Feature: AuthenticationFeature
	In order to access the system
	As an administrator
	I want to be able to logon using forms authentication

@mytag
Scenario: Logon with forms authentication
	Given I am not logged on
	And I enter valid credentials into the logon form
	When I press logon
	Then the ItSa home page should be shown
