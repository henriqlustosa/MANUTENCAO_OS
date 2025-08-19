using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OsDAO
/// </summary>
public class OsDAO
{
    public static List<ConsultasCentroDeCusto> carregaCentroDeCusto()
    {
        var lista = new List<ConsultasCentroDeCusto>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery;
                strQuery = @"SELECT codigoCentroDeCusto, CONCAT( [codigoCentroDeCusto],+' - ',[descricao]) as custo
  FROM [hspm_OS].[dbo].[CentroDeCusto] order by descricao";
                con.Open();
                SqlCommand commd = new SqlCommand(strQuery, con);
                SqlDataReader dr = commd.ExecuteReader();
                while (dr.Read())
                {
                    ConsultasCentroDeCusto c = new ConsultasCentroDeCusto();
                    c.idCentroCusto = dr.GetInt32(0);
                    c.desc_centroCusto = dr.IsDBNull(1) ? null : dr.GetString(1);
                    lista.Add(c);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                throw;
            }
            return lista;
        }
    }
    public static void GravaCentroDecustoDofuncionario(string login, int codCentroDeCusto)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        { 
                try
                {
                    string strQuery = @"INSERT INTO UsuarioCentroDeCusto (UsuarioId, idCentroDeCusto, Ativo)
SELECT 
    u.UsuarioId,
    c.idCentroDeCusto,
    1 AS Ativo
FROM dbo.Usuarios u
INNER JOIN dbo.CentroDeCusto c 
       ON c.codigoCentroDeCusto = @id_centroDeCusto 
WHERE u.LoginRede = @loginRede";          
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@loginRede", login);
                cmd.Parameters.AddWithValue("@id_centroDeCusto", codCentroDeCusto);                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    throw;
                } 
        }
    }
    public static List<SolicitanteDados> BuscarOS_Receber(int status)
    {
        var lista = new List<SolicitanteDados>();
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT [id_solicitacao]
      ,[NomeCompleto]
      ,[descricaoCentroDeCusto]
      ,[andar]
      ,[local]
      ,[descricaoServico]
      ,[dataSolicitacao]    
  FROM [hspm_OS].[dbo].[Vw_ReceberOS] where status = "+  status + " order by id_solicitacao asc";

            SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@loginSolicitante", login);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.idSolicitacao = Convert.ToInt32(reader["id_solicitacao"]);
                s.nomeSolicitante = reader["NomeCompleto"].ToString();
                s.andar = reader["andar"].ToString();
                s.localDaSolicitacao = reader["local"].ToString();
                s.descricaoCentroCusto = reader["descricaoCentroDeCusto"].ToString();               
                s.descServicoSolicitado = reader["descricaoServico"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;
                     

                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }

    public static List<SolicitanteDados> BuscarOS_Finalizar(int status)
    {
        var lista = new List<SolicitanteDados>();
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT [idSolicitacao]
      ,[nomeUsuario]
      ,[descricaoCentroDeCusto]
      ,[andar]
      ,[local]
      ,[descricaoServico]
      ,[dataSolicitacao]    
  FROM [hspm_OS].[dbo].[Vw_Impressao] where status = " + status + " order by idSolicitacao asc";

            SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@loginSolicitante", login);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.idSolicitacao = Convert.ToInt32(reader["idSolicitacao"]);
                s.nomeSolicitante = reader["nomeUsuario"].ToString();
                s.andar = reader["andar"].ToString();
                s.localDaSolicitacao = reader["local"].ToString();
                s.descricaoCentroCusto = reader["descricaoCentroDeCusto"].ToString();
                s.descServicoSolicitado = reader["descricaoServico"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;


                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }
    public static List<SolicitanteDados> CarregaDadosOS_Receber(int idOs)
    {
        var lista = new List<SolicitanteDados>();
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT  [idSolicitacao]
      ,[nomeUsuario]
      ,[rfUsuario]
      ,[ramalSolicitante]
      ,[descricaoCentroDeCusto]
      ,[nomeResponsavel]
      ,[rfResponsavel]
      ,[descricaoPatrimonio]
      ,[andar]
      ,[local]
      ,[obsSolicitacao]
      ,[dataSolicitacao]
      ,[codigoCentroDeCusto]
      ,[resposavelID]
      ,[descricaoServico]
      ,[codigoPatrimonio]
   
  FROM [hspm_OS].[dbo].[Vw_ComplementarOS]
  where idSolicitacao=@idOs";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idOs", idOs);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.idSolicitacao = Convert.ToInt32(reader["idSolicitacao"]);
                s.nomeSolicitante = reader["nomeUsuario"].ToString();
                s.rfSolicitante = reader["rfUsuario"].ToString();
                s.ramalSolicitante = reader["ramalSolicitante"].ToString();
                s.codCentroCusto = Convert.ToInt32(reader["codigoCentroDeCusto"]);
                s.codRespCentroCusto = Convert.ToInt32(reader["resposavelID"]);
                s.descricaoCentroCusto = reader["descricaoCentroDeCusto"].ToString();
                s.nomeResponsavel_Custo = reader["nomeResponsavel"].ToString();
                s.andar = reader["andar"].ToString();
                s.localDaSolicitacao = reader["local"].ToString();
                s.codPatrimonio = Convert.ToInt32(reader["codigoPatrimonio"]);
                s.equipamentoDesc = reader["descricaoPatrimonio"].ToString();
               s.descServicoSolicitado = reader["descricaoServico"].ToString();
                s.obs = reader["obsSolicitacao"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;
                //s.equipamentoDesc = reader["BEM_A_DESC"].ToString();
                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }


    public static List<FinalizarOS> CarregaDadosOS_Finalizar(int idOs)
    {
        var lista = new List<FinalizarOS>();
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT [idSolicitacao]
      ,[nomeUsuario]
      ,[rfUsuario]
      ,[ramalSolicitante]
      ,[descricaoCentroDeCusto]
      ,[nomeResponsavel]
      ,[rfResponsavel]
      ,[descricaoPatrimonio]
      ,[andar]
      ,[local]
      ,[obsSolicitacao]
      ,[dataSolicitacao]
      ,[codigoCentroDeCusto]
      ,[resposavelID]
      ,[descricaoServico]
      ,[descricao]
      ,[ServicoArealizar]
      ,[codigoPatrimonio]
      ,[BEM_A_DESC]
      ,[status]
  FROM [hspm_OS].[dbo].[Vw_Impressao]
  where idSolicitacao=@idOs";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idOs", idOs);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new FinalizarOS();
                s.idSolicitacao = Convert.ToInt32(reader["idSolicitacao"]);
                s.nomeUsuario = reader["nomeUsuario"].ToString();
                s.rfUsuario = reader["rfUsuario"].ToString();
                s.ramalSolicitante = reader["ramalSolicitante"].ToString();
               
                s.descricaoCentroDeCusto = reader["descricaoCentroDeCusto"].ToString();
                s.nomeResponsavel = reader["nomeResponsavel"].ToString();
                s.andar = reader["andar"].ToString();
                s.local = reader["local"].ToString();
                s.codigoPatrimonio = Convert.ToInt32(reader["codigoPatrimonio"]);
                s.descricaoPatrimonio = reader["descricaoPatrimonio"].ToString();
                s.descricaoServico = reader["descricaoServico"].ToString();
                s.obsSolicitacao = reader["obsSolicitacao"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;
                //s.equipamentoDesc = reader["BEM_A_DESC"].ToString();
                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }
    public static List<SolicitanteDados> BuscarSetoresSolicitados()
    {
        var lista = new List<SolicitanteDados>();
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT [id],[descricao]      
             FROM [hspm_OS].[dbo].[setorSolicitado] 
             order by descricao";

            SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@loginRede", login);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.codSetorSolicitado = Convert.ToInt32(reader["id"]);
                s.setorSolicitadoDesc = reader["descricao"].ToString();
                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }

    public static bool GravaSolicitacaoOSRecebidaRecusa(ReceberOS s)
    {
        bool sucesso = true;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery = @"INSERT INTO [dbo].[receberOS]
                                ([id_solicitacao], [justificativa_recusar],[dataRecebimento])
                                VALUES (@id_solicitacao, @justificativa_recusar, @dataRecebimento)";

                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@id_solicitacao", s.id_solicitacao);
                cmd.Parameters.AddWithValue("@justificativa_recusar", s.justificativa_recusar);
                cmd.Parameters.AddWithValue("@dataRecebimento", s.dataRecebimento);
                con.Open();
                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    AtualizaStatusOS(s); // chama o update só se inseriu com sucesso
                }
               
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                sucesso = false;
            }
        }
        return sucesso;
    }

    public static bool GravaSolicitacaoOSRecebida(ReceberOS r)
    {
        bool sucesso = false;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery = @"INSERT INTO [dbo].[receberOS]
                                ([id_solicitacao], [codServicoRealizar], [dataRecebimento])
                                VALUES (@id_solicitacao, @codServicoRealizar, @dataRecebimento)";

                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@id_solicitacao", r.id_solicitacao);
            
                cmd.Parameters.AddWithValue("@codServicoRealizar", r.codServicoRealizar);

                cmd.Parameters.AddWithValue("@dataRecebimento", r.dataRecebimento);

                con.Open();
                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    AtualizaStatusOS(r); // chama o update só se inseriu com sucesso
                    sucesso = true;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                sucesso = false;
            }
        }
        return sucesso;
    }

    public static int AtualizaStatusOS(ReceberOS s)
    {
        int linhasAfetadas = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery = @"
                UPDATE [dbo].[Solicitacao]
                SET [status] = @status
                WHERE [id_solicitacao] = @idSolicitacao;";

                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@status", s.codStatus);
                cmd.Parameters.AddWithValue("@idSolicitacao", s.id_solicitacao);

                con.Open();
                linhasAfetadas = cmd.ExecuteNonQuery(); // retorna quantas linhas foram atualizadas
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                linhasAfetadas = 0;
            }
        }
        return linhasAfetadas;
    }
         
    public static List<SolicitanteDados> CarregaDadosOS_Imprimir(int idOs)
    {
        var lista = new List<SolicitanteDados>();
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT [idSolicitacao]
      ,[nomeUsuario]
      ,[rfUsuario]
      ,[ramalSolicitante]
      ,[descricaoCentroDeCusto]
      ,[nomeResponsavel]
      ,[rfResponsavel]
      ,[descricaoPatrimonio]
      ,[andar]
      ,[local]
      ,[obsSolicitacao]
      ,[dataSolicitacao]
      ,[codigoCentroDeCusto]    
      ,[descricaoServico]
      ,[descricao]
      ,[ServicoArealizar]
      ,[codigoPatrimonio]
      ,[BEM_A_DESC]
  FROM [hspm_OS].[dbo].[Vw_Impressao]
  where idSolicitacao=@idOs";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idOs", idOs);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.idSolicitacao = Convert.ToInt32(reader["idSolicitacao"]);
                s.nomeSolicitante = reader["nomeUsuario"].ToString();
                s.rfSolicitante = reader["rfUsuario"].ToString();
                s.ramalSolicitante = reader["ramalSolicitante"].ToString();
                s.codCentroCusto = Convert.ToInt32(reader["codigoCentroDeCusto"]);
                //s.codRespCentroCusto = Convert.ToInt32(reader["resposavelID"]);
                s.descricaoCentroCusto = reader["descricaoCentroDeCusto"].ToString();
                s.nomeResponsavel_Custo = reader["nomeResponsavel"].ToString();
                s.andar = reader["andar"].ToString();
                s.localDaSolicitacao = reader["local"].ToString();
                s.codPatrimonio = Convert.ToInt32(reader["codigoPatrimonio"]);
                s.equipamentoDesc = reader["descricaoPatrimonio"].ToString();
                s.descServicoSolicitado = reader["descricaoServico"].ToString();
                s.obs = reader["obsSolicitacao"].ToString();
                s.setorSolicitadoDesc = reader["descricao"].ToString();
                s.servicoSolicitadoDesc = reader["ServicoArealizar"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;
                //s.equipamentoDesc = reader["BEM_A_DESC"].ToString();
                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }
    public static bool GravaSolicitacaoOSRecebida(SolicitanteDados s)
    {
        bool sucesso = false;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery = @"INSERT INTO [dbo].[receberOS]
                                ([id_solicitacao], [codSetorSolcitado], [codServicoRealizar])
                                VALUES (@id_solicitacao, @codSetorSolcitado, @codServicoRealizar)";

                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@id_solicitacao", s.idSolicitacao);
                cmd.Parameters.AddWithValue("@codSetorSolcitado", s.codSetorSolicitado);
                cmd.Parameters.AddWithValue("@codServicoRealizar", s.idServicoSolicitado);

                con.Open();
                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    AtualizaStatusOS2(s); // chama o update só se inseriu com sucesso
                    sucesso = true;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                sucesso = false;
            }
        }
        return sucesso;
    }
    public static int AtualizaStatusOS2(SolicitanteDados s)
    {
        int linhasAfetadas = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery = @"
                UPDATE [dbo].[Solicitacao]
                SET [status] = @status
                WHERE [id_solicitacao] = @idSolicitacao;";

                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@status", s.codStatusSolicitacao);
                cmd.Parameters.AddWithValue("@idSolicitacao", s.idSolicitacao);

                con.Open();
                linhasAfetadas = cmd.ExecuteNonQuery(); // retorna quantas linhas foram atualizadas
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                linhasAfetadas = 0;
            }
        }
        return linhasAfetadas;
    }

}

//                s.statusSolicitacao = "Aguardando";
//                break;
//            case 1:
//                s.statusSolicitacao = "Recebido";
//                break;
//            case 2:
//                s.statusSolicitacao = "Em espera, executando";
//                break;
//            case 3:
//                s.statusSolicitacao = "Finalizado";
//                break;
//            case 4:
//                s.statusSolicitacao = "Recusado";
//                break;
//            default:
//                s.statusSolicitacao = "Desconhecido";
//                break;
//        }