Feature: Change My Email Spelling
    In order to correct mistakes in capitalization
    As a UCosmic.com user
    I want to change the spelling of my email addresses

@UsingFreshExampleEmailSpellingForAny1AtUsil
Scenario Outline: Changes Email Spelling

    Given I am using the <Browser> browser
    And I am signed in as <EmailAddress>
    And I am starting from the Personal Home page
    And I see a link titled "Change spelling"

    When I click the link titled "Change spelling"
    Then I should see the Update Email Spelling page
    And I should see a "Cancel" link

    # test cancel link
    When I click the "Cancel" link
    Then I should see the Personal Home page
    When I click the link titled "Change spelling"
    Then I should see the Update Email Spelling page

    # required error for email address
    When I type "" into the New Spelling text field
    And I click the "Save Changes" submit button
    Then I should still see the Update Email Spelling page
    And I should see the Invalid error message for the New Spelling text field

    # invalid error for email address
    When I type "incorrect" into the New Spelling text field
    And I click the "Save Changes" submit button
    Then I should still see the Update Email Spelling page
    And I should see the Invalid error message for the New Spelling text field

    # succeed changing email address
    When I type "<NewSpelling>" into the New Spelling text field
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your email address was successfully changed to <NewSpelling>."
    And I should see a "<NewSpelling>" link

    # change back to original
    When I click the "<NewSpelling>" link
    Then I should see the Update Email Spelling page
    And I should see "<NewSpelling>" in the New Spelling text field

    When I type "<EmailAddress>" into the New Spelling text field
    And I click the "Save Changes" submit button
    Then I should see the Personal Home page
    And I should see the flash feedback message "Your email address was successfully changed to <EmailAddress>."

Examples:
    | Browser | EmailAddress      | NewSpelling       |
    | Chrome  | any1@usil.edu.pe  | ANY1@USIL.EDU.PE  |
    | Firefox | any1@bjtu.edu.cn  | ANY1@BJTU.EDU.CN  |
    | MSIE    | any1@napier.ac.uk | ANY1@NAPIER.AC.UK |
