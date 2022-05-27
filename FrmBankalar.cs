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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Execute BankaFirmaBilgileri", bgl.baglanti());      //veritabanında procedure olusturuldu.
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
            TxtBankaAdi.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtSube.Text = "";
            MskIBAN.Text = "";
            TxtHesapNo.Text = "";
            TxtYetkili.Text = "";
            MskTelefon.Text = "";
            MskTarih.Text = "";
            TxtHesapTuru.Text = "";
            lookUpEdit1.Text = "";
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

        void firmaListesi()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select ID,AD From TBL_FIRMALAR", bgl.baglanti());
                da.Fill(dt);
                lookUpEdit1.Properties.ValueMember= "ID";
                lookUpEdit1.Properties.DisplayMember = "AD";
                lookUpEdit1.Properties.DataSource= dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            SehirListesi();
            firmaListesi();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Banka Kaydı Ekleyeceksiniz. Emin Misiniz?", "Banka Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Insert into TBL_BANKALAR " +
                    "(BANKAADI, IL,ILCE, SUBE,IBAN, HESAPNO, YETKILI, TELEFON, TARIH, HESAPTURU, FIRMAID) values " +
                    "(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtBankaAdi.Text);
                    komut.Parameters.AddWithValue("@p2", CmbIl.Text);
                    komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
                    komut.Parameters.AddWithValue("@p4", TxtSube.Text);
                    komut.Parameters.AddWithValue("@p5", MskIBAN.Text);
                    komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
                    komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
                    komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
                    komut.Parameters.AddWithValue("@p9", MskTarih.Text);
                    komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
                    komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Banka sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                secim = MessageBox.Show("Banka Kaydını Sileceksiniz. Emin Misiniz?", "Banka Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_BANKALAR where ID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Banka sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    "Update TBL_BANKALAR set " +
                    "BANKAADI=@p1, IL=@p2, ILCE=@p3, SUBE=@p4, IBAN=@p5, HESAPNO=@p6, YETKILI=@p7, TELEFON=@p8, TARIH=@p9, HESAPTURU=@p10, FIRMAID=@p11 where ID=@p12"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", TxtBankaAdi.Text);
                komut.Parameters.AddWithValue("@p2", CmbIl.Text);
                komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
                komut.Parameters.AddWithValue("@p4", TxtSube.Text);
                komut.Parameters.AddWithValue("@p5", MskIBAN.Text);
                komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
                komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
                komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p9", MskTarih.Text);
                komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
                komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
                komut.Parameters.AddWithValue("@p12", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Banka bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                //temizle();
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
            try
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    TxtId.Text = dr["ID"].ToString();
                    TxtBankaAdi.Text = dr["BANKAADI"].ToString();
                    CmbIl.Text = dr["IL"].ToString();
                    CmbIlce.Text = dr["ILCE"].ToString();
                    TxtSube.Text = dr["SUBE"].ToString();
                    MskIBAN.Text = dr["IBAN"].ToString();
                    TxtHesapNo.Text = dr["HESAPNO"].ToString();
                    TxtYetkili.Text = dr["YETKILI"].ToString();
                    MskTelefon.Text = dr["TELEFON"].ToString();
                    MskTarih.Text = dr["TARIH"].ToString();
                    TxtHesapTuru.Text = dr["HESAPTURU"].ToString();
                    //lookUpEdit1.Text = dr["FIRMAID"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
