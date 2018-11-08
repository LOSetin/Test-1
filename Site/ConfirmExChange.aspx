<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ConfirmExChange.aspx.cs" Inherits="ConfirmExChange" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <title>快速提交</title>
        <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

        <div class="main">
    	<div class="container clearfix">
            <br />
            <div class="title-bar1 yahei1">
            	<ul>
                	<li class="tj">申请兑换</li>
                    <li>商家发货</li>
                    <li>确认收货</li>
                    <li>兑换完成</li>       
              </ul>
             </div>
             <!--main-xlyd-->
            <div class="container select-wrap">
		<form class="checkform form-order" id="form1" runat="server">
			<div class="board-wrap">
    <div class="board-title-wrap font-s14">兑换商品：<%=MyGoods.Name %></div>
	<div class="board-content-wrap">
		<table class="table-v1 tl board-table" border="0">
			<colgroup>
			<col width="90" />
			<col width="425" />
			<col width="90" />
			<col />
			</colgroup>
			<tbody><tr>
				<th>礼品编号</th> 
				<td><%=MyGoods.ID %></td>
				<th>库存数量</th>
				<td><%=MyGoods.Num %></td>
			</tr>
			<tr>
				<th>所需积分</th>
				<td><%=MyGoods.Points %> </td>
				<th>我的积分</th>
				<td><%=MemberBLL.GetMemberPoints(CurrentMember.UserName) %> </td>
			</tr>
           
       
			
                       		</tbody></table>
	</div>
</div>
			<br/>

            <h2 class="font-s16 yahei line-h36">兑换信息</h2>
            <table class="iframe-lineroad-table mb10 iframe-lineroad-border iframe-lineroad-border-second lineroad-border-v1 big-table-padding">
	<colgroup>
	<col width="100" />
	<col/>
	</colgroup>
	<tbody>
        <tr>
            <th class="title">兑换数量</th>
            <td><asp:DropDownList ID="ddlBuyNum" runat="server" ></asp:DropDownList> </td>
        </tr>
	</tbody>
</table>
<br />

			<h2 class="font-s16 yahei line-h36">收货人信息</h2>
<table class="iframe-lineroad-table mb10 iframe-lineroad-border iframe-lineroad-border-second lineroad-border-v1 big-table-padding">
	<colgroup>
	<col width="100" />
	<col/>
	</colgroup>
	<tbody>
		<tr>
			<th class="title">收货人姓名<b class="mh5 red-color">*</b></th>
			<td class="pl10">
                <asp:TextBox ID="tbRealName" runat="server" CssClass="input-skin-1"></asp:TextBox></td>
		</tr>
		<tr>
			<th class="title">收货人电话 <b class="mh5 red-color">*</b></th>
			<td class="pl10">
				<asp:TextBox ID="tbPhone" runat="server" CssClass="input-skin-1"></asp:TextBox>
			</td>
		</tr>
        	<tr>
			<th class="title">收货人地址 <b class="mh5 red-color">*</b></th>
			<td class="pl10">
				<asp:TextBox ID="tbAddress" runat="server" CssClass="input-skin-1" Width="450"></asp:TextBox>
			</td>
		</tr>
	</tbody>
</table>
			<br/>
        
		     
			<div class="no-see-box">
				<table width="100%" class="layer-table-box table-v1 table-default-skin" id="wrap-show-reserved">
					<tbody>
                        <tr>
							<td colspan="2" align="center">
                                <asp:Button ID="btnSubmit" runat="server" Text="申请兑换" OnClick="btnSubmit_Click" CssClass="button-operation-small button-bg-orange button-font-white action-select-reserved" />
                            
						</td>
                        </tr>
					</tbody>
				</table>
			</div>
		</form>
	</div>
             <!--main-xlyd-end-->
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
        <script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

