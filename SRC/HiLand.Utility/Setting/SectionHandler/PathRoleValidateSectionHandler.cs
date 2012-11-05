using System.Configuration;
using System.Xml;

namespace HiLand.Utility.Setting.SectionHandler
{
    /// <summary>
    /// 读取站点验证配置节点section的处理程序
    /// </summary>
    /// <remarks>
    /// 验证配置节点section的组成结构如下：
    ///     <pathRoleValidate >
    ///         <validate path="CheckDirAdmin">
    ///             <role name="admin"></role>
    ///             <role name="manager"></role>
    ///         </validate>
    ///         <validate path="CheckDirRole">
    ///             <role name="admin"></role>
    ///             <role name="otherRole"></role>
    ///         </validate>
    ///     </pathRoleValidate>
    ///     说明：
    ///     1.本验证功能是按照角色目录对照进行验证（即某个角色是否可以访问某个目录）
    ///     2.如果有多个目录需要验证，就在节点pathRoleValidate内添加多个子节点validate（每个validate可以验证一个目录）
    ///     3.那些角色能访问本目录就在节点validate内创建子节点role（每个role表示一个角色可以访问本目录）
    ///     4.如果运行多个角色访问一个目录，那么就在节点validate内添加多个子节点role
    /// </remarks>
    public class PathRoleValidateSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// 获取config节点，创建配置信息实体
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            PathRoleValidateConfig config = new PathRoleValidateConfig();
            PathRoleValidateEntity entity = null;
            foreach (XmlNode node in section.ChildNodes)
            {
                entity = new PathRoleValidateEntity();
                entity.PathToValidate = node.Attributes["path"].Value;

                foreach (XmlNode roleNode in node.ChildNodes)
                {
                    string roleName= roleNode.Attributes["name"].Value;
                    entity.RolesToValidate.Add(roleName);
                }

                config.ValidateEntities.Add(entity.PathToValidate, entity);
            }

            return config;
        }
    }
}
