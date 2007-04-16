using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

using Secv.Dado;



namespace Secv.Ng.Manutencao.CadeiaAutorizacao
{

    /// <summary>
    /// Tipo de participacao do usu�rio numa inst�ncia de cadeia
    /// </summary>
    public enum TipoParticipacao : int
    {
        /// <summary>
        /// Respons�vel por autorizar, a sua participa��o finaliza a autoriza��o
        /// </summary>
        Responsavel = 1,
        /// <summary>
        /// Participa da autoriza��o, mas sua participa��o n�o � definitiva
        /// </summary>
        Colaborador = 2,
        /// <summary>
        /// Participa apenas como leitor
        /// </summary>
        Leitor = 3
    }

    /// <summary>
    /// Participa��o do usu�rio numa inst�ncia
    /// </summary>
    public enum Autorizacao
    {
        Autorizado = 1,
        NaoAutorizado = 2
    }

    /// <summary>
    /// Status de uma inst�ncia de cadeia
    /// </summary>
    public enum StatusCadeia
    {
        Cancelada = -1,
        Aberta = 0,
        Aprovada = 1,
        Reprovada = 2
    }

    /// <summary>
    /// Classe representando a API do mecanismo de cadeia de autoriza��o
    /// Data: 03/11/2006
    /// Autor: Jos� Maria R Santos J�nior
    /// </summary>
    public class CadeiaAutorizacaoNG
    {

        /// <summary>
        /// Retorna o chamado para o Usp_ID
        /// </summary>
        /// <param name="usp_ID"></param>
        /// <returns></returns>
        public static int ObterCai_IDPorUsp_ID(int usp_ID)
        {
            string sql = @"SELECT Cai_ID
                        FROM CadeiaAutorizacao.UsuarioParticipacao
                        WHERE Usp_ID = @Usp_ID";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Usp_ID", DbType.Int32, usp_ID);
            return bd.ExecuteScalar<int>();
        }

        /// <summary>
        /// Cancela uma inst�ncia de cadeia. Altera o status para StatusCadeia.Cancelada (-1)
        /// </summary>
        /// <param name="IdInstancia">Id da Inst�ncia</param>
        public static void Cancelar(int IdInstancia)
        {
            //using (Escopo escopo = new Escopo(OpcaoTransacao.Requerido))
            //{
                string sql = @"UPDATE CadeiaAutorizacao.CadeiaInstancia
                             SET Cai_Status = -1
                             WHERE Cai_Id = @Cai_Id";
                BdUtil bd = new BdUtil(sql);
                bd.AdicionarParametro("@Cai_Id", DbType.Int32, IdInstancia);
                bd.ExecuteNonQuery();
            //}
        }

        /// <summary>
        /// Cria uma nova inst�ncia de cadeia de autoriza��o (tabela: CadeiaAutorizacaoInstancia)
        /// Adicionando tamb�m as paticipa��es dos superiores hier�rquicos do usu�rio participante
        /// </summary>
        /// <param name="IdCadeia">Id da cadeia de autoriza��o</param>
        /// <param name="IdProprietario">Id do usu�rio propriet�rio da inst�ncia da cadeia</param>
        /// <param name="valor">valor</param>
        /// <param name="descricao">descri��o (t�tulo)</param>
        /// <param name="conteudo">conte�do da inst�ncia</param>
        /// <param name="parametrosFuncaoHierarquia">Cole��o com os par�metros para executar a fun��o de hierarquia.
        /// dever� ser null quando a cadeia n�o possuir fun��o armazenada de hierarquia</param>
        /// <returns>Id da inst�ncia cadeia. O sistema dever� armazenar este valor, pois quando
        /// esta inst�ncia de cadeia for finalizada este valor ser� fornecido
        /// ao m�todo do sistema especificado em CadeiaAutorizacao.Cau_MetodoSistema
        /// </returns>
        public static int Incluir(int IdCadeia, int IdProprietario, float? valor,
                                  string descricao, string conteudo,
                                  CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable parametrosFuncaoHierarquia)
        {

            int IdInstancia = 0;
            using (Escopo escopo = new Escopo(OpcaoTransacao.Requerido))
            {
                IdInstancia = IncluirInstancia(descricao, DateTime.Now, StatusCadeia.Aberta, valor, conteudo,
                                                   IdCadeia, IdProprietario);

                // Recuperar os superiores do usu�rio (atrav�s da fun��es ou tabelas de hierarquia) e 
                // para cada superior inserir uma linha na tabela UsuarioParticipante,
                // onde o usu�rio de maior hierarquia ser� o Respons�vel
                DataTable superiores = ObterSuperioresUsuario(IdCadeia, IdProprietario, parametrosFuncaoHierarquia);
                // Inclu�ndo a participa��o de cada superior
                for (int i = 0; i < superiores.Rows.Count; i++)
                {
                    DataRow superior = superiores.Rows[i];
                    // Se a hierarquia for descoberta atrav�s das tabelas
                    if (parametrosFuncaoHierarquia == null)
                    {
                        // A participa��o padr�o � 2 = Colaborador
                        TipoParticipacao participacao = TipoParticipacao.Colaborador;
                        // O �limo ser� o superior de mais alto n�vel, sendo o respons�vel
                        if (i == superiores.Rows.Count - 1)
                        {
                            participacao = TipoParticipacao.Responsavel;
                        }
                        // Adicionando a participa��o do superior
                        IncluirParticipante(IdInstancia, Convert.ToInt32(superior["IdSuperior"]), DateTime.Now, participacao);
                    }
                    else // Se a hierarquia for descoberta atrav�s de stored function
                    {
                        IncluirParticipante(IdInstancia, Convert.ToInt32(superior["IdSuperior"]), DateTime.Now, (TipoParticipacao) superior["Participacao"]);
                    }
                }
                escopo.Terminar();
            }
            return IdInstancia;
        }

