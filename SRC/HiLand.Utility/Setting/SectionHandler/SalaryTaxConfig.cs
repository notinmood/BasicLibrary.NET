using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 工资税配置信息
    /// </summary>
    public class SalaryTaxConfig
    {
        private static SalaryTaxConfig instance = null;
        /// <summary>
        /// 工资税配置信息实例
        /// </summary>
        public static SalaryTaxConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Config.GetSection<SalaryTaxConfig>("SalaryTaxSection");
                }

                return instance;
            }
        }

        private SalaryTax salaryTax = new SalaryTax();
        /// <summary>
        /// 工资税计算信息
        /// </summary>
        public SalaryTax SalaryTax
        {
            get { return this.salaryTax; }
            set { this.salaryTax = value; }
        }
    }
}
