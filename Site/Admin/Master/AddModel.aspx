<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master/MasterPage.master" AutoEventWireup="true" CodeFile="AddModel.aspx.cs" Inherits="Admin_Master_AddModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Info">在这里，您可以添加信息。</div>

     <div class="Edit">
              <table class="EditForm">
             <tr><td class="txt">销售合同名称：</td><td >
            <input name="ctl00$ContentPlaceHolder1$tbContractName" type="text" id="ctl00_ContentPlaceHolder1_tbContractName" class="EditForm_textbox" style="width:200px;" />&nbsp;&nbsp;<span class="EditForm_red">*必填</span>
            </td></tr>
     <tr><td class="txt">销售合同编号：</td><td >
            <input name="ctl00$ContentPlaceHolder1$tbContractGUID" type="text" maxlength="50" id="ctl00_ContentPlaceHolder1_tbContractGUID" class="EditForm_textbox" style="width:200px;" />&nbsp;&nbsp;<span class="EditForm_red">*必填</span>
            </td></tr>
                   <tr><td class="txt">销售合同方：</td><td >
            <select name="ctl00$ContentPlaceHolder1$ddlClient" id="ctl00_ContentPlaceHolder1_ddlClient">
	<option value="1">供应商1111111111111</option>
	<option value="2">供应商22222</option>
	<option value="3">士大夫顶顶顶</option>

</select>&nbsp;&nbsp;<span class="EditForm_red"></span>
            </td></tr>
                   <tr><td class="txt">销售合同类型：</td><td >
                       <select name="ctl00$ContentPlaceHolder1$ddlContractType" id="ctl00_ContentPlaceHolder1_ddlContractType">
	<option value="1">成套设备</option>
	<option value="2">技术改造</option>
	<option value="3">配件</option>
	<option value="4">技术服务</option>
	<option value="5">ss</option>

</select>
            </td></tr>
                    
                    
                    <tr><td class="txt">商务合同名称：</td><td >
        <input name="ctl00$ContentPlaceHolder1$tbBussiness" type="text" id="ctl00_ContentPlaceHolder1_tbBussiness" class="EditForm_textbox" style="width:200px;" />
            </td></tr>
             <tr><td class="txt">商务合同文件：</td><td >
            <input name="ctl00$ContentPlaceHolder1$tbBussinessFile" type="text" id="ctl00_ContentPlaceHolder1_tbBussinessFile" class="EditForm_textbox" style="width:200px;" />&nbsp;&nbsp;<input type="file" name="ctl00$ContentPlaceHolder1$upBusiness" id="ctl00_ContentPlaceHolder1_upBusiness" style="width:200px;" />&nbsp;<input type="submit" name="ctl00$ContentPlaceHolder1$btnBussinessUpload" value="上传" id="ctl00_ContentPlaceHolder1_btnBussinessUpload" />
            </td></tr>
                    <tr><td class="txt">技术合同名称：</td><td >
        <input name="ctl00$ContentPlaceHolder1$tbTech" type="text" id="ctl00_ContentPlaceHolder1_tbTech" class="EditForm_textbox" style="width:200px;" />
            </td></tr>
                   <tr><td class="txt">技术合同文件：</td><td >
            <input name="ctl00$ContentPlaceHolder1$tbTechFile" type="text" id="ctl00_ContentPlaceHolder1_tbTechFile" class="EditForm_textbox" style="width:200px;" />&nbsp;&nbsp;<input type="file" name="ctl00$ContentPlaceHolder1$upTech" id="ctl00_ContentPlaceHolder1_upTech" style="width:200px;" />&nbsp;<input type="submit" name="ctl00$ContentPlaceHolder1$btnTech" value="上传" id="ctl00_ContentPlaceHolder1_btnTech" />
            </td></tr>
                    <tr><td class="txt">补充合同名称：</td><td >
        <input name="ctl00$ContentPlaceHolder1$tbOther" type="text" id="ctl00_ContentPlaceHolder1_tbOther" class="EditForm_textbox" style="width:200px;" />
            </td></tr>
                   <tr><td class="txt">补充合同文件：</td><td >
            <input name="ctl00$ContentPlaceHolder1$tbOtherFile" type="text" id="ctl00_ContentPlaceHolder1_tbOtherFile" class="EditForm_textbox" style="width:200px;" />&nbsp;&nbsp;<input type="file" name="ctl00$ContentPlaceHolder1$upOther" id="ctl00_ContentPlaceHolder1_upOther" style="width:200px;" />&nbsp;<input type="submit" name="ctl00$ContentPlaceHolder1$btnOther" value="上传" id="ctl00_ContentPlaceHolder1_btnOther" />
            </td></tr>
                    <tr><td class="txt">合同备注：</td><td >
            <textarea name="ctl00$ContentPlaceHolder1$tbRemarks" rows="2" cols="20" id="ctl00_ContentPlaceHolder1_tbRemarks" class="EditForm_textbox" style="height:80px;width:300px;">
</textarea>&nbsp;&nbsp;<span class="EditForm_red"></span>
            </td></tr>
        <tr><td class="txt" ></td><td style="padding-top:10px;"><input type="submit" name="ctl00$ContentPlaceHolder1$btnAdd" value="确定添加" id="ctl00_ContentPlaceHolder1_btnAdd" class="btnPrime" />
            &nbsp;<input type="submit" name="ctl00$ContentPlaceHolder1$btnReturn" value="返回" id="ctl00_ContentPlaceHolder1_btnReturn" class="btnGray" />
                                  </td></tr>
        </table>
    </div>
</asp:Content>

