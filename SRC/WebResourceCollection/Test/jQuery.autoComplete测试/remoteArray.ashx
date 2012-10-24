<%@ WebHandler Language="C#" Class="remoteArray" %>

using System;
using System.Web;
using System.Text;

public class remoteArray : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        // 查询的参数名称默认为term
        string query = context.Request.QueryString["term"];
        StringBuilder builder = new StringBuilder();
        builder.Append("[");
        for (int i = 0; i < 10; i++)
        {
            builder.Append("\"");
            builder.Append(query);
            builder.Append(i.ToString());
            builder.Append("\",");
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