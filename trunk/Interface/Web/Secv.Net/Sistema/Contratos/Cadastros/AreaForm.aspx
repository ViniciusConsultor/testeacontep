<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="AreaForm.aspx.cs" Inherits="Sistema_Contratos_Cadastros_AreaForm" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 20%">
                            Área:</td>
                        <td style="width: 70%">
                            <asp:TextBox ID="tbxArea" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnPesquisar" runat="server" SkinID="Pesquisar" OnClick="btnPesquisar_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlAreas" runat="server" DataSourceID="odsArea" DataTextField="Nome"
                                DataValueField="IDArea" AutoPostBack="True">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsArea" runat="server" DeleteMethod="Apagar"
                                InsertMethod="Inserir" OldValuesParameterFormatString="original_{0}" SelectMethod="ObterPor_UQ_Area"
                                TypeName="Acontep.AD.Contratos.AreaAD" UpdateMethod="Alterar">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_IDArea" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="original_IDArea" Type="Int32" />
                                    <asp:Parameter Name="Nome" Type="String" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="tbxArea" Name="Nome" PropertyName="Text" Type="String" />
                                </SelectParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Nome" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnIncluir" runat="server" SkinID="Incluir" OnClick="btnIncluir_Click" />
                            <asp:Button ID="btnAlterar" runat="server" SkinID="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button ID="btnExcluir" runat="server" SkinID="Excluir" OnClick="btnExcluir_Click"
                                OnClientClick="return confirm('Deseja excluir o item selecionado?')" CommandName="Delete" /></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:FormView ID="fvwItem" runat="server" DataSourceID="odsManterItem" DataKeyNames="IDArea"
                                Width="100%">
                                <EditItemTemplate>
                                    <asp:Panel runat="server" ID="pnlEdicao" GroupingText="Edição">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    Nome:</td>
                                                <td>
                                                    <asp:TextBox ID="tbxArea" runat="server" MaxLength="50" Text='<%# Bind("Nome") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right">
                                                    <asp:Button ID="btnConfirmar" runat="server" CommandName="Update" SkinID="Confirmar" />
                                                    <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:Panel runat="server" ID="pnlInclusao" GroupingText="Inclusão">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    Nome:</td>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="tbxArea" runat="server" MaxLength="50" Text='<%# Bind("Nome") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right">
                                                    <asp:Button ID="btnConfirmar" runat="server" CommandName="Insert" SkinID="Confirmar" />
                                                    <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </InsertItemTemplate>
                            </asp:FormView>
                            <asp:ObjectDataSource ID="odsManterItem" runat="server" DeleteMethod="Apagar" InsertMethod="Inserir"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="ObterPor_PK_Area"
                                TypeName="Acontep.AD.Contratos.AreaAD" UpdateMethod="Alterar" OnDeleted="odsManterItem_Deleted" OnInserted="odsManterItem_Inserted" OnUpdated="odsManterItem_Updated">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_IDArea" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="original_IDArea" Type="Int32" />
                                    <asp:Parameter Name="Nome" Type="String" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlAreas" Name="IDArea" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Nome" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
