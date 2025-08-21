using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FuncionarioFinalizacao
/// </summary>
public class FuncionarioFinalizacao
{
    public int idSolicitacao { get; set; }
    public int id_funcionario { get; set; }

    public int status { get; set; }
    public DateTime dataCadastro { get; set; }
    public FuncionarioFinalizacao(int idSolicitacao, int id_funcionario)
    {
        this.idSolicitacao = idSolicitacao;
        this.id_funcionario = id_funcionario;
      
        this.dataCadastro = DateTime.Now; // sempre a data/hora atual
        this.status = 1; // Mantido conforme o original
    }
}
