Handler,Config,Entity关系:
1.Entity是某一个配置项的实体
2.Config是Entity的集合,web.config内的某个section的配置都会读取到这个config中
3.Handler的作用是将web.config的section中配置项内容解析并载入到config中,其载入时机是在某个文件(通常是Module)中调用 类似如下代码的时候
	***Config config = (***Config)ConfigurationManager.GetSection("generalValidate");