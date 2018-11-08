<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>
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
                	<li class="tj">快速提交</li>
                    <li>编辑参团人</li>
                    <li>商家确认</li>
                    <li>网上支付</li>
                    <li>交易完成</li>       
              </ul>
             </div>
             <!--main-xlyd-->
            <div class="container select-wrap">
		<form class="checkform form-order" id="form1" runat="server">
			<div class="board-wrap">
    <div class="board-title-wrap font-s14"><%=MyLine.Name %></div>
	<div class="board-content-wrap">
		<table class="table-v1 tl board-table" border="0">
			<colgroup>
			<col width="90" />
			<col width="425" />
			<col width="90" />
			<col />
			</colgroup>
			<tbody><tr>
				<th>产品编号</th> 
				<td><%=MyLine.ProductNum %></td>
				<th>旅游天数</th>
				<td><%=MyLine.TravelDays %>天</td>
			</tr>
			<tr>
				<th>出团日期</th>
				<td><%=MyGroup.GoDate.ToDateOnlyString() %></td>
				<th>返程日期</th>
				<td><%=MyGroup.BackDate.ToDateOnlyString() %></td>
			</tr>
                <tr>
				<th>出发地点</th>
				<td><%=MyLine.StartAddress %>&nbsp;</td>
				<th>目的地</th>
				<td><%=MyLine.TargetAddress %>&nbsp;</td>
			</tr>
			<tr>
				<th>去程交通</th>
				<td><%=MyGroup.GoTravel %>&nbsp;</td>
				<th>返程交通</th>
				<td><%=MyGroup.BackTravel %>&nbsp;</td>
			</tr>
                <tr>
				<th>集合地点</th>
				<td><%=MyGroup.GatheringPlace %>&nbsp;</td>
				<th>集合时间</th>
				<td><%=MyGroup.GatheringTime %>&nbsp;</td>
			</tr>
            <tr>
				<th>促销信息</th>
				<td class="orange-color"><%=MyPromotion==null?"":MyPromotion.Name %>&nbsp;</td>
				<th></th>
				<td></td>
			</tr>
			
                       		</tbody></table>
	</div>
</div>
			<br/>

            <h2 class="font-s16 yahei line-h36">购买信息</h2>
            <table class="iframe-lineroad-table mb10 iframe-lineroad-border iframe-lineroad-border-second lineroad-border-v1 big-table-padding">
	<colgroup>
	<col width="100" />
	<col/>
	</colgroup>
	<tbody>
		<tr>
			<th class="title">价格</th>
			<td style="height:35px;">
				￥<asp:Literal ID="ltPrice" runat="server"></asp:Literal> </td>
		</tr>
        <tr>
            <th class="title">购买团位</th>
            <td><asp:DropDownList ID="ddlBuyNum" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyNum_SelectedIndexChanged"></asp:DropDownList> </td>
        </tr>
		<tr>
			<th class="title">总价 </th>
			<td style="height:35px;">
				￥<asp:Literal ID="ltTotal" runat="server"></asp:Literal>
			</td>
		</tr>
	</tbody>
</table>
<br />

			<h2 class="font-s16 yahei line-h36">联系人信息</h2>
<table class="iframe-lineroad-table mb10 iframe-lineroad-border iframe-lineroad-border-second lineroad-border-v1 big-table-padding">
	<colgroup>
	<col width="100" />
	<col/>
	</colgroup>
	<tbody>
		<tr>
			<th class="title">联系人姓名<b class="mh5 red-color">*</b></th>
			<td class="pl10">
                <asp:TextBox ID="tbRealName" runat="server" CssClass="input-skin-1"></asp:TextBox></td>
		</tr>
		<tr>
			<th class="title">联系人电话 <b class="mh5 red-color">*</b></th>
			<td class="pl10">
				<asp:TextBox ID="tbPhone" runat="server" CssClass="input-skin-1"></asp:TextBox>
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
                                <asp:Button ID="btnSubmit" runat="server" Text="快速提交订单" OnClick="btnSubmit_Click" CssClass="button-operation-small button-bg-orange button-font-white action-select-reserved" />
                            
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

