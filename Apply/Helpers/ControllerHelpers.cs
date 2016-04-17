using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Apply.Helpers {
    public class ControllerHelpers : Controller {
        /// <summary>
        /// Creates an error page with a custom message for the user and 
        /// a link back to where they came from.
        /// </summary>
        /// <param name="errorMessage">Message to be displayed to the user</param>
        /// <param name="controllerName">Controller part of the return url</param>
        /// <param name="actionName">Action part of the return url</param>
        /// <param name="routeValues">Route values object eg. new {id = 5}. Leave blank if not required</param>
        /// <returns>ViewResult</returns>
        public ViewResult CreateErrorPage(string errorMessage, string controllerName, string actionName, object routeValues = null)
        {
            //TODO: Take an exception object in instead of the errorMessage param and 
            //translate it's codes into something more user friendly
            var exception = new Exception(errorMessage);
            var error = new HandleErrorInfo(exception, controllerName, actionName);
            ViewBag.RouteValues = routeValues;
            return View("~/Views/Shared/Error.cshtml", error);
        }

    }
}