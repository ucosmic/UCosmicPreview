@RepsModule

# Recruitment Agency Supervisor role must be seeded in database.
# See UCosmic.Impl/Seeders/RoleSeeder.cs.

# To facilitate testing, also add all users with the name supervisor to this role.
# See UCosmic.Impl/Seeders/UserSeeder.cs, specifically the "supervisorRoles" array.

# See the RA_US_0002 scribble file(s) associated with this feature.

Feature: RA_US_0002 - Configure Recruitment Agency Module for Academic Institution
	In order to customize the recruitment agency module for my institution
	As a recrutiment agency supervisor
	I want to configure certain recruitment agency module options



Scenario: Navigate to Recruitment Agency Module Configuration pages

    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
    When I browse to the Home page
    Then I should see a "Representatives" link

    When I click the "Representatives" link
    Then I should see a page at the "www.uc.edu/recruitment-agencies" url
    And I should see a "Configure module" link

    When I click the "Configure module" link
    Then I should see a page at the "www.uc.edu/recruitment-agencies/configure" url
    And I should see a "Welcome Message" link
    And I should see a "Notifications" link

    When I click the "Welcome Message" link
    Then I should see a page at the "www.uc.edu/recruitment-agencies/configure/welcome-message" url
    And I should see a "Notifications" link

    When I click the "Notifications" link
    Then I should see a page at the "www.uc.edu/recruitment-agencies/configure/notifications" url
    And I should see a "Welcome message" link



Scenario: Prevent unauthorized navigation to the Recruitment Agency Module Configuration pages

	Given I have signed in as "supervisor1@suny.edu" with password "asdfasdf"
	When I browse to the "www.uc.edu/recruitment-agencies" url
    Then I should not see a "Configure module" link

    When I browse to the "www.uc.edu/recruitment-agencies/configure" url
    Then I should see a 404 Not Found page

    When I browse to the "www.uc.edu/recruitment-agencies/configure/welcome-message" url
    Then I should see a 404 Not Found page

    When I browse to the "www.uc.edu/recruitment-agencies/configure/notifications" url
    Then I should see a 404 Not Found page



Scenario: Prevent anonymous navigation to the Recruitment Agency Module Configuration pages

    Given I have signed out
	When I browse to the "www.uc.edu/recruitment-agencies" url
    Then I should not see a "Configure module" link

    When I browse to the "www.uc.edu/recruitment-agencies/configure" url
    Then I should see a 404 Not Found page

    When I browse to the "www.uc.edu/recruitment-agencies/configure/welcome-message" url
    Then I should see a 404 Not Found page

    When I browse to the "www.uc.edu/recruitment-agencies/configure/notifications" url
    Then I should see a 404 Not Found page


