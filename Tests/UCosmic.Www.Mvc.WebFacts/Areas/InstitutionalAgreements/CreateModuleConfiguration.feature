Feature: Create Initial Institutional Agreements Module Configuration
    In order to control certain module aspects for my institution
    As an Institutional Agreement Manager
    I want to create a set of module configuration settings that apply to my institution

@UsingExampleUnconfiguredInstitutionalAgreementModules
Scenario Outline: Create initial module configuration

    Given I am using the <Browser> browser
    And I am signed in as <Supervisor>
    And I am starting from the Institutional Agreement Management page
    Then I should see a "Configure module" link

    When I click the "Configure module" link
    Then I should see the Institutional Agreement Configure Module page
    And I should see a "Click here to set it up now" link

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Agreement Types list

    When I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an Agreement Type text field for item #1 in the Agreement Types list

    When I type "A New Agreement" into the Agreement Type text field for item #1 in the Agreement Types list
    And I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an item for "A New Agreement" in the Agreement Types list

    When I click the "Create Configuration" submit button
    Then I should see the Institutional Agreement Management page
    And I should see the flash feedback message "Module configuration was set up successfully."

Examples:
    | Browser | Supervisor              |
    | Chrome  | supervisor1@lehigh.edu  |
    | Firefox | supervisor1@umn.edu     |
    | MSIE    | supervisor1@usil.edu.pe |

@UsingExampleUnconfiguredInstitutionalAgreementModules
Scenario Outline: Create, edit, and save initial module configuration

    Given I am using the <Browser> browser
    And I am signed in as <Supervisor>
    And I am starting from the Institutional Agreement Management page
    Then I should see a "Configure module" link

    When I click the "Configure module" link
    Then I should see the Institutional Agreement Configure Module page
    And I should see a "Click here to set it up now" link

    When I click the "Click here to set it up now" link
    Then I should see the Institutional Agreement Set Up Module page
    And I should see an "Add" link for item #1 in the Agreement Types list

    When I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an Agreement Type text field for item #1 in the Agreement Types list

    When I type "A New Agreement" into the Agreement Type text field for item #1 in the Agreement Types list
    And I click the "Add" link for item #1 in the Agreement Types list
    Then I should see an item for "A New Agreement" in the Agreement Types list

    When I click the "Create Configuration" submit button
    Then I should see the Institutional Agreement Management page
    And I should see the flash feedback message "Module configuration was set up successfully."
    And I should see a "Configure module" link

    When I click the "Configure module" link
    Then I should see the Institutional Agreement Configure Module page
    And I should see a "Remove" link for item #2 in the Agreement Types list

    When I click the "Remove" link for item #2 in the Agreement Types list
    Then I should not see an item for "A New Agreement" in the Agreement Types list

    When I click the "Save Changes" submit button
    Then I should see the Institutional Agreement Configure Module page
    And I should see the flash feedback message "Module configuration was saved successfully."

Examples:
    | Browser | Supervisor               |
    | Chrome  | supervisor1@napier.ac.uk |
    | Firefox | supervisor1@suny.edu     |
    | MSIE    | supervisor1@bjtu.edu.cn  |
