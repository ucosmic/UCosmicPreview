@InstAgrForms @InstAgrFormsR03 @InstAgrFormsFreshTestAgreementUc01
Feature:  Institutional Agreement Management Preview Revision 3
		  In order to know which people are responsible for my Institutional Agreements
		  As an Institutional Agreement Manager
		  I want to add, remove, and generally manage the contact information attached to my Institutional Agreements in UCosmic

#execute these steps before every scenario in this file
Background: 
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url

@InstAgrFormsR0301
Scenario Outline: Institutional Agreement forms successfully display Add Contact link
    When  I click the "<LinkText>" link
    And   I see a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	Then  I should see a "Add Contact" link
    Examples: 
    | AddOrEdit | LinkText              |
    | new       | Add a new agreement   |
    | edit      | Agreement, UC 01 test |

@InstAgrFormsR0302
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully display after clicking Add Contact link
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    When  I click the "Add Contact" link
	Then  I should see the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
    Examples: 
    | AddOrEdit | LinkText              |
    | new       | Add a new agreement   |
    | edit      | Agreement, UC 01 test |

@InstAgrFormsR0303
Scenario Outline: Institutional Agreement Forms Add Contact modal dialog unsuccessfully submit with empty required field
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	When  I type "<Text>" into the "<Field>" text box on the Add Contact modal dialog
	And   I click the "Add Contact" button on the Add Contact modal dialog
    Then  I should <SeeOrNot> the error message "<ErrorMessage>" for the "<Field>" text box on the Add Contact modal dialog
    Examples: 
    | AddOrEdit | LinkText              | Text | Field        | SeeOrNot | ErrorMessage                    |
    | new       | Add a new agreement   |      | Contact type | see      | Contact type is required.       |
    | new       | Add a new agreement   | Test | Contact type | not see  | Contact type is required.       |
    | new       | Add a new agreement   |      | First name   | see      | Contact first name is required. |
    | new       | Add a new agreement   | Test | First name   | not see  | Contact first name is required. |
    | new       | Add a new agreement   |      | Last name    | see      | Contact last name is required.  |
    | new       | Add a new agreement   | Test | Last name    | not see  | Contact last name is required.  |
    | edit      | Agreement, UC 01 test |      | Contact type | see      | Contact type is required.       |
    | edit      | Agreement, UC 01 test | Test | Contact type | not see  | Contact type is required.       |
    | edit      | Agreement, UC 01 test |      | First name   | see      | Contact first name is required. |
    | edit      | Agreement, UC 01 test | Test | First name   | not see  | Contact first name is required. |
    | edit      | Agreement, UC 01 test |      | Last name    | see      | Contact last name is required.  |
    | edit      | Agreement, UC 01 test | Test | Last name    | not see  | Contact last name is required.  |

@InstAgrFormsR0304
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully display autocomplete example after clicking Contact Type the dropdown arrow
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
    When  I click the autocomplete dropdown arrow button for the "Contact type" text box on the Add Contact modal dialog
    Then  I should see a "Contact type" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    Examples: 
    | AddOrEdit | LinkText              | ListValue         |
    | new       | Add a new agreement   | Partner Principal |
    | new       | Add a new agreement   | Partner Secondary |
    | new       | Add a new agreement   | Home Principal    |
    | new       | Add a new agreement   | Home Secondary    |
    | edit      | Agreement, UC 01 test | Partner Principal |
    | edit      | Agreement, UC 01 test | Partner Secondary |
    | edit      | Agreement, UC 01 test | Home Principal    |
    | edit      | Agreement, UC 01 test | Home Secondary    |

@InstAgrFormsR0305
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully display autocomplete example after typing into Contact type text box
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	When  I type "<InputValue>" into the "Contact type" text box on the Add Contact modal dialog
    Then  I should see a "Contact type" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    Examples: 
    | AddOrEdit | LinkText              | InputValue | ListValue         |
    | new       | Add a new agreement   | Partner P  | Partner Principal |
    | new       | Add a new agreement   | Home S     | Home Secondary    |
    | edit      | Agreement, UC 01 test | Partner S  | Partner Secondary |
    | edit      | Agreement, UC 01 test | Home P     | Home Principal    |

