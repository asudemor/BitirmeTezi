using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmNotDetay : Form
    {

        public string metin;

        public FrmNotDetay()
        {
            InitializeComponent();
        }

        private void FrmNotDetay_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = metin;
        }
    }
}
