Feature: Manage Role Grants
    In order to control access to UCosmic features
    As a Security Executive
    I want to grant and revoke user role memberships

Background:

    Given I am signed in as agent1@uc.edu
    And I am starting from the Role Management page

Scenario: Role Name is read only

    When I click the "Institutional Agreement Manager" link
    Then I should see the Role Edit page
    And I should see a Role Description text field
    And I shouldn't see a Role Name text field

Scenario Outline: Dismiss Role Edit form by clicking the Cancel link

    When I click the "<RoleToEdit>" link
    Then I should see the Role Edit page
    And I should see a "Cancel" link

    When I click the "Cancel" link
    Then I should see the Role Management page

Examples:
    | RoleToEdit                         |
    | Institutional Agreement Manager    |
    | Institutional Agreement Supervisor |
    | Authorization Agent                |
    | Authentication Agent               |

Scenario Outline: Save Role fails when Description is empty

    When I click the "<RoleToEdit>" link
    Then I should see the Role Edit page

    When I type "" into the Role Description text field
    And I click the "Save Changes" submit button
    Then I should still see the Role Edit page
    And I should see the Required error message for the Role Description text field

Examples:
    | RoleToEdit                         |
    | Institutional Agreement Manager    |
    | Institutional Agreement Supervisor |

Scenario Outline: Add member to Grants list succeeds when member is not already in list

    When I click the "<RoleToEdit>" link
    Then I should see the Role Edit page

    When I type "<UserSearchTerm>" into the Member Search text field
    Then I should see an autocomplete dropdown menu item "<FoundUser>" for the Member Search combo field

    When I click the autocomplete dropdown menu item "<FoundUser>" for the Member Search combo field
    Then I should see an item for "<FoundUser>" in the Members list

Examples:
    | RoleToEdit                      | UserSearchTerm | FoundUser             |
    | Institutional Agreement Manager | nyb            | Debra.Nyby@lehigh.edu |
    | Institutional Agreement Manager | bal            | DBallen@usil.edu.pe   |

Scenario Outline: Add member to Grants list fails when member is already in list

    When I click the "<RoleToEdit>" link
    Then I should see the Role Edit page

    When I type "<UserSearchTerm>" into the Member Search text field
    Then I should see an autocomplete dropdown menu item "<FoundUser>" for the Member Search combo field

    When I click the autocomplete dropdown menu item "<FoundUser>" for the Member Search combo field
    Then I should see an item for "<FoundUser>" in the Members list

    When I type "<UserSearchTerm>" into the Member Search text field
    And I wait for 2 seconds
    Then I shouldn't see an autocomplete dropdown menu item "<FoundUser>" for the Member Search combo field

Examples:
    | RoleToEdit                      | UserSearchTerm | FoundUser             |
    | Institutional Agreement Manager | nyb            | Debra.Nyby@lehigh.edu |
    | Institutional Agreement Manager | bal            | DBallen@usil.edu.pe   |

@UsingFreshExampleRoleData
Scenario Outline: Add member to Grants list persists after saving changes

    Given I am using the <Browser> browser
    When I click the "Test Role" link
    Then I should see the Role Edit page
    And I should not see an item for "any1@suny.edu" in the Members list

    When I type "any1@suny" into the Member Search text field
    Then I should see an autocomplete dropdown menu item "any1@suny.edu" for the Member Search combo field

    When I click the autocomplete dropdown menu item "any1@suny.edu" for the Member Search combo field
    Then I should see an item for "any1@suny.edu" in the Members list

    When I click the "Save Changes" submit button
    Then I should see the Role Management page
    And I should see the flash feedback message "Role has been successfully saved."

    When I click the "Test Role" link
    Then I should see the Role Edit page
    And I should see an item for "any1@suny.edu" in the Members list

    When I click the remove icon for "any1@suny.edu" in the Members list
    Then I should not see an item for "any1@suny.edu" in the Members list

    When I click the "Save Changes" submit button
    Then I should see the Role Management page
    And I should see the flash feedback message "Role has been successfully saved."

    When I click the "Test Role" link
    Then I should see the Role Edit page
    And I should not see an item for "any1@suny.edu" in the Members list

Examples:
    | Browser |
    | Chrome  |
    | Firefox |
    | MSIE    |
