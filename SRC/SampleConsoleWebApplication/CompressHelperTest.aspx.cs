using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class CompressHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Literal1.Text= CompressHelper.Compress("http://localhost/databaseManage/Labor/Index/9?LaborQuery||QueryControlDisplayStatus=open&LaborQuery||ConditionCountName=6&LaborQuery||ConditionFieldName||0=CurrentEnterpriseName&LaborQuery||ConditionTypeName||0=System.String%2C%20mscorlib&LaborQuery||ConditionValueName||0=%E6%B3%B0%E7%A7%91&LaborQuery||ConditionOperatorName||0=like&LaborQuery||ConditionRelationshipName||0=AND&LaborQuery||ConditionFieldName||1=UserNameCN&LaborQuery||ConditionTypeName||1=System.String%2C%20mscorlib&LaborQuery||ConditionValueName||1=&LaborQuery||ConditionOperatorName||1=%3D&LaborQuery||ConditionRelationshipName||1=AND&LaborQuery||ConditionFieldName||2=UserSex&LaborQuery||ConditionTypeName||2=HiLand.Utility.Enums.Sexes%2C%20HiLand.Utility&LaborQuery||ConditionValueName||2=&LaborQuery||ConditionOperatorName||2=%3D&LaborQuery||ConditionRelationshipName||2=AND&LaborQuery||ConditionFieldName||3=WorkSkill&LaborQuery||ConditionTypeName||3=System.String%2C%20mscorlib&LaborQuery||ConditionValueName||3=&LaborQuery||ConditionOperatorName||3=%3D&LaborQuery||ConditionRelationshipName||3=AND&LaborQuery||ConditionFieldName||4=LaborWorkStatus&LaborQuery||ConditionTypeName||4=XQYC.Business.Enums.LaborWorkStatuses%2C%20XQYC.Business&LaborQuery||ConditionValueName||4=&LaborQuery||ConditionOperatorName||4=%3D&LaborQuery||ConditionRelationshipName||4=AND&LaborQuery||ConditionFieldName||5=CurrentContractDiscontinueDate&LaborQuery||ConditionTypeName||5=System.DateTime%2C%20mscorlib&LaborQuery||ConditionValueName||5=&LaborQuery||ConditionOperatorName||5=%3D");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Literal1.Text = CompressHelper.Decompress("H4sIAAAAAAAEAKWUbWvCMBSFf032sbUvKg7C0OrYwDlcdS8fo71oME1KcgsW/PGz3TrmVmnET0nIOSHPOXC3iNmt6wq1ZmKrDLoJQ7ZiBp6YZBtwp2yltPsoE9i7g7vqNM9BF4dDtURKolZizE0mWBEjw9xQlYG8+S09qhKOXMlI5RJnLAXaaxbccxBJKTgcOjTKtQaJE4mgM80NlBfNvkWRQW2LC4OQOjFqLjfEj4jfSc1aacFXzeZXJvIfN5n0yCggow6Z9MmwTwZes+k5A81Q6don+O7M315AsHI1W57V6uFs3BqAR5cGqvejWRu1dxW1R20YPUqCM7/+j+hZIfoVYgz7Nj6fPvApk4mzRC44Fs5E5qlxjk4wX7Cn963IvhWyfwmyb4Uc0Deld/GOC9EGHVxVamBFGFxCGFgRhrRSVJjVQGjjDOn7/CNyRrnhEoz57vbPI3XPJ9LWEEKrEMJLQgitQujW06uakGyNxxG5Pm65zGHMsHWMdevuS/GCp3Bh+10r8G4J/gmpBmYWAwYAAA==");
        }
    }
}