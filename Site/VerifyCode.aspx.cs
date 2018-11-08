using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class VerifyCode : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //定义图片的宽度
        int ImageWidth = 55;
        //定义图片高度
        int ImageHeigh = 22;
        //定义字体，用于绘制文字
        Font font = new Font("Arial", 12, FontStyle.Bold);
        //定义画笔，用于绘制文字
        Brush brush = new SolidBrush(Color.Black);
        
        //定义钢笔，用于绘制干扰线
        Pen pen1 = new Pen(Color.Red, 0);//这里也可以直接获得一个现有的color对象如：Color.Gold.我是为了美观所以定义和下面一样
        Pen pen2 = new Pen(Color.Red, 0);//这里根据ARGB值定义获得了一个color对象

        //创建一个图像
        Bitmap BitImage = new Bitmap(ImageWidth, ImageHeigh);
        //从图像获取一个绘画面
        Graphics graphics = Graphics.FromImage(BitImage);
        
        //清除整个绘图画面并用颜色填充
        graphics.Clear(Color.White);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        //定义文字的绘制矩形区域
        RectangleF rect = new RectangleF(5, 2, ImageWidth, ImageHeigh);
        //定义一个随机数对象，用于绘制干扰线
        Random rand = new Random();

        //背景噪点生成
        Pen blackPen = new Pen(Color.Lavender, 0);
        for (int i = 0; i < 100; i++)
        {
            int x = rand.Next(0, ImageWidth);
            int y = rand.Next(0, ImageHeigh);
            graphics.DrawRectangle(blackPen, x, y, 1, 1);
        }

       
        //生成两条横向的干扰线
        for (int i = 0; i < 2; i++)
        {
            //定义起点
            Point p1 = new Point(0, rand.Next(ImageHeigh));
            //定义终点
            Point p2 = new Point(ImageWidth, rand.Next(ImageHeigh));
            //绘制直线
            graphics.DrawLine(pen1, p1, p2);
        }
        //生成两条纵向的干扰线
        for (int i = 0; i < 2; i++)
        {
            //定义起点
            Point p1 = new Point(rand.Next(ImageWidth), 0);
            //定义终点
            Point p2 = new Point(rand.Next(ImageWidth), ImageHeigh);
            //绘制直线
            graphics.DrawLine(pen2, p1, p2);
        }
        //绘制验证码文字
        string vc = "";
        Random rnd=new Random();
        for (int i = 0; i < 4; i++)
        {
            vc += rnd.Next(0,9);
        }
        Session["MemberVerifyCode"] = vc;

        graphics.DrawString(vc, font, brush, rect);
        //保存图片为gif格式
        BitImage.Save(Response.OutputStream, ImageFormat.Gif);
       
        //释放对象
        graphics.Dispose();
        BitImage.Dispose();
    }
}
