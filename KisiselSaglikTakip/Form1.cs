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
using Tulpep.NotificationWindow;
using KisiselSaglikTakip.Class;

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
                Settings.Default.Username = Username_Textbox.Text;
                Settings.Default.IsLogged = true;
                Settings.Default.UserAge = Convert.ToInt32(Age_Textbox.Text);

                if (Erkek_Btn.Checked)
                {
                    Settings.Default.UserGender = 'E';
                }
                else
                {
                    Settings.Default.UserGender = 'K';
                }


                Settings.Default.Save();
                Confirm_Button.Enabled = false;
                LoadUserData();

            }
        }

        private void Logut_Button_Click(object sender, EventArgs e)
        {
            SoruMesaj.instance.Logout();
            if (SoruMesaj.instance.res == DialogResult.Yes)
            {
                Settings.Default.Username = " ";
                Settings.Default.IsLogged = false;
                Settings.Default.UserGender = 'E';
                Settings.Default.UserAge = 0;
                Settings.Default.Save();
                UserShow_Label.Text = Settings.Default.Username;
                UserShow_Label.Hide();
                Settings.Default.Yapilacaklar.Clear();
                LoadUserData();
                LoadRaporlarList();
                LoadYapilacaklarList();
                Confirm_Button.Enabled = true;
            }

        }

        private void LoadUserData()
        {
            if (Settings.Default.IsLogged)
            {
                UserShow_Label.Text = Settings.Default.Username;
                Username_Textbox.Text = Settings.Default.Username;
                UserShow_Label.Show();
                Age_Textbox.Text = Settings.Default.UserAge.ToString();

                if (Settings.Default.UserGender == 'E')
                {
                    Erkek_Btn.Checked = true;
                }
                else
                {
                    Kadin_Btn.Checked = false;
                }

            }
            else
            {
                UserShow_Label.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmr.Interval = 500;
            tmr.Tick += Timer_Tick;
            if (Settings.Default.IsLogged)
            {
                LoadUserData();
                LoadRaporlarList();
                LoadYapilacaklarList();
                Confirm_Button.Enabled = false;

            }
            else
            {
                HataMesaj.GirisYap();
                Confirm_Button.Enabled = true;
            }


        }

        private void LoadRaporlarList()
        {
            try
            {
                Raporlar_List.Items.Clear();
                foreach (var item in Settings.Default.Yapildi)
                {
                    string[] text = item.ToString().Split('-');
                    if (text[1].ToString().Trim() == Settings.Default.Username)
                    {
                        Raporlar_List.Items.Add(item);
                    }
                }
                saniye_sayac = 0;
                dakika_sayac = 0;

            }
            catch
            {

            }
        }

        private void LoadYapilacaklarList()
        {
            try
            {
                Yapilacaklar_List.Items.Clear();

                foreach (var item in Settings.Default.Yapilacaklar)
                {

                    Yapilacaklar_List.Items.Add(item);
                }
            }
            catch
            {

            }

        }

        private void Egzersiz_Ekle(object sender, EventArgs e)
        {
            if (Settings.Default.IsLogged)
            {
                EgzersizKontrol(sender, e);
                LoadYapilacaklarList();
            }
            else
            {
                HataMesaj.GirisYap();
            }

        }

        private void EgzersizKontrol(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                foreach (var item in Settings.Default.Yapilacaklar)
                {
                    if (item == (btn.Name + " Egzersizi"))
                    {
                        HataMesaj.ZatenVar();
                        return;
                    }
                }
                Settings.Default.Yapilacaklar.Add(btn.Name + " Egzersizi");
                Settings.Default.Save();

                Popup.instance.Info("Programa Eklendi", $"{btn.Name} Egzersizi Başarılı Bir Şekilde Programa Eklenmiştir");
            }
            catch
            {

            }
        }

        private void ClearYapilacaklarList(object sender, EventArgs e)
        {
            SoruMesaj.instance.YapilacaklarListesiTemizle();
            if (SoruMesaj.instance.res == DialogResult.Yes)
            {
                Settings.Default.Yapilacaklar.Clear();
                Settings.Default.Save();
                LoadYapilacaklarList();

                Popup.instance.Success("Liste Temizlendi", "Yapılacaklar Listesi Başarılı Bir Şekilde Temizlendi");
            }


        }

        int total = 0;
        int egz = 1;
        Timer tmr = new Timer();

        private void Start_Egzersize_Click(object sender, EventArgs e)
        {
            try
            {
                egz = Convert.ToInt32(EgzersizSure_Textbox.Text);
                if (EgzersizSure_Textbox.Text != string.Empty)
                {

                    total = egz * Settings.Default.Yapilacaklar.Count;

                    saniye_sayac = 0;
                    dakika_sayac = 0;
                    egz_basi_sayac = 0;

                    tmr.Start();

                    Yapilacaklar_List.SelectedIndex = 0;

                    Popup.instance.Info("Egzersiz Başladı", "İyi çalışmalar!");

                    Exercise_Cancel_Button.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HataMesaj.CatchError(ex);
            }

        }
        int saniye_sayac = 0, dakika_sayac = 0, egz_basi_sayac = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            saniye_sayac++;

            if (saniye_sayac >= 60)
            {
                saniye_sayac = 0;
                dakika_sayac++;
                egz_basi_sayac++;
            }

            if (total == dakika_sayac)
            {
                tmr.Stop();
                RaporOlustur();

            }

            else if (egz == egz_basi_sayac)
            {
                egz_basi_sayac = 0;
                var index = Yapilacaklar_List.SelectedIndex;
                Yapilacaklar_List.SelectedIndex = index + 1;
            }

            Sec_Label.Text = saniye_sayac.ToString();
            Min_Label.Text = dakika_sayac.ToString();

        }

        private void Sec_Label_Click(object sender, EventArgs e)
        {

        }

        private void Exercise_Cancel_Button_Click(object sender, EventArgs e)
        {
            SoruMesaj.instance.EgzersizIptal();
            if (SoruMesaj.instance.res == DialogResult.Yes)
            {
                tmr.Stop();
                Min_Label.Text = "0";
                Sec_Label.Text = "0";
                Exercise_Cancel_Button.Enabled = false;

            }


        }

        private void Clear_RaporlarList(object sender, EventArgs e)
        {
            SoruMesaj.instance.RaporlarListTemizle();
            if(SoruMesaj.instance.res == DialogResult.Yes)
            {
                Settings.Default.Yapildi.Clear();
                Settings.Default.Save();
                LoadRaporlarList();
                Popup.instance.Success("Raporlar Temizlendi", "Raporlar Listesi Başarılı Bir Şekilde Temizlendi");

            }

        }

        private void Call_Reports_Btn_Click(object sender, EventArgs e)
        {
            LoadRaporlarList();
        }

        int dk_Kalori = 10;

        private void RaporOlustur()
        {
            try
            {
                int index = Settings.Default.Yapildi.Count == 0 ? 1 : Settings.Default.Yapildi.Count + 1;

                Settings.Default.Yapildi.Add($"{index} - {Settings.Default.Username} - Geçirilen süre: {total} dk - Yakılan Kalori: {total * dk_Kalori}");
                Settings.Default.Save();
                LoadRaporlarList();

                Popup.instance.Info("Rapor Oluşturuldu", "Raporlar sayfasından erişebilirsiniz.");

                Exercise_Cancel_Button.Enabled = false;
            }
            catch
            {

            }

        }
    }
}
