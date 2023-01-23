using Admin_WebForm.App_Code;
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
    public partial class Users : BasePage
    {
        private IList<User> _users;
        protected void Page_Load(object sender, EventArgs e)
        {
            _users = ((IRepository)Application["database"]).GetUsers();
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            gvUsers.DataSource = _users;
            gvUsers.DataBind();
        }
    }
}