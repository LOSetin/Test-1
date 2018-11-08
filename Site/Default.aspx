<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
       <!--main-start-->
    <div class="main wrap">
        <!--幻灯-->
    	<div class="banner">
                <%=showSlider() %>
        </div>
         <!--幻灯结束-->

        
        <!--热卖-->
        <div class="clearfix">
        	<div class="idx_dqrm box_mould">
            	<div class="head">
                	<span>当期热卖</span>
                    <ul class=" clearfix">
                    	<%=showHotSell() %>
                    </ul>
                </div>
                <div class="body">
                    <%=showHotSellItem() %>
                </div>
            </div>
            
            <div class="idx_rmph">
            	<div class="head">
                	最近热卖排行
                </div>
                <div class="body">
                	<%=showHotSellSort() %>
                </div>
            </div>
        </div>
        <!--热卖结束-->
        
        <div class="idx_gny box_mould">
            <%if (lineCatsShowIndex().Count >= 1)
                {%>
            <%=showModule(lineCatsShowIndex()[0]) %>
            <%} %>

        </div>
        
        <div class="box_mould idx_zby">
           <%if (lineCatsShowIndex().Count >= 2)
                {%>
            <%=showModule(lineCatsShowIndex()[1]) %>
            <%} %>
    </div>
    <!--main-end-->


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
  <script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

