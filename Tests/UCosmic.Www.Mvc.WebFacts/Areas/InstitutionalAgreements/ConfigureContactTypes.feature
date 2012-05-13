Feature: Configure Institutional Agreements Module Contact Types
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

Scenario: Add item to Contact Types list fails when text is empty

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Contact Types list

    When I click the "Add" link for item #1 in the Contact Types list
    Then I should see a Contact Type text field for item #1 in the Contact Types list

    When I click the "Add" link for item #1 in the Contact Types list again
    Then I should see the Required error message for the Contact Type text field

    When I click the "Create Configuration" submit button
    Then I should still see the Institutional Agreement Set Up Module page
    And I should still see the Required error message for the Contact Type text field

Scenario Outline: Add item to Contact Types list fails when text is duplicate

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Contact Types list

    When I click the "Add" link for item #1 in the Contact Types list
    Then I should see a Contact Type text field for item #1 in the Contact Types list

    When I type "<DuplicateValue>" into the Contact Type text field for item #1 in the Contact Types list
    And I click the "Add" link for item #1 in the Contact Types list again
    Then I should still see the Institutional Agreement Set Up Module page
    And I should see the '<DuplicateValue> is a Duplicate' error message for the Contact Type text field

Examples:
    | DuplicateValue    |
    | Home Principal    |
    | Home Secondary    |
    | Partner Principal |
    | Partner Secondary |

Scenario Outline: Add item to Contact Types list causes automatic alphabetization

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Contact Types list

    When I click the "Add" link for item #1 in the Contact Types list
    Then I should see a Contact Type text field for item #1 in the Contact Types list

    When I type "<NewItemText>" into the Contact Type text field for item #1 in the Contact Types list
    And I click the "Add" link for item #1 in the Contact Types list again
    Then I should see an item for "<NewItemText>" in the Contact Types list
    And I should see a "Remove" link for item #<NewItemNumber> in the Contact Types list

    When I click the "Remove" link for item #<NewItemNumber> in the Contact Types list
    Then I shouldn't see an item for "<NewItemText>" in the Contact Types list

Examples:
    | NewItemText | NewItemNumber |
    | AAA         | 2             |
    | Home Pro    | 3             |
    | Home Soc    | 4             |
    | Partner Pro | 5             |
    | Partner Soc | 6             |

Scenario Outline: Edit item in Contact Types list fails when text is empty

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Edit" link for item #<ListItemNumber> in the Contact Types list

    When I click the "Edit" link for item #<ListItemNumber> in the Contact Types list
    Then I should see a Contact Type text field for item #<ListItemNumber> in the Contact Types list
    And I should see a "Save" link for item #<ListItemNumber> in the Contact Types list

    When I type "" into the Contact Type text field for item #<ListItemNumber> in the Contact Types list
    And I click the "Save" link for item #<ListItemNumber> in the Contact Types list
    Then I should see the Required error message for the Contact Type text field

    When I click the "Create Configuration" submit button
    Then I should still see the Institutional Agreement Set Up Module page
    And I should still see the Required error message for the Contact Type text field

Examples:
    | ListItemNumber |
    | 2              |
    | 3              |
    | 4              |
    | 5              |

Scenario: Edit item in Contact Types list fails when text is duplicate

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "Partner Principal" in the Contact Types list
    And I should see an "Edit" link for item #4 in the Contact Types list

    When I click the "Edit" link for item #4 in the Contact Types list
    Then I should see a Contact Type text field for item #4 in the Contact Types list
    And I should see a "Save" link for item #4 in the Contact Types list
    And I should see a "Cancel" link for item #4 in the Contact Types list

    When I type "Home Secondary" into the Contact Type text field for item #4 in the Contact Types list
    And I click the "Save" link for item #4 in the Contact Types list
    Then I should see the 'Home Secondary is a Duplicate' error message for the Contact Type text field
    And I should still see a "Cancel" link for item #4 in the Contact Types list

    When I click the "Cancel" link for item #4 in the Contact Types list
    Then I should see an "Edit" link for item #4 in the Contact Types list

