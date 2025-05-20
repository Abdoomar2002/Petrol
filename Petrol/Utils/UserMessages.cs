
using System.Windows.Forms;

namespace Petrol.Utils
{
    public class UserMessages
    {
        public static void Info(string message) 
        {
            MessageBox.Show(message,"معلومه",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void Error(string message) {
            MessageBox.Show(message,"خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        public static DialogResult Warning(string message) {
            var result= MessageBox.Show(message,"تنبيه",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            return result;
        }

    }
}