@InstAgrFormsR0306
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully choose Contact type autocomplete example after clicking dropdown arrow
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
    And   I have clicked the autocomplete dropdown arrow button for the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    When  I click the "Contact type" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    Then  I should see "<ListValue>" in the "Contact type" text box on the Add Contact modal dialog
    Examples:
    | AddOrEdit | LinkText              | ListValue         |
    | new       | Add a new agreement   | Partner Principal |
    | edit      | Agreement, UC 01 test | Partner Secondary |
    | new       | Add a new agreement   | Home Principal    |
    | edit      | Agreement, UC 01 test | Home Secondary    |

@InstAgrFormsR0307
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully dismiss by clicking Cancel button
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	When  I click the "Cancel" button on the Add Contact modal dialog
	Then  I should not see the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	Examples: 
	| AddOrEdit | LinkText              |
	| new       | Add a new agreement   |
	| edit      | Agreement, UC 01 test |

@InstAgrFormsR0308
@OnlyInChrome
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully dismiss by clicking close icon
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	When  I click the Add Contact modal dialog close icon
	Then  I should not see the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	Examples: 
	| AddOrEdit | LinkText              |
	| new       | Add a new agreement   |
	| edit      | Agreement, UC 01 test |
	
@InstAgrFormsR0309
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully show additional name fields
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	And   I have seen a "Click here to show additional textboxes" link
	When  I click the "Click here to show additional textboxes" link
	Then  I should see a text box for "Salutation" on the Add Contact modal dialog
    And   I should see a text box for "Middle name or initial" on the Add Contact modal dialog
    And   I should see a text box for "Suffix" on the Add Contact modal dialog
	Examples: 
	| AddOrEdit | LinkText              |
	| new       | Add a new agreement   |
	| edit      | Agreement, UC 01 test |

@InstAgrFormsR0310
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully hide additional name fields
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	And   I have seen a "Click here to show additional textboxes" link
	And   I have clicked the "Click here to show additional textboxes" link
    And   I have seen a text box for "Salutation" on the Add Contact modal dialog
    And   I have seen a text box for "Middle name or initial" on the Add Contact modal dialog
    And   I have seen a text box for "Suffix" on the Add Contact modal dialog
	And   I have seen a "Click here to hide additional textboxes" link
	When  I click the "Click here to hide additional textboxes" link
	Then  I should not see a text box for "Salutation" on the Add Contact modal dialog
    And   I should not see a text box for "Middle name or initial" on the Add Contact modal dialog
    And   I should not see a text box for "Suffix" on the Add Contact modal dialog
	Examples: 
	| AddOrEdit | LinkText              |
	| new       | Add a new agreement   |
	| edit      | Agreement, UC 01 test |

@InstAgrFormsR0311
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully revert from read only person to user entered value
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	And   I have typed "<InputValue>" into the "<FieldName>" text box on the Add Contact modal dialog
	And   I have seen a "<FieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
	And   I have clicked the "<FieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
	And   I have seen "<FieldValue>" in the "<FieldName>" text box on the Add Contact modal dialog
	And   I have seen a "Click here to clear your selection and try again" link
	When  I click the "Click here to clear your selection and try again" link
	Then  I should see "<InputValue>" in the "<FieldName>" text box on the Add Contact modal dialog
	Examples: 
	| AddOrEdit | LinkText              | InputValue | FieldName  | ListValue                                   | FieldValue |
	| new       | Add a new agreement   | le         | Last name  | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Leventhal  |
	| edit      | Agreement, UC 01 test | br         | First name | Brandon Lee (Brandon@terradotta.com)        | Brandon    |

