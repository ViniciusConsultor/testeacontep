<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="Panel5" SkinID="Accordion">
        <asp:Panel runat="server" ID="Panel6" SkinID="Accordion-Titulo">
            Grid</asp:Panel>
        <asp:Panel runat="server" ID="Panel7" SkinID="Accordion-Conteudo">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="5"
                UseAccessibleHeader="true">
                <Columns>
                    <asp:BoundField DataField="nome" HeaderText="Nome" />
                    <asp:BoundField DataField="email" HeaderText="email" />
                    <asp:BoundField DataField="telefone" HeaderText="telefone" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" UseAccessibleHeader="true">
                <Columns>
                    <asp:BoundField DataField="nome" HeaderText="Nome" />
                    <asp:BoundField DataField="email" HeaderText="email" />
                    <asp:BoundField DataField="telefone" HeaderText="telefone" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>
    </form>
</asp:Content>
