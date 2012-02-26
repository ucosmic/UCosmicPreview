@ChangePassword
Feature:  Change Passwords
	      In order to protect access to sensitive information
	      As a UCosmic user
	      I want to change my password

#execute these steps before every scenario in this file
Background: 
    Given I have signed in as "any1@suny.edu" with password "asdfasdf"
    And   I have browsed to the "my/profile" url

@ChangePassword01
Scenario Outline: My Email Address(es) Info form changes password unsuccessfully with invalid inputs  
    Given I have clicked the "Change Password" link
    And   I have seen a page at the "me/change-password.html" url
    When  I type "<OldPassword>" into the "OldPassword" text box on the Change Password form
    And   I type "<NewPassword>" into the "NewPassword" text box on the Change Password form 
    And   I type "<ConfirmPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I click the "Change Password" button on the Change Password form
    Then  I should see a page at the "me/change-password.html" url
    And   I should see the error message "<OldPasswordError>" for the "OldPassword" text box on the Change Password form
    And   I should see the error message "<NewPasswordError>" for the "NewPassword" text box on the Change Password form
    And   I should see the error message "<ConfirmPasswordError>" for the "ConfirmPassword" text box on the Change Password form
    Examples:
    | OldPassword | NewPassword | ConfirmPassword | OldPasswordError               | NewPasswordError                                  | ConfirmPasswordError                                     |
    |             |             |                 | Current password is required.  | New password is required.                         | Password confirmation is required.                       |
    | asdfasdf    | asdf        | asdf            |                                | Your password must be at least 6 characters long. |                                                          |
    | asdfasdf    | abcdabcd    | abcdabc         |                                |                                                   | The new password and confirmation password do not match. |

@ChangePassword02A
@OnlyInChrome
Scenario Outline: My Email Address(es) Info form changes password successfully with valid input using Chrome                                                                                                     
    Given I have clicked the "Change Password" link
    And   I have seen a page at the "me/change-password.html" url
    And   I have typed "<RealPassword>" into the "OldPassword" text box on the Change Password form
    And   I have typed "<TempPassword>" into the "NewPassword" text box on the Change Password form
    And   I have typed "<TempPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I have clicked the "Change Password" button on the Change Password form
    And   I have seen a page at the "my/profile" url 
    And   I have seen top feedback message "Your password has been changed successfully."
    And   I have seen a "<SignInEmail>" link
    And   I have clicked the "<SignInEmail>" link
    And   I have seen a "Sign Out" link
    And   I have clicked the "Sign Out" link
    And   I have seen a page at the "sign-out?returnUrl=[PathVar]" url
    And   I have typed "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I have typed "<TempPassword>" into the "Password" text box on the Username & Password form
    And   I have clicked the "Sign In" button on the Username & Password form
    And   I have seen a page at the "my/profile" url
    And   I have clicked the "Change Password" link
    And   I have typed "<TempPassword>" into the "OldPassword" text box on the Change Password form    
    And   I have typed "<RealPassword>" into the "NewPassword" text box on the Change Password form
    And   I have typed "<RealPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I have clicked the "Change Password" button on the Change Password form
    And   I have seen a page at the "my/profile" url
    And   I have seen top feedback message "Your password has been changed successfully."
    And   I have seen a "<SignInEmail>" link
    And   I have clicked the "<SignInEmail>" link
    And   I have seen a "Sign Out" link
    And   I have clicked the "Sign Out" link
    And   I have seen a page at the "sign-out?returnUrl=[PathVar]" url
    And   I have typed "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I have typed "<RealPassword>" into the "Password" text box on the Username & Password form  
    When  I click the "Sign In" button on the Username & Password form
    Then  I should see a page at the "my/profile" url
    And   I should see a top identity signed-in message with "<SignInEmail>" greeting
    Examples:
    | RealPassword | TempPassword  | SignInEmail   |
    | asdfasdf     | abcdabcd      | any1@suny.edu |
    | asdfasdf     | Newpassword@1 | any1@suny.edu |

