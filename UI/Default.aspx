<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Caso Studio Final - Default</title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 74px;
        }
    </style>
</head>
<body bgcolor="antiquewhite">
    <form id="form1" runat="server">
    <div style="text-align: center">
        Proyecto Final<br />
        <br />
        <br />
        <table style="width: 600px" border="1">
            <tr>
                <td style="width: 211px">
                    Entidades</td>
                <td style="width: 482px">
                    <asp:GridView ID="EntidadesCompleto" runat="server" Width="427px">
                        <SelectedRowStyle BackColor="Fuchsia" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnVer" runat="server" OnClick="btnVer_Click" Text="Entidades Seleccion" />
                    &nbsp;<asp:Button ID="btnMostrarTramites" runat="server" CssClass="auto-style1" Height="25px" OnClick="btnMostrarTramites_Click" Text="Mostrar tramites" Width="146px" />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 211px; height: 59px">
                    Tipos de tramites associado con&nbsp; &lt;Nombre Entidad&gt;</td>
                <td style="width: 482px; height: 59px">
                    <asp:GridView ID="EntidadesSelecion" runat="server" Width="212px">
                        <SelectedRowStyle BackColor="Fuchsia" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 211px">
                </td>
                <td style="width: 482px">
                    <asp:Label ID="lblerror" runat="server" Width="400px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 211px">
                </td>
               
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
