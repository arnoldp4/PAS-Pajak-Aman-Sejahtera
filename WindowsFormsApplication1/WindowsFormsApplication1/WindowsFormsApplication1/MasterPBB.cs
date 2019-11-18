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
    public partial class MasterPBB : Form
    {
        private OracleConnection conn;
        OracleCommand cmd;
        OracleDataAdapter oda;

        public MasterPBB(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void loadGrid()
        {
            try
            {
                cmd = new OracleCommand("select * from pbb", conn);
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
                        .Max(r => Convert.ToInt32(r.Cells["pbb_id"].Value));
            ctr = MaxID + 1; ;
            return ctr;
        }

        private void MasterPBB_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO pbb VALUES (:pbb_id, :pbb_tipe, :pbb_biaya, :pbb_keterangan)", conn);
                int pbb_id = autogen();
                cmd.Parameters.Add("pbb_id", pbb_id);
                cmd.Parameters.Add("pbb_tipe", textBox2.Text);
                cmd.Parameters.Add("pbb_biaya", textBox3.Text);
                cmd.Parameters.Add("pbb_keterangan", textBox4.Text);
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
                OracleCommand cmd = new OracleCommand("update pbb set pbb_tipe = :pbb_tipe, pbb_biaya = :pbb_biaya, pbb_keteranga = :pbb_keterangan WHERE pbb_id = :pbb_id", conn);
                cmd.Parameters.Add("pbb_tipe", textBox2.Text);
                cmd.Parameters.Add("pbb_biaya", textBox3.Text);
                cmd.Parameters.Add("pbb_keterangan", textBox4.Text);
                cmd.Parameters.Add("pbb_id", textBox1.Text);
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
                OracleCommand cmd = new OracleCommand("delete from pbb where pbb_id = :pbb_id", conn);
                cmd.Parameters.Add("pbb_id", textBox1.Text);
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
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["pbb_id"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["pbb_tipe"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["pbb_biaya"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["pbb_keteranga"].Value.ToString();
        }
    }
}
