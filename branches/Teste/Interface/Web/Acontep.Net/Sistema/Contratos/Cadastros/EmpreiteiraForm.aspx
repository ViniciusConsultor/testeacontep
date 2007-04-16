<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmpreiteiraForm.aspx.cs" Inherits="Sistema_Contratos_Cadastros_EmpreiteiraForm"
    Title="Untitled Page" %>
<%@ Import Namespace="Acontep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="pnlPesquisa">
        <table width="100%">
            <tr>
                <td style="width: 30%">
                    Pesquisar por:</td>
                <td style="width: 70%">
                    <asp:DropDownList ID="ddlTipoPesquisa" runat="server">
                        <asp:ListItem Value="CNPJ">CNPJ</asp:ListItem>
                        <asp:ListItem Value="RazaoSocial">Raz&#227;o Social</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Fantasia">Nome Fantasia</asp:ListItem>
                        <asp:ListItem Value="Responsavel">Respons&#225;vel</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 30%">
                    Valor:</td>
                <td style="width: 70%">
                    <asp:TextBox ID="tbxValor" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="BotaoAcao" colspan="2">
                    <asp:Button ID="btnPesquisar" runat="server" SkinID="Pesquisar" OnClick="btnPesquisar_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlResultado" runat="server" GroupingText="Empreiteira" Height="300px"
                        Width="100%" ScrollBars="Auto">
                        <x:GridView ID="gvwEmpreiteira" runat="server" AscImage="" AutoGenerateColumns="False"
                            AutoGenerateColumnSelect="RadioButton" AutoGenerateColumnSelectAutoPostBack="True"
                            AutoGenerateSelectButton="True" DescImage="" GenerateColumnSelectIndex="0" ModelSelect="ColumnAndRowSelected"
                            Width="100%" DataSourceID="odsItem" DataKeyNames="IDEmpreiteira" WebControlCustomColection="">
                            <Columns>
                                <asp:TemplateField HeaderText="CNPJ">
                                    <edititemtemplate>
<asp:TextBox runat="server" Text='<%# Formatacao.FormatarCnpj(Eval("CNPJ")) %>' id="TextBox1"></asp:TextBox>
</edititemtemplate>
                                    <itemtemplate>
