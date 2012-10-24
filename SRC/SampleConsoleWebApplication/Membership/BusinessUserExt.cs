using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.FoundationLayer;

namespace WebApplicationConsole.Membership
{
    public class BusinessUserExt : BusinessUser
    {
        public string ExtProteryA
        {
            get { return ((IModelExtensible)this).ExtensiableRepository.GetExtentibleProperty("ExtProteryA"); }
            set { ((IModelExtensible)this).ExtensiableRepository.SetExtentibleProperty("ExtProteryA", value); }
        }

        public int ExtProteryB
        {
            get { return ((IModelExtensible)this).ExtensiableRepository.GetExtentibleProperty<int>("ExtProteryB"); }
            set { ((IModelExtensible)this).ExtensiableRepository.SetExtentibleProperty("ExtProteryB", value.ToString()); }
        }
    }
}