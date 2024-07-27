using System;
using System.Collections.Generic;

namespace HiLand.Utility.Setting.SectionHandler
{
    public class GeneralValidateConfig
    {
        private Dictionary<Guid, GeneralValidateApplication> applications = new Dictionary<Guid, GeneralValidateApplication>();

        public Dictionary<Guid, GeneralValidateApplication> Applications
        {
            get { return applications; }
            set { applications = value; }
        }


        public static Dictionary<Guid, GeneralValidateSubModule> AllSubModuleDic = null;
        public static Dictionary<string, Guid> FileSubModuleDic = null;
        public static Dictionary<Guid, int> SubModelActionDic = null;
        /// <summary>
        /// 优化GeneralValidateConfig存储结构
        /// </summary>
        /// <param name="config"></param>
        internal static void OptimizeStrcture(GeneralValidateConfig config)
        {
            AllSubModuleDic = new Dictionary<Guid, GeneralValidateSubModule>();
            FileSubModuleDic = new Dictionary<string, Guid>();
            SubModelActionDic = new Dictionary<Guid, int>();

            foreach (KeyValuePair<Guid, GeneralValidateApplication> application in config.Applications)
            {
                foreach (KeyValuePair<Guid, GeneralValidateModule> module in application.Value.Modules)
                {
                    foreach (KeyValuePair<Guid, GeneralValidateSubModule> subModule in module.Value.SubModules)
                    {
                        AllSubModuleDic.Add(subModule.Key, subModule.Value);
                        foreach (KeyValuePair<string, string> kvp in subModule.Value.Pages)
                        {
                            if (FileSubModuleDic.ContainsKey(kvp.Value) == false)
                            {
                                FileSubModuleDic.Add(kvp.Value, subModule.Key);
                            }
                        }

                        int allActionValues = 0;
                        foreach (KeyValuePair<string, GeneralValidateOperation> kvp in subModule.Value.Operations)
                        {
                            allActionValues += kvp.Value.OperationValue;
                        }
                        SubModelActionDic.Add(subModule.Key, allActionValues);
                    }
                }
            }
        }
    }
}