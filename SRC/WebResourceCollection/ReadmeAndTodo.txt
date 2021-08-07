20210807 本项目下，页面效果部分，迁移进入https://github.com/notinmood/BasicLibrary.Web

1.人员组的功能，
2.人员权限按照组进行计算的逻辑
4.考虑WebResourceCollection\PageEffect\20个具有代表性的web2.0配色方案.htm中提取出一个统一的样式放入Hiland.css中
5.BrowserEx内要将加入收藏和设置首页测试弄通(WebResourceCollection\Scripts\jQuery.PlugIn\jQuery.hiland.general.js)
6.独立的日志功能（可以将日志记录在各种介质中（即需要进行各种介质的日志实现））
7.记录异常记录（使用日志的功能，记录方式可以配置）
==【已经实现】===============================================================
1.判断当前用户和缓存这个地方，目前还没有实现 Web App和Native App之间的自适应。可以考虑在自适应的基础上，加入一个能够区分何种应用的配置项，以提供选择应用类型的性能
3.权限表，可以对各种主体类型进行统一管理