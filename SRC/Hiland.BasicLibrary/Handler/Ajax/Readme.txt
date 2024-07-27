此Handler的作用就是允许前端Javascript中调用C#方法
此handler通常在WebForm开发模式下使用（在MVC模式下JS可以直接调用Controller/Action）

建议在WEB下建立单位的文件夹用了映射这个Handler，即所有对这个文件夹的请求都通过这个handler来处理，在这个文件夹的web.config中如下配置
  <appSettings>
    <add key="AjaxHandlerTypeDescriptionDllName" value="[表示需要调用方法所在的DLL的名称（不能省略）]"/>
	<add key="AjaxHandlerTypeDescriptionPrefix" value="[表示需要调用方法所在类型的前缀命名空间（可以省略，但是需要在URL中指定全名称）]"/>
  </appSettings>
  
  <system.web>
    <httpHandlers>
      <add path="*.aspx" validate="false" type="Hiland.BasicLibrary.Handler.Ajax.AjaxHandler,Hiland.BasicLibrary" verb="*"/>
    </httpHandlers>
  </system.web>
  <system.webServer>
    <handlers>
      <add name="AjaxRequestFactory" path="*.aspx" type="Hiland.BasicLibrary.Handler.Ajax.AjaxHandler,Hiland.BasicLibrary" verb="*"/>
    </handlers>
  </system.webServer>

  具体可以参考http://www.cnblogs.com/robot/archive/2012/03/09/2386083.html和例子
  \SampleConsoleWebApplication\HLAjaxHandlerDemo\TestPage.htm