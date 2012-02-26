@Self
@SelfR01
Feature:  User Self Management
	      In order to control what UCosmic says about me
	      As a UCosmic user
	      I want to manage my UCosmic profile

#execute these steps before every scenario in this file
Background: 
    Given I have signed in as "any1@usil.edu.pe" with password "asdfasdf"
    And   I have browsed to the "my/profile" url

@SelfName
@SelfR0101
Scenario: My Name form save changes unsuccessfully with invalid input
	Given I have unchecked the DisplayName automatic generation checkbox in the My Name form
	When  I type "" into the "DisplayName" text box on the My Name form
	And   I click the "Save Changes" button on the My Name form
    Then  I should see a page at the "my/profile" url
	And   I should see the error message "You must have a Display name." for the "DisplayName" text box on the My Name form
    And   I should see "" in the "DisplayName" text box on the My Name form

@SelfName
@SelfR0102
Scenario Outline: My Name form save changes successfully with valid input
    Given I am using the <BrowserName> browser
	And   I have unchecked the DisplayName automatic generation checkbox in the My Name form
	When  I type "<DisplayName>" into the "DisplayName" text box on the My Name form
	And   I type "<Salutation>" into the "Salutation" text box on the My Name form
	And   I type "<FirstName>" into the "FirstName" text box on the My Name form
	And   I type "<MiddleName>" into the "MiddleName" text box on the My Name form
	And   I type "<LastName>" into the "LastName" text box on the My Name form
	And   I type "<Suffix>" into the "Suffix" text box on the My Name form
	And   I successfully submit the My Name form
	Then  I should see a page at the "my/profile" url
	And   I should see top feedback message "Your personal info was saved successfully."
    And   I should see "<DisplayName>" in the "DisplayName" text box on the My Name form
    And   I should see "<Salutation>" in the "Salutation" text box on the My Name form
    And   I should see "<FirstName>" in the "FirstName" text box on the My Name form
    And   I should see "<MiddleName>" in the "MiddleName" text box on the My Name form
    And   I should see "<LastName>" in the "LastName" text box on the My Name form
    And   I should see "<Suffix>" in the "Suffix" text box on the My Name form
    Examples:
    | BrowserName       | DisplayName      | Salutation        | FirstName      | MiddleName      | LastName      | Suffix        |
    | Chrome            | Test DisplayName | Dr.               | TestFirstName1 | TestMiddleName1 | TestLastName1 | Jr.           |
    | Chrome            | Test DisplayName | Prof.             | TestFirstName2 | TestMiddleName2 | TestLastName2 | PhD           |
    | Chrome            | Test DisplayName | Custom Salutation | TestFirstName3 | TestMiddleName3 | TestLastName3 | Custom Suffix |
    | Chrome            | Test DisplayName |                   | TestFirstName4 |                 | TestLastName4 |               |
    | Chrome            | Test DisplayName |                   |                |                 |               |               |
    | Chrome            | Test DisplayName | Salutation        |                |                 |               |               |
    | Chrome            | Test DisplayName |                   | TestFirstName  |                 |               |               |
    | Chrome            | Test DisplayName |                   |                | TestMiddleName  |               |               |
    | Chrome            | Test DisplayName |                   |                |                 | TestLastName  |               |
    | Chrome            | Test DisplayName |                   |                |                 |               | TestSuffix    |
    | Chrome            | Any One          |                   | Any            |                 | One           |               |
    | Firefox           | Test DisplayName | Dr.               | TestFirstName1 | TestMiddleName1 | TestLastName1 | Jr.           |
    | Firefox           | Test DisplayName | Prof.             | TestFirstName2 | TestMiddleName2 | TestLastName2 | PhD           |
    | Firefox           | Test DisplayName | Custom Salutation | TestFirstName3 | TestMiddleName3 | TestLastName3 | Custom Suffix |
    | Firefox           | Test DisplayName |                   | TestFirstName4 |                 | TestLastName4 |               |
    | Firefox           | Test DisplayName |                   |                |                 |               |               |
    | Firefox           | Test DisplayName | Salutation        |                |                 |               |               |
    | Firefox           | Test DisplayName |                   | TestFirstName  |                 |               |               |
    | Firefox           | Test DisplayName |                   |                | TestMiddleName  |               |               |
    | Firefox           | Test DisplayName |                   |                |                 | TestLastName  |               |
    | Firefox           | Test DisplayName |                   |                |                 |               | TestSuffix    |
    | Firefox           | Any One          |                   | Any            |                 | One           |               |
    | Internet Explorer | Test DisplayName | Dr.               | TestFirstName1 | TestMiddleName1 | TestLastName1 | Jr.           |
    | Internet Explorer | Test DisplayName | Prof.             | TestFirstName2 | TestMiddleName2 | TestLastName2 | PhD           |
    | Internet Explorer | Test DisplayName | Custom Salutation | TestFirstName3 | TestMiddleName3 | TestLastName3 | Custom Suffix |
    | Internet Explorer | Test DisplayName |                   | TestFirstName4 |                 | TestLastName4 |               |
    | Internet Explorer | Test DisplayName |                   |                |                 |               |               |
    | Internet Explorer | Test DisplayName | Salutation        |                |                 |               |               |
    | Internet Explorer | Test DisplayName |                   | TestFirstName  |                 |               |               |
    | Internet Explorer | Test DisplayName |                   |                | TestMiddleName  |               |               |
    | Internet Explorer | Test DisplayName |                   |                |                 | TestLastName  |               |
    | Internet Explorer | Test DisplayName |                   |                |                 |               | TestSuffix    |
    | Internet Explorer | Any One          |                   | Any            |                 | One           |               |

