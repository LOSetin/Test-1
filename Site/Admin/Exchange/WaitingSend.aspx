<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="WaitingSend.aspx.cs" Inherits="WaitingSend" %>
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

    <div class="Info">在这里，显示等待发货的积分兑换申请。<br />
        输入快递号和快递公司名称，点击发货即可
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr>
                    <td style=" text-align:center;"> 申请时间：
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100px" ></asp:TextBox>&nbsp;
                    至&nbsp;
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100"></asp:TextBox>&nbsp;
             
                        <asp:Button ID="btnConfirm" runat="server" Text="搜索" CssClass="btnPrime" OnClick="btnConfirm_Click" />
                    </td>
                </tr>
            </table>
        </div>
    <div class="Line"></div>
    <div class="H10"></div>
      <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="100%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="OrderNum" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
        
                <asp:BoundField DataField="UserName" HeaderText="用户名" />
                        <asp:TemplateField HeaderText="兑换商品">
                            <ItemTemplate>
                                <a target="_blank" href='../../GoodsDetail.aspx?ID=<%#Eval("GoodsID") %>'><%# Eval("GoodsName") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
      
                <asp:BoundField DataField="Num" HeaderText="兑换数量" />
                <asp:BoundField DataField="LinkMan" HeaderText="联系人" />
               <asp:BoundField DataField="LinkAddress" HeaderText="联系地址" />
                    <asp:BoundField DataField="LinkPhone" HeaderText="联系电话" />
              
                <asp:TemplateField HeaderText="快递公司">
                    <ItemTemplate>
                        <asp:TextBox ID="tbExpressName" runat="server" CssClass="EditForm_textbox" Width="100"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="单号">
                    <ItemTemplate>
                        <asp:TextBox ID="tbExpressNum" runat="server" CssClass="EditForm_textbox" Width="200"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfMemberUserName" runat="server" Value='<%# Eval("UserName") %>' />
                      <asp:LinkButton ID="lbtnConfirm" runat="server" Text="确认发货" CommandArgument='<%# Eval("ID") %>' CommandName="ConfirmData"  ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

