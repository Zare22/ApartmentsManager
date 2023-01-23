using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin_WebForm.App_Code
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            //if (Session["user"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
        }
    }

}