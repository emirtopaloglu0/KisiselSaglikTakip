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
            res = MessageBox.Show("Kullanıcı adını onaylıyor musunuz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        internal void Logout()
        {
            res = MessageBox.Show("Kullanıcıyı değiştirmek istediğinize emin misiniz?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
