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
    public partial class FrmNotlar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmNotlar()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(
                    "Select * From TBL_NOTLAR", bgl.baglanti()
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
            TxtId.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtBaslik.Text = "";
            TxtOlusturan.Text = "";
            TxtHitap.Text = "";
            RchDetay.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Not Kaydını Ekleyeceksiniz. Emin Misiniz?", "Not Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Insert into TBL_NOTLAR " +
                    "(TARIH, SAAT, BASLIK, OLUSTURAN, HITAP, DETAY) values" +
                    "(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", MskTarih.Text);
                    komut.Parameters.AddWithValue("@p2", MskSaat.Text);
                    komut.Parameters.AddWithValue("@p3", TxtBaslik.Text);
                    komut.Parameters.AddWithValue("@p4", TxtOlusturan.Text);
                    komut.Parameters.AddWithValue("@p5", TxtHitap.Text);
                    komut.Parameters.AddWithValue("@p6", RchDetay.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Not bilgisi sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                secim = MessageBox.Show("Not Kaydını Sileceksiniz. Emin Misiniz?", "Not Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_NOTLAR where ID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Not sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    "Update TBL_NOTLAR set " +
                    "TARIH=@p1, SAAT=@p2, BASLIK=@p3, OLUSTURAN=@p4, HITAP=@p5, DETAY=@p6 where ID=@p7"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", MskTarih.Text);
                komut.Parameters.AddWithValue("@p2", MskSaat.Text);
                komut.Parameters.AddWithValue("@p3", TxtBaslik.Text);
                komut.Parameters.AddWithValue("@p4", TxtOlusturan.Text);
                komut.Parameters.AddWithValue("@p5", TxtHitap.Text);
                komut.Parameters.AddWithValue("@p6", RchDetay.Text);
                komut.Parameters.AddWithValue("@p7", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Not bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.metin = dr["DETAY"].ToString();
            }
            fr.Show();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtBaslik.Text = dr["BASLIK"].ToString();
                TxtOlusturan.Text = dr["OLUSTURAN"].ToString();
                TxtHitap.Text = dr["HITAP"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
            }
        }
    }
}
