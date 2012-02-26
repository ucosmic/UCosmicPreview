@InstAgrConfig
@InstAgrConfigR04
Feature:  Configure Institutional Agreements Module
		  In order to manage and restrict all the options for agreement types, current statuses and contact types for my Institutional Agreements
		  As an Institutional Agreement Manager
		  I want to add, remove, and generally manage the options attached to my Institutional Agreements in UCosmic

@InstAgrConfigR0401
Scenario: Institutional Agreement Module Configuration form unsuccessfully add with empty required fields in Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement types listbox 
    When  I click the add link for option 1 in the Agreement types listbox
    And   I see a textbox for option "1" editor in the Agreement types listbox
    And   I click the add link for option 1 in the Agreement types listbox
    Then  I should see the error message "Please enter an Agreement type option." for option "1" in the Agreement types listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0402
Scenario Outline: Institutional Agreement Module Configuration unsuccessfully saves with empty required fields in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Agreement types listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Agreement types listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement types listbox
    And   I type "" into the textbox for option "<OptionValue>" in the Agreement types listbox
    And   I see a Save link for option "<OptionValue>" editor in the Agreement types listbox 
    And   I click the Save link for option "<OptionValue>" editor in the Agreement types listbox
    Then  I should see the error message "Please enter an Agreement type option." for option "<OptionValue>" in the Agreement types listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue |
   | 2           |
   | 3           |
   | 4           |

@InstAgrConfigR0403
Scenario Outline: Institutional Agreement Module Configuration form successfully removes the text boxes in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen a Remove link for option "<OptionValue>" info in the Agreement types listbox 
    When  I click the Remove link for option "<OptionValue>" info in the Agreement types listbox
    Then  I should <SeeOrNot> a textbox for option "<OptionValue>" info in the Agreement types listbox
Examples: 
  | SeeOrNot | OptionValue |
  | not see  | 2           |
  | not see  | 3           |
  | not see  | 4           |

@InstAgrConfigR0404
Scenario Outline: Institutional Agreement Module Configuration form reverts the changes to default settings as Activity Agreement in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Agreement types listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Agreement types listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement types listbox
    And   I type "Agreement" into the textbox for option "<OptionValue>" in the Agreement types listbox
    And   I see a Cancel link for option "<OptionValue>" editor in the Agreement types listbox 
    And   I click the Cancel link for option "<OptionValue>" editor in the Agreement types listbox
    Then  I should see a text "<Text>" for option "<OptionValue>" info in the Agreement types listbox
Examples: 
   | OptionValue | Text                                  |
   | 2           | Activity Agreement                    |
   | 3           | Institutional Collaboration Agreement |
   | 4           | Memorandum of Understanding           |

@InstAgrConfigR0405
Scenario Outline: Institutional Agreement Module Configuration form saves the changes for the Activity Agreement in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Agreement types listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Agreement types listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement types listbox
    And   I type "<Text>" into the textbox for option "<OptionValue>" in the Agreement types listbox
    And   I see a Save link for option "<OptionValue>" editor in the Agreement types listbox 
    And   I click the Save link for option "<OptionValue>" editor in the Agreement types listbox
    Then  I should see a text "<Text>" for option "<OptionValue>" info in the Agreement types listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue | Text                                  |
   | 2           | Activity Agreement                    |
   | 3           | Institutional Collaboration Agreement |
   | 4           | Memorandum of Understanding           |

@InstAgrConfigR0406
Scenario Outline: Institutional Agreement Module Configuration form do not allow duplicate options in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement types listbox 
    When  I click the add link for option 1 in the Agreement types listbox
    And   I see a textbox for option "1" editor in the Agreement types listbox
    And   I type "<Text>" into the textbox for option "1" in the Agreement types listbox
    And   I click the add link for option 1 in the Agreement types listbox
    Then  I should see the error message "The option 'Activity Agreement' already exists. Please do not add any duplicate options." for option "1" in the Agreement types listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
  | Text               |
  | Activity Agreement |

