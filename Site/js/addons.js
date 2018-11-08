

$(function(){
	$('.abu-content .photo ul li:nth-child(4n)').css('margin-right','0');
	$('.hot_mdd .body dl dt:first-child').addClass('first');
	$(".show-more").on('click',function(){
		 var $oUl=$(this).prev();
		 var $oLis=$oUl.find('.lihide');
		 if ($(this).attr('date')) {
			$(this).html('显示更多<i class="iconfont more-icon">▼</i>');
			$oLis.hide();
			$(this).removeAttr('date');
		 } else {
			$(this).html('收起更多<i class="iconfont more-icon on">▲</i>');
			$oLis.show();
			$(this).attr('date', '1');
		 }
	}); //旅游路线


$(function(){
$(".hot_mdd .body dl dt").hover( 
function(){ 
$(this).addClass("act").siblings("dt").removeClass("act"); 
$(this).next("dd").addClass("act").siblings("dd").removeClass("act");
},
function(){
$(this).removeClass("act");
$(this).next("dd").removeClass("act");
}
);
});
$(function(){
$(".hot_mdd .body dl dd").hover( 
function(){ 
$(this).prev('dt').addClass("act").siblings("dt").removeClass("act");
$(this).addClass("act").siblings("dd").removeClass("act");
},
function(){
$(this).prev('dt').removeClass("act");
$(this).removeClass("act");
}
);
});
//下拉展开

	$(".line-show").click(function() {
		$(this).addClass("hide");
		var li = $(this).closest("li");
		li.find(".line-hide").removeClass("hide");
		li.find(".details-wrap").removeClass("hide");
		li.find(".line-traffic-box").show();
	});
	$(".line-hide").click(function() {
		$(this).addClass("hide");
		var li = $(this).closest("li");
		li.find(".line-show").removeClass("hide");
		li.find(".details-wrap").addClass("hide");
		li.find(".line-traffic-box").hide();
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
	/* 线路说明与报名须知切换 */
	$(".line-road-desc-h4").find('h4').on('click',function(){
		var nIndex=$(this).index();
		var $oP = $(this).closest('.line-road-desc').find('.details-content')
		$(this).addClass('current').siblings().removeClass('current');
		$oP.eq(nIndex).removeClass('contenthide').siblings('.details-content').addClass('contenthide');
	});

	//分享
	$(".line-share-road").hover(function() {
		var imgBox=$(this).find(".share-imgbox");
		imgBox.show();
	}, function() {
		var imgBox=$(this).find(".share-imgbox");
		imgBox.hide();
	});
		
		
		
});
	

		


$(".search .all-list a").click(function () {
	var category = parseInt($(this).attr('data-val')) || 1;
	$(".search .search-category").html($(this).text()+'<i class="iconfont"></i>').attr("data-val",category);
	if (category == "1") {
		$(".search-input").attr("placeholder", "请填写线路关键字，如“三亚五日游”");
	}
	if (category == "2") {
		$(".search-input").attr("placeholder", "请填写商家名称关键字");
	}
	if (category == "3") {
		$(".search-input").attr("placeholder", "请填写资讯信息关键字");
	}
	$(".submit-search").attr("data-val", category);
	$(".all-list").hide();
});//搜索框左侧改变文本款提示

$(".all-product").click(function (e) {
        if ($(e.target).closest(".all-list").size() > 0) return;
        $(".all-list", this).toggle();
        $(document).unbind("click.search").bind("click.search", function (e) {
            if (!$(".all-list").is(":hidden") && $(e.target).closest(".all-product").size()==0) {
                $(".all-list").hide();
            }
        })
    });//搜索框模拟下拉


$(function(){
	$(".hot_mdd").hover( 
		function(){ 
		$(this).addClass("on");
	},
	function(){
		$(this).removeClass("on");
	}
	);
});//首页下拉展开






jQuery(".banner").slide({titCell:".head li",mainCell:".body ul",autoPlay:true}); //首页页焦点图

jQuery(".dqrm_hot").slide({titCell:".hd li",mainCell:".bd ul",effect:"left"});//首页当期热卖

jQuery(".idx_rmph .body").slide({titCell:".hd li",mainCell:".bd",effect:"left"});//首页热卖排行

jQuery(".box_mould").slide({titCell:".head li",mainCell:".body ",trigger:"click"});//盒子模板

//详情页行程和事项tab
$(".dist .dist-wrap .menu-tab-category ").click(function() {
	$(this).toggleClass("cur").siblings(".menu-tab-category ").removeClass("cur");
	$(".dist .line-border-wrap > .dist-content").eq($('.dist .dist-wrap .menu-tab-category ').index(this)).removeClass("hide").siblings(".dist .line-border-wrap > .dist-content").addClass("hide");
});
//旅游线路排序
$('.sort-item').click(function(){
		$(this).addClass('on');	
		if($(this).hasClass('sort-category-1')){
			$(this).removeClass('sort-category-1');	
			$(this).removeClass('sort-category-3');	
			$(this).addClass('sort-category-2');
			
		}							  
		else{
			$(this).addClass('sort-category-1');
			$(this).removeClass('sort-category-2');		
		}
		$(this).siblings('.sort-item').removeClass('on');		
	})

