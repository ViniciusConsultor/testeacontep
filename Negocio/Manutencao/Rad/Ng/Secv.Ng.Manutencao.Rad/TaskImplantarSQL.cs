using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
namespace Acontep.Ng.Manutencao.Rad
{
    public class ImplantarSQLTask : Task
    {
        string _FilePath;

        [Required]
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }       
        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.Low, "Carregando aplicação.");
            ExecutarListaSQL executarSQL = new ExecutarListaSQL();
            executarSQL.FilePath = FilePath;
            Log.LogMessage(MessageImportance.Low, "Início da implantação dos sqls.");
            bool resultado = executarSQL.Work();
            if ( resultado )
                BuildEngine.LogMessageEvent( new BuildMessageEventArgs("Script Implantado", "SQL", "ImplantarSQLTask", MessageImportance.Normal));
            else
            {
                BuildEngine.LogErrorEvent(new BuildErrorEventArgs("IMPLANTAR SQL", executarSQL.LastError != null ? executarSQL.LastError.ToString() : "", executarSQL.FilePath, 0, 0, 0, 0, "Erro ao implantar o sql", "SQL", "ImplantarSQLTask"));
            }
            Log.LogMessage(MessageImportance.Low, "Fim da implantação dos sqls.");

            
            return resultado;
        }
    }
}
