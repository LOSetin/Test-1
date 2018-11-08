using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework
{
    public static class VideoHelper
    {
        /// <summary>
        /// 调用WMplayer
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="filepathname">路径和文件名</param>
        public static string ShowWMPlayer(string id, int width, int height, string filepathname)
        {
            StringBuilder sbPlayer = new StringBuilder();
            sbPlayer.Append("<object id=\"" + id + "\" width=\"" + width + "\" height=\"" + height + "\" classid=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\">");
            sbPlayer.Append("<param NAME=\"AutoStart\" VALUE=\"-1\">");
            //<!--是否自动播放-->
            sbPlayer.Append("<param NAME=\"Balance\" VALUE=\"0\">");
            //<!--调整左右声道平衡,同上面旧播放器代码-->
            sbPlayer.Append("<param name=\"EnableContextMenu\" value=\"0\">");
            //<!-是否用右键弹出菜单控制-->
            sbPlayer.Append("<param name=\"enabled\" value=\"1\">");
            //<!--播放器是否可人为控制-->
            sbPlayer.Append("<param NAME=\"EnableContextMenu\" VALUE=\"-1\">");
            //<!--是否启用上下文菜单-->
            sbPlayer.Append("<param NAME=\"url\" value=\"" + filepathname + "\">");
            //<!--播放的文件地址-->
            sbPlayer.Append("<param NAME=\"PlayCount\" VALUE=\"1\">");
            //<!--播放次数控制,为整数-->
            sbPlayer.Append("<param name=\"rate\" value=\"1\">");
            //<!--播放速率控制,1为正常,允许小数,1.0-2.0-->
            sbPlayer.Append("<param name=\"currentPosition\" value=\"-1\">");
            //<!--控件设置:当前位置-->
            sbPlayer.Append("<param name=\"currentMarker\" value=\"-1\">");
            //<!--控件设置:当前标记-->
            sbPlayer.Append("<param name=\"defaultFrame\" value=\"0\">");
            //<!--显示默认框架-->
            sbPlayer.Append("<param name=\"invokeURLs\" value=\"0\">");
            //<!--脚本命令设置:是否调用URL-->
            sbPlayer.Append("<param name=\"baseURL\" value=\"\">");
            //<!--脚本命令设置:被调用的URL-->
            sbPlayer.Append("<param name=\"stretchToFit\" value=\"1\">");
            //<!--是否按比例伸展-->
            sbPlayer.Append("<param name=\"volume\" value=\"50\">");
            //<!--默认声音大小0%-100%,50则为50%-->
            sbPlayer.Append("<param name=\"mute\" value=\"0\">");
            //<!--是否静音-->
            sbPlayer.Append("<param name=\"uiMode\" value=\"Full\">");
            //<!--播放器显示模式:Full显示全部;mini最简化;None不显示播放控制,只显示视频窗口;invisible全部不显示-->
            sbPlayer.Append("<param name=\"windowlessVideo\" value=\"0\">");
            //<!--如果是0可以允许全屏,否则只能在窗口中查看-->
            sbPlayer.Append("<param name=\"fullScreen\" value=\"0\">");
            //<!--开始播放是否自动全屏-->
            sbPlayer.Append("<param name=\"VideoBorder3D\" value=\"-1\">");
            sbPlayer.Append("<param name=\"enableErrorDialogs\" value=\"-1\">");
            //<!--是否启用错误提示报告-->
            sbPlayer.Append("<param name=\"SAMIStyle\" value>");
            //<!--SAMI样式-->
            sbPlayer.Append("<param name=\"SAMILang\" value>");
            //<!--SAMI语言-->
            sbPlayer.Append("<param name=\"SAMIFilename\" value>");
            //<!--字幕ID-->
            sbPlayer.Append("<param name=\"ShowDisplay\" value=\"0\">");
            sbPlayer.Append("<param name=\"ShowStatusBar\" value=\"-1\">");
            sbPlayer.Append("<param name=\"ShowControls\" value=\"-1\">");
            sbPlayer.Append("<embed width=\"" + width + "\" height=\"" + height + "\" type=\"application/x-mplayer2\" src=\"" + filepathname + "\"></embed>"); //FireFox下
            sbPlayer.Append("</object>");
            return sbPlayer.ToString();
        }

        /// <summary>
        /// 调用RealonePlayer
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="filepathname">路径和文件名</param>
        public static string ShowRealonePlayer(string id, int width, int height, string filepathname)
        {
            StringBuilder sbPlayer = new StringBuilder();

            sbPlayer.Append("<object onerror=\"alert('请先安装RealPlayer播放器！');\" CLASSID=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" ID=\"" + id + "\"  width=\"" + width + "\" height=\"" + height + "\" type=\"audio/x-pn-realaudio-plugin\">");
            sbPlayer.Append("<param name=\"_ExtentX\" value=\"9313\">");
            sbPlayer.Append("<param name=\"_ExtentY\" value=\"7620\">");
            sbPlayer.Append("<param name=\"AUTOSTART\" value=\"1\">");
            sbPlayer.Append("<param name=\"SHUFFLE\" value=\"0\">");
            sbPlayer.Append("<param name=\"PREFETCH\" value=\"0\">");
            sbPlayer.Append("<param name=\"NOLABELS\" value=\"0\">");
            sbPlayer.Append("<param name=\"SRC\" value=\"" + filepathname + "\">");
            sbPlayer.Append("<param name=\"CONTROLS\" value=\"ImageWindow,controlpanel,StatusBar\">");
            sbPlayer.Append("<param name=\"CONSOLE\" value=\"Clip1\">");
            sbPlayer.Append("<param name=\"LOOP\" value=\"0\">");
            sbPlayer.Append("<param name=\"NUMLOOP\" value=\"0\">");
            sbPlayer.Append("<param name=\"CENTER\" value=\"0\">");
            sbPlayer.Append("<param name=\"MAINTAINASPECT\" value=\"0\">");
            sbPlayer.Append("<param name=\"BACKGROUNDCOLOR\" value=\"#000000\">");
            sbPlayer.Append("<embed SRC type=\"audio/x-pn-realaudio-plugin\" CONSOLE=\"Clip1\" CONTROLS=\"ImageWindow,controlpanel,StatusBar\" WIDTH=\"" + width + "\" HEIGHT=\"" + height + "\" AUTOSTART=\"false\">");
            sbPlayer.Append("</object>");
            return sbPlayer.ToString();

        }

    }
}
