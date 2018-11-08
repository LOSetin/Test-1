<%@ Page Title="QA" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="QA.aspx.cs" Inherits="QA" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <title>QA</title>
    <link href="css/myfix.css" rel="stylesheet" />
    <script src="Js/showList.js" type="text/javascript"></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

    <script type="text/javascript">
		jQuery(".nav1").slide({ 
				type:"menu", 
				titCell:".xia", 
				targetCell:".sub", 
				delayTime:0, 
				triggerTime:0, 
				returnDefault:true 
			});
	</script> 
               
    <% var items = new TMS.QA().SelectList("1=1"); %>
    
    <!--main-start-->
    <div class="main1">
    	<div class="containerl clearfix">
             <!--main-orders-->
            <div class="left-sider">
      <div class="operate">
        <h3> 常见问题 </h3>
        <ul id="J_navlist">
          <li >
            <h4 >网上支付问题</h4>
            <%=ShowQA(items,QACat.QANetPay) %>
          </li>
          <li >
            <h4 >旅游常见概念解释</h4>
              <%=ShowQA(items,QACat.QATravelConcept) %>
          </li>
          <li >
            <h4 >签证相关问题</h4>
            <%=ShowQA(items,QACat.QASign) %>
          </li>       
        </ul>
            <script type="text/javascript" language="javascript">
			    navList(<%=Request.QueryString["ID"]%>);
		    </script>
      </div>
    </div>


                 <!--information-start-->
                  <div class=" right-sider ml15">
                   <div class="titlebiao">
                        <ul>
                        	<li class="ti"><a href="#">常见问题</a></li>
                            <li><a href="#"><%=Request.QueryString["cat"] %></a></li>
                            
                        </ul>
                      </div>
                   
                  		<div class="t1">
                        	<h1><%=CurrentQA.QATitle %></h1>
                        </div>
                        
                  <!--biao-start-->
       <div class="search_main">
                            <div class="search_txt">
                                <%=CurrentQA.QAContent %>
                            </div>
                        </div>

                
                          
              
                  <!--biao-end-->
                  </div> 
                 <!--information-end-->
            </div>
             <!--main-orders-end-->
        </div>
        


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

