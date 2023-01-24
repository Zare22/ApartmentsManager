using Admin_WebForm.App_Code;
using DataLayer.DAL;
using DataLayer.Enums;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Admin_WebForm
{
    public partial class Apartments : BasePage
    {
        IList<Apartment> _apartments;

        protected void Page_Load(object sender, EventArgs e)
        {
            _apartments = ((IRepository)Application["database"]).GetApartments();
            if (!IsPostBack)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
                LoadStatuses();
                LoadApartments(_apartments);
            }
        }

        private void LoadStatuses()
        {
            foreach (var status in Enum.GetValues(typeof(Status)))
            {
                ddlStatus.Items.Add(new ListItem(status.ToString(), ((int)status).ToString()));

            }
            ddlStatus.SelectedValue = "6";
        }

        private void LoadApartments(IList<Apartment> apartments)
        {
            gridViewApartments.DataSource = apartments;
            gridViewApartments.DataBind();
            ViewState["dataSource"] = gridViewApartments.DataSource;
        }

        protected void gridViewApartments_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (_apartments != null)
            {
                var currentData = (List<Apartment>)ViewState["dataSource"];
                switch (e.SortExpression)
                {
                    case "Name":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.Name).ToList() : currentData.OrderByDescending(a => a.Name).ToList();
                        break;
                    case "TotalRooms":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.TotalRooms).ToList() : currentData.OrderByDescending(a => a.TotalRooms).ToList();
                        break;
                    case "MaxAdults":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.MaxAdults).ToList() : currentData.OrderByDescending(a => a.MaxAdults).ToList();
                        break;
                    case "MaxChildren":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.MaxChildren).ToList() : currentData.OrderByDescending(a => a.MaxChildren).ToList();
                        break;
                    case "Price":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.Price).ToList() : currentData.OrderByDescending(a => a.Price).ToList();
                        break;
                    case "CityName":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.CityName).ToList() : currentData.OrderByDescending(a => a.CityName).ToList();
                        break;
                    case "Status":
                        currentData = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? currentData.OrderBy(a => a.Status).ToList() : currentData.OrderByDescending(a => a.Status).ToList();
                        break;
                }
                ViewState["SortDirection"] = (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                LoadApartments(currentData);
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (_apartments != null)
            {
                var filteredApartments = _apartments;

                if (ddlStatus.SelectedItem.ToString() != Status.Any.ToString())
                {
                    filteredApartments = _apartments.Where(a => ((int)a.Status).ToString() == ddlStatus.SelectedValue.ToString()).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtBoxCity.Text.Trim()))
                {
                    filteredApartments = filteredApartments.Where(a => a.CityName != null && a.CityName.ToLower().Contains(txtBoxCity.Text.Trim().ToLower())).ToList();
                }
                LoadApartments(filteredApartments);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e) => LoadApartments(_apartments);

        protected void btnEditApartment_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int id = Convert.ToInt32(btn.CommandArgument);
            var apartment = _apartments.Where(a => a.Id == id).SingleOrDefault();
            Session["apartment"] = apartment;
            Response.Redirect("ApartmentManager.aspx");
        }

        protected void btnDeleteApartment_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int id = Convert.ToInt32(btn.CommandArgument);
            ((IRepository)Application["database"]).DeleteApartment(id);
            LoadAfterDelete();
        }

        private void LoadAfterDelete()
        {
            _apartments = ((IRepository)Application["database"]).GetApartments();
            gridViewApartments.DataSource = _apartments;
            gridViewApartments.DataBind();
            ViewState["dataSource"] = gridViewApartments.DataSource;
        }

        protected void btnAddApartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentManager.aspx");
        }
    }
}