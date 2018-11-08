<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberOrderPeopleEdit.aspx.cs" Inherits="MemberOrderPeopleEdit" %>
<%@ Import Namespace="TMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <link href="css/myfix.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
     <!--main-start-->
    <div class="main1">
        <form id="form1" runat="server">
    	<div class="containerl clearfix">
             <!--main-orders-->
            <div class="containerl select-wrap">
            	<div class="ordersnav1">
                    	<ul>
                          <li class="hy">会员中心</li>
                            <asp:Literal ID="ltMenu" runat="server"></asp:Literal>
                    	 <%=ShowMenu("MemberOrderPeopleEdit.aspx") %>

                  	    </ul>
                </div>
              <div class="information mt18 ml15">
                      <div class="titlebiao">
                        <ul>
                        	<li class="ti" ><a href="MemberIndex.aspx">会员中心</a></li>
                            <li class="ti"><a href="MemberOrder.aspx">我的订单</a></li>
                            <li ><a href="#">编辑参团人</a></li>
                        </ul>
                      </div>
                     <div class="czsm"> <h3>操作说明：</h3>
                         在本页，您可以编辑参团人信息。<br />
                         若你订了3个团位，则必须填满3个参团者信息。<br />
                         如果参团者资料错误，您可以与管理员联系修改。
                     </div>

                  <%
                      var order = new V_OrderInfo();
                      var orderList = new TMS.V_Order().SelectList("OrderNum='" + OrderNum + "'");
                      if (orderList.Count == 1)
                          order = orderList[0];
                       %>

                  <div class="t1">
                        	<h1>编辑参团人</h1>
                        </div>
      
                            <table width="1050" class="gridview">
                            <tr>
                                <th class="w150">线路名：</th><td><a target="_blank" href="LineDetail.aspx?id=<%=order.LineID %>"><%=order.LineName %></a> </td>
                            </tr>
                           
                            <tr>
                                <th class="w150">促&nbsp;&nbsp;&nbsp;销：</th><td><%=order.PromotionName %></td>
                            </tr>
                            <tr>
                                <th class="w150">单&nbsp;&nbsp;&nbsp;价：</th><td><%=order.InnerPrice %></td>
                            </tr>
                                <tr>
                                <th class="w150">团&nbsp;&nbsp;&nbsp;位：</th><td><%=order.BuyNum %></td>
                            </tr>
                            <tr>
                                <th class="w150">总&nbsp;&nbsp;&nbsp;价：</th><td><%=order.TotalMoney %></td>
                            </tr>
                           
                           
                            </table>
     
                              <div>  
                                  <br />
                                  <p class="actionbutton"><asp:Button ID="tbUpdate" runat="server" Text="确认参团者资料并提交" OnClick="tbUpdate_Click" CssClass="button-operation-small button-bg-darkblue button-font-white" /> </p>
                                  <br />
                               <div>
       <Arrow:GridView ID="gvData" runat="server"
           AutoGenerateColumns="false" Width="1050" CssClass="gridview" >
           <Columns>
               <asp:TemplateField HeaderText="真实姓名">
                   <ItemTemplate>
                       <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                       <asp:TextBox ID="tbRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="性别">
                   <ItemTemplate>
                         <asp:DropDownList ID="ddlSex" runat="server" >
                             <asp:ListItem Value="男" Text="男"></asp:ListItem>
                             <asp:ListItem Value="女" Text="女"></asp:ListItem>
                         </asp:DropDownList>
                       <asp:HiddenField ID="hfSex" runat="server" Value='<%# Eval("Sex") %>' />
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="身份证">
                   <ItemTemplate>
                         <asp:TextBox ID="tbIDNum" runat="server" Text='<%# Eval("IDNum") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                   <ItemTemplate>
                         <asp:TextBox ID="tbPhone" runat="server" Text='<%# Eval("MobileNum") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                
           </Columns>
       </Arrow:GridView>
    </div>

                 </div>
              


                </div>
            <div class="bk10"></div>

            </div>
             <!--main-orders-end-->
        </div>
        </form>
    </div>
    <!--main-end-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

