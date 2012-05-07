@InstAgrForms
@InstAgrFormsR01
Feature:  Institutional Agreement Management Preview Revision 1
    In order to add, view, and edit my Institutional Agreements
    As an Institutional Agreement Manager
    I want to manage my Institutional Agreement data in UCosmic

Background:
    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

@InstAgrFormsR0101
Scenario Outline: Fail to submit Institutional Agreement form because required fields are empty

    When I click the "<LinkToForm>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I type "<Value1>" into the Agreement Type field
    And I type "<Value2>" into the Summary Description field
    And I type "<Value3>" into the Start Date field
    And I type "<Value4>" into the Expiration Date field
    And I type "<Value5>" into the Current Status field
    And I click the submit button

    Then I should still see the Institutional Agreement <AddOrEdit> page
    And I should <OrNot1> see the Required error message for the Agreement Type field
    And I should <OrNot2> see the Required error message for the Summary Description field
    And I should <OrNot3> see the Required error message for the Start Date field
    And I should <OrNot4> see the Required error message for the Expiration Date field
    And I should <OrNot5> see the Required error message for the Current Status field
    And I should <OrNot1> see the Agreement Type field's Required error message included in 2 error summaries
    And I should <OrNot2> see the Summary Description field's Required error message included in 2 error summaries
    And I should <OrNot3> see the Start Date field's Required error message included in 2 error summaries
    And I should <OrNot4> see the Expiration Date field's Required error message included in 2 error summaries
    And I should <OrNot5> see the Current Status field's Required error message included in 2 error summaries

Examples:
    | AddOrEdit | LinkToForm            | SubmitLabel   | Value1 | Value2 | Value3 | Value4 | Value5 | OrNot1 | OrNot2 | OrNot3 | OrNot4 | OrNot5 |
    | Add       | Add a new agreement   | Add Agreement |        |        |        |        |        |        |        |        |        |        |
    | Add       | Add a new agreement   | Add Agreement | Test   |        |        |        |        | not    |        |        |        |        |
    | Add       | Add a new agreement   | Add Agreement |        | Test   |        |        |        |        | not    |        |        |        |
    | Add       | Add a new agreement   | Add Agreement |        |        | 8/6/11 |        |        |        |        | not    |        |        |
    | Add       | Add a new agreement   | Add Agreement |        |        |        | 8/5/15 |        |        |        |        | not    |        |
    | Add       | Add a new agreement   | Add Agreement |        |        |        |        | Test   |        |        |        |        | not    |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        |        |        |        |        |        |        |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  | Test   |        |        |        |        | not    |        |        |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        | Test   |        |        |        |        | not    |        |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        | 8/6/11 |        |        |        |        | not    |        |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        |        | 8/5/15 |        |        |        |        | not    |        |
    | Edit      | Agreement, UC 01 test | Save Changes  |        |        |        |        | Test   |        |        |        |        | not    |

@InstAgrFormsR0102 @InstAgrFormsFreshTestAgreementUcGc @InstAgrFormsFreshTestAgreementUcFf @InstAgrFormsFreshTestAgreementUcIe
Scenario Outline: Successfully submit Institutional Agreement Edit form after editing various fields

    #TODO: this is a very poor test and should be refactored
    Given I am using the <BrowserName> browser
    When I click the "<AgreementTitle>" link
    Then I should see the Institutional Agreement Edit page

    When I do<OrNot1> type "<Value1>" into the Agreement Type field
    And I do<OrNot2> type "<Value2>" into the Summary Description field
    And I do<OrNot3> type "<Value3>" into the Start Date field
    And I do<OrNot4> type "<Value4>" into the Expiration Date field
    And I do<OrNot5> type "<Value5>" into the Current Status field
    And I click the submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see the flash feedback message "<Flash>"

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should<OrNot1> see "<Value1>" in the Agreement Type field
    And I should<OrNot2> see "<Value2>" in the Summary Description field
    And I should<OrNot3> see "<Value3>" in the Start Date field
    And I should<OrNot4> see "<Value4>" in the Expiration Date field
    And I should<OrNot5> see "<Value5>" in the Current Status field

    # need to reset the form to previous state for next scenario example
	When I type "<AgreementTitle>" into the Summary Description field
    And I click the submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

