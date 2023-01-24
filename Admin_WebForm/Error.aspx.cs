using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_WebForm
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTryAgain_Click(object sender, EventArgs e) => Response.Redirect(Request.RawUrl);

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("Apartments.aspx");
            }
        }
    }
}