using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 工资税计算信息
    /// </summary>
    public class SalaryTax
    {
        /// <summary>
        /// 工资税起征点
        /// </summary>
        public decimal TaxThreshold { get; set; }

        private List<SalaryTaxLevel> salaryTaxLevels = new List<SalaryTaxLevel>();
        /// <summary>
        /// 工资税起征级别列表
        /// </summary>
        public List<SalaryTaxLevel> SalaryTaxLevels
        {
            get { return salaryTaxLevels; }
            set { this.salaryTaxLevels = value; }
        }
    }

    /// <summary>
    /// 工资税起征级别
    /// </summary>
    public class SalaryTaxLevel
    {
        /// <summary>
        /// 个税起征级别的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 个税起征级别的最小值
        /// </summary>
        public decimal Min { get; set; }

        /// <summary>
        /// 个税起征级别的最大值
        /// </summary>
        public decimal Max { get; set; }

        /// <summary>
        /// 个税起征级别的税率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 个税起征级别的速算扣除数
        /// </summary>
        public decimal ExpressCalcValue { get; set; }
    }
}