Examples:
| BrowserName       | AgreementTitle        | OrNot1 | Value1 | OrNot2 | Value2                       | OrNot3 | Value3   | OrNot4 | Value4   | OrNot5 | Value5 | Flash                                           |
| Chrome            | Agreement, UC GC test | n't    |        | n't    |                              | n't    |          | n't    |          | n't    |        | No changes were saved.                          |
| Chrome            | Agreement, UC GC test |        | Test   | n't    |                              | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | n't    |        |        | Agreement, UC GC test edited | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | n't    |        | n't    |                              |        | 8/7/1976 | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | n't    |        | n't    |                              | n't    |          |        | 8/6/2056 | n't    |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | n't    |        | n't    |                              | n't    |          | n't    |          |        | Test   | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | n't    |        | n't    |                              | n't    |          | n't    |          | n't    |        | No changes were saved.                          |
| Firefox           | Agreement, UC FF test |        | Test   | n't    |                              | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | n't    |        |        | Agreement, UC FF test edited | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | n't    |        | n't    |                              |        | 8/7/1976 | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | n't    |        | n't    |                              | n't    |          |        | 8/6/2056 | n't    |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | n't    |        | n't    |                              | n't    |          | n't    |          |        | Test   | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | n't    |        | n't    |                              | n't    |          | n't    |          | n't    |        | No changes were saved.                          |
| Internet Explorer | Agreement, UC IE test |        | Test   | n't    |                              | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | n't    |        |        | Agreement, UC IE test edited | n't    |          | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | n't    |        | n't    |                              |        | 8/7/1976 | n't    |          | n't    |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | n't    |        | n't    |                              | n't    |          |        | 8/6/2056 | n't    |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | n't    |        | n't    |                              | n't    |          | n't    |          |        | Test   | Institutional agreement was saved successfully. |

@InstAgrFormsR0103
Scenario Outline: Add Institutional Agreement Participant to the list

    When I click the "<LinkToForm>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I type "<ParticipantTerm>" into the Participant Search field
    Then I should see an autocomplete dropdown menu item "<ParticipantTarget>" for the Participant Search field

    When I click the autocomplete dropdown menu item "<ParticipantTarget>" for the Participant Search field
    Then I should see an item for "<ParticipantTarget>" in the Participants list

Examples:
    | AddOrEdit | LinkToForm            | ParticipantTerm | ParticipantTarget           |
    | Add       | Add a new agreement   | alf             | Alfred University (SUNY)    |
    | Add       | Add a new agreement   | beij            | Beijing Jiaotong University |
    | Edit      | Agreement, UC 01 test | alf             | Alfred University (SUNY)    |
    | Edit      | Agreement, UC 01 test | beij            | Beijing Jiaotong University |

@InstAgrFormsR0104
Scenario Outline: Generate summary description based on participants, type, and status when adding an agreement

   When I click the "Add a new agreement" link
   Then I should see the Institutional Agreement Add page

   When I type "<Term>" into the Participant Search field
   Then I should <OrNot> see an autocomplete dropdown menu item "<Participant>" for the Participant Search field
   And I should <OrNot> click the autocomplete dropdown menu item "<Participant>" for the Participant Search field
   And I should <OrNot> see an item for "<Participant>" in the Participants list

   When I type "<AgreementType>" into the Agreement Type field
   And I type "<Status>" into the Current Status field
   And I check the Automatically Generate Summary Description checkbox
   Then I should see "<GenerationExpected>" in the Summary Description field

Examples:
    | OrNot | Term | Participant              | AgreementType               | Status | GenerationExpected                                                                                           |
    | not   |      |                          |                             |        | Institutional Agreement between University of Cincinnati and...                                              |
    | not   |      |                          | Activity Agreement          |        | Activity Agreement between University of Cincinnati and...                                                   |
    | not   |      |                          |                             | Dead   | Institutional Agreement between University of Cincinnati and... - Status is Dead                             |
    | not   |      |                          | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and... - Status is Active                       |
    |       | alf  | Alfred University (SUNY) | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and Alfred University (SUNY) - Status is Active |

