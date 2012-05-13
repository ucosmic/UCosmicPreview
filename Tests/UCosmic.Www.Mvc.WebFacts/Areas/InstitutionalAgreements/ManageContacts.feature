Feature: Manage Contacts
    In order to know which people are responsible for my agreements
    As an Institutional Agreement Manager
    I want to manage a list of Contacts for each agreement

Background:

    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

Scenario: Add Contact fails when required text fields are empty

    When I click the "Add a new agreement" link
    Then I should see the Institutional Agreement Add page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I click the "Add Contact" submit button
    Then I should see the Required error message for the Contact Type text field
    And I should see the Required error message for the First Name text field
    And I should see the Required error message for the Last Name text field

    When I type "Test" into the Contact Type text field
    And I click the "Add Contact" submit button
    Then I should not see the Required error message for the Contact Type text field

    When I type "" into the Contact Type text field
    And I type "Test" into the First Name text field
    And I click the "Add Contact" submit button
    Then I should not see the Required error message for the First Name text field

    When I type "" into the First Name text field
    And I type "Test" into the Last Name text field
    And I click the "Add Contact" submit button
    Then I should not see the Required error message for the Last Name text field

    # close dialog using close icon
    When I click the Add Institutional Agreement Contact modal dialog close icon
    Then I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see a "Cancel" link

    When I click the "Cancel" link
    Then I should see the Institutional Agreement Management page
    And I should see an "Agreement, UC 01 test" link

    When I click the "Agreement, UC 01 test" link
    Then I should see the Institutional Agreement Edit page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I click the "Add Contact" submit button
    Then I should see the Required error message for the Contact Type text field
    And I should see the Required error message for the First Name text field
    And I should see the Required error message for the Last Name text field

    When I type "Test" into the Contact Type text field
    And I click the "Add Contact" submit button
    Then I should not see the Required error message for the Contact Type text field

    When I type "" into the Contact Type text field
    And I type "Test" into the First Name text field
    And I click the "Add Contact" submit button
    Then I should not see the Required error message for the First Name text field

    When I type "" into the First Name text field
    And I type "Test" into the Last Name text field
    And I click the "Add Contact" submit button
    Then I should not see the Required error message for the Last Name text field

    # close dialog using cancel button
    When I click the "Cancel" button
    Then I should not see a modal dialog with an Add Institutional Agreement Contact form

Scenario: Display example Contact Type values by typing matching text

    When I click the "Add a new agreement" link
    Then I should see the Institutional Agreement Add page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When  I type "Partner" into the Contact Type text field
    Then I should see an autocomplete dropdown menu item "Partner Principal" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Partner Secondary" for the Contact Type combo field

    When I click the "Cancel" button
    And I wait for 2 seconds
    And I click the "Cancel" link
    Then I should see the Institutional Agreement Management page
    And I should see an "Agreement, UC 01 test" link

    When I click the "Agreement, UC 01 test" link
    Then I should see the Institutional Agreement Edit page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When  I type "Home" into the Contact Type text field
    Then I should see an autocomplete dropdown menu item "Home Principal" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Home Secondary" for the Contact Type combo field

    When I click the "Cancel" button
    And I wait for 2 seconds
    Then I should see a "Cancel" link

Scenario: Display and select example Contact Type value after clicking down arrow button

    When I click the "Add a new agreement" link
    Then I should see the Institutional Agreement Add page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I click the autocomplete dropdown arrow button for the Contact Type combo field
    Then I should see an autocomplete dropdown menu item "Partner Principal" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Partner Secondary" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Home Principal" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Home Secondary" for the Contact Type combo field

    When I click the autocomplete dropdown menu item "Partner Secondary" for the Contact Type combo field
    Then I should see "Partner Secondary" in the Contact Type text field

    When I click the "Cancel" button
    And I wait for 2 seconds
    And I click the "Cancel" link
    Then I should see the Institutional Agreement Management page
    And I should see an "Agreement, UC 01 test" link

    When I click the "Agreement, UC 01 test" link
    Then I should see the Institutional Agreement Edit page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I click the autocomplete dropdown arrow button for the Contact Type combo field
    Then I should see an autocomplete dropdown menu item "Partner Principal" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Partner Secondary" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Home Principal" for the Contact Type combo field
    And I should see an autocomplete dropdown menu item "Home Secondary" for the Contact Type combo field

    When I click the autocomplete dropdown menu item "Home Secondary" for the Contact Type combo field
    Then I should see "Home Secondary" in the Contact Type text field

