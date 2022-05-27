using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string aktifKullaniciAd;

        void musteriharekeret()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());      //veritabanında procedure olusturuldu.
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmaharekeret()
        {
            try
            {
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());      //veritabanında procedure olusturuldu.
                da2.Fill(dt2);
                gridControl3.DataSource = dt2;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void toplamTutar()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT SUM(TUTAR) FROM TBL_FATURADETAY", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblKasaToplam.Text = dr[0].ToString() + " ₺";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ayHarcamalar()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA), AY FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblOdemeler.Text = dr[0].ToString() + " ₺";
                    LblAy.Text = dr[1].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void personelMaas()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT MAASLAR AY FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblPersonelMaaslari.Text = dr[0].ToString() + " ₺";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void musteriSayisi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblMusteriSayisi.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmaSayisi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBL_FIRMALAR", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblFirmaSayisi.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmaSehirSayisi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblFirmaSehir.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void musteriSehirSayisi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_MUSTERILER", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblMusteriSehir.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void personelSayisi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblPersonelSayisi.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void stokSayisi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT SUM(ADET) FROM TBL_URUNLER", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    LblStokSayisi.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void elektrikFaturasi4()
        {
            try
            {
                //chart1 controle elektrik faturası son 4 ay listeleme
                SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC ", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
                    LblChart1Baslik.Text = "SON 4 AYIN ELEKTRIK FATURASI";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void suFaturasi4()
        {
            try
            {
                //chart2 controle su faturası son 4 ay listeleme
                SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, SU FROM TBL_GIDERLER ORDER BY ID DESC ", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
                    LblChart2Baslik.Text = "SON 4 AYIN SU FATURASI";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dogalgazFaturasi4()
        {
            try
            {
                //chart3 controle doğalgaz faturası son 4 ay listeleme
                SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC ", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl3.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
                    LblChart3Baslik.Text = "SON 4 AYIN DOGALGAZ FATURASI";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void internetFaturasi4()
        {
            try
            {
                //chart4 controle internet faturası son 4 ay listeleme
                SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, INTERNET FROM TBL_GIDERLER ORDER BY ID DESC ", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl4.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
                    LblChart4Baslik.Text = "SON 4 AYIN INTERNET FATURASI";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void extraFaturasi4()
        {
            try
            {
                //chart5 controle extra son 4 ay listeleme
                SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC ", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl5.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
                    LblChart5Baslik.Text = "SON 4 AYIN EKSTRALARI";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void maaslar4()
        {
            try
            {
                //chart6 controle maaslar son 4 ay listeleme
                SqlCommand komut = new SqlCommand("SELECT TOP 4 AY, MAASLAR FROM TBL_GIDERLER ORDER BY ID DESC ", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl6.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
                    LblChart6Baslik.Text = "SON 4 AYIN MAAŞ ÖDEMELERİ";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = aktifKullaniciAd;
            musteriharekeret();
            firmaharekeret();
            toplamTutar();
            ayHarcamalar();
            personelMaas();
            musteriSayisi();
            firmaSayisi();
            firmaSehirSayisi();
            musteriSehirSayisi();
            personelSayisi();
            stokSayisi();
            elektrikFaturasi4();
            suFaturasi4();
            dogalgazFaturasi4();
            internetFaturasi4();
            extraFaturasi4();
            maaslar4();
        }
    }
}
