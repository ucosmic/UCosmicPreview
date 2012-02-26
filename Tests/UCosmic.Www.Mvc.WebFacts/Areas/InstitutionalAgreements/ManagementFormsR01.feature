@InstAgrForms
@InstAgrFormsR01
Feature:  Institutional Agreement Management Preview Revision 1
		  In order to add, view, and edit my Institutional Agreements
		  As an Institutional Agreement Manager
		  I want to manage my Institutional Agreement data in UCosmic

@InstAgrFormsR0101
Scenario Outline: Institutional Agreement forms unsuccessfully submit with empty required fields
  	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
    And   I have browsed to the "my/institutional-agreements/v1" url
	And   I have clicked the "<LinkToForm>" link
	And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	When  I type "<Value1>" into the "Agreement type" text box on the Institutional Agreement <AddOrEdit> form
	And   I type "<Value2>" into the "Summary description" text box on the Institutional Agreement <AddOrEdit> form
	And   I type "<Value3>" into the "Start date" text box on the Institutional Agreement <AddOrEdit> form
	And   I type "<Value4>" into the "Expiration date" text box on the Institutional Agreement <AddOrEdit> form
	And   I type "<Value5>" into the "Current status" text box on the Institutional Agreement <AddOrEdit> form
	And   I click the "<SubmitLabel>" submit button on the Institutional Agreement <AddOrEdit> form
    Then  I should see a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I should <SeeMsg1> the error message "Agreement type is required." for the "Agreement type" text box on the Institutional Agreement add form
    And   I should <SeeMsg2> the error message "Summary description is required." for the "Summary description" text box on the Institutional Agreement add form
    And   I should <SeeMsg3> the error message "Start date is required." for the "Start date" text box on the Institutional Agreement add form
    And   I should <SeeMsg4> the error message "Expiration date is required." for the "Expiration date" text box on the Institutional Agreement add form
    And   I should <SeeMsg5> the error message "Current status is required." for the "Current status" text box on the Institutional Agreement add form
    And   I should <SeeMsg1> the message "Agreement type is required." included in 2 error summaries
    And   I should <SeeMsg2> the message "Summary description is required." included in 2 error summaries
    And   I should <SeeMsg3> the message "Start date is required." included in 2 error summaries
    And   I should <SeeMsg4> the message "Expiration date is required." included in 2 error summaries
    And   I should <SeeMsg5> the message "Current status is required." included in 2 error summaries
Examples: 
    | AddOrEdit | LinkToForm            | SubmitLabel   | Value1 | Value2 | Value3 | Value4 | Value5 | SeeMsg1 | SeeMsg2 | SeeMsg3 | SeeMsg4 | SeeMsg5 |
    | new       | Add a new agreement   | Add Agreement |        |        |        |        |        | see     | see     | see     | see     | see     |
    | new       | Add a new agreement   | Add Agreement | Test   |        |        |        |        | not see | see     | see     | see     | see     |
    | new       | Add a new agreement   | Add Agreement |        | Test   |        |        |        | see     | not see | see     | see     | see     |
    | new       | Add a new agreement   | Add Agreement |        |        | 8/6/11 |        |        | see     | see     | not see | see     | see     |
    | new       | Add a new agreement   | Add Agreement |        |        |        | 8/5/15 |        | see     | see     | see     | not see | see     |
    | new       | Add a new agreement   | Add Agreement |        |        |        |        | Test   | see     | see     | see     | see     | not see |
    | edit      | Agreement, UC 01 test | Save Changes  |        |        |        |        |        | see     | see     | see     | see     | see     |
    | edit      | Agreement, UC 01 test | Save Changes  | Test   |        |        |        |        | not see | see     | see     | see     | see     |
    | edit      | Agreement, UC 01 test | Save Changes  |        | Test   |        |        |        | see     | not see | see     | see     | see     |
    | edit      | Agreement, UC 01 test | Save Changes  |        |        | 8/6/11 |        |        | see     | see     | not see | see     | see     |
    | edit      | Agreement, UC 01 test | Save Changes  |        |        |        | 8/5/15 |        | see     | see     | see     | not see | see     |
    | edit      | Agreement, UC 01 test | Save Changes  |        |        |        |        | Test   | see     | see     | see     | see     | not see |

