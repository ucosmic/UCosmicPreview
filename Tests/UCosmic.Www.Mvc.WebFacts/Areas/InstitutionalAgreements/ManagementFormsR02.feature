@InstAgrForms
@InstAgrFormsR02
Feature:  Institutional Agreement Management Preview Revision 2
		  In order to have centralized online access to documents relating to my Institutional Agreements
		  As an Institutional Agreement Manager
		  I want to upload, remove, and generally manage which files are attached to my Institutional Agreements in UCosmic

@InstAgrFormsR0201
Scenario Outline: Institutional Agreement forms File Attachment successfully display upload input
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
    And   I have browsed to the "my/institutional-agreements/v1" url
    When  I click the "<LinkText>" link
    Then  I should see a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I should see a File Attachment upload input on the Institutional Agreements <AddOrEdit> form
    Examples: 
    | AddOrEdit | LinkText              |
    | new       | Add a new agreement   |
    | edit      | Agreement, UC 01 test |

@InstAgrFormsR0202
@NotInChrome
Scenario Outline: Institutional Agreement forms File Attachment unsuccessfully add with invalid extension <FileName>
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a File Attachment upload input on the Institutional Agreements <AddOrEdit> form
    When  I select "<FilePath>" as a File Attachment on the Institutional Agreements <AddOrEdit> form
    Then  I should not see "<FileName>" in the File Attachments list box on the Institutional Agreement <AddOrEdit> form
    And   I should see an invalid extension error message for the File Attachment upload input on the Institutional Agreement <AddOrEdit> form
    Examples:  
    | AddOrEdit | LinkText              | FilePath                                     | FileName           |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\JavaScriptFile1.js | JavaScriptFile1.js |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\VbScriptFile1.vb   | VbScriptFile1.vb   |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\ExtensionlessFile1 | ExtensionlessFile1 |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\JavaScriptFile1.js | JavaScriptFile1.js |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\VbScriptFile1.vb   | VbScriptFile1.vb   |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\ExtensionlessFile1 | ExtensionlessFile1 |

@InstAgrFormsR0203
@NotInChrome
Scenario Outline: Institutional Agreement forms File Attachment successfully add to list box
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a File Attachment upload input on the Institutional Agreements <AddOrEdit> form
    When  I select "<FilePath>" as a File Attachment on the Institutional Agreements <AddOrEdit> form
    Then  I should see "<FileName>" in the File Attachments list box on the Institutional Agreement <AddOrEdit> form
    And   I should not see an invalid extension error message for the File Attachment upload input on the Institutional Agreement <AddOrEdit> form
    Examples:  
    | AddOrEdit | LinkText              | FilePath                                       | FileName             |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf   | SpecFlow Guide.pdf   |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\WordDocument1.doc    | WordDocument1.doc    |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\WordDocument1.docx   | WordDocument1.docx   |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\OpenDocument1.odt    | OpenDocument1.odt    |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\ExcelWorkbook1.xls   | ExcelWorkbook1.xls   |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\ExcelWorkbook1.xlsx  | ExcelWorkbook1.xlsx  |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\OpenSpreadsheet1.ods | OpenSpreadsheet1.ods |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\PowerPoint1.ppt      | PowerPoint1.ppt      |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\PowerPoint1.pptx     | PowerPoint1.pptx     |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf   | SpecFlow Guide.pdf   |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\WordDocument1.doc    | WordDocument1.doc    |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\WordDocument1.docx   | WordDocument1.docx   |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\OpenDocument1.odt    | OpenDocument1.odt    |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\ExcelWorkbook1.xls   | ExcelWorkbook1.xls   |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\ExcelWorkbook1.xlsx  | ExcelWorkbook1.xlsx  |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\OpenSpreadsheet1.ods | OpenSpreadsheet1.ods |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\PowerPoint1.ppt      | PowerPoint1.ppt      |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\PowerPoint1.pptx     | PowerPoint1.pptx     |

@NotInChrome
@InstAgrFormsR0204
Scenario Outline: Institutional Agreement forms File Attachment successfully remove from list box
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "<LinkText>" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/<AddOrEdit>" url
    And   I have seen a File Attachment upload input on the Institutional Agreements <AddOrEdit> form
    And   I have selected "<FilePath>" as a File Attachment on the Institutional Agreements <AddOrEdit> form
    And   I have seen "<FileName>" in the File Attachments list box on the Institutional Agreement <AddOrEdit> form
    And   I have not seen an invalid extension error message for the File Attachment upload input on the Institutional Agreement <AddOrEdit> form
    When  I click the File Attachment remove icon for "<FileName>" on the Institutional Agreements <AddOrEdit> form
    Then  I should not see "<FileName>" in the File Attachments list box on the Institutional Agreement <AddOrEdit> form
Examples: 
    | AddOrEdit | LinkText              | FilePath                                   | FileName         |
    | new       | Add a new agreement   | C:\\WebDriverFileUploads\\LargePdf33.8.pdf | LargePdf33.8.pdf |
    | edit      | Agreement, UC 01 test | C:\\WebDriverFileUploads\\LargePdf33.8.pdf | LargePdf33.8.pdf |

@InstAgrFormsR0205 @NotInChrome @InstAgrFormsFreshTestAgreementUc01
Scenario Outline: Institutional Agreement forms File Attachment successfully display in files column after edit
  	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
    And   I have browsed to the "my/institutional-agreements/v1" url
	And   I have clicked the "Agreement, UC 01 test" link
	And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I have seen a File Attachment upload input on the Institutional Agreements <AddOrEdit> form
    And   I have selected "<FilePath>" as a File Attachment on the Institutional Agreements <AddOrEdit> form
    And   I have seen "<FileName>" in the File Attachments list box on the Institutional Agreement edit form
    And   I have not seen an invalid extension error message for the File Attachment upload input on the Institutional Agreement <AddOrEdit> form
    When  I successfully submit the Institutional Agreement edit form
	Then  I should see a page at the "institutional-agreements/[PathVar]" url within 30 seconds
	And   I should see a "<FileName>" link
    Examples: 
    | FilePath                                     | FileName           |
    | C:\\WebDriverFileUploads\\SpecFlow Guide.pdf | SpecFlow Guide.pdf |
    | C:\\WebDriverFileUploads\\WordDocument1.doc  | WordDocument1.doc  |
    | C:\\WebDriverFileUploads\\WordDocument1.docx | WordDocument1.docx |

@InstAgrFormsR0206
@NotInChrome
Scenario Outline: Institutional Agreement forms File Attachment unsuccessfully add file over 25 mb in size
	Given I have signed in as "manager1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "my/institutional-agreements/v1" url
    And   I have clicked the "Agreement, UC 01 test" link
    And   I have seen a page at the "my/institutional-agreements/v1/[PathVar]/edit" url
    And   I have seen a File Attachment upload input on the Institutional Agreements edit form
    And   I have selected "<FilePath>" as a File Attachment on the Institutional Agreements edit form
    And   I have seen "<FileName>" in the File Attachments list box on the Institutional Agreement edit form
    When  I click the "Save Changes" submit button on the Institutional Agreement edit form
    Then  I should see a page at the "errors/file-upload-too-large.html" url within 60 seconds
    Examples:  
    | FilePath                                   | FileName         |
    | C:\\WebDriverFileUploads\\LargePdf33.8.pdf | LargePdf33.8.pdf |