@InstAgrFormsR0312
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully select read only person from autocomplete dropdown menu
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
	And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	And   I have typed "<InputValue>" into the "<InputField>" text box on the Add Contact modal dialog
	And   I have seen a "<InputField>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
	When  I click the "<InputField>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
	Then  I should see "<FirstName>" in the "First name" text box on the Add Contact modal dialog
    And   I should see a read only text box for "First name" on the Add Contact modal dialog
    And   I should see "<LastName>" in the "Last name" text box on the Add Contact modal dialog
    And   I should see a read only text box for "Last name" on the Add Contact modal dialog
    And   I should see "<Email>" in the "Email address" text box on the Add Contact modal dialog
    And   I should see a read only text box for "Email address" on the Add Contact modal dialog
    Examples: 
    	| AddOrEdit | LinkText              | InputValue | InputField    | ListValue                                   | FirstName | LastName  | Email                    |
    	| new       | Add a new agreement   | br         | First name    | Brandon Lee (Brandon@terradotta.com)        | Brandon   | Lee       | Brandon@terradotta.com   |
    	| new       | Add a new agreement   | le         | Last name     | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Mitch     | Leventhal | Mitch.Leventhal@suny.edu |
    	| new       | Add a new agreement   | cu         | Email address | Ron Cushing (Ronald.Cushing@uc.edu)         | Ron       | Cushing   | Ronald.Cushing@uc.edu    |
    	| edit      | Agreement, UC 01 test | br         | First name    | Brandon Lee (Brandon@terradotta.com)        | Brandon   | Lee       | Brandon@terradotta.com   |
    	| edit      | Agreement, UC 01 test | le         | Last name     | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Mitch     | Leventhal | Mitch.Leventhal@suny.edu |
    	| edit      | Agreement, UC 01 test | cu         | Email address | Ron Cushing (Ronald.Cushing@uc.edu)         | Ron       | Cushing   | Ronald.Cushing@uc.edu    |

@InstAgrFormsR0313
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully submit with read only person
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	And   I have typed "<ContactTypeValue>" into the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "<ContactTypeValue>" on the Add Contact modal dialog
    And   I have clicked the autocomplete dropdown arrow button for the "ContactType" text box
    And   I have typed "<PersonFieldValue>" into the "<PersonFieldName>" text box on the Add Contact modal dialog
    And   I have seen a "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
	When  I click the "Add Contact" button on the Add Contact modal dialog
    Then  I should not see the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
    And   I should see "<ContactName>" in the Contacts list box on the Institutional Agreement <AddOrEdit> form
    Examples: 
    | AddOrEdit | LinkText              | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                                   | ContactName                       |
    | new       | Add a new agreement   | Partner Principal | mi               | First name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | new       | Add a new agreement   | Partner Secondary | le               | Last name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | new       | Add a new agreement   | Home Principal    | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |
    | edit      | Agreement, UC 01 test | Partner Principal | mi               | First name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | edit      | Agreement, UC 01 test | Partner Secondary | le               | Last name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | edit      | Agreement, UC 01 test | Home Principal    | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |

@InstAgrFormsR0314
Scenario Outline: Institutional Agreement forms Add Contact modal dialog successfully remove contact from list box
    Given I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
	And   I have typed "<ContactTypeValue>" into the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "<ContactTypeValue>" on the Add Contact modal dialog
    And   I have clicked the autocomplete dropdown arrow button for the "ContactType" text box
    And   I have typed "<PersonFieldValue>" into the "<PersonFieldName>" text box on the Add Contact modal dialog
    And   I have seen a "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "Add Contact" button on the Add Contact modal dialog
    And   I have not seen the Add Contact modal dialog on the institutional agreement <AddOrEdit> form
    And   I have seen "<ContactName>" in the Contacts list box on the Institutional Agreement <AddOrEdit> form
    When  I click the Contacts remove icon for "<ContactName>" on the Institutional Agreements <AddOrEdit> form
    Then  I should not see "<ContactName>" in the Contacts list box on the Institutional Agreement <AddOrEdit> form
Examples: 
    | AddOrEdit | LinkText              | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                                   | ContactName                       |
    | new       | Add a new agreement   | Partner Principal | mi               | First name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | new       | Add a new agreement   | Partner Secondary | le               | Last name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | new       | Add a new agreement   | Home Principal    | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |
    | edit      | Agreement, UC 01 test | Partner Principal | mi               | First name      | Mitch Leventhal (Mitch.Leventhal@suny.edu)  | Partner Principal Mitch Leventhal |
    | edit      | Agreement, UC 01 test | Partner Secondary | le               | Last name       | Leventhal, Mitch (Mitch.Leventhal@suny.edu) | Partner Secondary Mitch Leventhal |
    | edit      | Agreement, UC 01 test | Home Principal    | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu)         | Home Principal Ron Cushing        |
    
