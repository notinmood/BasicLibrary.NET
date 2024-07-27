//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;

//namespace HiLand.Utility.Native
//{
//    /// <summary>
//    /// 对话框辅助器
//    /// </summary>
//    public static class MessageBoxHelper
//    {
//        public static bool Show(string text)
//        {
//            return Show(text, "警告");
//        }

//        public static bool Show(string text, string caption)
//        {
//            return Show(text, caption, MessageBoxIcon.Question);
//        }

//        public static bool Show(string text, string caption, MessageBoxIcon icon)
//        {
//            DialogResult dialogResult = MessageBox.Show(text, caption, MessageBoxButtons.OKCancel, icon);
//            return DialogMisc.ConfirmIsEffectButton(dialogResult);
//        }
//    }
//}
