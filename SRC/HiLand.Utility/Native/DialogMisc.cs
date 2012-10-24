using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HiLand.Utility.Native
{
    public static class DialogMisc
    {
        /// <summary>
        /// 判断当前对话框中是否点击了 “YES”或者“OK”按钮
        /// </summary>
        /// <param name="dialogResult"></param>
        /// <returns></returns>
        public static bool ConfirmIsEffectButton(DialogResult dialogResult)
        {
            bool result = false;
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                result = true;
            }

            return result;
        }


    }
}
