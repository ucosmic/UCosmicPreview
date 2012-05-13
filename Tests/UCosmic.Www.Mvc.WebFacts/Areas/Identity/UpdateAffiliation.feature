Feature: Update My Affiliation
    In order to control what UCosmic says about me
    As a UCosmic user
    I want to update my affiliation and employment information

@UsingFreshExampleDefaultAffiliationForAny1AtUsil
Scenario Outline: Update Affiliaton Type, Employment Categories, and Job Titles

    Given I am using the <Browser> browser
    And I am signed in as any1@usil.edu.pe
    And I am starting from the Personal Home page
    When I click the "Set up affiliation" link
    Then I should see the Personal Affiliation page
    And I should see a "Cancel" link

    When I check the Affiliation Type radio button labeled 'I am an employee.'
    Then I should see an 'Additional Employee Details' section
    And I should see an 'International Office Employee' check box
    And I should see an 'Administrator Employee' check box
    And I should see an 'Faculty Employee' check box
    And I should see an 'Staff Employee' check box

    # check administrator and staff employee boxes
    When I check the 'Administrator Employee' check box
    And I check the 'Staff Employee' check box
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see an "Edit affiliation info" link

    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page
    And the 'Administrator Employee' check box should be checked
    And the 'Staff Employee' check box should be checked

    # uncheck administrator and staff employee boxes
    When I uncheck the 'Administrator Employee' check box
    And I uncheck the 'Staff Employee' check box
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see an "Edit affiliation info" link

    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page
    And the 'Administrator Employee' check box should be unchecked
    And the 'Staff Employee' check box should be unchecked

    # check international office and faculty boxes
    When I check the 'International Office Employee' check box
    And I check the 'Faculty Employee' check box
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see an "Edit affiliation info" link

    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page
    And the 'International Office Employee' check box should be checked
    And the 'Faculty Employee' check box should be checked

    # uncheck international office and faculty boxes
    When I uncheck the 'International Office Employee' check box
    And I uncheck the 'Faculty Employee' check box
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see an "Edit affiliation info" link

    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page
    And the 'International Office Employee' check box should be unchecked
    And the 'Faculty Employee' check box should be unchecked

    # test cancel link
    When I click the "Cancel" link
    Then I should see the Personal Home page
    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page

    # select employee only radio with no job title
    When I check the Affiliation Type radio button labeled 'I am an employee.'
    Then I should see an 'Additional Employee Details' section

    When I type "" into the Job Titles & Departments text field
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see an "Edit affiliation info" link
    And I should see "[Job Title(s) Unknown]" in the Affiliation Details section

    # select student only radio
    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page

    When I check the Affiliation Type radio button labeled 'I am a student.'
    Then I should not see an 'Additional Employee Details' section

    When I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see "Student" in the Affiliation Details section

    # select student and employee radio with no job title
    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page

    When I check the Affiliation Type radio button labeled 'I am both an employee and a student.'
    Then I should see an 'Additional Employee Details' section

    When I type "" into the Job Titles & Departments text field
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see "[Job Title(s) Unknown]" in the Affiliation Details section

    # select neither student nor employee radio
    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page

    When I check the Affiliation Type radio button labeled 'I am neither an employee nor a student.'
    Then I should not see an 'Additional Employee Details' section

    When I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see an "Edit affiliation info" link
    And I should see "Neither student nor employee" in the Affiliation Details section

    # select employee radio with job title
    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page

    When I check the Affiliation Type radio button labeled 'I am an employee.'
    Then I should see an 'Additional Employee Details' section

    When I type "Applications Analyst" into the Job Titles & Departments text field
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see "Applications Analyst" in the Affiliation Details section

    # select both employee and student radio with job title
    When I click the "Edit affiliation info" link
    Then I should see the Personal Affiliation page

    When I check the Affiliation Type radio button labeled 'I am both an employee and a student.'
    Then I should see an 'Additional Employee Details' section

    When I type "Teaching Assistant" into the Job Titles & Departments text field
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your affiliation info was successfully updated."
    And I should see "Teaching Assistant" in the Affiliation Details section

Examples:
    | Browser |
    | Chrome  |
    | Firefox |
    | MSIE    |
