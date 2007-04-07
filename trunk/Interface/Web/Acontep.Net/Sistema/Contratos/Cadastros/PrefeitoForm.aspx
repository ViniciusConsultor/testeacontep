<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="PrefeitoForm.aspx.cs" Inherits="Sistema_Contratos_Cadastros_PrefeitoForm"
    Title="Untitled Page" %>

<%@ Import Namespace="Acontep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlPesquisa" runat="server" GroupingText="Pesquisa" Width="100%">
        <table>
            <tr>
                <td width="20%">
                    Nome:</td>
                <td width="50%">
                    <asp:TextBox ID="tbxPesquisa" runat="server"></asp:TextBox></td>
                <td width="30%">
                    <asp:Button ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" SkinID="Pesquisar" /></td>
            </tr>
            <tr>
                <td>
                    Resultado:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="ddlResultado" ErrorMessage="RequiredFieldValidator" SkinID="CampoObrigatorio"
                        ValidationGroup="Manutencao"></asp:RequiredFieldValidator></td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlResultado" runat="server" DataSourceID="odsConsulta" DataTextField="Nome"
                        DataValueField="IDPrefeito">
                    </asp:DropDownList><asp:ObjectDataSource ID="odsConsulta" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="ObterPor_UQ_Prefeito" TypeName="Acontep.AD.Contratos.PrefeitoAD">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="tbxPesquisa" Name="Nome" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td class="BotaoAcao" colspan="3">
                    <asp:Button ID="btnIncluir" runat="server" CausesValidation="False" SkinID="Incluir"
                        OnClick="btnIncluir_Click" />
                    <asp:Button ID="btnAlterar" runat="server" SkinID="Alterar" ValidationGroup="Manutencao"
                        OnClick="btnAlterar_Click" />
                    <asp:Button ID="btnExcluir" runat="server" SkinID="Excluir" ValidationGroup="Manutencao" OnClick="btnExcluir_Click" /></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlManutencao" Visible="False" Width="100%">
        <asp:FormView ID="fvwItem" runat="server" DataSourceID="odsManutencao" Width="100%" DataKeyNames="IDPrefeito">
            <InsertItemTemplate>
                <asp:Panel ID="pnlInclusao" runat="server" GroupingText="Inclusão" Width="100%">
                    <table>
                        <tr>
                            <td width=20%>
                                Nome:<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="tbxNome"
                                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator></td>
                            <td width=80%>
                                <asp:TextBox ID="tbxNome" runat="server" Text='<%# Bind("Nome") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Data Nascimento:</td>
                            <td>
                                <ew:CalendarPopup ID="tbxDataNascimento" runat="server" SelectedDate="<%# DateTime.Now %>"
                                    SelectedValue='<%# Bind("Nascimento") %>' VisibleDate="04/07/2007 13:07:02" PostedDate="" UpperBoundDate="12/31/9999 23:59:59">
                                </ew:CalendarPopup>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                E-mail:</td>
                            <td>
                                <asp:TextBox ID="tbxEmail" runat="server" AutoCompleteType="Email" Text='<%# Bind("Email") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Identidade:</td>
                            <td>
                                <asp:TextBox ID="tbxRG" runat="server" MaxLength="15" Text='<%# Bind("RG") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Data emissão:</td>
                            <td>
                                <ew:CalendarPopup ID="tbxDataExpedicao" runat="server" SelectedDate="04/07/2007 13:07:02"
                                    SelectedValue='<%# Bind("DataExpedicao") %>'
                                    VisibleDate="04/07/2007 13:07:02">
                                </ew:CalendarPopup>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Orgão Expedidor:</td>
                            <td>
                                <asp:TextBox ID="tbxOrgao" runat="server" Columns="10" MaxLength="10" Text='<%# Bind("OrgaoExpedidor") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Estado:</td>
                            <td>
                                <asp:DropDownList ID="ddlEstadoExpedidor" runat="server" DataSourceID="odsEstado"
                                    DataTextField="Sigla" DataValueField="IDEstado" SelectedValue='<%# Bind("IDEstadoExpedidor") %>'
                                    AppendDataBoundItems="True" OnDataBinding="NenhumItemSelecionado">
                                </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Logradouro:</td>
                            <td>
                                <asp:TextBox ID="tbxLogradouro" runat="server" Text='<%# Bind("Logradouro") %>' TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Bairro:</td>
                            <td>
                                <asp:TextBox ID="tbxBairro" runat="server" Text='<%# Bind("Bairro") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Cep:</td>
                            <td>
                                <ew:MaskedTextBox ID="tbxCep" runat="server" Mask="99.999-999" SkinID="Cep"></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Estado:</td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstado" DataTextField="Nome"
                                    DataValueField="IDEstado" SelectedValue='<%# Eval("IDEstado") %>' AppendDataBoundItems="True"
                                    OnDataBinding="NenhumItemSelecionado" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                Cidade:</td>
                            <td>
                                <asp:DropDownList ID="ddlCidade" runat="server" DataSourceID="odsCidade" DataTextField="Nome"
                                    DataValueField="IDCidade" AppendDataBoundItems="True" OnDataBinding="NenhumItemSelecionado" SelectedValue='<%# Bind("IDCidade") %>'>
                                </asp:DropDownList><asp:ObjectDataSource ID="odsCidade" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_IX_Municipio" TypeName="Acontep.AD.Cadastros.MunicipioAD">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlEstado" Name="IDEstado" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telefone:</td>
                            <td>
                                <ew:MaskedTextBox ID="tbxTelefone" runat="server" SkinID="Telefone" Text='<%# Bind("Telefone") %>'></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Celular:</td>
                            <td>
                                <ew:MaskedTextBox ID="tbxFax" runat="server" SkinID="Telefone" Text='<%# Bind("Celular") %>'></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Nome Cônjuge:</td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("NomeConjuge") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Data nascimento cônjuge:</td>
                            <td>
                                <ew:CalendarPopup ID="tbxDataNascimentoConjuge" runat="server" SelectedDate="<%# DateTime.Now %>"
                                    SelectedValue='<%# Bind("DataNascimentoConjuge") %>'
                                    VisibleDate="04/07/2007 13:07:02">
                                </ew:CalendarPopup>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="BotaoAcao">
                                <asp:Button ID="btnConfirmar" runat="server" CommandName="Insert" SkinID="Confirmar" />
                                <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" OnClick="btnCancelar_Click"
                                    SkinID="Cancelar" CausesValidation="False" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnlInclusao" runat="server" GroupingText="Alteração" Width="100%">
                    <table>
                        <tr>
                            <td width=20%>
                                Nome:<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="tbxNome"
                                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator></td>
                            <td width=80%>
                                <asp:TextBox ID="tbxNome" runat="server" Text='<%# Bind("Nome") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Data Nascimento:</td>
                            <td>
                                <ew:CalendarPopup ID="tbxDataNascimento" runat="server" SelectedDate="<%# DateTime.Now %>"
                                    SelectedValue='<%# Bind("Nascimento") %>' VisibleDate="04/07/2007 13:07:02" PostedDate="" UpperBoundDate="12/31/9999 23:59:59" AllowArbitraryText="True" DisableTextBoxEntry="False">
                                </ew:CalendarPopup>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                E-mail:</td>
                            <td>
                                <asp:TextBox ID="tbxEmail" runat="server" AutoCompleteType="Email" Text='<%# Bind("Email") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Identidade:</td>
                            <td>
                                <asp:TextBox ID="tbxRG" runat="server" MaxLength="15" Text='<%# Bind("RG") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Data emissão:</td>
                            <td>
                                <ew:CalendarPopup ID="tbxDataExpedicao" runat="server" SelectedDate="04/07/2007 13:07:02"
                                    SelectedValue='<%# Bind("DataExpedicao") %>'
                                    VisibleDate="04/07/2007 13:07:02" AllowArbitraryText="True" DisableTextBoxEntry="False">
                                </ew:CalendarPopup>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Orgão Expedidor:</td>
                            <td>
                                <asp:TextBox ID="tbxOrgao" runat="server" Columns="10" MaxLength="10" Text='<%# Bind("OrgaoExpedidor") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Estado:</td>
                            <td>
                                <asp:DropDownList ID="ddlEstadoExpedidor" runat="server" DataSourceID="odsEstado"
                                    DataTextField="Sigla" DataValueField="IDEstado" SelectedValue='<%# Bind("IDEstadoExpedidor") %>'
                                    AppendDataBoundItems="True" OnDataBinding="NenhumItemSelecionado">
                                </asp:DropDownList><asp:ObjectDataSource ID="odsEstado" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Logradouro:</td>
                            <td>
                                <asp:TextBox ID="tbxLogradouro" runat="server" Text='<%# Bind("Logradouro") %>' TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Bairro:</td>
                            <td>
                                <asp:TextBox ID="tbxBairro" runat="server" Text='<%# Bind("Bairro") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Cep:</td>
                            <td>
                                <ew:MaskedTextBox ID="tbxCep" runat="server" Mask="99.999-999" SkinID="Cep" Text='<%# Formatacao.FormatarCep(Eval("Cep")) %>'></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Estado:</td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstadoPrefeito" DataTextField="Nome"
                                    DataValueField="IDEstado" SelectedValue='<%# Eval("IDEstado") %>' AppendDataBoundItems="True"
                                    OnDataBinding="NenhumItemSelecionado" AutoPostBack="True">
                                </asp:DropDownList><asp:ObjectDataSource ID="odsEstadoPrefeito" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_UQ_Nome" TypeName="Acontep.AD.Cadastros.EstadoAD">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cidade:</td>
                            <td>
                                <asp:DropDownList ID="ddlCidade" runat="server" DataSourceID="odsCidade" DataTextField="Nome"
                                    DataValueField="IDCidade" AppendDataBoundItems="True" OnDataBinding="NenhumItemSelecionado" SelectedValue='<%# Eval("IDCidade") %>' OnDataBound="ddlCidade_DataBound">
                                </asp:DropDownList><asp:ObjectDataSource ID="odsCidade" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="ObterPor_IX_Municipio" TypeName="Acontep.AD.Cadastros.MunicipioAD">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlEstado" Name="IDEstado" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="%" Name="Nome" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telefone:</td>
                            <td>
                                <ew:MaskedTextBox ID="tbxTelefone" runat="server" SkinID="Telefone" Text='<%# Bind("Telefone") %>'></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Celular:</td>
                            <td>
                                <ew:MaskedTextBox ID="tbxFax" runat="server" SkinID="Telefone" Text='<%# Bind("Celular") %>'></ew:MaskedTextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Nome Cônjuge:</td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("NomeConjuge") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Data nascimento cônjuge:</td>
                            <td>
                                <ew:CalendarPopup ID="tbxDataNascimentoConjuge" runat="server" SelectedDate="<%# DateTime.Now %>"
                                    SelectedValue='<%# Bind("DataNascimentoConjuge") %>'
                                    VisibleDate="04/07/2007 13:07:02" AllowArbitraryText="True" DisableTextBoxEntry="False">
                                </ew:CalendarPopup>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="BotaoAcao">
                                <asp:Button ID="btnConfirmar" runat="server" CommandName="Update" SkinID="Confirmar" />
                                <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" OnClick="btnCancelar_Click"
                                    SkinID="Cancelar" CausesValidation="False" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsManutencao" runat="server" DeleteMethod="Apagar" InsertMethod="Inserir"
            OldValuesParameterFormatString="original_{0}" OnInserted="odsManutencao_Updated"
            OnUpdated="odsManutencao_Updated" SelectMethod="ObterPorID" TypeName="Acontep.AD.Contratos.PrefeitoAD"
            UpdateMethod="Alterar" OnInserting="odsManutencao_Inserting" OnSelected="odsManutencao_Selected" OnUpdating="odsManutencao_Inserting">
            <DeleteParameters>
                <asp:Parameter Name="original_IDPrefeito" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="original_IDPrefeito" Type="Int32" />
                <asp:Parameter Name="Nome" Type="String" />
                <asp:Parameter Name="RG" Type="String" />
                <asp:Parameter Name="OrgaoExpedidor" Type="String" />
                <asp:Parameter Name="IDEstadoExpedidor" Type="Int32" />
                <asp:Parameter Name="DataExpedicao" Type="DateTime" />
                <asp:Parameter Name="Logradouro" Type="String" />
                <asp:Parameter Name="Bairro" Type="String" />
                <asp:Parameter Name="IDCidade" Type="Int32" />
                <asp:Parameter Name="CEP" Type="String" />
                <asp:Parameter Name="Nascimento" Type="DateTime" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Telefone" Type="String" />
                <asp:Parameter Name="Celular" Type="String" />
                <asp:Parameter Name="NomeConjuge" Type="String" />
                <asp:Parameter Name="DataNascimentoConjuge" Type="DateTime" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlResultado" Name="IDPrefeito" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="Nome" Type="String" />
                <asp:Parameter Name="RG" Type="String" />
                <asp:Parameter Name="OrgaoExpedidor" Type="String" />
                <asp:Parameter Name="IDEstadoExpedidor" Type="Int32" />
                <asp:Parameter Name="DataExpedicao" Type="DateTime" />
                <asp:Parameter Name="Logradouro" Type="String" />
                <asp:Parameter Name="Bairro" Type="String" />
                <asp:Parameter Name="IDCidade" Type="Int32" />
                <asp:Parameter Name="CEP" Type="String" />
                <asp:Parameter Name="Nascimento" Type="DateTime" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Telefone" Type="String" />
                <asp:Parameter Name="Celular" Type="String" />
                <asp:Parameter Name="NomeConjuge" Type="String" />
                <asp:Parameter Name="DataNascimentoConjuge" Type="DateTime" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
