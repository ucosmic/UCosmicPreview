@RepsModule

# See the RA_US_0004 scribble file(s) associated with this feature.

Feature: RA_US_0004 - Configure Recruitment Agency Module Norifications
	In order to ensure my office responds quickly to new recruitment agency applications
	As a recrutiment agency supervisor
	I want to designate one or more email addresses to receive notifications when a new recruitment agency application is submitted



Scenario Outline: Recruitment Agency Module Notification Emails Basic Edit

    # to prevent concurrency conflicts, use different users/establishments for different browsers
    Given I am using the <Browser> browser

    # basic form layout
    When I sign in as "<UserEmail>" with password "asdfasdf"
    And I browse to the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    Then I should see a "Add a new notification email" field with textbox
    And I should see a "Add" button
    And I should see a message indicating that I have no emails set up

    # email address required validation
    When I click the "Add" button
    Then I should see an error message "Please enter a valid email address." for the "Add a new notification email" field

    # email address regex validation
    When I type "invalid" into the "Add a new notification email" field
    And I click the "Add" button
    Then I should see an error message "Please enter a valid email address." for the "Add a new notification email" field

    # successfully add recipient 1
    When I type "recipient1@<ValidEmailDomain>" into the "Add a new notification email" field
    And I click the "Add" button
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    And I should see a temporary "Recruitment Agency Application Notification Email has been added" feedback message
    And I should not see a message indicating that I have no emails set up
    And I should see a list containing the email address "recipient1@<ValidEmailDomain>"
    And I should see a delete icon for the email address "recipient1@<ValidEmailDomain>"

    # successfully add recipient 2
    When I type "recipient2@<ValidEmailDomain>" into the "Add a new notification email" field
    And I click the "Add" button
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    And I should see a temporary "Recruitment Agency Application Notification Email has been added" feedback message
    And I should not see a message indicating that I have no emails set up
    And I should see a list containing the email address "recipient2@<ValidEmailDomain>"
    And I should see a delete icon for the email address "recipient2@<ValidEmailDomain>"

    # successfully add recipient N
    When I type "recipientN@<ValidEmailDomain>" into the "Add a new notification email" field
    And I click the "Add" button
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    And I should see a temporary "Recruitment Agency Application Notification Email has been added" feedback message
    And I should not see a message indicating that I have no emails set up
    And I should see a list containing the email address "recipientN@<ValidEmailDomain>"
    And I should see a delete icon for the email address "recipientN@<ValidEmailDomain>"

    # email address duplicate validation
    When I type "recipientN@<ValidEmailDomain>" into the "Add a new notification email" field
    And I click the "Add" button
    Then I should see an error message "This email address is already set up to receive notifications." for the "Add a new notification email" field

    # deleting email requires confirmation
    When I click the delete icon for the email address "recipient1@<ValidEmailDomain>"
    Then I should see a dialog confirming that I want to delete an email address

    # cancel/dismiss email delete confirmation
    When I click to cancel the deletion of the email address
    Then I should see a list containing the email address "recipient1@<ValidEmailDomain>"

    # accept email address delete confirmation
    When I click the delete icon for the email address "recipient1@<ValidEmailDomain>"
    And I see a dialog confirming that I want to delete an email address
    And I click to accept the deletion of the email address
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    And I should see a temporary "Recruitment Agency Application Notification Email has been deleted" feedback message
    And I should see a list not containing the email address "recipient1@<ValidEmailDomain>"

    # delte remaining email addresses
    When I click the delete icon for the email address "recipient2@<ValidEmailDomain>"
    And I see a dialog confirming that I want to delete an email address
    And I click to accept the deletion of the email address
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    And I should see a temporary "Recruitment Agency Application Notification Email has been deleted" feedback message
    And I should see a list not containing the email address "recipient2@<ValidEmailDomain>"

    # after last email address is delted, the list should no longer appear, and the empty message should reappear
    When I click the delete icon for the email address "recipientN@<ValidEmailDomain>"
    And I see a dialog confirming that I want to delete an email address
    And I click to accept the deletion of the email address
    Then I should see a page at the "<EstablishmentId>/recruitment-agencies/configure/notifications" url
    And I should see a temporary "Recruitment Agency Application Notification Email has been deleted" feedback message
    And I should not see a list of email addresses
    And I should see a message indicating that I have no emails set up

#to prevent concurrency conflicts, use different users/establishments for different browsers
Examples:
    | Browser | UserEmail                | EstablishmentId  | ValidEmailDomain |
    | Chrome  | supervisor1@uc.edu       | www.uc.edu       | uc.edu           |
    | Firefox | supervisor1@suny.edu     | www.suny.edu     | suny.edu         |
    | MSIE    | supervisor1@napier.ac.uk | www.napier.ac.uk | napier.ac.uk     |
