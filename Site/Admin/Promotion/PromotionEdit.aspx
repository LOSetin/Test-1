<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="PromotionEdit.aspx.cs" Inherits="PromotionEdit" %>

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

    <div class="Info"><asp:Literal ID="ltTips" runat="server"></asp:Literal> </div>

    <div class="H20"></div>
        
    <div class="Edit">
        <table class="EditForm">
            <tr>
                <td class="txt">促销名称：</td>
                <td>
                    <asp:TextBox ID="tbName" runat="server" CssClass="EditForm_textbox" Width="250px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>

            <tr>
                <td class="txt">促销时间：</td>
                <td>从
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="70px" ></asp:TextBox>&nbsp;&nbsp;
                    至&nbsp;&nbsp;
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="70"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp; <span class="EditForm_red">*必填</span>
                </td>
            </tr>

               <tr>
                <td class="txt">促销类型：</td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
                    </td>
            </tr>

               <tr>
                <td class="txt">折扣：</td>
                <td>
                    <asp:TextBox ID="tbDiscount" runat="server" CssClass="EditForm_textbox" Width="50px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;<span class="red">0.8表示八折，改设置仅对打折类型促销有效</span></td>
            </tr>

             <tr>
                <td class="txt">一次付款满：</td>
                <td>
                    <asp:TextBox ID="tbTotalPay" runat="server" CssClass="EditForm_textbox" Width="50px" MaxLength="10"></asp:TextBox> 元&nbsp;&nbsp;
                    可购买团位：<asp:TextBox ID="tbJoinNum" runat="server" CssClass="EditForm_textbox" Width="50"></asp:TextBox> 个&nbsp;&nbsp;<span class="red">该设置仅对打包促销方式有效</span>
                </td>
            </tr>
            
            <tr>
                <td class="txt">封面照片：</td>
                <td>
                    <asp:TextBox ID="tbCoverPath" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;
                    <asp:FileUpload ID="upCover" runat="server" Width="200"  />&nbsp;<asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" />
                </td>
            </tr>
           
          
            <tr>
                <td class="txt">促销说明：</td>
                <td>
                    <asp:TextBox ID="tbRemarks" runat="server" TextMode="MultiLine" CssClass="EditForm_textbox" Width="300" Height="100"></asp:TextBox>&nbsp;&nbsp;</td>
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