Scenario Outline: Display additional person name text fields by clicking Show Additional Textboxes link

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form
    And I should see a "Click here to show additional textboxes" link

    When  I click the "Click here to show additional textboxes" link
    Then I should see a Salutation text field
    And I should see a Middle Name Or Initial text field
    And I should see a Suffix text field

Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |

Scenario Outline: Hide additional person name text fields by clicking Hide Additional Textboxes link

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form
    And I should see a "Click here to show additional textboxes" link

    When  I click the "Click here to show additional textboxes" link
    Then I should see a Salutation text field
    And I should see a Middle Name Or Initial text field
    And I should see a Suffix text field
    And I should see a "Click here to hide additional textboxes" link

    When  I click the "Click here to hide additional textboxes" link
    Then I should not see a Salutation text field
    And I should not see a Middle Name Or Initial text field
    And I should not see a Suffix text field

Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |

Scenario Outline: Revert from read only person to user entered values by clicking Clear Your Selection and Try Again link

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<InputValue>" into the <FieldName> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <FieldName> combo field

    When I click the autocomplete dropdown menu item "<ListValue>" for the <FieldName> combo field
    Then I should see "<FieldValue>" in the <FieldName> text field
    And I should see a "Click here to clear your selection and try again" link

    When I click the "Click here to clear your selection and try again" link
    Then I should see "<InputValue>" in the <FieldName> text field

Examples:
    | AddOrEdit | LinkText              | InputValue | FieldName  | ListValue                                   | FieldValue |
    | Add       | Add a new agreement   | le         | Last Name  | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Leventhal  |
    | Edit      | Agreement, UC 01 test | br         | First Name | Brandon Lee (Brandon@terradotta.com)        | Brandon    |

Scenario Outline: Select read only person from autocomplete dropdown menu

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<InputValue>" into the <InputField> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <InputField> combo field

    When I click the autocomplete dropdown menu item "<ListValue>" for the <InputField> combo field
    Then I should see "<FirstName>" in the First Name text field
    And the First Name text field should be read only

    And I should see "<LastName>" in the Last Name text field
    And the Last Name text field should be read only

    And I should see "<Email>" in the Email Address text field
    And the Email Address text field should be read only

Examples:
    | AddOrEdit | LinkText              | InputValue | InputField    | ListValue                                   | FirstName | LastName  | Email                    |
    | Add       | Add a new agreement   | br         | First Name    | Brandon Lee (Brandon@terradotta.com)        | Brandon   | Lee       | Brandon@terradotta.com   |
    | Add       | Add a new agreement   | le         | Last Name     | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Mitch     | Leventhal | Mitch.Leventhal@suny.edu |
    | Add       | Add a new agreement   | cu         | Email Address | Ron Cushing (Ronald.Cushing@uc.edu)         | Ron       | Cushing   | Ronald.Cushing@uc.edu    |
    | Edit      | Agreement, UC 01 test | br         | First Name    | Brandon Lee (Brandon@terradotta.com)        | Brandon   | Lee       | Brandon@terradotta.com   |
    | Edit      | Agreement, UC 01 test | le         | Last Name     | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Mitch     | Leventhal | Mitch.Leventhal@suny.edu |
    | Edit      | Agreement, UC 01 test | cu         | Email Address | Ron Cushing (Ronald.Cushing@uc.edu)         | Ron       | Cushing   | Ronald.Cushing@uc.edu    |

Scenario Outline: Add existing person to Contacts list

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<PersonFieldValue>" into the <PersonFieldName> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field
    And I should click the autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field

    When I type "<ContactTypeValue>" into the Contact Type text field
    And I see an autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    And I click the autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    Then I should see "<ContactTypeValue>" in the Contact Type text field

    When I click the "Add Contact" submit button
    Then I should still see the Institutional Agreement <AddOrEdit> page
    And I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see an item for "<ContactName>" in the Contacts list

Examples:
    | AddOrEdit | LinkText              | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                                   | ContactName                       |
    | Add       | Add a new agreement   | Partner Principal | mi               | First Name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | Add       | Add a new agreement   | Partner Secondary | le               | Last Name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | Add       | Add a new agreement   | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |
    | Edit      | Agreement, UC 01 test | Partner Principal | mi               | First Name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | Edit      | Agreement, UC 01 test | Partner Secondary | le               | Last Name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | Edit      | Agreement, UC 01 test | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |

