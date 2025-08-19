using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manutencao_ReceberOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        buscarOsReceber();
    }

    private void buscarOsReceber()
    {        
        var lista = new List<SolicitanteDados>();
        lista = OsDAO.BuscarOS_Receber(2);
        gdvRecebeOS.DataSource = lista;
        gdvRecebeOS.DataBind();
    }
    protected void gdvRecebeOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idOS = Convert.ToInt32(gdvRecebeOS.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());

         Response.Redirect("~/manutencao/ImprimirOS.aspx?idOS=" + idOS);
       // Response.Redirect("~/administrativo/RegistrarLigacao.aspx?nrConulta=" + NrConsulta);
    }
}