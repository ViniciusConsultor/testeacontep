<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Erro.aspx.cs" Inherits="Erro_Erro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sistema Empresarial Constâncio Vieira</title>
</head>
<!--onload="javascript:history.back(1);"-->
<body bgcolor="#05235F">
    <form id="form1" runat="server">
    <table width="100%" height="404" border="0" cellpadding="0" cellspacing="0" background="../App_Themes/GrupoConstancioVieira/Imagens/erros/repeat_ferros.jpg" class="fundoTelaErros">
  <tr>
    <td height="404" align="center" valign="top"><table width="775" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="left" valign="top"><img src="../App_Themes/GrupoConstancioVieira/Imagens/erros/logo3d.jpg" width="282" height="418" /></td>
        <td width="0" align="left" valign="top"><table width="100%" border="0" cellspacing="10" cellpadding="10">
          <tr>
            <td width="246"></td>
          </tr>
          <tr>
            <td height="306" align="left" valign="top" bgcolor="#FFFFFF" class="textoBase11">
                <div>
                    <asp:Label ID="lblMensagem" runat="server" Text="Mensagem"></asp:Label>
                    <br />
                    <br />
                    <asp:HyperLink ID="HyperLink1" runat="server"  EnableTheming =True  NavigateUrl="javascript:history.back(1);" BorderColor="Red" Font-Bold="True" Font-Italic="True" Font-Overline="True" Font-Size="Larger" Font-Strikeout="False" Font-Underline="True" ForeColor="Red">Voltar</asp:HyperLink>
                </div>
           </td>
          </tr>          
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>
</form>
</body>
</html>