        /// <summary>
        /// Insere no banco de dados uma inst�ncia de cadeia de autoriza��o
        /// </summary>
        /// <param name="descricao">Descri��o da inst�ncia</param>
        /// <param name="data">Data</param>
        /// <param name="statusCadeia">Status</param>
        /// <param name="valor">Valor</param>
        /// <param name="conteudo">Conte�do</param>
        /// <param name="IdCadeia">Id da cadeia</param>
        /// <param name="IdProprietario">Id do usu�rio que iniciou a inst�ncia da cadeia</param>
        /// <returns></returns>
        private static int IncluirInstancia(string descricao, DateTime data, StatusCadeia statusCadeia, float? valor, string conteudo,
                                            int IdCadeia, int IdProprietario)
        {
            string sql = @"INSERT INTO CadeiaAutorizacao.CadeiaInstancia
                             (Cai_Descri, Cai_Data, Cai_Status, Cau_Id, Usu_IdProprietario, Cai_Valor, Cai_Conteudo) 
                             VALUES (@Cai_Descri, @Cai_Data, @Cai_Status, @Cau_Id, @Usu_IdProprietario, @Cai_Valor, @Cai_Conteudo);
                           SELECT SCOPE_IDENTITY();";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Cai_Descri", DbType.String, descricao);
            bd.AdicionarParametro("@Cai_Data", DbType.DateTime, data);
            bd.AdicionarParametro("@Cai_Status", DbType.Int16, (int)statusCadeia);
            bd.AdicionarParametro("@Cau_Id", DbType.Int32, IdCadeia);
            bd.AdicionarParametro("@Usu_IdProprietario", DbType.Int32, IdProprietario);
            bd.AdicionarParametro("@Cai_Valor", DbType.Decimal, valor);
            bd.AdicionarParametro("@Cai_Conteudo", DbType.String, conteudo);
            object objRetorno = bd.ExecuteScalar();
            return (objRetorno == null) ? 0 : Convert.ToInt32(objRetorno);
            
        }

        /// <summary>
        /// Obt�m os superiores de um usu�rio para uma determinada cadeia
        /// </summary>
        /// <param name="IdCadeia">Id da cadeia</param>
        /// <param name="IdUsuario">Id da cadeia</param>
        /// <param name="parametrosFuncaoHierarquia">Par�metros para executar a store procedure para a hierarquia</param>
        /// <returns>Superiores do usu�rio, onde a �ltima linha ser� o s�perior de mais alto n�vel e respons�vel pela cadeia</returns>
        private static DataTable ObterSuperioresUsuario(int IdCadeia, int IdUsuario,
                                                        CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable parametrosFuncaoHierarquia)
        {
            DataTable superiores = null;
            // Identificando o mecanismo para obter a hierarquia
            // Casdo seja atrav�s da fun��o armazenada
            if (parametrosFuncaoHierarquia != null)
            {
                superiores = ObterSuperioresPorFuncao(IdCadeia, IdUsuario, parametrosFuncaoHierarquia);
            }
            else
            {
                // Caso seja atrav�s das tabelas de hierarquia
                superiores = ObterSuperioresPorTabelas(IdCadeia, IdUsuario);
            }
            return superiores;
        }

        /// <summary>
        /// Obt�m os superiores do usu�rio baseado na estrutura de tabelas para hierarquia
        /// </summary>
        /// <param name="IdCadeia">Id da cadeia</param>
        /// <param name="IdUsuario">Id do usu�rio</param>
        /// <returns>Superiores do usu�rio</returns>
        private static DataTable ObterSuperioresPorTabelas(int IdCadeia, int IdUsuario)
        {
            string sql = @"WITH Superior (IdSuperior, alcada) AS
                           (
                             SELECT hus.Usu_IdSuperior, hus.Hus_Alcada
                               FROM  CadeiaAutorizacao.HierarquiaUsuario hus
                               JOIN CadeiaAutorizacao.Cadeia cau on cau.Hie_Id = hus.Hie_Id
                               WHERE hus.Usu_Id = @Usu_Id and
                                     cau.Cau_Id = @Cau_Id

                              UNION ALL 

                              SELECT Hus.Usu_IdSuperior, hus.Hus_Alcada
                               FROM CadeiaAutorizacao.HierarquiaUsuario hus
                               JOIN CadeiaAutorizacao.Cadeia cau on cau.Hie_Id = hus.Hie_Id
                               JOIN Superior ON hus.Usu_Id = Superior.IdSuperior
									and Cau_Id = @Cau_Id 
                           )
                           SELECT IdSuperior, alcada
                             FROM Superior
                             WHERE IdSuperior IS NOT NULL;";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Usu_Id", DbType.Int32, IdUsuario);
            bd.AdicionarParametro("@Cau_Id", DbType.Int32, IdCadeia);
            return bd.ObterDataTable();
        }

        /// <summary>
        /// Obt�m os superiores do usu�rio executando a stored function de hierarquia
        /// </summary>
        /// <param name="IdCadeia">Id da cadeia</param>
        /// <param name="IdUsuario">Id do usu�rio</param>
        /// <param name="parametrosFuncaoHierarquia">Par�metros para executar a stored function de hierarquia</param>
        /// <returns></returns>
        private static DataTable ObterSuperioresPorFuncao(int IdCadeia, int IdUsuario,
                                                          CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable parametrosFuncaoHierarquia)
        {
            // Obtendo o nome da funcao para a hierarquia da cadeia
            string nomeFuncao = ObterNomeFuncaHierarquia(IdCadeia);
            if (string.IsNullOrEmpty(nomeFuncao))
            {
                throw new SecvException("Fun��o de hirarquia n�o encontrada");
            }
            BdUtil bd = new BdUtil(nomeFuncao);
            //bd.Command.CommandType = CommandType.Text;// StoredProcedure;
            // Preparando os par�metros
            foreach (DataRow parametro in parametrosFuncaoHierarquia)
            {
                string nome = parametro["nome"].ToString();
                DbType tipo = (DbType)Enum.Parse(typeof(DbType), parametro["tipo"].ToString(), true);
                object valor = parametro["valor"];
                bd.AdicionarParametro(nome, tipo, valor);
            }
            return bd.ObterDataTable();
        }

        /// <summary>
        /// Obt�m o nome da fun��o de hierarquia para uma determinada cadeia
        /// </summary>
        /// <param name="IdCadeia">Id da cadeia</param>
        /// <returns>Nome da stored function de hierarquia</returns>
        private static string ObterNomeFuncaHierarquia(int IdCadeia)
        {
            string sql = @"SELECT h.Hie_Funcao 
                             FROM CadeiaAutorizacao.Hierarquia h
                             INNER JOIN CadeiaAutorizacao.Cadeia c on c.Hie_Id = h.Hie_Id
                             WHERE c.cau_Id = @IdCadeia";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdCadeia", DbType.Int32, IdCadeia);
            return Convert.ToString(bd.ExecuteScalar());
        }

        /// <summary>
        /// Incluir um participante na inst�ncia da cadeia de autorizacao
        /// </summary>
        /// <param name="IdInstancia">Id da inst�ncia</param>
        /// <param name="IdParticipante">Id do usu�rio participante</param>
        /// <param name="data">Data da cria��o da participa��o</param>
        /// <param name="tipo">Tipo da participa��o: 1 = Respons�vel; 2 = Colaborador; 3 = Leitor</param>
        public static void IncluirParticipante(int IdInstancia, int IdParticipante,
                                               DateTime data, TipoParticipacao tipoParticipacao)
        {
            string sql = @"INSERT INTO CadeiaAutorizacao.UsuarioParticipacao
                             (Cai_Id, Usu_Id, Usp_Tipo, Usp_Data)
                             VALUES (@Cai_Id, @Usu_Id, @Usp_Tipo, @Usp_Data)";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Cai_Id", DbType.Int32, IdInstancia);
            bd.AdicionarParametro("@Usu_Id", DbType.Int32, IdParticipante);
            bd.AdicionarParametro("@Usp_Tipo", DbType.Int16, (int)tipoParticipacao);
            bd.AdicionarParametro("@Usp_Data", DbType.DateTime, data);
            bd.ExecuteNonQuery();
        }

        /// <summary>
        /// Para uma cadeia retorna os par�metros definidos para a sua fun��o armazenada de hierarquia
        /// O objetivo desse m�todo � facilitar a cria��o do DataTable para o par�metro parametrosFuncaoHierarquia
        /// do m�todo Incluir
        /// </summary>
        /// <returns>Superiores hier�rquicos do funcion�rio</returns>
        public static CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable
            ObterParametrosFuncaoHierarquia(int IdCadeia)
        {
            string sql = @"SELECT Hpr_Nome AS nome, tpa.Tpa_Descri AS tipo, Hpr_Tamanho AS tamanho, null AS valor
                             FROM CadeiaAutorizacao.HierarquiaParametro hpr
                             INNER JOIN CadeiaAutorizacao.Hierarquia hie on hie.Hie_Id = hpr.Hie_Id
                             INNER JOIN CadeiaAutorizacao.Cadeia cau on cau.Hie_Id = hie.Hie_Id
							 INNER JOIN Manutencao.TipoADO tpa on tpa.Tpa_Id = hpr.Tpa_Id
                             WHERE cau.Cau_Id = @IdCadeia";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdCadeia", DbType.Int32, IdCadeia);
            return bd.ObterDataTable<CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable>();
        }

        /// <summary>
        /// Retorna os dados da inst�ncia da cadeia.
        /// </summary>
        /// <param name="IdInstancia">Id da inst�ncia</param>
        /// <returns>DataTable com a linha da inst�ncia</returns>
        public static DataTable ObterInstancia(int IdInstancia)
        {
            string sql = @"SELECT cau.Cau_Descri,
                                  cai.Cai_Id, cai.Cai_Descri, cai.Cai_Data, 
                                  cai.Cai_Status,
                                  CASE cai.Cai_Status
                                    WHEN 0 THEN 'Aberta'
                                    WHEN 1 THEN 'Aprovada'
                                    WHEN 2 THEN 'Reprovada'
                                  END AS InstanciaStatus,
                                  cai.Cai_Valor, cai.Cai_Conteudo,
                                  usu.usu_Nome
                             FROM CadeiaAutorizacao.Cadeia cau
                             INNER JOIN CadeiaAutorizacao.CadeiaInstancia cai on cau.Cau_Id = cai.Cau_Id
                             INNER JOIN Usuario usu on usu.Usu_Id = cai.Usu_IdProprietario
                             WHERE cai.Cai_Id = @IdInstancia";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdInstancia", DbType.Int32, IdInstancia);
            return bd.ObterDataTable();
        }

        public static int ObterQuantidadeParticipacoesAtivas(int usu_ID)
        {
            string sql = @"SELECT count(*)
                              FROM CadeiaAutorizacao.UsuarioParticipacao usp
                              JOIN CadeiaAutorizacao.CadeiaInstancia cai on cai.Cai_Id = usp.Cai_Id
                              WHERE usp.Usp_Autorizacao = 0 AND
                                    cai.cai_status = 0 AND    
                                    usp.Usu_Id = @IdUsuario";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdUsuario", DbType.Int32, usu_ID);
            return bd.ExecuteScalar<int>();
        }


        /// <summary>
        /// Retorna todas as participa��es pendentes do usu�rio
        /// </summary>
        /// <param name="IdUsuario">Id do usu�rio</param>
        /// <returns>DataTable com as todas as participa��es pendentes do usu�rio.</returns>
        public static DataTable ObterParticipacoesUsuario(int IdUsuario)
        {
            string sql = @"SELECT 
                                   -- Cadeia
                                   cau.Cau_Descri, cau.Cau_UrlAprovacao,
                                   -- CadeiaInstancia
                                   cai.Cai_Id, cai.Cai_Descri, cai.Cai_Data, cai.Cai_Valor, cai.Cai_Conteudo,
                                   CASE cai.Cai_Status
                                     WHEN 0 THEN 'Aberta'
	                                 WHEN 1 THEN 'Aprovada'
	                                 WHEN 2 THEN 'Reprovada'
	                               END AS InstanciaStatus,
                                   -- UsuarioParticipacao
                                   usp.Usp_Id,
                                   Usp_Tipo,
                                   CASE usp.Usp_Tipo
                                     WHEN 1 THEN 'Responsavel'
	                                 WHEN 2 THEN 'Colaborador'
	                                 WHEN 3 THEN 'Leitor'
	                               END AS ParticipacaoTipo,
                                   usp.Usp_Mensagem
                              FROM CadeiaAutorizacao.UsuarioParticipacao usp
                              INNER JOIN CadeiaAutorizacao.CadeiaInstancia cai on cai.Cai_Id = usp.Cai_Id
                              INNER JOIN CadeiaAutorizacao.Cadeia cau on cau.Cau_Id = cai.Cau_Id
                              WHERE usp.Usp_Autorizacao = 0 AND
                                    cai.cai_status = 0 AND    
                                    usp.Usu_Id = @IdUsuario
                                order by Cai_Data";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdUsuario", DbType.Int32, IdUsuario);
            return bd.ObterDataTable();
        }

        /// <summary>
        /// Atualiza a participac�o de um usu�rio
        /// </summary>
        /// <param name="IdParticipacao">Id da Participa��o</param>
        /// <param name="participacao">participa��o do usu�rio (Autorizado ou N�o Autorizado)</param>
        public static void AtualizarParticipacaoUsuario(int IdParticipacao, Autorizacao autorizacao)
        {
            AtualizarParticipacaoUsuario(IdParticipacao, autorizacao, "");
        }
        /// <summary>
        /// Atualiza a participac�o de um usu�rio
        /// </summary>
        /// <param name="IdParticipacao">Id da Participa��o</param>
        /// <param name="participacao">participa��o do usu�rio (Autorizado ou N�o Autorizado)</param>
        public static void AtualizarParticipacaoUsuario(int IdParticipacao, Autorizacao autorizacao, string Mensagem)
        {
            using (Escopo escopo = new Escopo(OpcaoTransacao.Requerido))            
            {
                string sql = @"UPDATE CadeiaAutorizacao.UsuarioParticipacao
                             SET Usp_Autorizacao = @Autorizacao,
                                 Usp_Mensagem = @Mensagem
                             WHERE Usp_Id = @IdParticipacao";
                BdUtil bd = new BdUtil(sql);
                bd.AdicionarParametro("@IdParticipacao", DbType.Int32, IdParticipacao);
                bd.AdicionarParametro("@Autorizacao", DbType.Int16, autorizacao);
                bd.AdicionarParametro("@Mensagem", DbType.String, Mensagem);
                bd.ExecuteNonQuery();
                // Verificando se a participacao do usu�rio finalizou a inst�ncia da cadeia
                // Aprovando ou N�o aprovando
                int IdInstacia = ObterIDInstaciaPelaParticipacao(IdParticipacao);
                int respAutorizacao = VerificarAutorizacao(IdInstacia, IdParticipacao);
                AtualizarStatusInstancia(IdInstacia, respAutorizacao);
                if (respAutorizacao == 1 || respAutorizacao == 2)
                {
                    //Executando o m�todo do sistema indicando o resultado da autoriza��o
                    ExecutarMetodoSistema(IdParticipacao, autorizacao, Mensagem);
                }
                escopo.Terminar();
            }
        }

        public static int ObterIDInstaciaPelaParticipacao(int IdParticipacao)
        {
         //Obtendo o Id da inst�ncia    
            string sql = @"
    SELECT Cai_Id
       FROM CadeiaAutorizacao.UsuarioParticipacao
       WHERE Usp_Id = @IdParticipacao";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdParticipacao", DbType.Int32, IdParticipacao);
            return bd.ExecuteScalar<int>();
        }

        public static void AtualizarStatusInstancia(int IdInstancia, int status)
        {
            string sql = @"UPDATE CadeiaAutorizacao.CadeiaInstancia 
                             SET Cai_Status = @Status 
                             WHERE Cai_Id = @IdInstancia";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdInstancia", DbType.Int32, IdInstancia);
            bd.AdicionarParametro("@Status", DbType.Int32, status);
            bd.ExecuteNonQuery();    
        }

        /// <summary>
        /// Altera a situa��o de uma cadeia de autoriza��o.
        /// </summary>
        /// <param name="IdInstancia">Id da Inst�ncia</param>
        /// <param name="IdUsuario">Id do usu�rio.</param>
        /// <param name="Sitaucao">Nova situa��o.</param>
        /// <param name="Mensagem">Mensagem ou observa��o do usu�rio.</param>
        /// <returns>o "Usp_ID"</returns>
        public static int AtualizarParticipacaoUsuario(int IdInstancia, int IdUsuario, Autorizacao Autorizacao, string Mensagem)
        {
            string sql = @"
SELECT Usp_Id 
FROM CadeiaAutorizacao.UsuarioParticipacao
WHERE Usu_Id = @Usu_Id
AND   Cai_Id = @Cai_Id
";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@Usu_Id", DbType.Int32, IdUsuario);
            bd.AdicionarParametro("@Cai_Id", DbType.Int32, IdInstancia);
            int uspId = bd.ExecuteScalar<int>();            
            AtualizarParticipacaoUsuario(uspId, Autorizacao, Mensagem);
            return uspId;
        }


        /// <summary>
        /// Verifica as autoriza��es para a inst�ncia de uma cadeia a partir de uma participa��o
        /// </summary>
        /// <param name="IdParticipacao">Id da participa��o</param>
        /// <returns>Status da inst�ncia da cadeia de acordo com a participa��o fornecida</returns>
        private static int VerificarAutorizacao(int IdInstacia, int IdParticipacao)
        {
            //BdUtil bd = new BdUtil("select CadeiaAutorizacao.UF_VerificarAutorizacao(@IdInstancia, @IdParticipacao)");
            BdUtil bd = new BdUtil("select CadeiaAutorizacao.UF_VerificarAutorizacao(@IdParticipacao)");
            //bd.AdicionarParametro("@IdInstancia", DbType.Int32, IdInstacia);
            bd.AdicionarParametro("@IdParticipacao", DbType.Int32, IdParticipacao);
            return bd.ExecuteScalar<int>();
        }

        /// <summary>
        /// Executa o m�todo do sistema fornecendo o Id da participa��o
        /// </summary>
        /// <param name="IdParticipacao">Id da participa��o</param>
        private static void ExecutarMetodoSistema(int IdParticipacao, Autorizacao Autorizacao, string Mensagem)
        {
            string sql = @"SELECT cau.Cau_ClasseRetorno, cau.Cau_MetodoRetorno, cai.Cai_Id, usp.Usu_ID
                             FROM CadeiaAutorizacao.UsuarioParticipacao usp
                             INNER JOIN CadeiaAutorizacao.CadeiaInstancia cai on cai.Cai_Id = usp.Cai_Id
                             INNER JOIN CadeiaAutorizacao.Cadeia cau on cau.Cau_Id = cai.Cau_Id
                             WHERE usp.Usp_Id = @IdParticipacao";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdParticipacao", DbType.Int32, IdParticipacao);
            DataTable dt = bd.ObterDataTable();
            if (dt.Rows.Count == 0)
            {
                throw new SecvException("Erro ao obter o m�todo do sistema e id da inst�ncia");
            }
            string nomeClasse = dt.Rows[0]["Cau_ClasseRetorno"].ToString();
            string nomeMetodo = dt.Rows[0]["Cau_MetodoRetorno"].ToString();
            int IdInstancia = Convert.ToInt32(dt.Rows[0]["Cai_Id"]);
            Type tipo = Type.GetType(nomeClasse);
            if (tipo == null)
            {
                throw new SecvException("Classe n�o encontrada.");
            }
            MethodInfo metodo = tipo.GetMethod(nomeMetodo);
            if (metodo == null)
            {
                throw new SecvException("M�todo n�o encontrado.");
            }

            int i = 0;
            List<object> parametros = new List<object>();
            parametros.Add(IdInstancia);
            parametros.Add(Autorizacao);            
            foreach (ParameterInfo parametro in metodo.GetParameters())
            {
                switch (i)
                {
                    case 0:
                        {
                            if (parametro.ParameterType != typeof(int))
                            {
                                throw new SecvException("A ordem dos par�metros tem que ser System.Int32, Autorizacao.");
                            }
                            i++;
                        }
                        break;
                    case 1:
                        {
                            if (parametro.ParameterType != typeof(Autorizacao))
                            {
                                throw new SecvException("A ordem dos par�metros tem que ser System.Int32, Autorizacao.");
                            }
                            i++;
                        }
                        break;
                    case 2:
                        {
                            if (parametro.ParameterType == typeof(String))
                            {
                                parametros.Add(Mensagem);
                            }
                            else if (parametro.ParameterType == typeof(int))
                            {
                                
                                parametros.Add(Convert.ToInt32(dt.Rows[0]["Usu_ID"]));
                            }
                            else
                            {
                                throw new SecvException("A ordem dos par�metros tem que ser [System.Int32 cai_ID, Autorizacao autorizacao, System.String mensagem] ou [System.Int32 cai_ID, Autorizacao autorizacao, System.Int32 UsuarioAprovador].");
                            }
                            
                            i++;
                        }
                        break;
                }
            }
            if ( i < 2 && i > 3)
                throw new SecvException("Quantidade de parametros inv�lida");
            metodo.Invoke(null, parametros.ToArray());
        }

        /// <summary>
        /// Obt�m as participa��es dos usu�rio para uma determinada inst�ncia de cadeia
        /// </summary>
        /// <param name="IdInstancia">Id da inst�ncia</param>
        /// <returns></returns>
        public static DataTable ObterParticipacoesCadeia(int IdInstancia)
        {
            string sql = @"SELECT usp.Usp_Id, 
                               CASE usp.Usp_Autorizacao
                                 WHEN 1 THEN 'Autorizado'
                                 WHEN 2 THEN 'N�o Autorizado'
                               END AS AutorizacaoTipo,
                               Case usp.Usp_Tipo
                                When 1 then 'Responsavel'
                                When 2 then 'Colaborador'
                                When 3 then 'Leitor'
                               end as Tipo,
                               usp.Usp_Mensagem,
                               usp.usp_Data,
                               u.Usu_Nome
                          FROM CadeiaAutorizacao.UsuarioParticipacao usp
                          INNER JOIN Usuario u on u.Usu_Id = usp.Usu_Id
                          WHERE usp.Cai_Id = @IdCadeia";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdCadeia", DbType.Int32, IdInstancia);
            return bd.ObterDataTable();
        }

        /// <summary>
        /// Adiciona um substituto para um usu�rio, copiando as participa��es para
        /// o substituto
        /// </summary>
        /// <param name="IdUsuario">Id do usu�rio</param>
        /// <param name="IdSubstituto">Id do substituto</param>
        public static void AdicionarSubstituto(int IdUsuario, int IdSubstituto)
        {
            using (Escopo escopo = new Escopo(OpcaoTransacao.Requerido))
            {
                // Inserir na tabela susbstitui��o
                InserirSubstituicao(IdUsuario, IdSubstituto);
                // Copiar as participa��es do usu�rio para o substituto
                CopiarParticipacoes(IdUsuario, IdSubstituto);
                escopo.Terminar();
            }
        }

        private static void InserirSubstituicao(int IdUsuario, int IdSubstituto)
        {
            string sql = @"INSERT INTO CadeiaAutorizacao.Substituicao (Sub_IdUsuario, Sub_IdSubstituto)
                                 VALUES (@IdUsuario, @IdSubstituto)";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdUsuario", DbType.Int32, IdUsuario);
            bd.AdicionarParametro("@IdSubstituto", DbType.Int32, IdSubstituto);
            bd.ExecuteNonQuery();
        }

        /// <summary>
        /// Remove o substituto do usu�rio
        /// </summary>
        /// <param name="IdUsuario">Id do usu�rio</param>
        public static void RemoverSubstituto(int IdUsuario)
        {
            string sql = @"DELETE FROM CadeiaAutorizacao.Substituicao
                            WHERE Sub_IdUsuario = @IdUsuario";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdUsuario", DbType.Int32, IdUsuario);
            bd.ExecuteNonQuery();
        }

        /// <summary>
        /// Retorna o substituto de um usu�rio
        /// </summary>
        /// <param name="IdUsario">Id do usu�rio</param>
        /// <returns>DataTable com o substituto do usu�rio.</returns>
        public static DataTable ObterSubstituto(int IdUsario)
        {
            string sql = @"SELECT usu.Usu_Id, usu.Usu_Nome
                             FROM CadeiaAutorizacao.Substituicao sub
                             INNER JOIN Usuario usu on usu.Usu_Id = sub.Sub_IdSubstituto
                             WHERE sub.Sub_IdUsuario = @IdUsuario";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdUsuario", DbType.Int32, IdUsario);
            return bd.ObterDataTable();
        }

        private static void CopiarParticipacoes(int IdUsuario, int IdSubstituto)
        {
            DataTable participacoes = ObterParticipacoesUsuario(IdUsuario);
            foreach (DataRow participacao in participacoes.Rows)
            {
                IncluirParticipante(Convert.ToInt32(participacao["Cai_Id"]), IdSubstituto,
                                    DateTime.Now, ((TipoParticipacao)((int)participacao["Usp_Tipo"])));
            }
        }

        /// <summary>
        /// Retorna o substituto de um usu�rio
        /// </summary>
        /// <param name="IdUsario">Id do usu�rio</param>
        /// <param name="NmUsuario">Valor para ser usado como filtro</param>
        /// <returns>DataTable com os usu�rios.</returns>
        public static DataTable ObterUsuarios(int IdUsario, string NmUsuario)
        {
            string sql = @"
SELECT Usu_Id, Usu_Nome
FROM  Usuario
where usu_id <> @IdUsuario 
  and usu_nome like @NmUsuario 
order by usu_nome";
            BdUtil bd = new BdUtil(sql);
            bd.AdicionarParametro("@IdUsuario", DbType.Int32, IdUsario);
            bd.AdicionarParametro("@NmUsuario", DbType.String, NmUsuario);
            return bd.ObterDataTable();
        }
    }
}


