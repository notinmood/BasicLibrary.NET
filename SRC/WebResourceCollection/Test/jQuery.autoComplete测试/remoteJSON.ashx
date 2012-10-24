<%@ WebHandler Language="C#" Class="remoteJSON" %>

using System;
using System.Web;
using System.Text;

public class remoteJSON : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //演示客户端提交过了其他值进行解析
        string foo = context.Request.Params["autoCompleteExtraParam"];
        
        // 查询的参数名称默认为term
        string query = context.Request.QueryString["term"];
        StringBuilder builder = new StringBuilder();
        builder.Append("[");
        for (int i = 0; i < 10; i++)
        {
            builder.Append("{\"value\":\"");
            builder.Append(query);
            builder.Append(i.ToString());
            builder.Append("\",\"label\":\"");
            builder.Append(query);
            builder.Append("显示");
            builder.Append(i.ToString());
            builder.Append("\",\"name\":\"");
            builder.Append("名称");
            builder.Append(i.ToString());
            builder.Append("\"},");
        }
        if (builder.Length > 1)
            builder.Length = builder.Length - 1;
        builder.Append("]");

        context.Response.ContentType = "text/javascript";
        context.Response.Write(builder.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}