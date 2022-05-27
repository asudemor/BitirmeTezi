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
    public partial class FrmMusteriler : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmMusteriler()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(
                    "Select * From TBL_MUSTERILER", bgl.baglanti()
                );
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            RchAdres.Text = "";
            TxtVergi.Text = "";
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
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
                secim = MessageBox.Show("Müşteri Kaydı Ekleyeceksiniz. Emin Misiniz?", "Müşteri Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand(
                    "Insert into TBL_MUSTERILER" +
                    "(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values" +
                    "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                    komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
                    komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
                    komut.Parameters.AddWithValue("@p5", MskTC.Text);
                    komut.Parameters.AddWithValue("@p6", TxtMail.Text);
                    komut.Parameters.AddWithValue("@p7", CmbIl.Text);
                    komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
                    komut.Parameters.AddWithValue("@p9", RchAdres.Text);
                    komut.Parameters.AddWithValue("@p10", TxtVergi.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Müşteri sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Müşteri Kaydını Sileceksiniz. Emin Misiniz?", "Müşteri Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_MUSTERILER where ID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Müşteri sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    listele();
                    temizle();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                //Verileri guncelleme
                SqlCommand komut = new SqlCommand(
                    "Update TBL_MUSTERILER set " +
                    "AD=@p1, SOYAD=@p2, TELEFON=@p3, TELEFON2=@p4, TC=@p5, MAIL=@p6, IL=@p7, ILCE=@p8, ADRES=@p9, VERGIDAIRE=@p10 where ID=@p11"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
                komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
                komut.Parameters.AddWithValue("@p5", MskTC.Text);
                komut.Parameters.AddWithValue("@p6", TxtMail.Text);
                komut.Parameters.AddWithValue("@p7", CmbIl.Text);
                komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
                komut.Parameters.AddWithValue("@p9", RchAdres.Text);
                komut.Parameters.AddWithValue("@p10", TxtVergi.Text);
                komut.Parameters.AddWithValue("@p11", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Müşteri bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIcerikSil_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
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
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Mail Gönderme", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }
    }
}
