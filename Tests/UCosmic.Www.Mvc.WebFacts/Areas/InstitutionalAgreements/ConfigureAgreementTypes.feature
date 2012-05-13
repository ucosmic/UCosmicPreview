Feature: Configure Institutional Agreements Module Agreement Types
    In order to control agreement type designation at my institution
    As an Institutional Agreement Manager
    I want to manage the drop down options and behavior of the agreement type field

Background:

    Given I am signed in as supervisor1@uc.edu
    And I am starting from the Institutional Agreement Management page
    Then I should see a "Configure module" link

    When I click the "Configure module" link
    Then I should see the Institutional Agreement Configure Module page
    And I should see a "Click here to set it up now" link

Scenario: Add item to Agreement Types list fails when text is empty

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Agreement Types list

    When I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an Agreement Type text field for item #1 in the Agreement Types list

    When I click the "Add" link for item #1 in the Agreement Types list again
    Then I should see the Required error message for the Agreement Type text field

    When I click the "Create Configuration" submit button
    Then I should still see the Institutional Agreement Set Up Module page
    And I should still see the Required error message for the Agreement Type text field

Scenario Outline: Add item to Agreement Types list fails when text is duplicate

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Agreement Types list

    When I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an Agreement Type text field for item #1 in the Agreement Types list

    When I type "<DuplicateValue>" into the Agreement Type text field for item #1 in the Agreement Types list
    And I click the "Add" link for item #1 in the Agreement Types list again
    Then I should still see the Institutional Agreement Set Up Module page
    And I should see the '<DuplicateValue> is a Duplicate' error message for the Agreement Type text field

Examples:
  | DuplicateValue                        |
  | Activity Agreement                    |
  | Institutional Collaboration Agreement |
  | Memorandum of Understanding           |

Scenario Outline: Add item to Agreement Types list causes automatic alphabetization

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Agreement Types list

    When I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an Agreement Type text field for item #1 in the Agreement Types list

    When I type "<NewItemText>" into the Agreement Type text field for item #1 in the Agreement Types list
    And I click the "Add" link for item #1 in the Agreement Types list again
    Then I should see an item for "<NewItemText>" in the Agreement Types list
    And I should see a "Remove" link for item #<NewItemNumber> in the Agreement Types list

    When I click the "Remove" link for item #<NewItemNumber> in the Agreement Types list
    Then I shouldn't see an item for "<NewItemText>" in the Agreement Types list

Examples:
    | NewItemText | NewItemNumber |
    | AAA         | 2             |
    | BBB         | 3             |
    | JJJ         | 4             |
    | NNN         | 5             |

Scenario Outline: Edit item in Agreement Types list fails when text is empty

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Edit" link for item #<ListItemNumber> in the Agreement Types list

    When I click the "Edit" link for item #<ListItemNumber> in the Agreement Types list
    Then I should see an Agreement Type text field for item #<ListItemNumber> in the Agreement Types list
    And I should see a "Save" link for item #<ListItemNumber> in the Agreement Types list

    When I type "" into the Agreement Type text field for item #<ListItemNumber> in the Agreement Types list
    And I click the "Save" link for item #<ListItemNumber> in the Agreement Types list
    Then I should see the Required error message for the Agreement Type text field

    When I click the "Create Configuration" submit button
    Then I should still see the Institutional Agreement Set Up Module page
    And I should still see the Required error message for the Agreement Type text field

Examples:
    | ListItemNumber |
    | 2              |
    | 3              |
    | 4              |

Scenario: Edit item in Agreement Types list fails when text is duplicate

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "Memorandum of Understanding" in the Agreement Types list
    And I should see an "Edit" link for item #4 in the Agreement Types list

    When I click the "Edit" link for item #4 in the Agreement Types list
    Then I should see an Agreement Type text field for item #4 in the Agreement Types list
    And I should see a "Save" link for item #4 in the Agreement Types list
    And I should see a "Cancel" link for item #4 in the Agreement Types list

    When I type "Activity Agreement" into the Agreement Type text field for item #4 in the Agreement Types list
    And I click the "Save" link for item #4 in the Agreement Types list
    Then I should see the 'Activity Agreement is a Duplicate' error message for the Agreement Type text field
    And I should still see a "Cancel" link for item #4 in the Agreement Types list

    When I click the "Cancel" link for item #4 in the Agreement Types list
    Then I should see an "Edit" link for item #4 in the Agreement Types list

