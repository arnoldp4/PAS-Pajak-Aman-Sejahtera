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
    public partial class FormSearch : Form
    {
        private OracleConnection conn;
        public FormSearch(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("NIK Kosong!");
                    return;
                }


                OracleCommand cmd = new OracleCommand("SELECT NIK FROM WARGA WHERE NIK = :NIK", conn);
                cmd.Parameters.Add(":NIK", textBox2.Text);

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if (i == 1)
                {
                    FormTransaksi frm = new FormTransaksi(conn, textBox2.Text);
                    frm.ShowDialog();

                    ds.Clear();
                }
                else
                {
                    MessageBox.Show("NIK Tidak terdaftar!");
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
