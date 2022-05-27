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
    public partial class FrmGiderler : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmGiderler()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_GIDERLER Order by ID Asc", bgl.baglanti());
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
            CmbAy.Text = "";
            CmbYil.Text = "";
            TxtElektrik.Text = "";
            TxtSu.Text = "";
            TxtDogalgaz.Text = "";
            TxtInternet.Text = "";
            TxtMaaslar.Text = "";
            TxtEkstra.Text = "";
            RchNotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Gider Kaydı Ekleyeceksiniz. Emin Misiniz?", "Gider Kaydı Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Insert into TBL_GIDERLER " +
                   "(AY, YIL, ELEKTRIK, SU, DOGALGAZ, INTERNET, MAASLAR, EKSTRA, NOTLAR) values" +
                   "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", CmbAy.Text);
                    komut.Parameters.AddWithValue("@p2", CmbYil.Text);
                    komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
                    komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
                    komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
                    komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
                    komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
                    komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
                    komut.Parameters.AddWithValue("@p9", RchNotlar.Text);

                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Giderler sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    temizle();
                }
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

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Gider Kaydını Sileceksiniz. Emin Misiniz?", "Gider Kaydı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("Delete From TBL_GIDERLER where ID=@p1", bgl.baglanti());

                    komut.Parameters.AddWithValue("@p1", TxtId.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    MessageBox.Show("Gider sistemden silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    "Update TBL_GIDERLER set " +
                    "AY=@p1, YIL=@p2, ELEKTRIK=@p3, SU=@p4, DOGALGAZ=@p5, INTERNET=@p6, MAASLAR=@p7, EKSTRA=@p8, NOTLAR=@p9 where ID=@p10"
                    , bgl.baglanti());

                komut.Parameters.AddWithValue("@p1", CmbAy.Text);
                komut.Parameters.AddWithValue("@p2", CmbYil.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
                komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
                komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
                komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
                komut.Parameters.AddWithValue("@p10", TxtId.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Gider bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
                temizle();
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
                CmbAy.Text = dr["AY"].ToString();
                CmbYil.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtInternet.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }
    }
}
