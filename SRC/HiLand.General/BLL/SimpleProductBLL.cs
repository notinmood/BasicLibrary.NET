using System;
using System.Collections.Generic;
using HiLand.Framework.FoundationLayer;
using HiLand.General.DAL;
using HiLand.General.Entity;

namespace HiLand.General.BLL
{
    public class SimpleProductBLL : BaseBLL<SimpleProductBLL, SimpleProductEntity, SimpleProductDAL>
    {
        public override bool Delete(SimpleProductEntity model)
        {
            if (model == null)
            {
                return false;
            }
            else
            {
                //1.删除图片（包括图片文件，数据库记录）
                List<ImageEntity> imageList= ImageBLL.Instance.GetList(Guid.Empty,model.ProductGuid);
                if (imageList != null)
                {
                    foreach (ImageEntity currentItem in imageList)
                    {
                        ImageBLL.Instance.Delete(currentItem);
                    }
                }
                
                //2.删除数据库记录
                return base.Delete(model);
            }
        }

    }
}
