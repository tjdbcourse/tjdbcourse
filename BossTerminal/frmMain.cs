﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BossTerminal
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataSet.library”中。您可以根据需要移动或删除它。
            this.libraryTableAdapter.Fill(this.dataSet.library);
            // TODO: 这行代码将数据加载到表“dataSet.manager”中。您可以根据需要移动或删除它。
            this.managerTableAdapter.Fill(this.dataSet.manager);
        }

        private void mnuToolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuBossChangeName_Click(object sender, EventArgs e)
        {
            frmBossName fm = new frmBossName();
            fm.ShowDialog();
            fm.Close();
        }

        private void mnuBossChangePassword_Click(object sender, EventArgs e)
        {
            new frmPassword().ShowDialog();
        }

        private void mnuToolRefresh_Click(object sender, EventArgs e)
        {
            this.managerTableAdapter.Fill(this.dataSet.manager);
            this.libraryTableAdapter.Fill(this.dataSet.library);
        }

        private void mnuToolSave_Click(object sender, EventArgs e)
        {
            new SqlCommandBuilder(this.managerTableAdapter.Adapter);
            this.managerTableAdapter.Update(this.dataSet);
            new SqlCommandBuilder(this.libraryTableAdapter.Adapter);
            this.libraryTableAdapter.Update(this.dataSet);
        }

        private string lastPassword = "";

        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            DataGridViewRow row = view.Rows[e.RowIndex];
            DataGridViewColumn column = view.Columns[e.ColumnIndex];
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            if (column.DataPropertyName.Equals("password"))
            {
                if (cell.Value is string)
                    lastPassword = (string) cell.Value;
                cell.Value = "";
            }
            else
                Library.Util.TrimGridCell((DataGridView)sender, e.RowIndex, e.ColumnIndex);
        }

        private void dgvManager_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            DataGridViewRow row = view.Rows[e.RowIndex];
            DataGridViewColumn column = view.Columns[e.ColumnIndex];
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            if (column.DataPropertyName.Equals("password"))
            {
                if (((string)cell.Value).Length == 0)
                    cell.Value = lastPassword;
                else
                    cell.Value = Library.Util.MD5((string)cell.Value);
            }
            else if (column.DataPropertyName.Equals("library_id"))
            {
                if (!(row.Cells[0].Value is DBNull) && ((int)row.Cells[0].Value > 0))
                {
                    Library.Util.UpdateGridCell(view, e.RowIndex, e.ColumnIndex);
                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            mnuToolRefresh_Click(sender, e);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            mnuToolSave_Click(sender, e);
        }

        private void tsbChangeName_Click(object sender, EventArgs e)
        {
            mnuBossChangeName_Click(sender, e);
        }

        private void tsbChangePassword_Click(object sender, EventArgs e)
        {
            mnuBossChangePassword_Click(sender, e);
        }
    }
}