@SelfName
@SelfR0103
Scenario Outline: My Name form automatically generate Display Name text box
	Given I have unchecked the DisplayName automatic generation checkbox in the My Name form
	When  I type "<UserEnteredDisplayName>" into the "DisplayName" text box on the My Name form
	And   I type "<Salutation>" into the "Salutation" text box on the My Name form
	And   I type "<FirstName>" into the "FirstName" text box on the My Name form
	And   I type "<MiddleName>" into the "MiddleName" text box on the My Name form
	And   I type "<LastName>" into the "LastName" text box on the My Name form
	And   I type "<Suffix>" into the "Suffix" text box on the My Name form
	And   I <CheckOrUncheck> the DisplayName automatic generation checkbox in the My Name form
	Then  I should see "<AutoGeneratedDisplayName>" in the "DisplayName" text box on the My Name form
    Examples:
    | UserEnteredDisplayName | Salutation | FirstName | MiddleName | LastName | Suffix | CheckOrUncheck | AutoGeneratedDisplayName |
    | Test DisplayName       | Dr.        | First1    | M.         | Last1    | PhD    | check          | Dr. First1 M. Last1 PhD  |
    | Test DisplayName       |            |           |            |          |        | check          |                          |
    | Test DisplayName       | Dr.        | First3    | M.         | Last3    | PhD    | uncheck        | Test DisplayName         |
    | Test DisplayName       |            |           |            |          |        | uncheck        | Test DisplayName         |
    | Test DisplayName       | Dr.        |           |            |          |        | check          | Dr.                      |
    | Test DisplayName       |            | Any       |            |          |        | check          | Any                      |
    | Test DisplayName       |            |           | Single     |          |        | check          | Single                   |
    | Test DisplayName       |            |           |            | One      |        | check          | One                      |
    | Test DisplayName       |            |           |            |          | PhD    | check          | PhD                      |
    | Test DisplayName       | Dr.        | Any       | Single     | One      | PhD    | check          | Dr. Any Single One PhD   |

