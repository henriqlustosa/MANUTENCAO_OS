using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manutencao_Finalizadas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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
        buscarOsReceber();
    }

    private void buscarOsReceber()
    {
        var lista = new List<SolicitanteDados>();
        lista = OsDAO.BuscarOS_Finalizar(5);
        gdvFinalizarOS.DataSource = lista;
        gdvFinalizarOS.DataBind();
    }
    protected void gdvRecebeOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idOS = Convert.ToInt32(gdvFinalizarOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());

        Response.Redirect("~/manutencao/FinalizaVisualizar.aspx?idOS=" + idOS);
        // Response.Redirect("~/administrativo/RegistrarLigacao.aspx?nrConulta=" + NrConsulta);
    }
}