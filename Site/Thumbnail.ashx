<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// 动态缩略图处理程序，如果要显示的图片的长宽大于实际长宽，则直接输出图片
/// 调用示例： <img runat="server" src="~/Thumbnail.ashx?src=201404/1.jpg&width=128&height=128" />
/// </summary>
public class ImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string fileName = context.Server.UrlDecode(context.Request["src"]);
        //是否已经处理过图片
        bool isHandled = false;
        if (string.IsNullOrEmpty(fileName))
        {
            context.Response.Write("缺少参数src.");
            return;
        }
        fileName = context.Server.MapPath(GlobalSetting.ImageUploadPath + fileName);
        Stream fileStream = null;
        try
        {
            string wStr = context.Request["width"];
            string hStr = context.Request["height"];
            int width = 0, height = 0;
            if (!string.IsNullOrEmpty(wStr) && !string.IsNullOrEmpty(hStr))
            {
                int.TryParse(wStr, out width);
                int.TryParse(hStr, out height);
            }
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
            {
                return;
            }
            string contentType = getContentType(fi.Extension);
            context.Response.ContentType = contentType;
            //只能处理jpg及png图片格式，没有宽高参数不进行缩放处理,wide,height此处已经是从url获得的长和宽
            if (width > 0 && height > 0 && (contentType.Contains("jpg") || contentType.Contains("jpeg") || contentType.Contains("png")))
            {
                Image image = Image.FromFile(fi.FullName);
                int sWidth = image.Width, sHeight = image.Height;
                int nWidth = 0, nHeight = 0;
                //sWidth,sHeight为原来图片的长和宽
                if (sWidth > width || sHeight > height)
                {
                    if ((sWidth / sHeight) > (width / (height)))
                    {
                        //以宽度为基准缩小
                        if (sWidth > width)
                        {
                            nWidth = width;
                            nHeight = (int)(width * sHeight / (double)sWidth);
                        }
                        else
                        {
                            nWidth = sWidth;
                            nHeight = sHeight;
                        }
                    }
                    else
                    {
                        //以高度为基准缩小
                        if (sHeight > height)
                        {
                            nWidth = (int)(height * sWidth / (double)sHeight);
                            nHeight = height;
                        }
                        else
                        {
                            nWidth = sWidth;
                            nHeight = sHeight;
                        }
                    }
                    Bitmap bitmap = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.Clear(Color.White);
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed; //平滑处理
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //缩放质量
                    graphics.DrawImage(image, new Rectangle(0, 0, nWidth, nHeight));
                    image.Dispose();
                    EncoderParameters parameters = new EncoderParameters(1);
                    parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)80)); //图片质量参数
                    fileStream = new MemoryStream();
                    bitmap.Save(fileStream, GetImageCodecInfo(contentType), parameters);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, GetImageCodecInfo(contentType), parameters);
                        context.Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
                    }
                    isHandled = true;
                    parameters.Dispose();
                    bitmap.Dispose();
                    return;
                }
                if (image != null)
                    image.Dispose();
            }

            if (!isHandled)
            {
                fileStream = new FileStream(fi.FullName, FileMode.Open);
                byte[] bytes = new byte[(int)fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                context.Response.ContentType = contentType;
                context.Response.BinaryWrite(bytes);
            }

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
                fileStream.Dispose();
            }
        }
        System.GC.Collect();
    }
    /// <summary>
    /// 获取文件下载类型
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    private string getContentType(string extension)
    {
        string ct = string.Empty;
        switch (extension.ToLower())
        {
            case ".jpg":
                ct = "image/jpeg";
                break;
            case ".png":
                ct = "image/png";
                break;
            case ".gif":
                ct = "image/gif";
                break;
            case ".bmp":
                ct = "application/x-bmp";
                break;
            default:
                ct = "image/jpeg";
                break;
        }
        return ct;

    }

    //获得包含有关内置图像编码解&码&器的信息的ImageCodecInfo 对象.
    private ImageCodecInfo GetImageCodecInfo(string contentType)
    {
        ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
        ImageCodecInfo jpegICI = null;
        for (int x = 0; x < arrayICI.Length; x++)
        {
            if (arrayICI[x].MimeType.Equals(contentType))
            {
                jpegICI = arrayICI[x];
                //设置JPEG编码
                break;
            }
        }
        return jpegICI;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}