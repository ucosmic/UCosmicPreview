Feature: Configure Institutional Agreements Module Current Statuses
    In order to control current status designation at my institution
    As an Institutional Agreement Manager
    I want to manage the drop down options and behavior of the current status field

Background:

    Given I am signed in as supervisor1@uc.edu
    And I am starting from the Institutional Agreement Management page
    Then I should see a "Configure module" link

    When I click the "Configure module" link
    Then I should see the Institutional Agreement Configure Module page
    And I should see a "Click here to set it up now" link

Scenario: Add item to Current Statuses list fails when text is empty

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Current Statuses list

    When I click the "Add" link for item #1 in the Current Statuses list
    Then I should see a Current Status text field for item #1 in the Current Statuses list

    When I click the "Add" link for item #1 in the Current Statuses list again
    Then I should see the Required error message for the Current Status text field

    When I click the "Create Configuration" submit button
    Then I should still see the Institutional Agreement Set Up Module page
    And I should still see the Required error message for the Current Status text field

Scenario Outline: Add item to Current Statuses list fails when text is duplicate

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Current Statuses list

    When I click the "Add" link for item #1 in the Current Statuses list
    Then I should see a Current Status text field for item #1 in the Current Statuses list

    When I type "<DuplicateValue>" into the Current Status text field for item #1 in the Current Statuses list
    And I click the "Add" link for item #1 in the Current Statuses list again
    Then I should still see the Institutional Agreement Set Up Module page
    And I should see the '<DuplicateValue> is a Duplicate' error message for the Current Status text field

Examples:
    | DuplicateValue |
    | Active         |
    | Dead           |
    | Inactive       |
    | Unknown        |

Scenario Outline: Add item to Current Statuses list causes automatic alphabetization

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Current Statuses list

    When I click the "Add" link for item #1 in the Current Statuses list
    Then I should see a Current Status text field for item #1 in the Current Statuses list

    When I type "<NewItemText>" into the Current Status text field for item #1 in the Current Statuses list
    And I click the "Add" link for item #1 in the Current Statuses list again
    Then I should see an item for "<NewItemText>" in the Current Statuses list
    And I should see a "Remove" link for item #<NewItemNumber> in the Current Statuses list

    When I click the "Remove" link for item #<NewItemNumber> in the Current Statuses list
    Then I shouldn't see an item for "<NewItemText>" in the Current Statuses list

Examples:
    | NewItemText | NewItemNumber |
    | AAA         | 2             |
    | BBB         | 3             |
    | EEE         | 4             |
    | JJJ         | 5             |
    | VVV         | 6             |

Scenario Outline: Edit item in Current Statuses list fails when text is empty

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Edit" link for item #<ListItemNumber> in the Current Statuses list

    When I click the "Edit" link for item #<ListItemNumber> in the Current Statuses list
    Then I should see a Current Status text field for item #<ListItemNumber> in the Current Statuses list
    And I should see a "Save" link for item #<ListItemNumber> in the Current Statuses list

    When I type "" into the Current Status text field for item #<ListItemNumber> in the Current Statuses list
    And I click the "Save" link for item #<ListItemNumber> in the Current Statuses list
    Then I should see the Required error message for the Current Status text field

    When I click the "Create Configuration" submit button
    Then I should still see the Institutional Agreement Set Up Module page
    And I should still see the Required error message for the Current Status text field

Examples:
    | ListItemNumber |
    | 2              |
    | 3              |
    | 4              |
    | 5              |

Scenario: Edit item in Current Statuses list fails when text is duplicate

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "Unknown" in the Current Statuses list
    And I should see an "Edit" link for item #5 in the Current Statuses list

    When I click the "Edit" link for item #5 in the Current Statuses list
    Then I should see a Current Status text field for item #5 in the Current Statuses list
    And I should see a "Save" link for item #5 in the Current Statuses list
    And I should see a "Cancel" link for item #5 in the Current Statuses list

    When I type "Active" into the Current Status text field for item #5 in the Current Statuses list
    And I click the "Save" link for item #5 in the Current Statuses list
    Then I should see the 'Active is a Duplicate' error message for the Current Status text field
    And I should still see a "Cancel" link for item #5 in the Current Statuses list

    When I click the "Cancel" link for item #5 in the Current Statuses list
    Then I should see an "Edit" link for item #5 in the Current Statuses list

