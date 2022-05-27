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
    public partial class FrmRehber : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmRehber()
        {
            InitializeComponent();
        }

        void listele()
        {
            try
            {
                //musteri bilgileri
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select AD, SOYAD, TELEFON, TELEFON2, MAIL From TBL_MUSTERILER", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;

                //firma bilgileri
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("Select AD, YETKILIADSOYAD, TELEFON1, TELEFON2, TELEFON3, MAIL, FAX From TBL_FIRMALAR", bgl.baglanti());
                da2.Fill(dt2);
                gridControl2.DataSource = dt2;

                //PERSONELbilgileri
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter("Select AD, SOYAD, TELEFON, MAIL FROM TBL_PERSONELLER", bgl.baglanti());
                da3.Fill(dt3);
                gridControl3.DataSource = dt3;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmMail frm = new FrmMail();
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                if(dr != null)
                {
                    frm.mail = dr["MAIL"].ToString();
                }
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmMail frm = new FrmMail();
                DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);

                if (dr != null)
                {
                    frm.mail = dr["MAIL"].ToString();
                }
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmMail frm = new FrmMail();
                DataRow dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);

                if (dr != null)
                {
                    frm.mail = dr["MAIL"].ToString();
                }
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
