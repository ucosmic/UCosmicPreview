Feature: Update My Name
    In order to control what UCosmic says about me
    As a UCosmic user
    I want to update my name

Scenario: Display and select example Salutation and Suffix values after clicking down arrow buttons

    Given I am signed in as any1@usil.edu.pe
    And I am starting from the Personal Home page

    # salutation Dr.
    When I click the autocomplete dropdown arrow button for the Salutation combo field
    Then I should see an autocomplete dropdown menu item "Dr." for the Salutation combo field
    When I click the autocomplete dropdown menu item "Dr." for the Salutation combo field
    Then I should see "Dr." in the Salutation text field

    # salutation Mr.
    When I click the autocomplete dropdown arrow button for the Salutation combo field
    Then I should see an autocomplete dropdown menu item "Mr." for the Salutation combo field
    When I click the autocomplete dropdown menu item "Mr." for the Salutation combo field
    Then I should see "Mr." in the Salutation text field

    # salutation Ms.
    When I click the autocomplete dropdown arrow button for the Salutation combo field
    Then I should see an autocomplete dropdown menu item "Ms." for the Salutation combo field
    When I click the autocomplete dropdown menu item "Ms." for the Salutation combo field
    Then I should see "Ms." in the Salutation text field

    # salutation Prof.
    When I click the autocomplete dropdown arrow button for the Salutation combo field
    Then I should see an autocomplete dropdown menu item "Prof." for the Salutation combo field
    When I click the autocomplete dropdown menu item "Prof." for the Salutation combo field
    Then I should see "Prof." in the Salutation text field

    # salutation [None]
    When I click the autocomplete dropdown arrow button for the Salutation combo field
    Then I should see an autocomplete dropdown menu item "[None]" for the Salutation combo field
    When I click the autocomplete dropdown menu item "[None]" for the Salutation combo field
    Then I should see "" in the Salutation text field

    #suffixes Esq.
    When I click the autocomplete dropdown arrow button for the Suffix combo field
    Then I should see an autocomplete dropdown menu item "Esq." for the Suffix combo field
    When I click the autocomplete dropdown menu item "Esq." for the Suffix combo field
    Then I should see "Esq." in the Suffix text field

    #suffixes Jr.
    When I click the autocomplete dropdown arrow button for the Suffix combo field
    Then I should see an autocomplete dropdown menu item "Jr." for the Suffix combo field
    When I click the autocomplete dropdown menu item "Jr." for the Suffix combo field
    Then I should see "Jr." in the Suffix text field

    #suffixes PhD
    When I click the autocomplete dropdown arrow button for the Suffix combo field
    Then I should see an autocomplete dropdown menu item "PhD" for the Suffix combo field
    When I click the autocomplete dropdown menu item "PhD" for the Suffix combo field
    Then I should see "PhD" in the Suffix text field

    #suffixes Sr.
    When I click the autocomplete dropdown arrow button for the Suffix combo field
    Then I should see an autocomplete dropdown menu item "Sr." for the Suffix combo field
    When I click the autocomplete dropdown menu item "Sr." for the Suffix combo field
    Then I should see "Sr." in the Suffix text field

    #suffixes [None]
    When I click the autocomplete dropdown arrow button for the Suffix combo field
    Then I should see an autocomplete dropdown menu item "[None]" for the Suffix combo field
    When I click the autocomplete dropdown menu item "[None]" for the Suffix combo field
    Then I should see "" in the Suffix text field

Scenario: Display example Salutation and Suffix values by typing matching text info fields

    Given I am signed in as any1@usil.edu.pe
    And I am starting from the Personal Home page

    # salutation doesn't match [
    When  I type "[" into the Salutation text field
    Then  I should not see an autocomplete dropdown menu item "[None]" for the Salutation combo field

    # salutation matches D to Dr.
    When  I type "D" into the Salutation text field
    Then  I should see an autocomplete dropdown menu item "Dr." for the Salutation combo field

    # salutation empty when text matches
    When  I type "Dr." into the Salutation text field
    Then  I should not see an autocomplete dropdown menu item "Dr." for the Salutation combo field

    # salutation matches M to Mr. and Ms.
    When  I type "M" into the Salutation text field
    Then  I should see an autocomplete dropdown menu item "Mr." for the Salutation combo field
    And I should see an autocomplete dropdown menu item "Ms." for the Salutation combo field

    # salutation matches Mr to Mr. only
    When  I type "Mr" into the Salutation text field
    Then  I should see an autocomplete dropdown menu item "Mr." for the Salutation combo field
    And I should not see an autocomplete dropdown menu item "Ms." for the Salutation combo field

    # suffix doesn't match [
    When  I type "[" into the Suffix text field
    Then  I should not see an autocomplete dropdown menu item "[None]" for the Suffix combo field

    # suffix matches Esq to Esq.
    When  I type "Es" into the Suffix text field
    Then  I should see an autocomplete dropdown menu item "Esq." for the Suffix combo field
    And I should not see an autocomplete dropdown menu item "Sr." for the Suffix combo field

    # suffix empty when text matches
    When  I type "Esq." into the Suffix text field
    Then  I should not see an autocomplete dropdown menu item "Esq." for the Suffix combo field

    # suffix matches p to PhD
    When  I type "P" into the Suffix text field
    Then  I should see an autocomplete dropdown menu item "PhD" for the Suffix combo field