@InstAgrConfigR0407
Scenario Outline: Institutional Agreement Module Configuration form add a new option in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement types listbox 
    When  I click the add link for option 1 in the Agreement types listbox
    And   I see a textbox for option "1" editor in the Agreement types listbox
    And   I type "<Text>" into the textbox for option "1" in the Agreement types listbox
    And   I click the add link for option 1 in the Agreement types listbox
    Then  I should see a text "<Text>" for option "3" info in the Agreement types listbox
    And   I should see a Remove link for option "3" info in the Agreement types listbox
    And   I should click the Remove link for option "3" info in the Agreement types listbox
    And   I should not see a textbox for option "3" info in the Agreement types listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | Text                        |
   | An Agreement                |
   | Collaboration Agreement     |

@InstAgrConfigR0408
Scenario: Institutional Agreement Module Configuration form does not allow user to add a new Agreement type
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen a radio button "IsCustomTypeAllowed_False" to decide access to the users to enter any text for Agreement type form
    When  I click the radio button "IsCustomTypeAllowed_False" to decide access to the users to enter any text for Agreement type form
    Then  I should see a read-only text box "ExampleTypeInput" for Agreement type form
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0409
Scenario: Institutional Agreement Module Configuration form successfully removes option 4 in the Agreement types listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "4" info in the Agreement types listbox
    And   I have clicked the Edit link for option "4" info in the Agreement types listbox
    And   I have seen a textbox for option "4" editor in the Agreement types listbox
    And   I have typed "Activity Agreement" into the textbox for option "4" in the Agreement types listbox
    And   I have clicked the Save link for option "4" editor in the Agreement types listbox
    And   I have seen the error message "The option 'Activity Agreement' already exists. Please do not add any duplicate options." for option "4" in the Agreement types listbox
    And   I have seen a Cancel link for option "4" editor in the Agreement types listbox
    And   I have clicked the Cancel link for option "4" editor in the Agreement types listbox
    And   I have seen an Edit link for option "4" info in the Agreement types listbox
    And   I have clicked the Edit link for option "4" info in the Agreement types listbox
    And   I have seen a textbox for option "4" editor in the Agreement types listbox
    And   I have typed "Memorandum of Understanding" into the textbox for option "4" in the Agreement types listbox
    And   I have seen a Save link for option "4" editor in the Agreement types listbox
    When  I click the Save link for option "4" editor in the Agreement types listbox
    And   I see a text "Memorandum of Understanding" for option "4" info in the Agreement types listbox
    And   I see a Remove link for option "4" info in the Agreement types listbox
    And   I click the Remove link for option "4" info in the Agreement types listbox
    Then  I should not see a textbox for option "4" info in the Agreement types listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
    
@InstAgrConfigR0410
Scenario: Institutional Agreement Module Configuration form unsuccessfully adds with empty required fields in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Current statuses listbox 
    When  I click the add link for option 1 in the Current statuses listbox
    And   I see a textbox for option "1" editor in the Current statuses listbox
    And   I click the add link for option 1 in the Current statuses listbox
    Then  I should see the error message "Please enter a Agreement status option." for option "1" in the Current statuses listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0411
Scenario Outline: Institutional Agreement Module Configuration unsuccessfully saves with empty required fields in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Current statuses listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Current statuses listbox
    And   I see a textbox for option "<OptionValue>" editor in the Current statuses listbox
    And   I type "" into the textbox for option "<OptionValue>" in the Current statuses listbox
    And   I see a Save link for option "<OptionValue>" editor in the Current statuses listbox 
    And   I click the Save link for option "<OptionValue>" editor in the Current statuses listbox
    Then  I should see the error message "Please enter a Agreement status option." for option "<OptionValue>" in the Current statuses listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue |
   | 2           |
   | 3           |
   | 4           |
   | 5           |

