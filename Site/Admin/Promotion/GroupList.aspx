<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="GroupList.aspx.cs" Inherits="GroupList" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
           <script src="../Resources/js/jquery-1.3.2.min.js"></script>
    <script src="../Resources/js/jquery.calendar.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= tbBegin.ClientID %>").cld();
            $("#<%= tbEnd.ClientID %>").cld();
    }
)
        </script>
    <link href="../Resources/css/calendar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="Info">在这里，您可以对促销：<span class="red bold"><asp:Literal ID="ltTips" runat="server"></asp:Literal></span> 添加促销旅行团<br />
        点击返回：可返回促销管理界面<br />
        <span class="red bold">说明：此处只返回未加入该促销的团，同时，出团日期在今天之后。已加入该促销的团将显示在促销团管理。</span><br />
        输入促销的位数，点击后面的加入促销，即可将该团位置加入该促销。<br />
        促销团位的价格由促销活动决定，例如促销是打八折，那么价格就是普通团位的会员价*0.8<br />
        促销团位不能下订金，只能全额支付。
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="width:250px; text-align:left;"><asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="btnGray" OnClick="btnReturn_Click" />
</td>
                    <td style="padding-right:250px">
                        出团日期：
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100px" ></asp:TextBox>&nbsp;&nbsp;
                    至&nbsp;&nbsp;
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnConfirm" runat="server" CssClass="btnPrime" Text="搜索" OnClick="btnConfirm_Click" />
                    </td>
                </tr>
            </table>
        </div>
    <div class="Line"></div>
    <div style="text-align:center;">        <span class="subTitle">当前促销：<asp:Literal ID="ltTitle" runat="server"></asp:Literal></span>
</div>
    <div class="H20"></div>
      <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="100%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="ID" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
                  <asp:BoundField DataField="LineName" HeaderText="所属线路" />
                <asp:TemplateField HeaderText="出团日期">
                    <ItemTemplate>
                        <%#  Eval("GoDate").ToArrowDateTime().ToDateOnlyString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="返回日期">
                    <ItemTemplate>
                        <%#  Eval("BackDate").ToArrowDateTime().ToDateOnlyString()%>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="GruopLeader" HeaderText="带团人" />
            <asp:BoundField DataField="TotalNum" HeaderText="普通团位总数" />
                 <asp:BoundField DataField="JoinNum" HeaderText="已报名人数" />
                 <asp:BoundField DataField="InnerPrice" HeaderText="会员价" />
                 <asp:BoundField DataField="OuterPrice" HeaderText="普通价" />
         <%--        <asp:BoundField DataField="Deposit" HeaderText="订金" />--%>
<%--                   <asp:TemplateField HeaderText="可促销团位">
                    <ItemTemplate>
                        <%#  Eval("TotalNum").ToArrowInt()-Eval("JoinNum").ToArrowInt()%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="促销团位数">
                    <ItemTemplate>
                        <asp:TextBox ID="tbNum" runat="server" Text="" CssClass="EditForm_textbox" Width="50"></asp:TextBox>
                        <asp:HiddenField ID="hfLineID" runat="server" Value='<%# Eval("LineID") %>' />
                        <asp:HiddenField ID="hfInnerPrice" runat="server" Value='<%# Eval("InnerPrice") %>' />
                        <asp:HiddenField ID="hfOuterPrice" runat="server" Value='<%# Eval("OuterPrice") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
           <%--     <asp:TemplateField HeaderText="开始抢购时间">
                    <ItemTemplate>
                       <asp:DropDownList ID="ddlDate" runat="server"></asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlTime" runat="server"></asp:DropDownList>点
                    </ItemTemplate>
                </asp:TemplateField>--%>
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                         <asp:LinkButton ID="lbtnAdd" runat="server" Text="加入促销" CommandArgument='<%# Eval("ID") %>' CommandName="AddData" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

