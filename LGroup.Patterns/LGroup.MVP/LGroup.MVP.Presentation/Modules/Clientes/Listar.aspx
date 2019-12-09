<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listar.aspx.cs" Inherits="LGroup.MVP.Presentation.Modules.Clientes.Listar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="grvClientes" runat="server" Height="134px" Width="534px">
        </asp:GridView>
    
    </div>
        <br />
        <asp:Button ID="btnCarregar" runat="server" Text="Carregar" OnClick="btnCarregar_Click" />
    </form>
</body>
</html>
