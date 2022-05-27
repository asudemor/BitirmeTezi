using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD, SUM(ADET) AS 'STOK KALAN' FROM TBL_URUNLER " +
                    "GROUP BY URUNAD HAVING SUM(ADET) <= 20 ORDER BY SUM(ADET)", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ajanda()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 TARIH, SAAT, BASLIK FROM TBL_NOTLAR " +
                    "ORDER BY TARIH DESC", bgl.baglanti());
                da.Fill(dt);
                gridControl3.DataSource = dt;
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
                SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketlerAnaSayfa", bgl.baglanti());      //veritabanında procedure olusturuldu.
                da.Fill(dt);
                gridControl2.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmaRehber()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT AD, TELEFON1, TELEFON2 FROM TBL_FIRMALAR", bgl.baglanti());   
                da.Fill(dt);
                gridControl4.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dovizKuru()
        {
            try
            {
                webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void haberler()
        {
            try
            {
                XmlTextReader xmlOku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");

                while (xmlOku.Read())
                {
                    if (xmlOku.Name == "title")
                    {
                        listBox1.Items.Add(xmlOku.ReadString());
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*void youtube()
        {
            webBrowser3.Navigate("http://www.youtube.com");
        }*/

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            listeleFirma();
            firmaRehber();
            dovizKuru();
            haberler();
            //youtube();
        }
    }
}
