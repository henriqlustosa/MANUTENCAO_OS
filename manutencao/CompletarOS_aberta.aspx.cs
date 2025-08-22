using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manutencao_CompletarOS_aberta : System.Web.UI.Page
{
    public static class VG
    {
        public static int codSetorSolicitado { get; set; }
        public static int idOS { get; set; }
        public static string login { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("~/login.aspx"); // Redireciona se não estiver logado
                return;
            }

            // 2. Verifica se o perfil é diferente de "1" (Administrador)
            List<int> perfis = Session["perfis"] as List<int>;
            if (perfis == null || (!perfis.Contains(2)) && (!perfis.Contains(3)))
            {
                Response.Redirect("~/aberto/SemPermissao.aspx");
            }
            string id1 = Request.QueryString["idOS"];
            int idOs = Convert.ToInt32(id1);
            carregaDados_OSreceber(idOs);
            CarregarDropDownSetoresSolicitados();
            VG.idOS = idOs;
            VG.login = Session["login"].ToString();
        }
        if (ddlSetorSolicitado.SelectedItem.Text == "-- Selecione um setor --")
        {
            ddlSetorSolicitado.Attributes.Add("style", "color : red;");
        }
        else
        {
            ddlSetorSolicitado.Attributes.Add("style", "color : black;");
        }
    }

    private void carregaDados_OSreceber(int idOs)
    {
        List<SolicitanteDados> lista = OsDAO.CarregaDadosOS_Receber(idOs);
        foreach (var i in lista)
        {
            LabelID_OS.Text = i.idSolicitacao.ToString();
            LabelNomeSolicitante.Text = i.nomeSolicitante;
            LabelRFSolicitante.Text = i.rfSolicitante;
            LabelRamalSolcicitante.Text = i.ramalSolicitante;
            LabelDataSolicitacao.Text = i.dataSolicitacao.ToString();
            LabelCodNomeRespSetor.Text = i.codRespCentroCusto + " - " + i.nomeResponsavel_Custo;
            LabelCentroCusto.Text = i.codCentroCusto + " - " + i.descricaoCentroCusto;
            LabelAndarLocal.Text = i.andar + " - " + i.localDaSolicitacao;
            LabelPatrimonio.Text = i.codPatrimonio + " - " + i.equipamentoDesc;
            LabeldescrServico.Text = i.descServicoSolicitado;
            
            //if (i.obs == "")
            //{
            //    LabelObS.Text = i.obs;
            //}
            LabelObS.Text = i.obs;
            //LabelNomeSolicitante.Text = i.nomeSolicitante;
        }
    }

    private void CarregarDropDownSetoresSolicitados()
    {
        List<SolicitanteDados> lista = OsDAO.BuscarSetoresSolicitados();

        ddlSetorSolicitado.DataSource = lista;
        ddlSetorSolicitado.DataTextField = "setorSolicitadoDesc";   // Vai aparecer no dropdown
        ddlSetorSolicitado.DataValueField = "codSetorSolicitado";     // Vai ser o valor interno
        ddlSetorSolicitado.DataBind();
        if (lista.Count > 1)
        {
            ddlSetorSolicitado.Items.Insert(0, new ListItem("-- Selecione um setor --", ""));
            //ddlSetor.Items[0].Attributes.CssStyle.Add("color", "red");
        }

    }

    protected void ddlSetorSolicitado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSetorSolicitado.SelectedItem.Text == "-- Selecione um setor --")
        {
            ddlSetorSolicitado.Attributes.Add("style", "color : red;");
        }
        else
        {
            ddlSetorSolicitado.Attributes.Add("style", "color : black;");
            VG.codSetorSolicitado = Convert.ToInt32(ddlSetorSolicitado.SelectedValue);
        }
        txtServicoRealizar.Text = "";
    }
 
    [WebMethod]

    public static string[] getSetor(string prefixo)
    {
        int v = VG.codSetorSolicitado;
        List<string> clientes = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                if (prefixo == "*")
                {
                    cmd.CommandText = @"SELECT TOP 100 [SetorManutencao], [idServico], [ServicoArealizar]
                                    FROM [hspm_OS].[dbo].[ServicoArealizar]
                                    WHERE SetorManutencao = @Setor order by ServicoArealizar";
                }
                else
                {
                    cmd.CommandText = @"SELECT TOP 100 [SetorManutencao], [idServico], [ServicoArealizar]
                                    FROM [hspm_OS].[dbo].[ServicoArealizar]
                                    WHERE SetorManutencao = @Setor AND ServicoArealizar LIKE '%' + @Texto + '%' order by ServicoArealizar";
                    cmd.Parameters.AddWithValue("@Texto", prefixo);
                }

                cmd.Parameters.AddWithValue("@Setor", v);
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        string texto = sdr["ServicoArealizar"].ToString();
                        string id = sdr["idServico"].ToString();
                        clientes.Add(texto + ";" + id); // <-- Aqui é o formato esperado pelo JS
                    }                   
                }
                conn.Close();
            }
        }
        return clientes.ToArray();
    }


    protected void btnGravarComplementoOS_Click(object sender, EventArgs e)
    {
        if (ddlSetorSolicitado.SelectedItem.Text == "-- Selecione um setor --")
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "alert('Erro! \\n Informe o setor Solicitado!');", true);
            return;
        }
        if (txtServicoRealizar.Text.Length < 2)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "alert('Erro! \\n Informe o serviço Solicitado!');", true);
            return;
        }
        ReceberOS r = new ReceberOS(Convert.ToInt32(LabelID_OS.Text), Convert.ToInt32(hfCustomerId.Value), 2, OsDAO.ObterIdPorLogin(VG.login));


        bool sucesso = OsDAO.GravaSolicitacaoOSRecebida(r);
        if (sucesso)
        {
            Thread.Sleep(1000);
            // Script único que mostra mensagem e pergunta sobre impressão
            //alert('Gravada com Sucesso!');
            string script = @"
        
        if(confirm('Deseja imprimir a OS agora?')) {
            window.location='ImprimirOS.aspx?idOS=" + VG.idOS + @"';
        } else {
            window.location='ReceberOS.aspx';
        }";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmaImpressao", script, true);
            return;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "alert('Erro! \\n Não foi possível atualizar a OS!');", true);
            return;
        }
    
    }

    protected void btnRecusar_Click(object sender, EventArgs e)
    {
        ReceberOS r = new ReceberOS();
        r.id_solicitacao= Convert.ToInt32(LabelID_OS.Text);
        string motivoRecusa = "Solicitação Recusada - Motivo: " + txtMotivoRecusa.Text.Trim();
        r.justificativa_recusar = motivoRecusa;
        r.codStatus = 6; // 6 = Recusada   
        bool sucesso = OsDAO.GravaSolicitacaoOSRecebidaRecusa(r);
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