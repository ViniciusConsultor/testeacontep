<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Login.aspx.cs"
    Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema Empresarial Constâncio Vieira</title>
</head>
<body>    
    <form id="form1" runat="server" defaultfocus="lgnLogin">
        <table width="100%" height="500px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="middle">        
                <asp:Login ID="lgnLogin" runat="server" DisplayRememberMe="False" LoginButtonType="Image" EnableViewState="False">                                                               
                <LayoutTemplate>
                    <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse">
                        <tr>
                            <td><img src="App_Themes/GrupoConstancioVieira/Imagens/botoes/login/mundo.jpg" /></td>
                            <td>                                
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td align="right">
                                            <img src="App_Themes/GrupoConstancioVieira/Imagens/botoes/login/usuario.jpg" style="vertical-align: top" />&nbsp;</td>
                                        <td style="vertical-align: text-top; width: 138px;">
                                            <asp:TextBox ID="UserName" runat="server" Width="130px" ToolTip="Digite o nome do Usuário"></asp:TextBox>
                                            </td>
                                            <td>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                ErrorMessage="O nome do usuário é requerido." ToolTip="O nome do usuário é requerido."
                                                ValidationGroup="lgnLogin">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <img src="App_Themes/GrupoConstancioVieira/Imagens/botoes/login/senha.jpg" style="vertical-align: text-bottom" />&nbsp;</td>
                                        <td style="vertical-align: top; width: 138px;">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="130px" ToolTip="Digite a senha do Usuário"></asp:TextBox>
                                            </td>
                                            <td>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                ErrorMessage="A senha é requerida." ToolTip="A senha é requerida." ValidationGroup="lgnLogin">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: red">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" ValidationGroup="lgnLogin" ImageUrl="~/App_Themes/GrupoConstancioVieira/Imagens/botoes/login/entrar.jpg" SkinID="Entrar" />
                                        </td>
                                    </tr>
                                </table>                                
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login> 
            </td>
        </tr>        
        </table>        
    </form>
     
</body>
</html>
