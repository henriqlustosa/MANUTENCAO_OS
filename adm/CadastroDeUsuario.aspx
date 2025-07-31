<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CadastroDeUsuario.aspx.cs" Inherits="administrativo_CadastroDeUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="../bootstrap5/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../bootstrap5/dist/js/bootstrap.bundle.min.js"></script>    
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    <style type="text/css">
        input[type=radio], input[type=CheckBox] {
            border: 2px;
            width: 20px;
            height: 15px;
            vertical-align: middle;
        }
    </style>
    <%--Feito por henrique--%>
    <%-- --%>  <script src='<%= ResolveUrl("~/moment/jquery-3.7.0.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/moment/moment.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/moment/jquery.dataTables.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/moment/datetime.js") %>' charset="utf8" type="text/javascript"></script>
    <link href="../js/jquery.dataTable.css" rel="stylesheet" />
    <link href="../js/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-ui.js"></script>

    <link href="../js_/chosen.min.css" rel="stylesheet" />
    <script src="../js_/chosen.jquery.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h2 class="text-center">Cadastro de Usuário</h2>
        <div class="row">

            <div class="col-2">
                <label class="form-label">Login de Rede:</label>
                <asp:TextBox ID="txtLogin" runat="server" Class="form-control" />
            </div>
            <div class="col-1 m-2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBuscarAD" runat="server" Text="Pesquisar" Width="100" Class="btn btn-outline-primary" OnClick="btnBuscarAD_Click" />
            </div>

            &nbsp;&nbsp;&nbsp;&nbsp;
        <div class="col-5">
            <label>Nome:</label>
            <asp:TextBox ID="txtNome" runat="server" Class="form-control" />
        </div>

            <div class="col-3">
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" Class="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                RF ou CPF(terceiro):
              <asp:TextBox ID="txtRF" runat="server" class="form-control" ForeColor="Black" MaxLength="13"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNomeRe" runat="server"
                    ControlToValidate="txtRF"
                    ErrorMessage="Campo obrigatório"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="Cadastro" />
            </div>
            <div class="col-2">
                Ramal:
                <asp:TextBox ID="txtRamal1" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server"
                    ControlToValidate="txtRamal1"
                    ErrorMessage="Campo obrigatório"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="Cadastro" />
            </div>
            <div class="col-7">
                Centro de Custo
                   <select data-placeholder="Selecione uma opção" id="select1" multiple style="height: 4000px"
                       runat="server" class="form-control" clientidmode="Static">
                   </select>
                <%--   Setor:
                <asp:TextBox ID="txtSetor" runat="server" class="form-control"  MaxLength="100"></asp:TextBox>
                  <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server"
                        controltovalidate="txtSetor"
                        errormessage="Campo obrigatório"
                        display="Dynamic"
                        forecolor="Red"
                        validationgroup="Cadastro" />--%>
            </div>


        </div>

        <%--  <br />
        <div class="row col-12">
            <select data-placeholder="Selecione uma opção" id="select1" multiple style="width: 1220px; height: 4000px"
                runat="server" clientidmode="Static" cssclass="form-control">
            </select>
        </div>
        <br />--%>



        <div>
            <label class="form-label">Perfis de Acesso:</label>
            <asp:CheckBoxList ID="cblPerfis" runat="server" RepeatDirection="Horizontal" Width="400px" />
        </div>

        <div style="text-align: center;">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-outline-primary" OnClick="btnSalvar_Click" ValidationGroup="Cadastro" />
        </div>
        <br />
        <asp:Label ID="lblResultado" runat="server" ForeColor="Green" />
        <div>
            <h3>Usuários Cadastrados</h3>
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Id" OnRowCommand="gvUsuarios_RowCommand" Class="table table-bordered" HorizontalAlign="NotSet">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="LoginRede" HeaderText="Login" />
                    <asp:BoundField DataField="NomeCompleto" HeaderText="Nome" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="rf_Coordenador" HeaderText="RF" />
                    <asp:BoundField DataField="ramal_Coordenador" HeaderText="Ramal" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnCentroCusto" runat="server" class="btn btn-outline-primary" Width="150"
                                CommandName="centro_custo"
                                CommandArgument='<%# Eval("LoginRede") %>'
                                Text="Centro de Custo" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnExcluir" runat="server" class="btn btn-outline-danger" Width="80" CommandName="Inativar"
                                CommandArgument='<%# Eval("Id") %>' Text="Inativar"
                                OnClientClick="return confirm('Tem certeza que deseja inativar este usuário?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
         <div>
            <h3>Usuários Inativos</h3>
            <asp:GridView ID="gvUsuariosInativos" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Id" OnRowCommand="gvUsuariosInativos_RowCommand" Class="table table-bordered" HorizontalAlign="NotSet">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="LoginRede" HeaderText="Login" />
                    <asp:BoundField DataField="NomeCompleto" HeaderText="Nome" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="rf_Coordenador" HeaderText="RF" />
                    <asp:BoundField DataField="ramal_Coordenador" HeaderText="Ramal" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" class="btn btn-outline-primary" Width="150"
                                CommandName="centro_custo"
                                CommandArgument='<%# Eval("LoginRede") %>'
                                Text="Centro de Custo" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnInativar" runat="server" class="btn btn-outline-secondary" Width="80" CommandName="Ativar"
                                CommandArgument='<%# Eval("Id") %>' Text="Ativar"
                                OnClientClick="return confirm('Tem certeza que deseja Ativar este usuário?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Modal Bootstrap -->
        <div class="modal fade" id="modalCentroCusto" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <!-- modal-lg para caber o grid -->
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Centros de Custo do Usuário</h5>                    
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvCentroCusto" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="gvCentroCusto_RowCommand"
                            CssClass="table table-bordered table-striped" >
                            <Columns>
                                <asp:BoundField DataField="NomeCompleto" HeaderText="Nome" />
                                <asp:BoundField DataField="SET_A_COD" HeaderText="Código" />
                                <asp:BoundField DataField="SET_A_DES" HeaderText="Descrição" />                           
                              <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" class="btn btn-outline-danger" Width="80" CommandName="Remover"
                                CommandArgument='<%# Eval("Id") %>' Text="Remover"
                                OnClientClick="return confirm('Tem certeza que deseja Remover este centro de custo?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                                 </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                       <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>

                    </div>
                </div>
            </div>
        </div>


    </div>
    <script type="text/javascript">
        window.onload = function () {
            var usuario = '<%= Session["login"] != null ? Session["login"].ToString() : "" %>';

            if (usuario === "") {
                alert("Sessão expirada. Você será redirecionado para o login.");
                window.location.href = "../Login.aspx";
            }
        }
    </script>
    <script type="text/javascript">

        $(function () {

            $("[id$=txtSetor]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("CadastroDeUsuario.aspx/getSetor") %>',
                        data: "{ 'prefixo': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split(';')[0],
                                    val: item.split(';')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%= gvUsuarios.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvUsuarios.ClientID %>').find("tbody tr:first"))).DataTable({
                language: {
                    search: "<i class='fa fa-search' aria-hidden='true'>Pesquisar</i>",
                    processing: "Processando...",
                    lengthMenu: "Mostrando _MENU_ registros por páginas",
                    info: "Mostrando página _PAGE_ de _PAGES_",
                    infoEmpty: "Nenhum registro encontrado",
                    infoFiltered: "(filtrado de _MAX_ registros no total)"
                }
            });

        });
    </script>

        <script type="text/javascript">
        $(document).ready(function () {

            $('#<%= gvUsuariosInativos.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvUsuariosInativos.ClientID %>').find("tbody tr:first"))).DataTable({
                language: {
                    search: "<i class='fa fa-search' aria-hidden='true'>Pesquisar</i>",
                    processing: "Processando...",
                    lengthMenu: "Mostrando _MENU_ registros por páginas",
                    info: "Mostrando página _PAGE_ de _PAGES_",
                    infoEmpty: "Nenhum registro encontrado",
                    infoFiltered: "(filtrado de _MAX_ registros no total)"
                }
            });

        });
    </script>
    <script src="../js/jquery.mask.js"></script>
    <script type="text/javascript">      
        $('#<%=txtRF.ClientID %>').mask("99999999999");
       <%-- $('#<%=txtCPF.ClientID %>').mask("999.999.999-99");--%>

</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=select1.ClientID %>").chosen({ no_results_text: "Nada encontrado!" });
        });
    </script>

</asp:Content>
