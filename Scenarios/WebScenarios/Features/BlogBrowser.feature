Feature: BlogBrowser
	In order to read blog posts
	As an unregistered user
	I want to be able to view the blog


Scenario: View Main Page
	Given I am a reader
	And There are posts available
	Then I can read the latest posts
