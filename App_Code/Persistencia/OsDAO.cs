using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
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
  FROM [hspm_OS].[dbo].[Vw_ReceberOS] where status = " + status + " order by id_solicitacao asc";

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
                s.codigoCentroDeCusto = Convert.ToInt32(reader["codigoCentroDeCusto"].ToString());
                s.descricaoCentroDeCusto = reader["descricaoCentroDeCusto"].ToString();
                s.nomeResponsavel = reader["nomeResponsavel"].ToString();
                s.andar = reader["andar"].ToString();
                s.local = reader["local"].ToString();
                s.codigoPatrimonio = Convert.ToInt32(reader["codigoPatrimonio"]);
                s.descricaoPatrimonio = reader["descricaoPatrimonio"].ToString();
                s.descricaoServico = reader["descricaoServico"].ToString();
                s.obsSolicitacao = reader["obsSolicitacao"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;
                s.descricao = reader["descricao"].ToString();
                s.ServicoArealizar = reader["ServicoArealizar"].ToString();
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
                    AtualizaStatusOS(s.codStatus, s.id_solicitacao); // chama o update só se inseriu com sucesso
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
                    AtualizaStatusOS(r.codStatus, r.id_solicitacao); // chama o update só se inseriu com sucesso
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
                    AtualizaStatusOS(s.codStatusSolicitacao,s.idSolicitacao); // chama o update só se inseriu com sucesso
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
   
    public static object carregaFuncionarios(int id)
    {
        var lista = new List<Funcionario>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery;
                strQuery = @"SELECT idFuncionario,nomeFuncionario
  FROM [hspm_OS].[dbo].[View_Visualizar_Equipe_Funcionario_Finalizar] where id_solicitacao =" + id + " order by nomeFuncionario asc ";
                con.Open();
                SqlCommand commd = new SqlCommand(strQuery, con);
                SqlDataReader dr = commd.ExecuteReader();


                while (dr.Read())
                {
                    Funcionario c = new Funcionario();
                    c.id_funcionario = dr.GetInt32(0);
                    c.nome_funcionario = dr.IsDBNull(1) ? null : dr.GetString(1);
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
    public static int AtualizaStatusOS(int status, int idSolicitacao)
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
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@idSolicitacao", idSolicitacao);

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

 

 
        // Converte "HH:mm" -> TimeSpan (C# 3.0 compatível)
        private static bool TryParseHoraHHmm(string hhmm, out TimeSpan ts)
        {
            ts = TimeSpan.Zero;
            if (string.IsNullOrEmpty(hhmm)) return false;

            string s = hhmm.Trim();
            Match m = Regex.Match(s, @"^(?<H>\d{1,2}):(?<M>[0-5]\d)$");
            if (!m.Success) return false;

            int h, mi;
            if (!int.TryParse(m.Groups["H"].Value, out h)) return false;
            if (!int.TryParse(m.Groups["M"].Value, out mi)) return false;

            // limite de horas (ajuste se quiser 0–23)
            if (h < 0 || h > 99) return false;

            ts = new TimeSpan(h, mi, 0);
            return true;
        }

        public static bool GravaFinalizacaoOSRecebida(FinalizadoOS s)
        {
            bool sucesso = true;

            // Garantia de dataCadastro (fixo no momento da gravação, se vier default)
            if (s.dataCadastro == default(DateTime))
                s.dataCadastro = DateTime.Now;

            // Prepara a hora como TimeSpan (gravar na coluna time)
            TimeSpan? horaTs = null;
            if (!string.IsNullOrEmpty(s.hora) && s.hora.Trim() != "")
            {
                TimeSpan ts;
                if (TryParseHoraHHmm(s.hora, out ts))
                    horaTs = ts;
            }

            string cs = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    const string sql = @"
INSERT INTO dbo.finalizarOS
    (id_solicitacao, dataFinalizacao, dataCadastro, hora)
VALUES
    (@id_solicitacao, @dataFinalizacao, @dataCadastro, @hora);";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // id_solicitacao (int)
                        cmd.Parameters.Add("@id_solicitacao", SqlDbType.Int).Value = s.idSolicitacao;

                        // dataFinalizacao (datetime) — permite nulo se não informado
                        if (s.dataFinalizacao != default(DateTime))
                            cmd.Parameters.Add("@dataFinalizacao", SqlDbType.DateTime).Value = s.dataFinalizacao;
                        else
                            cmd.Parameters.Add("@dataFinalizacao", SqlDbType.DateTime).Value = DBNull.Value;

                        // dataCadastro (datetime)
                        cmd.Parameters.Add("@dataCadastro", SqlDbType.DateTime).Value = s.dataCadastro;

                        // hora (time) — grava nulo se parsing falhar
                        if (horaTs.HasValue)
                            cmd.Parameters.Add("@hora", SqlDbType.Time).Value = horaTs.Value;
                        else
                            cmd.Parameters.Add("@hora", SqlDbType.Time).Value = DBNull.Value;

                        // idUsuarioFinalizar (int)
                        //cmd.Parameters.Add("@idUsuarioFinalizar", SqlDbType.Int).Value = idUsuarioFinalizar;

                        con.Open();
                        int linhas = cmd.ExecuteNonQuery();

                        if (linhas > 0)
                        {
                            // Atualiza status somente após inserir com sucesso
                            AtualizaStatusOS(s.status, s.idSolicitacao);
                        }
                        else
                        {
                            sucesso = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // logue se tiver logger
                    string erro = ex.Message;
                    sucesso = false;
                }
            }

            return sucesso;
        }

    public static bool GravaFuncionarioFinalizacao(FuncionarioFinalizacao funcionario)
    {
        bool sucesso = true;

        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString()))
        {
            try
            {
                string strQuery = @"
                INSERT INTO [dbo].[Funcionarios_Finalizacao]
                       ([id_funcionario]
                       ,[id_solicitacao]
                       ,[status]
                       ,[dataCadastro])
                 VALUES
                       (@id_funcionario
                       ,@id_solicitacao
                       ,@status
                       ,@dataCadastro)";

                using (SqlCommand cmd = new SqlCommand(strQuery, con))
                {
                    // parâmetros
                    cmd.Parameters.AddWithValue("@id_funcionario", funcionario.id_funcionario);
                    cmd.Parameters.AddWithValue("@id_solicitacao", funcionario.idSolicitacao);
                    cmd.Parameters.AddWithValue("@status", funcionario.status);
                    cmd.Parameters.AddWithValue("@dataCadastro", funcionario.dataCadastro);

                    con.Open();
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    sucesso = linhasAfetadas > 0;
                }
            }
            catch (Exception ex)
            {
                // Aqui você pode logar o erro em vez de engolir a exceção
                string erro = ex.Message;
                sucesso = false;
            }
        }

        return sucesso;
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