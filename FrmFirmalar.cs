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
    public partial class FrmFirmalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmFirmalar()
        {
            InitializeComponent();
        }
        
        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(
                    "Select * From TBL_FIRMALAR", bgl.baglanti()
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
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtMail.Text = "";
            TxtSektor.Text = "";
            TxtVergi.Text = "";
            TxtYetkili.Text = "";
            TxtYetkiliGorev.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            MskYetkiliTC.Text = "";
            RchAdres.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
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

        void carikodaciklamalari()
        {
            try
            {
                SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    RchKod1.Text = dr[0].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            SehirListesi();
            temizle();
            carikodaciklamalari();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Firma Kaydı Ekleyeceksiniz. Emin Misiniz?", "Firma Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Insert into TBL_FIRMALAR " +
                    "(AD, YETKILISTATU, YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON1, TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values " +
                    "(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17)", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komut.Parameters.AddWithValue("@p2", TxtYetkiliGorev.Text);
                    komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
                    komut.Parameters.AddWithValue("@p4", MskYetkiliTC.Text);
                    komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
                    komut.Parameters.AddWithValue("@p6", MskTelefon1.Text);
                    komut.Parameters.AddWithValue("@p7", MskTelefon2.Text);
                    komut.Parameters.AddWithValue("@p8", MskTelefon3.Text);
                    komut.Parameters.AddWithValue("@p9", TxtMail.Text);
                    komut.Parameters.AddWithValue("@p10", MskFax.Text);
                    komut.Parameters.AddWithValue("@p11", CmbIl.Text);
                    komut.Parameters.AddWithValue("@p12", CmbIlce.Text);
                    komut.Parameters.AddWithValue("@p13", TxtVergi.Text);
                    komut.Parameters.AddWithValue("@p14", RchAdres.Text);
                    komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
                    komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
                    komut.Parameters.AddWithValue("@p17", TxtKod3.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Firma sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                secim = MessageBox.Show("Firma Kaydını Sileceksiniz. Emin Misiniz?", "Firma Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_FIRMALAR where ID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Firma sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    listele();
                    temizle();
                }
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
        
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                //Verileri guncelleme
                SqlCommand komut = new SqlCommand(
                    "Update TBL_FIRMALAR set " +
                    "AD=@p1, YETKILISTATU=@p2, YETKILIADSOYAD=@p3, YETKILITC=@p4, SEKTOR=@p5, TELEFON1=@p6, TELEFON2=@p7, TELEFON3=@p8, MAIL=@p9, " +
                    "FAX=@p10, IL=@p11, ILCE=@p12, VERGIDAIRE=@p13, ADRES=@p14, OZELKOD1=@p15, OZELKOD2=@p16, OZELKOD3=@p17  where ID=@p18"
                    , bgl.baglanti());


                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtYetkiliGorev.Text);
                komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
                komut.Parameters.AddWithValue("@p4", MskYetkiliTC.Text);
                komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
                komut.Parameters.AddWithValue("@p6", MskTelefon1.Text);
                komut.Parameters.AddWithValue("@p7", MskTelefon2.Text);
                komut.Parameters.AddWithValue("@p8", MskTelefon3.Text);
                komut.Parameters.AddWithValue("@p9", TxtMail.Text);
                komut.Parameters.AddWithValue("@p10", MskFax.Text);
                komut.Parameters.AddWithValue("@p11", CmbIl.Text);
                komut.Parameters.AddWithValue("@p12", CmbIlce.Text);
                komut.Parameters.AddWithValue("@p13", TxtVergi.Text);
                komut.Parameters.AddWithValue("@p14", RchAdres.Text);
                komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
                komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
                komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
                komut.Parameters.AddWithValue("@p18", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Firma bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                TxtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskYetkiliTC.Text = dr["YETKILITC"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }
    }
}
