Feature: Change My Password
    In order to protect access to my sensitive information
    As a UCosmic user
    I want to change my password

@UsingFreshExamplePasswords
Scenario Outline: Change Password successfully after making typing mistakes

    Given I am using the <Browser> browser
    And I am signed in as <EmailAddress>
    And I am starting from the Personal Home page
    And I see a "Change Password" link

    When I click the "Change Password" link
    Then I should see the Change Password page
    And I should see a "Cancel" link

    # test cancel link
    When I click the "Cancel" link
    Then I should see the Personal Home page
    When I click the "Change Password" link
    Then I should see the Change Password page

    # fields should not be empty
    When I type "" into the Current Password text field
    And I type "" into the New Password text field
    And I type "" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should still see the Change Password page
    And I should see the Required error message for the Current Password text field
    And I should see the Required error message for the New Password text field
    And I should see the Required error message for the New Password Confirmation text field

    # password should not be too short
    When I type "asdfasdf" into the Current Password text field
    And I type "pass" into the New Password text field
    And I type "pass" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should still see the Change Password page
    And I should see the Too Short error message for the New Password text field
    But I should not see any error messages for the Current Password text field
    And I should not see any error messages for the New Password Confirmation text field

    # password and confirmation should match
    When I type "asdfasdf" into the Current Password text field
    And I type "password" into the New Password text field
    And I type "passwrod" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should still see the Change Password page
    And I should see the No Match error message for the New Password Confirmation text field
    But I should not see any error messages for the Current Password text field
    And I should not see any error messages for the New Password text field

    # password confirmation should not be empty
    When I type "asdfasdf" into the Current Password text field
    And I type "password" into the New Password text field
    And I type "" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should still see the Change Password page
    And I should see the Required error message for the New Password Confirmation text field
    But I should not see any error messages for the Current Password text field
    And I should not see any error messages for the New Password text field

    # current password should be valid
    When I type "incorrect" into the Current Password text field
    And I type "password" into the New Password text field
    And I type "password" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should still see the Change Password page
    And I should see the Invalid error message for the Current Password text field
    But I should not see any error messages for the New Password text field
    And I should not see any error messages for the New Password Confirmation text field

    # change the password
    When I type "asdfasdf" into the Current Password text field
    And I type "newpwd" into the New Password text field
    And I type "newpwd" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your password has been changed. Use your new password to sign on next time."
    And I should see a "Sign Out" link

    # sign out and sign back in with the new password
    When I click the "Sign Out" link
    Then I should see the Sign Out page
    And I should see a "Sign On" link

    When I click the "Sign On" link
    Then I should see the Sign On page

    When I type "<EmailAddress>" into the Email Address text field
    And I click the "Next >>" submit button
    Then I should see the Enter Password page

    When I type "newpwd" into the Password text field
    And I click the "Sign On" submit button
    Then I should see the Personal Home page
    And I should see a "Sign Out" link
    And I should see a "Change Password" link

    When I click the "Change Password" link
    Then I should see the Change Password page

    # change the password back to original value
    When I type "newpwd" into the Current Password text field
    And I type "asdfasdf" into the New Password text field
    And I type "asdfasdf" into the New Password Confirmation text field
    And I click the "Change Password" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your password has been changed. Use your new password to sign on next time."
    And I should see a "Sign Out" link

Examples:
    | Browser | EmailAddress      |
    | Chrome  | any1@usil.edu.pe  |
    | Firefox | any1@bjtu.edu.cn  |
    | MSIE    | any1@napier.ac.uk |
