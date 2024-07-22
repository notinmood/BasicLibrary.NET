//using System;
//using System.Collections.Generic;
//using System.Data;
//using HiLand.Utility.Event;
//using HiLand.Utility.Paging;
//using HiLand.Utility.Pattern;

//namespace HiLand.Utility.DataBase
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <typeparam name="TTransaction"></typeparam>
//    /// <typeparam name="TConnection"></typeparam>
//    /// <typeparam name="TCommand"></typeparam>
//    /// <typeparam name="TDataReader"></typeparam>
//    /// <typeparam name="TParameter"></typeparam>
//    public class CommonGeneral<TTransaction, TConnection, TCommand, TDataReader, TParameter>
//        where TConnection : class, IDbConnection, new()
//        where TCommand : IDbCommand, new()
//        where TTransaction : IDbTransaction
//        where TDataReader : class, IDataReader
//        where TParameter : IDataParameter, IDbDataParameter, new()
//    {
//        /// <summary>
//        /// 单例
//        /// </summary>
//        public static CommonGeneral<TTransaction, TConnection, TCommand, TDataReader, TParameter> Instance
//        {
//            get
//            {
//                return Singleton<CommonGeneral<TTransaction, TConnection, TCommand, TDataReader, TParameter>>.Instance;
//            }
//        }

//        /// <summary>
//        /// 数据库访问辅助器扩展单例
//        /// </summary>
//        public static CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter> HelperExInstance
//        {
//            get
//            {
//                return CommonHelperEx<TTransaction, TConnection, TCommand, TDataReader, TParameter>.Instance;
//            }
//        }

//        /// <summary>
//        /// 获取总的条目
//        /// </summary>
//        /// <param name="whereClause">过滤条件</param>
//        /// <param name="tableName">要查询的表名</param>
//        /// <returns></returns>
//        public int GetTotalCount(string tableName, string whereClause)
//        {
//            if (string.IsNullOrEmpty(whereClause))
//            {
//                whereClause = " 1=1 ";
//            }

//            string commandText = string.Format("SELECT COUNT(1) FROM [{0}] WHERE {1}", tableName, whereClause);
//            return Convert.ToInt32(HelperExInstance.ExecuteScalar(commandText));
//        }

//        /// <summary>
//        /// 通过Command语句获取数据库内的一条记录，然后从该记录装载业务实体
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="commandText"></param>
//        /// <param name="sqlParas"></param>
//        /// <param name="loadEntityFunction">通过IDataReader组装业务实体的方法</param>
//        /// <returns></returns>
//        public T GetEntity<T>(string commandText, TParameter[] sqlParas, Funcs<IDataReader, T> loadEntityFunction)
//        {
//            T entity = default(T);

//            using (IDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null && reader.Read())
//                {
//                    entity = loadEntityFunction(reader);
//                }
//            }

//            return entity;
//        }

//        /// <summary>
//        /// 通过Command语句获取数据库内符合条件的记录，然后从这些记录装载业务实体集合
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="commandText"></param>
//        /// <param name="sqlParas"></param>
//        /// <param name="loadEntityFunction">通过IDataReader组装业务实体的方法</param>
//        /// <returns></returns>
//        public List<T> GetEntityList<T>(string commandText, TParameter[] sqlParas, Funcs<IDataReader, T> loadEntityFunction)
//        {
//            List<T> list = new List<T>();
//            using (TDataReader reader = HelperExInstance.ExecuteReader(commandText, sqlParas))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        T entity = loadEntityFunction(reader);
//                        list.Add(entity);
//                    }
//                }
//            }

//            return list;
//        }

//        /// <summary>
//        /// 获取分页显示的实体数据（注：其所依赖的存储过程是有特定要求的，包括参数的名称等）
//        /// </summary>
//        /// <param name="startIndex"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="whereClause"></param>
//        /// <param name="orderClause"></param>
//        /// <param name="loadEntityFunction">通过IDataReader组装业务实体的方法</param>
//        /// <param name="pagingSPName">能够有分页功能的存储过程的名称</param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 1.其所依赖的存储过程参数要求如下（参数名称必须如下）
//        /// (@startIndex INT, 
//        /// @endindex INT,
//        /// @whereClause Nvarchar(512)= NULL,
//        /// @orderClause Nvarchar(200)=NULL
//        /// )
//        /// 2.此存储过程返回2个结果集：第一个是某页要显示的数据，第二个是符合条件的总的记录数
//        /// </remarks>
//        public PagedEntityCollection<T> GetPagedCollection<T>(string pagingSPName, string whereClause, string orderClause, int startIndex, int pageSize, Funcs<IDataReader, T> loadEntityFunction) where T : new()
//        {
//            int pageIndex = (startIndex - 1) / pageSize + 1;
//            return GetPagedCollection<T>(pagingSPName, pageIndex, pageSize, whereClause, orderClause, loadEntityFunction);
//        }