Scenario Outline: Edit item in Agreement Types list replaces previous value

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "<OriginalText>" in the Agreement Types list
    And I should see an "Edit" link for item #<ListItemNumber> in the Agreement Types list

    When I click the "Edit" link for item #<ListItemNumber> in the Agreement Types list
    Then I should see an Agreement Type text field for item #<ListItemNumber> in the Agreement Types list
    And I should see a "Save" link for item #<ListItemNumber> in the Agreement Types list

    When I type "Test Value" into the Agreement Type text field for item #<ListItemNumber> in the Agreement Types list
    And  I click the "Save" link for item #<ListItemNumber> in the Agreement Types list
    Then I should see an item for "Test Value" in the Agreement Types list
    And I shouldn't see an item for "<OriginalText>" in the Agreement Types list

Examples:
    | ListItemNumber | OriginalText                          |
    | 2              | Activity Agreement                    |
    | 3              | Institutional Collaboration Agreement |
    | 4              | Memorandum of Understanding           |

Scenario: Edit item in Agreement Types list causes automatic alphabetization

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "Activity Agreement" in the Agreement Types list
    And I should see an "Edit" link for item #2 in the Agreement Types list

    When I click the "Edit" link for item #2 in the Agreement Types list
    Then I should see an Agreement Type text field for item #2 in the Agreement Types list
    And I should see a "Save" link for item #2 in the Agreement Types list
    And I should see a "Cancel" link for item #2 in the Agreement Types list

    When I type "XXX Last Agreement" into the Agreement Type text field for item #2 in the Agreement Types list
    And I click the "Save" link for item #2 in the Agreement Types list
    Then I should see an item for "XXX Last Agreement" in the Agreement Types list
    And I should not see an item for "Activity Agreement" in the Agreement Types list

    When I click the "Remove" link for item #4 in the Agreement Types list
    Then I shouldn't see an item for "XXX Last Agreement" in the Agreement Types list

Scenario Outline: Cancel item edit in Agreement Types list

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "<OriginalText>" in the Agreement Types list
    And I should see an "Edit" link for item #<ListItemNumber> in the Agreement Types list

    When I click the "Edit" link for item #<ListItemNumber> in the Agreement Types list
    Then I should see an Agreement Type text field for item #<ListItemNumber> in the Agreement Types list
    And I should see a "Cancel" link for item #<ListItemNumber> in the Agreement Types list

    When I type "Agreement" into the Agreement Type text field for item #<ListItemNumber> in the Agreement Types list
    And I click the "Cancel" link for item #<ListItemNumber> in the Agreement Types list
    Then I should still see an item for "<OriginalText>" in the Agreement Types list

Examples:
    | ListItemNumber | OriginalText                          |
    | 2              | Activity Agreement                    |
    | 3              | Institutional Collaboration Agreement |
    | 4              | Memorandum of Understanding           |

Scenario Outline: Remove item from Agreement Types list

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see a "Remove" link for item #<ListItemNumber> in the Agreement Types list

    When I click the "Remove" link for item #<ListItemNumber> in the Agreement Types list
    Then I shouldn't see an item for "<OriginalText>" in the Agreement Types list

Examples:
    | ListItemNumber | OriginalText                          |
    | 2              | Activity Agreement                    |
    | 3              | Institutional Collaboration Agreement |
    | 4              | Memorandum of Understanding           |

Scenario: Changing Agreement Type Behavior makes Example combo box read only or editable

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an Agreement Type Behavior radio button labeled 'Allow any text'
    And I should see an Agreement Type Behavior radio button labeled 'Use specific values'
    And the Agreement Type Behavior radio button labeled 'Allow any text' should be checked
    And I should see an Agreement Type Example text field

    When I check the Agreement Type Behavior radio button labeled 'Use specific values'
    Then the Agreement Type Example text field should be read only

    When I check the Agreement Type Behavior radio button labeled 'Allow any text'
    Then the Agreement Type Example text field should not be read only
