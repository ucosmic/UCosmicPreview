@Authorization
@AuthR04
Feature:  User Role Management
	      In order to control access to UCosmic features
          As a Security Executive
	      I want to grant and revoke user role memberships

#execute these steps before every scenario in this file
#Background: 
#    Given I have signed in as "agent1@uc.edu" with password "asdfasdf"
#    And   I have browsed to the "roles" url

#2012.01.22: Roles are now added during DB seeding, there is no UI for adding roles.

#@AuthR0401
#Scenario Outline: Role Authorization forms unsuccessfully submit with empty required fields
#	Given I have clicked the "<LinkToForm>" link
#    And   I have seen a page at the "roles/manage/[PathVar]/<AddOrEdit>.html" url
#    When  I type "<Value1>" into the "Name" text box on the Role Authorization <AddOrEdit> form
#	And   I type "<Value2>" into the "Description" text box on the Role Authorization <AddOrEdit> form	
#    And   I click the "<SubmitLabel>" submit button on the Role Authorization <AddOrEdit> form
#    Then  I should see a page at the "roles/manage/add.html" url
#    And   I should see the error message "<SeeMsg1>" for the "Name" text box on the Role Authorization <AddOrEdit> form
#    And   I should see the error message "<SeeMsg2>" for the "Description" text box on the Role Authorization <AddOrEdit> form
#Examples: 
#    | AddOrEdit | LinkToForm              | Value1   | Value2 | SubmitLabel            | SeeMsg1                | SeeMsg2                  |
#    | add       | Add a new role          |          |        | Add Role Authorization | Role name is required. | Description is required. |
#    | add       | Add a new role          | TestRole |        | Add Role Authorization |                        | Description is required. |
#    | add       | Add a new role          |          | TBD    | Add Role Authorization | Role name is required. |                          |

#@AuthR0402A
#@OnlyInChrome
#Scenario Outline: Role Authorization forms succesfully submit after valid inputs using chrome
#    Given I have clicked the "Add a new role" link
#    And   I have seen a page at the "roles/manage/[PathVar]/add.html" url
#    When  I type "<Name>" into the "Name" text box on the Role Authorization add form
#	And   I type "<Description>" into the "Description" text box on the Role Authorization add form	
#    And   I click the "Add Role Authorization" submit button on the Role Authorization add form
#    Then  I should see a page at the "roles/manage/browse.html" url
#    And   I should see top feedback message "New role has been successfully added !" 
#Examples: 
#    | Name               | Description          |
#    | Test Role R0402A 1 | Created using Chrome |
#    | Test Role R0402A 2 | Created using Chrome |

#@AuthR0402B
#@OnlyInFirefox
#Scenario Outline: Role Authorization forms succesfully submit after valid inputs using firefox
#    Given I have clicked the "Add a new role" link
#    And   I have seen a page at the "roles/manage/[PathVar]/add.html" url
#    When  I type "<Name>" into the "Name" text box on the Role Authorization add form
#	And   I type "<Description>" into the "Description" text box on the Role Authorization add form	
#    And   I click the "Add Role Authorization" submit button on the Role Authorization add form
#    Then  I should see a page at the "roles/manage/browse.html" url
#    And   I should see top feedback message "New role has been successfully added !" 
#Examples: 
#    | Name               | Description           |
#    | Test Role R0402B 1 | Created using Firefox |
#    | Test Role R0402B 2 | Created using Firefox |
#
#@AuthR0402C
#@OnlyInInternetExplorer
#Scenario Outline: Role Authorization forms succesfully submit after valid inputs using internet explorer
#    Given I have clicked the "Add a new role" link
#    And   I have seen a page at the "roles/manage/[PathVar]/add.html" url
#    When  I type "<Name>" into the "Name" text box on the Role Authorization add form
#	And   I type "<Description>" into the "Description" text box on the Role Authorization add form	
#    And   I click the "Add Role Authorization" submit button on the Role Authorization add form
#    Then  I should see a page at the "roles/manage/browse.html" url
#    And   I should see top feedback message "New role has been successfully added !" 
#Examples: 
#    | Name               | Description      |
#    | Test Role R0402C 1 | Created using IE |
#    | Test Role R0402C 2 | Created using IE |

