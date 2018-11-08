<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;


public class UploadHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        HttpPostedFile file = context.Request.Files[0];

        //~/Upload/App/
        string uploadPath = "~/Upload/TinyMce/";
        string rootPath = context.Server.MapPath(uploadPath);
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        if (file != null)
        {
            string oldFileName = file.FileName;
            int pos = oldFileName.LastIndexOf(".");
            string extName = oldFileName.Substring(pos + 1);
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extName;
            String dirName = DateTime.Now.ToString("yyyyMM");
            String path = HttpContext.Current.Server.MapPath(uploadPath + dirName + "/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            file.SaveAs(path + newFileName);
            context.Response.Write("/Upload/TinyMce/" + dirName + "/" + newFileName);
        }
        else
        {
            context.Response.Write("-1");
        }


    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}