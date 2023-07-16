using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dev_TicariOtomasyon
{
    public partial class FrmAna : Form
    {
        public FrmAna()
        {
            InitializeComponent();
        }

        FrmUrunler fr;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr==null)
            {
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
            }
            
        }
        FrmMusteriler frmMusteriler;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmMusteriler==null)
            {
                frmMusteriler=new FrmMusteriler();
                frmMusteriler.MdiParent = this;
                frmMusteriler.Show();
            }
        }

        FrmFirmalar frmFirmalar;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmFirmalar==null)
            {
                frmFirmalar = new FrmFirmalar();
                frmFirmalar.MdiParent = this;
                frmFirmalar.Show();
            }
        }
    }
}
