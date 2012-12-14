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
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataForChange()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="originalData">原始数据</param>
        /// <param name="newData">新数据</param>
        public DataForChange(T originalData, T newData)
        {
            this.originalData = originalData;
            this.newData = newData;
        }

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
