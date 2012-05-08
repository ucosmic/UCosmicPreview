@InstAgrForms
@InstAgrFormsR04
Feature: Institutional Agreement Management Preview Revision 4
    In order to inform the people about the use of an expiration date field for my Institutional Agreements
    As an Institutional Agreement Manager
    I want to help the people enter expiration date information attached to my Institutional Agreements in UCosmic

#execute these steps before every scenario in this file
Background: 
    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

@InstAgrFormsR0401
Scenario Outline: Display Help link for Expiration Date field

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a "Help" link

 Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |

 @InstAgrFormsR0402
 Scenario Outline: Display help bubble dialog for Expiration Date field by clicking Help link

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a "Help" link

    When I click the "Help" link
    Then I should see a help bubble dialog
    And I should see a "Close this popup" link

Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |
 
 @InstAgrFormsR0403
 Scenario Outline: Close help bubble dialog by clicking the Close This Popup link

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a "Help" link

    When I click the "Help" link
    Then I should see a help bubble dialog
    And I should see a "Close this popup" link

    When  I click the "Close this popup" link
    Then I should not see a help bubble dialog

Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |

 @InstAgrFormsR0404
 Scenario Outline: Close help bubble dialog by clicking the Help link a second time

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a "Help" link

    When I click the "Help" link
    Then I should see a help bubble dialog
    And I should see a "Close this popup" link

    When  I click the "Help" link
    Then I should not see a help bubble dialog

Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |