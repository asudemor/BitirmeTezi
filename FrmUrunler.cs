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
    public partial class FrmUrunler : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmUrunler()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(
                    "Select * From TBL_URUNLER", bgl.baglanti()
                );
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
            TxtAd.Text = "";
            TxtId.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            MskYil.Text = "";
            TxtAdet.Text = "";
            TxtAlis.Text = "";
            TxtSatis.Text = "";
            RchDetay.Text = "";
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Ürün Kaydını Ekleyeceksiniz. Emin Misiniz?", "Ürün Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    //Verileri kaydetme
                    SqlCommand komut = new SqlCommand(
                    "Insert into TBL_URUNLER " +
                    "(URUNAD, MARKA, MODEL, YIL, ADET, ALISFIYAT, SATISFIYAT, DETAY) values" +
                    "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
                    komut.Parameters.AddWithValue("@p3", TxtModel.Text);
                    komut.Parameters.AddWithValue("@p4", MskYil.Text);
                    komut.Parameters.AddWithValue("@p5", TxtAdet.Text);
                    komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
                    komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
                    komut.Parameters.AddWithValue("@p8", RchDetay.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Ürün sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
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
                secim = MessageBox.Show("Ürün Kaydını Sileceksiniz. Emin Misiniz?", "Ürün Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    //Verileri silme
                    SqlCommand komutsil = new SqlCommand(
                        "Delete From TBL_URUNLER where ID=@p1", bgl.baglanti());

                    komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
                    komutsil.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Ürün sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    "Update TBL_URUNLER set " +
                    "URUNAD=@p1, MARKA=@p2, MODEL=@p3, YIL=@p4, ADET=@p5, ALISFIYAT=@p6, SATISFIYAT=@p7, DETAY=@p8 where ID=@p9"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
                komut.Parameters.AddWithValue("@p3", TxtModel.Text);
                komut.Parameters.AddWithValue("@p4", MskYil.Text);
                komut.Parameters.AddWithValue("@p5", TxtAdet.Text);
                komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
                komut.Parameters.AddWithValue("@p8", RchDetay.Text);
                komut.Parameters.AddWithValue("@p9", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Ürün bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            TxtId.Text = dr["ID"].ToString();
            TxtAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYil.Text = dr["YIL"].ToString();
            TxtAdet.Text = dr["ADET"].ToString();
            TxtAlis.Text = dr["ALISFIYAT"].ToString();
            TxtSatis.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();
        }
    }
}
