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
    public partial class FormTransaksi : Form
    {
        private string nik;
        private OracleConnection conn;
        public string id_k, id_h, id_p, id_htrans;

        public FormTransaksi(OracleConnection conn, string nik)
        {
            InitializeComponent();
            this.nik = nik;
            this.conn = conn;
        }

        public void loadData()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM WARGA WHERE NIK = :NIK", conn);
                cmd.Parameters.Add(":NIK", nik);
                OracleDataReader warga = cmd.ExecuteReader();
                while(warga.Read())
                {
                    textBox1.Text = warga.GetValue(0).ToString();//nik
                    textBox2.Text = warga.GetValue(1).ToString();//nama
                    textBox3.Text = warga.GetValue(2).ToString();//alamat
                    dateTimePicker1.Value = Convert.ToDateTime(warga.GetValue(3).ToString());//tgl
                    if (warga.GetValue(4).ToString() == "1")
                    {
                        radioButton1.Checked = true;//laki
                        radioButton2.Enabled = false;
                    }
                    else
                    {
                        radioButton2.Checked = true;//perempuan
                        radioButton1.Enabled = false;
                    }
                    textBox4.Text = warga.GetValue(5).ToString();//telp
                }


                OracleCommand harta = new OracleCommand("SELECT WH.ID_WH, H.H_BIAYA FROM WH_MEMILIKI WH, HARTA H WHERE WH.H_ID = H.H_ID AND WH.NIK = :NIK", conn);
                harta.Parameters.Add(":NIK", nik);
                OracleDataReader h = harta.ExecuteReader();
                while(h.Read())
                {
                    id_h = h.GetValue(0).ToString();
                    label6.Text = h.GetValue(1).ToString();//hrg harta
                }

                OracleCommand kendaraan = new OracleCommand("SELECT WK.ID_WK, K.K_BIAYA FROM WK_MEMILIKI WK, KENDARAAN K WHERE WK.K_ID = K.K_ID AND WK.NIK = :NIK", conn);
                kendaraan.Parameters.Add(":NIK", nik);
                OracleDataReader k = kendaraan.ExecuteReader();
                while (k.Read())
                {
                    id_k = k.GetValue(0).ToString();
                    label7.Text = k.GetValue(1).ToString();//hrg kendaraan 
                }

                OracleCommand pbb = new OracleCommand("SELECT WP.ID_WP, P.PBB_BIAYA FROM WP_MEMILIKI WP, PBB P WHERE WP.PBB_ID = P.PBB_ID AND WP.NIK = :NIK", conn);
                pbb.Parameters.Add(":NIK", nik);
                OracleDataReader p = pbb.ExecuteReader();
                while (p.Read())
                {
                    id_p = p.GetValue(0).ToString();
                    label8.Text = p.GetValue(1).ToString();//hrg pbb
                }
            }
            catch(OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable getHtrans()
        {
            string query = "SELECT * FROM H_TRANS";
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter adap = new OracleDataAdapter(query, conn);
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            label6.Text = "0";
            label7.Text = "0";
            label8.Text = "0";
        }

        private void generate()
        {
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "generateHTrans";
            OracleParameter p1 = new OracleParameter();
            p1.ParameterName = "kode_akhir";
            p1.OracleDbType = OracleDbType.Varchar2;
            p1.Size = 20;
            p1.Direction = ParameterDirection.ReturnValue;
            cmd1.Parameters.Add(p1);
            cmd1.ExecuteNonQuery();
            label14.Text = p1.Value.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            loadData();
            generate();

            dataGridView1.DataSource = getHtrans();
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO H_TRANS VALUES (:id, :id_wk, :id_wp, :id_wh, sysdate, :total, '-')", conn);
                cmd.Parameters.Add(":id", label14.Text);
                cmd.Parameters.Add(":id_wk", id_k);
                cmd.Parameters.Add(":id_wp", id_p);
                cmd.Parameters.Add(":id_wh", id_h);
                int harta = Convert.ToInt32(label6.Text);
                int kendaraan = Convert.ToInt32(label7.Text);
                int pbb = Convert.ToInt32(label8.Text);
                int total = harta + kendaraan + pbb;
                cmd.Parameters.Add(":total", total);

                cmd.ExecuteNonQuery();
                dataGridView1.DataSource = getHtrans();
                clear();
            }
            catch(OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
