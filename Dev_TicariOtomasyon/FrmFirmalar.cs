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
        void Temizle()
        {
            txtAd.Text = "";
            txtYetkili.Text = "";
            txtYGorev.Text = "";
            txtYTC.Text = "";
            txtTel1.Text = "";
            txtTel2.Text = "";
            txtTel3.Text = "";
            txtSektor.Text = "";
            txtOzlKod1.Text = "";
            txtOzlKod2.Text = "";
            txtOzlKod3.Text = "";
            txtMail.Text = "";
            txtId.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            vergiil.Text = "";
            rchAdres.Text = "";
            txtAd.Focus();
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT sehir FROM TBL_ILLER", sql.Connection());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
                vergiil.Properties.Items.Add(dr[0]);
            }
            sql.Connection().Close();
        }

        void carikodAciklama()
        {
            SqlCommand komut = new SqlCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR", sql.Connection());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rchOzlKod1.Text = dr[0].ToString();
            }
            sql.Connection().Close();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
            sehirListesi();
            carikodAciklama();


            //ComboBox ın text yazılabilmesini engelledim sadece veri seçilebilecek.
            cmbil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbilce.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            vergiil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            txtId.Properties.NullValuePrompt = "Silme işlemi için !"; // Placeholder olarak kullanılacak metin
            txtId.Properties.NullValuePromptShowForEmptyValue = true;
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

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FIRMALAR (AD,YETKILIGOREV,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,IL,ILCE,VERGIDAIREIL,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)", sql.Connection());
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtYGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", txtYTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", txtTel1.Text);
            komut.Parameters.AddWithValue("@P7", txtTel2.Text);
            komut.Parameters.AddWithValue("@P8", txtTel3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", cmbil.Text);
            komut.Parameters.AddWithValue("@P11", cmbilce.Text);
            komut.Parameters.AddWithValue("@P12",vergiil.Text);
            komut.Parameters.AddWithValue("@P13", rchAdres.Text);
            komut.Parameters.AddWithValue("@P14", txtOzlKod1.Text);
            komut.Parameters.AddWithValue("@P15", txtOzlKod2.Text);
            komut.Parameters.AddWithValue("@P16", txtOzlKod3.Text);
            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Firma sisteme başarıyla eklendi.", "BİLGİ", MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cmbil den seçilen indexe göre ilçe göstereceğiz o yüzden cmb ilden seçilen il değiştikçe ona göre ilçeler gelecek.
            cmbilce.Properties.Items.Clear();
            cmbilce.Text = "";
            SqlCommand komut = new SqlCommand("SELECT ilce FROM TBL_ILCELER WHERE sehir=@p1", sql.Connection());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            sql.Connection().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?", "Kaydı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("DELETE FROM TBL_FIRMALAR WHERE ID=@P1", sql.Connection());
                komut.Parameters.AddWithValue("@P1", txtId.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarıyla silindi.", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            sql.Connection().Close();
            Listele();
            Temizle();
        }
    }
}
