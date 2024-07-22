using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Event
{
     /// <summary>
     /// 
     /// </summary>
    public class SearchEventArgs : EventArgs
    {
        private string selectedSearchKey = string.Empty;
        /// <summary>
        /// 选定的搜索项
        /// </summary>
        public string SelectedSearchKey
        {
            get
            {
                return this.selectedSearchKey;
            }
            set
            {
                this.selectedSearchKey = value;
            }
        }

        private CompareModes selectedSearchOperator = CompareModes.Equal;
        /// <summary>
        /// 选定的搜索操作符
        /// </summary>
        public CompareModes SelectedSearchOperator
        {
            get
            {
                return this.selectedSearchOperator;
            }
            set
            {
                this.selectedSearchOperator = value;
            }
        }

        private string searchValue = string.Empty;
        /// <summary>
        /// 要搜索的值文本信息
        /// </summary>
        public string SearchValue
        {
            get
            {
                return this.searchValue;
            }
            set
            {
                this.searchValue = value;
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchExpression
        {
            //TODO:xieran20121001 searchValue为字符串和数字时需要分开处理
            get
            {
                string expression = string.Empty;
                if (string.IsNullOrEmpty(this.selectedSearchKey) == false && string.IsNullOrEmpty(this.searchValue) == false)
                {
                    switch (selectedSearchOperator)
                    {
                        case CompareModes.LessThan:
                            expression = string.Format("{0} < {1}", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.NotLessThan:
                            expression = string.Format("{0} >= {1}", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.MoreThan:
                            expression = string.Format("{0} > {1}", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.NotMoreThan:
                            expression = string.Format("{0} <= {1}", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.Like:
                            expression = string.Format("{0} like '%{1}%'", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.LikeLeft:
                            expression = string.Format("{0} like '{1}%'", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.LikeRight:
                            expression = string.Format("{0} = '%{1}'", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.NotEqual:
                            expression = string.Format("{0} != '{1}'", this.selectedSearchKey, this.searchValue);
                            break;
                        case CompareModes.Equal:
                        default:
                            expression = string.Format("{0} = '{1}'", this.selectedSearchKey, this.searchValue);
                            break;
                    }
                }

                return expression;
            }
        }
    }
}
