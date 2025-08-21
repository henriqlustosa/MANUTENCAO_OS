<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FinalizarOS.aspx.cs" Inherits="manutencao_ReceberOS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">

         <h5 class="text-center">Finalizar Ordem de Serviço</h5>
          <br />
             <asp:GridView ID="gdvFinalizarOS" AutoGenerateColumns="False" DataKeyNames="idSolicitacao"
            runat="server" OnRowCommand="gdvRecebeOS_RowCommand" CssClass="table table-bordered" width="100%">
            <Columns>
                   <asp:BoundField DataField="idSolicitacao" HeaderText="Nº Os" />
                   <asp:BoundField DataField="nomeSolicitante" HeaderText="Nome Solicitante" />
                   <asp:BoundField DataField="descricaoCentroCusto" HeaderText="Centro de Custo" />
                   <asp:BoundField DataField="andar" HeaderText="Andar" />
                   <asp:BoundField DataField="localDaSolicitacao" HeaderText="Local" />
                   <asp:BoundField DataField="descServicoSolicitado" HeaderText="Descrição do Serviço" />
                   <asp:BoundField DataField="dataSolicitacao" HeaderText="Data da Os"  />              
                <asp:TemplateField HeaderStyle-CssClass="sorting_disabled" HeaderText="Ação">
                    <ItemTemplate>
                        <div class="form-inline">
                            <asp:LinkButton ID="lbDadosPaciente" CommandName="remover" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                Class="btn btn-outline-danger" runat="server">Finalizar                                                                
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>

