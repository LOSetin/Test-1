<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="LineEdit.aspx.cs" Inherits="LineEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <script src="../Resources/js/jquery-1.3.2.min.js"></script>
    <script src="../Resources/js/jquery.calendar.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
         <%--   $("#<%= tbBegin.ClientID %>").cld();
        $("#<%= tbEnd.ClientID %>").cld();--%>
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
                <td class="txt">线路名称：</td>
                <td>
                    <asp:TextBox ID="tbName" runat="server" CssClass="EditForm_textbox" Width="350px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>

               <tr>
                <td class="txt">线路分类：</td>
                <td>
                    <asp:DropDownList ID="ddlCat" runat="server"></asp:DropDownList> &nbsp;&nbsp;<span class="EditForm_red">*尽量选择最下层分类，例如“国内专线”下有“北京专线”，则应该选“北京专线”</span>
                </td>
            </tr>

             <tr>
                <td class="txt">产品编号：</td>
                <td>
                    <asp:TextBox ID="tbProductNum" runat="server" CssClass="EditForm_textbox" Width="150px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>

              <tr>
                <td class="txt">封面照片：</td>
                <td>
                    <asp:TextBox ID="tbCoverPath" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;
                    <asp:FileUpload ID="upCover" runat="server" Width="200"  />&nbsp;<asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" />
                </td>
            </tr>

               <tr>
                <td class="txt">旅游天数：</td>
                <td>
                    <asp:TextBox ID="tbDays" runat="server" CssClass="EditForm_textbox" Width="50px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;<span class="red">*必填</span></td>
            </tr>


            <tr>
                <td class="txt">最低价：</td>
                <td>
                    <asp:TextBox ID="tbMinPrice" runat="server" CssClass="EditForm_textbox" Width="70px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;<span class="red">*必填，用于显示“xxx元起”</span></td>
            </tr>

             <tr>
                <td class="txt">是否接送：</td>
                <td>
                    <asp:CheckBox ID="chkPickup" runat="server" /> </td>
            </tr>

               <tr>
                <td class="txt">出发地：</td>
                <td>
                    <asp:TextBox ID="tbStartAddress" runat="server" CssClass="EditForm_textbox" Width="100px" MaxLength="100"></asp:TextBox></td>
            </tr>

               <tr>
                <td class="txt">目的地：</td>
                <td>
                    <asp:TextBox ID="tbTargetAddress" runat="server" CssClass="EditForm_textbox" Width="100px" MaxLength="100"></asp:TextBox></td>
            </tr>

             <tr>
                <td class="txt">去程交通：</td>
                <td>
                   <asp:TextBox ID="tbGoTravel" runat="server" CssClass="EditForm_textbox" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            
             <tr>
                <td class="txt">返程交通：</td>
                <td>
                  <asp:TextBox ID="tbBackTravel" runat="server" CssClass="EditForm_textbox" Width="200px" ></asp:TextBox>
                </td>
            </tr>
          
            <tr>
                <td class="txt">线路说明：</td>
                <td>
                    <asp:TextBox ID="tbRemarks" runat="server" TextMode="MultiLine" CssClass="EditForm_textbox" Width="300" Height="100"></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>

             <tr>
                <td class="txt">报名须知：</td>
                <td>
                    <asp:TextBox ID="tbNotice" runat="server" TextMode="MultiLine" CssClass="EditForm_textbox" Width="300" Height="100"></asp:TextBox>&nbsp;&nbsp;</td>
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

