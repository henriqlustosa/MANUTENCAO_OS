using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Finalizado
/// </summary>
public class FinalizadoOS
{
    public int idSolicitacao { get; set; }
    // Novas propriedades para o construtor
    public DateTime? dataFinalizacao { get; set; }
    public string hora { get; set; }
    public DateTime? dataCadastro { get; set; }

    public int status { get; set; } // Mantido conforme o original
    public int idUsuarioFinalizar { get; set; }
    // Construtor vazio (mantido)
    public FinalizadoOS() { }

    // Construtor solicitado
    public FinalizadoOS(int idSolicitacao, DateTime? dataFinalizacao, string hora,int status,int  idUsuarioFinalizar)
    {
        this.idSolicitacao = idSolicitacao;
        this.dataFinalizacao = dataFinalizacao;
        this.hora = hora;
        this.dataCadastro = DateTime.Now; // sempre a data/hora atual
        this.status = status; // Mantido conforme o original
        this.idUsuarioFinalizar =   idUsuarioFinalizar; // Mantido conforme o original
    }
}