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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER",sql.Connection());
            da.Fill(dt);
            gridControl1.DataSource=dt;
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT sehir FROM TBL_ILLER",sql.Connection());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
                vergiil.Properties.Items.Add(dr[0]);
            }
            sql.Connection().Close();
        }
        void Temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            mskTC.Text = "";
            TxtMail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchAdres.Text = "";
            vergiil.Text = "";
        }
        
        SqlBaglanti sql = new SqlBaglanti();
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
            sehirListesi();

            //ComboBox ın text yazılabilmesini engelledim sadece veri seçilebilecek.
            cmbil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbilce.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            vergiil.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            Txtid.Properties.NullValuePrompt = "Silme işlemi için !"; // Placeholder olarak kullanılacak metin
            Txtid.Properties.NullValuePromptShowForEmptyValue = true;
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cmbil den seçilen indexe göre ilçe göstereceğiz o yüzden cmb ilden seçilen il değiştikçe ona göre ilçeler gelecek.
            cmbilce.Properties.Items.Clear();
            cmbilce.Text = "";
            SqlCommand komut = new SqlCommand("SELECT ilce FROM TBL_ILCELER WHERE sehir=@p1", sql.Connection());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex+1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            sql.Connection().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIREIL) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", sql.Connection());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p4", maskedTextBox2.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", cmbil.Text);
            komut.Parameters.AddWithValue("@p8", cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", rchAdres.Text);
            komut.Parameters.AddWithValue("@p10", vergiil.Text);

            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri sisteme başarılı şekilde eklendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // pointer ile seçtiğim satırdaki değerleri dr ye ata
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                maskedTextBox1.Text = dr["TELEFON"].ToString();
                maskedTextBox2.Text = dr["TELEFON2"].ToString();
                mskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();
                vergiil.Text = dr["VERGIDAIREIL"].ToString();
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?", "Kaydı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("DELETE FROM TBL_MUSTERILER WHERE ID=@p1", sql.Connection());
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarıyla silindi.", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            sql.Connection().Close();
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_MUSTERILER SET AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,ADRES=@P9,VERGIDAIREIL=@P10 WHERE ID=@P11", sql.Connection());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@P4", maskedTextBox2.Text);
            komut.Parameters.AddWithValue("@P5", mskTC.Text);
            komut.Parameters.AddWithValue("@P6", TxtMail.Text);
            komut.Parameters.AddWithValue("@P7", cmbil.Text);
            komut.Parameters.AddWithValue("@P8", cmbilce.Text);
            komut.Parameters.AddWithValue("@P9", rchAdres.Text);
            komut.Parameters.AddWithValue("@P10", vergiil.Text);
            komut.Parameters.AddWithValue("@P11", Txtid.Text);

            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri bilgiler başarılı şekilde güncellendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        
    }
}
