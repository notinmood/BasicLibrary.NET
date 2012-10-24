权限验证有两套体系
1.本目录下的适用于MVC场景之下执行的验证，可用验证到具体的操作
2.HiLand.Utility\Setting\SectionHandler 目录下的适用于WebForm场景之下执行的验证，可用验证到页面


使用时，需做如下配置
1.（本段配置，各数值精确确定为如下内容）
  <configSections>
    <sectionGroup name="permissionValidate">
      <section name="generalValidate" type="HiLand.Utility4.MVC.SectionHandler.PermissionValidateSectionHandler,HiLand.Utility4" />
    </sectionGroup>
  </configSections>
2.  （本段配置，使用时文档结构确定，同时请适当调整各个属性的值）
  <permissionValidate>
    <generalValidate>
      <application guid="541F8657-938D-48CF-B4B2-348C6065F723" name="应用DEMO1">
        <module guid="EDCA19F1-C471-4342-A9FB-52DD7F605A83" name="模块A">
          <subModule guid="87773E95-0219-46A6-8409-0264446CCCE8" name="用户管理">
            <operation action="List" controller="Home" area="" name="List" text="列表" value="2"></operation>
            <operation action="Add" controller="Home" area=""  name="Add" value="4"></operation>
            <operation action="Edit" controller="Home" area=""  name="Edit" value="8"></operation>
            <operation action="Delete" controller="Home" area=""  name="Delete" text="状态控制" value="16"></operation>
            <operation action="PermissionSetting" controller="Home" area=""  name="PermissionSetting" text="用户权限设置" value="512"></operation>
          </subModule>
          <subModule guid="97004E95-0219-46A6-8409-0264446CCCE8" name="子模块A1">
            <operation action="List" controller="Admin" area="" name="List" text="列表" value="2"></operation>
          </subModule>
        </module>
      </application>
    </generalValidate>
  </permissionValidate>