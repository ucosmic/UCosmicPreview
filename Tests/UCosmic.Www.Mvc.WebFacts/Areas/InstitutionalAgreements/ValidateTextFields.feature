Feature: Validate Text Fields
    In order to be sure that data is complete
    As an Institutional Agreement Manager
    I want Agreement Type, Summary Description, Start Date, Expiration Date, and Current Status text fields to be required

Background:

    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

Scenario Outline: Form submit fails when required text fields are empty

    When I click the "<LinkToForm>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I type "<Value1>" into the Agreement Type text field
    And I type "<Value2>" into the Summary Description text field
    And I type "<Value3>" into the Start Date text field
    And I type "<Value4>" into the Expiration Date text field
    And I type "<Value5>" into the Current Status text field
    And I click the submit button

    Then I should still see the Institutional Agreement <AddOrEdit> page
    And I should<OrNot1> see the Required error message for the Agreement Type text field
    And I should<OrNot2> see the Required error message for the Summary Description text field
    And I should<OrNot3> see the Required error message for the Start Date text field
    And I should<OrNot4> see the Required error message for the Expiration Date text field
    And I should<OrNot5> see the Required error message for the Current Status text field
    And I should<OrNot1> see the Agreement Type text field's Required error message included in 2 error summaries
    And I should<OrNot2> see the Summary Description text field's Required error message included in 2 error summaries
    And I should<OrNot3> see the Start Date text field's Required error message included in 2 error summaries
    And I should<OrNot4> see the Expiration Date text field's Required error message included in 2 error summaries
    And I should<OrNot5> see the Current Status text field's Required error message included in 2 error summaries

Examples:
    | AddOrEdit | LinkToForm            | SubmitLabel   | Value1 | Value2 | Value3 | Value4 | Value5 | OrNot1 | OrNot2 | OrNot3 | OrNot4 | OrNot5 |
    | Add       | Add a new agreement   | Add Agreement |        |        |        |        |        |        |        |        |        |        |
    | Add       | Add a new agreement   | Add Agreement | Test   |        |        |        |        | n't    |        |        |        |        |
    | Add       | Add a new agreement   | Add Agreement |        | Test   |        |        |        |        | n't    |        |        |        |
    | Add       | Add a new agreement   | Add Agreement |        |        | 8/6/11 |        |        |        |        | n't    |        |        |
    | Add       | Add a new agreement   | Add Agreement |        |        |        | 8/5/15 |        |        |        |        | n't    |        |
    | Add       | Add a new agreement   | Add Agreement |        |        |        |        | Test   |        |        |        |        | n't    |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        |        |        |        |        |        |        |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  | Test   |        |        |        |        | n't    |        |        |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        | Test   |        |        |        |        | n't    |        |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        | 8/6/11 |        |        |        |        | n't    |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        |        | 8/5/15 |        |        |        |        | n't    |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        |        |        | Test   |        |        |        |        | n't    |

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Edit form displays new text field values after saving changes

    #TODO: this is a poor test and should be refactored
    Given I am using the <Browser> browser
    When I click the "<AgreementTitle>" link
    Then I should see the Institutional Agreement Edit page

    When I do<OrNot1> type "<Value1>" into the Agreement Type text field
    And I do<OrNot2> type "<Value2>" into the Summary Description text field
    And I do<OrNot3> type "<Value3>" into the Start Date text field
    And I do<OrNot4> type "<Value4>" into the Expiration Date text field
    And I do<OrNot5> type "<Value5>" into the Current Status text field
    And I click the submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see the flash feedback message "<Flash>"

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should<OrNot1> see "<Value1>" in the Agreement Type text field
    And I should<OrNot2> see "<Value2>" in the Summary Description text field
    And I should<OrNot3> see "<Value3>" in the Start Date text field
    And I should<OrNot4> see "<Value4>" in the Expiration Date text field
    And I should<OrNot5> see "<Value5>" in the Current Status text field

    ## need to reset the form to previous state for next scenario example
	#When I type "<AgreementTitle>" into the Summary Description text field
    #And I click the submit button
    #Then I should see the Public Institutional Agreement Detail page
    #And I should see an "Edit this agreement" link

Examples:
| Browser | AgreementTitle        | OrNot1 | Value1 | OrNot2 | Value2                       | OrNot3 | Value3   | OrNot4 | Value4   | OrNot5 | Value5 | Flash                                           |
| Chrome  | Agreement, UC GC test | n't    |        | n't    |                              | n't    |          | n't    |          | n't    |        | No changes were saved.                          |
| Chrome  | Agreement, UC GC test |        | Test   | n't    |                              | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Chrome  | Agreement, UC GC test | n't    |        |        | Agreement, UC GC test edited | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Chrome  | Agreement, UC GC test | n't    |        | n't    |                              |        | 8/7/1976 | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Chrome  | Agreement, UC GC test | n't    |        | n't    |                              | n't    |          |        | 8/6/2056 | n't    |        | Institutional agreement was saved successfully. |
| Chrome  | Agreement, UC GC test | n't    |        | n't    |                              | n't    |          | n't    |          |        | Test   | Institutional agreement was saved successfully. |
| Firefox | Agreement, UC FF test | n't    |        | n't    |                              | n't    |          | n't    |          | n't    |        | No changes were saved.                          |
| Firefox | Agreement, UC FF test |        | Test   | n't    |                              | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Firefox | Agreement, UC FF test | n't    |        |        | Agreement, UC FF test edited | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Firefox | Agreement, UC FF test | n't    |        | n't    |                              |        | 8/7/1976 | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Firefox | Agreement, UC FF test | n't    |        | n't    |                              | n't    |          |        | 8/6/2056 | n't    |        | Institutional agreement was saved successfully. |
| Firefox | Agreement, UC FF test | n't    |        | n't    |                              | n't    |          | n't    |          |        | Test   | Institutional agreement was saved successfully. |
| MSIE    | Agreement, UC IE test | n't    |        | n't    |                              | n't    |          | n't    |          | n't    |        | No changes were saved.                          |
| MSIE    | Agreement, UC IE test |        | Test   | n't    |                              | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| MSIE    | Agreement, UC IE test | n't    |        |        | Agreement, UC IE test edited | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| MSIE    | Agreement, UC IE test | n't    |        | n't    |                              |        | 8/7/1976 | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| MSIE    | Agreement, UC IE test | n't    |        | n't    |                              | n't    |          |        | 8/6/2056 | n't    |        | Institutional agreement was saved successfully. |
| MSIE    | Agreement, UC IE test | n't    |        | n't    |                              | n't    |          | n't    |          |        | Test   | Institutional agreement was saved successfully. |
