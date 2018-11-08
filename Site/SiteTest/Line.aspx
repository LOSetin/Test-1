<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="Line.aspx.cs" Inherits="SiteTest_Line" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center;font-size:14px; margin:50px 0 10px 0"><asp:Literal ID="ltLevel1" runat="server"></asp:Literal></div>
    <div style="text-align:center;font-size:12px; margin:10px 0 50px 0"><asp:Literal ID="ltLevel2" runat="server"></asp:Literal> </div>
    <div>
        <% var list = GetLines();
            foreach (var model in list)
            { %>

        <div style="padding:0 50px 0 50px;">
            <table >
                <tr><td style="width:100px;height:100px;">
                    <%= SiteUtility.ShowImage(model.CoverPath, 100,100, false) %>
                    </td>
                    <td>
                        <p style="font-size:16px;font-weight:bold;color:red"><%=model.Name %>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=model.MinPrice %>起</p>
                        <p>产品编号：<%=model.ProductNum %>&nbsp;&nbsp;&nbsp;旅游天数：<%=model.TravelDays %>&nbsp;&nbsp;&nbsp;出发/抵达：<%=model.StartAddress %>/<%=model.TargetAddress %></p>
                        <% var group = GroupBLL.GetRecentlyGroup(model.ID); %>
                         <p>最近发团：<%=group!=null?group.GoDate.ToDateOnlyString():"" %></p>
                        <p><%= model.LineDesc.NewLineCharToBr() %></p>
                    </td>
                    <td>
                            <a style="display:block; width:100px; height:30px;text-align:center; line-height:30px;  background-color:brown;color:wheat" href="SetFav.aspx?LineID=<%=model.ID %>">收藏</a>
                        <br />
                        <a style="display:block; width:100px; height:40px;text-align:center; line-height:40px;  background-color:darkblue;color:white" href="Detail.aspx?LineID=<%=model.ID %>">查看/预定</a>
                    </td>
                </tr>
            </table>
        </div>

        <%} %>
    </div>
</asp:Content>

