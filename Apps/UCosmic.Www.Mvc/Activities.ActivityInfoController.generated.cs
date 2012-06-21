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
namespace UCosmic.Www.Mvc.Areas.Activities.Controllers {
    public partial class ActivityInfoController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ActivityInfoController(Dummy d) { }

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

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActivityInfoController Actions { get { return MVC.Activities.ActivityInfo; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Activities";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ActivityInfo";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ActivityInfo";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Get = "activity-info";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Get = "activity-info";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_ActivityInfoController: UCosmic.Www.Mvc.Areas.Activities.Controllers.ActivityInfoController {
        public T4MVC_ActivityInfoController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Get(System.Guid entityId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Get);
            callInfo.RouteValueDictionary.Add("entityId", entityId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591