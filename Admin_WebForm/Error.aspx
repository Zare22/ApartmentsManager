<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Admin_WebForm.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container my-5">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <h1 class="text-center">Error</h1>
                            <p class="text-center">An error has occurred. Please try again or go back.</p>
                            <div class="text-center">
                                <asp:Button ID="btnTryAgain" runat="server" CssClass="btn btn-danger mr-2" Text="Try Again" OnClick=""/>
                                <asp:Button ID="btnGoBack" runat="server" CssClass="btn btn-secondary" Text="Go Back" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
