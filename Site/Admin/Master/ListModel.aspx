<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="ListModel.aspx.cs" Inherits="Admin_Master_ListModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以查看列表和相关的操作</div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="width:150px; text-align:left;"><input type="submit" name="ctl00$ContentPlaceHolder1$btnAdd" value="添 加" id="ctl00_ContentPlaceHolder1_btnAdd" class="btnAdd" /></td>
                    <td style="padding-right:150px"> 物料关键字：<input name="ctl00$ContentPlaceHolder1$tbClientName" type="text" id="ctl00_ContentPlaceHolder1_tbClientName" class="EditForm_textbox" />&nbsp;
                        <select name="ctl00$ContentPlaceHolder1$ddlCat" id="ctl00_ContentPlaceHolder1_ddlCat">
	<option value="0">所有分类</option>
	<option value="1">电子元件</option>
	<option value="3">　├电机</option>
	<option value="4">　├电路板</option>
	<option value="7">　├保险管</option>
	<option value="8">　├电容</option>
	<option value="2">零配件</option>
	<option value="5">　├弹簧</option>
	<option value="6">　├铜环</option>
	<option value="10">新的分类</option>

</select>&nbsp;
            <input type="submit" name="ctl00$ContentPlaceHolder1$btnSearch" value="搜 索" id="ctl00_ContentPlaceHolder1_btnSearch" class="btnYellow" />&nbsp;</td>
                </tr>
            </table>
        </div>
    <div class="Line"></div>
    <div class="H10"></div>
    <div>
        
    <div>
	<table class="gridview" cellspacing="0" cellpadding="2" align="Center" id="ctl00_ContentPlaceHolder1_gvData" style="width:100%;border-collapse:collapse;">
		<tr>
			<th scope="col">物料编码</th><th scope="col">物料名称</th><th scope="col">物料分类</th><th scope="col">物料型号</th><th scope="col">物料规格</th><th scope="col">材料</th><th scope="col">物料单位</th><th scope="col">提前期</th><th scope="col">操作</th>
		</tr><tr>
			<td>A0100030</td><td>士大夫</td><td>
                    电子元件-电机
                </td><td>1</td><td>1</td><td>
                    1
                </td><td>&nbsp;</td><td>1</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl02_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl02$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl02_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl02$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr style="background-color:#F8F8F8;">
			<td>A0100028</td><td>士大夫</td><td>
                    电子元件-电机
                </td><td>234</td><td>234</td><td>
                    234
                </td><td>&nbsp;</td><td>22</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl03_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl03$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl03_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl03$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr>
			<td>A0100027</td><td>禅城</td><td>
                    电子元件-电机
                </td><td>B2</td><td>A1</td><td>
                    啊啊
                </td><td>&nbsp;</td><td>22</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl04_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl04$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl04_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl04$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr style="background-color:#F8F8F8;">
			<td>A0100026</td><td>禅城</td><td>
                    电子元件-电机
                </td><td>B1</td><td>A1</td><td>
                    公共
                </td><td>&nbsp;</td><td>22</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl05_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl05$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl05_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl05$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr>
			<td>A0100025</td><td>华盛顿</td><td>
                    电子元件-电机
                </td><td>99</td><td>B1</td><td>
                    啊
                </td><td>&nbsp;</td><td>22</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl06_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl06$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl06_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl06$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr style="background-color:#F8F8F8;">
			<td>A0100024</td><td>华盛顿</td><td>
                    电子元件-电机
                </td><td>A1</td><td>B9</td><td>
                    啊
                </td><td>&nbsp;</td><td>11</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl07_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl07$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl07_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl07$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr>
			<td>A0100023</td><td>华盛顿</td><td>
                    电子元件-电机
                </td><td>A3</td><td>B3</td><td>
                    啊
                </td><td>&nbsp;</td><td>33</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl08_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl08$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl08_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl08$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr style="background-color:#F8F8F8;">
			<td>A0100022</td><td>华盛顿</td><td>
                    电子元件-电机
                </td><td>A2</td><td>B2</td><td>
                    的
                </td><td>&nbsp;</td><td>10</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl09_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl09$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl09_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl09$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr>
			<td>A0100021</td><td>华盛顿</td><td>
                    电子元件-电机
                </td><td>A1</td><td>B1</td><td>
                    的
                </td><td>&nbsp;</td><td>11</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl10_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl10$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl10_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl10$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr><tr style="background-color:#F8F8F8;">
			<td>A0100019</td><td>岁的法国</td><td>
                    电子元件-电机
                </td><td>&nbsp;</td><td>&nbsp;</td><td>
                    
                </td><td>&nbsp;</td><td>5</td><td>
                    <a id="ctl00_ContentPlaceHolder1_gvData_ctl11_lbtnEdit" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl11$lbtnEdit&#39;,&#39;&#39;)">修改</a>
                    <a onclick="return confirm(&#39;确定删除吗？&#39;);" id="ctl00_ContentPlaceHolder1_gvData_ctl11_lbtnDel" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$gvData$ctl11$lbtnDel&#39;,&#39;&#39;)">删除</a>             
                </td>
		</tr>
	</table>
</div><div class="gvPage"><span class='Tips'>共32条  第1/4页</span><a href=?p=1&resid=73>首页</a><a  href=?p=1&resid=73>上页</a> <span class='Current'>1</span> <a href=?p=2&resid=73><span>2</span></a><a href=?p=3&resid=73><span>3</span></a><a href=?p=4&resid=73><span>4</span></a><a href=?p=2&resid=73>下页</a><a href=?p=4&resid=73>末页</a></div>

</div>
    <div class="H20"></div>


</asp:Content>

