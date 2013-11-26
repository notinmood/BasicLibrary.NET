using System;

namespace HiLand.Utility.IO
{
    public class OperateStreamEnventArgs : EventArgs
    {
        /// <summary>
        /// (循环操作中每次)读取到的是字节数组
        /// </summary>
        public byte[] BytesReaded { get; set; }
        /// <summary>
        /// 回调对象
        /// </summary>
        public object CallBackObject { get; set; }
    }

    /// <summary>
    /// 对流进行操作的委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public delegate bool OperateStreamEnventHandler(object sender, OperateStreamEnventArgs args);
}
