@Nav
Feature: Navigate using hyperlinks
    In order to access UCosmic content
    As someone browsing the web
    I want to navigate the site

Scenario Outline: Browse from one url to another using hyperlinks without signing in

    Given I am not signed on
    And I am starting from the <StartAt> page
    Then I should see a "<LinkTextToClick>" link

    When  I click the "<LinkTextToClick>" link
    Then  I should see the <ArriveAt> page

Examples:
    | StartAt  | LinkTextToClick | ArriveAt |
    | Home     | Sign On         | Sign On  |
    | Sign Out | Sign On         | Sign On  |
    | Sign On  | Home            | Home     |

Scenario Outline: Browse from one url to another using hyperlinks after signing in as any1@uc.edu

    Given I am signed in as any1@uc.edu
    And I am starting from the <StartAt> page
    Then I should see a "<LinkTextToClick>" link

    When  I click the "<LinkTextToClick>" link
    Then  I should see the <ArriveAt> page

Examples:
    | StartAt       | LinkTextToClick | ArriveAt      |
    | Home          | any1@uc.edu     | Personal Home |
    | Personal Home | Home            | Home          |
    | Personal Home | any1@uc.edu     | Personal Home |

Scenario Outline: Browse from one url to another using hyperlinks after signing in as supervisor@suny.edu

    Given I am signed in as manager1@suny.edu
    And   I am starting from the <StartAt> page
    Then I should see a "<LinkTextToClick>" link

    When  I click the "<LinkTextToClick>" link
    Then  I should see the <ArriveAt> page

Examples:
    | StartAt                               | LinkTextToClick     | ArriveAt                                 |
    | Home                                  | Agreements          | Institutional Agreement Public Search    |
    | Personal Home                         | Agreements          | Institutional Agreement Public Search    |
    | Institutional Agreement Management    | Home                | Home                                     |
    | Institutional Agreement Management    | Add a new agreement | Institutional Agreement Add              |

