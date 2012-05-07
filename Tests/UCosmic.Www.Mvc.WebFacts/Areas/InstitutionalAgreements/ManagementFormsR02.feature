﻿@InstAgrForms
@InstAgrFormsR02
Feature: Institutional Agreement Management Preview Revision 2
    In order to have centralized online access to documents relating to my Institutional Agreements
    As an Institutional Agreement Manager
    I want to upload, remove, and generally manage which files are attached to my Institutional Agreements in UCosmic

Background:
    Given I am signed in as manager1@uc.edu
    And I am starting from the Institutional Agreement Management page

@InstAgrFormsR0201
Scenario Outline: File upload input field is displayed in the File Attachments fieldset

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a File Attachments upload field

Examples:
    | AddOrEdit | LinkText              |
    | Add       | Add a new agreement   |
    | Edit      | Agreement, UC 01 test |

@InstAgrFormsR0202 @NotInChrome
Scenario Outline: Add file attachment fails for invalid file types

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a File Attachments upload field

    When I choose the file "<FilePath>" for the File Attachments upload field
    Then I should not see an item for "<FileName>" in the File Attachments list
    And I should see the Invalid error message for the File Attachments field

Examples:
    | AddOrEdit | LinkText              | FilePath                                     | FileName           |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\JavaScriptFile1.js | JavaScriptFile1.js |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\VbScriptFile1.vb   | VbScriptFile1.vb   |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\ExtensionlessFile1 | ExtensionlessFile1 |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\JavaScriptFile1.js | JavaScriptFile1.js |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\VbScriptFile1.vb   | VbScriptFile1.vb   |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\ExtensionlessFile1 | ExtensionlessFile1 |

@InstAgrFormsR0203 @NotInChrome
Scenario Outline: Add file attachment succeeds for allowed file types

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a File Attachments upload field

    When I choose the file "<FilePath>" for the File Attachments upload field
    Then I should see an item for "<FileName>" in the File Attachments list
    And I should not see the Invalid error message for the File Attachments field

Examples:
    | AddOrEdit | LinkText              | FilePath                                       | FileName             |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf   | SpecFlow Guide.pdf   |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\WordDocument1.doc    | WordDocument1.doc    |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\WordDocument1.docx   | WordDocument1.docx   |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\OpenDocument1.odt    | OpenDocument1.odt    |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\ExcelWorkbook1.xls   | ExcelWorkbook1.xls   |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\ExcelWorkbook1.xlsx  | ExcelWorkbook1.xlsx  |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\OpenSpreadsheet1.ods | OpenSpreadsheet1.ods |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\PowerPoint1.ppt      | PowerPoint1.ppt      |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\PowerPoint1.pptx     | PowerPoint1.pptx     |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf   | SpecFlow Guide.pdf   |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\WordDocument1.doc    | WordDocument1.doc    |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\WordDocument1.docx   | WordDocument1.docx   |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\OpenDocument1.odt    | OpenDocument1.odt    |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\ExcelWorkbook1.xls   | ExcelWorkbook1.xls   |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\ExcelWorkbook1.xlsx  | ExcelWorkbook1.xlsx  |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\OpenSpreadsheet1.ods | OpenSpreadsheet1.ods |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\PowerPoint1.ppt      | PowerPoint1.ppt      |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\PowerPoint1.pptx     | PowerPoint1.pptx     |

@InstAgrFormsR0204 @NotInChrome
Scenario Outline: Remove file attachment from list succeeds

    When I click the "<LinkText>" link
    Then I should see the Institutional Agreement <AddOrEdit> page
    And I should see a File Attachments upload field

    When I choose the file "<FilePath>" for the File Attachments upload field
    Then I should see an item for "<FileName>" in the File Attachments list
    And I should not see the Invalid error message for the File Attachments field

    When I click the remove icon for "<FileName>" in the File Attachments list
    Then I should not see an item for "<FileName>" in the File Attachments list

Examples:
    | AddOrEdit | LinkText              | FilePath                                   | FileName         |
    | Add       | Add a new agreement   | C:\\WebDriverFileUploads\\LargePdf33.8.pdf | LargePdf33.8.pdf |
    | Edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\LargePdf33.8.pdf | LargePdf33.8.pdf |

@InstAgrFormsR0205 @InstAgrFormsFreshTestAgreementUc01
Scenario Outline: Upload file attachment succeeds and displays link to file after upload

    Given I am using the <BrowserName> browser

    When I click the "<AgreementLink>" link
    Then I should see the Institutional Agreement Edit page
    And I should see a File Attachments upload field

    When I choose the file "<FilePath>" for the File Attachments upload field
    Then I should see an item for "<FileName>" in the File Attachments list
    And I should not see the Invalid error message for the File Attachments field

    When I click the "Save Changes" submit button
    Then I should see the Public Institutional Agreement Detail page within 30 seconds
    And I should see a "<FileName>" link

Examples:
    | BrowserName       | AgreementLink         | FilePath                                     | FileName           |
    | Firefox           | Agreement, UC 01 test | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf | SpecFlow Guide.pdf |
    | Firefox           | Agreement, UC 01 test | C:\\WebDriverFileUploads\\WordDocument1.doc  | WordDocument1.doc  |
    | Firefox           | Agreement, UC 01 test | C:\\WebDriverFileUploads\\WordDocument1.docx | WordDocument1.docx |
    | Internet Explorer | Agreement, UC 02 test | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf | SpecFlow Guide.pdf |
    | Internet Explorer | Agreement, UC 02 test | C:\\WebDriverFileUploads\\WordDocument1.doc  | WordDocument1.doc  |
    | Internet Explorer | Agreement, UC 02 test | C:\\WebDriverFileUploads\\WordDocument1.docx | WordDocument1.docx |

@InstAgrFormsR0206 @NotInChrome
Scenario Outline: Upload file attachment faile for file over 25 megabytes in size

    When I click the "Agreement, UC 01 test" link
    Then I should see the Institutional Agreement Edit page
    And I should see a File Attachments upload field

    When I choose the file "<FilePath>" for the File Attachments upload field
    Then I should see an item for "<FileName>" in the File Attachments list

    When I click the "Save Changes" submit button
    Then I should see the File Upload Too Large page within 60 seconds

Examples:
    | FilePath                                   | FileName         |
    | C:\\WebDriverFileUploads\\LargePdf33.8.pdf | LargePdf33.8.pdf |