@InstAgrFormsR0315 @InstAgrFormsResetTestAgreementsUc
Scenario Outline: Institutional Agreement add form successfully store read only Contact in list box after submit
    Given I have clicked the "Add a new agreement" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/new" url
    And   I have typed "Memorandum of Understanding" into the "Agreement type" text box on the Institutional Agreement add form
	And   I have typed "Active" into the "Current status" text box on the Institutional Agreement add form
	And   I have typed "9/1/2011" into the "Start date" text box on the Institutional Agreement add form
	And   I have typed "8/31/2015" into the "Expiration date" text box on the Institutional Agreement add form
	And   I have typed "<Title>" into the "Summary description" text box on the Institutional Agreement add form
    And   I have dismissed all autocomplete dropdowns
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement add form
	And   I have typed "<ContactTypeValue>" into the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "<ContactTypeValue>" on the Add Contact modal dialog
    And   I have clicked the autocomplete dropdown arrow button for the "ContactType" text box
    And   I have typed "<PersonFieldValue>" into the "<PersonFieldName>" text box on the Add Contact modal dialog
    And   I have seen a "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "Add Contact" button on the Add Contact modal dialog
    And   I have not seen the Add Contact modal dialog on the institutional agreement add form
    And   I have seen "<ContactName>" in the Contacts list box on the Institutional Agreement add form
    And   I have successfully submitted the Institutional Agreement add form
    And   I have seen a page at the "institutional-agreements/[PathVar]" url
    And   I have seen a "Edit this agreement" link
    When  I click the "Edit this agreement" link
    Then  I should see a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I should see "<ContactName>" in the Contacts list box on the Institutional Agreement add form
Examples:
    | Title                 | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                           | ContactName                   |
    | Agreement, UC A1 test | Home Principal    | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu) | Home Principal Ron Cushing    |
    | Agreement, UC B1 test | Partner Principal | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu) | Partner Principal Ron Cushing |

@InstAgrFormsR0316 @InstAgrFormsResetTestAgreementsUc
Scenario Outline: Institutional Agreement add form successfully remove Contact from list box after submit
    Given I have clicked the "Add a new agreement" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/new" url
    And   I have typed "Institutional Collaboration Agreement" into the "Agreement type" text box on the Institutional Agreement add form
	And   I have typed "Active" into the "Current status" text box on the Institutional Agreement add form
	And   I have typed "9/1/2011" into the "Start date" text box on the Institutional Agreement add form
	And   I have typed "8/31/2015" into the "Expiration date" text box on the Institutional Agreement add form
	And   I have typed "<Title>" into the "Summary description" text box on the Institutional Agreement add form
    And   I have dismissed all autocomplete dropdowns
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement add form
	And   I have typed "<ContactTypeValue>" into the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "<ContactTypeValue>" on the Add Contact modal dialog
    And   I have clicked the autocomplete dropdown arrow button for the "ContactType" text box
    And   I have typed "<PersonFieldValue>" into the "<PersonFieldName>" text box on the Add Contact modal dialog
    And   I have seen a "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "Add Contact" button on the Add Contact modal dialog
    And   I have not seen the Add Contact modal dialog on the institutional agreement add form
    And   I have seen "<ContactName>" in the Contacts list box on the Institutional Agreement add form
    And   I have successfully submitted the Institutional Agreement add form
    And   I have seen a page at the "institutional-agreements/[PathVar]" url
    And   I have seen a "Edit this agreement" link
    And   I have clicked the "Edit this agreement" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I have seen "<ContactName>" in the Contacts list box on the Institutional Agreement add form 
    When  I click the Contacts remove icon for "<ContactName>" on the Institutional Agreements edit form
    Then  I should not see "<ContactName>" in the Contacts list box on the Institutional Agreement edit form
    And   I should successfully submit the Institutional Agreement edit form
    And   I should see a page at the "institutional-agreements/[PathVar]" url
    And   I should see a "Edit this agreement" link
    And   I should click the "Edit this agreement" link
    And   I should see a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I should not see "<ContactName>" in the Contacts list box on the Institutional Agreement edit form
Examples:
    | Title                 | ContactTypeValue | PersonFieldValue | PersonFieldName | ListValue                                  | ContactName                    |
    | Agreement, UC A2 test | Home Principal   | ro               | Email address   | Ron Cushing (Ronald.Cushing@uc.edu)        | Home Principal Ron Cushing     |
    | Agreement, UC B2 test | Home Principal   | mi               | Email address   | Mitch Leventhal (Mitch.Leventhal@suny.edu) | Home Principal Mitch Leventhal |

