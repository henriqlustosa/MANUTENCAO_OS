using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manutencao_CompletarOS_aberta : System.Web.UI.Page
{
    public static class VG
    {
        public static int codSetorSolicitado { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.DataBind();
            string id1 = Request.QueryString["idOS"];
            int idOs = Convert.ToInt32(id1);
            carregaDados_OSreceber(idOs);           
        }
     
    }

    private void carregaDados_OSreceber(int idOs)
    {
        List<SolicitanteDados> lista = OsDAO.CarregaDadosOS_Imprimir(idOs);
        foreach (var i in lista)
        {
            LabelID_OS.Text = i.idSolicitacao.ToString();
            LabelNomeSolicitante.Text = i.nomeSolicitante;
            LabelRFSolicitante.Text = i.rfSolicitante;
            LabelRamalSolcicitante.Text = i.ramalSolicitante;
            LabelDataSolicitacao.Text = i.dataSolicitacao.ToString();
            //LabelCodNomeRespSetor.Text = i.codRespCentroCusto + " - " + i.nomeResponsavel_Custo;
            LabelCentroCusto.Text = i.codCentroCusto + " - " + i.descricaoCentroCusto;
            LabelAndarLocal.Text = i.andar + " - " + i.localDaSolicitacao;
            LabelPatrimonio.Text = i.codPatrimonio + " - " + i.equipamentoDesc;
            LabeldescrServico.Text = i.descServicoSolicitado;
            LabelSetorSolicitado.Text = i.setorSolicitadoDesc;
            LabelDescTecnicaServico.Text = i.servicoSolicitadoDesc;
            LabelObS.Text = i.obs;          
        }
    }

    protected void btnGravarComplementoOS_Click(object sender, EventArgs e)
    {
        SolicitanteDados s = new SolicitanteDados();
        int idOs = Convert.ToInt32(LabelID_OS.Text);
        s.idSolicitacao = idOs;
        s.codStatusSolicitacao = 2;
        bool sucesso = OsDAO.GravaSolicitacaoOSRecebida(s);
    }
}