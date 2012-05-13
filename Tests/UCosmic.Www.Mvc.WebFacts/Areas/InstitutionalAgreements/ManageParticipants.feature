Feature: Manage Participants
    In order to keep track of bilateral and multilateral agreements
    As an Institutional Agreement Manager
    I want to manage a list of Participants for each agreement

Background:

    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

Scenario Outline: Add Participant to list

    When I click the "<LinkToForm>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I type "<ParticipantTerm>" into the Participant Search text field
    Then I should see an autocomplete dropdown menu item "<ParticipantTarget>" for the Participant Search combo field

    When I click the autocomplete dropdown menu item "<ParticipantTarget>" for the Participant Search combo field
    Then I should see an item for "<ParticipantTarget>" in the Participants list

Examples:
    | AddOrEdit | LinkToForm            | ParticipantTerm | ParticipantTarget           |
    | Add       | Add a new agreement   | alf             | Alfred University (SUNY)    |
    | Add       | Add a new agreement   | beij            | Beijing Jiaotong University |
    | Edit      | Agreement, UC 01 test | alf             | Alfred University (SUNY)    |
    | Edit      | Agreement, UC 01 test | beij            | Beijing Jiaotong University |
