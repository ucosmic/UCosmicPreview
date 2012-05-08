@RepsModule

# See the RA_US_0003 scribble file(s) associated with this feature.

Feature: RA_US_0003 - Configure Recruitment Agency Module Welcome Message
	In order to communicate specific details about my institution's recruitment agency application and approval process
	As a recrutiment agency supervisor
	I want to configure a custom message to be displayed to recruitment agency applicants before they begin the online application



Scenario Outline: Recruitment Agency Module Welcome Message Basic Edit

    # to prevent concurrency conflicts, use different users/establishments for different browsers
    Given I am using the <Browser> browser

    # basic form layout
    When I sign in as "<UserEmail>" with password "asdfasdf"
    And I browse to the "<EstablishmentId>/recruitment-agencies/configure/welcome-message" url
    Then I should see a "Status" field with "enabled" and "disabled" radio buttons
    And I should see a "Title" field with textarea
    And I should see a "Content" field with TinyMCE editor
    And I should see radio buttons for "Active message" and "Draft message"
    And I should see a "Save Draft" button
    And I should see a "Save & Activate" button
    And I should see a "Discard & Cancel" link

    # first edit & save
    When I click the "enabled" radio button for the "Status" field
    And I type "Here is a sample heading" into the "Title" field
    And I type "Here is sample content" into the "Content" field
    And I click the "Save & Activate" button
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure" url
    And I should see a temporary "Your Recruitment Agency Application Welcome Message has been saved" feedback message

    # confirm edits were persisted
    When I browse to the "<EstablishmentId>/recruitment-agencies/configure/welcome-message" url
    Then I should see the "enabled" radio button for the "Status" field is checked
    And I should see "Here is a sample heading" in the "Title" field
    And I should see "Here is sample content" in the "Content" field
    And I should see the "Active message" radio button is checked

    # editing changes to draft message mode
    When I type "Here is sample content that I have edited" into the "Content" field
    Then I should see the "Draft message" radio button is checked

    # switch between active & draft content
    When I click the "Active message" radio button
    Then I should see "Here is sample content" in the "Content" field

    When I click the "Draft message" radio button
    Then I should see "Here is sample content that I have edited" in the "Content" field

    # Save draft button should not do a full postback, save drafts over ajax only
    When I click the "Save Draft" button
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/welcome-message" url
    And I should see a temporary "Your Welcome Message draft has been saved" feedback message

    # diabling welcome message requires confirmation
    When I browse to the "<EstablishmentId>/recruitment-agencies/configure/welcome-message" url
    And I click the "disabled" radio button for the "Status" field
    Then I should see a dialog confirming that I want to disable the welcome message

    # cancel/dismiss welcome message diable confirmation
    When I click to cancel the disabling of the welcome message
    Then I should see the "enabled" radio button for the "Status" field is checked

    # accept welcome message disable confirmation
    When I click the "disabled" radio button for the "Status" field
    And I see a dialog confirming that I want to disable the welcome message
    And I click to accept the disabling of the welcome message
    Then I should see the "disabled" radio button for the "Status" field is checked

    # edit title & save changes
    When I type "Here is a sample heading that I have edited" into the "Title" field
    And I click the "Save & Activate" button
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure" url
    And I should see a temporary "Your Recruitment Agency Application Welcome Message has been saved" feedback message

    # confirm second edit persisted changes
    When I browse to the "<EstablishmentId>/recruitment-agencies/configure/welcome-message" url
    Then I should see the "disabled" radio button for the "Status" field is checked
    And I should see "Here is a sample heading that I have edited" in the "Title" field
    And I should see "Here is sample content that I have edited" in the "Content" field
    And I should see the "Active message" radio button is checked

# to prevent concurrency conflicts, use different users/establishments for different browsers
Examples:
    | Browser | UserEmail                | EstablishmentId  |
    | Chrome      | supervisor1@uc.edu       | www.uc.edu       |
    | Firefox     | supervisor1@suny.edu     | www.suny.edu     |
    | MSIE        | supervisor1@napier.ac.uk | www.napier.ac.uk |
