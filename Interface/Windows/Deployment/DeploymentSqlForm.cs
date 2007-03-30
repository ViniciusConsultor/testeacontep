using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Deployment
{
    public partial class DeploymentSqlForm : Form
    {
        DeploymentSqlDst lista = new DeploymentSqlDst();
        public DeploymentSqlForm()
        {
            InitializeComponent();
        }

        private void tpeItems_DragOver(object sender, DragEventArgs e)
        {
            validarItems((string[])(e.Data.GetData(DataFormats.FileDrop)));
        }

        private void validarItems(string[] items)
        {
            string filtro = string.Empty;
            foreach (string i in items)
            {
                filtro = "Arquivo = '{0}'";
                if (lista.File.Select(string.Format(filtro, i)).Length == 0)
                {
                    lista.File.AddFileRow(i, lista.File.Rows.Count == 0 ? 1 : (int)lista.File.Rows[lista.File.Rows.Count - 1]["codigo"] + 1, 0,new FileInfo(i).Name);
                    //dgvItems.Rows.Add(i);
                }
            }
            AtuatlizarItems((DeploymentSqlDst.FileRow[])lista.File.Select("0=0", "Arquivo"));
        }

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                lista.WriteXml(saveFileDialog.FileName);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lista = new DeploymentSqlDst();
                lista.ReadXml(openFileDialog.FileName);
                AtuatlizarItems((DeploymentSqlDst.FileRow[])lista.File.Select("0=0","Arquivo"));
            }
        }
        private void AtuatlizarItems(DeploymentSqlDst.FileRow[] items)
        {
            dgvItems.Rows.Clear();
            cbxItems.Items.Clear();
            cbxItems.Text = string.Empty;
            foreach (DataRow drw in items)
            {
                dgvItems.Rows.Add(drw["Arquivo"].ToString());
                cbxItems.Items.Add(drw["Arquivo"].ToString());
            }
            if (cbxItems.Items.Count > 0)
                cbxItems.SelectedIndex = 0;
            AtualizarDependencias((DeploymentSqlDst.FileRow[])lista.File.Select("0=0", "Arquivo"));
        }

        private void AtualizarDependencias(DeploymentSqlDst.FileRow[] items)
        {
            clbDependecias.Items.Clear();
            string sql = "codigo = {0} and Dependencia = {1}";
            if (cbxItems.SelectedIndex != -1)
            {
                DataRow item = ObterFileRow(lista, cbxItems.Text);
                foreach (DataRow drw in items)
                {
                    if (drw["Arquivo"].ToString() != cbxItems.Text)
                        if (lista.Dependeci.Select(string.Format(sql, item["Codigo"].ToString(), drw["Codigo"].ToString())).Length > 0)
                            clbDependecias.Items.Add(drw["Arquivo"].ToString(), true);
                        else
                            clbDependecias.Items.Add(drw["Arquivo"].ToString());
                }
            }
        }

        private void cbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarDependencias((DeploymentSqlDst.FileRow[])lista.File.Select("0=0", "Arquivo"));
            if (cbxItems.SelectedIndex != -1)
            {
                DeploymentSqlDst.FileRow file = ObterFileRow(lista, cbxItems.Text);
                rbnOpBasica.Checked = false;
                rbnDados.Checked = false;
                switch (file.Atributo)
                {
                    case 1:
                        rbnOpBasica.Checked = true;
                        break;
                    case 2:
                        rbnDados.Checked = true;
                        break;
                }
            }
        }

        private void clbDependecias_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                DeploymentSqlDst.FileRow dependente = ObterFileRow(lista, cbxItems.Text);
                DeploymentSqlDst.FileRow dependencia = ObterFileRow(lista, clbDependecias.Items[e.Index].ToString());
                if (dependencia.Atributo != 0)
                {
                    MessageBox.Show("Dependecia definida como operação basica ou dados");
                    e.NewValue = CheckState.Unchecked;
                    return;
                }
                string sql = "Codigo = {0} and Dependencia = {1}";
                string itemExiste = string.Format(sql, dependente["Codigo"], dependencia["Codigo"].ToString());
                string ItemDependente = string.Format(sql, dependencia["Codigo"].ToString(), dependente["Codigo"].ToString());
                if (lista.Dependeci.Select(ItemDependente).Length > 0)
                {
                    MessageBox.Show("Dependecia já dependente do item");
                    e.NewValue = CheckState.Unchecked;
                    return;
                }
                if (lista.Dependeci.Select(itemExiste).Length == 0)
                {
                    if (!VerificarDependecia((int)dependente["Codigo"], (int)dependencia["Codigo"]))
                    {
                        DeploymentSqlDst.DependeciRow linha = lista.Dependeci.NewDependeciRow();
                        linha.Dependencia = (int)dependencia["Codigo"];
                        linha.Codigo = (int)dependente["Codigo"];
                        lista.Dependeci.AddDependeciRow(linha);
                    }
                    else
                    {
                        MessageBox.Show("Dependecia já dependente do item");
                        e.NewValue = CheckState.Unchecked;
                        return;
                    }
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                int dependecia = (int)ObterFileRow(lista, clbDependecias.Items[e.Index].ToString())["Codigo"];
                int dependente = (int)ObterFileRow(lista, cbxItems.Text)["Codigo"];
                foreach (DataRow drw in lista.Dependeci.Rows)
                {
                    if ((int)drw["Codigo"] == dependente && (int)drw["Dependencia"] == dependecia)
                    {
                        lista.Dependeci.Rows.Remove(drw);
                        return;
                    }
                }
            }
        }
        private bool VerificarDependecia(int dependente, int dependencia)
        {
            foreach (DeploymentSqlDst.DependeciRow row in ObterDependeciRows(lista, dependencia))
            {
                if (ObterDependeciRows(lista, (int)row["Dependencia"]).Length > 0)
                {
                    if (VerificarDependecia(dependente, (int)row["Dependencia"]))
                        return true;
                }
                //else
                //{
                    if ((int)row["Dependencia"] == dependente)
                        return true;
                //}
            }
            return false;
        }

        private void dgvItems_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int codigo = ObterCodigo(lista, e.Row.Cells[0].Value.ToString());
            foreach (DeploymentSqlDst.DependeciRow row in lista.Dependeci.Select("Codigo = '" + codigo + "'"))
            {
                lista.Dependeci.RemoveDependeciRow(row);
            }
            lista.File.RemoveFileRow(ObterFileRow(lista, e.Row.Cells[0].Value.ToString()));
            AtuatlizarItems((DeploymentSqlDst.FileRow[])lista.File.Select());
            e.Cancel = true;
        }

        private int ObterCodigo(DeploymentSqlDst valor, string path)
        {
            DeploymentSqlDst.FileRow[] rows = (DeploymentSqlDst.FileRow[])lista.File.Select("Arquivo = '" + path + "'");
            if (rows.Length > 0)
                return (int)rows[0]["Codigo"];
            return -1;
        }

        private DeploymentSqlDst.FileRow ObterFileRow(DeploymentSqlDst valor, string path)
        {
            DeploymentSqlDst.FileRow[] rows = (DeploymentSqlDst.FileRow[])lista.File.Select("Arquivo = '" + path + "'");
            if (rows.Length > 0)
                return rows[0];
            return null;
        }

        private DeploymentSqlDst.FileRow ObterFileRow(DeploymentSqlDst valor, int codigo)
        {
            DeploymentSqlDst.FileRow[] rows = (DeploymentSqlDst.FileRow[])lista.File.Select("Codigo = " + codigo);
            if (rows.Length > 0)
                return rows[0];
            return null;
        }

        private DeploymentSqlDst.DependeciRow[] ObterDependeciRows(DeploymentSqlDst valor, int codigo)
        {
            DeploymentSqlDst.DependeciRow[] rows = (DeploymentSqlDst.DependeciRow[])lista.Dependeci.Select("Codigo = " + codigo);
            if (rows.Length > 0)
                return rows;
            return new List<DeploymentSqlDst.DependeciRow>().ToArray();
        }

        private void cbxOpBasica_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbxItems.SelectedIndex != -1)
            //{
            //    DeploymentSqlDst.FileRow file = ObterFileRow(lista, cbxItems.Text);
            //    file["Basico"] = cbxOpBasica.Checked;
            //    if (cbxOpBasica.Checked)
            //    {
            //        foreach (DeploymentSqlDst.DependeciRow row in ObterDependeciRows(lista,(int)file["Codigo"]))
            //        {
            //            lista.Dependeci.RemoveDependeciRow(row);
            //        }
            //        clbDependecias.Enabled = false;
            //    }
            //    else
            //        clbDependecias.Enabled = true;

            //}
        }

        private void OrdenacaoAtributo(DeploymentSqlDst valor, int atributo)
        {           
            foreach (DeploymentSqlDst.FileRow row in valor.File.Rows)
            {
                if (row.Atributo == atributo)
                {
                    ordem.Add((int)row["Codigo"]);                    
                }
            }            
        }
        List<int> ordem = new List<int>();
        private void OrdenaDependecias(int codigo)//int codigo, ref Deployment ordenado)
        {
            foreach (DeploymentSqlDst.DependeciRow row in ObterDependeciRows(lista, codigo))
            {
                if (ObterDependeciRows(lista, (int)row["Dependencia"]).Length > 0)
                    OrdenaDependecias((int)row["Dependencia"]);
                if (!ordem.Contains((int)row["Dependencia"]))
                    ordem.Add((int)row["Dependencia"]);
            }            
        }

        private void OrdenaDependecias(DeploymentSqlDst valor)
        {
            OrdenacaoAtributo(lista,1);
            foreach (DeploymentSqlDst.FileRow row in valor.File.Rows)
            {
                if (row.Atributo == 0)
                {
                    OrdenaDependecias(row.Codigo);
                    if (!ordem.Contains(row.Codigo))
                        ordem.Add(row.Codigo);
                }
            }
            OrdenacaoAtributo(lista, 2);
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ordem.Clear();
            OrdenaDependecias(lista);
            if (saveFileDialogOrdem.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialogOrdem.FileName))
                    File.Delete(saveFileDialogOrdem.FileName);
                StreamWriter swr = new StreamWriter(saveFileDialogOrdem.FileName, false);
                foreach (int i in ordem)
                    swr.WriteLine(ObterFileRow(lista, i)["Path"]);
                swr.Flush();
                swr.Close();
            }
        }

        private void tbxItems_TextChanged(object sender, EventArgs e)
        {
            //Deployment items = lista.Copy();
            //items.File = 
            //.File.Select("Path like " + tbxItems.Text);
            if (tbxItems.Text != string.Empty)
                AtuatlizarItems((DeploymentSqlDst.FileRow[])lista.File.Select("Arquivo like '" + tbxItems.Text + "'"));
            else
                AtuatlizarItems((DeploymentSqlDst.FileRow[])lista.File.Select());

        }

        private void rbnOpBasica_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxItems.SelectedIndex != -1)
            {
                DeploymentSqlDst.FileRow file = ObterFileRow(lista, cbxItems.Text);
                if(rbnOpBasica.Checked)
                    file.Atributo = 1;
                if (rbnOpBasica.Checked)
                {
                    foreach (DeploymentSqlDst.DependeciRow row in ObterDependeciRows(lista, (int)file["Codigo"]))
                    {
                        lista.Dependeci.RemoveDependeciRow(row);
                    }
                    clbDependecias.Enabled = false;
                }
                else
                    clbDependecias.Enabled = true;

            }
        }

        private void rbnDados_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxItems.SelectedIndex != -1)
            {
                DeploymentSqlDst.FileRow file = ObterFileRow(lista, cbxItems.Text);
                if (rbnDados.Checked)
                    file.Atributo = 2;
                if (rbnDados.Checked)
                {
                    foreach (DeploymentSqlDst.DependeciRow row in ObterDependeciRows(lista, (int)file["Codigo"]))
                    {
                        lista.Dependeci.RemoveDependeciRow(row);
                    }
                    clbDependecias.Enabled = false;
                }
                else
                    clbDependecias.Enabled = true;

            }
        }
    }
}