@InstAgrFormsR0317
Scenario Outline: Institutional Agreement edit form successfully store read only Contact in list box after submit
    Given I have clicked the "<Title>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement edit form
	And   I have typed "<ContactTypeValue>" into the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "<ContactTypeValue>" on the Add Contact modal dialog
    And   I have clicked the autocomplete dropdown arrow button for the "ContactType" text box
    And   I have typed "<PersonFieldValue>" into the "<PersonFieldName>" text box on the Add Contact modal dialog
    And   I have seen a "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "<PersonFieldName>" autocomplete dropdown menu item "<ListValue>" on the Add Contact modal dialog
    And   I have clicked the "Add Contact" button on the Add Contact modal dialog
    And   I have not seen the Add Contact modal dialog on the institutional agreement edit form
    And   I have seen "<ContactName>" in the Contacts list box on the Institutional Agreement edit form
    And   I have successfully submitted the Institutional Agreement edit form
    And   I have seen a page at the "institutional-agreements/[PathVar]" url
    And   I have seen a "Edit this agreement" link
    When  I click the "Edit this agreement" link
    Then  I should see a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I should see "<ContactName>" in the Contacts list box on the Institutional Agreement edit form
Examples:
    | Title                 | ContactTypeValue  | PersonFieldValue | PersonFieldName | ListValue                            | ContactName                    |
    | Agreement, UC 01 test | Partner Principal | mary             | First name      | Mary Watkins (Mary.Watkins@uc.edu)   | Partner Principal Mary Watkins |
    | Agreement, UC 01 test | Partner Secondary | watk             | Last name       | Watkins, Mary (Mary.Watkins@uc.edu)  | Partner Secondary Mary Watkins |
    | Agreement, UC 01 test | Home Principal    | br               | Email address   | Brandon Lee (Brandon@terradotta.com) | Home Principal Brandon Lee     |

@InstAgrFormsR0318 @InstAgrFormsFreshTestAgreementUc02
Scenario Outline: Institutional Agreement edit form successfully remove Contact from list box after submit
    Given I am using the <BrowserName> browser
    And   I have clicked the "Agreement, UC 02 test" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I have seen a "Add Contact" link
    And   I have clicked the "Add Contact" link
	And   I have seen the Add Contact modal dialog on the institutional agreement edit form
	And   I have typed "Home Principal" into the "Contact type" text box on the Add Contact modal dialog
    And   I have seen a "Contact type" autocomplete dropdown menu item "Home Principal" on the Add Contact modal dialog
    And   I have clicked the autocomplete dropdown arrow button for the "ContactType" text box
    And   I have typed "ro" into the "Email address" text box on the Add Contact modal dialog
    And   I have seen a "Email address" autocomplete dropdown menu item "Ron Cushing (Ronald.Cushing@uc.edu)" on the Add Contact modal dialog
    And   I have clicked the "Email address" autocomplete dropdown menu item "Ron Cushing (Ronald.Cushing@uc.edu)" on the Add Contact modal dialog
    And   I have clicked the "Add Contact" button on the Add Contact modal dialog
    And   I have not seen the Add Contact modal dialog on the institutional agreement edit form
    And   I have seen "Home Principal Ron Cushing" in the Contacts list box on the Institutional Agreement edit form
    And   I have successfully submitted the Institutional Agreement edit form
    And   I have seen a page at the "institutional-agreements/[PathVar]" url
    And   I have seen a "Edit this agreement" link
    And   I have clicked the "Edit this agreement" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I have seen "Home Principal Ron Cushing" in the Contacts list box on the Institutional Agreement edit form 
    When  I click the Contacts remove icon for "Home Principal Ron Cushing" on the Institutional Agreements edit form
    Then  I should not see "Home Principal Ron Cushing" in the Contacts list box on the Institutional Agreement edit form
    And   I should successfully submit the Institutional Agreement edit form
    And   I should see a page at the "institutional-agreements/[PathVar]" url
    And   I should see a "Edit this agreement" link
    And   I should click the "Edit this agreement" link
    And   I should see a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I should not see "Home Principal Ron Cushing" in the Contacts list box on the Institutional Agreement edit form
Examples:
    | BrowserName       |
    | Chrome            |
    | Firefox           |
    | Internet Explorer |
