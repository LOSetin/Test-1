<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonFooter.ascx.cs" Inherits="CommonFooter" %>
 <div class="bk8"></div>
    <div class="footer ">
    <% var list = new TMS.QA().SelectList("1=1"); %>
    	<div class="ft_nav  clearfix">
        	<dl>
            	<dt>关于我们</dt>
                <dd>
                	<p><a href="Company.aspx">公司简介</a></p>
                </dd>
            </dl>
        	<dl>
            	<dt>网上支付问题</dt>
                <dd>
                	<%=ShowQA(list,QACat.QANetPay) %>
                </dd>
            </dl>
        	<dl>
            	<dt>旅游常见概念解释</dt>
                <dd>
                	<%=ShowQA(list,QACat.QATravelConcept) %>
                </dd>
            </dl>
        	<dl>
            	<dt>签证相关问题</dt>
                <dd>
                	<%=ShowQA(list,QACat.QASign) %>
                </dd>
            </dl>

        </div>
        <div class="copyright">
        	<%=UIHelper.ShowCopyright() %>
        </div>
    </div>