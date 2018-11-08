<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Right.aspx.cs" Inherits="Master_Right" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .Header { font-size:14px; font-weight:bold;  border-bottom:solid 1px #ccc; padding-bottom:5px; }
        .Row { text-indent: 50px; margin-top:10px; }
        .LWB { height: 22px; width: 50%; }
        .LW { width: 75px; }

        .gvPage1
    {
        width:95%;
        padding:5px 0px;
        height:15px;
        line-height:15px;
        font-size:12px;
        font-family:宋体;
        text-align:left;
        
    }
    .gvPage1 a,.gvPage1 a:visited
    {
        text-decoration:none;
        color:#0A5BAA;
        margin:3px;
    }
    .gvPage1 a span,.gvPage1 a:visited span
    {
        padding-left:2px;
        padding-right:2px;
        color:#0A5BAA;
    }
    .gvPage1 .Current
    {
        color:#ff0000;
        font-weight:bold;
        padding-left:2px;
        padding-right:2px;
    }
    .gvPage1 .Tips
    {
        margin-right:10px;
        color:#333333;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="padding-left:50px; padding-right:50px;">
    <div class="Header">登陆信息</div>
        <div class="H5"></div>
        <div class="Row">欢迎，<span style="color:#cc0000; font-size:12px;"><%=CurrentAdmin.RealName %></span>，您是 <span style="color:#cc0000;"><%=CurrentAdmin.IsSuper?"超级管理员":"普通管理员" %></span></div>
        <div class="Row">上次登陆时间：<%=CurrentAdmin.LastLoginTime %>&nbsp;&nbsp;上次登录IP：<%=CurrentAdmin.LastLoginIP %></div>
        <div class="Row">本次登陆时间：<%=CurrentAdmin.ThisLoginTime %>&nbsp;&nbsp;本次登录IP：<%=CurrentAdmin.ThisLoginIP %></div>
        <div class="Row"><asp:Literal ID="ltLoginMsg" runat="server"></asp:Literal> </div>
        <div class="H30"></div>

        <div class="Header">服务器环境</div>
        <div class="H5"></div>
        <div style="padding-left:50px;" >  
            <table width="98%">
                    <tr class="LH">
                        <td class="LWB">服务器名称：<%=Environment.MachineName %></td>
                       <td class="LWB">操作系统：<%=Environment.OSVersion %></td>
            </tr>
                <tr class="LH">
            <td class="LWB">服务器IP：<%=GetServerIP()%></td>       
            <td class="LWB">服务器端口：<%=Request.ServerVariables["SERVER_PORT"]%></td>
            </tr>
            <tr class="LH">
            <td class="LWB">服务器域名：<%= Request.ServerVariables["SERVER_NAME"]%></td>
              <td class="LWB">服务器运行时间：<%=((Environment.TickCount / 0x3e8) / 60).ToString() + "分钟"%></td>
            </tr>
         <tr class="LH">
            <td class="LWB">IIS版本：<%=Request.ServerVariables["SERVER_SOFTWARE"]%></td>
            <td class="LWB">.NET版本：<%=string.Concat(new object[] { Environment.Version.Major, ".", Environment.Version.Minor, Environment.Version.Build, ".", Environment.Version.Revision })%></td>
            </tr>
        </table></div>

        <div class="H30"></div>

        <div class="Header">程序信息</div>
        <div class="H5"></div>
         <div style="padding-left:50px;" >  
            <table width="98%">
                <tr class="LH">
            <td class="LWB">当前程序占用内存：<%=((Double)GC.GetTotalMemory(false) / 1048576).ToString("N2") + "M"%></td>       
            <td class="LWB">Asp.Net占用内存：<%=((Double) System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + "M"%></td>
            </tr>
            <tr class="LH">
            <td class="LWB">程序路径：<%= HttpContext.Current.Request.PhysicalApplicationPath%></td>
            <td class="LWB">脚本超时：<%=(HttpContext.Current.Server.ScriptTimeout / 1000).ToString() + "秒"%></td>
            </tr>
            <tr class="LH">
            <td class="LWB">Session总数：<%=Session.Contents.Count%></td>
            <td class="LWB">Application总数：<%=Application.Contents.Count%></td>
            </tr>
            
        </table></div>


        </div>
</asp:Content>

