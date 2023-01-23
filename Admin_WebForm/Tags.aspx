<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Admin_WebForm.Tags" %>

<asp:Content ID="TagsContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="text-center my-5">Tag Management</h1>
        <div class="row">
            <div class="col-md-3">
                <asp:Panel ID="pnlAddTag" runat="server">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtNewTag" Text="New Tag"></asp:Label>
                        <asp:TextBox runat="server" ID="txtNewTag" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" ID="btnAddTag" Text="Add Tag" CssClass="btn btn-primary mt-2" OnClick="btnAddTag_Click"/>
                </asp:Panel>
            </div>
            <div class="col-md-9">
                <asp:GridView ID="gvTags" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hiddenIsInUse" runat="server" Value='<%# Eval("IsInUse") %>' />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="fas fa-trash-alt text-danger" CommandArgument='<%# Eval("Id") %>' 
                                    OnDataBinding="btnDelete_DataBinding" OnClick="btnDelete_Click" CausesValidation="false"
                                     OnClientClick="return confirm('Are you sure you want to delete this tag?');"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
