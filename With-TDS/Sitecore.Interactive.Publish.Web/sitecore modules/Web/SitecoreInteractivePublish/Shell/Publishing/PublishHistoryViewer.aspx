<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishHistoryViewer.aspx.cs" Inherits="Sitecore.Interactive.Publish.Web.sitecore_modules.Web.SitecoreInteractivePublish.Shell.Publishing.PublishHistoryViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="content-type" content="text/html; charset=utf-8" />
        <title>Publish History Viewer</title>

        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container">
                <h2>Sitecore Interactive Publish</h2>
                <p></p>

                <%-- Kept for future updates --%>
<%--                <h3>Publish History</h3>
                <p></p>
                <asp:Table class="table" ID="tblCurrentPublish" runat="server">
                    <asp:TableHeaderRow CssClass="info">
                        <asp:TableHeaderCell>Publish Job Handle</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Publish Target</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Owner</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Date & Time</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>--%>
                <%-- End of Kept for future updates --%>
                <h3>Publish History</h3>
                <p></p>
                <asp:Table class="table" ID="tblPublishHistory" runat="server">
                    <asp:TableHeaderRow CssClass="info">
                        <asp:TableHeaderCell>Publish Job Handle</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Publish Target</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Owner</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Date & Time</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
        </form>
    </body>
</html>