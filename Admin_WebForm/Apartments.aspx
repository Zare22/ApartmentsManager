<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="Admin_WebForm.Apartments" %>

<asp:Content ID="ApartmentsContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="text-center my-5">Apartments</h1>
        <div class="row">
            <div class="col-md-3">
                <div>
                    <div class="form-group">
                        <label for="filterByStatus">Filter by Status</label>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="filterByCity">Filter by City</label>
                        <asp:TextBox ID="txtBoxCity" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group mt-1">
                        <asp:Button ID="btnFilter" CssClass="btn btn-primary" runat="server" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button ID="btnReset" CssClass="btn btn-secondary" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                    <div class="form-group mt-1">
                        <asp:Button ID="btnAddApartment" CssClass="btn btn-primary" runat="server" Text="Add Apartment" OnClick="btnAddApartment_Click" />
                    </div>
                </div>
            </div>

            <div class="col-md-9">
                <asp:GridView ID="gridViewApartments" runat="server" CssClass="table" AutoGenerateColumns="false" AllowSorting="true" OnSorting="gridViewApartments_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="TotalRooms" HeaderText="Total Rooms" SortExpression="TotalRooms" />
                        <asp:BoundField DataField="MaxAdults" HeaderText="Max Adults" SortExpression="MaxAdults" />
                        <asp:BoundField DataField="MaxChildren" HeaderText="Max Children" SortExpression="MaxChildren" />
                        <asp:TemplateField HeaderText="Price" SortExpression="Price">
                            <ItemTemplate>
                                <%# String.Format("{0:0.00} €", Eval("Price")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CityName" HeaderText="City" SortExpression="CityName" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditApartment" runat="server" CssClass="fas fa-edit" CommandArgument='<%# Eval("Id") %>' OnClick="btnEditApartment_Click" />
                                <asp:LinkButton ID="btnDeleteApartment" runat="server" CssClass="fas fa-trash-alt text-danger" CommandArgument='<%# Eval("Id") %>' OnClick="btnDeleteApartment_Click"
                                    OnClientClick="return confirm('Are you sure you want to delete this apartment?');" />
                                <asp:HiddenField ID="hiddenIsReserved" runat="server" Value='<%# Eval("Status") %>' />
                                <asp:button ID="btnConfirmReservation" runat="server" CssClass="fas fa-check" Text="Confirm" CommandArgument='<%# Eval("Id") %>' OnClick="btnConfirmReservation_Click" OnDataBinding="btnConfirmReservation_DataBinding"/>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
