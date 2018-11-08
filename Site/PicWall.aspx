<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PicWall.aspx.cs" Inherits="PicWall" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<%@ Import Namespace="Arrow.Framework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <title>图片墙</title>
    <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
     <!--main-start-->
    <div class="main">
    	<div class="container clearfix">
            <div class="title-bar yahei">图片墙<span class="font-s16">PHOTO</span></div>
            
			<div class="abu-content clearfix box_tpq">
                    <div class="photo mt20">
                        <ul>
                         	<% 
                                int pageIndex = GetUrlInt("p");
                                if (pageIndex <= 0)
                                    pageIndex = 0;
                                int recordCount = GetRecordCount();
                                int pageSize = 8;
                                int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                                if (pageIndex > pageCount) pageIndex = pageCount;
                                 %>
                            <%=ShowPics(pageIndex) %>
					
                        </ul>
                    </div>
                </div>
		</div>

        <div style="margin:0 auto;text-align:center;">
        <div class="pagination-v1 page-v1 tac" style="display:inline-block;  ">     
	<form action="PicWall.aspx<%=GetUrlQuery() %>" method="get">
		<%=TMSPager.Show(pageIndex,pageSize, pageCount, 5, recordCount,GetUrlQuery()) %>
		<%--<div class="go-wrap">
			<div class="go-label">转到</div><input type="text" name="p" class="go-input" /><div class="go-unit">页</div><input type="submit" class="button-bg-blue button-font-white go-submit" value="确定"  />
		</div>--%>
	</form>
</div>
            </div>
        
    </div>
    <!--main-end-->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="js/region.js"></script>
	<script src="js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" href="css/jquery.fancybox-1.3.4.css" media="screen" />
    <script>
		$(function () {
			$("a[rel=example_group]").fancybox({
				'transitionIn': 'none',
				'transitionOut': 'none',
				'titlePosition': 'over',
				'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
					return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
				}
			});
		})
	</script>
<script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

