Feature: Sign On
    In order to access my protected information
    As a UCosmic user
    I want to sign on using my email address

Background:

    When I am not signed on
    Then I should see a "Sign On" link

    When I click the "Sign On" link
    Then I should see the Sign On page

Scenario: Sign On fails when email address is empty

    When I click the "Next >>" submit button
    Then I should see the Required error message for the Email Address text field

Scenario: Sign On fails when email address is invalid

    When I type "invalid" into the Email Address text field
    And I click the "Next >>" submit button
    Then I should see the Invalid error message for the Email Address text field

Scenario: Sign On fails when email address is ineligible

    When I type "test@gmail.com" into the Email Address text field
    And I click the "Next >>" submit button
    Then I should see the 'test@gmail.com is Ineligible' error message for the Email Address text field

@ClearSigningEmailAddress
Scenario: Sign On redirects to Enter Password page for local accounts

    When I type "any1@suny.edu" into the Email Address text field
    And I click the "Next >>" submit button
    Then I should see the Enter Password page
