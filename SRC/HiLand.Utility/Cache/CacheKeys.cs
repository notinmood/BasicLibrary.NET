using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Cache
{
    public class CacheKeys
    {
        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字；{2}：表示实体的主键值
        /// </summary>
        public const string EntityKeyFormat = "{0}-{1}||-ByKey:{2}";
        
        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字；{2}：表示属性的名字；{3}：表示属性的值
        /// </summary>
        public const string EntityPropertyFormat = "{0}-{1}||-By{2}:{3}";

        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字
        /// </summary>
        public const string EntityPrefixFormat = "{0}-{1}||";

        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字
        /// </summary>
        public const string EntityListPrefixFormat = "{0}-{1}||s-";

        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字；{2}：表示Where过滤条件
        /// </summary>
        public const string EntityCountFormat = "{0}-{1}||s-Count:{2}";
        
        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字；{2}:OnlyDisplayUsable；{3}:WhereClause；{4}:TopCount；{5}:OrderByClause
        /// </summary>
        public const string EntityListFormat = "{0}-{1}||s-OnlyDisplayUsable:{2}-WhereClause:{3}-TopCount:{4}-OrderByClause:{5}";
        
        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字；{2}:StartIndex；{3}:EndIndex；{4}:WhereClause；{5}:OrderByClause
        /// </summary>
        public const string EntityPageCollectionFormat = "{0}-{1}||s-StartIndex{2}-EndIndex{3}-WhereClause{4}-OrderByClause{5}";

        /// <summary>
        /// {0}:表示模块的名字；{1}：表示实体的名字；{2}：表示Sql语句
        /// </summary>
        public const string EntityScalarFormat = "{0}-{1}||s-Scalar:{2}";
    }
}