@InstAgrConfigR0412
Scenario Outline: Institutional Agreement Module Configuration form successfully removes the text boxes in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link 
    And   I have seen a "Click here to set it up now" link
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen a Remove link for option "<OptionValue>" info in the Current statuses listbox 
    When  I click the Remove link for option "<OptionValue>" info in the Current statuses listbox
    Then  I should <SeeOrNot> a textbox for option "<OptionValue>" info in the Current statuses listbox
Examples: 
  | SeeOrNot | OptionValue |
  | not see  | 2           |
  | not see  | 3           |
  | not see  | 4           |
  | not see  | 5           |

@InstAgrConfigR0413
Scenario Outline: Institutional Agreement Module Configuration form reverts the changes to default settings in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Current statuses listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Current statuses listbox
    And   I see a textbox for option "<OptionValue>" editor in the Current statuses listbox
    And   I type "Agreement" into the textbox for option "<OptionValue>" in the Current statuses listbox
    And   I see a Cancel link for option "<OptionValue>" editor in the Current statuses listbox 
    And   I click a Cancel link for option "<OptionValue>" editor in the Current statuses listbox
    Then  I should see a text "<Text>" for option "<OptionValue>" editor in the Current statuses listbox
Examples: 
   | OptionValue | Text     |
   | 2           | Active   |
   | 3           | Dead     |
   | 4           | Inactive |
   | 5           | Unknown  |
   
@InstAgrConfigR0414
Scenario Outline: Institutional Agreement Module Configuration form saves the changes in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Current statuses listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Current statuses listbox
    And   I see a textbox for option "<OptionValue>" editor in the Current statuses listbox
    And   I type "<Text>" into the textbox for option "<OptionValue>" in the Current statuses listbox
    And   I see a Save link for option "<OptionValue>" editor in the Current statuses listbox 
    And   I click the Save link for option "<OptionValue>" editor in the Current statuses listbox
    Then  I should see a text "<Text>" for option "<OptionValue>" info in the Current statuses listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue | Text     |
   | 2           | Active   |
   | 3           | Dead     |
   | 4           | Inactive |
   | 5           | Unknown  |

@InstAgrConfigR0415
Scenario Outline: Institutional Agreement Module Configuration form do not allow duplicate options in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Current statuses listbox 
    When  I click the add link for option 1 in the Current statuses listbox
    And   I see a textbox for option "<OptionValue>" editor in the Current statuses listbox
    And   I type "<Text>" into the textbox for option "<OptionValue>" in the Current statuses listbox
    And   I click the add link for option 1 in the Current statuses listbox
    Then  I should see the error message "The option 'Active' already exists. Please do not add any duplicate options." for option "<OptionValue>" in the Current statuses listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue | Text   |
   | 1           | Active |

@InstAgrConfigR0416
Scenario Outline: Institutional Agreement Module Configuration form add a new option in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Current statuses listbox 
    When  I click the add link for option 1 in the Current statuses listbox
    And   I see a textbox for option "1" editor in the Current statuses listbox
    And   I type "<Text>" into the textbox for option "1" in the Current statuses listbox
    And   I click the add link for option 1 in the Current statuses listbox
    Then  I should see a text "<Text>" for option "3" info in the Current statuses listbox
    And   I should see a Remove link for option "3" info in the Current statuses listbox
    And   I should click the Remove link for option "3" info in the Current statuses listbox
    And   I should not see a textbox for option "3" info in the Current statuses listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | Text                        |
   | An Agreement                |
   | Collaboration Agreement     |

