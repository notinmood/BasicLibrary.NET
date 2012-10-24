using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Entity
{
    /// <summary>
    /// 信息改变时使用的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataForChange<T>
    {
        private T originalData= default(T);
        /// <summary>
        /// 信息改变前的原始值
        /// </summary>
        public T OriginalData
        {
            get { return this.originalData; }
            set { this.originalData = value; }
        }

        private T newData = default(T);
        /// <summary>
        /// 信息改变后的新值
        /// </summary>
        public T NewData
        {
            get { return this.newData; }
            set { this.newData = value; }
        }
    }
}
