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
    public partial class MasterHarta : Form
    {

        private OracleConnection conn;
        OracleCommand cmd;
        OracleDataAdapter oda;
        public MasterHarta(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void loadGrid()
        {
            try
            {
                cmd = new OracleCommand("select * from harta", conn);
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
                        .Max(r => Convert.ToInt32(r.Cells["h_id"].Value));
            ctr = MaxID + 1;
            return ctr;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["H_ID"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["H_STATUS"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["H_BIAYA"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["H_KETERANGAN"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("insert into harta values (:h_id, :h_status, :h_biaya, :h_keterangan)", conn);
                int h_id = autogen();
                cmd.Parameters.Add("h_id", h_id);
                cmd.Parameters.Add("h_status", textBox2.Text);
                cmd.Parameters.Add("h_biaya", textBox3.Text);
                cmd.Parameters.Add("h_keterangan", textBox4.Text);
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
                OracleCommand cmd = new OracleCommand("update harta set h_status = :h_status, h_biaya = :h_biaya, h_keterangan = :h_keterangan WHERE h_id = :h_id", conn);
                cmd.Parameters.Add("h_status", textBox2.Text);
                cmd.Parameters.Add("h_biaya", textBox3.Text);
                cmd.Parameters.Add("h_keterangan", textBox4.Text);
                cmd.Parameters.Add("h_id", textBox1.Text);
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
                OracleCommand cmd = new OracleCommand("delete from harta where h_id = :h_id", conn);
                cmd.Parameters.Add("h_id", textBox1.Text);
                cmd.ExecuteNonQuery();

                loadGrid();
                clear();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MasterHarta_Load(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
