<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="ScheduleEdit.aspx.cs" Inherits="ScheduleEdit" %>

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
    <div class="Info"><asp:Literal ID="ltTips" runat="server"></asp:Literal> 
        <br />
        在形成标题插入[汽车]，[飞机]，[轮船]会分别显示对应的图标，例如北京[飞机]广州，在网页上，[飞机]将会转成飞机的图标。<br />
        更快速的做法是点击后面的，插入飞机，插入汽车，插入轮船按钮
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
                <td class="txt">行程次序：</td>
                <td>
                    第 <asp:TextBox ID="tbSort" runat="server" CssClass="EditForm_textbox" Width="50px" ></asp:TextBox> 天&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>
            <tr>
                <td class="txt">行程标题：</td>
                <td>
                    <asp:TextBox ID="tbName" runat="server" CssClass="EditForm_textbox" Width="350px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span>
                    <asp:Button ID="btnPlane" runat="server" Text="插入飞机" OnClick="btnPlane_Click" />
                    <asp:Button ID="btnBus" runat="server" Text="插入汽车" OnClick="btnBus_Click" />
                    <asp:Button ID="btnShip" runat="server" Text="插入轮船" OnClick="btnShip_Click" />
                </td>
            </tr>

              <tr>
                <td class="txt">照片1：</td>
                <td>
                    <asp:TextBox ID="tbCoverPath" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;
                    <asp:FileUpload ID="upCover" runat="server" Width="200"  />&nbsp;<asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" />
                </td>
            </tr>

              <tr>
                <td class="txt">照片2：</td>
                <td>
                    <asp:TextBox ID="tbCoverPath2" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;
                    <asp:FileUpload ID="upCover2" runat="server" Width="200"  />&nbsp;<asp:Button ID="Button1" runat="server" Text="上传" OnClick="Button1_Click" />
                </td>
            </tr>

              <tr>
                <td class="txt">照片3：</td>
                <td>
                    <asp:TextBox ID="tbCoverPath3" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;
                    <asp:FileUpload ID="upCover3" runat="server" Width="200"  />&nbsp;<asp:Button ID="Button2" runat="server" Text="上传" OnClick="Button2_Click"  />
                </td>
            </tr>
          
            <tr>
                <td class="txt">行程说明：</td>
                <td>
                    <asp:TextBox ID="tbRemarks" runat="server" TextMode="MultiLine" CssClass="EditForm_textbox" Width="500" Height="100"></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>
           
               <tr>
                <td class="txt">三餐：</td>
                <td>
                    <asp:TextBox ID="tbTips" runat="server" TextMode="MultiLine"  CssClass="EditForm_textbox" Width="300" Height="50"></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>

              <tr>
                <td class="txt">酒店：</td>
                <td>
                    <asp:TextBox ID="tbHotel" runat="server" TextMode="MultiLine"  CssClass="EditForm_textbox" Width="300" Height="50"></asp:TextBox>&nbsp;&nbsp;</td>
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

