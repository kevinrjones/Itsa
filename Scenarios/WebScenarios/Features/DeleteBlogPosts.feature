Feature: DeleteBlogPosts
	As an administrator
	I want to be able to delete blog posts
	In order to remove unwanted entries

@addpost
@deletepost
Scenario: Delete a blog post should be available to an administrator
	Given I am logged on
	Then the delete blog post element show be shown

@addpost
Scenario: Delete a blog post as an administrator
	Given I am logged on
	When I click the delete post element
	Then the post should deleted

@addpost
@deletepost
Scenario: Delete a blog post as a reader
	Given I am not logged on
	And I browse to the home page
	Then the delete blog post button is not visible
