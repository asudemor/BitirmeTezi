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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD, SUM(ADET) AS 'MIKTAR' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());      
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
        void listeleStokEksi()
        {
            try
            {
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT URUNAD, SUM(ADET)<=0 AS 'MIKTAR' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
                da2.Fill(dt2);
                gridControl2.DataSource = dt2;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
        void firmaEkle()
        {
            try
            {
                //CHARTA FIRMALARI CEKME
                SqlCommand komut2 = new SqlCommand("SELECT urunad, COUNT(*) FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
                    chartControl2.Series[0].LegendTextPattern = "{A}: {VP: P2} ({V: F1})";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void urunEkle()
        {
            try
            {
                //CHARTA URUNLERI CEKME
                SqlCommand komut = new SqlCommand("SELECT URUNAD, SUM(ADET) AS 'MIKTAR' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
                    chartControl1.Series[0].LegendTextPattern = "{A}: {VP: P2} ({V: F1})";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            listele();
            firmaEkle();
            urunEkle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.ad = dr["URUNAD"].ToString();
            }
            fr.Show();
        }
    }
}
