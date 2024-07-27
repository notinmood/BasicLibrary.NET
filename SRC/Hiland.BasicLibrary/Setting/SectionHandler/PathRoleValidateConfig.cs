using System.Collections.Generic;

namespace HiLand.Utility.Setting.SectionHandler
{
    public class PathRoleValidateConfig
    {
        public Dictionary<string, PathRoleValidateEntity> ValidateEntities = new Dictionary<string, PathRoleValidateEntity>();

        public PathRoleValidateEntity FindByPath(string path)
        {
            if (ValidateEntities.ContainsKey(path))
            {
                return ValidateEntities[path];
            }
            else
            {
                return null;
            }
        }
    }
}
