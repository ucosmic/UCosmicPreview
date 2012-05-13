Feature: ResetPassword
    In order to sign into UCosmic.com after forgetting my password
    As a UCosmic user
    I want to reset my password using my email address

@UsingFreshExamplePasswords
Scenario Outline: Reset Password succeeds after receiving secret confirmation code

    Given I am using the <Browser> browser
    And I am not signed on
    And I am starting from the Sign On page

    When I type "<EmailAddress>" into the Email Address text field
    And I click the "Next >>" submit button
    Then I should see the Enter Password page
    And I should see an "I forgot my password" link

    When I click the "I forgot my password" link
    Then I should see the Forgot Password page
    And I should see "<EmailAddress>" in the Email Address text field
    And I should see a "Cancel" link

    # test cancel link
    When I click the "Cancel" link
    Then I should see the Enter Password page
    When I click the "I forgot my password" link
    Then I should see the Forgot Password page

    # validation error for empty email address
    When I type "" into the Email Address text field
    And I click the "Send Confirmation Email" submit button
    Then I should still see the Forgot Password page
    And I should see the Required error message for the Email Address text field

    # validation error for invalid email address
    When I type "invalid email" into the Email Address text field
    And I click the "Send Confirmation Email" submit button
    Then I should still see the Forgot Password page
    And I should see the Invalid error message for the Email Address text field

    # validation error for ineligible email address
    When I type "test@gmail.com" into the Email Address text field
    And I click the "Send Confirmation Email" submit button
    Then I should still see the Forgot Password page
    And I should see the 'test@gmail.com not found' error message for the Email Address text field

    # send the confirmation email
    When I type "<EmailAddress>" into the Email Address text field
    And I click the "Send Confirmation Email" submit button
    Then I should see the Confirm Email Ownership page
    And I should see the flash feedback message "A password reset email has been sent to <EmailAddress>."

    # make sure confirmation code is not empty
    When I click the "Confirm Email Address" submit button
    Then I should see the Required error message for the Confirmation Code text field

    # make sure confirmation code is not incorrect
    When I type "test" into the Confirmation Code text field
    And I click the "Confirm Email Address" submit button
    Then I should see the Invalid error message for the Confirmation Code text field

    # enter code from email message
    When I receive mail with the subject "Password reset instructions for UCosmic.com"
    And I type the mailed code into the Confirmation Code text field
    And I click the "Confirm Email Address" submit button
    Then I should see the Reset Password page
    And I should see the flash feedback message "Your email address has been confirmed. Please reset your password now."

    # make sure password fields are not empty
    When I click the "Reset Password" submit button
    Then I should still see the Reset Password page
    And I should see the Required error message for the Password text field
    And I should see the Required error message for the Password Confirmation text field

    # make sure password is not too short
    When I type "pass" into the Password text field
    And I click the "Reset Password" submit button
    Then I should still see the Reset Password page
    But I should see the 'Too Short' error message for the Password text field
    And I should see the Required error message for the Password Confirmation text field

    # make sure confirmation is not empty
    When I type "password" into the Password text field
    And I click the "Reset Password" submit button
    Then I should still see the Reset Password page
    But I should not see any error messages for the Password text field
    And I should see the Required error message for the Password Confirmation text field

    # make sure confirmation matches password
    When I type "pass" into the Password Confirmation text field
    And I click the "Reset Password" submit button
    Then I should still see the Reset Password page
    But I should not see any error messages for the Password text field
    And I should see the 'No Match' error message for the Password Confirmation text field

    # proceed creating password
    When I type "password" into the Password Confirmation text field
    And I click the "Reset Password" submit button
    Then I should see the Enter Password page
    And I should see the flash feedback message "You can now use your new password to sign on."

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
    | Browser | EmailAddress      |
    | Chrome  | any1@usil.edu.pe  |
    | Firefox | any1@bjtu.edu.cn  |
    | MSIE    | any1@napier.ac.uk |