@InstAgrConfigR0417
Scenario: Institutional Agreement Module Configuration form does not allow user to add a new Current status
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen a radio button "IsCustomStatusAllowed_False" to decide access to the users to enter any text for Current statuses form
    When  I click the radio button "IsCustomStatusAllowed_False" to decide access to the users to enter any text for Current statuses form
    Then  I should see a read-only text box "ExampleStatusInput" for Current statuses form
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0418
Scenario: Institutional Agreement Module Configuration form successfully removes option 3 in the Current statuses listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "3" info in the Current statuses listbox
    And   I have clicked the Edit link for option "3" info in the Current statuses listbox
    And   I have seen a textbox for option "3" editor in the Current statuses listbox
    And   I have typed "Active" into the textbox for option "3" in the Current statuses listbox
    And   I have clicked the Save link for option "3" editor in the Current statuses listbox
    And   I have seen the error message "The option 'Active' already exists. Please do not add any duplicate options." for option "3" in the Current statuses listbox
    And   I have seen a Cancel link for option "3" editor in the Current statuses listbox
    And   I have clicked the Cancel link for option "3" editor in the Current statuses listbox
    And   I have seen an Edit link for option "3" info in the Current statuses listbox
    And   I have clicked the Edit link for option "3" info in the Current statuses listbox
    And   I have seen a textbox for option "3" editor in the Current statuses listbox
    And   I have typed "Dead" into the textbox for option "3" in the Current statuses listbox
    And   I have seen a Save link for option "3" editor in the Current statuses listbox
    When  I click the Save link for option "3" editor in the Current statuses listbox
    And   I see a text "Dead" for option "3" info in the Current statuses listbox
    And   I see a Remove link for option "3" info in the Current statuses listbox
    And   I click the Remove link for option "3" info in the Current statuses listbox
    Then  I should not see a textbox for option "3" info in the Current statuses listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0419
Scenario: Institutional Agreement Module Configuration form unsuccessfully adds with empty required fields in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement Contact type listbox 
    When  I click the add link for option 1 in the Agreement Contact type listbox
    And   I see a textbox for option "1" editor in the Agreement Contact type listbox
    And   I click the add link for option 1 in the Agreement Contact type listbox
    Then  I should see the error message "Please enter a Contact type option." for option "1" in the Agreement Contact type listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0420
Scenario Outline: Institutional Agreement Module Configuration unsuccessfully saves with empty required fields in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Agreement Contact type listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Agreement Contact type listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement Contact type listbox
    And   I type "" into the textbox for option "<OptionValue>" in the Agreement Contact type listbox
    And   I see a Save link for option "<OptionValue>" editor in the Agreement Contact type listbox 
    And   I click the Save link for option "<OptionValue>" editor in the Agreement Contact type listbox
    Then  I should see the error message "Please enter a Contact type option." for option "<OptionValue>" in the Agreement Contact type listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue |
   | 2           |
   | 3           |
   | 4           |

@InstAgrConfigR0421
Scenario Outline: Institutional Agreement Module Configuration form successfully removes the text boxes in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen a Remove link for option "<OptionValue>" info in the Agreement Contact type listbox 
    When  I click the Remove link for option "<OptionValue>" info in the Agreement Contact type listbox
    Then  I should <SeeOrNot> a textbox for option "<OptionValue>" info in the Agreement Contact type listbox
Examples: 
  | SeeOrNot | OptionValue |
  | not see  | 2           |
  | not see  | 3           |
  | not see  | 4           |
  | not see  | 5           |

@InstAgrConfigR0422
Scenario Outline: Institutional Agreement Module Configuration form reverts the changes to default settings in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Agreement Contact type listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Agreement Contact type listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement Contact type listbox
    And   I type "Agreement" into the textbox for option "<OptionValue>" in the Agreement Contact type listbox
    And   I see a Cancel link for option "<OptionValue>" editor in the Agreement Contact type listbox 
    And   I click the Cancel link for option "<OptionValue>" editor in the Agreement Contact type listbox
    Then  I should see a text "<Text>" for option "<OptionValue>" info in the Agreement Contact type listbox
Examples: 
   | OptionValue | Text              |
   | 2           | Home Principal    |
   | 3           | Home Secondary    |
   | 4           | Partner Principal |
   | 5           | Partner Secondary |