#@AuthR0403
#Scenario Outline: Role Authorization forms successfully add members to list box
#   Given  I have clicked the "<LinkToForm>" link
#   And    I have seen a page at the "roles/[PathVar]/<AddOrEdit>/[PathVar]" url
#   When   I type "<MemberTerm>" into the "member_search" text box on the Role Authorization <AddOrEdit> form
#   And    I see a "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   And    I click the "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   Then   I should see "<MemberTarget>" in the Members list box on the Role Authorization <AddOrEdit> form
#Examples: 
#    | AddOrEdit | LinkToForm                      | MemberTerm | MemberTarget             |
##   | add       | Add a new role                  | mit        | Mitch.Leventhal@suny.edu |
##   | add       | Add a new role                  | dan        | Daniel.Ludwig@uc.edu     |
#    | edit      | Institutional Agreement Manager | nyb        | Debra.Nyby@lehigh.edu    |
#    | edit      | Institutional Agreement Manager | bal        | DBallen@usil.edu.pe      |
#
#@AuthR0404
#Scenario Outline: Role Authorization forms does not allow duplicate entries into the members list box
#   Given  I have clicked the "<LinkToForm>" link
#   And    I have seen a page at the "roles/[PathVar]/<AddOrEdit>/[PathVar]" url
#   And    I have typed "<MemberTerm>" into the "member_search" text box on the Role Authorization <AddOrEdit> form
#   And    I have seen a "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   And    I have clicked the "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   And    I have seen "<MemberTarget>" in the Members list box on the Role Authorization <AddOrEdit> form
#   When   I type "<MemberTerm>" into the "member_search" text box on the Role Authorization <AddOrEdit> form
#   Then   I should not see a "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#Examples: 
#    | AddOrEdit | LinkToForm                      | MemberTerm | MemberTarget             |
##   | add       | Add a new role                  | mit        | Mitch.Leventhal@suny.edu |
##   | add       | Add a new role                  | dan        | Daniel.Ludwig@uc.edu     |
#    | edit      | Institutional Agreement Manager | nyb        | Debra.Nyby@lehigh.edu    |
#    | edit      | Institutional Agreement Manager | bal        | DBallen@usil.edu.pe      |
#
#@AuthR0405
#Scenario Outline: Role Authorization forms successfully dismiss by clicking Cancel button
#   Given  I have clicked the "<LinkToForm>" link
#   And    I have seen a page at the "roles/[PathVar]/<AddOrEdit>/[PathVar]" url
#   When   I click the cancel link on the Role Authorization <AddOrEdit> form
#   Then   I should see a page at the "roles" url
#Examples:
#    | AddOrEdit | LinkToForm                      |
##   | add       | Add a new role                  |
#    | edit      | Institutional Agreement Manager |
#
#@AuthR0406
#Scenario: Edit Role Authorization form does not allow role name to be changed
#   Given I have clicked the "Institutional Agreement Manager" link
#   When  I see a page at the "roles/[PathVar]/edit/[PathVar]" url 
#   Then  I should see a readonly hidden textbox for field "Role name" on the Role Authorization Edit form
#
#@AuthR0407
#Scenario: Edit Role Authorization form unsuccessfully submit with empty required field
#   Given I have clicked the "Institutional Agreement Manager" link
#   And   I have seen a page at the "roles/[PathVar]/edit/[PathVar]" url
#   When  I type "" into the "Description" text box on the Role Authorization edit form
#   And   I click the "Save Changes" submit button on the Role Authorization edit form
#   Then  I should see a page at the "roles/[PathVar]/edit/[PathVar]" url
#   And   I should see the error message "Description is required." for the "Description" text box on the Role Authorization edit form
#

