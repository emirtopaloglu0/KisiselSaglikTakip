using KisiselSaglikTakip.Mesajlar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KisiselSaglikTakip.Properties;

namespace KisiselSaglikTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            SoruMesaj.instance.CloseApp();
            if (SoruMesaj.instance.res == DialogResult.Yes)
            {
                Close();
            }
        }

        private void Confirm_Button_Click(object sender, EventArgs e)
        {
            SoruMesaj.instance.ConfirmUser();
            if (SoruMesaj.instance.res == DialogResult.Yes)
            {
                Settings.Default.Username = Usernam_Textbox.Text;
                Settings.Default.IsLogged = true;
                Settings.Default.Save();
                UserShow_Label.Text = Settings.Default.Username;
                UserShow_Label.Show();
            }
        }

        private void Logut_Button_Click(object sender, EventArgs e)
        {
            SoruMesaj.instance.Logout();
            if(SoruMesaj.instance.res == DialogResult.Yes)
            {
                Settings.Default.Username = " ";
                Settings.Default.IsLogged = false;
                Settings.Default.Save();
                UserShow_Label.Text = Settings.Default.Username;
                UserShow_Label.Hide();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.IsLogged)
            {
                UserShow_Label.Text = Settings.Default.Username;
            }
            else
            {
                UserShow_Label.Hide();
            }
        }
    }
}
