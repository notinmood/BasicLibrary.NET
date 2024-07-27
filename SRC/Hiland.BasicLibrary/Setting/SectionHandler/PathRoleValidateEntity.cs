using System.Collections.Generic;

namespace HiLand.Utility.Setting.SectionHandler
{
    public class PathRoleValidateEntity
    {
        private string pathToValidate = string.Empty;
        /// <summary>
        /// 需要验证的目录
        /// </summary>
        public string PathToValidate
        {
            get { return this.pathToValidate; }
            set { this.pathToValidate = value; }
        }

        private List<string> rolesToValidate = new List<string>();
        /// <summary>
        /// 需要验证的角色
        /// </summary>
        public List<string> RolesToValidate
        {
            get { return this.rolesToValidate; }
            set { this.rolesToValidate = value; }
        }
    }   
}
