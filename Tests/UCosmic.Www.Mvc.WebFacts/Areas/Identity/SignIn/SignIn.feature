Feature: Sign In
    In order to access my protected information
    As a UCosmic user
    I want to sign in using my password

Background:
    Given I am starting from the Home page
    When I am not signed on
    Then I should see a "Sign On" link

    When I click the "Sign On" link
    Then I should see the Sign On page

    When I type "any1@suny.edu" into the Email address field
    And I click the "Next >>" submit button
    Then I should see the Enter Password page

@ClearSigningEmailAddress
Scenario: Sign In fails when password is empty
    When I click the "Sign On" submit button
    Then I should see the Required error message for the Password field

@ClearSigningEmailAddress
Scenario: Sign In fails when password is incorrect
    When I type "incorrect" into the Password field
    And I click the "Sign On" submit button
    Then I should see the 'Invalid with 4 remaining attempts' error message for the Password field

Scenario: Sign In succeeds when password is correct
    When I type "asdfasdf" into the Password field
    And I click the "Sign On" submit button
    Then I should see the Personal Home page
    And I should see a "Sign Out" link

