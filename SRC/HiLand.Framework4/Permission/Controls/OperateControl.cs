using HiLand.Utility.Enums;
using HiLand.Utility.Setting;
using HiLand.Utility4.MVC.Controls;

namespace HiLand.Framework4.Permission.Controls
{
    /// <summary>
    /// 与MVC中Area/Controller/Action对应操作的控件基类
    /// 主要包括通过点击等方式触发客户端与服务器端进行交互的控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OperateControl<T> : BaseControl<T> where T : BaseControl<T>
    {
        private string _area = string.Empty;
        protected virtual string area
        {
            get { return this._area; }
        }
        /// <summary>
        /// MVC中Area的名称
        /// </summary>
        public T Area(string value)
        {
            this._area = value;
            return this as T;
        }

        private string _controller = string.Empty;
        protected virtual string controller
        {
            get { return this._controller; }
        }
        /// <summary>
        /// MVC中控制器名称
        /// </summary>
        public T Controller(string value)
        {
            this._controller = value;
            return this as T;
        }

        private string _action = string.Empty;
        protected virtual string action
        {
            get { return this._action; }
        }
        /// <summary>
        /// MVC中Action名称
        /// </summary>
        public T Action(string value)
        {
            this._action = value;
            return this as T;
        }

        /// <summary>
        /// 对Action,Controller,Area进行统一赋值
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public T ACA(string action, string controller="", string area = "")
        {
            this._area = area;
            this._controller = controller;
            this._action = action;
            return this as T;
        }

        private bool isUsePermission = Config.GetAppSettingBool("isUsePermissionOnControl");

        /// <summary>
        /// 是否在控件上进行权限控制(缺省值为false)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T IsUsePermission(bool value)
        {
            this.isUsePermission = value;
            return this as T;
        }

        /// <summary>
        /// 当前用户是否有操作此ACA的权限
        /// </summary>
        /// <returns></returns>
        protected bool HasPermission
        {
            get
            {
                if (isUsePermission == false)
                {
                    return true;
                }
                else
                {
                    PermissionValidateStatuses permissionValidateStatus= PermissionValidationHelper.GeneralValidate(action, controller, area);
                    if (permissionValidateStatus == PermissionValidateStatuses.Successful)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