@InstAgrConfigR0423
Scenario Outline: Institutional Agreement Module Configuration form saves the changes in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "<OptionValue>" info in the Agreement Contact type listbox 
    When  I click the Edit link for option "<OptionValue>" info in the Agreement Contact type listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement Contact type listbox
    And   I type "<Text>" into the textbox for option "<OptionValue>" in the Agreement Contact type listbox
    And   I see a Save link for option "<OptionValue>" editor in the Agreement Contact type listbox 
    And   I click the Save link for option "<OptionValue>" editor in the Agreement Contact type listbox
    Then  I should see a text "<Text>" for option "<OptionValue>" info in the Agreement Contact type listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue | Text              |
   | 2           | Home Principal    |
   | 3           | Home Secondary    |
   | 4           | Partner Principal |
   | 5           | Partner Secondary |

@InstAgrConfigR0424
Scenario Outline: Institutional Agreement Module Configuration form do not allow duplicate options in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement Contact type listbox 
    When  I click the add link for option 1 in the Agreement Contact type listbox
    And   I see a textbox for option "<OptionValue>" editor in the Agreement Contact type listbox
    And   I type "<Text>" into the textbox for option "<OptionValue>" in the Agreement Contact type listbox
    And   I click the add link for option 1 in the Agreement Contact type listbox
    Then  I should see the error message "The option 'Home Principal' already exists. Please do not add any duplicate options." for option "<OptionValue>" in the Agreement Contact type listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | OptionValue | Text           |
   | 1           | Home Principal |

@InstAgrConfigR0425
Scenario Outline: Institutional Agreement Module Configuration form add a new option in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement Contact type listbox 
    When  I click the add link for option 1 in the Agreement Contact type listbox
    And   I see a textbox for option "1" editor in the Agreement Contact type listbox
    And   I type "<Text>" into the textbox for option "1" in the Agreement Contact type listbox
    And   I click the add link for option 1 in the Agreement Contact type listbox
    Then  I should see a text "<Text>" for option "3" info in the Agreement Contact type listbox
    And   I should see a Remove link for option "3" info in the Agreement Contact type listbox
    And   I should click the Remove link for option "3" info in the Agreement Contact type listbox
    And   I should not see a textbox for option "3" info in the Agreement Contact type listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url
Examples: 
   | Text                        |
   | An Agreement                |
   | Collaboration Agreement     |

@InstAgrConfigR0426
Scenario: Institutional Agreement Module Configuration form does not allow user to add a new Agreement Contact type
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen a radio button "IsCustomContactTypeAllowed_False" to decide access to the users to enter any text for Agreement Contact type form
    When  I click the radio button "IsCustomContactTypeAllowed_False" to decide access to the users to enter any text for Agreement Contact type form
    Then  I should see a read-only text box "ExampleContactTypeInput" for Agreement Contact type form
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0427
Scenario: Institutional Agreement Module Configuration form successfully removes option 4 in the Agreement Contact type listbox
    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an Edit link for option "4" info in the Agreement Contact type listbox
    And   I have clicked the Edit link for option "4" info in the Agreement Contact type listbox
    And   I have seen a textbox for option "4" editor in the Agreement Contact type listbox
    And   I have typed "Home Principal" into the textbox for option "4" in the Agreement Contact type listbox
    And   I have clicked the Save link for option "4" editor in the Agreement Contact type listbox
    And   I have seen the error message "The option 'Home Principal' already exists. Please do not add any duplicate options." for option "4" in the Agreement Contact type listbox
    And   I have seen a Cancel link for option "4" editor in the Agreement Contact type listbox
    And   I have clicked the Cancel link for option "4" editor in the Agreement Contact type listbox
    And   I have seen an Edit link for option "4" info in the Agreement Contact type listbox
    And   I have clicked the Edit link for option "4" info in the Agreement Contact type listbox
    And   I have seen a textbox for option "4" editor in the Agreement Contact type listbox
    And   I have typed "Partner Principal" into the textbox for option "4" in the Agreement Contact type listbox
    And   I have seen a Save link for option "4" editor in the Agreement Contact type listbox
    When  I click the Save link for option "4" editor in the Agreement Contact type listbox
    And   I see a text "Partner Principal" for option "4" info in the Agreement Contact type listbox
    And   I see a Remove link for option "4" info in the Agreement Contact type listbox
    And   I click the Remove link for option "4" info in the Agreement Contact type listbox
    Then  I should not see a textbox for option "4" info in the Agreement Contact type listbox
    And   I should see a page at the "my/institutional-agreements/configure/set-up.html" url

