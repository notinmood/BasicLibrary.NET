using HiLand.Utility.Enums;
using System;

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// 
    /// </summary>
    internal class ConditionModel
    {
        public string FieldName { get; set; }

        public object FieldValue { get; set; }

        public Type FieldType { get; set; }

        public CompareModes compareMode { get; set; }
    }
}
