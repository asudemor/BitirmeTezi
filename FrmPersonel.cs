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
    public partial class FrmPersonel : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmPersonel()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(
                    "Select * From TBL_PERSONELLER", bgl.baglanti()
                );
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SehirListesi()
        {
            try
            {
                SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    CmbIl.Items.Add(dr[0]);
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTelefon1.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            RchAdres.Text = "";
            TxtGorev.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            listele();
            SehirListesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Personel Kaydını Ekleyeceksiniz. Emin Misiniz?", "Personel Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Insert into TBL_PERSONELLER " +
                   "(AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values" +
                   "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                    komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
                    komut.Parameters.AddWithValue("@p4", MskTC.Text);
                    komut.Parameters.AddWithValue("@p5", TxtMail.Text);
                    komut.Parameters.AddWithValue("@p6", CmbIl.Text);
                    komut.Parameters.AddWithValue("@p7", CmbIlce.Text);
                    komut.Parameters.AddWithValue("@p8", RchAdres.Text);
                    komut.Parameters.AddWithValue("@p9", TxtGorev.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Personel sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Personel Kaydını Sileceksiniz. Emin Misiniz?", "Personel Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_PERSONELLER where ID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Personel sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    "Update TBL_PERSONELLER set " +
                    "AD=@p1, SOYAD=@p2, TELEFON=@p3, TC=@p4, MAIL=@p5, IL=@p6, ILCE=@p7, ADRES=@p8, GOREV=@p9 where ID=@p10"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
                komut.Parameters.AddWithValue("@p4", MskTC.Text);
                komut.Parameters.AddWithValue("@p5", TxtMail.Text);
                komut.Parameters.AddWithValue("@p6", CmbIl.Text);
                komut.Parameters.AddWithValue("@p7", CmbIlce.Text);
                komut.Parameters.AddWithValue("@p8", RchAdres.Text);
                komut.Parameters.AddWithValue("@p9", TxtGorev.Text);
                komut.Parameters.AddWithValue("@p10", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Personel bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIcerikSil_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void CmbIl_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                CmbIlce.Items.Clear();

                SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER where SEHIR = @p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    CmbIlce.Items.Add(dr[0]);
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }
    }
}