@InstAgrFormsR0105 @InstAgrFormsFreshTestAgreementUc02
Scenario Outline: Generate summary description based on participants, type, and status when editing an agreement

   When I click the "Agreement, UC 02 test" link
   Then I should see the Institutional Agreement Edit page

   When I type "<Term>" into the Participant Search field
   Then I should <OrNot> see an autocomplete dropdown menu item "<Participant>" for the Participant Search field
   And I should <OrNot> click the autocomplete dropdown menu item "<Participant>" for the Participant Search field
   And I should <OrNot> see an item for "<Participant>" in the Participants list

   When I type "<AgreementType>" into the Agreement Type field
   And I type "<Status>" into the Current Status field
   And I check the Automatically Generate Summary Description checkbox
   Then I should see "<GenerationExpected>" in the Summary Description field

Examples:
    | OrNot | Term | Participant              | AgreementType               | Status | GenerationExpected                                                                                           |
    | not   |      |                          |                             |        | Institutional Agreement between University of Cincinnati and...                                              |
    | not   |      |                          | Activity Agreement          |        | Activity Agreement between University of Cincinnati and...                                                   |
    | not   |      |                          |                             | Dead   | Institutional Agreement between University of Cincinnati and... - Status is Dead                             |
    | not   |      |                          | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and... - Status is Active                       |
    |       | alf  | Alfred University (SUNY) | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and Alfred University (SUNY) - Status is Active |

@InstAgrFormsR0106
Scenario Outline: Display example values by typing matching text into Agreement Type and Current Status fields

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I type "<InputValue>" into the <FieldName> field
    Then I should <OrNot> see the autocomplete dropdown menu item "<ListValue>" for the <FieldName> field

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
   
@InstAgrFormsR0107
Scenario Outline: Display example values when clicking Agreement Type and Current Status field dropdown arrows

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the autocomplete dropdown arrow button for the <FieldName> field
    Then I should see the autocomplete dropdown menu item "<ListValue>" for the <FieldName> field

Examples:
    | AddOrEdit | LinkText              | FieldName      | ListValue                             |
    | Add       | Add a new agreement   | Agreement Type | Activity Agreement                    |
    | Add       | Add a new agreement   | Agreement Type | Memorandum of Understanding           |
    | Add       | Add a new agreement   | Current Status | Active                                |
    | Add       | Add a new agreement   | Current Status | Dead                                  |
    | Add       | Add a new agreement   | Current Status | Inactive                              |
    | Add       | Add a new agreement   | Current Status | Unknown                               |
    | Edit      | Agreement, UC 01 test | Agreement Type | Institutional Collaboration Agreement |

@InstAgrFormsR0108
Scenario Outline: Display and select example values by clicking Agreement Type and Current Status dropdown arrows and menu items

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the autocomplete dropdown arrow button for the <FieldName> field
    Then I should see the autocomplete dropdown menu item "<ListValue>" for the <FieldName> field

    When I click the autocomplete dropdown menu item "<ListValue>" for the <FieldName> field
    Then I should see "<InputValue>" in the <FieldName> field

Examples:
    | AddOrEdit | LinkText              | FieldName      | ListValue                             | InputValue                            |
    | Add       | Add a new agreement   | Agreement Type | Institutional Collaboration Agreement | Institutional Collaboration Agreement |
    | Add       | Add a new agreement   | Agreement Type | Memorandum of Understanding           | Memorandum of Understanding           |
    | Add       | Add a new agreement   | Current Status | Active                                | Active                                |
    | Add       | Add a new agreement   | Current Status | Dead                                  | Dead                                  |
    | Add       | Add a new agreement   | Current Status | Inactive                              | Inactive                              |
    | Edit      | Agreement, UC 01 test | Agreement Type | Activity Agreement                    | Activity Agreement                    |
    | Edit      | Agreement, UC 01 test | Current Status | Unknown                               | Unknown                               |
