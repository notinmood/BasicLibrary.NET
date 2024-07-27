//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Hiland.BasicLibrary.Setting.SectionHandler
//{
//    /// <summary>
//    /// 系统任务配置信息
//    /// </summary>
//    public class SystemTaskSectionConfig
//    {
//        private static SystemTaskSectionConfig instance = null;
//        /// <summary>
//        /// 系统任务配置信息实例
//        /// </summary>
//        public static SystemTaskSectionConfig Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = Config.GetSection<SystemTaskSectionConfig>("systemTaskSection");
//                }

//                return instance;
//            }
//        }

//        private List<SystemTaskOfDailyExcutorEntity> systemTaskOfDailyExcutorList = new List<SystemTaskOfDailyExcutorEntity>();
//        /// <summary>
//        /// 每日执行的系统任务列表
//        /// </summary>
//        public List<SystemTaskOfDailyExcutorEntity> SystemTaskOfDailyExcutorList
//        {
//            get { return this.systemTaskOfDailyExcutorList; }
//            set { this.systemTaskOfDailyExcutorList = value; }
//        }
//    }
//}
