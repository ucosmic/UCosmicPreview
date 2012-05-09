Feature: Display and Select Example Values
    In order to more quicly enter common agreement data
    As an Institutional Agreement Manager
    I want to select values for Agreement Type and Current Status by typing matching text or clicking a down arrow button

Background:

    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

Scenario Outline: Display example values by typing matching text into Agreement Type and Current Status text fields

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I type "<InputValue>" into the <FieldName> text field
    Then I should <OrNot> see the autocomplete dropdown menu item "<ListValue>" for the <FieldName> text field

Examples:
    | AddOrEdit | LinkText              | FieldName      | InputValue | OrNot | ListValue                             |
    | Add       | Add a new agreement   | Agreement Type | [          | not   |                                       |
    | Add       | Add a new agreement   | Agreement Type | m          |       | Memorandum of Understanding           |
    | Add       | Add a new agreement   | Current Status | a          | not   | dead                                  |
    | Add       | Add a new agreement   | Current Status | d          |       | Dead                                  |
    | Add       | Add a new agreement   | Current Status | i          |       | Inactive                              |
    | Add       | Add a new agreement   | Current Status | u          |       | Unknown                               |
    | Edit      | Agreement, UC 01 test | Agreement Type | a          |       | Activity Agreement                    |
    | Edit      | Agreement, UC 01 test | Agreement Type | i          |       | Institutional Collaboration Agreement |
   
Scenario Outline: Display example values by clicking Agreement Type and Current Status text field dropdown arrow buttons

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the autocomplete dropdown arrow button for the <FieldName> text field
    Then I should see the autocomplete dropdown menu item "<ListValue>" for the <FieldName> text field

Examples:
    | AddOrEdit | LinkText              | FieldName      | ListValue                             |
    | Add       | Add a new agreement   | Agreement Type | Activity Agreement                    |
    | Add       | Add a new agreement   | Agreement Type | Memorandum of Understanding           |
    | Add       | Add a new agreement   | Current Status | Active                                |
    | Add       | Add a new agreement   | Current Status | Dead                                  |
    | Add       | Add a new agreement   | Current Status | Inactive                              |
    | Add       | Add a new agreement   | Current Status | Unknown                               |
    | Edit      | Agreement, UC 01 test | Agreement Type | Institutional Collaboration Agreement |

Scenario Outline: Select Agreement Type and Current Status values by clicking dropdown menu items

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the autocomplete dropdown arrow button for the <FieldName> text field
    Then I should see the autocomplete dropdown menu item "<ListValue>" for the <FieldName> text field

    When I click the autocomplete dropdown menu item "<ListValue>" for the <FieldName> text field
    Then I should see "<InputValue>" in the <FieldName> text field

Examples:
    | AddOrEdit | LinkText              | FieldName      | ListValue                             | InputValue                            |
    | Add       | Add a new agreement   | Agreement Type | Institutional Collaboration Agreement | Institutional Collaboration Agreement |
    | Add       | Add a new agreement   | Agreement Type | Memorandum of Understanding           | Memorandum of Understanding           |
    | Add       | Add a new agreement   | Current Status | Active                                | Active                                |
    | Add       | Add a new agreement   | Current Status | Dead                                  | Dead                                  |
    | Add       | Add a new agreement   | Current Status | Inactive                              | Inactive                              |
    | Edit      | Agreement, UC 01 test | Agreement Type | Activity Agreement                    | Activity Agreement                    |
    | Edit      | Agreement, UC 01 test | Current Status | Unknown                               | Unknown                               |
