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

namespace Ticari_Otomasyon
{
    public partial class FrmFaturalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmFaturalar()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FATURABILGI", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            MskVergiDairesi.Text = "";
            TxtAlici.Text = "";
            TxtTeslimEden.Text = "";
            TxtTeslimAlan.Text = "";
            TxtUrunAd.Text = "";
            TxtMiktar.Text = "";
            TxtFiyat.Text = "";
            TxtTutar.Text = "";
            TxtPersonel.Text = "";
            TxtCari.Text = "";
            TxtUrunID.Text = "";
            TxtFaturaID.Text = "";
            CmbCari.SelectedIndex = 0;
        }

        void personelBos()
        {
            try
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURABILGI " +
                           "(SERI,SIRANO,CARITURU,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values" +
                           "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@p3", CmbCari.Text);
                komut.Parameters.AddWithValue("@p4", MskTarih.Text);
                komut.Parameters.AddWithValue("@p5", MskSaat.Text);
                komut.Parameters.AddWithValue("@p6", MskVergiDairesi.Text);
                komut.Parameters.AddWithValue("@p7", TxtAlici.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p9", TxtTeslimAlan.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        void personelDoluFirma()
        {
            try
            {
                double MIKTAR, FIYAT, TUTAR;
                FIYAT = Convert.ToDouble(TxtFiyat.Text);
                MIKTAR = Convert.ToDouble(TxtMiktar.Text);
                TUTAR = FIYAT * MIKTAR;
                TxtTutar.Text = TUTAR.ToString();

                SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURADETAY " +
                   "(URUNAD, MIKTAR, FIYAT, TUTAR, FATURAID) values" +
                   "(@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut.Parameters.AddWithValue("@p5", TxtFaturaID.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut2 = new SqlCommand("INSERT INTO TBL_FIRMAHAREKETLER " +
                    "(URUNID, ADET, PERSONEL, FIRMA, FIYAT, TOPLAM, FATURAID, TARIH) VALUES " +
                    "(@h1, @h2, @h3, @h4, @h5, @h6, @h7, @h8)",bgl.baglanti());

                komut2.Parameters.AddWithValue("@h1", TxtUrunID.Text);
                komut2.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut2.Parameters.AddWithValue("@h4", TxtCari.Text);
                komut2.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@h7", TxtFaturaID.Text);
                komut2.Parameters.AddWithValue("@h8", MskTarih.Text);

                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
            
                //stoktan urun azaltma
                SqlCommand komut3 = new SqlCommand("UPDATE TBL_URUNLER SET ADET=ADET-@a1 WHERE ID=@a2", bgl.baglanti());

                komut3.Parameters.AddWithValue("@a1", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@a2", TxtUrunID.Text);

                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void personelDoluMusteri()
        {
            try
            {
                double MIKTAR, FIYAT, TUTAR;
                FIYAT = Convert.ToDouble(TxtFiyat.Text);
                MIKTAR = Convert.ToDouble(TxtMiktar.Text);
                TUTAR = FIYAT * MIKTAR;
                TxtTutar.Text = TUTAR.ToString();

                SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURADETAY " +
                   "(URUNAD, MIKTAR, FIYAT, TUTAR, FATURAID) values" +
                   "(@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut.Parameters.AddWithValue("@p5", TxtFaturaID.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
            
                //Musteri hareket tablosuna veri giris

                SqlCommand komut2 = new SqlCommand("INSERT INTO TBL_MUSTERIHAREKETLER " +
                    "(URUNID, ADET, PERSONEL, MUSTERI, FIYAT, TOPLAM, FATURAID, TARIH) VALUES " +
                    "(@h1, @h2, @h3, @h4, @h5, @h6, @h7, @h8)", bgl.baglanti());

                komut2.Parameters.AddWithValue("@h1", TxtUrunID.Text);
                komut2.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut2.Parameters.AddWithValue("@h4", TxtCari.Text);
                komut2.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@h7", TxtFaturaID.Text);
                komut2.Parameters.AddWithValue("@h8", MskTarih.Text);

                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
            
                //stoktan urun azaltma
                SqlCommand komut3 = new SqlCommand("UPDATE TBL_URUNLER SET ADET=ADET-@a1 WHERE ID=@a2", bgl.baglanti());

                komut3.Parameters.AddWithValue("@a1", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@a2", TxtUrunID.Text);

                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            CmbCari.Items.Insert(0, "SEÇİM YAPIN");
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Fatura Kaydı Ekleyeceksiniz. Emin Misiniz?", "Fatura Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    if (TxtFaturaID.Text == "")
                    {
                        personelBos();

                        MessageBox.Show("Faturaya ait ürün sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }

                    if (TxtFaturaID.Text != "" && CmbCari.Text == "FIRMA")
                    {
                        personelDoluFirma();

                        MessageBox.Show("Faturaya ait ürün sisteme eklendi.", "FİRMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }

                    if (TxtFaturaID.Text != "" && CmbCari.Text == "MUSTERI")
                    {
                        personelDoluMusteri();

                        MessageBox.Show("Faturaya ait ürün sisteme eklendi.", "MÜŞTERİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }

                    if (TxtFaturaID.Text != "" && CmbCari.Text == "")
                    {
                        MessageBox.Show("Lütfen 'CARİ TÜRÜ' kısmını boş bırakmayın!", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                    }
                }
            }
            
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Fatura Kaydını Sileceksiniz. Emin Misiniz?", "Fatura Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Fatura sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    listele();
                    temizle();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                //Verileri guncelleme
                SqlCommand komut = new SqlCommand(
                    "Update TBL_FATURABILGI set " +
                    "SERI=@p1, SIRANO=@p2, TARIH=@p3, SAAT=@p4, VERGIDAIRE=@p5, ALICI=@p6, TESLIMEDEN=@p7, TESLIMALAN=@p8 " +
                    "where FATURABILGIID=@p9"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", MskVergiDairesi.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
                komut.Parameters.AddWithValue("@p9", TxtFaturaID.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Fatura bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIcerikSil_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnIcerikSil2_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["FATURABILGIID"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtSiraNo.Text = dr["SIRANO"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                MskVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                TxtFaturaID.Text = dr["FATURABILGIID"].ToString();
                CmbCari.Text = dr["CARITURU"].ToString();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT URUNAD, SATISFIYAT FROM TBL_URUNLER WHERE ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtUrunID.Text);

                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    TxtUrunAd.Text = dr[0].ToString();
                    TxtFiyat.Text = dr[1].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Girilen ID ye ait ürün bulunamadı. ID kontrolü yapıp tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTutar_Click(object sender, EventArgs e)
        {
            double MIKTAR, FIYAT, TUTAR;
            FIYAT = Convert.ToDouble(TxtFiyat.Text);
            MIKTAR = Convert.ToDouble(TxtMiktar.Text);
            TUTAR = Convert.ToDouble(FIYAT * MIKTAR);
            TxtTutar.Text = TUTAR.ToString();
        }
    }
}
