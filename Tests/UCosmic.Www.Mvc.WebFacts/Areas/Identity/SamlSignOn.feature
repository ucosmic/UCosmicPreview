Feature: Sign in without creating a separate UCosmic user account
	In order to avoid setting up a separate UCosmic user account and forgetting my UCosmic password
	As an employee of an academic institution
	I want to sign into UCosmic using my institutional username and password

@currentwork
Scenario: Sign in page only displays email text box by default
    Given I browsed to the Home page
	And I have seen a "Sign In" link
	When I click the "Sign In" link
	Then I should see the Sign In page
