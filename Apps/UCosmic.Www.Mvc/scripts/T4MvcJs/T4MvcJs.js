

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
			NameConst: "ActivityList",
			ShortListLength: "5"
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
			Get: function(token, secretCode) {
				var url = "/Identity/ConfirmEmail/Get?token={token}&secretCode={secretCode}";
				
				if (token) {
					url = url.replace("{token}", token);
				} else {
					url = url.replace("token={token}", "").replace("?&","?").replace("&&","&");
				}
				
				if (secretCode) {
					url = url.replace("{secretCode}", secretCode);
				} else {
					url = url.replace("secretCode={secretCode}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidateSecretCode: function(model) {
				var url = "/confirm-email/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/ConfirmEmail/Post?model={model}";
				
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
			Get: function(token) {
				var url = "/Identity/CreatePassword/Get?token={token}";
				
				if (token) {
					url = url.replace("{token}", token);
				} else {
					url = url.replace("token={token}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidatePasswordConfirmation: function(model) {
				var url = "/create-password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/CreatePassword/Post?model={model}";
				
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
			Get: function() {
				var url = "/Identity/ForgotPassword/Get";

				return url.replace(/([?&]+$)/g, "");
			},
			ValidateEmailAddress: function(model) {
				var url = "/forgot-password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/ForgotPassword/Post?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessageFormat: "A password reset email has been sent to {0}.",
			NameConst: "ForgotPassword"
		},
		ListIdentityProviders: {
			Get: function() {
				var url = "/Identity/ListIdentityProviders/Get";

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "ListIdentityProviders"
		},
		MyHome: {
			Get: function() {
				var url = "/Identity/MyHome/Get";

				return url.replace(/([?&]+$)/g, "");
			},
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
			Get: function(token) {
				var url = "/Identity/ResetPassword/Get?token={token}";
				
				if (token) {
					url = url.replace("{token}", token);
				} else {
					url = url.replace("token={token}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidatePasswordConfirmation: function(model) {
				var url = "/reset-password/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/ResetPassword/Post?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessage: "You can now use your new password to sign on.",
			NameConst: "ResetPassword"
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
			Get: function(returnUrl) {
				var url = "/Identity/SignIn/Get?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidatePassword: function(model) {
				var url = "/sign-in/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/SignIn/Post?model={model}";
				
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
			Get: function(returnUrl) {
				var url = "/Identity/SignOn/Get?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidateEmailAddress: function(model) {
				var url = "/sign-on/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/SignOn/Post?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			AlphaProxy: function(establishmentId, returnUrl) {
				var url = "/sign-on/alpha-proxy/{establishmentId}?returnUrl={returnUrl}";
				
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
			SuccessMessageFormat: "You are now signed on to UCosmic as {0}.",
			NameConst: "SignOn"
		},
		SignOut: {
			Get: function(returnUrl) {
				var url = "/Identity/SignOut/Get?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			HadSamlSignOnSessionKey: "HadSamlSignOn",
			SuccessMessage: "You have successfully been signed out of UCosmic.",
			NameConst: "SignOut"
		},
		SignOver: {
			Get: function(returnUrl) {
				var url = "/Identity/SignOver/Get?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidateEmailAddress: function(model) {
				var url = "/sign-over/validate?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/SignOver/Post?model={model}";
				
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
			Get: function(returnUrl) {
				var url = "/Identity/SignUp/Get?returnUrl={returnUrl}";
				
				if (returnUrl) {
					url = url.replace("{returnUrl}", returnUrl);
				} else {
					url = url.replace("returnUrl={returnUrl}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Post: function(model) {
				var url = "/Identity/SignUp/Post?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessageFormat: "A sign up confirmation email has been sent to {0}.",
			NameConst: "SignUp"
		},
		UpdateAffiliation: {
			Get: function(establishmentId) {
				var url = "/Identity/UpdateAffiliation/Get?establishmentId={establishmentId}";
				
				if (establishmentId) {
					url = url.replace("{establishmentId}", establishmentId);
				} else {
					url = url.replace("establishmentId={establishmentId}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Put: function(model) {
				var url = "/Identity/UpdateAffiliation/Put?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessage: "Your affiliation info was successfully updated.",
			NoChangesMessage: "No changes were made.",
			NameConst: "UpdateAffiliation"
		},
		UpdateEmailValue: {
			Get: function(number) {
				var url = "/Identity/UpdateEmailValue/Get?number={number}";
				
				if (number) {
					url = url.replace("{number}", number);
				} else {
					url = url.replace("number={number}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			Put: function(model) {
				var url = "/Identity/UpdateEmailValue/Put?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			ValidateValue: function(model) {
				var url = "/Identity/UpdateEmailValue/ValidateValue?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessageFormat: "Your email address was successfully changed to {0}.",
			NoChangesMessage: "No changes were made.",
			NameConst: "UpdateEmailValue"
		},
		UpdateName: {
			Get: function() {
				var url = "/Identity/UpdateName/Get";

				return url.replace(/([?&]+$)/g, "");
			},
			Put: function(model) {
				var url = "/Identity/UpdateName/Put?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			SuccessMessage: "Your info was successfully updated.",
			NoChangesMessage: "No changes were made.",
			NameConst: "UpdateName"
		},
		UpdatePassword: {
			Get: function() {
				var url = "/Identity/UpdatePassword/Get";

				return url.replace(/([?&]+$)/g, "");
			},
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
			Post: function(model) {
				var url = "/Identity/UpdatePassword/Post?model={model}";
				
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

		Item: {
			Get: function(id) {
				var url = "/languages/{id}";
				
				if (id) {
					url = url.replace("{id}", id);
				} else {
					url = url.replace("id={id}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Item"
		},
		Search: {
			Get: function(request) {
				var url = "/languages?request={request}";
				
				if (request) {
					url = url.replace("{request}", request);
				} else {
					url = url.replace("request={request}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Search"
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
			SalutationAndSuffixNullValueLabel: "[None]",
			NameConst: "PersonName"
		}
	}
,

	Preferences: {

		Change: {
			Put: function(model) {
				var url = "/my/preferences?model={model}";
				
				if (model) {
					url = url.replace("{model}", model);
				} else {
					url = url.replace("model={model}", "").replace("?&","?").replace("&&","&");
				}

				return url.replace(/([?&]+$)/g, "");
			},
			NameConst: "Change"
		},
		Shared: {

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







