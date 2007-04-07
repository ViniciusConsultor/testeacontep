<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="MunicipioForm.aspx.cs" Inherits="Sistema_Contratos_Cadastros_MunicipioForm"
    Title="Untitled Page" %>
<%@ Import Namespace="Acontep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td nowrap="nowrap" style="width: 20%">
                            Estado:</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstado" DataTextField="Nome"
                                DataValueField="IDEstado" OnDataBound="ddlEstado_DataBound">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" nowrap="nowrap">
                            Municipio:</td>
                        <td style="width: 70%">
                            <asp:TextBox ID="tbxMunicipio" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnPesquisar" runat="server" SkinID="Pesquisar" OnClick="btnPesquisar_Click" CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlMunicipio" runat="server" DataSourceID="odsMunicipio"
                                DataTextField="Nome" DataValueField="IDCidade" AutoPostBack="True">
                            </asp:DropDownList><asp:ObjectDataSource ID="odsMunicipio" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ObterPor_IX_Municipio"
                                TypeName="Acontep.AD.Cadastros.MunicipioAD">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlEstado" Name="IDEstado" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="tbxMunicipio" Name="Nome" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnIncluir" runat="server" SkinID="Incluir" OnClick="btnIncluir_Click" CausesValidation="False" />
                            <asp:Button ID="btnAlterar" runat="server" SkinID="Alterar" OnClick="btnAlterar_Click" CausesValidation="False" />
                            <asp:Button ID="btnExcluir" runat="server" SkinID="Excluir" OnClick="btnExcluir_Click"
                                OnClientClick="return confirm('Deseja excluir o item selecionado?')" CommandName="Delete" /></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:FormView ID="fvwItem" runat="server" DataSourceID="odsManterItem" DataKeyNames="IDCidade"
                                Width="100%">
                                <EditItemTemplate>
                                    <asp:Panel runat="server" ID="pnlEdicao" GroupingText="Edição"><table style="width: 100%">
                                        <tr>
                                            <td style="width: 20%">
                                                Estado:</td>
                                            <td style="width: 80%">
                                                <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstado" DataTextField="Nome"
                                                        DataValueField="IDEstado" SelectedValue='<%# Bind("IDEstado") %>' OnDataBound="ddlEstado_DataBound">
                                                </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Nome:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNome"
                                                    ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                                            <td style="width: 80%; color: #000000;">
                                                <asp:TextBox ID="tbxNome" runat="server" MaxLength="50" Text='<%# Bind("Nome") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td style="width: 20%">
                                                Nome completo:</td>
                                            <td style="width: 80%">
                                                <asp:TextBox ID="tbxNomeCompleto" runat="server" MaxLength="100" Text='<%# Bind("NomeCompleto") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Logradouro:</td>
                                            <td style="width: 80%">
                                                <asp:TextBox ID="tbxLogradouro" runat="server" MaxLength="200" Text='<%# Bind("Logradouro") %>'
                                                    TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Bairro:</td>
                                            <td style="width: 80%">
                                                <asp:TextBox ID="tbxBairro" runat="server" MaxLength="200" Text='<%# Bind("Bairro") %>'></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Cep:</td>
                                            <td style="width: 80%">
                                                <ew:MaskedTextBox ID="tbxCep" runat="server" Mask="99.999-999" Text='<%# Formatacao.FormatarCep(Eval("Cep")) %>'></ew:MaskedTextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Telefone:</td>
                                            <td style="width: 80%">
                                                <ew:MaskedTextBox ID="tbxTelefone" runat="server" Mask="(99)9999-9999" Text='<%# Bind("Telefone") %>'></ew:MaskedTextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Fax:</td>
                                            <td style="width: 80%">
                                                <ew:MaskedTextBox ID="tbxFax" runat="server" Mask="(99)9999-9999" Text='<%# Bind("Fax") %>'></ew:MaskedTextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                E-mail:</td>
                                            <td style="width: 80%">
                                                <ew:MaskedTextBox ID="tbxEmail" runat="server" AutoCompleteType="Email" Text='<%# Bind("Email") %>'></ew:MaskedTextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                Prefeito:</td>
                                            <td style="width: 80%">
                                                <asp:DropDownList ID="ddlPrefeitos" runat="server" AppendDataBoundItems="True" DataSourceID="odsPrefeitos"
                                                    DataTextField="Nome" DataValueField="IDPrefeito" OnDataBinding="ddlPrefeitos_DataBinding"
                                                    SelectedValue='<%# Bind("IDPrefeito") %>'>
                                                </asp:DropDownList><asp:ObjectDataSource ID="odsPrefeitos" runat="server" OldValuesParameterFormatString="original_{0}"
                                                    SelectMethod="ObterPor_UQ_Prefeito" TypeName="Acontep.AD.Contratos.PrefeitoAD">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: right">
                                                <asp:Button ID="btnConfirmar" runat="server" CommandName="Update" SkinID="Confirmar" />
                                                <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" CausesValidation="False" OnClick="btnCancelar_Click" /></td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:Panel runat="server" ID="pnlInclusao" GroupingText="Inclusão">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    Estado:</td>
                                                <td style="width: 80%">
                                                    <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstado" DataTextField="Nome"
                                                        DataValueField="IDEstado" SelectedValue='<%# Bind("IDEstado") %>' OnDataBound="ddlEstado_DataBound">
                                                    </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Nome:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNome"
                                                        ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="tbxNome" runat="server" MaxLength="50" Text='<%# Bind("Nome") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Nome completo:</td>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="tbxNomeCompleto" runat="server" MaxLength="100" Text='<%# Bind("NomeCompleto") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Logradouro:</td>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="tbxLogradouro" runat="server" MaxLength="200" Text='<%# Bind("Logradouro") %>'
                                                        TextMode="MultiLine"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Bairro:</td>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="tbxBairro" runat="server" MaxLength="200" Text='<%# Bind("Bairro") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Cep:</td>
                                                <td style="width: 80%">
                                                    <ew:MaskedTextBox ID="tbxCep" runat="server" Mask="99.999-999"></ew:MaskedTextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Telefone:</td>
                                                <td style="width: 80%">
                                                    <ew:MaskedTextBox ID="tbxTelefone" runat="server" Mask="(99)9999-9999" Text='<%# Bind("Telefone") %>'></ew:MaskedTextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Fax:</td>
                                                <td style="width: 80%">
                                                    <ew:MaskedTextBox ID="tbxFax" runat="server" Mask="(99)9999-9999" Text='<%# Bind("Fax") %>'></ew:MaskedTextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    E-mail:</td>
                                                <td style="width: 80%">
                                                    <ew:MaskedTextBox ID="tbxEmail" runat="server" AutoCompleteType="Email" Text='<%# Bind("Email") %>'></ew:MaskedTextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    Prefeito:</td>
                                                <td style="width: 80%">
                                                    <asp:DropDownList ID="ddlPrefeitos" runat="server" AppendDataBoundItems="True" DataSourceID="odsPrefeitos"
                                                        DataTextField="Nome" DataValueField="IDPrefeito" OnDataBinding="ddlPrefeitos_DataBinding"
                                                        SelectedValue='<%# Bind("IDPrefeito") %>'>
                                                    </asp:DropDownList><asp:ObjectDataSource ID="odsPrefeitos" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="ObterPor_UQ_Prefeito" TypeName="Acontep.AD.Contratos.PrefeitoAD">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right">
                                                    <asp:Button ID="btnConfirmar" runat="server" CommandName="Insert" SkinID="Confirmar" />
                                                    <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" SkinID="Cancelar" CausesValidation="False" OnClick="btnCancelar_Click" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </InsertItemTemplate>
                            </asp:FormView>
                            <asp:ObjectDataSource ID="odsManterItem" runat="server" DeleteMethod="Apagar" InsertMethod="Inserir"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="ObterPor_PK_Municipio"
                                TypeName="Acontep.AD.Cadastros.MunicipioAD" UpdateMethod="Alterar" OnDeleted="odsManterItem_Deleted"
                                OnInserted="odsManterItem_Inserted" OnUpdated="odsManterItem_Updated" OnInserting="odsManterItem_Updating" OnUpdating="odsManterItem_Updating">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_IDCidade" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="original_IDCidade" Type="Int32" />
                                    <asp:Parameter Name="IDEstado" Type="Int32" />
                                    <asp:Parameter Name="Nome" Type="String" />
                                    <asp:Parameter Name="NomeCompleto" Type="String" />
                                    <asp:Parameter Name="Logradouro" Type="String" />
                                    <asp:Parameter Name="Bairro" Type="String" />
                                    <asp:Parameter Name="Cep" Type="String" />
                                    <asp:Parameter Name="Telefone" Type="String" />
                                    <asp:Parameter Name="Fax" Type="String" />
                                    <asp:Parameter Name="Email" Type="String" />
                                    <asp:Parameter Name="IDPrefeito" Type="Int32" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlMunicipio" Name="IDCidade" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="IDEstado" Type="Int32" />
                                    <asp:Parameter Name="Nome" Type="String" />
                                    <asp:Parameter Name="NomeCompleto" Type="String" />
                                    <asp:Parameter Name="Logradouro" Type="String" />
                                    <asp:Parameter Name="Bairro" Type="String" />
                                    <asp:Parameter Name="Cep" Type="String" />
                                    <asp:Parameter Name="Telefone" Type="String" />
                                    <asp:Parameter Name="Fax" Type="String" />
                                    <asp:Parameter Name="Email" Type="String" />
                                    <asp:Parameter Name="IDPrefeito" Type="Int32" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
