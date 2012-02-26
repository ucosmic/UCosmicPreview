@Auth
Feature:  Authentication
	      In order to access my protected information
	      As a UCosmic user
	      I want to sign into and out of UCosmic.com

@Auth01
Scenario Outline: Sign in unsuccessfully with invalid email or password from the sign-<InOrOut1> url
	Given I have browsed to the "sign-<InOrOut1>" url
    When  I type "<SignInEmail>" into the "EmailAddress" text box on the Username & Password form
    And   I type "<Password>" into the "Password" text box on the Username & Password form
	And   I click the "Sign In" button on the Username & Password form
    Then  I should see a page at the "sign-<InOrOut2>" url
    And   I should see the error message "<SignInEmailError>" for the "EmailAddress" text box on the Username & Password form
    And   I should see the error message "<PasswordError>" for the "Password" text box on the Username & Password form
    And   I should see the error message "<SummaryError>" in the error summary on the Username & Password form
Examples: 
    | InOrOut1 | SignInEmail   | Password | SignInEmailError                    | PasswordError                      | SummaryError                       | InOrOut2 |
    | in       |               |          | Email Address is required.          | Password is required.              |                                    | in       |
    | out      |               |          | Email Address is required.          | Password is required.              |                                    | out      |
    | in       | any1@uc.edu   |          |                                     | Password is required.              |                                    | in       |
    | out      | any1@suny.edu |          |                                     | Password is required.              |                                    | out      |
    | in       |               | asdfasdf | Email Address is required.          |                                    |                                    | in       |
    | out      |               | asdfasdf | Email Address is required.          |                                    |                                    | out      |
    | in       | non1@uc.edu   | asdfasdf |                                     | Invalid email address or password. | Invalid email address or password. | in       |
    | out      | non1@suny.edu | asdfasdf |                                     | Invalid email address or password. | Invalid email address or password. | in       |
    | in       | invalid email | asdfasdf | Please enter a valid email address. |                                    |                                    | in       |
    | out      | invalid email | asdfasdf | Please enter a valid email address. |                                    |                                    | out      |

@Auth02
Scenario Outline: Sign in successfully as user <SignInEmail> from the sign-<InOrOut> url
	Given I have browsed to the "sign-<InOrOut>" url
    When  I type "any1@uc.edu" into the "EmailAddress" text box on the Username & Password form
    And   I type "asdfasdf" into the "Password" text box on the Username & Password form
	And   I click the "Sign In" button on the Username & Password form
	Then  I should see a page at the "my/profile" url
    And   I should see a top identity signed-in message with "any1@uc.edu" greeting
Examples: 
    | InOrOut |
    | in      |
    | out     |

@Auth03
Scenario Outline: Sign out successfully as user <SignInEmail> from the <StartAt> url
	Given I have signed in as "<SignInEmail>" with password "asdfasdf"
    And   I have browsed to the "<StartAt>" url
    And   I have seen a "Sign Out" link
    When  I click the "Sign Out" link
    Then  I should see a page at the "sign-out[PathVar]" url
    And   I should see a "Sign In" link
Examples: 
    | SignInEmail     | StartAt                        |
    | any1@uc.edu     |                                |
    | any1@uc.edu     | my/profile                     |
    | manager1@uc.edu | my/institutional-agreements/v1 |



