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
            // Adicionando uma nova instância de cadeia de autorização com hierarquia definida em tabelas
            //
            //CadeiaAutorizacaoNG.Incluir(1, 112, 500.00f, "descrição da instância", "conteúdo da instância\n linha2\nlinha3\n", null);

            //
            // Atualizando a autorização de um usuário
            //
            CadeiaAutorizacaoNG.AtualizarParticipacaoUsuario(18, Autorizacao.NaoAutorizado);

            //
            // obtendo as autorizações pendentes de um usuário de um usuário
            //
            //DataTable partic = CadeiaAutorizacaoNG.ObterParticipacoesUsuario(111);
            //Console.WriteLine(partic.Rows.Count);

            //
            // Adicionando uma instância de cadeia utilizando uma função de hierarquia
            //
            // Obtendo/setando os parâmetros definidos para a função de hierarquia
            //CadeiaAutorizacaoDataSet.ParemetroFuncaoHierarquiaDataTable parametros =
            //  CadeiaAutorizacaoNG.ObterParametrosFuncaoHierarquia(1);
            //parametros.FindBynome("@IdUsuario").valor = 112;
            //parametros.FindBynome("@IdCadeia").valor = 1;
            //Console.WriteLine(parametros.Rows.Count);
            // Obtendo os superiores de um particpante
            //CadeiaAutorizacaoNG.Incluir(1, 112, 500.00f, "descrição da instância", "conteúdo da instância\n linha2\nlinha3\n", parametros);


            //DataTable superiores = CadeiaAutorizacaoNG.ObterSuperioresPorFuncao(1, 112, parametros);
            //Console.WriteLine(superiores.Rows.Count);
        }
    }
}