Scenario Outline: Edit item in Current Statuses list replaces previous value

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "<OriginalText>" in the Current Statuses list
    And I should see an "Edit" link for item #<ListItemNumber> in the Current Statuses list

    When I click the "Edit" link for item #<ListItemNumber> in the Current Statuses list
    Then I should see a Current Status text field for item #<ListItemNumber> in the Current Statuses list
    And I should see a "Save" link for item #<ListItemNumber> in the Current Statuses list

    When I type "Test Value" into the Current Status text field for item #<ListItemNumber> in the Current Statuses list
    And  I click the "Save" link for item #<ListItemNumber> in the Current Statuses list
    Then I should see an item for "Test Value" in the Current Statuses list
    And I shouldn't see an item for "<OriginalText>" in the Current Statuses list

Examples:
    | ListItemNumber | OriginalText |
    | 2              | Active       |
    | 3              | Dead         |
    | 4              | Inactive     |
    | 5              | Unknown      |

Scenario: Edit item in Current Statuses list causes automatic alphabetization

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "Active" in the Current Statuses list
    And I should see an "Edit" link for item #2 in the Current Statuses list

    When I click the "Edit" link for item #2 in the Current Statuses list
    Then I should see a Current Status text field for item #2 in the Current Statuses list
    And I should see a "Save" link for item #2 in the Current Statuses list
    And I should see a "Cancel" link for item #2 in the Current Statuses list

    When I type "XXX Last Agreement" into the Current Status text field for item #2 in the Current Statuses list
    And I click the "Save" link for item #2 in the Current Statuses list
    Then I should see an item for "XXX Last Agreement" in the Current Statuses list
    And I should not see an item for "Activity Agreement" in the Current Statuses list

    When I click the "Remove" link for item #5 in the Current Statuses list
    Then I shouldn't see an item for "XXX Last Agreement" in the Current Statuses list

Scenario Outline: Cancel item edit in Current Statuses list

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "<OriginalText>" in the Current Statuses list
    And I should see an "Edit" link for item #<ListItemNumber> in the Current Statuses list

    When I click the "Edit" link for item #<ListItemNumber> in the Current Statuses list
    Then I should see a Current Status text field for item #<ListItemNumber> in the Current Statuses list
    And I should see a "Cancel" link for item #<ListItemNumber> in the Current Statuses list

    When I type "Agreement" into the Current Status text field for item #<ListItemNumber> in the Current Statuses list
    And I click the "Cancel" link for item #<ListItemNumber> in the Current Statuses list
    Then I should still see an item for "<OriginalText>" in the Current Statuses list

Examples:
    | ListItemNumber | OriginalText |
    | 2              | Active       |
    | 3              | Dead         |
    | 4              | Inactive     |
    | 5              | Unknown      |

Scenario Outline: Remove item from Current Statuses list

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see a "Remove" link for item #<ListItemNumber> in the Current Statuses list

    When I click the "Remove" link for item #<ListItemNumber> in the Current Statuses list
    Then I shouldn't see an item for "<OriginalText>" in the Current Statuses list

Examples:
    | ListItemNumber | OriginalText |
    | 2              | Active       |
    | 3              | Dead         |
    | 4              | Inactive     |
    | 5              | Unknown      |

Scenario: Changing Current Status Behavior makes Example combo box read only or editable

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see a Current Status Behavior radio button labeled 'Allow any text'
    And I should see a Current Status Behavior radio button labeled 'Use specific values'
    And the Current Status Behavior radio button labeled 'Allow any text' should be checked
    And I should see a Current Status Example text field

    When I check the Current Status Behavior radio button labeled 'Use specific values'
    Then the Current Status Example text field should be read only

    When I check the Current Status Behavior radio button labeled 'Allow any text'
    Then the Current Status Example text field should not be read only
