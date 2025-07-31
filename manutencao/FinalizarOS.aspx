<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FinalizarOS.aspx.cs" Inherits="manutencao_CompletarOS_aberta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>  
    <script src="../js/jquery.mask.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <link href="../js/jquery-ui.css" rel="stylesheet" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h2 class="text-center">Receber e complementar OS</h2>
        <div class="row">
            <div class="col-1">
                Nº OS
                <asp:label id="LabelID_OS" runat="server" cssclass="form-control" text="Sem" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-3">
                Nome Solicitante
                <asp:label id="LabelNomeSolicitante" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-1">
                RF 
                <asp:label id="LabelRFSolicitante" runat="server" cssclass="form-control" text="Sem" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-1">
                Ramal
                <asp:label id="LabelRamalSolcicitante" runat="server" cssclass="form-control" text="Sem" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-4">
                Responsavel Setor
                <asp:label id="LabelCodNomeRespSetor" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-2">
                Data da Soclitação
                <asp:label id="LabelDataSolicitacao" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
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
                <asp:label id="LabelCentroCusto" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-4">
                Andar / Local
                <asp:label id="LabelAndarLocal" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-4">
                Patrimônio
                <asp:label id="LabelPatrimonio" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-7">Descrição do Serviço
                <asp:label id="LabeldescrServico" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
            </div>
            <div class="col-5">Observação
                <asp:label id="LabelObS" runat="server" cssclass="form-control" text="Sem Dados" BackColor="#F4F4F4"></asp:label>
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
    OnSelectedIndexChanged="ddlSetorSolicitado_SelectedIndexChanged" ></asp:DropDownList>
            </div>
              <div class="col-8">Serviço Solicitado   (para ver todos os itens digite * )
                  <asp:TextBox ID="txtServicoRealizar" runat="server" cssclass="form-control"></asp:TextBox>
                  <asp:HiddenField ID="hfCustomerId" runat="server" />
              </div>
        </div>
          <br />
        <div class="row">
            <div class="col-12">Justificativa
            <asp:Textbox ID="txtJustificativa" runat="server" cssclass="form-control" MaxLength="1000" TextMode="MultiLine" Height="100"></asp:Textbox>
                </div>
        </div>
        <br />
      <div class="row" style="justify-content: center; display: flex;">
    <asp:Button ID="btnGravarComplementoOS" runat="server" Text="Gravar" CssClass="btn btn-primary" Width="100" OnClick="btnGravarComplementoOS_Click" />
</div>

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
</asp:Content>


