<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompletarOS_aberta.aspx.cs" Inherits="manutencao_CompletarOS_aberta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.mask.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <link href="../js/jquery-ui.css" rel="stylesheet" />
   <%-- <script src="../js/jquery.min.js"></script>--%>
     <script src="../bootstrap5/dist/js/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h2 class="text-center">Receber e complementar OS</h2>
        <div class="row">
            <div class="col-1">
                Nº OS
                <asp:Label ID="LabelID_OS" runat="server" CssClass="form-control" Text="Sem" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-3">
                Nome Solicitante
                <asp:Label ID="LabelNomeSolicitante" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-1">
                RF 
                <asp:Label ID="LabelRFSolicitante" runat="server" CssClass="form-control" Text="Sem" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-1">
                Ramal
                <asp:Label ID="LabelRamalSolcicitante" runat="server" CssClass="form-control" Text="Sem" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-4">
                Responsavel Setor
                <asp:Label ID="LabelCodNomeRespSetor" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-2">
                Data da Soclitação
                <asp:Label ID="LabelDataSolicitacao" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <%--  
                <div class="col-2">
                <asp:Label ID="Label5" runat="server" CssClass="form-control" Text="Sem Dados"></asp:Label>
            </div>--%>
        </div>
        <br />
        <div class="row">
            <div class="col-4">
                Centro de Custo
                <asp:Label ID="LabelCentroCusto" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-4">
                Andar / Local
                <asp:Label ID="LabelAndarLocal" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-4">
                Patrimônio
                <asp:Label ID="LabelPatrimonio" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-7">
                Descrição do Serviço
                <asp:Label ID="LabeldescrServico" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-5">
                Observação
                <asp:Label ID="LabelObS" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <%--  <div class="col-2">
                <asp:label id="Label11" runat="server" cssclass="form-control" text="Sem Dados"></asp:label>
            </div>--%>
        </div>
        <br />
        <div class="row">
            <div class="col-4">
                <asp:Label ID="Label9" runat="server" Text="Setor Solicitado:"></asp:Label>
                <asp:DropDownList ID="ddlSetorSolicitado" CssClass="input ddlSetorSolicitado" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSetorSolicitado_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-8">
                Serviço Solicitado   (para ver todos os itens digite * )
                  <asp:TextBox ID="txtServicoRealizar" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:HiddenField ID="hfCustomerId" runat="server" />
            </div>
        </div>
        <br />
        <%--     <div class="row">
            <div class="col-12">Justificativa
            <asp:Textbox ID="txtJustificativa" runat="server" cssclass="form-control" MaxLength="1000" TextMode="MultiLine" Height="100"></asp:Textbox>
                </div>
        </div>--%>
        <br />
        <div class="row" style="justify-content: center; display: flex;">
            <asp:Button ID="btnGravarComplementoOS" runat="server" Text="Gravar" CssClass="btn btn-primary" Width="100" OnClick="btnGravarComplementoOS_Click" />
            <div class="col-1"></div>
            <asp:Button ID="btnRecusar" runat="server" Text="Recusar" CssClass="btn btn-danger" Width="100" OnClientClick="abrirModalMotivo(); return false;" />
        </div>
               <!-- Modal Motivo da Recusa tem que ser no bootstrp 5 -->
<div class="modal fade" id="modalMotivoRecusa" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Motivo da Recusa</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <asp:TextBox ID="txtMotivoRecusa" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Digite o motivo da recusa"></asp:TextBox>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnConfirmarRecusa" runat="server" Text="Confirmar Recusa" CssClass="btn btn-danger" OnClick="btnRecusar_Click" />
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
      </div>
    </div>
  </div>
</div>

         <!--Fim Modal Motivo da Recusa -->
    </div>
    <script type="text/javascript">

        $(function () {

            $("[id$=txtServicoRealizar]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("CompletarOS_aberta.aspx/getSetor") %>',
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
        <script>
    function abrirModalMotivo() {
        $('#modalMotivoRecusa').modal('show');
    }
</script>
</asp:Content>


