using DataLayer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Admin_WebForm
{
    public class Global : System.Web.HttpApplication
    {
        private readonly IRepository _repository;

        public Global()
        {
            _repository = RepoFactory.GetRepository();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["database"] = _repository;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e) => Response.Redirect("Error.aspx");
    }
}