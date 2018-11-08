<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="promotion.aspx.cs" Inherits="SiteTest_promotion" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <% var list = PromotionBLL.GetAllValidPromotion();
        foreach (var model in list)
        {
        %>

     <div style="padding:0 50px 0 50px;">
            <table >
                <tr><td style="width:100px;height:100px;">
                    <%= SiteUtility.ShowImage(model.CoverPath, 100,100, false) %>
                    </td>
                    <td>
                        <p>促销名：<%=model.Name %>&nbsp;&nbsp;&nbsp;</p>
                         <p>促销时间：<%=model.StartTime.ToDateOnlyString()+"-------"+model.EndTime.ToDateOnlyString() %></p>
                        <p><%= model.PromotionDesc %></p>
                    </td>
                    <td style="padding-left:50px;">
                        <a style="display:block; width:100px; height:40px;text-align:center; line-height:40px;  background-color:darkblue;color:white" href="PromotionDetail.aspx?PromotionID=<%=model.ID %>">查看详细</a>
                    </td>
                </tr>
            </table>
        </div>

    <%} %>

</asp:Content>

