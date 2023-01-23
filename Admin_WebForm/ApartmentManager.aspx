<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApartmentManager.aspx.cs" Inherits="Admin_WebForm.ApartmentManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/all.min.css" />
    <link rel="stylesheet" href="Content/MyCss/FormControl.css" />
    <link rel="stylesheet" href="Content/MyCss/Image.css" />
</head>
<body>
    <form id="formApartmentEditing" runat="server">
        <asp:Panel ID="pnlApartmentForm" runat="server">
            <div class="container">

                <h1 class="text-center my-5">
                    <% if (Session["apartment"] != null)
                        { %> 
                        Edit apartment
                    <% }
                        else
                        { %>
                        Add apartment
                    <% } %>
                </h1>


                <div class="form-group mt-3">
                    <asp:Label ID="lblName" runat="server" Text="Name:" AssociatedControlID="txtName" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblMaxAdults" runat="server" Text="Max Adults:" AssociatedControlID="txtMaxAdults" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtMaxAdults" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMaxAdults" runat="server" ControlToValidate="txtMaxAdults" ErrorMessage="Max Adults is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblMaxChildren" runat="server" Text="Max Children:" AssociatedControlID="txtMaxChildren" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtMaxChildren" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMaxChildren" runat="server" ControlToValidate="txtMaxChildren" ErrorMessage="Max children is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblTotalRooms" runat="server" Text="Total rooms:" AssociatedControlID="txtTotalRooms" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtTotalRooms" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTotalRooms" runat="server" ControlToValidate="txtTotalRooms" ErrorMessage="Total rooms is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPrice" runat="server" Text="Price:" AssociatedControlID="txtPrice" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCityName" runat="server" Text="City Name:" AssociatedControlID="txtCityName" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ControlToValidate="txtCityName" ErrorMessage="City name is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAddress" runat="server" Text="Address:" AssociatedControlID="txtAddress" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblBeachDistance" runat="server" Text="Beach Distance:" AssociatedControlID="txtBeachDistance" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtBeachDistance" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBeachDistance" runat="server" ControlToValidate="txtBeachDistance" ErrorMessage="Beach distance is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblStatus" runat="server" Text="Status:" AssociatedControlID="ddlStatus" CssClass="control-label"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ErrorMessage="Status is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">

                    <div class="form-control">
                        <div class="row">
                            <div class="col-6 d-flex justify-content-center align-items-center">
                                <asp:Label ID="lblAllTags" runat="server" Text="All tags:" CssClass="control-label"></asp:Label>
                                <asp:ListBox ID="listBoxAllTags" runat="server" ItemType="DataLayer.Models.Tag" CssClass="form-control h-100" DataValueField="Id" DataTextField="Name"></asp:ListBox>
                                <div class="col m-1">
                                    <asp:LinkButton ID="btnAddTag" runat="server" CssClass="btn btn-primary ml-2" Text="Select" OnClick="btnAddTag_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="col-6 d-flex justify-content-center align-items-center">
                                <asp:Label ID="lblUsedTags" runat="server" Text="Apartment tags:" CssClass="control-label"></asp:Label>
                                <asp:ListBox ID="listBoxUsedTags" runat="server" ItemType="DataLayer.Models.Tag" CssClass="form-control h-100" DataValueField="Id" DataTextField="Name"></asp:ListBox>
                                <div class="col m-1">
                                    <asp:LinkButton ID="btnRemoveTag" runat="server" CssClass="fas fa-trash-alt text-danger" Text="Remove" OnClick="btnRemoveTag_Click" CausesValidation="false"
                                        OnClientClick="return confirm('Are you sure you want to remove this tag?');"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-group mt-3">
                    <asp:Label ID="lblImages" runat="server" Text="Add images:" AssociatedControlID="fileUpload" CssClass="control-label"></asp:Label>
                    <asp:FileUpload ID="fileUpload" runat="server" multiple="multiple" CssClass="btn btn-light " Accept=".png, .jpg, .jpeg"/>
                    <asp:Button ID="btnImagesToRepeater" runat="server" CssClass="btn btn-primary" OnClick="btnImagesToRepeater_Click" CausesValidation="false" Text="Upload images"/>
                </div>

                <div class="form-group mt-3">
                    <asp:Label ID="lblUsedImages" runat="server" Text="Images:" CssClass="control-label"></asp:Label>
                    <div id="imageContainer" class="row-cols-4 w-100 h-100">
                        <asp:Repeater ID="repeaterImages" runat="server">
                            <ItemTemplate>
                                <div class="form-group">
                                    <img src='<%# Eval("imageUrl") %>' alt='<%# Eval("Name") %>' class="img-thumbnail"/>
                                    <asp:Button ID="btnRemoveImage" runat="server" CssClass="fas fa-trash-alt text-danger" CommandArgument='<%# Eval("Name") %>' Text="X"
                                        OnClick="btnRemoveImage_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this image?');"/>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>


                <div class="form-group mt-3">
                    <asp:LinkButton ID="btnCloseApartmentManager" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnCloseApartmentManager_Click" CausesValidation="false"></asp:LinkButton>
                    <% if (Session["apartment"] != null)
                        { %>
                    <asp:LinkButton ID="btnSaveChanges" runat="server" CssClass="btn btn-primary" Text="Update apartment" OnClick="btnSaveChanges_Click"></asp:LinkButton>
                    <% }
                        else
                        { %>
                    <asp:LinkButton ID="btnAddNewApartment" runat="server" CssClass="btn btn-primary" Text="Add new apartment" OnClick="btnAddNewApartment_Click"></asp:LinkButton>
                    <% } %>
                </div>
            </div>
        </asp:Panel>
    </form>
    <script src="Scripts/jquery-3.6.1.js"></script>
</body>
</html>
