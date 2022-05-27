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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listeleMusteri()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());      //veritabanında procedure olusturuldu.
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void listeleFirma()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());      //veritabanında procedure olusturuldu.
                da.Fill(dt);
                gridControl2.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void stoklar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD, ADET FROM TBL_URUNLER ORDER BY ADET ASC", bgl.baglanti());
                da.Fill(dt);
                gridControl3.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            listeleFirma();
            listeleMusteri();
            stoklar();
        }
    }
}
