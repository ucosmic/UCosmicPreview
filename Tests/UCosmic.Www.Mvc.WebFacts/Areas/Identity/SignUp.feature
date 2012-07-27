Feature: Sign Up
    In order to sign into UCosmic.com
    As a consortium member
    I want to sign up and create a password

Background:
    Given I am starting from the Sign On page

@GeneratesEmail @UsingFreshExampleUnregisteredEmailAddresses
Scenario Outline: Sign Up succeeds after receiving secret confirmation code

    # go from sign on page to sign up page
    Given I am using the <Browser> browser
    When I type "<EligibleEmail>" into the Email Address text field
    And I click the "Next >>" submit button
    Then I should see the Sign Up page
    And the "Email me a confirmation code..." check box should be unchecked
    And the "Send Confirmation Email" submit button should be disabled

    # check box to enable submit button
    When I check the "Email me a confirmation code..." check box
    Then the "Send Confirmation Email" submit button should become enabled

    # send the confirmation email
    When I click the "Send Confirmation Email" submit button
    Then I should see the Confirm Email Ownership page
    And I should see the flash feedback message "A sign up confirmation email has been sent to <EligibleEmail>."

    # make sure confirmation code is not empty
    When I click the "Confirm Email Address" submit button
    Then I should see the Required error message for the Confirmation Code text field

    # make sure confirmation code is not incorrect
    When I type "test" into the Confirmation Code text field
    And I click the "Confirm Email Address" submit button
    Then I should see the Invalid error message for the Confirmation Code text field

    # enter code from email message
    When I receive mail with the subject "Confirm your email address for UCosmic.com"
    And I type the mailed code into the Confirmation Code text field
    And I click the "Confirm Email Address" submit button
    Then I should see the Create Password page
    And I should see the flash feedback message "Your email address has been confirmed. Please create your password now."

    # make sure password fields are not empty
    When I click the "Create Password" submit button
    Then I should still see the Create Password page
    And I should see the Required error message for the Password text field
    And I should see the Required error message for the Password Confirmation text field

    # make sure password is not too short
    When I type "pass" into the Password text field
    And I click the "Create Password" submit button
    Then I should still see the Create Password page
    But I should see the 'Too Short' error message for the Password text field
    And I should see the Required error message for the Password Confirmation text field

    # make sure confirmation is not empty
    When I type "password" into the Password text field
    And I click the "Create Password" submit button
    Then I should still see the Create Password page
    But I should not see any error messages for the Password text field
    And I should see the Required error message for the Password Confirmation text field

    # make sure confirmation matches password
    When I type "pass" into the Password Confirmation text field
    And I click the "Create Password" submit button
    Then I should still see the Create Password page
    But I should not see any error messages for the Password text field
    And I should see the 'No Match' error message for the Password Confirmation text field

    # proceed creating password
    When I type "password" into the Password Confirmation text field
    And I click the "Create Password" submit button
    Then I should see the Enter Password page
    And I should see the flash feedback message "You can now use your password to sign on."

    # attempt sign in without entering password
    When I click the "Sign On" submit button
    Then I should still see the Enter Password page
    And I should see the Required error message for the Password text field

    # attempt sign in with incorrect password
    When I type "incorrect" into the Password text field
    And I click the "Sign On" submit button
    Then I should still see the Enter Password page
    And I should see the 'Invalid with 4 remaining attempts' error message for the Password text field

    # sign in successfully
    When  I type "password" into the Password text field
    And   I click the "Sign On" submit button
    Then  I should see the Personal Home page

Examples:
    | Browser | EligibleEmail       |
    | Chrome  | new@bjtu.edu.cn     |
    | Firefox | new@usil.edu.pe     |
    | MSIE    | new@griffith.edu.au |
