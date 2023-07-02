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
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            MskYil.Text = "";
            NmrAdet.Value = 0;
            TxtAlisfiyat.Text = "";
            TxtSatisFiyat.Text = "";
            RchDetay.Text = "";
            
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

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from TBL_URUNLER where ID=@p1",sql.Connection());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Ürün silindi.","BİLGİ",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

       


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            TxtAd.Text = dr["KATEGORIAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYil.Text = dr["YIL"].ToString();
            NmrAdet.Value = int.Parse(dr["ADET"].ToString());
            TxtAlisfiyat.Text = dr["ALISFIYAT"].ToString();
            TxtSatisFiyat.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("update TBL_URUNLER set KATEGORIAD=@P1,MARKA=@P2,MODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYAT=@P6,SATISFIYAT=@P7,DETAY=@P8 where ID=@P9", sql.Connection());

            komutguncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@P2", TxtMarka.Text);
            komutguncelle.Parameters.AddWithValue("@P3", TxtModel.Text);
            komutguncelle.Parameters.AddWithValue("@P4", MskYil.Text);
            komutguncelle.Parameters.AddWithValue("@P5", int.Parse((NmrAdet.Value).ToString()));
            komutguncelle.Parameters.AddWithValue("@P6", decimal.Parse(TxtAlisfiyat.Text));
            komutguncelle.Parameters.AddWithValue("@P7", decimal.Parse(TxtSatisFiyat.Text));
            komutguncelle.Parameters.AddWithValue("@P8", RchDetay.Text);
            komutguncelle.Parameters.Add("@P9", Txtid.Text);

            komutguncelle.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Ürün Başarılı bir şekilde güncellendi.","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            Listele();

        }
    }
}
