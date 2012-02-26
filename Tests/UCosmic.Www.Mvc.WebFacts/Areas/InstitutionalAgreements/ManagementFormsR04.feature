@InstAgrForms
@InstAgrFormsR04
Feature:  Institutional Agreement Management Preview Revision 4
		  In order to inform the people about the use of an expiration date field for my Institutional Agreements
		  As an Institutional Agreement Manager
		  I want to help the people enter expiration date information attached to my Institutional Agreements in UCosmic

#execute these steps before every scenario in this file
Background: 
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url

@InstAgrFormsR0401
Scenario Outline: Institutional Agreement forms display the Help link
    When  I click the "<LinkText>" link
    And   I see a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    Then  I should see a "Help" link
 Examples: 
 | AddOrEdit | LinkText              |
 | new       | Add a new agreement   |
 | edit      | Agreement, UC 01 test |

 @InstAgrFormsR0402
 Scenario Outline: Institutional Agreement forms successfully displays bubble pop up for the Help link
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a "Help" link
    When  I click the "Help" link
    Then  I should see a bubble pop up in the Institutional Agreements <AddOrEdit> form
Examples:
 | AddOrEdit | LinkText              |
 | new       | Add a new agreement   |
 | edit      | Agreement, UC 01 test |
 
 @InstAgrFormsR0403
 Scenario Outline: Institutional Agreement forms successfully closes the bubble pop up for the Help link
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a "Help" link
    And   I have clicked the "Help" link
    And   I have seen a bubble pop up in the Institutional Agreements <AddOrEdit> form
    When  I click the "Close this popup" link
    Then  I should not see a bubble pop up in the Institutional Agreements <AddOrEdit> form
Examples:
 | AddOrEdit | LinkText              |
 | new       | Add a new agreement   |
 | edit      | Agreement, UC 01 test |

 @InstAgrFormsR0404
 Scenario Outline: Institutional Agreement forms successfully closes the bubble pop up for the Help link by clicking the help link
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a "Help" link
    And   I have clicked the "Help" link
    And   I have seen a bubble pop up in the Institutional Agreements <AddOrEdit> form
    When  I click the "Help" link
    Then  I should not see a bubble pop up in the Institutional Agreements <AddOrEdit> form
Examples:
 | AddOrEdit | LinkText              |
 | new       | Add a new agreement   |
 | edit      | Agreement, UC 01 test |