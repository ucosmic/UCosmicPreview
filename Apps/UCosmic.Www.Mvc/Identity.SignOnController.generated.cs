// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace UCosmic.Www.Mvc.Areas.Identity.Controllers {
    public partial class SignOnController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SignOnController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Begin() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Begin);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SignOnController Actions { get { return MVC.Identity.SignOn; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Identity";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "SignOn";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "SignOn";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Begin = "sign-on";
            public readonly string Saml2Post = "post";
            public readonly string Saml2Integrations = "providers";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Begin = "sign-on";
            public const string Saml2Post = "post";
            public const string Saml2Integrations = "providers";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string providers = "~/Areas/Identity/Views/SignOn/providers.cshtml";
            public readonly string sign_on = "~/Areas/Identity/Views/SignOn/sign-on.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_SignOnController: UCosmic.Www.Mvc.Areas.Identity.Controllers.SignOnController {
        public T4MVC_SignOnController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Begin(string returnUrl) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Begin);
            callInfo.RouteValueDictionary.Add("returnUrl", returnUrl);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Begin(UCosmic.Www.Mvc.Areas.Identity.Models.SignOn.SignOnBeginForm model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Begin);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Saml2Post() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Saml2Post);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Saml2Integrations() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Saml2Integrations);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