<asp:Label runat="server" Text='<%# Formatacao.FormatarCnpj(Eval("CNPJ") ) %>' id="Label1"></asp:Label>
</itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RazaoSocial" HeaderText="Raz&#227;o Social" />
                                <asp:BoundField DataField="Fantasia" HeaderText="Fantasia" />
                                <asp:BoundField DataField="Responsavel" HeaderText="Respons&#225;vel" />
                            </Columns>
                        </x:GridView>
                        <asp:ObjectDataSource ID="odsItem" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="ObterEmpreiteira" TypeName="Acontep.AD.Contratos.EmpreiteiraAD" >
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlTipoPesquisa" Name="Filtro" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:ControlParameter ControlID="tbxValor" Name="ValorFiltro" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnIncluir" runat="server" SkinID="Incluir" OnClick="btnIncluir_Click" />
                    <asp:Button ID="btnAlterar" runat="server" SkinID="Alterar" OnClick="btnAlterar_Click" />
                    <asp:Button ID="btnExcluir" runat="server" SkinID="Excluir" CommandName="Delete" OnClick="btnExcluir_Click" OnClientClick="confirm('Deseja excluir o item selecionado?');" /></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlManutencao" GroupingText="Empreiteira:" Width="100%" Visible="False">
        <asp:FormView ID="fvwItem" runat="server" DataSourceID="odsManutencao" DataKeyNames="IDEmpreiteira" Width="100%">
            <InsertItemTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 20%">
                            CNPJ:<asp:RequiredFieldValidator ID="rfvCNPJ" runat="server" ControlToValidate="tbxCNPJ"
                                ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                        <td width="80%">
                            <ew:MaskedTextBox ID="tbxCNPJ" runat="server" Mask="99.999.999/9999-99"></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Razão Social:<asp:RequiredFieldValidator ID="rfvRazaoSocial" runat="server" ControlToValidate="tbxRazaoSocial"
                                ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                        <td>
                            <asp:TextBox ID="tbxRazaoSocial" runat="server" MaxLength="200" Text='<%# Bind("RazaoSocial") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Fantasia:<asp:RequiredFieldValidator ID="rfvFantasia" runat="server" ControlToValidate="tbxFantasia"
                                ErrorMessage="*" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                        <td>
                            <asp:TextBox ID="tbxFantasia" runat="server" MaxLength="200" Text='<%# Bind("Fantasia") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Logradouro:</td>
                        <td>
                            <asp:TextBox ID="tbxLogradouro" runat="server" MaxLength="200" Text='<%# Bind("Logradouro") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Cep:</td>
                        <td>
                            <ew:MaskedTextBox ID="tbxCep" runat="server" Mask="99.999-999"></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Bairro:</td>
                        <td>
                            <asp:TextBox ID="tbxBairro" runat="server" MaxLength="100" Text='<%# Bind("Bairro") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Estado:</td>
                        <td>
                            <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstado" DataTextField="Nome"
                                DataValueField="IDEstado" SelectedValue='<%# IDEstadoPadrao %>' AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            Cidade:</td>
                        <td>
                            <asp:DropDownList ID="ddlCidade" runat="server" DataSourceID="odsCidade" DataTextField="Nome"
                                DataValueField="IDCidade" AppendDataBoundItems="True" OnDataBinding="TextoNenumItemInformado">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            Telefone:</td>
                        <td>
                            <asp:TextBox ID="tbxTelefone" runat="server" MaxLength="15" Text='<%# Bind("Telefone") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Fax:</td>
                        <td>
                            <asp:TextBox ID="tbxFax" runat="server" MaxLength="15" Text='<%# Bind("Fax") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Responsável:</td>
                        <td>
                            <asp:TextBox ID="tbxResponsavel" runat="server" MaxLength="200" Text='<%# Bind("Responsavel") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            E-mail:</td>
                        <td>
                            <asp:TextBox ID="tbxEmail" runat="server" MaxLength="200" Text='<%# Bind("Email") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Conta:</td>
                        <td>
                            <asp:TextBox ID="tbxConta" runat="server" MaxLength="50" Text='<%# Bind("Conta") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Agência:</td>
                        <td>
                            <asp:TextBox ID="tbxAgencia" runat="server" MaxLength="50" Text='<%# Bind("Agencia") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Banco:</td>
                        <td>
                            <ew:NumericBox ID="tbxBanco" runat="server" MaxLength="3" SkinID="Inteiro" Text='<%# Bind("IDBanco") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Tipo de Operação:</td>
                        <td>
                            <asp:TextBox ID="tbxTipoOperacao" runat="server" MaxLength="3" Text='<%# Bind("TipoOperacao") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="BotaoAcao" colspan="2">
                            <asp:Button ID="btnConfirmar" runat="server" CommandName="Insert" SkinID="Confirmar" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" SkinID="Cancelar" OnClick="btnCancel_Click" /></td>
                    </tr>
                </table>
        <asp:ObjectDataSource ID="odsCidade" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ObterPor_IX_Municipio" TypeName="Acontep.AD.Cadastros.MunicipioAD">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlEstado" Name="IDEstado" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
            <SelectParameters>
                <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </InsertItemTemplate>
            <EditItemTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            CNPJ:<asp:RequiredFieldValidator ID="rfvCNPJ" runat="server" ControlToValidate="tbxCNPJ"
                                ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                        <td width="80%" style="color: #000000">
                            <ew:MaskedTextBox ID="tbxCNPJ" runat="server" Mask="99.999.999/9999-99" Text='<%# Formatacao.FormatarCnpj(Eval("CNPJ")) %>'></ew:MaskedTextBox></td>
                    </tr>
                    <tr style="color: #000000">
                        <td>
                            Razão Social:<asp:RequiredFieldValidator ID="rfvRazaoSocial" runat="server" ControlToValidate="tbxRazaoSocial"
                                ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                        <td style="color: #000000">
                            <asp:TextBox ID="tbxRazaoSocial" runat="server" MaxLength="200" Text='<%# Bind("RazaoSocial") %>'></asp:TextBox></td>
                    </tr>
                    <tr style="color: #000000">
                        <td>
                            Fantasia:<asp:RequiredFieldValidator ID="rfvFantasia" runat="server" ControlToValidate="tbxFantasia"
                                ErrorMessage="*" SkinID="CampoObrigatorio"></asp:RequiredFieldValidator></td>
                        <td style="color: #000000">
                            <asp:TextBox ID="tbxFantasia" runat="server" MaxLength="200" Text='<%# Bind("Fantasia") %>'></asp:TextBox></td>
                    </tr>
                    <tr style="color: #000000">
                        <td>
                            Logradouro:</td>
                        <td>
                            <asp:TextBox ID="tbxLogradouro" runat="server" MaxLength="200" Text='<%# Bind("Logradouro") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Cep:</td>
                        <td>
                            <ew:MaskedTextBox ID="tbxCep" runat="server" Mask="99.999-999" Text='<%# Formatacao.FormatarCep(Eval("Cep")) %>'></ew:MaskedTextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Bairro:</td>
                        <td>
                            <asp:TextBox ID="tbxBairro" runat="server" MaxLength="100" Text='<%# Bind("Bairro") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Estado:</td>
                        <td>
                            <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstado" DataTextField="Nome"
                                DataValueField="IDEstado" SelectedValue='<%# Eval("IDEstado") %>' AutoPostBack="True" AppendDataBoundItems="True" OnDataBinding="TextoNenumItemInformado">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            Cidade:</td>
                        <td>
                            <asp:DropDownList ID="ddlCidade" runat="server" DataSourceID="odsCidade" DataTextField="Nome"
                                DataValueField="IDCidade" AppendDataBoundItems="True" OnDataBinding="TextoNenumItemInformado" OnDataBound="ddlCidade_DataBound">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            Telefone:</td>
                        <td>
                            <asp:TextBox ID="tbxTelefone" runat="server" MaxLength="15" Text='<%# Bind("Telefone") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Fax:</td>
                        <td>
                            <asp:TextBox ID="tbxFax" runat="server" MaxLength="15" Text='<%# Bind("Fax") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Responsável:</td>
                        <td>
                            <asp:TextBox ID="tbxResponsavel" runat="server" MaxLength="200" Text='<%# Bind("Responsavel") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            E-mail:</td>
                        <td>
                            <asp:TextBox ID="tbxEmail" runat="server" MaxLength="200" Text='<%# Bind("Email") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Conta:</td>
                        <td>
                            <asp:TextBox ID="tbxConta" runat="server" MaxLength="50" Text='<%# Bind("Conta") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Agência:</td>
                        <td>
                            <asp:TextBox ID="tbxAgencia" runat="server" MaxLength="50" Text='<%# Bind("Agencia") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Banco:</td>
                        <td>
                            <ew:NumericBox ID="tbxBanco" runat="server" MaxLength="3" SkinID="Inteiro" Text='<%# Bind("IDBanco") %>'></ew:NumericBox></td>
                    </tr>
                    <tr>
                        <td>
                            Tipo de Operação:</td>
                        <td>
                            <asp:TextBox ID="tbxTipoOperacao" runat="server" MaxLength="3" Text='<%# Bind("TipoOperacao") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="BotaoAcao" colspan="2">
                            <asp:Button ID="btnConfirmar" runat="server" CommandName="Update" SkinID="Confirmar" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" SkinID="Cancelar" OnClick="btnCancel_Click" /></td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="odsCidade" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ObterPor_IX_Municipio" TypeName="Acontep.AD.Cadastros.MunicipioAD">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlEstado" Name="IDEstado" PropertyName="SelectedValue"
                    Type="Int32" />
                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </EditItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsManutencao" runat="server" DeleteMethod="Apagar" InsertMethod="Inserir"
            OldValuesParameterFormatString="original_{0}" SelectMethod="ObterEmpreiteiraPorID"
            TypeName="Acontep.AD.Contratos.EmpreiteiraAD" UpdateMethod="Alterar" OnInserting="odsManutencao_Inserting" OnInserted="odsItem_Updated" OnSelected="odsManutencao_Selected" OnUpdated="odsItem_Updated" OnUpdating="odsManutencao_Inserting">
            <DeleteParameters>
                <asp:Parameter Name="original_IDEmpreiteira" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="original_IDEmpreiteira" Type="Int32" />
                <asp:Parameter Name="CNPJ" Type="String" />
                <asp:Parameter Name="RazaoSocial" Type="String" />
                <asp:Parameter Name="Fantasia" Type="String" />
                <asp:Parameter Name="Logradouro" Type="String" />
                <asp:Parameter Name="Bairro" Type="String" />
                <asp:Parameter Name="IDCidade" Type="Int32" />
                <asp:Parameter Name="Cep" Type="Int32" />
                <asp:Parameter Name="Telefone" Type="String" />
                <asp:Parameter Name="Fax" Type="String" />
                <asp:Parameter Name="Responsavel" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="IDBanco" Type="String" />
                <asp:Parameter Name="Conta" Type="String" />
                <asp:Parameter Name="Agencia" Type="String" />
                <asp:Parameter Name="TipoOperacao" Type="String" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="gvwEmpreiteira" Name="IDEmpreiteira" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="CNPJ" Type="String" />
                <asp:Parameter Name="RazaoSocial" Type="String" />
                <asp:Parameter Name="Fantasia" Type="String" />
                <asp:Parameter Name="Logradouro" Type="String" />
                <asp:Parameter Name="Bairro" Type="String" />
                <asp:Parameter Name="IDCidade" Type="Int32" />
                <asp:Parameter Name="Cep" Type="Int32" />
                <asp:Parameter Name="Telefone" Type="String" />
                <asp:Parameter Name="Fax" Type="String" />
                <asp:Parameter Name="Responsavel" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="IDBanco" Type="String" />
                <asp:Parameter Name="Conta" Type="String" />
                <asp:Parameter Name="Agencia" Type="String" />
                <asp:Parameter Name="TipoOperacao" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
