using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Dado
{
    /// <summary>
    /// Indica qual é a propriedade que o <see cref="BdUtil"/> vai configurar do <see cref="BdUtil.DataAdapter"/>
    /// quando configurando parametros e texto do command.
    /// </summary>
    /// <remarks>
    /// Com essa opção pode-se configurar 
    /// </remarks>
    public enum TipoCommandDataAdapter
    {
        /// <summary>
        /// Operação SELECT. Esse é o padrão
        /// </summary>
        Select,
        /// <summary>
        /// Operação INSERT
        /// </summary>
        Insert,
        /// <summary>
        /// Operação UPDATE
        /// </summary>
        Update,
        /// <summary>
        /// Operação DELETE
        /// </summary>
        Delete

    }
}