Scenario Outline: Edit item in Contact Types list replaces previous value

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "<OriginalText>" in the Contact Types list
    And I should see an "Edit" link for item #<ListItemNumber> in the Contact Types list

    When I click the "Edit" link for item #<ListItemNumber> in the Contact Types list
    Then I should see a Contact Type text field for item #<ListItemNumber> in the Contact Types list
    And I should see a "Save" link for item #<ListItemNumber> in the Contact Types list

    When I type "Test Value" into the Contact Type text field for item #<ListItemNumber> in the Contact Types list
    And  I click the "Save" link for item #<ListItemNumber> in the Contact Types list
    Then I should see an item for "Test Value" in the Contact Types list
    And I shouldn't see an item for "<OriginalText>" in the Contact Types list

Examples:
    | ListItemNumber | OriginalText      |
    | 2              | Home Principal    |
    | 3              | Home Secondary    |
    | 4              | Partner Principal |
    | 5              | Partner Secondary |

Scenario: Edit item in Contact Types list causes automatic alphabetization

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "Home Principal" in the Contact Types list
    And I should see an "Edit" link for item #2 in the Contact Types list

    When I click the "Edit" link for item #2 in the Contact Types list
    Then I should see a Contact Type text field for item #2 in the Contact Types list
    And I should see a "Save" link for item #2 in the Contact Types list
    And I should see a "Cancel" link for item #2 in the Contact Types list

    When I type "XXX Last Agreement" into the Contact Type text field for item #2 in the Contact Types list
    And I click the "Save" link for item #2 in the Contact Types list
    Then I should see an item for "XXX Last Agreement" in the Contact Types list
    And I should not see an item for "Home Principal" in the Contact Types list

    When I click the "Remove" link for item #5 in the Contact Types list
    Then I shouldn't see an item for "XXX Last Agreement" in the Contact Types list

Scenario Outline: Cancel item edit in Contact Types list

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an item for "<OriginalText>" in the Contact Types list
    And I should see an "Edit" link for item #<ListItemNumber> in the Contact Types list

    When I click the "Edit" link for item #<ListItemNumber> in the Contact Types list
    Then I should see a Contact Type text field for item #<ListItemNumber> in the Contact Types list
    And I should see a "Cancel" link for item #<ListItemNumber> in the Contact Types list

    When I type "Agreement" into the Contact Type text field for item #<ListItemNumber> in the Contact Types list
    And I click the "Cancel" link for item #<ListItemNumber> in the Contact Types list
    Then I should still see an item for "<OriginalText>" in the Contact Types list

Examples:
    | ListItemNumber | OriginalText      |
    | 2              | Home Principal    |
    | 3              | Home Secondary    |
    | 4              | Partner Principal |
    | 5              | Partner Secondary |

Scenario Outline: Remove item from Contact Types list

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see a "Remove" link for item #<ListItemNumber> in the Contact Types list

    When I click the "Remove" link for item #<ListItemNumber> in the Contact Types list
    Then I shouldn't see an item for "<OriginalText>" in the Contact Types list

Examples:
    | ListItemNumber | OriginalText      |
    | 2              | Home Principal    |
    | 3              | Home Secondary    |
    | 4              | Partner Principal |
    | 5              | Partner Secondary |

Scenario: Changing Contact Type Behavior makes Example combo box read only or editable

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see a Contact Type Behavior radio button labeled 'Allow any text'
    And I should see a Contact Type Behavior radio button labeled 'Use specific values'
    And the Contact Type Behavior radio button labeled 'Allow any text' should be checked
    And I should see a Contact Type Example text field

    When I check the Contact Type Behavior radio button labeled 'Use specific values'
    Then the Contact Type Example text field should be read only

    When I check the Contact Type Behavior radio button labeled 'Allow any text'
    Then the Contact Type Example text field should not be read only