//        /// <summary>
//        /// 获取分页显示的实体数据（注：其所依赖的存储过程是有特定要求的，包括参数的名称等）
//        /// </summary>
//        /// <param name="pageIndex"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="whereClause"></param>
//        /// <param name="orderClause"></param>
//        /// <param name="loadEntityFunction">通过IDataReader组装业务实体的方法</param>
//        /// <param name="pagingSPName">能够有分页功能的存储过程的名称</param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 1.其所依赖的存储过程参数要求如下（参数名称必须如下）
//        /// (@startIndex INT, 
//        /// @endindex INT,
//        /// @whereClause Nvarchar(512)= NULL,
//        /// @orderClause Nvarchar(200)=NULL
//        /// )
//        /// 2.此存储过程返回2个结果集：第一个是某页要显示的数据，第二个是符合条件的总的记录数
//        /// </remarks>
//        public PagedEntityCollection<T> GetPagedCollection<T>(string pagingSPName, int pageIndex, int pageSize, string whereClause, string orderClause, Funcs<IDataReader, T> loadEntityFunction) where T : new()
//        {
//            if (pageIndex <= 0)
//            {
//                pageIndex = 1;
//            }
//            int startIndex = (pageIndex - 1) * pageSize + 1;
//            int endIndex = pageIndex * pageSize;
//            int totalCount = 0;

//            PagedEntityCollection<T> collection = new PagedEntityCollection<T>();
//            using (TDataReader reader = HelperExInstance.ExecuteReaderBySP(pagingSPName, PrepareParameterPaging(startIndex, endIndex, whereClause, orderClause)))
//            {
//                if (reader != null)
//                {
//                    while (reader.Read())
//                    {
//                        T entity = loadEntityFunction(reader);
//                        collection.Records.Add(entity);
//                    }

//                    if (reader.NextResult())
//                    {
//                        if (reader.Read())
//                        {
//                            totalCount = Convert.ToInt32(reader[0]);
//                        }
//                    }

//                    if (pageIndex > 0)
//                    {
//                        collection.PageIndex = pageIndex;
//                    }

//                    if (pageSize > 0)
//                    {
//                        collection.PageSize = pageSize;
//                    }

//                    if (totalCount > 0)
//                    {
//                        collection.TotalCount = totalCount;
//                    }
//                }
//            }

//            return collection;
//        }


//        ///// <summary>
//        ///// 通过反射自动填充实体
//        ///// </summary>
//        ///// <param name="targetObj">实体对象</param>
//        ///// <param name="reader">DbDataReader对象</param>
//        //public void FillEntity(object targetObj, IDataReader reader)
//        //{
//        //    try
//        //    {
//        //        //实现首字母大写，规范所有属性名称
//        //        CultureInfo cult = Thread.CurrentThread.CurrentCulture;
//        //        TextInfo textInfo = cult.TextInfo;
//        //        string name = string.Empty;

//        //        for (int i = 0; i < reader.FieldCount; i++)
//        //        {
//        //            //使用首字母大写转换，规范属性名以进行匹配
//        //            name = textInfo.ToTitleCase(reader.GetName(i).ToLower());
//        //            //根据属性名反射获取属性
//        //            PropertyInfo propertyInfo = targetObj.GetType().GetProperty(name);
//        //            if (propertyInfo != null)
//        //            {
//        //                //如果值不为空以及属性为可写，则进行写入操作
//        //                if (reader.GetValue(i) != DBNull.Value && propertyInfo.CanWrite)
//        //                {
//        //                    //为属性设置值
//        //                    propertyInfo.SetValue(targetObj, reader.GetValue(i), null);
//        //                }
//        //            }
//        //        }
//        //    }
//        //    catch { }
//        //}

//        #region 分页存储过程的辅助方法
//        /// <summary>
//        /// 为分页的存储过程准备参数
//        /// </summary>
//        /// <param name="startIndex"></param>
//        /// <param name="endIndex"></param>
//        /// <returns></returns>
//        public TParameter[] PrepareParameterPaging(int startIndex, int endIndex)
//        {
//            return PrepareParameterPaging(startIndex, endIndex, string.Empty);
//        }

//        /// <summary>
//        /// 为分页的存储过程准备参数
//        /// </summary>
//        /// <param name="startIndex"></param>
//        /// <param name="endIndex"></param>
//        /// <param name="whereClause"></param>
//        /// <returns></returns>
//        public TParameter[] PrepareParameterPaging(int startIndex, int endIndex, string whereClause)
//        {
//            return PrepareParameterPaging(startIndex, endIndex, whereClause, string.Empty);
//        }

//        /// <summary>
//        /// 为分页的存储过程准备参数
//        /// </summary>
//        /// <param name="startIndex"></param>
//        /// <param name="endIndex"></param>
//        /// <param name="whereClause"></param>
//        /// <returns></returns>
//        public TParameter[] PrepareParameterPaging(int startIndex, int endIndex, string whereClause, string orderClause)
//        {
//            List<TParameter> list = new List<TParameter>();
//            list.Add(SqlMisc.GenerateParameter<TParameter, int>("startIndex", startIndex));
//            list.Add(SqlMisc.GenerateParameter<TParameter, int>("endIndex", endIndex));

//            if (string.IsNullOrEmpty(whereClause) == false)
//            {
//                list.Add(SqlMisc.GenerateParameter<TParameter, string>("whereClause", whereClause));
//            }

//            if (string.IsNullOrEmpty(orderClause) == false)
//            {
//                list.Add(SqlMisc.GenerateParameter<TParameter, string>("orderClause", orderClause));
//            }

//            return list.ToArray();
//        }

//        /// <summary>
//        /// 准备过滤条件参数
//        /// </summary>
//        /// <param name="whereClause"></param>
//        /// <returns></returns>
//        public TParameter PrepareParameterCount(string whereClause)
//        {
//            //return new SqlParameter("@whereClause", whereClause);
//            return SqlMisc.GenerateParameter<TParameter, string>("whereClause", whereClause);
//        }
//        #endregion
//    }
//}
