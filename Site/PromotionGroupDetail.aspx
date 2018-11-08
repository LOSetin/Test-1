<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PromotionGroupDetail.aspx.cs" Inherits="PromotionGroupDetail" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
<link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<link rel="stylesheet" href="css/iconfont.css" />
    <link href="DatePicker/css/datepicker.css" rel="stylesheet" />
    
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
    <script>
        $(document).ready(function () {
            pickerEvent.setPriceArr(eval(<%=JsonInfo() %>));
            pickerEvent.Init("calendar");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <!--main-start-->
    <div class="main">
        <form class="all-form" id="form1" runat="server" >
	<div class="container line-distance clearfix">
		<div class="main-left">
			<div class="line-detail bg-white clearfix mb20">
				<div class="tit2"><span>线路名称：</span><%=CurrentLine.Name %></div>
                <div class="bor_xx">
                <dl>
                	<dd class="l line-info">
						<div class="line-price line-price-four">
							<div class="gray ">
                                <span >促销价：<%=ShowPrice(CurrentGroup) %></span></div>
                            </div>
						<div class="line-desc-context">
							
							<ul class="line-import">
                                <li>促销信息：<a class="red" href="PromotionDetail.aspx?PromotionID=<%=GetUrlInt("PromotionID") %>"><%=CurrentGroup.PromotionName %></a></li>
								<li>
                                	<p class="p-item">出团日期：
                                        <asp:DropDownList ID="ddlGoDate" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                	                      
									</p><p class="p-item">返程日期：<span><%=CurrentGroup.BackDate.ToDateOnlyString() %></span></p>
								</li>
								<li>
                                    <p class="p-item">团人数：<span><%=CurrentGroup.TotalNum %></span></p>
                                    <p class="p-item"> 剩余位置：<span><%= CurrentGroup.TotalNum -CurrentGroup.SelledNum%></span></p>
                                </li>
								<li>
                                    <p class="p-item">集合时间：<span><%=CurrentGroup.GatheringTime %>   </span></p>
                                    <p class="p-item">集合地：<span><%=CurrentGroup.GatheringPlace %> </span></p>
                                </li>
								<li>去程交通：
									<span><%=CurrentGroup.GoTravel %></span> 
								</li>
								<li>返程交通：<span><%=CurrentGroup.BackTravel %></span></li>
								
							</ul>
						</div>
					</dd>
					<dt class="fr">
					    <div class="js-changeimg"><img class="preview-picture" src="<%=getRawCover(CurrentLine.CoverPath) %>"></div>
						<div class="preview-box js-show">
						    <a class="btn-prev" href="javascript:;"></a>
							<div class="preview-wrap">
								<ul class="preview-list js-list">
									                                    <li data-url="<%=getRawCover(CurrentLine.CoverPath) %>"><a><img  src="<%=SiteUtility.GetImageUrl(CurrentLine.CoverPath,83,42) %>" /></a></li>
																	</ul>
							</div>
							<a href="javascript:;" class="btn-next"></a>
						</div>
					</dt>
				</dl>
                
                <div class="clearfix">
                	<div class="fl">
                        <div class="line-desc-context">
                            <div class="line-btn-group"> 
                                                                                         
                                 
                             <a href="Order.aspx?LineID=<%=CurrentLine.ID %>&GroupID=<%=CurrentGroupID %>&PromotionID=<%=GetUrlInt("PromotionID") %>" class="btn-oragne btn-font-white btn-book" >立即预订</a> 
                                 
                                <a class="btn-blue btn-font-white line-share-road" href="javascript:;" style="position:relative;"><i class="iconfont">&nbsp;</i>在线分享
                                    <div class="share-imgbox"><img src="picture/common.getqrcode.html"/></div>
                                </a> 
                                                            <a class="btn-primary btn-font-white btn-other" href="print.html?dateid=9427714&takeid=" target="_blank"><i class="iconfont">&nbsp;</i>导出行程</a>
                                
                                </div>
                            
                        </div>
                        
                        <div class="line-introduce clear" >
                            <p id="calendar"  name="calendar" readonly="readonly"  placeholder="点击选择时间"/>
						</div>

                    </div>
                    <div class="fr">
                    	<div class="line-desc-context">
						
                            <div class="line-road-desc">
                                <div class="line-road-desc-h4">
                                       <h4 class="current">报名须知</h4> 
                                       <h4>线路说明</h4>
                                </div>
                                <div class="details-content">
                                    <%=CurrentLine.SignUpNotice %>
                                </div>
                                <div class="contenthide details-content">
                                                                   <%=CurrentLine.LineDesc %>
                                                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                </div>
				
				<div class="clearfix"></div>
				<div class="dist mb20 bg-white mt10">
					<div class="dist-wrap">
						<ul class="dist-list clearfix">
							<li class="menu-tab-category cur">详细行程</li>
							<li class="menu-tab-category" style="display:none;">相关事项</li>	 
													</ul>
					</div>
					<div class="line-border-wrap ">
					<div class="dist-content">
					
                        <table class="details-table details-content">
                            <tbody>

                                <% List<TMS.LineDetailInfo> details = new TMS.LineDetail().SelectList("LineID=" + CurrentLine.ID + " Order by SortOrder");
                                    foreach (TMS.LineDetailInfo model in details)
                                    {
                                        int totalLength = 0;
                                        string[] pics = model.DayPics.Split('|');
                                        for (int i = 0; i < pics.Length; i++)
                                            totalLength += pics[i].Trim().Length;
                                    %>

                                <tr>
                                    <td class="tday"><span>第<%=model.SortOrder %>天</span></td>
                                    <td>
                                        <h3 class="tit-name"><%= IconHelper.Replace(model.Title) %></h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="vt tline"><span></span></td>
                                    <td class="vt tcontent">
                                        <p>
                                           <%=model.DayDesc %>
                                        </p>

                                        <% if (totalLength > 0)
                                                 { %>
                                        <ul class="day-list">
                                            <%for (int i = 0; i < pics.Length; i++)
                                                 { %>
                                            <li>
                                                <img src="<%=SiteUtility.GetImageUrl(pics[i], 170, 130) %>" width="170" height="130" alt="" /></li>
                                            <%} %>
                                        </ul>
                                        <%} %>

                                     <div class="tip mt10"><p>温馨提示：
											   <span><i class="iconfont i_sc" style="font-size:20px;"></i>三餐：<%=model.WarmTips %></span> <span><i class="iconfont i_jd" style="font-size:20px;"></i>酒店：<%=model.Remarks %></span></p>
										   </div>
                                    </td>
                                </tr>
                                <%} %>



                            </tbody>
                        </table>
						</div>
						<div class="dist-content hide">
							<div class="title-line-bar mt20"><i class="iconfont">&#xe617;</i>相关事项</div>
                            							<div class="title-item-bar"><span class="item">报名须知</span></div>
							<p class="item-content"> 
								无 
							</p>
                                                        							<div class="title-item-bar"><span class="item">包含项目</span></div>
							<p class="item-content"> 
								费用包含√<br />
集合地：上海虹桥进站口处<br />
交  通：当地空调旅游车（保证一人一个正座，车型根据人数而定）<br />
酒  店：上饶全程入住挂牌四星（参考和平国际及维多利亚皇家）标准间（出现单人请补房差300元/人）<br />
膳  食：2正餐（十人一桌、八菜一汤）；<br />
门  票：三清山大门票+熹园门票+篁岭门票含往返索道；<br />
导  游：地接导游服务；<br />
往返交通	：上海虹桥-上饶-上海虹桥·高铁二等座票（火车车次时间以确认件为准，我社保留行程根据具体火车车次时间前后调整的权利，景点、标准不变！）<br />
购物点：	无<br />
备  注：此散客为落地散拼团，二、五、七发班，1人起行的。 
							</p>
                                                          
							<div class="title-item-bar"><span class="item">不含项目</span></div>
							<p class="item-content"> 
								费用不包含×<br />
单房差：300元/人。 <br />
门票：行程中未注明所含景点，或是标注自费项目；  <br />
自费项目：龙虎山竹排100元/人、三清山往返缆车125元/人<br />
其他：酒店内洗衣、理发电话、传真、收费电视、饮品、烟酒等个人消费。<br />
因交通延阻、罢工、天气、飞机机器故障、航班取消或更改时间等不可抗力原因所引致的额外费用。 <br />
保险：旅游人身意外保险。<br />
火车站接送费用：我们会在出发前一天确认通知客人最终出团火车车次时间及入住的酒店名称。
							</p>
                             
                              
							<div class="title-item-bar"><span class="item">儿童安排</span></div>
							<p class="item-content"> 
								儿  童：小童（1.2-1.5米）只含当地旅游车位费、导服，门票及住宿自行购买。1.5米以上按成人收费。
							</p>
                             
                                
                            							<div class="title-item-bar"><span class="item">可选项目</span></div>
							<p class="item-content"> 
								三清山往返缆车125元/人
							</p>
                                
                             
							<div class="title-item-bar"><span class="item">注意事项</span></div>
							<p class="item-content"> 
								注意事项：<br />
抵达江西前，导游会电话联系客人，告知客人旅游注意事项及接团地点，请客人在火车上保持手机畅通，请勿关机，更不要误以为是陌生骚扰电话拒绝接听哦，谢谢配合。<br />
1、山区空气潮湿，接待条件无法与城市相比，散客住宿为普通间，如产生单男单女，安排三人间或补房差2、用餐口味南北各地有别，江西地区口味偏辣，景区用餐条件不能和城市相比的，景区用餐强调的是吃饱而不是吃好，如您想品尝当地特色小吃，请向导游咨询位置及价格，以免上当受骗，由于山区特殊情况，用餐时间可能不准时，请适当备点干粮充饥。3、为了保护庐山的自然和空气污染，山上采用公交式景区交通车，数量不多，所以在山上会出现人等车的情况，特别是七八月份及黄金周，山上景区很多地方实行单向行驶，等1-2个小时，旅游团之间抢车是经常的事情，所以建议能走的地方尽量步行，请各位游客做好心里准备，不要因此影响了您的旅途心情！4、行程以当地旅行社安排为准，景点顺序可能有所调整，但不减少景点5、行程当中有需要自理的小门票及索道，当地旅行社不会强制您消费，采取自愿形式，如您不去参观而同行团队中又有人去参观，请你听从导游意见在附近等待，不要走得太远，记好集合时间并准时集合！6、该线路为散客拼团，不安排全陪。 
							</p>
                                                         
							<div class="title-item-bar"><span class="item">其他事项</span></div>
							<p class="item-content"> 
								天气继续炎热，早晨出发时间过早，没有时间吃早餐，请客人自备干粮在车上解决。谢谢谅解 
							</p>
                                                         
							<div class="title-item-bar"><span class="item">温馨提示</span></div>
							<p class="item-content"> 
								1、收客前请来电咨询是否有座位和行程内容是否统一，以免造成给客人的行程与我社行程不一致。<br />
2、旅游期间对我社接待质量未提出疑议，我社视为满意，客人返程后若有疑议，我社不予处理；具备门票优惠政策的游客请带好有效证件，我中心只退还门票折扣差价，当时未出示证件的，我中心不负任何责任。因人力不可抗拒因1、游客游览行程需和我社行程一致，便与我社操作，如若不一致由此造成损失我社不承担责任，签章后作为《国内旅游合同》的组成部分。接待质量以当地意见单为凭证，请您认真填写，如在填写意见单时未注明投诉意见，返回后我社不再接受投诉，谢谢合作！<br />
2、如遇国家政策性调价或游客自身因素，造成的损失和增加的费用由游客自理。<br />
3、为保证游客的人身安全，游客如在遇到人力不可抗拒的因素导致的重大调整、顺延或提前终止时，游客应积极服从旅行社的处理安排，旅游费用多退少补。<br />
4、购物安排：旅游过程中，游客购物属个人行为，请慎重选择。<br />
5、以上景点价格为景点门市价，如游客有军官证、老年证、导游证等有效证件，进景点未产生门票或享受折扣票价的，我社负责按折扣价退还门票费用或门票差价。<br />
6、在不变动景点与住宿情况下，我社可根据实际情况对行程做适当调整<br />
7、因道路交通事故造成游客人身伤害及财产损失，按照《中华人民共和国道路交通事故处理办法》进行赔偿。 
							</p>
                                                                                </div>
					</div>
				</div>
            </div>
        </div>
        </div>
            </form>
    </div>
    <!--main-end-->

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="js/jquery.common-1.0.js"></script>
<script src="js/common.js"></script>
    <script src="DatePicker/js/zlDate.js"></script>
<script src="js/addons.js" type="text/javascript" language="javascript" ></script>
<script>

		$(function() {
			//日期改变跳转
		    $('#ddlGoDate').change(function () {
		        if ($(this).val() != '') {
		            var id = getUrlPara("id");
		            var promotionID = getUrlPara("PromotionID");
		            var lineID = getUrlPara("LineID");
		            var url = "PromotionGroupDetail.aspx?ID=" + id + "&PromotionID=" + promotionID + "&LineID=" + lineID + "&GroupID=" + $(this).val();
		            window.location.href = url;
				}
			});
			
			 /* 线路说明与报名须知切换 */
            $(".line-road-desc-h4").find('h4').on('click',function(){
                var nIndex=$(this).index();
                var $oP = $(this).closest('.line-road-desc').find('.details-content')
                $(this).addClass('current').siblings().removeClass('current');
                $oP.eq(nIndex).removeClass('contenthide').siblings('.details-content').addClass('contenthide');
            });

            //轮播图
            $(".preview-box .btn-prev").on('click',function(){
		     	prevScrollBox($(this))
		    });
		    $(".preview-box .btn-next").on('click',function(){
		    	nextScrollBox($(this))
		    });
		    $(".js-list").find("li").on('click',function() {
		    	showCurrentImg($(this))
		    });
		    function prevScrollBox(ele){
				var box=ele.parent().find('ul');
		     	var curShowBox=ele.parent().prev();
		     	var w=box.find('li').eq(0).outerWidth(true);
		     	box.find('li:last').prependTo(box);
		     	box.css({
		     		'margin-left': -w
		     	});
		     	box.animate({
		     		'margin-left': 0
		     	}, 200, function() {
		     		if (box.find("li").size() > 2 && box.find("li:lt(3).cur").size() == 0) {
		     			SelectedPicture(curShowBox,box,2);
		     		}
		     	});
			}
			function nextScrollBox(ele){
				var box=ele.parent().find('ul');
		    	var curShowBox=ele.parent().prev();
		    	var w=box.find('li').eq(0).outerWidth(true);
		    	console.log(w)
		        box.animate({
		    		"margin-left": -w
		    	}, 200, function() {
		    		box.find('li').eq(0).appendTo(box);
		    		box.css({
		    			'margin-left': 0
		    		});
		    		if (box.find("li").size() > 2 && box.find("li:lt(3).cur").size() == 0) {
		    			SelectedPicture(curShowBox,box,0);
		    		}
		    	})
			}
			function showCurrentImg(ele){
				var curShowBox=ele.closest('.js-show').prev();
		    	var box=ele.parent();
		    	SelectedPicture(curShowBox,box,ele.index());
			}
		    function SelectedPicture(curEle,ele,index) {
		    	var li = ele.find("li").eq(index);
		    	curEle.find("img").attr("src", (li.attr("data-url") || ""));
		    }

			//分享
			$(".line-share-road").hover(function() {
				var imgBox=$(this).find(".share-imgbox");
				imgBox.show();
			}, function() {
				var imgBox=$(this).find(".share-imgbox");
				imgBox.hide();
			});

		
		})


</script>
</asp:Content>

