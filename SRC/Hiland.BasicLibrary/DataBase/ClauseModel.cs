using System.Collections.Generic;
using System.Data;

namespace Hiland.BasicLibrary.DataBase
{
    /// <summary>
    /// SQL语句实体
    /// </summary>
    public class ClauseModel<TParameter> where TParameter : IDbDataParameter
    {
        /// <summary>
        /// 语句（可以带参数）
        /// </summary>
        public string CluaseString { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<TParameter> ParameterList { get; set; }

        /// <summary>
        /// 将当前条件跟其他条件进行合并
        /// </summary>
        /// <param name="anotherClauseModel"></param>
        /// <returns></returns>
        public ClauseModel<TParameter> Combine(ClauseModel<TParameter> anotherClauseModel)
        {
            return Combine(this, anotherClauseModel);
        }

        /// <summary>
        /// 将多个条件进行合并
        /// </summary>
        /// <param name="clauseModels"></param>
        /// <returns></returns>
        public static ClauseModel<TParameter> Combine(params ClauseModel<TParameter>[] clauseModels)
        {
            ClauseModel<TParameter> targetClauseModel = new ClauseModel<TParameter>();
            targetClauseModel.CluaseString = " 1=1 ";
            targetClauseModel.ParameterList = new List<TParameter>();

            if (clauseModels != null)
            {
                foreach (ClauseModel<TParameter> currentClauseModel in clauseModels)
                {
                    string currentClauseString = currentClauseModel.CluaseString;
                    if (string.IsNullOrEmpty(currentClauseString))
                    {
                        currentClauseString = " 1=1 ";
                    }
                    targetClauseModel.CluaseString = string.Format(" {0} AND ( {1} ) ", targetClauseModel.CluaseString, currentClauseString);
                    targetClauseModel.ParameterList.AddRange(currentClauseModel.ParameterList);
                }
            }

            return targetClauseModel;
        }
    }
}
