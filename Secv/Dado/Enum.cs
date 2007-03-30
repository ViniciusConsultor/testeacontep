using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Dado
{
    /// <summary>
    /// Indica qual � a propriedade que o <see cref="BdUtil"/> vai configurar do <see cref="BdUtil.DataAdapter"/>
    /// quando configurando parametros e texto do command.
    /// </summary>
    /// <remarks>
    /// Com essa op��o pode-se configurar 
    /// </remarks>
    public enum TipoCommandDataAdapter
    {
        /// <summary>
        /// Opera��o SELECT. Esse � o padr�o
        /// </summary>
        Select,
        /// <summary>
        /// Opera��o INSERT
        /// </summary>
        Insert,
        /// <summary>
        /// Opera��o UPDATE
        /// </summary>
        Update,
        /// <summary>
        /// Opera��o DELETE
        /// </summary>
        Delete

    }
}