@InstAgrFormsR0102 @InstAgrFormsFreshTestAgreementUcGc @InstAgrFormsFreshTestAgreementUcFf @InstAgrFormsFreshTestAgreementUcIe
Scenario Outline: Institutional Agreement edit form successfully submit after editing various fields
    Given I am using the <BrowserName> browser
  	And   I have signed in as "manager1@uc.edu" with password "asdfasdf"
    And   I have browsed to the "my/institutional-agreements/v1" url
	And   I have clicked the "<AgreementTitle>" link
	And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
	And   I have <TypeOrNot1> "<Value1>" into the "Agreement type" text box on the Institutional Agreement edit form
	And   I have <TypeOrNot2> "<Value2>" into the "Summary description" text box on the Institutional Agreement edit form
	And   I have <TypeOrNot3> "<Value3>" into the "Start date" text box on the Institutional Agreement edit form
	And   I have <TypeOrNot4> "<Value4>" into the "Expiration date" text box on the Institutional Agreement edit form
	And   I have <TypeOrNot5> "<Value5>" into the "Current status" text box on the Institutional Agreement edit form
    And   I have successfully submitted the Institutional Agreement edit form
	And   I have seen a page at the "institutional-agreements/[PathVar]" url
	And   I have seen top feedback message "<Flash>"
    When  I click the "Edit this agreement" link
    And   I see a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    Then  I should (or shouldn't) see "<Value1>" in the "Agreement type" text box because I <TypeOrNot1> it during the last Institutional Agreement form save
	And   I should (or shouldn't) see "<Value2>" in the "Summary description" text box because I <TypeOrNot2> it during the last Institutional Agreement form save
	And   I should (or shouldn't) see "<Value3>" in the "Start date" text box because I <TypeOrNot3> it during the last Institutional Agreement form save
	And   I should (or shouldn't) see "<Value4>" in the "Expiration date" text box because I <TypeOrNot4> it during the last Institutional Agreement form save
	And   I should (or shouldn't) see "<Value5>" in the "Current status" text box because I <TypeOrNot5> it during the last Institutional Agreement form save
	And   I should type "<AgreementTitle>" into the "Summary description" text box on the Institutional Agreement edit form
    And   I should successfully submit the Institutional Agreement edit form
    And   I should see a page at the "institutional-agreements/[PathVar]" url
    And   I should see a "Edit this agreement" link
Examples: 
| BrowserName       | AgreementTitle        | TypeOrNot1  | Value1 | TypeOrNot2  | Value2                       | TypeOrNot3  | Value3   | TypeOrNot4  | Value4   | TypeOrNot5  | Value5 | Flash                                           |
| Chrome            | Agreement, UC GC test | didn't type |        | didn't type |                              | didn't type |          | didn't type |          | didn't type |        | No changes were saved.                          |
| Chrome            | Agreement, UC GC test | typed       | Test   | didn't type |                              | didn't type |          | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | didn't type |        | typed       | Agreement, UC GC test edited | didn't type |          | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | didn't type |        | didn't type |                              | typed       | 8/7/1976 | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | didn't type |        | didn't type |                              | didn't type |          | typed       | 8/6/2056 | didn't type |        | Institutional agreement was saved successfully. |
| Chrome            | Agreement, UC GC test | didn't type |        | didn't type |                              | didn't type |          | didn't type |          | typed       | Test   | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | didn't type |        | didn't type |                              | didn't type |          | didn't type |          | didn't type |        | No changes were saved.                          |
| Firefox           | Agreement, UC FF test | typed       | Test   | didn't type |                              | didn't type |          | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | didn't type |        | typed       | Agreement, UC FF test edited | didn't type |          | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | didn't type |        | didn't type |                              | typed       | 8/7/1976 | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | didn't type |        | didn't type |                              | didn't type |          | typed       | 8/6/2056 | didn't type |        | Institutional agreement was saved successfully. |
| Firefox           | Agreement, UC FF test | didn't type |        | didn't type |                              | didn't type |          | didn't type |          | typed       | Test   | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | didn't type |        | didn't type |                              | didn't type |          | didn't type |          | didn't type |        | No changes were saved.                          |
| Internet Explorer | Agreement, UC IE test | typed       | Test   | didn't type |                              | didn't type |          | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | didn't type |        | typed       | Agreement, UC IE test edited | didn't type |          | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | didn't type |        | didn't type |                              | typed       | 8/7/1976 | didn't type |          | didn't type |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | didn't type |        | didn't type |                              | didn't type |          | typed       | 8/6/2056 | didn't type |        | Institutional agreement was saved successfully. |
| Internet Explorer | Agreement, UC IE test | didn't type |        | didn't type |                              | didn't type |          | didn't type |          | typed       | Test   | Institutional agreement was saved successfully. |


@InstAgrFormsR0103
Scenario Outline: Institutional Agreement forms successfully add participant to list box
   Given  I have signed in as "manager1@uc.edu" with password "asdfasdf"
   And    I have browsed to the "my/institutional-agreements/v1" url
   And    I have clicked the "<LinkToForm>" link
   And    I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
   When   I type "<ParticipantTerm>" into the "Participant search" text box on the Institutional Agreement <AddOrEdit> form
   And    I see a "ParticipantSearch" autocomplete dropdown menu item "<ParticipantTarget>"
   And    I click the "ParticipantSearch" autocomplete dropdown menu item "<ParticipantTarget>"
   Then   I should see "<ParticipantTarget>" in the Participants list box on the Institutional Agreement <AddOrEdit> form
Examples: 
    | AddOrEdit | LinkToForm            | ParticipantTerm | ParticipantTarget           |
    | new       | Add a new agreement   | alf             | Alfred University (SUNY)    |
    | new       | Add a new agreement   | beij            | Beijing Jiaotong University |
    | edit      | Agreement, UC 01 test | alf             | Alfred University (SUNY)    |
    | edit      | Agreement, UC 01 test | beij            | Beijing Jiaotong University |

@InstAgrFormsR0104
Scenario Outline: Institutional Agreement add form successfully generate summary description based on participants, type, and status
   Given  I have signed in as "manager1@uc.edu" with password "asdfasdf"
   And    I have browsed to the "my/institutional-agreements/v1" url
   And    I have clicked the "Add a new agreement" link
   And    I have seen a page at the "my/institutional-agreements/v1/new" url
   And    I have typed "<Term>" into the "Participant search" text box on the Institutional Agreement add form
   And    I have <Neg> seen a "ParticipantSearch" autocomplete dropdown menu item "<Participant>"
   And    I have <Neg> clicked the "ParticipantSearch" autocomplete dropdown menu item "<Participant>"
   And    I have <Neg> seen "<Participant>" in the Participants list box on the Institutional Agreement add form
   And    I have typed "<AgreementType>" into the "Agreement type" text box on the Institutional Agreement add form
   And    I have typed "<Status>" into the "Current status" text box on the Institutional Agreement add form
   When   I check the "Summary description" automatic generation checkbox on the Institutional Agreement add form
   Then   I should see the Summary description change to "<GenerationExpected>" on the Institutional Agreement add form
Examples: 
    | Neg | Term | Participant              | AgreementType               | Status | GenerationExpected                                                                                           |
    | not |      |                          |                             |        | Institutional Agreement between University of Cincinnati and...                                              |
    | not |      |                          | Activity Agreement          |        | Activity Agreement between University of Cincinnati and...                                                   |
    | not |      |                          |                             | Dead   | Institutional Agreement between University of Cincinnati and... - Status is Dead                             |
    | not |      |                          | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and... - Status is Active                       |
    |     | alf  | Alfred University (SUNY) | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and Alfred University (SUNY) - Status is Active |

@InstAgrFormsR0105 @InstAgrFormsFreshTestAgreementUc02
Scenario Outline: Institutional Agreement edit form successfully generate summary description based on participants, type, and status
   Given  I have signed in as "manager1@uc.edu" with password "asdfasdf"
   And    I have browsed to the "my/institutional-agreements/v1" url
   And    I have clicked the "Agreement, UC 02 test" link
   And    I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
   And    I have typed "<Term>" into the "Participant search" text box on the Institutional Agreement edit form
   And    I have <Neg> seen a "ParticipantSearch" autocomplete dropdown menu item "<Participant>"
   And    I have <Neg> clicked the "ParticipantSearch" autocomplete dropdown menu item "<Participant>"
   And    I have <Neg> seen "<Participant>" in the Participants list box on the Institutional Agreement edit form
   And    I have typed "<AgreementType>" into the "Agreement type" text box on the Institutional Agreement edit form
   And    I have typed "<Status>" into the "Current status" text box on the Institutional Agreement edit form
   When   I check the "Summary description" automatic generation checkbox on the Institutional Agreement edit form
   Then   I should see the Summary description change to "<GenerationExpected>" on the Institutional Agreement edit form
Examples: 
    | Neg | Term | Participant              | AgreementType               | Status | GenerationExpected                                                                                           |
    | not |      |                          |                             |        | Institutional Agreement between University of Cincinnati and...                                              |
    | not |      |                          | Activity Agreement          |        | Activity Agreement between University of Cincinnati and...                                                   |
    | not |      |                          |                             | Dead   | Institutional Agreement between University of Cincinnati and... - Status is Dead                             |
    | not |      |                          | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and... - Status is Active                       |
    |     | alf  | Alfred University (SUNY) | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and Alfred University (SUNY) - Status is Active |

@InstAgrFormsR0106
Scenario Outline: Institutional Agreement forms successfully display autocomplete example after clicking the <FieldName> dropdown arrow
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "<LinkText>" link
	And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    When  I click the autocomplete dropdown arrow button for the "<FieldName>" text box
    Then  I should see a "<FieldName>" autocomplete dropdown menu item "<ListValue>"
    Examples: 
    | AddOrEdit | LinkText              | FieldName | ListValue                             |
    | new       | Add a new agreement   | Type      | Activity Agreement                    |
    | new       | Add a new agreement   | Type      | Memorandum of Understanding           |
    | new       | Add a new agreement   | Status    | Active                                |
    | new       | Add a new agreement   | Status    | Dead                                  |
    | new       | Add a new agreement   | Status    | Inactive                              |
    | new       | Add a new agreement   | Status    | Unknown                               |
    | edit      | Agreement, UC 01 test | Type      | Institutional Collaboration Agreement |

@InstAgrFormsR0107
Scenario Outline: Institutional Agreement forms successfully display autocomplete example after typing into the <FieldName> text box
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "<LinkText>" link
	And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    When  I type "<InputValue>" into the "<FieldName>" text box on the Institutional Agreement <AddOrEdit> form 
    Then  I should <SeeOrNot> a "<FieldName>" autocomplete dropdown menu item "<ListValue>"
    Examples: 
    | AddOrEdit | LinkText              | FieldName | InputValue | SeeOrNot | ListValue                             |
    | new       | Add a new agreement   | Type      | [          | not see  |                                       |
    | new       | Add a new agreement   | Type      | m          | see      | Memorandum of Understanding           |
    | new       | Add a new agreement   | Status    | a          | not see  | dead                                  |
    | new       | Add a new agreement   | Status    | d          | see      | Dead                                  |
    | new       | Add a new agreement   | Status    | i          | see      | Inactive                              |
    | new       | Add a new agreement   | Status    | u          | see      | Unknown                               |
    | edit      | Agreement, UC 01 test | Type      | a          | see      | Activity Agreement                    |
    | edit      | Agreement, UC 01 test | Type      | i          | see      | Institutional Collaboration Agreement |
   
@InstAgrFormsR0108
Scenario Outline: Institutional Agreement forms successfully choose autocomplete example from dropdown menu
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have clicked the autocomplete dropdown arrow button for the "<FieldName>" text box
    And   I have seen a "<FieldName>" autocomplete dropdown menu item "<ListValue>"
    When  I click the "<FieldName>" autocomplete dropdown menu item "<ListValue>"
    Then  I should see "<InputValue>" in the "<FieldName>" text box on the Institutional Agreements form
    Examples:
    | AddOrEdit | LinkText              | FieldName | ListValue                             | InputValue                            |
    | new       | Add a new agreement   | Type      | Institutional Collaboration Agreement | Institutional Collaboration Agreement |
    | new       | Add a new agreement   | Type      | Memorandum of Understanding           | Memorandum of Understanding           |
    | new       | Add a new agreement   | Status    | Active                                | Active                                |
    | new       | Add a new agreement   | Status    | Dead                                  | Dead                                  |
    | new       | Add a new agreement   | Status    | Inactive                              | Inactive                              |
    | edit      | Agreement, UC 01 test | Type      | Activity Agreement                    | Activity Agreement                    |
    | edit      | Agreement, UC 01 test | Status    | Unknown                               | Unknown                               |
