using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionWrapper
/// </summary>
public class SessionWrapper
{
    public static string Login
    {
        get { return HttpContext.Current.Session["login"] as string; }
        set { HttpContext.Current.Session["login"] = value; }
    }

    public static string NomeUsuario
    {
        get { return HttpContext.Current.Session["nomeUsuario"] as string; }
        set { HttpContext.Current.Session["nomeUsuario"] = value; }
    }

    public static string LoginResponsavel
    {
        get { return HttpContext.Current.Session["loginResponsavel"] as string; }
        set { HttpContext.Current.Session["loginResponsavel"] = value; }
    }


    public static List<int> Perfis
    {
        get { return HttpContext.Current.Session["perfis"] as List<int>; }
    }
}