

var MvcJs = {
	
	Base: {
		VaryByCustomUser: "User",
		FeedbackMessageKey: "TopMessage"
	},
	Shared: {

	},

	Activities: {

		ActivityForm: {
			New: function() {
				var url = "/my/activities/new";

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ActivityForm",
			SuccessMessage: "Your changes have been saved successfully."
		},
		ActivityIndex: {
			Get: function() {
				var url = "/faculty-staff";

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ActivityIndex"
		},
		ActivityInfo: {
			NameConst: "ActivityInfo"
		},
		ActivityList: {
			ShortListLength: "5",
			NameConst: "ActivityList"
		},
		ActivitySearch: {
			AutoCompleteKeyword: function(establishment, term) {
				var url = "/{establishment}/activities/keywords?term={term}";
				
				if (establishment) {
					url = url.replace("{establishment}", establishment);
				} else {
					url = url.replace("establishment={establishment}", "").replace("?&","?").replace("&&","&");
				}
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ActivitySearch"
		},
		TagList: {
			Add: function(domainType, domainKey, text) {
				var url = "/activities/tags/add?domainType={domainType}&domainKey={domainKey}&text={text}";
				
				if (domainType) {
					url = url.replace("{domainType}", domainType);
				} else {
					url = url.replace("domainType={domainType}", "").replace("?&","?").replace("&&","&");
				}
				
				if (domainKey) {
					url = url.replace("{domainKey}", domainKey);
				} else {
					url = url.replace("domainKey={domainKey}", "").replace("?&","?").replace("&&","&");
				}
				
				if (text) {
					url = url.replace("{text}", text);
				} else {
					url = url.replace("text={text}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "TagList"
		},
		TagMenu: {
			Post: function(term, excludes) {
				var url = "/activities/tags/menu?term={term}&excludes={excludes}";
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}
				
				if (excludes) {
					url = url.replace("{excludes}", excludes);
				} else {
					url = url.replace("excludes={excludes}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "TagMenu"
		},
		Shared: {

		}
	}
,

	Common: {

		Errors: {
			Unexpected: function() {
				var url = "/errors/unexpected";

				return url.replace(/([?&]+$)/g, "");
			},
			Throw: function() {
				var url = "/errors/throw";

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Errors"
		},
		Features: {
			Releases: function(version) {
				var url = "/releases/{version}";
				
				if (version) {
					url = url.replace("{version}", version);
				} else {
					url = url.replace("version={version}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Requirements: function(module) {
				var url = "/features/{module}";
				
				if (module) {
					url = url.replace("{module}", module);
				} else {
					url = url.replace("module={module}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Features"
		},
		Health: {
			NameConst: "Health"
		},
		Navigation: {
			NameConst: "Navigation"
		},
		Qa: {
			NameConst: "Qa"
		},
		Skins: {
			Change: function(skinContext, returnUrl) {
				var url = "/as/{skinContext}?returnUrl={returnUrl}";
				
				if (skinContext) {
					url = url.replace("{skinContext}", skinContext);
				} else {
					url = url.replace("skinContext={skinContext}", "").replace("?&","?").replace("&&","&");
				}
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Apply: function(skinFile) {
				var url = "/skins/apply/{skinFile}";
				
				if (skinFile) {
					url = url.replace("{skinFile}", skinFile);
				} else {
					url = url.replace("skinFile={skinFile}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Logo: function() {
				var url = "/skins/logo";

				return url.replace(/([?&]+$)/g, "");
			},
			Sample: function(content) {
				var url = "/skins/sample/{content}";
				
				if (content) {
					url = url.replace("{content}", content);
				} else {
					url = url.replace("content={content}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Skins"
		}
	}
,

	Establishments: {

		ManagementForms: {
			Browse: function() {
				var url = "/establishments";

				return url.replace(/([?&]+$)/g, "");
			},
			Form: function(entityId) {
				var url = "/establishments/new?entityId={entityId}";
				
				if (entityId) {
					url = url.replace("{entityId}", entityId);
				} else {
					url = url.replace("entityId={entityId}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ManagementForms"
		},
		SupplementalForms: {
			Locate: function(establishmentId, returnUrl) {
				var url = "/establishments/locate?establishmentId={establishmentId}&returnUrl={returnUrl}";
				
				if (establishmentId) {
					url = url.replace("{establishmentId}", establishmentId);
				} else {
					url = url.replace("establishmentId={establishmentId}", "").replace("?&","?").replace("&&","&");
				}
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Locate1: function(model) {
				var url = "/establishments/locate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "SupplementalForms"
		},
		Shared: {

		}
	}
,

	Identity: {

		ConfirmEmail: {
			ValidateSecretCode: function(model) {
				var url = "/confirm-email/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ConfirmEmail"
		},
		CreatePassword: {
			ValidatePasswordConfirmation: function(model) {
				var url = "/create-password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessage: "You can now use your password to sign on.",
			NameConst: "CreatePassword"
		},
		ForgotPassword: {
			ValidateEmailAddress: function(model) {
				var url = "/forgot-password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ForgotPassword",
			SuccessMessageFormat: "A password reset email has been sent to {0}."
		},
		ListIdentityProviders: {
			NameConst: "ListIdentityProviders"
		},
		MyHome: {
			NameConst: "MyHome"
		},
		ReceiveSamlAuthnResponse: {
			Post: function() {
				var url = "/sign-on/saml/2/post";

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ReceiveSamlAuthnResponse"
		},
		ResetPassword: {
			ValidatePasswordConfirmation: function(model) {
				var url = "/reset-password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ResetPassword",
			SuccessMessage: "You can now use your new password to sign on."
		},
		ServiceProviderMetadata: {
			Real: function(contentType) {
				var url = "/sign-on/saml/2/metadata?contentType={contentType}";
				
				if (contentType) {
					url = url.replace("{contentType}", contentType);
				} else {
					url = url.replace("contentType={contentType}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Test: function(contentType) {
				var url = "/sign-on/saml/2/metadata/develop?contentType={contentType}";
				
				if (contentType) {
					url = url.replace("{contentType}", contentType);
				} else {
					url = url.replace("contentType={contentType}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ServiceProviderMetadata"
		},
		SignDown: {
			Get: function(returnUrl) {
				var url = "/sign-down?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "SignDown"
		},
		SignIn: {
			ValidatePassword: function(model) {
				var url = "/sign-in/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "SignIn"
		},
		SignOn: {
			ValidateEmailAddress: function(model) {
				var url = "/sign-on/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "SignOn",
			SuccessMessageFormat: "You are now signed on to UCosmic as {0}."
		},
		SignOut: {
			SuccessMessage: "You have successfully been signed out of UCosmic.",
			NameConst: "SignOut"
		},
		SignOver: {
			ValidateEmailAddress: function(model) {
				var url = "/sign-over/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Undo: function(returnUrl) {
				var url = "/sign-over/undo?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessageFormat: "Sign on was changed from {0} to {1}.",
			NameConst: "SignOver"
		},
		SignUp: {
			SuccessMessageFormat: "A sign up confirmation email has been sent to {0}.",
			NameConst: "SignUp"
		},
		UpdateAffiliation: {
			SuccessMessage: "Your affiliation info was successfully updated.",
			NoChangesMessage: "No changes were made.",
			NameConst: "UpdateAffiliation"
		},
		UpdateEmailValue: {
			NameConst: "UpdateEmailValue",
			SuccessMessageFormat: "Your email address was successfully changed to {0}.",
			NoChangesMessage: "No changes were made."
		},
		UpdateName: {
			SuccessMessage: "Your info was successfully updated.",
			NoChangesMessage: "No changes were made.",
			NameConst: "UpdateName"
		},
		UpdatePassword: {
			ValidateCurrentPassword: function(model) {
				var url = "/my/password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidateNewPasswordConfirmation: function(model) {
				var url = "/my/password/validate/new?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessage: "Your password has been changed. Use your new password to sign on next time.",
			NameConst: "UpdatePassword"
		},
		Shared: {

		}
	}
,

	InstitutionalAgreements: {

		ConfigurationForms: {
			Add: function() {
				var url = "/my/institutional-agreements/configure/set-up";

				return url.replace(/([?&]+$)/g, "");
			},
			Add1: function(model) {
				var url = "/my/institutional-agreements/configure/set-up?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Edit: function() {
				var url = "/my/institutional-agreements/configure";

				return url.replace(/([?&]+$)/g, "");
			},
			Edit1: function(model) {
				var url = "/my/institutional-agreements/configure?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ConfigurationForms"
		},
		ManagementForms: {
			Browse: function() {
				var url = "/my/institutional-agreements/v1";

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(entityId) {
				var url = "/my/institutional-agreements/v1/new?entityId={entityId}";
				
				if (entityId) {
					url = url.replace("{entityId}", entityId);
				} else {
					url = url.replace("entityId={entityId}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post1: function(model) {
				var url = "/my/institutional-agreements/v1/new?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ManagementForms"
		},
		PublicSearch: {
			ChangeOwner: function(newEstablishmentUrl, keyword) {
				var url = "/institutional-agreements/change-owner?newEstablishmentUrl={newEstablishmentUrl}&keyword={keyword}";
				
				if (newEstablishmentUrl) {
					url = url.replace("{newEstablishmentUrl}", newEstablishmentUrl);
				} else {
					url = url.replace("newEstablishmentUrl={newEstablishmentUrl}", "").replace("?&","?").replace("&&","&");
				}
				
				if (keyword) {
					url = url.replace("{keyword}", keyword);
				} else {
					url = url.replace("keyword={keyword}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Index: function(establishmentUrl, keyword) {
				var url = "/{establishmentUrl}/institutional-agreements/{keyword}";
				
				if (establishmentUrl) {
					url = url.replace("{establishmentUrl}", establishmentUrl);
				} else {
					url = url.replace("establishmentUrl={establishmentUrl}", "").replace("?&","?").replace("&&","&");
				}
				
				if (keyword) {
					url = url.replace("{keyword}", keyword);
				} else {
					url = url.replace("keyword={keyword}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			AutoCompleteKeyword: function(establishmentUrl, term) {
				var url = "/institutional-agreements/autocomplete/search/keyword/{establishmentUrl}?term={term}";
				
				if (establishmentUrl) {
					url = url.replace("{establishmentUrl}", establishmentUrl);
				} else {
					url = url.replace("establishmentUrl={establishmentUrl}", "").replace("?&","?").replace("&&","&");
				}
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "PublicSearch"
		},
		Shared: {

		}
	}
,

	Languages: {

		Language: {
			Get: function(id) {
				var url = "/languages/{id}";
				
				if (id) {
					url = url.replace("{id}", id);
				} else {
					url = url.replace("id={id}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Language"
		},
		Languages: {
			Get: function(inputs) {
				var url = "/languages?inputs={inputs}";
				
				if (inputs) {
					url = url.replace("{inputs}", inputs);
				} else {
					url = url.replace("inputs={inputs}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			PutPreference: function(key, value) {
				var url = "/preferences/languages?key={key}&value={value}";
				
				if (key) {
					url = url.replace("{key}", key);
				} else {
					url = url.replace("key={key}", "").replace("?&","?").replace("&&","&");
				}
				
				if (value) {
					url = url.replace("{value}", value);
				} else {
					url = url.replace("value={value}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Languages"
		},
		Shared: {

		}
	}
,

	People: {

		PersonInfo: {
			ByEmail: function(email) {
				var url = "/people/by-email?email={email}";
				
				if (email) {
					url = url.replace("{email}", email);
				} else {
					url = url.replace("email={email}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ByGuid: function(guid) {
				var url = "/people/by-guid?guid={guid}";
				
				if (guid) {
					url = url.replace("{guid}", guid);
				} else {
					url = url.replace("guid={guid}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			WithEmail: function(term, matchStrategy) {
				var url = "/people/with-email?term={term}&matchStrategy={matchStrategy}";
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}
				
				if (matchStrategy) {
					url = url.replace("{matchStrategy}", matchStrategy);
				} else {
					url = url.replace("matchStrategy={matchStrategy}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			WithFirstName: function(term, matchStrategy) {
				var url = "/people/with-first-name?term={term}&matchStrategy={matchStrategy}";
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}
				
				if (matchStrategy) {
					url = url.replace("{matchStrategy}", matchStrategy);
				} else {
					url = url.replace("matchStrategy={matchStrategy}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			WithLastName: function(term, matchStrategy) {
				var url = "/people/with-last-name?term={term}&matchStrategy={matchStrategy}";
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}
				
				if (matchStrategy) {
					url = url.replace("{matchStrategy}", matchStrategy);
				} else {
					url = url.replace("matchStrategy={matchStrategy}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "PersonInfo"
		},
		PersonName: {
			GenerateDisplayName: function(model) {
				var url = "/people/generate-display-name?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			AutoCompleteSalutations: function(term) {
				var url = "/people/salutations?term={term}";
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			AutoCompleteSuffixes: function(term) {
				var url = "/people/suffixes?term={term}";
				
				if (term) {
					url = url.replace("{term}", term);
				} else {
					url = url.replace("term={term}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "PersonName",
			SalutationAndSuffixNullValueLabel: "[None]"
		}
	}
,

	RecruitmentAgencies: {

		Shared: {

		}
	}
,

	Roles: {

		Roles: {
			Browse: function() {
				var url = "/roles";

				return url.replace(/([?&]+$)/g, "");
			},
			Form: function(slug) {
				var url = "/roles/{slug}/edit";
				
				if (slug) {
					url = url.replace("{slug}", slug);
				} else {
					url = url.replace("slug={slug}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Roles"
		}
	}
};







