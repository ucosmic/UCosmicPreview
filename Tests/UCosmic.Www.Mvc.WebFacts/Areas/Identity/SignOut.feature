Feature: Sign out
    In order to protect my information from others who may use the same computer as me
    As a UCosmic user
    I want to sign out of UCosmic

Scenario Outline: Sign Out prevents access to protected pages

	Given I am signed in as <User>
	Then I should see a "Sign Out" link

	When I click the "Sign Out" link
	Then I should see the Sign Out page

    When I enter the URL for the Personal Home page
    Then I should see the Sign On page

    When I enter the URL for the Institutional Agreement Management page
    Then I should see the Sign On page

    When I enter the URL for the Institutional Agreement Configure Module page
    Then I should see the Sign On page

Examples:
    | User                   |
    | manager1@usil.edu.pe   |
    | manager1@suny.edu      |
    | supervisor1@lehigh.edu |
