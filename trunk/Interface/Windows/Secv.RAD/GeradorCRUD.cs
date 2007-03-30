using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Secv.RAD;
using Secv.RAD.Properties;
using Acontep.Ng.Manutencao.Rad;

namespace Acontep.RAD
{

    public class GeradorCRUD : GeradorSECV
    {

        /// <summary>
        /// GeradorShema
        /// </summary>
        public GeradorCRUD()
        {
        }

        #region Geracao das chamadas dos Metodos "AdicionarParametro"

        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        private string MontarParametroDBUtil(DataColumn[] Filtros)
        {
            return MontarParametroDBUtil(Filtros, true);
        }
        /// <summary>
        /// Adiciona a declaração e atribuição dos parâmetros para a consulta.
        /// Parâmetros ADO.NET
        /// </summary>
        /// <param name="classe">Classe</param>
        /// <param name="parametros">Lista de parâmetros</param>
        private string MontarParametroDBUtil(string[] parametros)
        {
            StringBuilder classe = new StringBuilder();
            foreach (string parametro in parametros)
            {
                classe.AppendFormat("  bd.{0}(\"{1}{2}\", {2});\n",
                    Settings.Default.MetodoAdicionarParametro,
                    Settings.Default.SimboloParametro,
                    parametro
                    );
            }
            return classe.ToString();
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        private string MontarParametroDBUtil(DataColumn[] Filtros, bool AddIdentity)
        {
            return MontarParametroDBUtil(Filtros, AddIdentity, true);
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        private string MontarParametroDBUtil(DataColumn[] Filtros, bool AddIdentity, bool AddNonIdentity)
        {
            return MontarParametroDBUtil(Filtros, AddIdentity, AddNonIdentity, true, true);
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <param name="AddCamposTransacionaisAlteracao">Adiciona automaticamente os campos transacionais de alteracão.</param>
        /// <param name="AddCamposTransacionaisInclusao">Adiciona automaticamente os campos transacionais de inclusão</param>
        /// <param name="AddIdentity">Adicionar campo identity</param>
        /// <param name="AddNonIdentity">Adicionar campos não identity?</param>
        /// <returns></returns>
        private string MontarParametroDBUtil(DataColumn[] Filtros, bool AddIdentity, bool AddNonIdentity, bool AddCamposTransacionaisInclusao, bool AddCamposTransacionaisAlteracao)
        {
            return MontarParametroDBUtil(Filtros, AddIdentity, AddNonIdentity, AddCamposTransacionaisInclusao, AddCamposTransacionaisAlteracao, false);
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <param name="AddCamposTransacionaisAlteracao">Adiciona automaticamente os campos transacionais de alteracão.</param>
        /// <param name="AddCamposTransacionaisInclusao">Adiciona automaticamente os campos transacionais de inclusão</param>
        /// <param name="AddIdentity">Adicionar campo identity</param>
        /// <param name="AddNonIdentity">Adicionar campos não identity?</param>
        /// <returns></returns>
        private string MontarParametroDBUtil(DataColumn[] Filtros, bool AddIdentity, bool AddNonIdentity, bool AddCamposTransacionaisInclusao, bool AddCamposTransacionaisAlteracao, bool AddOriginal)
        {
            StringBuilder parametros = new StringBuilder();
            for (int i = 0; i < Filtros.Length; i++)
            {
                DataColumn Filtro = Filtros[i];
                DbType TipoFiltro = TypeConvertor.ToDbType(Filtro.DataType);
                string Valor = Filtro.ColumnName;
                string prefixo = String.Empty;
                if ((!AddIdentity && Filtro.AutoIncrement) || (!AddNonIdentity && !Filtro.AutoIncrement))
                    continue;
                if (AddOriginal)
                {
                    prefixo = Settings.Default.NomePrefixoCamposManutencao;
                }

                string NomeColuna = Filtro.ColumnName.Substring(Filtro.ColumnName.IndexOf('_') + 1).ToUpper();
                if (AddCamposTransacionaisAlteracao)
                {
                    switch (NomeColuna)
                    {
                        case "USUTRA":
                            Valor = Settings.Default.MetodoGetUser;
                            break;
                        case "DATHOR":
                            continue;                        
                    }
                }
                if (AddCamposTransacionaisInclusao)
                {
                    switch (NomeColuna)
                    {
                        case "USUINC":
                            Valor = Settings.Default.MetodoGetUser;
                            break;
                        case "DATINC":
                            continue;
                    }
                }
                parametros.AppendFormat("\tbd.{4}(\"{5}{6}{0}\", {1}, {2}, {3} );\n",
                    Filtro.ColumnName,
                    TipoFiltro.GetType().FullName + "." + TipoFiltro.ToString(),
                    Filtro.MaxLength,
                    prefixo + Valor,
                    Settings.Default.MetodoAdicionarParametro,
                    Settings.Default.SimboloParametro,
                    prefixo);
            }
            return parametros.ToString();
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        private string MontarParametroDBUtil(EstruturaSaveRAD.ListaCamposRow[] Campos, bool AddIdentity)
        {
            return MontarParametroDBUtil(RetornarColunas(Campos), AddIdentity);
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        private string MontarParametroDBUtil(EstruturaSaveRAD.ListaCamposRow[] Campos, bool AddIdentity, bool AddNonIdentity)
        {
            return MontarParametroDBUtil(RetornarColunas(Campos), AddIdentity, AddNonIdentity);
        }
        private string MontarParametroDBUtil(EstruturaSaveRAD.ListaCamposRow[] Campos, bool AddIdentity, bool AddNonIdentity, bool AddCamposTransacionaisAlteracao, bool AddCamposTransacionaisInclusao)
        {
            return MontarParametroDBUtil(RetornarColunas(Campos), AddIdentity, AddNonIdentity, AddCamposTransacionaisAlteracao, AddCamposTransacionaisInclusao);
        }
        /// <summary>
        /// Monta as linhas que correspondem aos comandos de adicionar parametro em dbutil.
        /// </summary>
        /// <param name="Campos"></param>
        /// <returns></returns>
        private string MontarParametroDBUtil(EstruturaSaveRAD.ListaCamposRow[] Campos)
        {
            return MontarParametroDBUtil(Campos, true);
        }
        
        #endregion Geracao das chamadas dos Metodos "AdicionarParametro"

        #region Geracao dos metodos Crud

        /// <summary>
        /// Gera a projecao da consulta sql concatenada
        /// </summary>
        /// <param name="CamposProjecao"></param>
        /// <returns></returns>
        private string RetornarProjecao(DataColumn[] CamposProjecao)
        {
            string[] resultado = new string[CamposProjecao.Length];
            for (int i = 0; i < resultado.Length; i++)
            {
                resultado[i] = "\n\t" + CamposProjecao[i].ColumnName;
            }
            return string.Join(", ", resultado);
        }

        private string GerarWhere(DataColumn[] Filtros)
        {
            List<string> resultado = new List<string>();
            for (int i = 0; i < Filtros.Length; i++)
            {
                string Formatacao = "{0} = {1}{2}{0}";
                
                resultado.Add(string.Format(Formatacao,
                    Filtros[i].ColumnName,
                    Settings.Default.SimboloParametroSQL,
                    Settings.Default.NomePrefixoCamposManutencao
                    )
                );
            }
            return string.Join(" AND ", resultado.ToArray());
        }
        /// <summary>
        /// Gera os campos do where concatenados com AND e buscando por IGUAL <code>[=]</code>
        /// </summary>
        /// <param name="Filtros">Lista de colunas</param>
        /// <param name="UsarIdentity">Adicionar campos identity</param>
        /// <returns></returns>
        private string GerarWhere(DataColumn[] Filtros, bool UsarIdentity)
        {
            return GerarWhere(Filtros, UsarIdentity, true);
        }
        /// <summary>
        /// Gera os campos do where concatenados com AND e buscando por IGUAL <code>[=]</code>
        /// </summary>
        /// <param name="Filtros">Lista de colunas</param>
        /// <param name="UsarIdentity">Adicionar campos identity</param>
        /// <param name="UsarCamposNaoIdentity">Adicionar campos não identity</param>
        /// <returns></returns>
        private string GerarWhere(DataColumn[] Filtros, bool UsarIdentity, bool UsarCamposNaoIdentity)
        {
            return GerarWhere(Filtros, UsarIdentity, UsarCamposNaoIdentity, true);
        }
        /// <summary>
        /// Gera os campos do where concatenados com AND e buscando por IGUAL <code>[=]</code> ou por <code>LIKE</code>
        /// </summary>
        /// <param name="Filtros">Lista de colunas</param>
        /// <param name="UsarIdentity">Adicionar campos identity</param>
        /// <param name="UsarCamposNaoIdentity">Adicionar campos não identity</param>
        /// <param name="UsarLike">Usar o operador <code>like</code> ou o operador IGUAL <code>=</code></param>
        /// <returns></returns>        
        private string GerarWhere(DataColumn[] Filtros, bool UsarIdentity, bool UsarCamposNaoIdentity, bool UsarLike)
        {
            List<string> resultado = new List<string>();
            for (int i = 0; i < Filtros.Length; i++)
            {
                DataColumn Filtro = Filtros[i];
                DbType TipoFiltro = TypeConvertor.ToDbType(Filtro.DataType);
                if (Filtro.AutoIncrement && !UsarIdentity)
                    continue;
                else if (!Filtro.AutoIncrement && !UsarCamposNaoIdentity)
                    continue;
                string Formatacao = "{0} = {1}{0}";
                if (UsarLike && EhTipoTexto(TipoFiltro) )
                {
                    Formatacao = "( {0} LIKE {1}{0} ";
                    if (Settings.Default.UsarSimboloCoringaParaLike)
                    {
                        Formatacao += "{2} {3} ";
                    }
                    if (Settings.Default.UsarNullEmLike && ColumnAllowNull(Filtro) )
                        Formatacao += " OR ( {1}{0} is null AND {0} is null ) ";
                    Formatacao += ")"; 
                        
                }
                resultado.Add(string.Format(Formatacao, 
                    Filtro.ColumnName,
                    Settings.Default.SimboloParametroSQL,
                    Settings.Default.SimboloJuncaoString,
                    Settings.Default.SimboloCoringaParaLike
                    )
                );
            }
            return string.Join(" AND ", resultado.ToArray());
        }

        #region Select

        
        private string GerarComandoSQLSelect(EstruturaSaveRAD.ListaCamposRow[] CamposProjecao,  DataColumn[] Filtros)
        {
            return GerarComandoSQLSelect(RetornarColunas(CamposProjecao), Filtros);
        }
        private string GerarComandoSQLSelect(DataColumn[] CamposProjecao, DataColumn[] Filtros)
        {
            StringBuilder comando = new StringBuilder();
            comando.AppendFormat("SELECT \n\t{0} \nFROM \n\t{1}\n\t ", RetornarProjecao(CamposProjecao), Tabela );
            if (Filtros != null && Filtros.Length > 0)
            {
                comando.AppendFormat(" \n WHERE \n\t{0}", GerarWhere(Filtros, true, true, true));
            }
            return comando.ToString();
        }
        protected override string GerarComandoSelect(string NomeMetodo, DataColumn[] CamposProjecao, DataColumn[] Filtros, EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            StringBuilder classe = new StringBuilder();
            classe.AppendLine("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Select)]");
            classe.AppendFormat("\tpublic static DataTable {0} ({1})",
                string.IsNullOrEmpty(NomeMetodo) ? Settings.Default.NomeDefaultMetodoSelect : NomeMetodo, 
                MontarDeclaracaoParametro(Filtros, true)
            );

            classe.AppendLine("{");
            // Adicionando o comando SQl Select
            classe.Append("string sql = @\"\n");
            classe.Append(GerarComandoSQLSelect(CamposProjecao, Filtros));
            classe.Append("\";");
            classe.AppendFormat("\n\t{0} bd = new {0}(sql);\n", Settings.Default.ClasseConsultaBanco);
            // Adicionando a configuração dos parâmetros
            classe.Append(MontarParametroDBUtil(Filtros));
            classe.AppendFormat("      return bd.{0}();\n", Settings.Default.MetodoObterDataTable);
            classe.Append("    }\n"); // Fechamento do método
            return classe.ToString();
        }
        
        
        #endregion Select

        #region Insert
        private string GerarComandoSQLInsert( EstruturaSaveRAD.MetodoRow mrw)
        {
            StringBuilder comando = new StringBuilder();
            EstruturaSaveRAD.ListaCamposRow[] campos = mrw.GetListaCamposRows();
            comando.Append("INSERT INTO " + mrw.ClasseCRUDRow.Tabela + "\n(");
            // Gerando os campos do comando insert
            DataColumn[] Colunas = RetornarColunas(campos);
            comando.Append(MontarDeclaracaoParametro(Colunas, false, false));
            comando.Append("\n)\n    VALUES \n(");
            
            // Gerando os parâmetros do insert
            foreach (DataColumn coluna in Colunas)
            {
                string Valor = String.Empty;
                if (!coluna.AutoIncrement)
                {
                    switch (coluna.ColumnName.Substring(coluna.ColumnName.IndexOf('_')+1).ToUpper() )
                    {
                        case "DATHOR":
                        case "DATINC":
                            Valor = "GetDate()";
                            break;                        
                        default:
                            Valor =  Settings.Default.SimboloParametroSQL + coluna.ColumnName;
                            break;
                    }
                    comando.AppendFormat("\n{0}, ", Valor);
                }
            }
            // Removendo a última vírgula e espaço
            comando.Remove(comando.Length - 2, 2);
            comando.Append("\n); SELECT SCOPE_IDENTITY();");
            return comando.ToString();
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
                string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoInsert: mrw.NomeMetodo,
                MontarDeclaracaoParametro(RetornarColunas(mrw.GetListaCamposRows()), false, true, true, true)
            );
            classe.AppendLine("{\n");
            // Adicionando os parâmetros método
            
            //TODO: adicionar validação de insert.
            // Adicionando a chamada para o método que irá validar os campos
            // AdicionarChamadaValidarCampos(classe);

            // Adicionando o comando SQl Insert
            classe.AppendLine("      string sql = @\"");
            classe.AppendLine(GerarComandoSQLInsert(mrw));
            classe.AppendLine("\";");
            classe.AppendFormat("\t{0} bd = new {0}(sql); \n", Settings.Default.ClasseConsultaBanco);
            // Adicionando a configuração de parâmetros
            classe.AppendLine(MontarParametroDBUtil(mrw.GetListaCamposRows(),false, true, true, true));
            classe.AppendFormat("\t object objRetorno = bd.{0}();\n", Settings.Default.MetodoExecuteScalar);
            classe.AppendLine("\t return ( objRetorno == null || Convert.IsDBNull(objRetorno)) ? 0 : Convert.ToInt32(objRetorno);");
            classe.AppendLine("\t}"); // Fechamento do método Insert
            
            return classe.ToString();
        }
        #endregion Insert

        private DataColumn[] PrimaryKey
        {
            get
            {
                DataTable dt = GetPrimaryKeys(Tabela);
                List<DataColumn> resultado = new List<DataColumn>();
                if (dt.Rows.Count == 1)
                {
                    DataColumn[] Colunas = new DataColumn[ColunasMetaData.Columns.Count];
                    ColunasMetaData.Columns.CopyTo(Colunas, 0);
                    resultado.Add(PesquisarColuna( Colunas, dt.Rows[0]["column_name"].ToString()));
                }
                else
                {
                    foreach (DataColumn primaryKey in this.ColunasMetaData.PrimaryKey)
                    {
                        //if (dt.Select("column_name='" + primaryKey.ColumnName + "'").Length <= 0)
                        //    continue;
                        //else
                        //{
                            resultado.Add(primaryKey);
                        //}
                    }
                }
                return resultado.ToArray();
            }
        }
        #region Update


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Não foi observada a otimização do código</remarks>
        /// <param name="ColunasParaUpdate"></param>
        /// <param name="Keys"></param>
        /// <param name="AddCamposTransacionaisAlteracao"></param>
        /// <param name="AddCamposTransacionaisInclusao"></param>
        /// <param name="AddTipoDado"></param>
        /// <returns></returns>
        private string MontarDeclaracaoParametrosUpdate(string Tabela, DataColumn[] ColunasParaUpdate, bool AddCamposTransacionaisAlteracao, bool AddCamposTransacionaisInclusao, bool AddTipoDado)
        {
            #region Adicionando as chaves no início
            List<string> resultado = new List<string>();
            foreach (DataColumn primaryKey in PrimaryKey)
            {                
                string parametro = string.Empty;
                string prefixo = Settings.Default.NomePrefixoCamposManutencao;

                string NomeColunaOriginal = primaryKey.ColumnName;
                //string NomeColunaOriginal = PrimaryKey["column_name"].ToString();
                string NomeColuna = NomeColunaOriginal.Substring(NomeColunaOriginal.IndexOf('_') + 1).ToUpper();
                if (AddCamposTransacionaisAlteracao)
                {
                    switch (NomeColuna)
                    {
                        case "USUTRA":
                        case "DATHOR":
                            continue;
                    }
                }
                if (AddCamposTransacionaisInclusao)
                {
                    switch (NomeColuna)
                    {
                        case "USUINC":
                        case "DATINC":
                            continue;
                    }
                }

                if (AddTipoDado)
                {
                    //parametro = this.ColunasMetaData.Columns[NomeColunaOriginal].DataType.ToString();
                    parametro = primaryKey.DataType.ToString();
                    parametro += ColumnAllowNull(primaryKey) ? "? " : " ";
                    //parametro += this.ColunasMetaData.Columns[NomeColunaOriginal].AllowDBNull && this.ColunasMetaData.Columns[NomeColunaOriginal].DataType.IsValueType ? "? " : " ";
                }
                else
                {
                    parametro = "\n ";
                }
                parametro += prefixo + NomeColunaOriginal;
                resultado.Add(parametro);
                //resultado[i] = (AddTipoDado ? Filtro.DataType.ToString() : "\n") + 
                //    " " + Filtro.ColumnName;
            }
            #endregion Adicionando as chaves no início
            #region Adicionando os outros campos que podem ser alterados
            if (ColunasParaUpdate != null && ColunasParaUpdate.Length > 0)
            {
                for (int i = 0; i < ColunasParaUpdate.Length; i++)
                {
                    DataColumn Filtro = ColunasParaUpdate[i];
                    string parametro = string.Empty;
                    string prefixo = String.Empty;
                    if (Filtro.AutoIncrement)
                        continue;

                    string NomeColuna = Filtro.ColumnName.Substring(Filtro.ColumnName.IndexOf('_') + 1).ToUpper();
                    if (AddCamposTransacionaisAlteracao)
                    {
                        switch (NomeColuna)
                        {
                            case "USUTRA":
                            case "DATHOR":
                                continue;
                        }
                    }
                    if (AddCamposTransacionaisInclusao)
                    {
                        switch (NomeColuna)
                        {
                            case "USUINC":
                            case "DATINC":
                                continue;
                        }
                    }

                    if (AddTipoDado)
                    {
                        parametro = Filtro.DataType.ToString();
                        parametro += ColumnAllowNull(Filtro) ? "? " : " ";
                    }
                    else
                    {
                        parametro = "\n ";
                    }
                    parametro += prefixo + Filtro.ColumnName;
                    resultado.Add(parametro);
                }
            }
            #endregion Adicionando os outros campos que podem ser alterados
            
            return string.Join(", ", resultado.ToArray());
        }

        protected override string GerarComandoUpdate(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (!ListaOpcoesContemOpcao(mrw, DataObjectMethodType.Update))
            {
                return "";
            }
            this.Tabela = mrw.ClasseCRUDRow.Tabela;

            StringBuilder classe = new StringBuilder();
            classe.AppendLine("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Update)]");
            classe.AppendFormat("public static void {0} ({1})", string.IsNullOrEmpty(mrw.NomeMetodo) ? Settings.Default.NomeDefaultMetodoUpdate : mrw.NomeMetodo,
                MontarDeclaracaoParametrosUpdate(Tabela, RetornarColunas(mrw.GetListaCamposRows()), true, true, true) 
                );
                //this.MontarDeclaracaoParametro(RetornarColunas(mrw.GetListaCamposRows()), true, true, true, true, true));
            classe.Append("{\n");


            // Adicionando a chamada para o método que irá validar os campos
            //AdicionarChamadaValidarCampos(classe);
            // Adicionando o comando SQl

            classe.Append("      string sql = \n@\"");
            classe.Append(GerarComandoSQLUpdate(mrw));
            classe.Append("\";\n");
            // Adicionando a configuração de parâmetros
            classe.AppendFormat("\t{0} bd = new {0}(sql);\n", Settings.Default.ClasseConsultaBanco);            
            IDictionary<string,DataColumn> Colunas = new Dictionary<string,DataColumn>();
            foreach (DataColumn coluna in PrimaryKey)
            {
                Colunas.Add(coluna.ColumnName, coluna);
            }
            foreach (DataColumn coluna in RetornarColunas(mrw.GetListaCamposRows()))
            {
                if (!Colunas.ContainsKey(coluna.ColumnName))
                {
                    string NomeColuna = coluna.ColumnName.Substring(coluna.ColumnName.IndexOf('_') + 1).ToUpper();
                    switch (NomeColuna)
                    {
                        case "DATHOR":
                        case "USUINC":
                        case "DATINC":
                            continue;
                    }
                    Colunas.Add(coluna.ColumnName, coluna);
                }
            }
            DataColumn[] ListaColunas = new DataColumn[Colunas.Values.Count];
            Colunas.Values.CopyTo(ListaColunas,0);
            classe.AppendLine(MontarParametroDBUtil(ListaColunas ,false, true,true,true, false));  
            classe.AppendLine(MontarParametroDBUtil(PrimaryKey, true, true, true, true, true));
            classe.AppendFormat("      bd.{0}();\n", Settings.Default.MetodoExecuteNonQuery);
            classe.Append("    }\n"); // Fechamento do método
            return classe.ToString();
        }
        private string GerarComandoSQLUpdate(EstruturaSaveRAD.MetodoRow mrw)
        {
            StringBuilder comando = new StringBuilder();
            comando.Append("UPDATE " + mrw.ClasseCRUDRow.Tabela + " SET\n");
            // Gerando os campos do comando update

            DataColumn[] Columns = RetornarColunas(mrw.GetListaCamposRows());
            List<string> Colunas = new List<string>(Columns.Length);
            for (int i = 0; i < Columns.Length; i++)
            {
                //
                // Remove os campos identity
                //                
                if (Columns[i].AutoIncrement)
                    continue;

                //DataColumn campoProcurado = PesquisarColuna(ColunasMetaData.PrimaryKey, Columns[i].ColumnName);

                switch (Columns[i].ColumnName.Substring(Columns[i].ColumnName.IndexOf("_") + 1).ToUpper())
                {
                    case "USUINC":
                    case "DATINC":
                        continue;
                    case "DATHOR":
                        Colunas.Add(string.Format("\t{0}= GetDate()\n", Columns[i].ColumnName));
                        break;
                    default:
                        Colunas.Add(string.Format("\t{0}= {1}{0}\n", Columns[i].ColumnName, Settings.Default.SimboloParametroSQL));
                        break;

                }                
            }
            comando.Append(string.Join(", ", Colunas.ToArray()));
            if (PrimaryKey.Length > 0)
            {
                // Adicionado a cláusula where com os campos da chave primária
                comando.Append("\n    WHERE ");
                // Gerando os parâmetros do comando
                comando.Append(this.GerarWhere(PrimaryKey));
            }
            return comando.ToString();
        }

        #endregion Update

        #region Delete
        
        protected override string GerarComandoDelete(EstruturaSaveRAD.MetodoRow mrw)
        {
            if (!ListaOpcoesContemOpcao(mrw, DataObjectMethodType.Delete))
            {
                return "";
            }
            this.Tabela = mrw.ClasseCRUDRow.Tabela;

            StringBuilder classe = new StringBuilder();
            classe.AppendFormat("\n\t[System.ComponentModel.DataObjectMethod( System.ComponentModel.DataObjectMethodType.Delete)]");
            classe.AppendFormat("\n    public static void {0} ({1})", string.IsNullOrEmpty(mrw.NomeMetodo)? Settings.Default.NomeDefaultMetodoDelete : mrw.NomeMetodo, 
                this.MontarDeclaracaoParametrosUpdate(Tabela, null, true, true, true));
            classe.Append("\n\t{\n");
            // Adicionando o comando SQl DELETE
            classe.Append("      string sql = \n@\"");
            classe.Append(GerarComandoSQLDelete(mrw));
            classe.AppendFormat("\";");
            classe.AppendFormat("\t{0} bd = new {0}(sql);\n", Settings.Default.ClasseConsultaBanco);
            // Adicionando a configuração dos parâmetros
            classe.AppendLine(MontarParametroDBUtil(PrimaryKey, true, true, true, true, true));

            //classe.Append( MontarParametroDBUtil(RetornarColunas( mrw.GetListaCamposRows() ), true, false, false, false, true ));
            classe.AppendFormat("      bd.{0}();\n", Settings.Default.MetodoExecuteNonQuery);
            classe.Append("    }\n"); // Fechamento do método
            return classe.ToString();
        }
        private string GerarComandoSQLDelete(EstruturaSaveRAD.MetodoRow mrw)
        {
            StringBuilder comando = new StringBuilder();
            comando.AppendLine("DELETE FROM " + mrw.ClasseCRUDRow.Tabela);
            comando.AppendLine("\n    WHERE ");
            comando.AppendLine(this.GerarWhere(PrimaryKey));
            return comando.ToString();
        }

        #endregion Delete



        protected override string RetornarNomeClasse(EstruturaSaveRAD.ClasseCRUDRow ccr)
        {
            return (ccr.IsNomeClasseNull() ? ccr.Tabela : ccr.NomeClasse);
        }

        #endregion Geracao dos metodos Crud

        

      

        

        #region Geracao de metodo Baseado em consulta SQL

        /// <summary>
        /// Retorna a lista com os parâmetros da consulta
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        private string[] ObterParametrosConsulta(string consulta)
        {
            List<string> parametros = new List<string>();
            // posição para pecorrer o texto da consulta
            int posAtual = 0;
            while (posAtual < consulta.Length)
            {
                // Obtendo a posição do início do próximo parâmetro (posição da @)
                // A partir da posição atual
                int posInicioParametro = consulta.IndexOf(Settings.Default.SimboloParametroSQL, posAtual);
                // Caso não exista mais parâmetros
                if (posInicioParametro == -1)
                {
                    break;
                }
                // posição do final do parâmetro
                int posFinalParametro = posInicioParametro + 1;
                // verificando se a string acabou e indo até encontrar um
                // caracter diferente de uma letra (indicativo de que o parâmetro acabou)
                while (posFinalParametro < consulta.Length &&
                       char.IsLetterOrDigit(consulta[posFinalParametro]))
                {
                    posFinalParametro++;
                }
                // adicionado o parâmetro
                parametros.Add(consulta.Substring(posInicioParametro + 1,
                               posFinalParametro - posInicioParametro - 1));
                // Continuando a busca após o parâmetro encontrado
                posAtual = posFinalParametro;
            }
            return parametros.ToArray();
        }

        
        public string GerarConsulta(params EstruturaSaveRAD.MetodoConsultaRow[] mcrs)
        {
            if (mcrs == null || mcrs.Length == 0)
            {
                throw new ArgumentException("os dados da consulta são obrigatórios.");
            }
            StringBuilder classe = new StringBuilder();

            // Iniciando a classe
            classe = new StringBuilder();
            foreach (EstruturaSaveRAD.MetodoConsultaRow mcr in mcrs)
            {
                string[] parametros = ObterParametrosConsulta(mcr.SQL);
                classe.AppendFormat("// Parametros: {0} \n", string.Join(", ", parametros));
                classe.Append("public static DataTable " + mcr.NomeConsulta + "(");
                // Obtendo a coleção de parâmetros
                // Adicionando a declaração da lista de parâmetros (Assinatura do método)
                classe.Append(MontarDeclaracaoParametro(parametros));
                classe.AppendLine(")"); // Fechamento dos parâmetros
                classe.AppendLine("{"); // Abertura do método
                //
                // Corpo do método
                //
                classe.AppendLine("\n  string sql = @\"\n" + mcr.SQL + "\";");
                classe.AppendFormat("\t{0} bd = new {0}(sql);\n", Settings.Default.ClasseConsultaBanco);
                // Adicionando a declaração dos parâmetros da consulta (ADO.NET)
                classe.AppendLine(MontarParametroDBUtil(parametros));
                classe.AppendFormat("  return bd.{0}();\n", Settings.Default.MetodoObterDataTable);
                classe.AppendLine("}"); // Fechamento do método
                classe.AppendLine();
            }
            return classe.ToString();
        }

        #endregion Geracao de metodo Baseado em consulta SQL


        
        #region DataSetTipado

        public string Gerar( EstruturaSaveRAD.ClasseCRUDDataTable ccr)
        {
            StringWriter sw = new StringWriter();
            ccr.WriteXmlSchema(sw,true);
            sw.Flush();
            CodeCompileUnit codeUnit = new CodeCompileUnit();
            CodeNamespace codeNamespace = new CodeNamespace("Teste");
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            codeUnit.Namespaces.Add(codeNamespace);
            
            System.Data.Design.TypedDataSetGenerator.Generate(sw.ToString(),
                codeUnit, codeNamespace, codeProvider, this.Factory
                );
            sw = new StringWriter();
            codeProvider.GenerateCodeFromCompileUnit(codeUnit, sw, new CodeGeneratorOptions());
            return sw.ToString();
        }
        #endregion DataSetTipado

 
    }

}