using Admin_WebForm.App_Code;
using DataLayer.DAL;
using DataLayer.Enums;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Image = DataLayer.Models.Image;

namespace Admin_WebForm
{
    public partial class ApartmentManager : BasePage
    {
        IList<Tag> _allTags;
        Apartment updateApartment;

        protected void Page_Load(object sender, EventArgs e)
        {
            _allTags = ((IRepository)Application["database"]).GetTags();
            if (!IsPostBack)
            {
                if (Session["apartment"] != null)
                {
                    FillApartmentForm();
                }
                LoadTags();

                foreach (var status in Enum.GetValues(typeof(Status)))
                {
                    ddlStatus.Items.Add(new ListItem(status.ToString(), ((int)status).ToString()));
                }
                ddlStatus.SelectedValue = "6";
            }
        }

        private void FillApartmentForm()
        {
            updateApartment = ((Apartment)Session["apartment"]);
            txtName.Text = updateApartment.Name;
            txtMaxAdults.Text = updateApartment.MaxAdults.ToString();
            txtMaxChildren.Text = updateApartment.MaxChildren.ToString();
            txtTotalRooms.Text = updateApartment.TotalRooms.ToString();
            txtPrice.Text = updateApartment.Price.ToString();
            txtCityName.Text = updateApartment.CityName;
            txtAddress.Text = updateApartment.Address;
            txtBeachDistance.Text = updateApartment.BeachDistance.ToString();
            ddlStatus.SelectedValue = updateApartment.Status.ToString();
            LoadApartmentImagesToRepeater();
        }

        //Tags logic
        private void LoadTags()
        {
            if (updateApartment != null)
            {
                BindTags(updateApartment.Tags, listBoxUsedTags);

                List<Tag> allTags = new List<Tag>(_allTags);
                allTags.RemoveAll(t => updateApartment.Tags.Any(tag => tag.Id == t.Id));

                BindTags(allTags, listBoxAllTags);
            }
            else
                BindTags(_allTags, listBoxAllTags);

        }

        private void BindTags(IList<Tag> tags, ListBox listBox)
        {
            listBox.DataSource = tags;
            listBox.DataBind();
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            if (listBoxAllTags.SelectedIndex != -1)
            {
                var selectedItem = listBoxAllTags.SelectedItem;
                listBoxUsedTags.Items.Add(selectedItem);
                listBoxAllTags.Items.Remove(selectedItem);
                RemoveSelections();
                listBoxAllTags.Focus();
            }
        }

        protected void btnRemoveTag_Click(object sender, EventArgs e)
        {
            if (listBoxUsedTags.SelectedIndex != -1)
            {
                var selectedItem = listBoxUsedTags.SelectedItem;
                listBoxAllTags.Items.Add(selectedItem);
                listBoxUsedTags.Items.Remove(selectedItem);
                RemoveSelections();
                listBoxUsedTags.Focus();
            }
        }

        private List<Tag> GetUsedTags()
        {
            List<int> tagIDs = listBoxUsedTags.Items.Cast<ListItem>().Select(x => int.Parse(x.Value)).ToList();
            List<Tag> tags = _allTags.Where(t => tagIDs.Contains(t.Id)).ToList();
            return tags;
        }

        private void RemoveSelections()
        {
            listBoxAllTags.ClearSelection();
            listBoxUsedTags.ClearSelection();
        }

        //Image logic
        private void LoadApartmentImagesToRepeater()
        {
            if (updateApartment.Images != null)
            {
                SetSelectedImages(updateApartment.Images);
                AssignImageUrlForRepeater(updateApartment.Images);
                BindImages(updateApartment.Images);
            }
        }

        private void SetSelectedImages(IList<Image> images) => Session["selectedImages"] = updateApartment.Images;

        private void AssignImageUrlForRepeater(IList<Image> images)
        {
            foreach (var image in images)
            {
                byte[] imageBytes = image.Base64ToByteArray();
                image.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
            }
        }

        private void BindImages(IList<Image> images)
        {
            repeaterImages.DataSource = images;
            repeaterImages.DataBind();
            Session["selectedImages"] = images;
        }

        protected void btnImagesToRepeater_Click(object sender, EventArgs e)
        {
            if (fileUpload.PostedFile != null)
            {
                List<Image> images = fileUpload.PostedFiles.Select(file => new Image(file)).ToList();
                AssignImageUrlForRepeater(images);
                var previousImages = (List<Image>)Session["selectedImages"] ?? new List<Image>();
                var bindingList = previousImages.Union(images).ToList();
                BindImages(bindingList);
            }
            repeaterImages.Focus();
        }

        protected void btnRemoveImage_Click(object sender, EventArgs e)
        {
            string imageName = ((Button)sender).CommandArgument;

            List<Image> selectedImages = (List<Image>)Session["selectedImages"];
            selectedImages.RemoveAll(i => i.Name == imageName);
            BindImages(selectedImages);
            repeaterImages.Focus();
        }

        protected void btnAddNewApartment_Click(object sender, EventArgs e)
        {
            if (repeaterImages.Items.Count > 0)
            {
                SetRepresentative((IList<Image>)Session["selectedImages"]);
            }

            ((IRepository)Application["database"]).CreateApartment(
                new Apartment
                {
                    Name = txtName.Text.Trim(),
                    MaxAdults = int.Parse(txtMaxAdults.Text.Trim()),
                    MaxChildren = int.Parse(txtMaxChildren.Text.Trim()),
                    Price = decimal.Parse(txtPrice.Text.Trim()),
                    TotalRooms = int.Parse(txtTotalRooms.Text.Trim()),
                    CityName = txtCityName.Text.Trim(),
                    BeachDistance = int.Parse(txtBeachDistance.Text.Trim()),
                    Status = (Status)int.Parse(ddlStatus.SelectedValue),
                    Address = txtAddress.Text.Trim(),
                    Tags = GetUsedTags(),
                    Images = (IList<Image>)Session["selectedImages"]
                });
            BackToApartments();
        }


        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (repeaterImages.Items.Count > 0)
            {
                SetRepresentative((IList<Image>)Session["selectedImages"]);
            }

            ((IRepository)Application["database"]).UpdateApartment(
                new Apartment
                {
                    Id = ((Apartment)Session["apartment"]).Id,
                    Name = txtName.Text.Trim(),
                    MaxAdults = int.Parse(txtMaxAdults.Text.Trim()),
                    MaxChildren = int.Parse(txtMaxChildren.Text.Trim()),
                    Price = decimal.Parse(txtPrice.Text.Trim()),
                    TotalRooms = int.Parse(txtTotalRooms.Text.Trim()),
                    CityName = txtCityName.Text.Trim(),
                    BeachDistance = int.Parse(txtBeachDistance.Text.Trim()),
                    Status = (Status)int.Parse(ddlStatus.SelectedValue),
                    Address = txtAddress.Text.Trim(),
                    Tags = GetUsedTags(),
                    Images = (IList<Image>)Session["selectedImages"]
                });
            BackToApartments();
        }

        private void SetRepresentative(IList<Image> images) => images.ToList()[0].IsRepresentative = true;

        protected void btnCloseApartmentManager_Click(object sender, EventArgs e)
        {
            BackToApartments();
        }

        private void BackToApartments()
        {
            Response.Redirect("Apartments");
            Session.Remove("apartment");
            Session.Remove("selectedImages");
        }
    }
}