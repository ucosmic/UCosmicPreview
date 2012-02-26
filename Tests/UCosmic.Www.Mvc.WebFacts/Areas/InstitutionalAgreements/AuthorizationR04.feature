#2012.02.04: Institutional agreement role authorization has been deprecated.
@InstAgrAuth
@InstAgrAuthR04
Feature:  Institutional Agreements Role Management
	      In order to control access to my institution's agreement data
          As an Institutional Agreement Supervisor
	      I want to grant and revoke access to Institutional Agreement Managers

##execute these steps before every scenario in this file
#Background: 
#    Given I have signed in as "supervisor1@uc.edu" with password "asdfasdf"
#    And   I have browsed to the "my/institutional-agreements/authorization/browse.html" url
#
#@InstAgrAuthR0401
#Scenario Outline: Edit Institutional Agreement Authorization form successfully adds current user's affiliation members to the list box
#   Given  I have clicked the "<LinkToForm>" link
#   And    I have seen a page at the "my/institutional-agreements/authorization/[PathVar]/manage.html" url
#   When   I type "<MemberTerm>" into the "member_search" text box on the Edit Institutional Agreement Authorization form
#   And    I <SeeOrNot> a "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   And    I <ClickOrNot> the "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   Then   I should <SeeOrNot1> "<MemberTarget>" in the Members list box on the Edit Institutional Agreement Authorization form
#Examples: 
#   | LinkToForm                         | MemberTerm | SeeOrNot | ClickOrNot | SeeOrNot1 | MemberTarget             |
#   | Institutional Agreement Manager    | dan        | see      | click      | see       | Daniel.Ludwig@uc.edu     |
#   | Institutional Agreement Manager    | mit        | not seen | not click  | not seen  | Mitch.Leventhal@suny.edu |
#   | Institutional Agreement Supervisor | cus        | see      | click      | see       | Ronald.Cushing@uc.edu    |
#   | Institutional Agreement Supervisor | lut        | not seen | not click  | not seen  | Gary.Lutz@lehigh.edu     |
#
#@InstAgrAuthR0402
#Scenario Outline: Edit Institutional Agreement Authorization form does not allow duplicate entries into the members list box
#   Given  I have clicked the "<LinkToForm>" link
#   And    I have seen a page at the "my/institutional-agreements/authorization/[PathVar]/manage.html" url
#   And    I have typed "<MemberTerm>" into the "member_search" text box on the Edit Institutional Agreement Authorization form
#   And    I have seen a "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   And    I have clicked the "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#   And    I have seen "<MemberTarget>" in the Members list box on the Edit Institutional Agreement Authorization form
#   When   I type "<MemberTerm>" into the "member_search" text box on the Edit Institutional Agreement Authorization form
#   Then   I should not see a "MemberSearch" autocomplete dropdown menu item "<MemberTarget>"
#Examples: 
#   | LinkToForm                         | MemberTerm | MemberTarget          |
#   | Institutional Agreement Manager    | dan        | Daniel.Ludwig@uc.edu  |
#   | Institutional Agreement Supervisor | cus        | Ronald.Cushing@uc.edu |
#
#@InstAgrAuthR0403
#Scenario Outline: Edit Institutional Agreement Authorization form successfully dismiss by clicking Cancel button
#   Given  I have clicked the "<LinkToForm>" link
#   And    I have seen a page at the "my/institutional-agreements/authorization/[PathVar]/manage.html" url
#   When   I click the "Cancel" submit button on the Edit Institutional Agreement Authorization form
#   Then   I should see a page at the "my/institutional-agreements/authorization/browse.html" url
#Examples:
#    | LinkToForm                         |
#    | Institutional Agreement Manager    |
#    | Institutional Agreement Supervisor |
