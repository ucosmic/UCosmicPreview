@ResetPassword
Feature:  ResetPassword
	      In order to sign into UCosmic.com after forgetting my password
	      As a UCosmic user
	      I want to reset my password using my email address

@PasswordR04
Scenario Outline: Reset Password unsuccessfully with invalid email address
    Given I have signed out
	And   I have clicked the "Sign In" link
	And   I have seen a page at the "sign-in" url
	And   I have clicked the "forgot your password" link
	And   I have seen a page at the "i-forgot-my-password" url
	When  I type "<EmailAddress>" into the "EmailAddress" text box on the Reset Password form
	And   I click the "Send Confirmation Email" button on the Reset Password form
	Then  I should see the error message "<EmailError>" for the "EmailAddress" text box on the Reset Password form
	And   I should see the validation error message "<ServerValidationError>" for the "EmailAddress" text box on the Reset Password form

Examples: 
    | EmailAddress            | EmailError                          | ServerValidationError                                                               |
    |                         | Email Address is required.          |                                                                                     |
    | invalid email           | Please enter a valid email address. |                                                                                     |
    | not-signed-up@gmail.com |                                     | A user account with the email address 'not-signed-up@gmail.com' could not be found. |

@PasswordR05
Scenario: Reset Password form successfully dismiss by clicking Cancel button
    Given I have signed out
	And   I have clicked the "Sign In" link
	And   I have seen a page at the "sign-in" url
	And   I have clicked the "forgot your password" link
    And   I have seen a page at the "i-forgot-my-password" url
    When  I click the "Cancel" button on the Reset Password form
    Then  I should see a page at the "sign-in" url 