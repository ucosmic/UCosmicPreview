@Self
@SelfR03
Feature:  User Change Email Spelling
	      In order to correct mistakes in capitalization
	      As a UCosmic.com user
	      I want to change the spelling of my email addresses

#execute these steps before every scenario in this file
Background: 
    Given I have signed in as "any1@uc.edu" with password "asdfasdf"
    And   I have browsed to the "my/profile" url

@SelfRespellEmail
@SelfR0301
Scenario Outline:My Change Email Spelling form changes spelling unsuccessfully with invalid inputs
	Given I have clicked the "Change spelling" link
	And   I have seen a page at the "me/emails/[PathVar]/change-spelling.html" url
	When  I type "<NewSpelling>" into the "Value" text box on the Change Email Spelling form
	And   I click the "Save Changes" button on the Change Email Spelling form
	Then  I should see the error message "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address." for the "Value" text box on the Change Email Spelling form
	Examples:
	| NewSpelling    |
	|                |
	| junkvalues     |
	| fkdkd@skd.csks |
	| any2@uc.edu    |

@SelfRespellEmail
@SelfR0302A
@OnlyInChrome
Scenario Outline:My Change Email Spelling form changes spelling successfully with valid inputs using Chrome
	Given I have seen a "Change spelling" link
	And   I have clicked the "Change spelling" link
	And   I have seen a page at the "me/emails/[PathVar]/change-spelling.html" url
	When  I type "<NewEmailAddress>" into the "Value" text box on the Change Email Spelling form
	And   I click the "Save Changes" button on the Change Email Spelling form
	Then  I should see a page at the "my/profile" url 
	And   I should see top feedback message "Your email address was successfully changed to <NewEmailAddress>."
    And   I should see the email address "<NewEmailAddress>" in the My Email Addresses form
	Examples:
	| NewEmailAddress |
	| any1@uc.EDU     |
	| ANY1@UC.EDU     |
	| ANY1@uc.edu     |
	| any1@uc.edu     |

@SelfRespellEmail
@SelfR0302B
@OnlyInFirefox
Scenario Outline:My Change Email Spelling form changes spelling successfully with valid inputs using Firefox
	Given I have seen a "Change spelling" link
	And   I have clicked the "Change spelling" link
	And   I have seen a page at the "me/emails/[PathVar]/change-spelling.html" url
	When  I type "<NewEmailAddress>" into the "Value" text box on the Change Email Spelling form
	And   I click the "Save Changes" button on the Change Email Spelling form
	Then  I should see a page at the "my/profile" url 
	And   I should see top feedback message "Your email address was successfully changed to <NewEmailAddress>."
    And   I should see the email address "<NewEmailAddress>" in the My Email Addresses form
	Examples:
	| NewEmailAddress |
	| any1@uc.EDU     |
	| ANY1@UC.EDU     |
	| ANY1@uc.edu     |
	| any1@uc.edu     |

@SelfRespellEmail
@SelfR0302C
@OnlyInInternetExplorer
Scenario Outline:My Change Email Spelling form changes spelling successfully with valid inputs using Internet Explorer
	Given I have seen a "Change spelling" link
	And   I have clicked the "Change spelling" link
	And   I have seen a page at the "me/emails/[PathVar]/change-spelling.html" url
	When  I type "<NewEmailAddress>" into the "Value" text box on the Change Email Spelling form
	And   I click the "Save Changes" button on the Change Email Spelling form
	Then  I should see a page at the "my/profile" url 
	And   I should see top feedback message "Your email address was successfully changed to <NewEmailAddress>."
    And   I should see the email address "<NewEmailAddress>" in the My Email Addresses form
	Examples:
	| NewEmailAddress |
	| any1@uc.EDU     |
	| ANY1@UC.EDU     |
	| ANY1@uc.edu     |
	| any1@uc.edu     |

@SelfRespellEmail
@SelfR0303
Scenario:My Change Email Spelling form successfully dismiss by clicking Cancel button
    Given I have clicked the "Change spelling" link
    And   I have seen a page at the "me/emails/[PathVar]/change-spelling.html" url
    When  I click the "Cancel" button on the Change Email Spelling form
    Then  I should see a page at the "my/profile" url 