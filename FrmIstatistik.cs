using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Charts;
using System.Data.SqlClient;


namespace Ticari_Otomasyon
{
    public partial class FrmIstatistik : Form
    {
        public FrmIstatistik()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void PersonelSatisMusteri()
        {
            try
            {
                SqlCommand komut = new SqlCommand("EXECUTE PersonelSatisMusteri", bgl.baglanti());
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

        void PersonelSatisFirma()
        {
            try
            {
                SqlCommand komut = new SqlCommand("EXECUTE PersonelSatisFirma", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
                    chartControl2.Series[0].LegendTextPattern = "{A}: {VP: P2} ({V: F1})";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void giderler()
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT ıl, count(Il) FROM tbl_personeller GROUP BY ıl", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl3.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
                    chartControl3.Series[0].LegendTextPattern = "{A}: {VP: P2} ({V: F1})";
                }
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ocakSatisFirma()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT FATURAID, TARIH FROM TBL_FIRMAHAREKETLER WHERE TARIH BETWEEN '01/01/2022' AND '31/01/2022' order by TARIH asc", bgl.baglanti());
                da.Fill(dt);
                gridControl3.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ocakSatisMusteri()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT FATURAID, TARIH FROM TBL_MUSTERIHAREKETLER WHERE TARIH BETWEEN '01/01/2022' AND '31/01/2022' order by TARIH asc", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmIstatistik_Load(object sender, EventArgs e)
        {
            PersonelSatisMusteri();
            PersonelSatisFirma();
            giderler();
            ocakSatisFirma();
            ocakSatisMusteri();
        }
    }
}
