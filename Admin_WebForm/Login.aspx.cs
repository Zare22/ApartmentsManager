using DataLayer.DAL;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_WebForm
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlLogin.Visible = true;
                pnlError.Visible = false;
            }

            if (Session["user"] != null)
            {
                Response.Redirect("Apartments.aspx");
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim();

            User user = ((IRepository)Application["database"]).AuthenticateAdmin(email, password);



            if (user == null)
            {
                pnlError.Visible = true;
                pnlLogin.Visible = true;
            }
            else
            {
                Session["user"] = user;
                Response.Redirect("Apartments.aspx");
            }
        }
    }
}