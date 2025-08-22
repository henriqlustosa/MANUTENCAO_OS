using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
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
            string id1 = Request.QueryString["idOS"];
            int idOs = Convert.ToInt32(id1);
            carregaDados_OSfinalizar(idOs);           
            //CarregarDropDownSetoresSolicitados();
        }

    }
    private void carregaDados_OSfinalizar(int idOs)
    {
        List<FinalizarOS> lista = OsDAO.CarregaDadosOS_Finalizada_Visualizar(idOs);
        foreach (var i in lista)
        {
            LabelID_OS.Text = i.idSolicitacao.ToString();
            LabelNomeSolicitante.Text = i.nomeUsuario;
            LabelRFSolicitante.Text = i.rfUsuario;
            LabelRamalSolcicitante.Text = i.ramalSolicitante;
            LabelDataSolicitacao.Text = i.dataSolicitacao.ToString();
            LabelCodNomeRespSetor.Text = i.rfResponsavel + " - " + i.nomeResponsavel;
            LabelCentroCusto.Text = i.codigoCentroDeCusto + " - " + i.descricaoCentroDeCusto;
            LabelAndarLocal.Text = i.andar + " - " + i.local;
            LabelPatrimonio.Text = i.codigoPatrimonio + " - " + i.descricaoPatrimonio;
            LabeldescrServico.Text = i.descricaoServico;
            LabelSetorSolicitado.Text = i.descricao;
            LabelServicoRealizar.Text = i.ServicoArealizar;
            LabelSetorSolicitado.Text = i.descricao;          
            LabelObS.Text = i.obsSolicitacao;
            txtDataFinalizacao.Text = i.dtOSfinalizada;
            txtQtdHoras.Text = i.qtdHorasServiço;
            txtFuncionarios.Text = i.nomeFuncionario_Operacional;           
        }
    }


    protected void btnGravarComplementoOS_Click(object sender, EventArgs e)
    {
        Response.Redirect("Finalizadas.aspx");
    }
}