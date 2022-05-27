using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public string mail;

        public FrmMail()
        {
            InitializeComponent();
        }

        private void FrmMail_Load(object sender, EventArgs e)
        {
            try
            {
                TxtMailAdres.Text = mail;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void MailGonder()
        {
            MailMessage mesajim = new MailMessage();
            mesajim.From = new MailAddress("bitirmetezi2022@hotmail.com","Asude MOR");
            mesajim.To.Add(TxtMailAdres.Text.ToString());
            mesajim.Subject = TxtKonu.Text.ToString();
            mesajim.IsBodyHtml = true;
            mesajim.Body = RchMesaj.Text;

            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new NetworkCredential("bitirmetezi2022@hotmail.com", "TicariOtomasyon2022");
            istemci.EnableSsl = true;
            istemci.Port = 587;           
            istemci.Host = "smtp.live.com"; //microsoft hesabı icin outlook,hotmail
            
            //istemci.Host = "smtp.gmail.com";
            //istemci.Timeout = 50000;

            istemci.Send(mesajim);
            MessageBox.Show("Mail Gönderildi.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = new DialogResult();
                secim = MessageBox.Show("Mail Gönderme İşlemini Onaylıyor Musunuz?", "Mail Gönderme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (secim == DialogResult.Yes)
                {
                    MailGonder();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
