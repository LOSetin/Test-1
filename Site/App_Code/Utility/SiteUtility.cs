using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

/// <summary>
/// SiteUtility 的摘要说明
/// </summary>
public class SiteUtility
{
    /// <summary>
    /// 上传图片
    /// </summary>
    /// <param name="fileUpload"></param>
    /// <returns></returns>
    public static string UploadPic(FileUpload fileUpload)
    {
        string uploadPath = GlobalSetting.ImageUploadPath;
        string strFileExtention = System.IO.Path.GetExtension(fileUpload.FileName.ToString());
        Boolean fileOK = false;
        //获取上传的文件名   
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + strFileExtention;
        //判断文件夹是否存在
        String dirName = DateTime.Now.ToString("yyyyMM");
        String path = HttpContext.Current.Server.MapPath(uploadPath + dirName + "/");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //判断上传控件是否上传文件   
        if (fileUpload.HasFile)
        {
            //判断上传文件的扩展名是否为允许的扩展名".gif", ".png", ".jpeg", ".jpg" ,".bmp"   
            String fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            String[] Extensions = GlobalSetting.ImageExtensions;
            for (int i = 0; i < Extensions.Length; i++)
            {
                if (fileExtension == Extensions[i])
                {
                    fileOK = true;
                }
            }
        }
        //如果上传文件扩展名为允许的扩展名，则将文件保存在服务器上指定的目录中   
        if (fileOK)
        {
            try
            {
                fileUpload.PostedFile.SaveAs(path + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件不能上传，原因：" + ex.Message);
                return "";
            }
        }
        else
        {
            MessageBox.Show("不能上传这种类型的文件");
            return "";
        }
        return dirName + "/" + fileName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string GetImageUrl(string imagePath,int width,int height)
    {
        string result = "";
        if (imagePath.IsNullOrEmpty())
            return result;

        string thumbnail = HttpHelper.GetRootURI() + "/Thumbnail.ashx?src={0}&width={1}&height={2}";
        thumbnail = string.Format(thumbnail, HttpContext.Current.Server.UrlEncode(imagePath), width, height);

        return thumbnail;
    }

    /// <summary>
    /// 显示图片
    /// </summary>
    /// <param name="imagePath">图片路径</param>
    /// <param name="width">图片宽度</param>
    /// <param name="height">图片高度</param>
    /// <param name="canSeeBigPic">点击是否可以查看大图</param>
    /// <returns></returns>
    public static string ShowImage( string imagePath,int width,int height,bool canSeeBigPic=false)
    {
        string result = "";
        if (imagePath.IsNullOrEmpty())
            return result;

        string thumbnail = HttpHelper.GetRootURI() + "/Thumbnail.ashx?src={0}&width={1}&height={2}";
        thumbnail = string.Format(thumbnail, HttpContext.Current.Server.UrlEncode(imagePath), width, height);
        string bigPic = HttpHelper.GetRootURI() + GlobalSetting.ImageUploadPath.Replace("~", "") + imagePath;

        if (canSeeBigPic)
        {
            string imgTag = "<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" /></a>";
            result= string.Format(imgTag, bigPic, thumbnail);
        }
        else
        {
            string imgTag = "<img src=\"{0}\"  />";
            result = string.Format(imgTag, thumbnail);
        }

        return result;
    }


    /// <summary>
    /// 显示图片
    /// </summary>
    /// <param name="imagePath">图片路径，以|分隔</param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="canSeeBigPic"></param>
    /// <returns></returns>
    public static string ShowAllImage(string imagePath, int width, int height, bool canSeeBigPic = false)
    {
        string result = "";
        if (imagePath.IsNullOrEmpty())
            return result;

        string[] paths = imagePath.Split('|');

        string thumbnail = HttpHelper.GetRootURI() + "/Thumbnail.ashx?src={0}&width={1}&height={2}";
       
        string imgTag = "<a href=\"{0}\" target=\"_blank\"><img style='margin-right:5px;' src=\"{1}\" /></a>";
        if (!canSeeBigPic)
            imgTag = "<img style='margin-right:5px;'  {0} src=\"{1}\" />";

        for(int i=0;i<paths.Length;i++)
        {
            if (!paths[i].IsNullOrEmpty())
            {
                string src = string.Format(thumbnail, HttpContext.Current.Server.UrlEncode(paths[i]), width, height);
                string bigPic = "";
                if (canSeeBigPic)
                    bigPic = HttpHelper.GetRootURI() + GlobalSetting.ImageUploadPath.Replace("~", "") + paths[i];
                result = result + string.Format(imgTag, bigPic, src);
            }
        }


        return result;
    }

   


}