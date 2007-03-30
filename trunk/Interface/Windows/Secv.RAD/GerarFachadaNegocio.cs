using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Secv.RAD.Properties;
using System.Data;
using Acontep.Ng.Manutencao.Rad;

namespace Acontep.RAD
{
    public class GerarFachadaNegocio : GeradorSECV
    {
        public GerarFachadaNegocio() : base()
        {
        }
        protected override string GerarComandoInsert(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (!ListaOpcoesContemOpcao(mrw, DataObjectMethodType.Insert))
            {
                return "";
            }
            this.Tabela = mrw.ClasseCRUDRow.Tabela;
            StringBuilder classe = new StringBuilder();
            classe.AppendLine("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Insert)]");
            classe.AppendFormat("\tpublic static int {0} ({1})",
                string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoInsert : mrw.NomeMetodo,
                MontarDeclaracaoParametro(RetornarColunas(mrw.GetListaCamposRows()), false, true, true, true)
            );
            classe.AppendLine("{\n");
            
            //TODO: adicionar validação de insert.
            // Adicionando a chamada para o método que irá validar os campos            

            classe.AppendFormat("return {0}.{1}.{2}({3});",
                mrw.ClasseCRUDRow.NameSpace,
                mrw.ClasseCRUDRow.NomeClasse,
                string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoInsert : mrw.NomeMetodo,
                MontarDeclaracaoParametro(RetornarColunas(mrw.GetListaCamposRows()), false, false, true, true)
            );

            classe.AppendLine("\n}");
            
            return classe.ToString();
        }
        
        protected override string GerarComandoUpdate(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (!ListaOpcoesContemOpcao(mrw, DataObjectMethodType.Update))
            {
                return "";
            }
            this.Tabela = mrw.ClasseCRUDRow.Tabela;
            string nomeMetodo = string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoUpdate : mrw.NomeMetodo;
            StringBuilder classe = new StringBuilder();
            classe.AppendLine("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]");
            classe.AppendFormat("public static void {0} ({1})", 
                nomeMetodo, 
                this.MontarDeclaracaoParametro(RetornarColunas(mrw.GetListaCamposRows()), true, true, true, true, true));
            classe.Append("{\n");

            // Adicionando a chamada para o método que irá validar os campos
            //AdicionarChamadaValidarCampos(classe);

            classe.AppendFormat("{0}.{1}.{2}({3});",
                mrw.ClasseCRUDRow.NameSpace,
                mrw.ClasseCRUDRow.NomeClasse,
                nomeMetodo,
                this.MontarDeclaracaoParametro(RetornarColunas(mrw.GetListaCamposRows()), true, false, true, true, true)
            );
            classe.Append("\n    }\n"); // Fechamento do método
            return classe.ToString();
        }

        protected override string GerarComandoDelete(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (!ListaOpcoesContemOpcao(mrw, DataObjectMethodType.Delete))
            {
                return "";
            }
            this.Tabela = mrw.ClasseCRUDRow.Tabela;
            string nomeMetodo = string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoDelete : mrw.NomeMetodo;
         
            StringBuilder classe = new StringBuilder();
            classe.AppendFormat("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]");
            classe.AppendFormat("\n    public static void {0} ({1})", 
                nomeMetodo,
                this.MontarDeclaracaoParametro(ColunasMetaData.PrimaryKey, true, true, false, false, true)
            );
            classe.Append("\n\t{\n");
            // Adicionando o comando SQL DELETE
            classe.AppendFormat("{0}.{1}.{2}({3});",
                mrw.ClasseCRUDRow.NameSpace,
                mrw.ClasseCRUDRow.NomeClasse,
                nomeMetodo,
                this.MontarDeclaracaoParametro(ColunasMetaData.PrimaryKey, true, false, true, true, true)
            );
            classe.Append("    }\n"); // Fechamento do método
            return classe.ToString();
        }

        protected override string GerarComandoSelect(string NomeMetodo, DataColumn[] CamposProjecao, DataColumn[] Filtros, EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            
            string nomeMetodo = string.IsNullOrEmpty(NomeMetodo) ? Settings.Default.NomeDefaultMetodoSelect : NomeMetodo;
            StringBuilder classe = new StringBuilder();
            classe.AppendLine("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]");
            classe.AppendFormat("\tpublic static DataTable {0} ({1})",
                nomeMetodo,
                MontarDeclaracaoParametro(Filtros, true)
            );
            classe.AppendLine("{");
            classe.AppendFormat("return {0}.{1}.{2}({3});",
                ccr.NameSpace,
                ccr.NomeClasse,
                nomeMetodo,
                MontarDeclaracaoParametro(Filtros, true, false)                
            );
            classe.Append("\n    }\n"); // Fechamento do método
            return classe.ToString();
        }

        protected override string RetornarNomeClasse(EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return (ccr.IsNomeClasseNull() || (ccr.NomeClasse.IndexOf("AD") == ccr.NomeClasse.Length - 2) ? ccr.Tabela + "Fn" : ccr.NomeClasse);
        }
    }

    
}
