using HiLand.Utility.Enums;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// 
    /// </summary>
    public class WhereClauseBuilder<TParameter>
        where TParameter : IDbDataParameter, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static WhereClauseBuilder<TParameter> Create()
        {
            return new WhereClauseBuilder<TParameter>();
        }

        private List<ConditionModel> conditionQueue = new List<ConditionModel>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public virtual WhereClauseBuilder<TParameter> AppendCondition(string fieldName, string fieldValue)
        {
            return AppendCondition<string>(fieldName, fieldValue, CompareModes.Equal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public virtual WhereClauseBuilder<TParameter> AppendCondition(string fieldName, string fieldValue, CompareModes compareMode)
        {
            return AppendCondition<string>(fieldName, fieldValue, compareMode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public virtual WhereClauseBuilder<TParameter> AppendCondition<T>(string fieldName, T fieldValue)
        {
            return AppendCondition<T>(fieldName, fieldValue, CompareModes.Equal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public virtual WhereClauseBuilder<TParameter> AppendCondition<T>(string fieldName, T fieldValue, CompareModes compareMode)
        {
            conditionQueue.Add(new ConditionModel()
            {
                compareMode = compareMode,
                FieldName = fieldName,
                FieldValue = fieldValue,
                FieldType = typeof(T)
            });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseType"></param>
        /// <returns></returns>
        public virtual ClauseModel<TParameter> GetClause()
        {
            return GetClause(ConditionItemRelationships.AND);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseType"></param>
        /// <returns></returns>
        public virtual ClauseModel<TParameter> GetClause(ConditionItemRelationships conditionConnector)
        {
            List<TParameter> paraList = new List<TParameter>();
            StringBuilder sb = new StringBuilder();


            string connector = string.Empty;
            if (conditionConnector == ConditionItemRelationships.AND)
            {
                connector = " AND ";
                sb.Append(" 1=1 ");
            }
            else
            {
                connector = " OR ";
                sb.Append(" 1=2 ");
            }

            foreach (ConditionModel currentCoditionModel in conditionQueue)
            {
                sb.AppendFormat(" {0} {1}=@{1} ", connector, currentCoditionModel.FieldName);
                TParameter para = new TParameter();
                para.ParameterName = string.Format("@{0}", currentCoditionModel.FieldName);
                para.Value = currentCoditionModel.FieldValue;
                paraList.Add(para);
            }

            return new ClauseModel<TParameter>()
            {
                CluaseString = sb.ToString(),
                ParameterList = paraList
            };
        }
    }
}
