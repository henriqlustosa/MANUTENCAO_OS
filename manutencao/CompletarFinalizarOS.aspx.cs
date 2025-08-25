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
        public static string login { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string id1 = Request.QueryString["idOS"];
            int idOs = Convert.ToInt32(id1);
            carregaDados_OSfinalizar(idOs);
            select1.DataSource = OsDAO.carregaFuncionarios(idOs);
            select1.DataTextField = "nome_funcionario";
            select1.DataValueField = "id_funcionario";
            select1.DataBind();
            // ajuda mobile a mostrar teclado numérico
            txtQtdHoras.Attributes["inputmode"] = "numeric";
            txtQtdHoras.Attributes["autocomplete"] = "off";
            txtQtdHoras.Attributes["placeholder"] = "00:00";
            txtQtdHoras.MaxLength = 5; // HH:mms
            VG.login = Session["login"].ToString();
            //CarregarDropDownSetoresSolicitados();
        }
        //if (ddlSetorSolicitado.SelectedItem.Text == "-- Selecione um setor --")
        //{
        //    ddlSetorSolicitado.Attributes.Add("style", "color : red;");
        //}
        //else
        //{
        //    ddlSetorSolicitado.Attributes.Add("style", "color : black;");
        //}
    }
    private void carregaDados_OSfinalizar(int idOs)
    {
        List<FinalizarOS> lista = OsDAO.CarregaDadosOS_Finalizar(idOs);
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
           
        }
    }




    protected void btnGravarComplementoOS_Click(object sender, EventArgs e)
    {
        // Data de finalização (já tratada antes): exemplo C# 3
        CultureInfo cultura = new CultureInfo("pt-BR");
        DateTime? dataFinalizacao = null;
        if (!string.IsNullOrEmpty(txtDataFinalizacao.Text) && txtDataFinalizacao.Text.Trim() != "")
        {
            DateTime dt;
            if (DateTime.TryParseExact(txtDataFinalizacao.Text.Trim(),
                                       "dd/MM/yyyy HH:mm",
                                       cultura,
                                       DateTimeStyles.None,
                                       out dt))
            {
                dataFinalizacao = dt;
            }
            else
            {
                // TODO: exibir mensagem de data inválida, se quiser
            }
        }



        FinalizadoOS r = new FinalizadoOS(Convert.ToInt32(LabelID_OS.Text), dataFinalizacao, txtQtdHoras.Text, 5, OsDAO.ObterIdPorLogin(VG.login));


        bool sucesso = OsDAO.GravaFinalizacaoOSRecebida(r);
      

        List<FuncionarioFinalizacao> funcionarios = new List<FuncionarioFinalizacao>();
        // Gravação do funcionário responsável pela finalização
        for (int i = 0; i < select1.Items.Count; i++)
        {
            if (select1.Items[i].Selected)
            {

                FuncionarioFinalizacao funcionario = new FuncionarioFinalizacao(Convert.ToInt32(LabelID_OS.Text), int.Parse(select1.Items[i].Value));

                funcionarios.Add(funcionario);
            }

        }
        if (funcionarios.Count > 0)
        {
            // Gravação dos funcionários selecionados
            foreach (var funcionario in funcionarios)
            {
            
              
                bool sucessoFuncionario = OsDAO.GravaFuncionarioFinalizacao(funcionario);
                if (!sucessoFuncionario)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "alert('Erro ao gravar funcionário responsável!');", true);
                    return;
                }
            }
        }

        if (sucesso == true)
        {
            string answer = "Gravada com Sucesso!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                        "alert('" + answer + "'); window.location.href='FinalizarOS.aspx';", true);
            return;

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "alert('Erro! \\n Não foi possivel atulizar a OS!');", true);
            return;
        }
    }

    private static bool TryParseHHmm(string hhmm, out TimeSpan ts)
    {
        ts = TimeSpan.Zero;
        if (string.IsNullOrEmpty(hhmm)) return false;

        string s = hhmm.Trim();
        // Aceita "H:mm" ou "HH:mm"
        Match m = Regex.Match(s, @"^(?<H>\d{1,2}):(?<M>[0-5]\d)$");
        if (!m.Success) return false;

        int h, mi;
        if (!int.TryParse(m.Groups["H"].Value, out h)) return false;
        if (!int.TryParse(m.Groups["M"].Value, out mi)) return false;

        // permite 0–99 horas (ajuste se quiser limitar a 23)
        if (h < 0 || h > 99) return false;

        ts = new TimeSpan(h, mi, 0);
        return true;
    }

    private static decimal TimeSpanToDecimalHours(TimeSpan ts)
    {
        return (decimal)ts.TotalMinutes / 60m;
    }


    public static bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim().Length == 0;
    }
    protected void btnRecusar_Click(object sender, EventArgs e)
    {
        ReceberOS r = new ReceberOS();
        r.id_solicitacao = Convert.ToInt32(LabelID_OS.Text);
        string motivoRecusa = "Solicitação Recusada - Motivo: " + txtMotivoRecusa.Text.Trim();
        r.justificativa_recusar = motivoRecusa;
        r.codStatus =11; // 11 = Recusada   
        bool sucesso = OsDAO.AtualizaSolicitacaoOSRecebidaRecusa(r);
        if (sucesso == true)
        {
            string answer = "Recusa gravada com Sucesso!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                        "alert('" + answer + "'); window.location.href='ReceberOS.aspx';", true);
            return;

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "alert('Erro! \\n Não foi possivel atulizar a OS!');", true);
            return;
        }
    }
}