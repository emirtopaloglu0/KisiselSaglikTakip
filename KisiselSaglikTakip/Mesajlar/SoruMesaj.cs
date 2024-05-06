using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KisiselSaglikTakip.Mesajlar
{
    internal class SoruMesaj
    {
        public static SoruMesaj instance = new SoruMesaj();

        public DialogResult res;

        public void CloseApp()
        {
            res = MessageBox.Show("Çıkmak İstediğinze Emin Misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void ConfirmUser()
        {
            res = MessageBox.Show("Kullanıcı Adını Onaylıyor Musunuz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        internal void EgzersizIptal()
        {
            res = MessageBox.Show("Egzersizi İptal Etmek İstediğinize Emin Misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        internal void Logout()
        {
            res = MessageBox.Show("Kullanıcıyı Değiştirmek İstediğinize Emin Misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        internal void RaporlarListTemizle()
        {
            res = MessageBox.Show("Raporlar Listesini Temizlemek İstediğinize Emin Misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        internal void YapilacaklarListesiTemizle()
        {
            res = MessageBox.Show("Yapılacaklar Listesi Temizlensin mi?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
