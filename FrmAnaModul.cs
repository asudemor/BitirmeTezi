using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaModul : Form
    {
        FrmUrunler fr;
        FrmMusteriler fr2;
        FrmFirmalar fr3;
        FrmPersonel fr4;
        FrmRehber fr5;
        FrmGiderler fr6;
        FrmBankalar fr7;
        FrmFaturalar fr8;
        FrmNotlar fr9;
        FrmHareketler fr10;
        FrmRaporlar fr11;
        FrmStoklar fr12;
        FrmAyarlar fr13;
        FrmKasa fr14;
        FrmAnaSayfa fr15;
        FrmIstatistik fr16;

        public string kullaniciAd;

        public FrmAnaModul()
        {
            InitializeComponent();
        }

        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr == null || fr.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr = new FrmUrunler();
                fr.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr.Show();
            }
        }

        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null || fr2.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr2 = new FrmMusteriler();
                fr2.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr2.Show();
            }
        }

        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3 == null || fr3.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr3 = new FrmFirmalar();
                fr3.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr3.Show();
            }
        }

        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4 == null || fr4.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr4 = new FrmPersonel();
                fr4.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr4.Show();
            }
        }

        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5 == null || fr5.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr5 = new FrmRehber();
                fr5.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr5.Show();
            }
        }

        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr6 == null || fr6.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr6 = new FrmGiderler();
                fr6.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr6.Show();
            }
        }

        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7 == null || fr7.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr7 = new FrmBankalar();
                fr7.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr7.Show();
            }
        }

        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr8 == null || fr8.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr8 = new FrmFaturalar();
                fr8.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr8.Show();
            }
        }

        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr9 == null || fr9.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr9 = new FrmNotlar();
                fr9.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr9.Show();
            }
        }

        private void BtnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr10 == null || fr10.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr10 = new FrmHareketler();
                fr10.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr10.Show();
            }
        }

        private void BtnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr11 == null || fr11.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr11 = new FrmRaporlar();
                fr11.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr11.Show();
            }
        }

        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr12 == null || fr12.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr12 = new FrmStoklar();
                fr12.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr12.Show();
            }
        }

        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr13 == null || fr13.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr13 = new FrmAyarlar();
                fr13.Show();
            }
        }

        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr14 == null || fr14.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr14 = new FrmKasa();
                fr14.aktifKullaniciAd = kullaniciAd;
                fr14.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr14.Show();
            }
        }

        private void BtnAnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr15 == null || fr15.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr15 = new FrmAnaSayfa();
                fr15.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr15.Show();
            }
        }

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            if (fr15 == null || fr15.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr15 = new FrmAnaSayfa();
                fr15.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr15.Show();
            }
        }

        private void BtnIstatik_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr16 == null || fr16.IsDisposed) //sekmenin birden fazla acilmasini engeller.
            {
                fr16 = new FrmIstatistik();
                fr16.MdiParent = this; //uzerinde calisildigi alanda ac.
                fr16.Show();
            }
        }

        private void BtnCikis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
