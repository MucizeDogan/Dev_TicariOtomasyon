using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dev_TicariOtomasyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        SqlBaglanti sql = new SqlBaglanti();

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR",sql.Connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
