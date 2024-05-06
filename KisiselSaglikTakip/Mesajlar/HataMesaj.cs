using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KisiselSaglikTakip.Mesajlar
{
    internal class HataMesaj
    {
        internal static void CatchError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void GirisYap()
        {
            MessageBox.Show("Lütfen Önce Giriş Yapınız", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static void ZatenVar()
        {
            MessageBox.Show("Eklemek istediğiniz egzersiz zaten var", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
