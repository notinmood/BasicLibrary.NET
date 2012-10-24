using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.DataBase
{
    internal class ConditionModel
    {
        public string FieldName { get; set; }

        public object FieldValue { get; set; }

        public Type FieldType { get; set; }

        public CompareModes compareMode { get; set; }
    }
}
