using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FinalizarOS
/// </summary>
public class FinalizarOS
{
    public int idSolicitacao { get; set; }
    public string nomeUsuario { get; set; }
    public string rfUsuario { get; set; }
    public string ramalSolicitante { get; set; }
    public string descricaoCentroDeCusto { get; set; }
    public string nomeResponsavel { get; set; }
    public string rfResponsavel { get; set; }
    public string descricaoPatrimonio { get; set; }
    public string andar { get; set; }
    public string local { get; set; }
    public string obsSolicitacao { get; set; }
    public DateTime? dataSolicitacao { get; set; }
    public int? codigoCentroDeCusto { get; set; }
    public int? resposavelID { get; set; }          // (mantido conforme está na view)
    public string descricaoServico { get; set; }
    public string descricao { get; set; }
    public string ServicoArealizar { get; set; }
    public int codigoPatrimonio { get; set; }
    public string BEM_A_DESC { get; set; }
    public int? status { get; set; }

    public string dtOSfinalizada { get; set; }
    public string qtdHorasServiço { get; set; }
    public string nomeFuncionario_Operacional { get; set; }
    // Construtor vazio
    public FinalizarOS() { }
}