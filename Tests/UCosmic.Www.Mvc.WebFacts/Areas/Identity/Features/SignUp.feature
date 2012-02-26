@Identity @SignUp
Feature:  Sign Up
	      In order to sign into UCosmic.com
	      As a consortium member
	      I want to sign up and create a password

Background: 
    Given I browsed to the Sign Up page

@SignUpHappy @SignUpSad @SignUp01
Scenario Outline: Qualify for sign up successfully with an eligible email address after entering an invalid email address

    When  I type "<InvalidEmail>" into the Email Address text box
    And   I double click the "<ButtonLabel>" button
    Then  I should still see the Sign Up page
    And   I should see an error message "<ErrorMessage>" under the Email Address text box
    And   I should not see a "Congratulations, your email address is eligible!" message
    And   I should not see [green checkmark icon] content

    When  I type "<EligibleEmail>" into the Email Address text box
    And   I double click the "Check Eligibility" button
    Then  I should still see the Sign Up page
    But   I should not see an error message "<ErrorMessage>" under the Email Address text box
    And   I should see a "Congratulations, your email address is eligible!" message
    And   I should see [green checkmark icon] content

Examples: 
    | InvalidEmail         | ButtonLabel             | ErrorMessage                                                              | EligibleEmail     |
    |                      | Check Eligibility       | Email Address is required.                                                | new@uc.edu        |
    | invalid email        | Check Eligibility       | Please enter a valid email address.                                       | new@ucmail.uc.edu |
    | any1@ineligible1.edu | Check Eligibility       | Sorry, emails ending in '@ineligible1.edu' are not eligible at this time. | new@suny.edu      |
    | any1@uc.edu          | Check Eligibility       | The email 'any1@uc.edu' has already been signed up.                       | new@umn.edu       |
    |                      | Send Confirmation Email | Email Address is required.                                                | new@uc.edu        |
    | invalid mail         | Send Confirmation Email | Please enter a valid email address.                                       | new@ucmail.uc.edu |
    | any1@ineligible2.edu | Send Confirmation Email | Sorry, emails ending in '@ineligible2.edu' are not eligible at this time. | new@suny.edu      |
    | any1@umn.edu         | Send Confirmation Email | The email 'any1@umn.edu' has already been signed up.                      | new@umn.edu       |

@SignUpHappy @SignUpSad @SignUp02 @GeneratesEmail @SignUpResetNewUsers
Scenario Outline: Receive sign up email confirmation message successfully for <EligibleEmail> using <BrowserName>

    Given I am using the <BrowserName> browser
    
    When  I type "<EligibleEmail>" into the Email Address text box
    And   I click the "Send Confirmation Email" button
    Then  I should see the Sign Up Confirm Email page
    And   I should see a temporary "A confirmation email has been sent to <EligibleEmail>" feedback message
    
    When  I click the "Confirm Email Address" button
    Then  I should still see the Sign Up Confirm Email page
    And   I should see an error message "Please enter a confirmation code." under the Confirmation Code text box
    
    When  I type "test" into the Confirmation Code text box
    And   I click the "Confirm Email Address" button
    Then  I should still see the Sign Up Confirm Email page
    But   I should see an error message "Invalid confirmation code, please try again." under the Confirmation Code text box
    
    When  I receive an email with subject "Confirm your email address for UCosmic.com"
    And   I type the emailed code into the Confirmation Code text box
    And   I click the "Confirm Email Address" button
    Then  I should see the Sign Up Create Password page
    And   I should see a temporary "Your email address was successfully confirmed" feedback message
    
    When  I click the "Create Password" button
    Then  I should still see the Sign Up Create Password page
    And   I should see an error message "Password is required." under the Password text box
    And   I should see an error message "Password confirmation is required." under the Confirmation text box
    
    When  I type "pass" into the Password text box
    And   I click the "Create Password" button
    Then  I should still see the Sign Up Create Password page
    But   I should see an error message "Your password must be at least 6 characters long (but no more than 100)." under the Password text box
    And   I should see an error message "Password confirmation is required." under the Confirmation text box
    
    When  I type "password" into the Password text box
    And   I click the "Create Password" button
    Then  I should still see the Sign Up Create Password page
    But   I should not see any error messages under the Password text box
    And   I should see an error message "Password confirmation is required." under the Confirmation text box
    
    When  I type "pass" into the Confirmation text box
    And   I click the "Create Password" button
    Then  I should still see the Sign Up Create Password page
    But   I should not see any error messages under the Password text box
    And   I should see an error message "The password and confirmation password do not match." under the Confirmation text box

    When  I type "password" into the Confirmation text box
    And   I click the "Create Password" button
    Then  I should see the Sign Up Completed page
    And   I should see a temporary "Your password was created successfully" feedback message

    When  I click the "Sign In" button
    Then  I should still see the Sign Up Completed page
    And   I should see an error message "Password is required." under the Password text box

    When  I type "password" into the Password text box
    And   I click the "Sign In" button
    Then  I should see a page at the "my/profile" url

Examples: 
    | BrowserName       | EligibleEmail       |
    | Chrome            | new@bjtu.edu.cn     |
    | Firefox           | new@usil.edu.pe     |
    | Internet Explorer | new@griffith.edu.au |

