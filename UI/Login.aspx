<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
            text-decoration: underline;
        }
        .auto-style1 {
            width: 347px;
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
                <strong><em><span class="style1">Proyecto final. Aplicaciones Web</span></em></strong><br />
        <br />
        <br />
    <div style="text-align: center">
        <table style="width: 271px" align="center" >
            <tr>
                <td style="width: 347px">
                    CI: &nbsp;&nbsp;
                    <asp:TextBox ID="TxtNomUsu" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 347px">
                    
                    Password:&nbsp;
                    <asp:TextBox ID="TxtPassUsu" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 347px; height: 21px; text-align: right">
                    <span style="color: #ff0066">
                    <asp:Button ID="BtnLogueo" runat="server" OnClick="BtnLogueo_Click" Text="Login" PostBackUrl="~/Default.aspx" /></span></td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style1">
                    <asp:Label ID="LblError" runat="server" Width="320px"></asp:Label></td>
            </tr>
        </table>
    
    </div>
        <br />
    
       
    
    </div>
    </form>
</body>
</html>
