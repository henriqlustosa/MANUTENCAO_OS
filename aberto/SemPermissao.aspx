<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SemPermissao.aspx.cs" Inherits="SemPermissao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">


        <br />  <br />  <br />  <br />  <br />  <br />

        <h2>"Você não tem permissão para esta página. Procure o administrador do sistema e solicite acesso."</h2>

        <br /> Voltar para pagina inicial<asp:Button ID="btnVoltarP" runat="server" Text="Pagina Inicial" OnClick="btnVoltarP_Click" /> 

    </div>
</asp:Content>


