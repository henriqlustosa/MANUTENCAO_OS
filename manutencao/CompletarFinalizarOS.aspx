<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompletarFinalizarOS.aspx.cs" Inherits="manutencao_CompletarOS_aberta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <!-- Scripts removidos pois a página está somente leitura -->
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
                Responsável Setor
                <asp:Label ID="LabelCodNomeRespSetor" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
            <div class="col-2">
                Data da Solicitação
                <asp:Label ID="LabelDataSolicitacao" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
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
        </div>

        <br />

        <div class="row">
            <div class="col-4">
                Setor Solicitado
                <!-- Substitui ddlSetorSolicitado -->
                <asp:Label ID="LabelSetorSolicitado" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>

            <div class="col-8">
                Serviço Solicitado
                <!-- Substitui txtServicoRealizar e hfCustomerId -->
                <asp:Label ID="LabelServicoRealizar" runat="server" CssClass="form-control" Text="Sem Dados" BackColor="#F4F4F4"></asp:Label>
            </div>
        </div>

        <br />

        

        <br />

        <!-- Se a página é somente leitura, normalmente o botão não é necessário.
             Se ainda precisar confirmar algo, mantenha o botão; caso contrário, remova. -->
        <div class="row" style="justify-content: center; display: flex;">
            <asp:Button ID="btnGravarComplementoOS" runat="server" Text="Gravar" CssClass="btn btn-primary" Width="100" OnClick="btnGravarComplementoOS_Click" />
        </div>

            <div class="col-7">
        Funcionários
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
      <script type="text/javascript">
      $(document).ready(function () {
          $("#<%=select1.ClientID %>").chosen({ no_results_text: "Nada encontrado!" });
      });
      </script>

</asp:Content>
