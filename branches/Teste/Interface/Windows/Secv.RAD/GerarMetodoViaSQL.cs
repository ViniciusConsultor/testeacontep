using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Acontep.Ng.Manutencao.Rad;

namespace Acontep.RAD
{
    public partial class GerarMetodoViaSQL : Form
    {
        public GerarMetodoViaSQL()
        {
            InitializeComponent();
        }
        private void PreencherLista()
        {
            PreencherLista(EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.Rows, true);
        }
        private void PreencherLista( object Metodos, bool limparLista )
        {
            if (limparLista)
                lstMetodos.Items.Clear();
            foreach (EstruturaSaveRAD.MetodoConsultaRow metodo in (ICollection)Metodos)
            {
                lstMetodos.Items.Add(metodo, false);
            }
        }
        private void Filtrar()
        {
            if (string.IsNullOrEmpty(edtFiltroSQL.Text))
            {
                PreencherLista();
            }
            else
            {
                PreencherLista(
                EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.Select(
                    string.Format("NomeConsulta like '{0}'", edtFiltroSQL.Text)
                    )
                , true);
            }
        }
        private void edtFiltroSQL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar();
            }
            catch (Exception Erro)
            {
                AppUtil.ExibirErro(Erro);
            }
        }

        private void lstMetodos_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((EstruturaSaveRAD.MetodoConsultaRow)e.ListItem).NomeConsulta;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            lstMetodos.ClearSelected();
        }

        private void PreencherTela(EstruturaSaveRAD.MetodoConsultaRow metodoConsultaRow)
        {
            edtNomeConsulta.Text = metodoConsultaRow.NomeConsulta;
            edtSQL.Text = metodoConsultaRow.SQL;
        }

        private void toolStripButtonAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                EstruturaSaveRAD.MetodoConsultaRow metodo = EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.NewMetodoConsultaRow();
                if (lstMetodos.SelectedItem == null)
                {
                    metodo.ID = Guid.NewGuid();
                }
                else
                {
                    metodo = ((EstruturaSaveRAD.MetodoConsultaRow)lstMetodos.SelectedItem);
                }
                metodo.String = string.Empty;
                metodo.Provider = string.Empty;
                metodo.SQL = edtSQL.Text;
                metodo.NomeConsulta = edtNomeConsulta.Text;
                if (lstMetodos.SelectedItem == null)
                {
                    EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.AddMetodoConsultaRow(metodo);

                }
                PreencherLista();
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }
        }

        private void GerarMetodoViaSQL_Load(object sender, EventArgs e)
        {
            try
            {
                PreencherLista();
            }
            catch (Exception erro)
            {
                AppUtil.ExibirErro(erro);
            }

        }

        private void tabControlMetodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControlMetodos.SelectedTab == tabPageResultado)
                {
                    edtResultado.Text = new GeradorCRUD().GerarConsulta((EstruturaSaveRAD.MetodoConsultaRow[])new ArrayList(EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.Rows).ToArray(typeof(EstruturaSaveRAD.MetodoConsultaRow)));
                }
            }
            catch (Exception Erro)
            {
                AppUtil.ExibirErro(Erro);
                tabControlMetodos.SelectedTab = tabPageConfiguracao;
            }
        }

        private void toolStripButtonAreaTransferencia_Click(object sender, EventArgs e)
        {
            edtResultado.Copy();

        }

        private void toolStripButtonRemover_Click(object sender, EventArgs e)
        {
            foreach (EstruturaSaveRAD.MetodoConsultaRow metodo in lstMetodos.CheckedItems)
            {
                metodo.Delete();
            }
            PreencherLista();

        }

        private void lstMetodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EstruturaSaveRAD.MetodoConsultaRow metodo = lstMetodos.SelectedItem as EstruturaSaveRAD.MetodoConsultaRow;
                if (metodo == null)
                {
                    metodo = EstruturaSaveRadUtil<EstruturaSaveRAD>.ConteudoClasse.MetodoConsulta.NewMetodoConsultaRow();
                }
                PreencherTela(metodo);
            }
            catch (Exception Erro)
            {
                AppUtil.ExibirErro(Erro);
            }
        }
    }
}