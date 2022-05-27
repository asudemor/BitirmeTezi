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
    public partial class FrmAdmin : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            BtnLogin.BackColor = Color.Navy;

            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_ADMIN WHERE KULLANICIAD=@p1 and SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.kullaniciAd = TxtKullaniciAd.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı adı veya Şifre Girişi", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void BtnLogin_MouseHover(object sender, EventArgs e)
        {
            BtnLogin.BackColor = Color.Navy;
        }

        private void BtnLogin_MouseLeave(object sender, EventArgs e)
        {
            BtnLogin.BackColor = Color.DarkGray;
        }
    }
}
