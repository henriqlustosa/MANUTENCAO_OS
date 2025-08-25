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
    /// Retorna o UsuarioId a partir do LoginRede.
    /// </summary>
    public static int ObterIdPorLogin(string loginRede)
    {
        int usuarioId = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT TOP 1 UsuarioId
                FROM dbo.Usuarios
                WHERE LoginRede = @loginRede
                  AND Ativo = 1";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@loginRede", loginRede);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    usuarioId = Convert.ToInt32(result);
            }
        }

        return usuarioId;
    }

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
                                ([id_solicitacao], [codServicoRealizar], [dataRecebimento], [idUsuarioReceber])
                                VALUES (@id_solicitacao, @codServicoRealizar, @dataRecebimento,@idUsuarioReceber )";

                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.AddWithValue("@id_solicitacao", r.id_solicitacao);

                cmd.Parameters.AddWithValue("@codServicoRealizar", r.codServicoRealizar);

                cmd.Parameters.AddWithValue("@dataRecebimento", r.dataRecebimento);
                cmd.Parameters.AddWithValue("@idUsuarioReceber", r.idUsuarioReceber);

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
                    AtualizaStatusOS(s.codStatusSolicitacao, s.idSolicitacao); // chama o update só se inseriu com sucesso
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
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }
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
    private static int? TryParseDuracaoParaMinutos(string texto)
    {
        if (StringHelper.IsNullOrWhiteSpace(texto))
            return null;

        texto = texto.Trim();

        // Somente horas? (ex.: "50")
        if (!texto.Contains(":"))
        {
            if (int.TryParse(texto, out int horasInteiras) && horasInteiras >= 0)
                return horasInteiras * 60;
            return null;
        }

        // Tem ":" -> tentar HHH:MM(:SS)
        var partes = texto.Split(':');
        if (partes.Length < 2) return null;

        if (!int.TryParse(partes[0], out int horas)) return null;
        if (!int.TryParse(partes[1], out int minutos)) return null;
        if (horas < 0 || minutos < 0 || minutos > 59) return null;

        // Ignora segundos se houver
        // (Se quiser arredondar por segundos, pode ler partes[2] e tratar)
        return (horas * 60) + minutos;
    }

    public static bool GravaFinalizacaoOSRecebida(FinalizadoOS s)
    {
        bool sucesso = true;

        // Garante dataCadastro
        if (s.dataCadastro == default(DateTime))
            s.dataCadastro = DateTime.Now;

        // Converte s.hora -> total de minutos (int)
        int? minutos = TryParseDuracaoParaMinutos(s.hora);

        string cs = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();

        using (SqlConnection con = new SqlConnection(cs))
        {
            try
            {
                const string sql = @"
INSERT INTO dbo.finalizarOS
    (id_solicitacao, dataFinalizacao, dataCadastro, qtdMinutos, idUsuarioFinalizar)
VALUES
    (@id_solicitacao, @dataFinalizacao, @dataCadastro, @qtdMinutos, @idUsuarioFinalizar);";

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

                    // qtdMinutos (int) — nulo se não informado/parse inválido
                    if (minutos.HasValue)
                        cmd.Parameters.Add("@qtdMinutos", SqlDbType.Int).Value = minutos.Value;
                    else
                        cmd.Parameters.Add("@qtdMinutos", SqlDbType.Int).Value = DBNull.Value;

                    // idUsuarioFinalizar (int)
                    cmd.Parameters.Add("@idUsuarioFinalizar", SqlDbType.Int).Value = s.idUsuarioFinalizar;

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
    public static List<FinalizarOS> CarregaDadosOS_Finalizada_Visualizar(int idOs)
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
      ,[dataFinalizacao]
	  ,[hora]
      ,[nome_funcionario]
 FROM [hspm_OS].[dbo].[Vw_FinalizadasVizualizar]
  where idSolicitacao=@idOs";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idOs", idOs);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            string nome2 = "";
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
                s.dtOSfinalizada = reader["dataFinalizacao"].ToString();
                s.qtdHorasServiço = reader["hora"].ToString();
                string nome = reader["nome_funcionario"].ToString();                
                nome2+= nome+"\n";
                s.nomeFuncionario_Operacional = nome2;




                //s.equipamentoDesc = reader["BEM_A_DESC"].ToString();
                lista.Add(s);
            }
            con.Close();
        }
        return lista;
    }







    // Recupera a string de conexão do arquivo Web.config
    private static readonly string connectionString = ConfigurationManager.ConnectionStrings["hspm_OSConnectionString"].ToString();
    // funçoes para abrir OS dentro do sistema OS_Manutenção
    public static List<SolicitanteDados> VerificaOsAbertaPatrimonio(int patrimonio)
    {
        var lista = new List<SolicitanteDados>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT NomeCompleto, id_solicitacao, dataSolicitacao, patrimonio, local, descricaoServico, status
                FROM Vw_VericaOsAbertaPatrimonio
                WHERE (status <> 4 AND status <> 5)
                  AND patrimonio = @patrimonio
                  AND patrimonio != 46879 AND patrimonio != 1";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@patrimonio", patrimonio);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.nomeSolicitante = reader["NomeCompleto"].ToString();
                s.idSolicitacao = Convert.ToInt32(reader["id_solicitacao"]);
                s.localDaSolicitacao = reader["local"].ToString();
                s.descServicoSolicitado = reader["descricaoServico"].ToString();
                s.dataSolicitacao = reader["dataSolicitacao"] != DBNull.Value ? Convert.ToDateTime(reader["dataSolicitacao"]) : DateTime.MinValue;
                s.codPatrimonio = Convert.ToInt32(reader["patrimonio"]);
                s.statusSolicitacao = ObterDescricaoStatus(Convert.ToInt32(reader["status"]));
                lista.Add(s);
            }
        }

        return lista;
    }

    private static string ObterDescricaoStatus(int statusId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(@"
        SELECT Nome 
        FROM dbo.StatusDaSolicitacao 
        WHERE StatusId = @StatusId", con))
        {
            cmd.Parameters.AddWithValue("@StatusId", statusId);

            con.Open();
            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
                return result.ToString();

            return "Desconhecido"; // valor padrão caso não encontre
        }
    }
    public static int GravaSolicitacaoOS(Solicitacao_Pedido s)
    {
        if (s == null) throw new ArgumentNullException("s");

        int novoId;

        // Use o mesmo nome de chave do field estático da classe
        string cs = ConfigurationManager
            .ConnectionStrings["hspm_OSConnectionString"]
            .ConnectionString;

        const string sql = @"
INSERT INTO dbo.Solicitacao
(
    UsuarioSolicitanteId,
    CentroCustoId,
    UsuarioRespCCId,
    ramalSolicitante,
    ramalRespCusto,
    patrimonio,
    andar,
    [local],
    descricaoServico,
    obs,
    dataSolicitacao,
    [status]

)
VALUES
(
    @UsuarioSolicitanteId,
    @CentroCustoId,
    @UsuarioRespCCId,
    @ramalSolicitante,
    @ramalRespCusto,
    @patrimonio,
    @andar,
    @local,
    @descricaoServico,
    @obs,
    @dataSolicitacao,
    @status
   
);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using (var con = new SqlConnection(cs))
        using (var cmd = new SqlCommand(sql, con))
        {
            // IDs obrigatórios
            cmd.Parameters.Add("@UsuarioSolicitanteId", SqlDbType.Int).Value = s.UsuarioSolicitanteId;
            cmd.Parameters.Add("@CentroCustoId", SqlDbType.Int).Value = s.CentroCustoId;
            cmd.Parameters.Add("@UsuarioRespCCId", SqlDbType.Int).Value = s.UsuarioRespCCId;

            // Strings curtas (null -> DBNull)
            cmd.Parameters.Add("@ramalSolicitante", SqlDbType.VarChar, 10)
               .Value = (object)(s.RamalSolicitante ?? (string)null) ?? DBNull.Value;

            cmd.Parameters.Add("@ramalRespCusto", SqlDbType.VarChar, 10)
               .Value = (object)(s.RamalRespCusto ?? (string)null) ?? DBNull.Value;

            // Patrimônio (tenta converter)
            object patrimonio = DBNull.Value;
            if (!IsNullOrWhiteSpace(s.Patrimonio))
            {
                int p;
                if (int.TryParse(s.Patrimonio, out p))
                    patrimonio = p;
            }
            cmd.Parameters.Add("@patrimonio", SqlDbType.Int).Value = patrimonio;

            // Demais textos
            cmd.Parameters.Add("@andar", SqlDbType.VarChar, 20).Value = (object)(s.Andar ?? (string)null) ?? DBNull.Value;
            cmd.Parameters.Add("@local", SqlDbType.VarChar, 100).Value = (object)(s.Local ?? (string)null) ?? DBNull.Value;
            cmd.Parameters.Add("@descricaoServico", SqlDbType.VarChar, 1000).Value = (object)(s.DescricaoServico ?? (string)null) ?? DBNull.Value;
            cmd.Parameters.Add("@obs", SqlDbType.VarChar, 1000).Value = (object)(s.Obs ?? (string)null) ?? DBNull.Value;

            // Datas
            cmd.Parameters.Add("@dataSolicitacao", SqlDbType.DateTime2)
               .Value = (object)(s.DataSolicitacao != default(DateTime) ? s.DataSolicitacao : DateTime.Now);



            cmd.Parameters.Add("@status", SqlDbType.Int).Value = s.Status;


            con.Open();
            novoId = (int)cmd.ExecuteScalar();
        }

        return novoId;
    }
    // Replace all usages of string.IsNullOrWhiteSpace with a compatible implementation for .NET 3.5
    private static bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim().Length == 0;
    }
    public static List<SolicitanteDados> CarregaDadosUsuarioEResponsavel(string login)
    {
        var lista = new List<SolicitanteDados>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT [nomeUsuario],[loginRedeUsuario],[rfResponsavel],[nomeResponsavel],[descricao],[codigoCentroDeCusto],[rfUsuario]
                FROM Vw_DadosUsuario_DadosRespCusto
                WHERE loginRedeUsuario = @loginRede";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@loginRede", login.Trim());

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var s = new SolicitanteDados();
                s.nomeSolicitante = dr["nomeUsuario"] as string;
                s.loginSolicitante = dr["loginRedeUsuario"] as string;
                s.rfSolicitante = dr["rfUsuario"] as string;
                s.nomeResponsavel_Custo = dr["nomeResponsavel"] as string;
                s.rfResponsavelCusto = dr["rfResponsavel"] as string;
                s.descricaoCentroCusto = dr["descricao"] as string;
                s.codCentroCusto = Convert.ToInt32(dr["codigoCentroDeCusto"]);

                s.rfUsuario = Convert.ToInt32(dr["rfUsuario"]);
                lista.Add(s);
            }
        }

        return lista;
    }
    public static List<SolicitanteDados> BuscarPatrimonio(int codPatrimonio)
    {
        var lista = new List<SolicitanteDados>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT BEM_A_DESC
                FROM NumeroPatrimonio
                WHERE BEM_A_CHAP = @BEM_A_CHAP";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BEM_A_CHAP", codPatrimonio);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.equipamentoDesc = reader["BEM_A_DESC"].ToString();
                lista.Add(s);
            }
        }

        return lista;
    }
    public static List<SolicitanteDados> BuscarResponsavelPorCentroDeCusto(string centroCusto)
    {
        var lista = new List<SolicitanteDados>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT [nomeResponsavel], [rfResponsavel], loginRedeResponsavel
                FROM Vw_DadosUsuario_DadosRespCusto
                WHERE [codigoCentroDeCusto] = @Codigo_centroCusto";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Codigo_centroCusto", centroCusto);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.nomeResponsavel_Custo = reader["nomeResponsavel"].ToString();
                s.rfResponsavelCusto = reader["rfResponsavel"].ToString();
                s.codRespCentroCusto = Convert.ToInt32(reader["loginRedeResponsavel"].ToString());
                lista.Add(s);
            }
        }

        return lista;
    }
    public static List<SolicitanteDados> BuscarCentroDeCustoPorLogin(string login)
    {
        var lista = new List<SolicitanteDados>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT  [codigoCentroDeCusto],[descricaoCentroDeCusto]
                FROM Vw_Usuario_CentroDeCusto
                WHERE LoginDeRedeUsuario = @loginRede";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@loginRede", login);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var s = new SolicitanteDados();
                s.codCentroCusto = Convert.ToInt32(reader["codigoCentroDeCusto"]);
                s.descricaoCentroCusto = reader["codigoCentroDeCusto"] + " - " + reader["descricaoCentroDeCusto"].ToString();

                lista.Add(s);
            }
        }

        return lista;
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