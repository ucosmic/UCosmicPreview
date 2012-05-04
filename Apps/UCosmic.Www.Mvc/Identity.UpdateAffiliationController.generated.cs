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
    public partial class UpdateAffiliationController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected UpdateAffiliationController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Get() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Get);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Put() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Put);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UpdateAffiliationController Actions { get { return MVC.Identity.UpdateAffiliation; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Identity";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "UpdateAffiliation";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "UpdateAffiliation";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Get = "update-affiliation";
            public readonly string Put = "update-affiliation";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Get = "update-affiliation";
            public const string Put = "update-affiliation";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _scripts = "~/Areas/Identity/Views/UpdateAffiliation/_scripts.cshtml";
            public readonly string _styles = "~/Areas/Identity/Views/UpdateAffiliation/_styles.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_UpdateAffiliationController: UCosmic.Www.Mvc.Areas.Identity.Controllers.UpdateAffiliationController {
        public T4MVC_UpdateAffiliationController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Get(int establishmentId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Get);
            callInfo.RouteValueDictionary.Add("establishmentId", establishmentId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Put(UCosmic.Www.Mvc.Areas.Identity.Models.UpdateAffiliationForm model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Put);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591