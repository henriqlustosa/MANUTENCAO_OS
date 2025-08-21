<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImprimirOS.aspx.cs" Inherits="manutencao_CompletarOS_aberta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="../css/print.css" rel="stylesheet" media="print" />
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.mask.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <link href="../js/jquery-ui.css" rel="stylesheet" />
    <script src="../bootstrap5/dist/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        function printPanel(panelId) {
            var panel = document.getElementById(panelId);
            if (!panel) {
                alert("Erro: Panel não encontrado! ID = " + panelId);
                return;
            }

            var printWindow = window.open('', '', 'height=1500,width=1200');
            printWindow.document.write('<html><head><title>Impressão</title></head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }



    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <!-- Botão fora do Panel, não será impresso -->
    <asp:Button ID="btnImprimir" CssClass="btn btn-primary" runat="server"
        Text="Imprimir OS"
        OnClientClick='<%# "printPanel(\"" + pnlOS.ClientID + "\"); return false;" %>' />
    <!-- Painel que será impresso -->
    <asp:Panel ID="pnlOS" runat="server" CssClass="print-area">

        <div class="container">
            <!-- Logo alinhada à esquerda -->
            <div style="text-align: left;">
                <img src="../img/HSPM_LOGO.jpg" style="max-width: 80px; height: auto;" />
            </div>

            <!-- Título centralizado na página -->
            <div style="text-align: center; margin-top: -40px;">
                <h4 style="margin: 0; padding: 0;">Ordem de Serviço</h4>
            </div>
            <br />

            <hr />
            <div class="row">
                <div class="col-auto">
                    <b>Nº OS: </b>
                    <asp:Label ID="LabelID_OS" runat="server" Text="Sem"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Unidade Solicitante:</b>
                    <asp:Label ID="LabelCentroCusto" runat="server" Text="Sem Dados"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Solicitante: </b>
                    <asp:Label ID="LabelNomeSolicitante" runat="server" Text="Sem Dados"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>RF:</b><asp:Label ID="LabelRFSolicitante" runat="server" Text="Sem"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Ramal: </b>
                    <asp:Label ID="LabelRamalSolcicitante" runat="server" Text="Sem"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-auto">
                    <b>Andar / Local: </b>
                    <asp:Label ID="LabelAndarLocal" runat="server" Text="Sem Dados"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Patrimônio:</b>
                    <asp:Label ID="LabelPatrimonio" runat="server" Text="Sem Dados"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Data da Solicitação:</b>
                    <asp:Label ID="LabelDataSolicitacao" runat="server" Text="Sem Dados"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-auto">
                    <b>Setor Solicitado:</b>
                    <asp:Label ID="LabelSetorSolicitado" runat="server" Text="Sem Dados"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Serviço Solicitado:</b>
                    <asp:Label ID="LabelDescTecnicaServico" runat="server" Text="Sem Dados"></asp:Label>
                </div>
                <div class="col-auto">
                    <b>Descrição do Serviço:</b>
                    <asp:Label ID="LabeldescrServico" runat="server" Text="Sem Dados"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-auto">
                    <b>Observação:</b>
                    <asp:Label ID="LabelObS" runat="server" Text="Sem Dados"></asp:Label>
                </div>
            </div>            
            <hr />  
            <br />
        <%--    <div class="row">
                <div class="col-auto">
                    <b>Existe condições técnicas para execução dos serviços?</b>&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox1" runat="server" Width="30"></asp:TextBox>&nbsp; SIM &nbsp;
                    <asp:TextBox ID="TextBox2" runat="server" Width="30"></asp:TextBox>&nbsp; NÃO
                </div>
            </div>
            <br />--%>
            <div class="row">
                <b>Serviço executado:</b>
                <hr />
            </div>
            <br />
            <div class="row">
                <hr />
            </div>
            <br />

            <div class="row">
                <hr />
            </div>
            <br />
     <%--        <div class="row">
     <hr />
 </div>
 <br />--%>
            <div class="row">
                <b>Material utilizado:</b>
                <hr />
            </div>
            <br />
            <div class="row">
                <hr />
            </div>
            <br />
            <div class="row">
                <hr />
                <br />
            </div>
      

            <table style="width: 100%; border-collapse: collapse; font-size: 12pt; margin-top: 10px;">
                <tr>
                    <td style="width: 22%;"><b>Nº de funcionários:</b> _____</td>
                    <td style="width: 22%;"><b>Horas Gastas:</b> _____</td>
                    <td style="width: 40%;"><b>Data do término do serviço:</b> ____/____/______</td>
                </tr>
            </table>

            
            <br />         
            <br />
            <div class="row">
                <div class="col-auto">
                    <b>Assinatura do funcionário:</b>________________________________________
                </div>
                <br />
                <div class="col-auto">
                    <b>Assinatura do coordenador:</b>________________________________________
                </div>
            </div>
                
            <br />
            <br />
       <br />
               <div class="row">
                <div class="col-auto">
                    <b>Serviço executado a contento?</b>&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox1" runat="server" Width="30" height="20"></asp:TextBox>&nbsp; SIM &nbsp;
                    <asp:TextBox ID="TextBox2" runat="server" Width="30" height="20"></asp:TextBox>&nbsp; NÃO
                </div>
            </div>
          
            <br />
     
    <div class="row">
                <div class="col-auto">
                    ________________________________________                    
                </div>
          </div>
      <div class="row">
                <div class="col-auto">
                    
                    <b>Assinatura do Coordenador/Gerente:</b>
                </div>
          </div>
        </div>
    </asp:Panel>

 

</asp:Content>

