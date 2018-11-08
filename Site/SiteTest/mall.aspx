<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="mall.aspx.cs" Inherits="mall" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <% var list = GoodsBLL.GetAllGoods();
        foreach (var model in list)
        {
        %>

     <div style="padding:0 50px 0 50px;">
            <table >
                <tr><td style="width:100px;height:100px;">
                    <%= SiteUtility.ShowImage(model.CoverPath, 100,100, false) %>
                    </td>
                    <td>
                        <p>商品名：<%=model.Name %>&nbsp;&nbsp;&nbsp;</p>
                        <p>所需积分：<%=model.Points %></p>
                        <p>库存数量：<%=model.Num %></p>
                        <p><%= model.Remarks %></p>
                    </td>
                    <td style="padding-left:50px;">
                        <a style="display:block; width:100px; height:40px;text-align:center; line-height:40px;  background-color:darkblue;color:white" href="ConfirmChange.aspx?GoodsID=<%=model.ID %>">我要兑换</a>
                    </td>
                </tr>
            </table>
        </div>

    <%} %>

</asp:Content>

