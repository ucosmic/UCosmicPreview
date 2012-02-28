@Nav
Feature:  Navigation
	      In order to access UCosmic content 
	      As someone browsing the web
	      I want to navigate UCosmic.com

@Nav01
Scenario Outline: Browse from one url to another using hyperlinks without signing in
    Given I have signed out
	And   I have browsed to the "<StartAt>" url
	When  I click the "<LinkTextToClick>" link
    Then  I should see a page at the "<ArriveAt>" url
Examples: 
    | StartAt  | LinkTextToClick | ArriveAt |
    |          | Sign In         | sign-in  |
    |          | sign in         | sign-in  |
    |          | Sign Up         | sign-up  |
    |          | sign up         | sign-up  |
    |          | Home            |          |
    | sign-in  | sign up         | sign-up  |
    | sign-in  | Sign Up         | sign-up  |
    | sign-in  | Sign In         | sign-in  |
    | sign-out | Sign Up         | sign-up  |
    | sign-out | Sign In         | sign-in  |

@Nav02
Scenario Outline: Browse from one url to another using hyperlinks after signing in as any1@uc.edu
    Given I have signed in as "any1@uc.edu" with password "asdfasdf"
	And   I have browsed to the "<StartAt>" url
	When  I click the "<LinkTextToClick>" link
    Then  I should see a page at the "<ArriveAt>" url
Examples: 
    | StartAt    | LinkTextToClick | ArriveAt   |
    |            | sign in         | sign-in    |
    |            | sign up         | sign-up    |
    |            | Home            |            |
    |            | any1@uc.edu     | my/profile |
    | my/profile | Home            |            |
    | my/profile | any1@uc.edu     | my/profile |
    | sign-in    | sign up         | sign-up    |
    | sign-out   | Sign Up         | sign-up    |
    | sign-out   | Sign In         | sign-in    |

@Nav03
Scenario Outline: Browse from one url to another using hyperlinks after signing in as manager1@suny.edu
    Given I have signed in as "manager1@suny.edu" with password "asdfasdf"
	And   I have browsed to the "<StartAt>" url
	When  I click the "<LinkTextToClick>" link
    Then  I should see a page at the "<ArriveAt>" url
Examples: 
    | StartAt                        | LinkTextToClick     | ArriveAt                           |
    |                                | Agreements          | my/institutional-agreements        |
    | my/profile                     | Agreements          | my/institutional-agreements        |
    | my/institutional-agreements/v1 | Home                |                                    |
    | my/institutional-agreements/v1 | Agreements          | my/institutional-agreements        |
    | my/institutional-agreements/v1 | Add a new agreement | my/institutional-agreements/v1/new |

