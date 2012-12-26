﻿Feature: AddBlogPosts
	In order to add posts to the blog
	As a blog administrator
	I want to be able to add blog entries

Scenario: Add a blog post as an administrator
	Given I am logged on
	When I press add blog post
	Then the new blog post screen show be shown

Scenario: Add a blog post as a reader
	Given I am not logged on
	And I browse to the home page
	Then the add blog post button is not visible