@SelfName
@SelfR0104
Scenario Outline: My Name form revert from automatically generated to user entered Display Name
	Given I have unchecked the DisplayName automatic generation checkbox in the My Name form
	When  I type "<UserEnteredDisplayName>" into the "DisplayName" text box on the My Name form
	And   I type "<Salutation>" into the "Salutation" text box on the My Name form
	And   I type "<FirstName>" into the "FirstName" text box on the My Name form
	And   I type "<MiddleName>" into the "MiddleName" text box on the My Name form
	And   I type "<LastName>" into the "LastName" text box on the My Name form
	And   I type "<Suffix>" into the "Suffix" text box on the My Name form
	And   I check the DisplayName automatic generation checkbox in the My Name form
    And   I see "<AutoGeneratedDisplayName>" in the "DisplayName" text box on the My Name form
    And   I uncheck the DisplayName automatic generation checkbox in the My Name form
	Then  I should see "<UserEnteredDisplayName>" in the "DisplayName" text box on the My Name form
    Examples:
    | UserEnteredDisplayName | Salutation | FirstName | MiddleName | LastName | Suffix | AutoGeneratedDisplayName |
    | Test DisplayName       | Dr.        | First1    | M.         | Last1    | PhD    | Dr. First1 M. Last1 PhD  |
    | Test DisplayName       |            |           |            |          |        |                          |
    | Test DisplayName       | Dr.        |           |            |          |        | Dr.                      |
    | Test DisplayName       |            | Any       |            |          |        | Any                      |
    | Test DisplayName       |            |           | Single     |          |        | Single                   |
    | Test DisplayName       |            |           |            | One      |        | One                      |
    | Test DisplayName       |            |           |            |          | PhD    | PhD                      |
    | Test DisplayName       | Dr.        | Any       | Single     | One      | PhD    | Dr. Any Single One PhD   |

@SelfName
@SelfR0105
Scenario Outline: My Name form successfully display autocomplete example after clicking dropdown arrow button
When  I click the autocomplete dropdown arrow for the "<FieldName>" text box
Then  I should see a "<FieldName>" autocomplete dropdown menu item "<ListValue>"
Examples: 
    | FieldName  | ListValue |
    | Salutation | [None]    |
    | Salutation | Dr.       |
    | Salutation | Mr.       |
    | Salutation | Mrs.      |
    | Salutation | Prof.     |
    | Suffix     | [None]    |
    | Suffix     | Esq.      |
    | Suffix     | Jr.       |
    | Suffix     | PhD       |
    | Suffix     | Sr.       |