Scenario Outline: Remove Contact from list

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<PersonFieldValue>" into the <PersonFieldName> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field
    And I should click the autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field

    When I type "<ContactTypeValue>" into the Contact Type text field
    And I see an autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    And I click the autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    Then I should see "<ContactTypeValue>" in the Contact Type text field

    When I click the "Add Contact" submit button
    Then I should still see the Institutional Agreement <AddOrEdit> page
    And I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see an item for "<ContactName>" in the Contacts list

    When I click the remove icon for "<ContactName>" in the Contacts list
    Then I should not see an item for "<ContactName>" in the Contacts list

Examples:
    | AddOrEdit | LinkText              | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                                   | ContactName                       |
    | Add       | Add a new agreement   | Partner Principal | mi               | First Name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | Add       | Add a new agreement   | Partner Secondary | le               | Last Name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | Add       | Add a new agreement   | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |
    | Edit      | Agreement, UC 01 test | Partner Principal | mi               | First Name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | Edit      | Agreement, UC 01 test | Partner Secondary | le               | Last Name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | Edit      | Agreement, UC 01 test | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Store added contact in new agreement after save

    Given I am using the <Browser> browser
    When I click the "Add a new agreement" link
    Then I should see the Institutional Agreement Add page

    When I type "Memorandum of Understanding" into the Agreement Type text field
    And I type "Active" into the Current Status text field
    And I type "9/1/2011" into the Start Date text field
    And I type "8/31/2015" into the Expiration Date text field
    And I type "<Title>" into the Summary Description text field
    And I dismiss all autocomplete dropdown menus
    And I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<PersonFieldValue>" into the <PersonFieldName> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field
    And I should click the autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field

    When I type "<ContactTypeValue>" into the Contact Type text field
    Then I should see an autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    And I should click the autocomplete dropdown arrow button for the Contact Type combo field

    When I click the "Add Contact" submit button
    Then I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see an item for "<ContactName>" in the Contacts list

    When I click the "Add Agreement" submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should see an item for "<ContactName>" in the Contacts list

Examples:
    | Browser | Title                 | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                           | ContactName                   |
    | Chrome  | Agreement, UC A1 test | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu) | Home Principal Ron Cushing    |
    | Chrome  | Agreement, UC B1 test | Partner Principal | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu) | Partner Principal Ron Cushing |
    | Firefox | Agreement, UC A1 test | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu) | Home Principal Ron Cushing    |
    | Firefox | Agreement, UC B1 test | Partner Principal | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu) | Partner Principal Ron Cushing |
    | MSIE    | Agreement, UC A1 test | Home Principal    | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu) | Home Principal Ron Cushing    |
    | MSIE    | Agreement, UC B1 test | Partner Principal | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu) | Partner Principal Ron Cushing |

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Purge removed contact from new agreement after save

    Given I am using the <Browser> browser
    When I click the "Add a new agreement" link
    Then I should see the Institutional Agreement Add page

    When I type "Institutional Collaboration Agreement" into the Agreement Type text field
    And I type "Active" into the Current Status text field
    And I type "9/1/2011" into the Start Date text field
    And I type "8/31/2015" into the Expiration Date text field
    And I type "<Title>" into the Summary Description text field
    And I dismiss all autocomplete dropdown menus
    And I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<PersonFieldValue>" into the <PersonFieldName> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field
    And I should click the autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field

    When I type "<ContactTypeValue>" into the Contact Type text field
    Then I should see an autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    And I should click the autocomplete dropdown arrow button for the Contact Type combo field

    When I click the "Add Contact" submit button
    Then I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see an item for "<ContactName>" in the Contacts list

    When I click the "Add Agreement" submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should see an item for "<ContactName>" in the Contacts list

    When I click the remove icon for "<ContactName>" in the Contacts list
    Then I should not see an item for "<ContactName>" in the Contacts list

    When I click the "Save Changes" submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should not see an item for "<ContactName>" in the Contacts list

