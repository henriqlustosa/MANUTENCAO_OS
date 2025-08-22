using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 
/// </summary>
public class ReceberOS
{
    public int id_solicitacao { get; set; }
    public int codServicoRealizar { get; set; }
    public string justificativa_recusar { get; set; }
    public DateTime dataRecebimento { get; set; }


    public int idUsuarioReceber { get; set; }

    public int codStatus { get; set; }

    // Construtor com parâmetros    
    public ReceberOS(int id_solicitacao, int codServicoRealizar, int codStatus, int idUsuarioReceber)
    {
        this.id_solicitacao = id_solicitacao;
        this.codServicoRealizar = codServicoRealizar;
        this.codStatus = codStatus;
        this.justificativa_recusar = null;
        this.dataRecebimento = DateTime.Now; // data e hora atuais
        this.idUsuarioReceber = idUsuarioReceber;
    }
    // Construtor vazio (sem parâmetros)
    public ReceberOS()
    {
        this.id_solicitacao = 0;
        this.codServicoRealizar = 0;
        this.justificativa_recusar = null;
        this.dataRecebimento = DateTime.Now; // opcional: já define a data atual
        this.codStatus = 2;
    }
}

