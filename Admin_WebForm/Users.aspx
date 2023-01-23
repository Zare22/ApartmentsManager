<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Admin_WebForm.Users" %>

<asp:Content ID="UsersContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="text-center my-5">Registered Users</h1>
        <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