Examples:
    | Browser | Title                 | ContactTypeValue | PersonFieldValue | PersonFieldName | ListValue                                  | ContactName                    |
    | Chrome  | Agreement, UC A2 test | Home Principal   | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)        | Home Principal Ron Cushing     |
    | Chrome  | Agreement, UC B2 test | Home Principal   | mi               | Email Address   | Mitch Leventhal (Mitch.Leventhal@suny.edu) | Home Principal Mitch Leventhal |
    | Firefox | Agreement, UC A2 test | Home Principal   | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)        | Home Principal Ron Cushing     |
    | Firefox | Agreement, UC B2 test | Home Principal   | mi               | Email Address   | Mitch Leventhal (Mitch.Leventhal@suny.edu) | Home Principal Mitch Leventhal |
    | MSIE    | Agreement, UC A2 test | Home Principal   | ro               | Email Address   | Ron Cushing (Ronald.Cushing@uc.edu)        | Home Principal Ron Cushing     |
    | MSIE    | Agreement, UC B2 test | Home Principal   | mi               | Email Address   | Mitch Leventhal (Mitch.Leventhal@suny.edu) | Home Principal Mitch Leventhal |

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Store added contact in existing agreement after save

    Given I am using the <Browser> browser
    When I click the "Agreement, UC 01 test" link
    Then I should see the Institutional Agreement Edit page

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "<PersonFieldValue>" into the <PersonFieldName> text field
    Then I should see an autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field
    And I should click the autocomplete dropdown menu item "<ListValue>" for the <PersonFieldName> combo field

    When I type "<ContactTypeValue>" into the Contact Type text field
    Then I should see an autocomplete dropdown menu item "<ContactTypeValue>" for the Contact Type combo field
    And I should click the autocomplete dropdown arrow button for the Contact Type combo field

    When I click the "Add Contact" submit button
    Then I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see an item for "<ContactName>" in the Contacts list

    When I click the "Save Changes" submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should see an item for "<ContactName>" in the Contacts list

Examples:
    | Browser | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                            | ContactName                    |
    | Chrome  | Partner Principal | mary             | First Name      | Mary Watkins (Mary.Watkins@uc.edu)   | Partner Principal Mary Watkins |
    | Chrome  | Partner Secondary | watk             | Last Name       | Watkins, Mary (Mary.Watkins@uc.edu)  | Partner Secondary Mary Watkins |
    | Chrome  | Home Principal    | br               | Email Address   | Brandon Lee (Brandon@terradotta.com) | Home Principal Brandon Lee     |
    | Firefox | Partner Principal | mary             | First Name      | Mary Watkins (Mary.Watkins@uc.edu)   | Partner Principal Mary Watkins |
    | Firefox | Partner Secondary | watk             | Last Name       | Watkins, Mary (Mary.Watkins@uc.edu)  | Partner Secondary Mary Watkins |
    | Firefox | Home Principal    | br               | Email Address   | Brandon Lee (Brandon@terradotta.com) | Home Principal Brandon Lee     |
    | MSIE    | Partner Principal | mary             | First Name      | Mary Watkins (Mary.Watkins@uc.edu)   | Partner Principal Mary Watkins |
    | MSIE    | Partner Secondary | watk             | Last Name       | Watkins, Mary (Mary.Watkins@uc.edu)  | Partner Secondary Mary Watkins |
    | MSIE    | Home Principal    | br               | Email Address   | Brandon Lee (Brandon@terradotta.com) | Home Principal Brandon Lee     |

@UsingFreshExampleUcInstitutionalAgreementData
Scenario Outline: Purge removed contact from existing agreement after save

    Given I am using the <Browser> browser
    When I click the "Agreement, UC 02 test" link
    Then I should see the Institutional Agreement Edit page
    And I should see an "Add Contact" link

    When I click the "Add Contact" link
    Then I should see a modal dialog with an Add Institutional Agreement Contact form

    When I type "Home Principal" into the Contact Type text field
    Then I should see an autocomplete dropdown menu item "Home Principal" for the Contact Type combo field
    And I should click the autocomplete dropdown arrow button for the Contact Type combo field

    When I type "ro" into the Email Address text field
    Then I should see an autocomplete dropdown menu item "Ron Cushing (Ronald.Cushing@uc.edu)" for the Email Address combo field
    And I should click the autocomplete dropdown menu item "Ron Cushing (Ronald.Cushing@uc.edu)" for the Email Address combo field

    When I click the "Add Contact" submit button
    Then I should not see a modal dialog with an Add Institutional Agreement Contact form
    And I should see an item for "Home Principal Ron Cushing" in the Contacts list

    When I click the "Save Changes" submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should see an item for "Home Principal Ron Cushing" in the Contacts list

    When I click the remove icon for "Home Principal Ron Cushing" in the Contacts list
    Then I should not see an item for "Home Principal Ron Cushing" in the Contacts list

    When I click the "Save Changes" submit button
    Then I should see the Public Institutional Agreement Detail page
    And I should see an "Edit this agreement" link

    When I click the "Edit this agreement" link
    Then I should see the Institutional Agreement Edit page
    And I should not see an item for "Home Principal Ron Cushing" in the Contacts list

Examples:
    | Browser |
    | Chrome  |
    | Firefox |
    | MSIE    |
