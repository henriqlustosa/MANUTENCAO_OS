
<%@ Page Title="Receber e complementar OS" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FinalizaVisualizar.aspx.cs" Inherits="manutencao_CompletarOS_aberta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

  <!-- Bootstrap 5 -->  
    <link href="../bootstrap5/dist/css/bootstrap.min.css" rel="stylesheet" />
  <!-- Chosen (multiselect) -->
    <link href="../css/chosen.min.css" rel="stylesheet" />
  <!-- Flatpickr (datepicker moderno) -->
    <link href="../css/flatpick.min.css" rel="stylesheet" />
    <link href="../css/material_blue.css" rel="stylesheet" />
  <style>
    /* Mantém a barra vertical sempre presente para evitar “pulo” de layout */
    html { overflow-y: scroll; }

    .readonly-box{
      background-color:#f8f9fa;
      border:1px solid #dee2e6;
      border-radius:.375rem;
      padding:.375rem .75rem;
      min-height:calc(1.5em + .75rem + 2px);
    }

    /* --- Chosen: aparência e sobreposição --- */
    .chosen-container { width: 100% !important; }

    /* Caixa "input" do chosen (multi) com look Bootstrap */
    .chosen-container-multi .chosen-choices {
      min-height: calc(2.25rem + 2px);
      padding: .375rem .5rem;
      border: 1px solid #dee2e6;
      border-radius: .375rem;
      box-shadow: none;
    }

    /* Dropdown (a “faixa branca”) — borda, sombra e sobreposição */
    .chosen-container .chosen-drop {
      margin-top: .25rem;
      border: 1px solid #dee2e6;
      border-radius: .5rem;
      box-shadow: 0 .5rem 1rem rgba(0,0,0,.15);
      z-index: 1065;
    }

    /* Lista rolável com altura limitada */
    .chosen-container .chosen-results {
      max-height: 260px;
      overflow-y: auto;
    }

    /* Campo de busca dentro do dropdown */
    .chosen-container .chosen-search input {
      height: 2.25rem;
      line-height: 2.25rem;
      border: 1px solid #dee2e6;
      border-radius: .375rem;
    }

    /* NÃO esconda o select original (Chosen já lida com isso) */
    /* select.form-select { visibility: hidden; }  <-- intencionalmente removido */

    .form-row + .form-row { margin-top: .75rem; }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container my-3">
    <h2 class="text-center mb-4">Visualizar Ordem de serviço finalizada</h2>

    <!-- Linha 1 -->
    <div class="row form-row">
      <div class="col-12 col-md-1">
        <label class="form-label">Nº OS</label>
        <asp:Label ID="LabelID_OS" runat="server" CssClass="readonly-box d-block" Text="Sem"></asp:Label>
      </div>
      <div class="col-12 col-md-3">
        <label class="form-label">Nome Solicitante</label>
        <asp:Label ID="LabelNomeSolicitante" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
      <div class="col-6 col-md-1">
        <label class="form-label">RF</label>
        <asp:Label ID="LabelRFSolicitante" runat="server" CssClass="readonly-box d-block" Text="Sem"></asp:Label>
      </div>
      <div class="col-6 col-md-1">
        <label class="form-label">Ramal</label>
        <asp:Label ID="LabelRamalSolcicitante" runat="server" CssClass="readonly-box d-block" Text="Sem"></asp:Label>
      </div>
      <div class="col-12 col-md-4">
        <label class="form-label">Responsável Setor</label>
        <asp:Label ID="LabelCodNomeRespSetor" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
      <div class="col-12 col-md-2">
        <label class="form-label">Data da Solicitação</label>
        <asp:Label ID="LabelDataSolicitacao" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
    </div>

    <!-- Linha 2 -->
    <div class="row form-row">
      <div class="col-12 col-md-4">
        <label class="form-label">Centro de Custo</label>
        <asp:Label ID="LabelCentroCusto" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
      <div class="col-12 col-md-4">
        <label class="form-label">Andar / Local</label>
        <asp:Label ID="LabelAndarLocal" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
      <div class="col-12 col-md-4">
        <label class="form-label">Patrimônio</label>
        <asp:Label ID="LabelPatrimonio" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
    </div>

    <!-- Linha 3 -->
    <div class="row form-row">
      <div class="col-12 col-md-7">
        <label class="form-label">Descrição do Serviço</label>
        <asp:Label ID="LabeldescrServico" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
      <div class="col-12 col-md-5">
        <label class="form-label">Observação</label>
        <asp:Label ID="LabelObS" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
    </div>

    <!-- Linha 4 -->
    <div class="row form-row">
      <div class="col-12 col-md-4">
        <label class="form-label">Setor Solicitado</label>
        <asp:Label ID="LabelSetorSolicitado" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
      <div class="col-12 col-md-8">
        <label class="form-label">Serviço Solicitado</label>
        <asp:Label ID="LabelServicoRealizar" runat="server" CssClass="readonly-box d-block" Text="Sem Dados"></asp:Label>
      </div>
    </div>

    <!-- Nova linha: Data de Finalização / Quantidade de horas / Funcionários -->
    <div class="row g-3 form-row">
      <!-- Data de Finalização -->
      <div class="col-12 col-md-4">
        <label for="<%= txtDataFinalizacao.ClientID %>" class="form-label">Data de Finalização</label>
        <asp:TextBox ID="txtDataFinalizacao" runat="server" CssClass="form-control" ></asp:TextBox>
      </div>

      <!-- Quantidade de horas -->
      <div class="col-12 col-md-4">
        <label for="<%= txtQtdHoras.ClientID %>" class="form-label">Quantidade de horas</label>
        <asp:TextBox ID="txtQtdHoras" runat="server"
                     CssClass="form-control"
                     MaxLength="5"
                     placeholder="00:00"></asp:TextBox>
      </div>

      <!-- Funcionários (Chosen) -->
      <div class="col-12 col-md-4">
        <label class="form-label" for="select1">Funcionários</label>
              <asp:TextBox ID="txtFuncionarios" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>

     
      </div>
    </div>

    <!-- Botão -->
    <div class="row mt-4">
      <div class="col-12 d-flex justify-content-center">
        <asp:Button ID="btnGravarComplementoOS" runat="server" Text="Voltar"
          CssClass="btn btn-primary px-4" OnClick="btnGravarComplementoOS_Click"/>
      </div>
    </div>
  </div>
      
</asp:Content>
