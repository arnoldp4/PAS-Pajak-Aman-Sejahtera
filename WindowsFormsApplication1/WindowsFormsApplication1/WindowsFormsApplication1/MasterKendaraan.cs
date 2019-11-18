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
    public partial class MasterKendaraan : Form
    {
        private OracleConnection conn;
        OracleCommand cmd;
        OracleDataAdapter oda;

        public MasterKendaraan(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void loadGrid()
        {
            try
            {
                cmd = new OracleCommand("select * from kendaraan", conn);
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
        }

        private int autogen()
        {
            int ctr = 0;
            DataTable dt = new DataTable();
            var MaxID = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Max(r => Convert.ToInt32(r.Cells["k_id"].Value));
            ctr = MaxID + 1;
            return ctr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO kendaraan VALUES (:k_id, :k_kategori, :k_biaya, :k_keterangan)", conn);
                int k_id = autogen();
                cmd.Parameters.Add("k_id", k_id);
                cmd.Parameters.Add("k_kategori", textBox2.Text);
                cmd.Parameters.Add("k_biaya", textBox3.Text);
                cmd.Parameters.Add("k_keterangan", textBox4.Text);
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
                OracleCommand cmd = new OracleCommand("update kendaraan set k_kategori = :k_kategori, k_biaya = :k_biaya, k_keterangan = :k_keterangan WHERE k_id = :k_id", conn);
                cmd.Parameters.Add("k_kategori", textBox2.Text);
                cmd.Parameters.Add("k_biaya", textBox3.Text);
                cmd.Parameters.Add("k_keterangan", textBox4.Text);
                cmd.Parameters.Add("k_id", textBox1.Text);
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
                OracleCommand cmd = new OracleCommand("delete from kendaraan where k_id = :k_id", conn);
                cmd.Parameters.Add("k_id", textBox1.Text);
                cmd.ExecuteNonQuery();

                loadGrid();
                clear();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["k_id"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["k_kategori"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["k_biaya"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["k_keterangan"].Value.ToString();
        }

        private void MasterKendaraan_Load(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
