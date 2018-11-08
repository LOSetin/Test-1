﻿<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsEdit.aspx.cs" Inherits="GoodsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info"><asp:Literal ID="ltTips" runat="server"></asp:Literal> </div>

    <div class="H20"></div>
        
    <div class="Edit">
        <table class="EditForm">
            <tr>
                <td class="txt">商品名称：</td>
                <td>
                    <asp:TextBox ID="tbName" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>
            <tr>
                <td class="txt">商品分类：</td>
                <td>
                    <asp:DropDownList ID="ddlCat" runat="server"></asp:DropDownList> </td>
            </tr>
            <tr>
                <td class="txt">商品数量：</td>
                <td>
                    <asp:TextBox ID="tbNum" runat="server" CssClass="EditForm_textbox" Width="200px"></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>
            <tr>
                <td class="txt">兑换点数：</td>
                <td>
                    <asp:TextBox ID="tbPoints" runat="server" CssClass="EditForm_textbox" Width="200px"></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填，需要多少点数才能兑换</span></td>
            </tr>
            <tr>
                <td class="txt">商品图片：</td>
                <td>
                    <asp:TextBox ID="tbCoverPath" runat="server" CssClass="EditForm_textbox" Width="200px" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;
                    <asp:FileUpload ID="upCover" runat="server" Width="200"  />&nbsp;<asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" />
                </td>
            </tr>
           
          
            <tr>
                <td class="txt">商品说明：</td>
                <td>
                    <asp:TextBox ID="tbRemarks" runat="server" TextMode="MultiLine" CssClass="EditForm_textbox" Width="300" Height="100"></asp:TextBox>&nbsp;&nbsp;</td>
            </tr>
           
            <tr>
                <td class="txt"></td>
                <td style="padding-top: 10px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="确定修改" CssClass="btnPrime" OnClick="btnSubmit_Click" />
                    &nbsp;
                    <asp:Button ID="btnClean" runat="server" Text="返回" CssClass="btnGray" OnClick="btnClean_Click" />
                </td>
            </tr>
        </table>
    </div>
     
    <div class="H20"></div>


</asp:Content>

