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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public string urunid;
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            try
            {
                TxtUrunID.Text = urunid;

                SqlCommand komut = new SqlCommand("Select * From TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", urunid);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    TxtUrunAd.Text = dr[1].ToString();
                    TxtMiktar.Text = dr[2].ToString();
                    TxtFiyat.Text = dr[3].ToString();
                    TxtTutar.Text = dr[4].ToString();
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
                    "Update TBL_FATURADETAY set " +
                    "URUNAD=@p1, MIKTAR=@p2, FIYAT=@p3, TUTAR=@p4 where FATURAURUNID=@p5"
                    , bgl.baglanti());


                komut.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut.Parameters.AddWithValue("@p5", TxtUrunID.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Değişiklikler güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                secim = MessageBox.Show("Ürün Kaydını Sileceksiniz. Emin Misiniz?", "Ürün Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete from TBL_FATURADETAY where FATURAURUNID=@p1 ", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", TxtUrunID.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Ürün silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
