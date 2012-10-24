using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// SQLite 数据库类型访问帮助器
    /// </summary>
    public class SQLiteHelper : CommonHelper<SQLiteTransaction, SQLiteConnection, SQLiteCommand, SQLiteDataReader, SQLiteParameter>
    {
        /*
         1.数据插入与更新

            使用REPLACE替代INSERT、UPDATE命令。在无满足条件记录，则执行Insert，有满足条件记录，则执行UPDATE。
                REPLACE INTO TABLE1(col1, col2, col3) VALUES(val1, val2,val3);

            Insert or Replace Into 和Replace Into 的效果是一样的上面这句话也可以这样写
                Insert or Replace INTO TABLE1(col1, col2, col3) VALUES(val1, val2,val3);


         2.SQLite 分页查询

            写法1:
            SELECT * FROM TABLE1 LIMIT  20 OFFSET 20 ;

            写法2:
            SELECT * FROM TABLE1 LIMIT 20 , 20;


         3.SQLite 文件的压缩
            在多次删除数据、插入数据、更新数据后，数据库体积增大，但实际有效数据量很小，则需要对数据库进行压缩、整理，把已经删除的数据从物理文件中移除。调用一下SQL命令即可：
            VACUUM
         */
    }
}