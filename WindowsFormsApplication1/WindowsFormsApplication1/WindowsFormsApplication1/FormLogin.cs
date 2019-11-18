using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Oracle.DataAccess.Client;

namespace WindowsFormsApplication1
{
    public partial class FormLogin : Form
    {
        public OracleConnection conn;
        public FormLogin()
        {
            InitializeComponent();
            conn = new OracleConnection("Data Source=arnoldp4;User ID=pcs17;Password=stts");
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Username atau password kosong!");
                    return;
                }


                OracleCommand cmd = new OracleCommand("SELECT * FROM SYSTEMUSER WHERE USERNAME=:u AND PASSWORD=:p", conn);
                cmd.Parameters.Add(":u", textBox1.Text);
                cmd.Parameters.Add(":p", textBox2.Text);

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if (i == 1)
                {
                    if(textBox1.Text == "admin")
                    {
                        MenuMaster frm = new MenuMaster(conn);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MenuAdministrasi frm1 = new MenuAdministrasi(conn);
                        frm1.ShowDialog();
                    }
                    ds.Clear();
                }
                else
                {
                    MessageBox.Show("Username Tidak terdaftar!");
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
