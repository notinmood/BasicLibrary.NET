using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HiLand.Utility.Data;
using HiLand.Utility.Reflection;

namespace HiLand.Utility.Entity
{
    public class EntityHelper
    {
        /// <summary>
        /// 浅克隆实体对象
        /// </summary>
        /// <typeparam name="T">实体对象的类型</typeparam>
        /// <param name="sourceEntity">源实体对象</param>
        /// <returns>克隆出的新实体对象</returns>
        public static T Clone<T>(T sourceEntity) where T : new()
        {
            return Converter.InheritedEntityConvert<T, T>(sourceEntity);
        }

        /// <summary>
        /// 深克隆实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceEntity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 需要克隆的对象实体，必须标记特性[Serializable]
        /// </remarks>
        public static T DeepClone<T>(T sourceEntity)
        {
            T clonedEntity = default(T);
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                formatter.Serialize(memStream, sourceEntity);
                memStream.Seek(0, SeekOrigin.Begin);
                clonedEntity = (T)formatter.Deserialize(memStream);
            }

            return clonedEntity;
        }

        /// <summary>
        /// 两个实体属性比较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceEntity"></param>
        /// <param name="targetEntity"></param>
        /// <param name="resultData">属性并更的信息：key为变更的属性名称；value为包含变更前值和后值的数据</param>
        /// <param name="excludePropertyName">不进行比较的属性名称集合</param>
        /// <returns></returns>
        public static bool Compare<T>(T sourceEntity, T targetEntity, out Dictionary<string, DataForChange<string>> resultData, params string[] excludePropertyName)
        {
            return ReflectHelper.Compare<T>(sourceEntity, targetEntity, out resultData, excludePropertyName);
        }
    }
}
