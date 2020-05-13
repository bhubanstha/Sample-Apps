<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormFileUploader.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>HTML5 File Uploader</h1>
            <asp:FileUpload ID="inFileUpload" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <br />
            <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
