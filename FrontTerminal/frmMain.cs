﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Library;

namespace FrontTerminal
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“frDataSet.overdue_reader”中。您可以根据需要移动或删除它。
            this.overdue_readerTableAdapter.Fill(this.frDataSet.overdue_reader);
        }

        private void btnSearchReader_Click(object sender, EventArgs e)
        {
            if (txbReaderId.Text == "")
            {
                MessageBox.Show("请输入读者编号");
            }
            else
            {
                int readerID = Convert.ToInt32(txbReaderId.Text);
                Console.Out.WriteLine(readerID);
                SqlConnection con = Connection.Instance();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmdshow = new SqlCommand();
                
                
                try
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from  Reader where id = " + readerID;
                    SqlDataReader record = cmd.ExecuteReader();

                    if (!record.HasRows)
                        MessageBox.Show("没有该用户！");
                    else
                    {
                        while (record.Read())
                        {
                            txbName.Text = record[1].ToString();
                            if (Convert.ToInt32(record[3]) == 0)
                                txbGender.Text = "女";
                            else
                                txbGender.Text = "男";
                        }
                        record.Close();
                    }

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                try
                {
                    cmdshow.Connection = con;
                    cmdshow.CommandText= "select * from rental where reader_id=" + readerID;
                    SqlDataReader recRental = cmdshow.ExecuteReader();
                    while (recRental.Read())
                    {
                        dgvReaderBorrow.Rows.Add(new object[] { recRental[0], recRental[1], recRental[2], recRental[3], recRental[4], recRental[5] });
                    }
                    recRental.Close();
                    
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                con.Close();
            }
           
        }

        private void dgvReaderBorrow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String readerName = Convert.ToString(txbReadName.Text);
            String readerGender = Convert.ToString(cbbReaderGender.Text);
            int readerGenderI;
            if (readerGender == "女")
            {
                readerGenderI = 0;
            }
            else if (readerGender == "男")
            {
                readerGenderI = 1;
            }
            else
            {
                readerGenderI = 2;
            }
            Console.Out.WriteLine(readerName);
            SqlConnection con = Connection.Instance();
            SqlCommand cmdReader = new SqlCommand();
            try
            {
                cmdReader.Connection = con;
                if (readerGenderI == 2)
                {
                    cmdReader.CommandText = "select * from  Reader where name like '*" + readerName + "%'";
                }
                else
                    cmdReader.CommandText = "select * from  Reader where name like '*" + readerName + "%' and gender=" + readerGenderI;
                SqlDataReader recordShow = cmdReader.ExecuteReader();

                if (!recordShow.HasRows)
                    MessageBox.Show("没有该用户！");
                else
                {
                    while (recordShow.Read())
                    {
                        dbgReaderinfo.Rows.Add(new object[] { recordShow[0], recordShow[1], recordShow[3], recordShow[4], recordShow[5], recordShow[6], recordShow[7], recordShow[8], recordShow[9], recordShow[10] });
                    }
                    recordShow.Close();

                }
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
    }
}