Scenario: Generate Display Name and then revert back to user entered Display Name

    Given I am signed in as any1@usil.edu.pe
    And I am starting from the Personal Home page

    # clear out values
    When I uncheck the Automatically Generate Display Name check box
    And I type "Test Display Name" into the Display Name text field
    And I type "" into the Salutation text field
    And I type "" into the First Name text field
    And I type "" into the Middle Name Or Initial text field
    And I type "" into the Last Name text field
    And I type "" into the Suffix text field
    And I check the Automatically Generate Display Name check box
    Then I should see "" in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

    # salutation only
    When I type "Dr." into the Salutation text field
    And I check the Automatically Generate Display Name check box
    Then I should see "Dr." in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

    # first name only
    When I type "" into the Salutation text field
    And I type "Any" into the First Name text field
    And I check the Automatically Generate Display Name check box
    Then I should see "Any" in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

    # middle name only
    When I type "" into the First Name text field
    And I type "Single" into the Middle Name Or Initial text field
    And I check the Automatically Generate Display Name check box
    Then I should see "Single" in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

    # last name only
    When I type "" into the Middle Name Or Initial text field
    And I type "One" into the Last Name text field
    And I check the Automatically Generate Display Name check box
    Then I should see "One" in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

    # suffix only
    When I type "" into the Last Name text field
    And I type "PhD" into the Suffix text field
    And I check the Automatically Generate Display Name check box
    Then I should see "PhD" in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

    # full name
    When I type "Dr." into the Salutation text field
    And I type "Any" into the First Name text field
    And I type "Single" into the Middle Name Or Initial text field
    And I type "One" into the Last Name text field
    And I type "PhD" into the Suffix text field
    And I check the Automatically Generate Display Name check box
    Then I should see "Dr. Any Single One PhD" in the Display Name text field
    When I uncheck the Automatically Generate Display Name check box
    Then I should see "Test Display Name" in the Display Name text field

Scenario: Update fails when Display Name is empty

    Given I am signed in as any1@usil.edu.pe
    And I am starting from the Personal Home page

    When I uncheck the Automatically Generate Display Name check box
    And I type "" into the Display Name text field
    And I click the "Save Changes" submit button

    Then I should still see the Personal Home page
    And I should see the Required error message for the Display Name text field
    And I should see "" in the Display Name text field

@UsingFreshExamplePersonNameForAny1AtUsil
Scenario Outline: Update changes person name fields

    Given I am using the <Browser> browser
    And I am signed in as any1@usil.edu.pe
    And I am starting from the Personal Home page

    When I uncheck the Automatically Generate Display Name check box
    And I type "Test Display Name 1" into the Display Name text field
    And I type "" into the Salutation text field
    And I type "" into the First Name text field
    And I type "" into the Middle Name Or Initial text field
    And I type "" into the Last Name text field
    And I type "" into the Suffix text field
    And I click the "Save Changes" submit button

    Then I should see the Personal Home page
    And I should see the flash feedback message "Your info was successfully updated."
    And I should see "Test Display Name 1" in the Display Name text field
    And I should see "" in the Salutation text field
    And I should see "" in the First Name text field
    And I should see "" in the Middle Name Or Initial text field
    And I should see "" in the Last Name text field
    And I should see "" in the Suffix text field

    When I type "" into the Display Name text field
    And I type "Dr." into the Salutation text field
    And I type "Adam" into the First Name text field
    And I type "B." into the Middle Name Or Initial text field
    And I type "West" into the Last Name text field
    And I type "Sr." into the Suffix text field
    And I check the Automatically Generate Display Name check box
    Then I should see "Dr. Adam B. West Sr." in the Display Name text field

    When I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your info was successfully updated."
    And I should see "Dr. Adam B. West Sr." in the Display Name text field
    And the Automatically Generate Display Name check box should be checked

Examples:
    | Browser |
    | Chrome  |
    | Firefox |
    | MSIE    |
