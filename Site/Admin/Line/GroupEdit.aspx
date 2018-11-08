<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="GroupEdit.aspx.cs" Inherits="GroupEdit" %>

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
    <div class="Info">
        <asp:Literal ID="ltTips" runat="server"></asp:Literal> <br />

    </div>

    <div class="H20"></div>
        
    <div class="Edit">
        <table class="EditForm">
             <tr>
                <td class="txt">所属线路：</td>
                <td>
                    <span class="red bold"><asp:Literal ID="ltLineName" runat="server"></asp:Literal> </span>
                  </td>
            </tr>
             <tr>
                <td class="txt">出团日期：</td>
                <td>
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" Width="100" bj="cBj"></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>
            <tr>
                <td class="txt">返回日期：</td>
                <td>
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" Width="100" bj="cBj"></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>
            <tr>
                <td class="txt">团位总数：</td>
                <td>
                    <asp:TextBox ID="tbNum" runat="server" CssClass="EditForm_textbox" Width="100px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填，即参团人数，不含促销位，请看上面说明</span></td>
            </tr>

              <tr>
                <td class="txt">剩余位数：</td>
                <td>
                    <asp:TextBox ID="tbRemain" runat="server" CssClass="EditForm_textbox" Width="100px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>

             <tr>
                <td class="txt">团位价格：</td>
                <td>
                    <asp:TextBox ID="tbPrice" runat="server" CssClass="EditForm_textbox" Width="100px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填，不需登陆看到的价格</span></td>
            </tr>

             <tr>
                <td class="txt">会员价：</td>
                <td>
                    <asp:TextBox ID="tbMemberPrice" runat="server" CssClass="EditForm_textbox" Width="100px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填，登陆后显示的价格</span></td>
            </tr>

             <tr>
                <td class="txt">普通团位订金：</td>
                <td>
                    <asp:TextBox ID="tbDeposit" runat="server" CssClass="EditForm_textbox" Width="100px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填，每个团位预定的价格</span></td>
            </tr>

              <tr>
                <td class="txt">集合时间：</td>
                <td>
                    <asp:TextBox ID="tbGatheringTime" runat="server" CssClass="EditForm_textbox" Width="200" ></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>

             <tr>
                <td class="txt">集合地点：</td>
                <td>
                    <asp:TextBox ID="tbGatheringPlace" runat="server" CssClass="EditForm_textbox" Width="200"  ></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>

             <tr>
                <td class="txt">接送信息：</td>
                <td>
                    <asp:TextBox ID="tbTransfer" runat="server" CssClass="EditForm_textbox" Width="300"  ></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>
            
             <tr>
                <td class="txt">带团人：</td>
                <td>
                    <asp:TextBox ID="tbLeader" runat="server" CssClass="EditForm_textbox" Width="100"  ></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>
          
            <tr>
                <td class="txt"></td>
                <td style="padding-top: 10px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="确定添加" CssClass="btnPrime" OnClick="btnSubmit_Click" />
                    &nbsp;
                    <asp:Button ID="btnClean" runat="server" Text="返回" CssClass="btnGray" OnClick="btnClean_Click" />
                </td>
            </tr>
        </table>
    </div>
     
    <div class="H20"></div>


</asp:Content>

