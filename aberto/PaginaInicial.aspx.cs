using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaginaInicial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AccessDeniedMessage"] != null)
        {
            lblMessage.Text = Session["AccessDeniedMessage"].ToString();
            Session["AccessDeniedMessage"] = null; // Limpa a mensagem após exibi-la
        }

        string nome1 = Session["nomeUsuario"] as string;

        string nome  = nome1.ToUpper().Trim();
        pegaNomeLoginUsuario.Text= nome;
       // pegaNomeLoginUsuario.Text= verificaNome(nome);

        // Deslogar após 20 minutos
        Response.AppendHeader("Refresh",
        //Session TimeOut é em minutos e o Refresh e segundos, por isso o Session.Timeout * 60 JUNIOR>> 1 vale 20 segundos 3 vale 1 Minuto
        String.Concat((Session.Timeout * 60),
        //Página para onde o usuário será redirecionado
        ";URL=../login.aspx"));
    }
   
}