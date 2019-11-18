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
    public partial class MenuAdministrasi : Form
    {
        private OracleConnection conn;
        public MenuAdministrasi(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSearch src = new FormSearch(conn);
            src.ShowDialog();
        }
    }
}
