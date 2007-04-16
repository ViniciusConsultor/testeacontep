using System;
using System.Collections.Generic;
using System.Text;
using Acontep;
using System.Data;
using Acontep.Dado;

namespace Acontep.AD.Manutencao.Seguranca
{
    public class FuncaoAcessoDados: AcessoDados
    {
        static public bool ObterFuncao(int Sis_CodSis, int Mod_CodMod, int Fun_CodFun)
        {
            string sql = @"Select Fun_LogAcesso from FUNCAO
where Fun_CodFun = @Fun_CodFun and Mod_CodMod = @Mod_CodMod and Sis_CodSis = @Sis_CodSis";
            BdUtil bdUtil = new BdUtil(sql);
            bdUtil.AdicionarParametro("@Sis_CodSis", DbType.Int32, Sis_CodSis);
            bdUtil.AdicionarParametro("@Mod_CodMod", DbType.Int32, Mod_CodMod);
            bdUtil.AdicionarParametro("@Fun_CodFun", DbType.Int32, Fun_CodFun);
            return bdUtil.ExecuteScalar<bool>();            
        }
    }
}
