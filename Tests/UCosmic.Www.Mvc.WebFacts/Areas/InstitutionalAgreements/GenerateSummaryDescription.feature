Feature: Generate Summary Description
    In order to identity agreements in a list
    As an Institutional Agreement Manager
    I want to automatically generate a summary description using Participants, Agreement Type, and Current Status

Background:

    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Generate summary description based on participants, type, and status when adding an agreement

   When I click the "Add a new agreement" link
   Then I should see the Institutional Agreement Add page

   When I type "<Term>" into the Participant Search text field
   Then I should<OrNot> see an autocomplete dropdown menu item "<Participant>" for the Participant Search combo field
   And I should<OrNot> click the autocomplete dropdown menu item "<Participant>" for the Participant Search combo field
   And I should<OrNot> see an item for "<Participant>" in the Participants list

   When I type "<AgreementType>" into the Agreement Type text field
   And I type "<Status>" into the Current Status text field
   And I check the Automatically Generate Summary Description check box
   Then I should see "<GenerationExpected>" in the Summary Description text field

Examples:
    | OrNot | Term | Participant              | AgreementType               | Status | GenerationExpected                                                                                           |
    | n't   |      |                          |                             |        | Institutional Agreement between University of Cincinnati and...                                              |
    | n't   |      |                          | Activity Agreement          |        | Activity Agreement between University of Cincinnati and...                                                   |
    | n't   |      |                          |                             | Dead   | Institutional Agreement between University of Cincinnati and... - Status is Dead                             |
    | n't   |      |                          | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and... - Status is Active                       |
    |       | alf  | Alfred University (SUNY) | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and Alfred University (SUNY) - Status is Active |

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Generate summary description based on participants, type, and status when editing an agreement

   When I click the "Agreement, UC 02 test" link
   Then I should see the Institutional Agreement Edit page

   When I type "<Term>" into the Participant Search text field
   Then I should<OrNot> see an autocomplete dropdown menu item "<Participant>" for the Participant Search combo field
   And I should<OrNot> click the autocomplete dropdown menu item "<Participant>" for the Participant Search combo field
   And I should<OrNot> see an item for "<Participant>" in the Participants list

   When I type "<AgreementType>" into the Agreement Type text field
   And I type "<Status>" into the Current Status text field
   And I check the Automatically Generate Summary Description check box
   Then I should see "<GenerationExpected>" in the Summary Description text field

Examples:
    | OrNot | Term | Participant              | AgreementType               | Status | GenerationExpected                                                                                           |
    | n't   |      |                          |                             |        | Institutional Agreement between University of Cincinnati and...                                              |
    | n't   |      |                          | Activity Agreement          |        | Activity Agreement between University of Cincinnati and...                                                   |
    | n't   |      |                          |                             | Dead   | Institutional Agreement between University of Cincinnati and... - Status is Dead                             |
    | n't   |      |                          | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and... - Status is Active                       |
    |       | alf  | Alfred University (SUNY) | Memorandum of Understanding | Active | Memorandum of Understanding between University of Cincinnati and Alfred University (SUNY) - Status is Active |