@InstAgrConfigR0428
@InstAgrConfigResetLehigh @InstAgrConfigResetUmn @InstAgrConfigResetUsil
Scenario Outline: Institutional Agreement Module Configuration form successfully create a configuration
    Given I am using the <BrowserName> browser
    And I have signed in as "<Supervisor>" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement types listbox 
    And   I have clicked the add link for option 1 in the Agreement types listbox
    And   I have seen a textbox for option "1" editor in the Agreement types listbox
    And   I have typed "A New Agreement" into the textbox for option "1" in the Agreement types listbox
    And   I have clicked the add link for option 1 in the Agreement types listbox
    And   I have seen a text "A New Agreement" for option "2" info in the Agreement types listbox
    When  I click the "create_configuration" submit button in the Institutional Agreement Module Configuration form 
    Then  I should see top feedback message "Module configuration was set up successfully."
    And   I should see a page at the "my/institutional-agreements/v1" url
Examples: 
    | BrowserName       | Supervisor              |
    | Chrome            | supervisor1@lehigh.edu  |
    | Firefox           | supervisor1@umn.edu     |
    | Internet Explorer | supervisor1@usil.edu.pe |

@InstAgrConfigR0429
@InstAgrConfigResetEdinburgh @InstAgrConfigResetSuny @InstAgrConfigResetBjtu
Scenario Outline: Institutional Agreement Module Configuration form successfully create and edit configuration
    Given I am using the <BrowserName> browser
    And   I have signed in as "<Supervisor>" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have seen a "Configure module" link
    And   I have clicked the "Configure module" link
    And   I have seen a "Click here to set it up now" link 
    And   I have clicked the "Click here to set it up now" link
    And   I have seen a page at the "my/institutional-agreements/configure/set-up.html" url
    And   I have seen an add link for option 1 in the Agreement types listbox 
    And   I have clicked the add link for option 1 in the Agreement types listbox
    And   I have seen a textbox for option "1" editor in the Agreement types listbox
    And   I have typed "A New Agreement" into the textbox for option "1" in the Agreement types listbox
    And   I have clicked the add link for option 1 in the Agreement types listbox
    And   I have seen a text "A New Agreement" for option "2" info in the Agreement types listbox
    And   I have clicked the "create_configuration" submit button in the Institutional Agreement Module Configuration form 
    And   I have seen top feedback message "Module configuration was set up successfully."
    And   I have seen a page at the "my/institutional-agreements/v1" url
    When  I see a "Configure module" link
    And   I click the "Configure module" link
    And   I see a page at the "my/institutional-agreements/configure" url
    And   I see a Remove link for option "3" info in the Agreement types listbox
    And   I click the Remove link for option "3" info in the Agreement types listbox
    Then  I should not see a textbox for option "3" info in the Agreement types listbox
    And   I should click the "save_changes_submit_button" submit button in the Institutional Agreement Module Configuration form 
    And   I should see top feedback message "Module configuration was saved successfully."
    And   I should see a page at the "my/institutional-agreements/configure" url
Examples: 
    | BrowserName       | Supervisor               |
    | Chrome            | supervisor1@napier.ac.uk |
    | Firefox           | supervisor1@suny.edu     |
    | Internet Explorer | supervisor1@bjtu.edu.cn  |

