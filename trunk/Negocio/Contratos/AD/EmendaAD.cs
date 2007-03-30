using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Contratos
{
    public static partial class EmendaAD
    {
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterEmenda(int? Numero,
            int? NumeroFuncional, int? IDArea,
            int? IDTipoRealizacao, string Autor)
        {
            BdUtil bd = new BdUtil(@"
SELECT 
    Emenda.*,
    M.Nome as NomeCidade,
    M.IDEstado as IDEstado
FROM 
    Contratos.Emenda Emenda
    LEFT JOIN Cadastros.Municipio M on M.IDCidade = Emenda.IDCidade
where 
    (@Numero is null OR Numero = @Numero ) AND
    (@NumeroFuncional is null OR NumeroFuncional = @NumeroFuncional ) AND
    (@IDArea is null OR IDArea = @IDArea ) AND
    (@IDTipoRealizacao is null OR IDTipoRealizacao = @IDTipoRealizacao ) AND
    (@Autor is null OR Autor LIKE @Autor + '%' )
ORDER BY Numero
");
            bd.AdicionarParametro("@Numero", DbType.Int32, Numero);
            bd.AdicionarParametro("@NumeroFuncional", DbType.Int32, NumeroFuncional);
            bd.AdicionarParametro("@IDArea", DbType.Int32, IDArea);
            bd.AdicionarParametro("@IDTipoRealizacao", DbType.Int32, IDTipoRealizacao);
            bd.AdicionarParametro("@Autor", DbType.AnsiString, Autor);
            return bd.ObterDataTable();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static DataTable ObterEmenda(int IDEmenda)
        {
            BdUtil bd = new BdUtil(@"
SELECT 
    Emenda.*,
    M.Nome as NomeCidade,
    M.IDEstado as IDEstado
FROM 
    Contratos.Emenda Emenda
    LEFT JOIN Cadastros.Municipio M on M.IDCidade = Emenda.IDCidade
where 
    IDEmenda = @IDEmenda
ORDER BY Numero
");
            bd.AdicionarParametro("@IDEmenda", DbType.Int32, IDEmenda);
            return bd.ObterDataTable();
        }
    }
}