@ChangePassword02B
@OnlyInFirefox
Scenario Outline: My Email Address(es) Info form changes password successfully with valid input using Firefox                                                                                                    
    Given I have clicked the "Change Password" link
    And   I have seen a page at the "me/change-password.html" url
    And   I have typed "<RealPassword>" into the "OldPassword" text box on the Change Password form
    And   I have typed "<TempPassword>" into the "NewPassword" text box on the Change Password form
    And   I have typed "<TempPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I have clicked the "Change Password" button on the Change Password form
    And   I have seen a page at the "my/profile" url 
    And   I have seen top feedback message "Your password has been changed successfully."
    And   I have seen a "<SignInEmail>" link
    And   I have clicked the "<SignInEmail>" link
    And   I have seen a "Sign Out" link
    And   I have clicked the "Sign Out" link
    And   I have seen a page at the "sign-out?returnUrl=[PathVar]" url
    And   I have typed "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I have typed "<TempPassword>" into the "Password" text box on the Username & Password form
    And   I have clicked the "Sign In" button on the Username & Password form
    And   I have seen a page at the "my/profile" url
    And   I have clicked the "Change Password" link
    And   I have typed "<TempPassword>" into the "OldPassword" text box on the Change Password form    
    And   I have typed "<RealPassword>" into the "NewPassword" text box on the Change Password form
    And   I have typed "<RealPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I have clicked the "Change Password" button on the Change Password form
    And   I have seen a page at the "my/profile" url
    And   I have seen top feedback message "Your password has been changed successfully."
    And   I have seen a "<SignInEmail>" link
    And   I have clicked the "<SignInEmail>" link
    And   I have seen a "Sign Out" link
    And   I have clicked the "Sign Out" link
    And   I have seen a page at the "sign-out?returnUrl=[PathVar]" url
    And   I have typed "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I have typed "<RealPassword>" into the "Password" text box on the Username & Password form  
    When  I click the "Sign In" button on the Username & Password form
    Then  I should see a page at the "my/profile" url
    And   I should see a top identity signed-in message with "<SignInEmail>" greeting
    Examples:
    | RealPassword | TempPassword  | SignInEmail   |
    | asdfasdf     | abcdabcd      | any1@suny.edu |
    | asdfasdf     | Newpassword@1 | any1@suny.edu |

@ChangePassword02C
@OnlyInInternetExplorer
Scenario Outline: My Email Address(es) Info form changes password successfully with valid input using Internet Explorer                                                                                                  
    Given I have clicked the "Change Password" link
    And   I have seen a page at the "me/change-password.html" url
    And   I have typed "<RealPassword>" into the "OldPassword" text box on the Change Password form
    And   I have typed "<TempPassword>" into the "NewPassword" text box on the Change Password form
    And   I have typed "<TempPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I have clicked the "Change Password" button on the Change Password form
    And   I have seen a page at the "my/profile" url 
    And   I have seen top feedback message "Your password has been changed successfully."
    And   I have seen a "Sign Out" link
    And   I have clicked the "Sign Out" link
    And   I have seen a page at the "sign-out?returnUrl=[PathVar]" url
    And   I have typed "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I have typed "<TempPassword>" into the "Password" text box on the Username & Password form
    And   I have clicked the "Sign In" button on the Username & Password form
    And   I have seen a page at the "my/profile" url
    And   I have seen a "Change Password" link
    And   I have clicked the "Change Password" link
    And   I have typed "<TempPassword>" into the "OldPassword" text box on the Change Password form    
    And   I have typed "<RealPassword>" into the "NewPassword" text box on the Change Password form
    And   I have typed "<RealPassword>" into the "ConfirmPassword" text box on the Change Password form
    And   I have clicked the "Change Password" button on the Change Password form
    And   I have seen a page at the "my/profile" url
    And   I have seen top feedback message "Your password has been changed successfully."
    And   I have seen a "Sign Out" link
    And   I have clicked the "Sign Out" link
    And   I have seen a page at the "sign-out?returnUrl=[PathVar]" url
    And   I have typed "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I have typed "<RealPassword>" into the "Password" text box on the Username & Password form  
    When  I click the "Sign In" button on the Username & Password form
    Then  I should see a page at the "my/profile" url
    And   I should see a top identity signed-in message with "<SignInEmail>" greeting
    Examples:
    | RealPassword | TempPassword  | SignInEmail   |
    | asdfasdf     | abcdabcd      | any1@suny.edu |
    | asdfasdf     | Newpassword@1 | any1@suny.edu |

@ChangePassword03
Scenario: My Email Address(es) Info form displays an error if the old password is incorrect
    Given I have clicked the "Change Password" link
    And   I have seen a page at the "me/change-password.html" url
    When  I type "asdfasd" into the "OldPassword" text box on the Change Password form
    And   I type "abcdabcd" into the "NewPassword" text box on the Change Password form
    And   I type "abcdabcd" into the "ConfirmPassword" text box on the Change Password form
    And   I click the "Change Password" button on the Change Password form
    Then  I should see a page at the "me/change-password.html" url
    And   I should see the validation error message "The Old Password is incorrect." for the "OldPassword" text box on the Change Password form

@ChangePassword04
Scenario: My Email Address(es) Info form successfully dismiss by clicking Cancel button
    Given I have clicked the "Change Password" link
    And   I have seen a page at the "me/change-password.html" url
    When  I click the "Cancel" button on the Change Password form
    Then  I should see a page at the "my/profile" url 
