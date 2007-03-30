using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep
{
    /// <summary>
    /// Opcoes para modificar como <see cref="Escopo.Atual"/> é afetado quando construindo um novo escopo
    /// </summary>
    public enum OpcaoEscopo
    {
        /// <summary>
        /// Se nao existir nenhum escopo entao cria um novo, caso contrario nao faz nada. Essa é a opção padrão
        /// </summary>
        Requerido,
        /// <summary>
        /// Informa que esse é o escopo atual (armazena escopos anteriores e restaura eles no dispose)
        /// </summary>
        /// <remarks>
        /// Use essa opção se deseja usar a mesma conexao mas em uma transação diferente, ou mesmo sem transacao.
        /// </remarks>
        RequerNovo
    }

    /// <summary>
    /// Opcoes para modificar como <see cref="Escopo.Atual"/> lida com a criacao de transacoes
    /// </summary>
    [Flags]
    public enum OpcaoTransacao
    {
        /// <summary>
        /// Usa a transacao corrente se existir no escopo, caso contrario nao faz nada. Essa é a opção padrão
        /// </summary>
        Automatico = 1,
        /// <summary>
        /// Usa a transacao corrente se existir no escopo, caso contrario cria um novo escopo com uma transacao aberta. 
        /// </summary>
        /// <remarks>Se uma for solicitado a criacao de uma transacao dentro de um escopo ja existente, e não
        /// for informado <see cref="OpcaoEscopo.Requerido"/> explicitamente entao um novo escopo sera iniciado de forma transparente, 
        /// fazendo com que ao fim do escopo que iniciou uma transação o commit ocorra.</remarks>
        Requerido = 2
        ///// <summary>
        ///// Cria uma nova transacao. Deve ser usado junto com <see cref="OpcaoEscopo.RequerNovo"/> se nao
        ///// usar opção de <see cref="OpcaoTransacao.TransactionScope" />
        ///// </summary>
        //RequerNovo = 4,
        ///// <summary>
        ///// Ignora transacao existente. Deve ser usado junto com <see cref="OpcaoEscopo.RequerNovo"/> se nao
        ///// usar opção de <see cref="OpcaoTransacao.TransactionScope" />
        ///// </summary>
        //Suprimir = 8,
        ///// <summary>
        ///// Indica se <see cref="TransactionScope"/> deve ser usado no lugar de <see cref="DbTransaction"/> nesse escopo
        ///// </summary>
        //TransactionScope = 16
    }

    /// <summary>
    /// Tipo de intervalo a ser verificado na conversao
    /// </summary>
    [Flags]
    public enum Intervalo
    {
        /// <summary>
        /// Validar valor igual
        /// </summary>
        Igual = 1,
        /// <summary>
        /// Validar apenas valor menor
        /// </summary>
        Menor = 2,
        /// <summary>
        /// Validar apenas valor maior
        /// </summary>
        Maior = 4,
        /// <summary>
        /// Validar valor menor ou igual
        /// </summary>
        MenorIgual = Menor | Igual,
        /// <summary>
        /// Validar valor maior ou igual
        /// </summary>
        MaiorIgual = Maior | Igual,
        /// <summary>
        /// Validar valor contido em um intervalo mas nao igual
        /// </summary>
        ContidoExclusivo = Menor | Maior,
        /// <summary>
        /// Validar valor contido em um intervalo e também igual
        /// </summary>
        ContidoInclusivo = MenorIgual | MaiorIgual
    }
}