/*
USE [SISSECVTESTE]
-- Fun��o para retornar o status de autoriza��o de uma
-- Inst�ncia. Avaliando se algum respons�vel j� autorizou ou
-- n�o autorizou.
-- Basta o voto de um respons�vel para autorizar a inst�ncia
-- Sendo que o voto de n�o autoriza��o sobrepoe o de autoriza��o
-- Autor: Jos� Maria Rodrigues Santos J�nior
-- Data: 10/11/2006
CREATE FUNCTION [dbo].[UF_VeriricarAutorizacao]
(
  @IdInstancia INT
)
RETURNS INT
AS
BEGIN

	-- Obtendo a quantidade de autoriza��es positivas dos respons�veis
	DECLARE @AUTORIZACOES_POSITIVAS_RESPONSAVEL INT 
	SELECT @AUTORIZACOES_POSITIVAS_RESPONSAVEL = COUNT(*)
	   FROM CadeiaAutorizacao.UsuarioParticipacao
	   WHERE Cai_id = @IdInstancia and
			 Usp_Tipo = 2 and
			 Usp_Autorizacao = 1 -- 1 = Autorizado

	-- Obtendo a quantidade de autoriza��es negativas dos respons�veis
	DECLARE @AUTORIZACOES_NEGATIVAS_RESPONSAVEL INT 
	SELECT @AUTORIZACOES_NEGATIVAS_RESPONSAVEL = COUNT(*)
	   FROM CadeiaAutorizacao.UsuarioParticipacao
	   WHERE Cai_id = @IdInstancia and
			 Usp_Tipo = 2 and
			 Usp_Autorizacao = 2 -- 2 = N�o autorizado

	-- Se tiver pelo menos 1 voto de respons�vel negativo
	IF (@AUTORIZACOES_NEGATIVAS_RESPONSAVEL >= 1) 
      BEGIN
		RETURN 2 -- Cadeia N�o aprovada
	  END
	-- Se tiver pelo menos 1 voto de respons�vel positivo
	IF (@AUTORIZACOES_POSITIVAS_RESPONSAVEL >= 1) 
      BEGIN
		RETURN 1 -- Cadeia Aprovada
	  END

    RETURN 0 -- Cadeia aberta. Nenhum respons�vel votou ainda
END
 */
