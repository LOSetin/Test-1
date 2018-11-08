<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberModifyData.aspx.cs" Inherits="MemberModifyData" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <link href="css/myfix.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
        <div class="main1">
            <form id="form1" runat="server">
    	<div class="containerl clearfix">
             <!--main-orders-->
            <div class="containerl select-wrap">
            	<div class="ordersnav1">
                    	<ul>
                          <li class="hy">会员中心</li>
                    	 <%=ShowMenu() %>
                    	  
                  	    </ul>
                </div>
              <div class="information mt18 ml15">
                      <div class="titlebiao">
                        <ul>
                        	<li class="ti"><a href="#">会员中心</a></li>
                            <li><a href="#">修改资料</a></li>
                        </ul>
                      </div>
                     <div class="czsm"> <h3>操作说明：</h3>
                   1.此处，您可以修改个人资料。

                     </div>

                  <div id="jsMsg"></div>

                </div>
              
               
                                  <div class="information mt18 ml15">
                  <!--biao-start-->
     
                            <table width="1050" class="gridtable1">
                            <tr>
                                <th class="w150">真实姓名：</th><td><asp:TextBox ID="tbRealName" runat="server" CssClass="import w200" Width="200"></asp:TextBox><span><asp:Literal ID="ltMsg1" runat="server"></asp:Literal> </span></td>
                            </tr>
                           
                            <tr>
                                <th class="w150">联系电话：</th><td><asp:TextBox ID="tbPhone" runat="server" CssClass="import w200" Width="200"></asp:TextBox><span><asp:Literal ID="ltMsg2" runat="server"></asp:Literal> </span></td>
                            </tr>
                            <tr>
                                <th class="w150">性&nbsp;&nbsp;&nbsp;别：</th><td><asp:DropDownList ID="ddlSex" runat="server" CssClass="import">
                                    <asp:ListItem Value="男" Text="男"></asp:ListItem>
                                    <asp:ListItem Value="女" Text="女"></asp:ListItem>
                                                                               </asp:DropDownList>
                                                                        </td>
                            </tr>
                            <tr>
                                <th class="w150">邀请码：</th><td style="text-align:left;"><asp:Literal ID="ltInviteCode" runat="server"></asp:Literal> </td>
                            </tr>
                           
                            <tr>
                                <th class="w150">QQ：</th><td><asp:TextBox ID="tbQQ" runat="server" CssClass="import w200" Width="200"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th class="w150">微&nbsp;信：</th><td><asp:TextBox ID="tbWeiXin" runat="server" CssClass="import w200" Width="200"></asp:TextBox></td>
                            </tr>
                                <tr><th class="w150"></th>
                                    <td><asp:Button ID="btnConfirm" runat="server" Text="修改资料" CssClass="button-operation-small button-bg-darkblue button-font-white" OnClick="btnConfirm_Click" /> </td>
                                </tr>
                             
                            </table>
                            
                  <!--biao-end-->
                  </div> 



            <div class="bk10"></div>
                 <!--biao-end-->
            </div>
             <!--main-orders-end-->
        </div>
        </form>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
 <asp:Literal ID="ltScript" runat="server"></asp:Literal>
</asp:Content>

