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
    public partial class MasterWarga : Form
    {
        private OracleConnection conn;
        OracleCommand cmd;
        OracleDataAdapter oda;

        public MasterWarga(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void loadGrid()
        {
            try
            {
                cmd = new OracleCommand("select * from warga", conn);
                oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                oda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Refresh();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.Text = "";
            comboBox1.Text = "";
        }

        private string autogen()
        {
            string kode = "", temp = "";
            int ctr = 0;

            temp = "0031000000000";
            DataTable dt = new DataTable();
            var MaxID = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Max(r => Convert.ToInt64(r.Cells["NIK"].Value));
            ctr = Convert.ToInt32(MaxID.ToString().Substring(10, 4));
            ctr = ctr + 1;

            if (ctr < 10)
                kode = temp + "00" + ctr;
            else if (ctr > 9 && ctr < 100)
                kode = temp + "0" + ctr;
            else
                kode = temp + ctr;

            return kode;
        }

        private void MasterWarga_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["NIK"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["NAMA"].Value.ToString();
            int jk = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells["JK"].Value.ToString());
            if (jk == 0)
                comboBox1.SelectedIndex = 1;
            else
                comboBox1.SelectedIndex = 0;
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["TGL_LAHIR"].Value.ToString());
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["ALAMAT"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["TELP"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["K_KETERANGAN"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO warga VALUES (:nik, :nama, :alamat, :tgl, :jk, :telp, NULL)", conn);
                string nik = autogen();
                cmd.Parameters.Add("nik", nik);
                cmd.Parameters.Add("nama", textBox2.Text);
                string kel;
                string jk = comboBox1.SelectedIndex.ToString();
                if (jk == "0")
                    kel = "1";
                else
                    kel = "0";
                cmd.Parameters.Add("jk", kel);
                cmd.Parameters.Add("tgl", dateTimePicker1.Value);
                cmd.Parameters.Add("alamat", textBox3.Text);
                cmd.Parameters.Add("telp", textBox4.Text);
                cmd.ExecuteNonQuery();

                loadGrid();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("update warga set nama=:nama, jk=:jk, tgl_lahir=:tgl, alamat=:alamat, telp=:telp WHERE nik = :nik", conn);
                cmd.Parameters.Add("nama", textBox2.Text);
                string kel;
                string jk = comboBox1.SelectedIndex.ToString();
                if (jk == "0")
                    kel = "1";
                else
                    kel = "0";
                cmd.Parameters.Add("jk", kel);
                cmd.Parameters.Add("tgl", dateTimePicker1.Value);
                cmd.Parameters.Add("alamat", textBox3.Text);
                cmd.Parameters.Add("telp", Convert.ToInt32(textBox4.Text));
                cmd.Parameters.Add("nik", textBox1.Text);
                cmd.ExecuteNonQuery();

                loadGrid();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("delete from warga where nik = :nik", conn);
                cmd.Parameters.Add("nik", textBox1.Text);
                cmd.ExecuteNonQuery();

                loadGrid();
                clear();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
