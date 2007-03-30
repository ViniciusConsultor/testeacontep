using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Dado;

namespace Acontep.Manutencao.Seguranca {

  public static class LogUsuarios {

    public static void Inserir (System.Byte Plt_Planta,
                                System.Int16 Sis_CodSis,
                                System.Int16? Mod_CodMod,
                                System.Byte? Fun_CodFun, 
                                System.String Usu_Login,
                                System.DateTime Log_DatLog, 
                                System.String Log_Tipo) {

      string sql = @"INSERT INTO LOGUSUARIOS (Fun_CodFun,  Log_DatLog,  Log_Tipo,  Mod_CodMod,  Plt_Planta,  Sis_CodSis,  Usu_Login)
                                      VALUES (@Fun_CodFun, @Log_DatLog, @Log_Tipo, @Mod_CodMod, @Plt_Planta, @Sis_CodSis, @Usu_Login)";
      BdUtil bd = new BdUtil(sql);
      bd.AdicionarParametro("@Fun_CodFun", System.Data.DbType.Double, Fun_CodFun);
      bd.AdicionarParametro("@Log_DatLog", System.Data.DbType.DateTime, Log_DatLog);
      bd.AdicionarParametro("@Log_Tipo", System.Data.DbType.String, Log_Tipo);
      bd.AdicionarParametro("@Mod_CodMod", System.Data.DbType.Int16, Mod_CodMod);
      bd.AdicionarParametro("@Plt_Planta", System.Data.DbType.Double, Plt_Planta);
      bd.AdicionarParametro("@Sis_CodSis", System.Data.DbType.Int16, Sis_CodSis);
      bd.AdicionarParametro("@Usu_Login", System.Data.DbType.String, 15, Usu_Login);
      bd.ExecuteNonQuery();
    }

  }
}
