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
    public partial class MenuMaster : Form
    {
        private OracleConnection conn;
        public MenuMaster(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasterWarga w = new MasterWarga(conn);
            w.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MasterHarta h = new MasterHarta(conn);
            h.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MasterKendaraan k = new MasterKendaraan(conn);
            k.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MasterPBB p = new MasterPBB(conn);
            p.ShowDialog();
        }
    }
}
