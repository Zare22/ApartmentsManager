using Admin_WebForm.App_Code;
using DataLayer.DAL;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Admin_WebForm
{
    public partial class Tags : BasePage
    {
        private IList<Tag> _tags;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTags();
        }

        private void LoadTags()
        {
            _tags = ((IRepository)Application["database"]).GetTags();
            gvTags.DataSource = _tags;
            gvTags.DataBind();
        }

        protected void btnDelete_DataBinding(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            HiddenField hidden = (HiddenField)btn.NamingContainer.FindControl("hiddenIsInUse");
            bool isInUse = Convert.ToBoolean(hidden.Value);
            btn.Visible = !isInUse;
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            ((IRepository)Application["database"]).CreateTag(txtNewTag.Text.Trim());
            LoadTags();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.CommandArgument);
            ((IRepository)Application["database"]).DeleteTag(id);
            LoadTags();
        }
    }
}