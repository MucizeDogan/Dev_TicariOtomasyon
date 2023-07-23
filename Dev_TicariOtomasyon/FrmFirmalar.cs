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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle); //Seçilen satırın veri değerini dr ye atadık.
            if (dr!=null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtYGorev.Text = dr["YETKILIGOREV"].ToString();
                txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                txtYTC.Text = dr["YETKILITC"].ToString();
                txtSektor.Text = dr["SEKTOR"].ToString();
                txtTel1.Text = dr["TELEFON1"].ToString();
                txtTel2.Text = dr["TELEFON2"].ToString();
                txtTel3.Text = dr["TELEFON3"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                vergiil.Text = dr["VERGIDAIREIL"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                txtOzlKod1.Text = dr["OZELKOD1"].ToString();
                txtOzlKod2.Text = dr["OZELKOD2"].ToString();
                txtOzlKod3.Text = dr["OZELKOD3"].ToString();
            }
        }
    }
}
