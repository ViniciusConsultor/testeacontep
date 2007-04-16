using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Secv.Ng.Manutencao.CadeiaAutorizacao;
            //DataTable partic = CadeiaAutorizacaoNG.ObterParticipacoesUsuario(111);

namespace CadeiaAutorizacaoTeste
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //
            // Adicionando uma nova inst�ncia de cadeia de autoriza��o com hierarquia definida em tabelas
            //
            //CadeiaAutorizacaoNG.Incluir(1, 112, 500.00f, "descri��o da inst�ncia", "conte�do da inst�ncia\n linha2\nlinha3\n", null);

            //
            // Atualizando a autoriza��o de um usu�rio
            //
            CadeiaAutorizacaoNG.AtualizarParticipacaoUsuario(18, Autorizacao.NaoAutorizado);

            //
            // obtendo as autoriza��es pendentes de um usu�rio de um usu�rio
            //
            //DataTable partic = CadeiaAutorizacaoNG.ObterParticipacoesUsuario(111);
            //Console.WriteLine(partic.Rows.Count);

            //
            // Adicionando uma inst�ncia de cadeia utilizando uma fun��o de hierarquia
            //
            // Obtendo/setando os par�metros definidos para a fun��o de hierarquia
            //CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable parametros =
            //  CadeiaAutorizacaoNG.ObterParametrosFuncaoHierarquia(1);
            //parametros.FindBynome("@IdUsuario").valor = 112;
            //parametros.FindBynome("@IdCadeia").valor = 1;
            //Console.WriteLine(parametros.Rows.Count);
            // Obtendo os superiores de um particpante
            //CadeiaAutorizacaoNG.Incluir(1, 112, 500.00f, "descri��o da inst�ncia", "conte�do da inst�ncia\n linha2\nlinha3\n", parametros);


            //DataTable superiores = CadeiaAutorizacaoNG.ObterSuperioresPorFuncao(1, 112, parametros);
            //Console.WriteLine(superiores.Rows.Count);
        }
    }
}