@SelfName
@SelfR0106
Scenario Outline: My Name form successfully display autocomplete example after typing into text box
When  I type "<InputValue>" into the "<FieldName>" text box on the My Name form
Then  I should <SeeOrNot> a "<FieldName>" autocomplete dropdown menu item "<ListValue>"
Examples: 
    | FieldName  | InputValue | SeeOrNot | ListValue |
    | Salutation | [          | not see  | [None]    |
    | Salutation | D          | see      | Dr.       |
    | Salutation | M          | see      | Mr.       |
    | Salutation | M          | see      | Mrs.      |
    | Salutation | Mrs        | not see  | Mr.       |
    | Salutation | P          | see      | Prof.     |
    | Suffix     | [          | not see  | [None]    |
    | Suffix     | E          | see      | Esq.      |
    | Suffix     | J          | see      | Jr.       |
    | Suffix     | P          | see      | PhD       |
    | Suffix     | S          | see      | Sr.       |

@SelfName
@SelfR0107
Scenario Outline: My Name form successfully choose autocomplete item from dropdown menu
Given I have clicked the autocomplete dropdown arrow for the "<FieldName>" text box
And   I have seen a "<FieldName>" autocomplete dropdown menu item "<ListValue>"
When  I click the "<FieldName>" autocomplete dropdown menu item "<ListValue>"
Then  I should see "<InputValue>" in the "<FieldName>" text box on the My Name form
Examples: 
    | FieldName  | ListValue | InputValue |
    | Salutation | [None]    |            |
    | Salutation | Dr.       | Dr.        |
    | Salutation | Mr.       | Mr.        |
    | Salutation | Mrs.      | Mrs.       |
    | Salutation | Prof.     | Prof.      |
    | Suffix     | [None]    |            |
    | Suffix     | Esq.      | Esq.       |
    | Suffix     | Jr.       | Jr.        |
    | Suffix     | PhD       | PhD        |
    | Suffix     | Sr.       | Sr.        |

@SelfAffiliation
@SelfR0108
Scenario Outline: My Affiliation Info form successfully save changes with valid input
    When  I click either the "Set up affiliation" or "Edit affiliation info" link
    And   I see a page at the "me/affiliations/[PathVar]/edit.html" url
    And   I click the radio button for "<AffiliationType>" on the Affiliation Info form
    And   I <SeeOrNot> additional employee fields on the Affiliation Info form
    And   I <TypeOrNot> "<JobTitle>" into the "Job Titles & Departments" text box on the Affiliation Info form
    And   I click the "Save Changes" button on the Affiliation Info form
	Then  I should see a page at the "my/profile" url
	And   I should see top feedback message "Your affiliation info was saved successfully."
    And   I should see "<AffiliationInfo>" Affiliation Info text on the My Affiliations section of the About Me url
    Examples:
    | AffiliationType                         | SeeOrNot  | TypeOrNot  | JobTitle             | AffiliationInfo              |
    | I am an employee.                       | see       | type       |                      | [Job Title(s) Unknown]       |
    | I am a student.                         | don't see | don't type |                      | Student                      |
    | I am both an employee and a student.    | see       | type       |                      | [Job Title(s) Unknown]       |
    | I am neither an employee nor a student. | don't see | don't type |                      | Neither student nor employee |
    | I am an employee.                       | see       | type       |                      | [Job Title(s) Unknown]       |
    | I am a student.                         | don't see | don't type |                      | Student                      |
    | I am both an employee and a student.    | see       | type       |                      | [Job Title(s) Unknown]       |
    | I am neither an employee nor a student. | don't see | don't type |                      | Neither student nor employee |
    | I am an employee.                       | see       | type       | Teaching Assistant   | Teaching Assistant           |
    | I am an employee.                       | see       | type       | Applications Analyst | Applications Analyst         |
    | I am both an employee and a student.    | see       | type       | Teaching Assistant   | Teaching Assistant           |
    | I am both an employee and a student.    | see       | type       | Applications Analyst | Applications Analyst         |

@SelfAffiliation
@SelfR0109
Scenario Outline: My Affiliation Info form confirm employee categories are checked or unchecked after successful save
    Given I have clicked either the "Set up affiliation" or "Edit affiliation info" link
    And   I have seen a page at the "me/affiliations/[PathVar]/edit.html" url
    And   I have clicked the radio button for "I am an employee." on the Affiliation Info form
    And   I have <CheckOrNot1> the "<EmployeeCategory1>" checkbox on the Affiliation Info form
    And   I have <CheckOrNot2> the "<EmployeeCategory2>" checkbox on the Affiliation Info form 
    And   I have clicked the "Save Changes" button on the Affiliation Info form
	And   I have seen a page at the "my/profile" url
	And   I have seen top feedback message "Your affiliation info was saved successfully."
    When  I click the "Edit affiliation info" link
    And   I see a page at the "me/affiliations/[PathVar]/edit.html" url 
    Then  I should see the "<EmployeeCategory1>" checkbox is <CheckOrNot1> on the Affiliation Info form 
    And   I should see the "<EmployeeCategory2>" checkbox is <CheckOrNot2> on the Affiliation Info form
    Examples:
    | CheckOrNot1 | EmployeeCategory1                                  | CheckOrNot2 | EmployeeCategory2      |
    | checked     | I am an administrator.                             | checked     | I am a staff employee. |
    | unchecked   | I am an administrator.                             | unchecked   | I am a staff employee. |
    | checked     | I am employed in the international affairs office. | checked     | I am a faculty member. |
    | unchecked   | I am employed in the international affairs office. | unchecked   | I am a faculty member. |
                                                                                                                            
