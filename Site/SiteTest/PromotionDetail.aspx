<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="PromotionDetail.aspx.cs" Inherits="PromotionDetail" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery-1.3.2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center;font-size:14px; margin:50px 0 10px 0"><asp:Literal ID="ltLevel1" runat="server"></asp:Literal></div>
    <div style="text-align:center;font-size:12px; margin:10px 0 50px 0"><asp:Literal ID="ltLevel2" runat="server"></asp:Literal> </div>
    <div>
        <% var list = GetPromotionGroups();
            foreach (var model in list)
            { %>

        <div style="padding:0 50px 0 50px;">
            <table >
                <tr><td style="width:100px;height:100px;">
                    <%= SiteUtility.ShowImage(model.CoverPath, 100,100, false) %>
                    </td>
                    <td>
                        <p style="font-size:16px;font-weight:bold;color:red"><%=model.LineName %>  </p>
                        <p>产品编号：<%=model.ProductNum %>&nbsp;&nbsp;&nbsp;旅游天数：<%=model.TravelDays %>&nbsp;&nbsp;&nbsp;出发/抵达：<%=model.StartAddress %>/<%=model.TargetAddress %></p>
                        <% var group = GroupBLL.GetRecentlyGroup(model.ID); %>
                         <p>出发日期：<%=model.GoDate.ToDateOnlyString() %></p>
                        <p>线路说明：<br />
                            <%= model.LineDesc.NewLineCharToBr() %></p>
                    </td>
                    <td>

                        <a style="display:block; width:100px; height:40px;text-align:center; line-height:40px;  background-color:darkblue;color:white" href="Order.aspx?PromotionID=<%=model.PromotionID %>&LineID=<%=model.LineID %>&GroupID=<%=model.GroupID %>">马上抢购</a>
                    </td>
                </tr>
                <tr><td></td><td>
                    <% ShowDetail(model.LineID); %>
           <div  style="text-align:center; font-weight:bold; font-size:16px;"  onclick="javascript:$('#p<%=model.GroupID %>').toggle();">详细行程(点我折叠/展开)</div>
            <div id='p<%=model.GroupID %>' style="display:none;">
                <asp:Literal ID="ltDetail" runat="server"></asp:Literal>
            </div>
                     </td></tr>
                <tr><td></td><td>
                    <div  style="height:20px; background-color:#ffd800; margin-bottom:50px;"></div>
                             </td></tr>
            </table>
        </div>

        <%} %>
    </div>
</asp:Content>

