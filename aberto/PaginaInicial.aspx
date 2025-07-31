<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PaginaInicial.aspx.cs" Inherits="PaginaInicial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.mask.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <link href="../js/jquery-ui.css" rel="stylesheet" />
    <style>
        #btn1 {
            width: 230px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <h4 class="text-center">
            <asp:Label ID="Label1" runat="server" Text="Bem Vindo - " ForeColor="#3B7B92"></asp:Label>
            <asp:Label ID="pegaNomeLoginUsuario" runat="server" Text="" ForeColor="#3B7B92" Font-Bold="True"></asp:Label>
        </h4>
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <br />
        <h5 class="text-center">Ordem de Serviço</h5>

     <%--   <div class="row">
            <div class="col-4"></div>
            <div class="col-2">
                <asp:Button ID="btnHSPM" runat="server" class="btn btn-outline-primary" Text="Funcionário HSPM" Height="50px" Width="180px"  />
            </div>
            <div class="col-2">
                <asp:Button ID="btnTerceiro" runat="server" class="btn btn-outline-primary" Text="Funcionário Terceiro" Height="50px" Width="180px"  />
            </div>
        </div>--%>
        <br />
        <br />
        <br />
        <br />
        <div class="row col-12">
            <div class="col-3"></div>
            <h6 class="text-center">
                <asp:Label ID="Label2" runat="server" Text="A T.I do HSPM disponibiliza um portal para abrir chamados >> Clique no botão abaixo."></asp:Label></h6>

        </div>
        <div class="row col-12">
            <div class="col-4"></div>
            <div class="col-4 text-center">
                &nbsp &nbsp &nbsp &nbsp<button type="button" id="btn1" class="btn btn-outline-secondary" onclick="openNewTab()">Abrir Chamados T.I - HSPM</button>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function openNewTab() {
            window.open('http://hspmins18/ti', '_blank');
        }
    </script>
        <script type="text/javascript">
    window.onload = function() {
        var usuario = '<%= Session["login"] != null ? Session["login"].ToString() : "" %>';

        if (usuario === "") {
            alert("Sessão expirada. Você será redirecionado para o login.");
            window.location.href = "../login.aspx";
        }
    }
</script>
</asp:Content>

