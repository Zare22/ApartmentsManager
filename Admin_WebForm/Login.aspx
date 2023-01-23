<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Admin_WebForm.Login" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">

        <asp:Panel ID="pnlError" CssClass="container mt-1" runat="server" Visible="False" Style="width: 15vw !important">
            <div class='alert alert-danger' role='alert'>
                <asp:Label ID="lblErrorLogin" meta:resourcekey="lblErrorLogin" runat="server" Text="Check the entered data again!"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlLogin" runat="server" Visible="true">
            <div class="container text-center" style="width: 15vw !important">
                <div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="example@mail.com" required="true"></asp:TextBox>
                        <asp:Label runat="server" AssociatedControlID="txtEmail" Text="E-mail"></asp:Label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" placeholder="Password" TextMode="Password" required="true"></asp:TextBox>
                        <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
                    </div>

                    <div>
                        <asp:Button runat="server" class="btn btn-primary" type="submit" Text="Login" OnClick="BtnLogin_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
