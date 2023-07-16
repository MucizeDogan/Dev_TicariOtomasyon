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
            TxtVergiil.Text = "";
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
            komut.Parameters.AddWithValue("@p10", TxtVergiil.Text);

            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri sisteme başarılı şekilde eklendi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }
    }
}
