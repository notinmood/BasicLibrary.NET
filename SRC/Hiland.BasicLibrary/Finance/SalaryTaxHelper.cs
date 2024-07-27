//using System;
//using System.Collections.Generic;
//using System.Text;
//using Hiland.BasicLibrary.Setting.SectionHandler;

//namespace Hiland.BasicLibrary.Finance
//{
//    /*
//     本类需要配置文件的支持
//     */

//    /// <summary>
//    /// 个税帮助器
//    /// </summary>
//    public class SalaryTaxHelper
//    {
//        /// <summary>
//        /// 计算应缴工资税
//        /// </summary>
//        /// <param name="data">参与计算的应付工资（通常都是扣除掉保险公积金的工资）</param>
//        /// <returns></returns>
//        public static decimal GetSalaryTax(decimal data)
//        {
//            SalaryTaxConfig config = SalaryTaxConfig.Instance;
//            decimal dataRemveThreadhold = (data - config.SalaryTax.TaxThreshold);

//            SalaryTaxLevel salaryTaxLevel = GetSalaryTaxLevel(dataRemveThreadhold);
            
//            //全月应纳税额=全月应纳税所得额×适用税率－速算扣除
//            return dataRemveThreadhold * salaryTaxLevel.Rate - salaryTaxLevel.ExpressCalcValue;
//        }

//        /// <summary>
//        /// 获取适用的工资税级别
//        /// </summary>
//        /// <param name="dataRemveThreadhold"></param>
//        /// <returns></returns>
//        private static SalaryTaxLevel GetSalaryTaxLevel(decimal dataRemveThreadhold)
//        {
//            SalaryTaxLevel result = new SalaryTaxLevel();
//            SalaryTaxConfig config = SalaryTaxConfig.Instance;
//            for (int i = 0; i < config.SalaryTax.SalaryTaxLevels.Count; i++)
//            {
//                SalaryTaxLevel salaryTaxLevel = config.SalaryTax.SalaryTaxLevels[i];
//                if (dataRemveThreadhold > salaryTaxLevel.Min && dataRemveThreadhold <= salaryTaxLevel.Max)
//                {
//                    result= salaryTaxLevel;
//                    break;
//                }
//            }

//            return result;
//        }
//    }
//}
