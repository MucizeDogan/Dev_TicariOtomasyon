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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        SqlBaglanti sql = new SqlBaglanti();
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_URUNLER",sql.Connection());
            da.Fill(dt);    //DataAdapter ın içini dataTable ile doldur.
            gridControl1.DataSource = dt;
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void BtnKaydetUrun_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_URUNLER(KATEGORIAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",sql.Connection());
           

            
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@p3", TxtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NmrAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            
                komut.ExecuteNonQuery();    // DML komutlarını gerçekleştiron kod.(Sorguyu çalıştır.)
            sql.Connection().Close();
            MessageBox.Show("Ürün sisteme başarılı şekilde eklendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            
          

            TxtAd.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            MskYil.Text = "";
            NmrAdet.Value = 0;
            TxtAlisfiyat.Text = "";
            TxtSatisFiyat.Text = "";
            RchDetay.Text = "";



        }
    }
}
