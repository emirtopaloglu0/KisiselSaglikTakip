using KisiselSaglikTakip.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulpep.NotificationWindow;

namespace KisiselSaglikTakip.Class
{
    internal class Popup
    {
        public static Popup instance = new Popup();

        public void Info(string titleText, string contentText)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Resources.info1;
            popup.BodyColor = Color.FromArgb(23, 162, 184);
            popup.TitleText = " "+titleText;
            popup.TitleColor = Color.White;
            popup.TitleFont = new Font("Figtree", 14, FontStyle.Bold);

            popup.ContentText = contentText;
            popup.ContentColor = Color.White;
            popup.ContentFont = new Font("Figtree", 13);
            popup.Popup();
        }

        public void Success(string titleText, string contentText)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Resources.check;
            popup.BodyColor = Color.FromArgb(40, 167, 69);
            popup.TitleText = " " + titleText;
            popup.TitleColor = Color.White;
            popup.TitleFont = new Font("Figtree", 14, FontStyle.Bold);

            popup.ContentText = contentText;
            popup.ContentColor = Color.White;
            popup.ContentFont = new Font("Figtree", 13);
            popup.Popup();
        }
    }
}
