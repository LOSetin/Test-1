//banner滚动JS
$.fn.YEXfocus = function (opts) {
    var opt = $.extend({
        speed: 5000,
        direction: 'left',
        eventType: 'click',
        theme: 'number',
        isPre: false,
        isOpacity: false,
        isTotal: false,
        isAutoPlay: true
    }, opts || {});
    return this.each(function () {
        var o = $(this);
        var sWidth = o.width(); //获取焦点图的宽度（显示面积）
        var sHeight = o.height(); //获取焦点图的宽度（显示面积）
        var len = $("ul li", o).length; //获取焦点图个数
        if (len < 2) { return; }
        var lis = $("img", o);
        var index = 0;
        var picTimer;

        //添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
		if (o.find("ol").length <= 0) {
			var btn = "<ol class='btn'>";
			for (var i = 0; i < len; i++) {
				switch (opt.theme) {
					case 'title':
						btn += '<li>' + lis.eq(i).attr('alt') + '</li>';
						break;
					case 'thumb':
						btn += '<li><img src="' + lis.eq(i).attr('src') + '"/></li>';
						break;
					case 'number':
						btn += '<li>' + (i + 1) + '</li>';
						break;
				}
			}
			btn += "</ol>";
			o.append(btn);
		}

        //为小按钮添加鼠标滑入事件，以显示相应的内容
        $(".btn li", o).mouseenter(function () {
            index = $(".btn li", o).index(this);
            showPics(index);
        }).eq(0).trigger("mouseenter");


        //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
        if (opt.isAutoPlay) {
            o.hover(function () {
                clearInterval(picTimer);
            }, function () {
                picTimer = setInterval(function () {
                    showPics(index);
                    index ++;
                    if (index == len) { index = 0; }
                }, opt.speed);
            }).trigger("mouseleave");
        }
        //显示图片函数，根据接收的index值显示相应的内容
        function showPics(index) { //普通切换
			if (opt.direction == 'top') {
				var nowTop = -index * sHeight;
				//$("ul", o).stop(true, false).animate({ "top": nowTop }, 500);
				$("ul", o).css('opacity', 0).css('top', nowTop).animate({opacity:'1'},500);
			} else {
				var nowLeft = -index * sWidth; //根据index值计算ul元素的left值
				$("ul", o).stop(true, false).animate({ "left": nowLeft }, 500); //通过animate()调整ul元素滚动到计算出的position
			}
			$(".btn li", o).stop(true, false).removeClass('select').eq(index).addClass('select');
		
        }
    })
}
//新闻上下滚动
function infoAutoScroll(obj, h, op, ob){
	var op = arguments[2] || 'ul';
	var ob = arguments[3] || 'li';
	$(obj).find(op + ":first").animate({
		marginTop:-h
	}, 500, function(){
		$(this).css({marginTop:0}).find(ob + ":first").appendTo(this);
	});
}

function getStateAndPerson(n, s, y){
	switch(s) {
		case 1:
			return '停止';
		case 2:
			return '客满';
		case 3:
			return '截止';
	}
	if (y == 1) {
		return parseInt(n) > 0 ? '余 ' + n : '客满';
	} else {
		return parseInt(n) > 9 ? '充足' : (parseInt(n) > 0 ? '余 ' + n : '客满');
	}
}

//登录
$(function () {
    $(".quickbutton").hover(function () { $(this).find(".service,.slider-position,.white-arrow").show(); }, function () { $(this).find(".service,.slider-position,.white-arrow").hide(); });
    $(".nav-list li").hover(function () {
        $(this).find(".sub-nav").show();
    }, function () {
        $(this).find(".sub-nav").hide();
    })

    $(".search .all-list a").click(function () {
        var category = parseInt($(this).attr('data-val')) || 1;
        $(".search .search-category").html($(this).text()+'<i class="iconfont">&#xe645;</i>').attr("data-val",category);
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
    });

    $(".search-input").keyup(function (e) {
        var key = e || event;
        var currkey = key.keyCode || key.charCode;
        if (currkey == "13") {
            submitSearch();
        }
    })

    $(".app-mask-close").click(function(){
        $(this).closest('.app-mask-wrap').slideUp(500);
    });


    var sWidth = $(".companypic ul li").width();
    var allWidth = $(".companypic").width();
    var len = $(".companypic ul li").length;
    var comindex = 0;
    var picTimer;
    $("#company_left").click(function () {
        comindex -= 1;
        if (comindex == -1) { comindex = len-1; }
        showPics(comindex);
    });
    $("#company_right").click(function () {
        comindex += 1;
        if (comindex == len) { comindex = 0; }
        showPics(comindex);
    });
    $(".companypic ul").css("width", sWidth * (len));
    $(".companypic").hover(function () {
        clearInterval(picTimer);
    }, function () {
        picTimer = setInterval(function () {
            showPics(comindex);
            comindex++;
            if (comindex >= len) { comindex = 0; }
        }, 4800);
    }).trigger("mouseleave");
    function showPics(comindex) {
        var nowLeft = -comindex * sWidth;
        $(".companypic ul").stop(true, false).animate({ "left": nowLeft }, 300);
    }
    //页面加载完成启动函数放下面，其他放上面
    $(".bannerimg").YEXfocus({direction:'top'});
    $(".company").YEXfocus({direction:'left'});
    var noteTimer = setInterval("infoAutoScroll('.note', 27)", 3000);
    $(".note a").hover(function(){clearInterval(noteTimer)},function(){noteTimer = setInterval("infoAutoScroll('.note', 27)", 3000);})
    var infoTimer = setInterval("infoAutoScroll('.info', 27)", 3000);
    $(".info a").hover(function(){clearInterval(infoTimer)},function(){infoTimer = setInterval("infoAutoScroll('.info', 27)", 3000);})


    //商家图片滑动
    $(".companypic li a").mouseenter(function(){
        $(this).find("span").show();
    });
    $(".companypic li a").mouseleave(function(){
        $(this).find("span").hide();
    });
    //地区选择
    $(".line-head span").hover(function(){$(this).find(".sites").show()},function(){$(this).find(".sites").hide()});             
    //地区line选择
    $(".linewhere li").click(function(){
        var index= $(".linewhere li").index($(this));
        $(".linewhere li").removeClass("cur");
        $(this).addClass("cur");
        $(".allline").removeClass("show");
        $(".allline").eq(index).addClass("show");
    });

    $(".all-product").click(function (e) {
        if ($(e.target).closest(".all-list").size() > 0) return;
        $(".all-list", this).toggle();
        $(document).unbind("click.search").bind("click.search", function (e) {
            if (!$(".all-list").is(":hidden") && $(e.target).closest(".all-product").size()==0) {
                $(".all-list").hide();
            }
        })
    });
    //$(".button-change-site").unbind("click").bind("click",function (e) {
    //    var dl = $(this).find("dl");
    //    dl.toggleClass("hide");
    //    if (!dl.hasClass("hide")) {
    //        $(document).unbind("click.site").bind("click.site", function (e) {
    //            if ($(e.target).closest(".button-change-site").size() == 0 || ($(e.target).closest("dl").size()!=0 && $(e.target).closest("dl")== dl)) {
    //                dl.addClass("hide");
    //            }
    //        })
    //    } else {
    //        $(document).unbind("click.site");
    //    }
    //})

    $(".slider-compare-button").hover(function () { $(this).addClass("cur"); }, function () { $(this).removeClass("cur"); });

	$(document).on("click",".compare-item-delete",function () {
	    var item = $(this).closest(".compare-item");
	    var id = item.attr('data-id') || 0;
	    item.remove();
	    var len = $(".compare-list .compare-item").size();
	    if (len <= 0) {
	        $(".slider-compare-button").removeClass("cur");
	    }
	    $.post("line.ajaxdelCookie.html", { id: id }, function (result) {
	        if (result == 200) {
	            layer.msg("取消成功", 1, 1);
	        } else {
	            layer.msg(result, 1, 13);
	        }

	    }, "text");
	});

	$(document.body).on("click", ".line-compare-add", function () {
	    if ($(this).hasClass("user-login-pop") || $(this).hasClass("user-login-dialog")) { return;}
	    var id = $(this).attr("data-id") || 0, title = $(this).attr("data-title") || "", href = $(this).attr("data-href") || "";
	    $.post("line.ajaxsetcookie.html", { id: id }, function (result) {
	        if (result == 200) {
	            layer.msg("加入成功", 2, 1);
	            var li= $("<li>", { "class": "compare-item","data-id":id, html: [$("<a>", { "href": href, text: title, title: title }), $("<i>", { "class": "iconfont compare-item-delete", html: "&#xf0020;" })] })
	            $(".slider-compare .compare-list").append(li);
	            $(".slider-compare-button").addClass("cur");

	        } else {
	            layer.msg(result, 2, 13);
	        }
	    }, "text")
	});
	!!$(".slider-compare").size() && $.post("line.ajaxright.html", {}, function (result) {
	    var list = [];
	    $.each(result, function () {
	        $(".slider-compare .compare-list").append($("<li>", { "class": "compare-item","data-id":this.id, html: [$("<a>", { "href": "line.details.html?dateid=" + this.id, text: this.title, title: this.title }), $("<i>", { "class": "iconfont compare-item-delete", html: "&#xf0020;" })] }));
	    });
	    //result.length>0&&$(".slider-compare-button").addClass("cur");
	}, "json")
	

	$(".btn-compare-clearall").click(function () {
	    $.post("line.delallcookies.html", {}, function (result) {
	        if (result.Status == "1") {
	            $(".compare-list").empty();
	        }
	    }, "json");
	});
    $(".btn-compare-submit").click(function(e){
        var $aLi=$(".compare-list").find("li");
        if($aLi.length<2){
             e.preventDefault();
             layer.msg("至少两条线路才可以比价",1,13);
        }else{
             window.location.href="line.contrast.html";
        }
    })

    //收藏商家
	$(".action-favourite-company-add").click(function () {
	    var id = $(this).data('id');
	    $.post("line.ajaxFavouriteCompany.html", { id: id }, function (result) {
	        if (result == 200) {
	            layer.msg("收藏成功", 2, 9);
	        } else {
	            layer.msg(result, 2, 13);
	        }

	    }, "text");
	});

	$(".m-gotopbtn").click(function () {
	    $('html,body').animate({ scrollTop: '0px' }, 500);
	});

	$(".submit-search").click(function () {
	    submitSearch();
	});
    //登录弹窗
	$(document.body).on('click',".user-login-dialog", function (e) {
	    var pagei = $.layer({
	        type: 1,   //0-4的选择,
	        title: false,
	        border: [0],
	        fix: true,
	        closeBtn: [0],
	        shadeClose: true,
	        area: ['315px', '297px'],
	        page: {
	            html: $(".user-login").tmpl()[0].outerHTML
	        }
	    });
	    $("#djtop").click(function () {
	        layer.close(pagei);
	    });

        return false;
	});
    //登录弹窗
    $(".user-login-pop").on('click',function(e){
        e.preventDefault();
        var pagei = $.layer({
            type: 1,   //0-4的选择,
            title: false,
            border: [0],
            fix: true,
            closeBtn: [0],
            shadeClose: true,
            area: ['315px', '297px'],
            page: {
                html:  $(".user-login").tmpl()[0].outerHTML
            }
        });
        $("#djtop").click(function () {
            layer.close(pagei);
        });
        return false;
    });
    $(".user-login .close").on('click',function(){
            layer.closeAll();
    });
    //获取金币
     $.getJSON("/coins.ajaxdetail.html",function(result){
         $(".footer-link-back").attr({"href":result.url,"target":"_blank"});
         $(".footer-link-back").text("获奖名单");
     });

     var layerindex;
     $(document).on("mouseenter",".login-error",function () {
         layerindex = layer.tips(
         '<p style="color:#fff;">如遇个别无法登录情况，请尝试如下操作：<br>第一步：清除浏览器缓存<br>第二步：关闭浏览器<br>第三步：重新打开浏览器，输入用户名、密码，重新登录。<br>如有疑问，请致电：400-626-0126</p>', this, {
             time: 0,
             style: ['background-color:#3477aa;', '#3477aa'],
             maxWidth: 260,
             guide: 1
         });
     })
     $(document).on("mouseleave",".login-error",function () {
         layer.close(layerindex);
     });

     $(document).on("mouseenter", ".pay-deductible-1,.pay-deductible-2", function () {
         var content = $(this).attr("data-content") || "…";
         layerindex = layer.tips(
         content, this, {
             time: 0,
             style: ['background-color:#fff;border-color:#c5c5c5;', '#fff;'],
             maxWidth: 260,
             guide: 0
         });
     })
     $(document).on("mouseleave", ".pay-deductible-1,.pay-deductible-2", function () {
         layer.close(layerindex);
     });

     $(document).on("mouseenter", ".date-picker-dialog a[href^='/line.details.html'],.calendar-wrap a[href^='/line.details.html']", function () {
         var content = $(this).attr("data-title") || "";
         if (!content.length) return;
         layerindex = layer.tips(
         content, this, {
             time: 0,
             style: ['background-color:#fff;border-color:#c5c5c5;', '#fff;'],
             maxWidth: 260,
             guide: 1,
             zindex:10
         });
     })
     $(document).on("mouseleave", ".date-picker-dialog a[href^='/line.details.html'],.calendar-wrap a[href^='/line.details.html']", function () {
         layer.close(layerindex);
     });
});

function submitSearch() {
    var action = "line.html?kwtp=1";
    var ltype = $(".search-category").attr("data-val");

    if (ltype == '1') {
        action = 'line.html?kwtp=1';
    } else if (ltype == '2') {
        action = 'company.html?kwtp=2';
    } else if (ltype == '3') {
        action = 'news.html?kwtp=3';
    }
    $(".searchform").attr("action", action);
    $('.searchform').submit();
}

var setTop = {
    init: function (el, wrap, css) {
        css = css || "fixed";
        $(window).scroll(function () {
            sctop = (document.documentElement.scrollTop || document.body.scrollTop);
            obtop = $(el).offset().top;
            if (sctop > obtop) {
                !$(wrap).hasClass(css) && $(wrap).addClass(css);
            } else {
                $(wrap).hasClass(css) && $(wrap).removeClass(css);
            }
        });
    }
};


var etu6 = {
    psh: null,
    count:0,
    hrefTextPrefix:'',
    total:0,
    p:1,
    ps:20,
    initParams: function() {
		if (etu6.count++ > 9) {
			clearInterval(etu6.psh);
		} else {
			if (etu6.hasOwnProperty("params") && etu6.params != "") {
				$.each(etu6.params, function(k, v) {
					if (k == 'stype') {
						$('select[name=stype]').val(v);
					} else if (k == 'b') {
                        $('select[name=b]').val(v);
                    } else if (k == 'kw') {
						$("input[name=kw]").val(v);
					} else if (typeof $("select[name=" + k + "]") != "undefined" && $("select[name=" + k + "]").is("select")) {
						var promitive = $("select[name=" + v + "]:eq(0)").val();

						if (promitive == "" && v === promitive) {
							$("select[name=" + k + "]").val("");
						} else {
							$("select[name=" + k + "]").val(v);
						}
					} else {
						$("input[name=" + k + "]").val(v);
					}
				});
				clearInterval(etu6.psh);
			}
		}
	},
    init: function() {
        // 修复验证调用问题
        $.each($(".checkform"), function(k, v) {
            $.htvalid($(v));
        });
        
        if (document.getElementById("webpage") != null) {
           var css = document.createElement("link"),script = document.createElement("script");            
            css.href = '/skin/web.v3/css/simplePagination.css';
            css.type = 'text/css';
            css.rel = 'stylesheet';
            script.src = '/skin/web.v3/js/simplePagination.js';
            script.type = 'text/javascript';           
            $("head").find("link").after(css);
            $("head").find("script").after(script);
            $.loadpage();
        }
    }
};

(function($) {
    $.getParams = function(uri, qk) {
         uri = uri || window.location.search;
         var re = /(\w*)\=([^\&\?]*)/ig;
         var retval = {};
         while ((arr = re.exec(uri)) != null)
             retval[arr[1]] = decodeURIComponent(arr[2]);
         if (qk)
             return retval[qk] ? retval[qk] : null;
         else
             return retval;
     }
     $.loadpage = function() {
        if (typeof etu6.params != "undefined" && etu6.params != "") {
            if (etu6.params.hasOwnProperty('total') && etu6.params.hasOwnProperty('ps') && etu6.params.hasOwnProperty('p')) {
                etu6.total = etu6.params.total;
                etu6.ps = etu6.params.ps;
                etu6.p = etu6.params.p;
                var paramsPrefix = [];

                jQuery.each(etu6.params, function(k, v) {
                    if ( ($("select[name=" + k + "]").is("select") && $("select[name=" + k + "]").find("option[value='0']").get(0) == "undefined")) {
                        return;
                    }

                    if (k != 'total' && k != 'ps' && k != 'p') {
                        paramsPrefix.push(k + "=" + v)
                    }
                });
                
                etu6.hrefTextPrefix = paramsPrefix.join("&");
            }
        }

        $("#webpage").pagination({
            items: etu6.total,
            itemsOnPage: etu6.ps,
            hrefTextPrefix: location.pathname + "?" + (etu6.hrefTextPrefix == "" ? "" : etu6.hrefTextPrefix + "&") + "p=",
            currentPage: etu6.p
        });
     }
     // 欢途通用验证
     $.htvalid = function(obj, sync, func) {
		var element = obj ? obj : "." + obj;
		var isajax = false;

        if ($(element).attr("isajax") == "true") {
            isajax = true;
        } else if (typeof sync != 'undefined' && sync)
			isajax = true;
		if ($(element).is("form")) {
			$(element).Validform({
				datatype: {
					"mphone": function(gets, obj, curform, regxp) {
						var reg1 = regxp["m"],
								reg2 = /(\(\d{3,6}\)|\d{3,6}-|\s)?\d{8}/,
								mobile = curform.find("input[name=mobile]");
						if (reg1.test(mobile.val())) {
							return true;
						}
						if (reg2.test(gets)) {
							return true;
						}
						return false;
					},
					"tel": function(gets, obj, curform, regxp) {
						var gs = gets.split('|');
						for (var i = 0; i < gs.length; i++) {
							var re = /^(((\+|)\d{2})|)(-| |)\d{3,4}(-| |)\d{3,4}(-| |)\d{3,4}(((-| |)\d{3,4})|)$/;
							if (!re.test(gs[i]))
								return false;
						}
						return true;
					},
					"qq": function(gets, obj, curform, regxp) {
						var gs = gets.split('|');
						for (var i = 0; i < gs.length; i++) {
							var re = /^[\d|]+$/;
							if (!re.test(gs[i]))
								return false;
						}
						return true;
					},
					"price": function(gets, obj, curform, regxp) { //价格的验证
						var re = /^[1-9]{1}\d{0,5}(\.[\d]{1,2})?$/;
						if (re.test(gets))
							return true;
						return false;
					},
                    "pint": function(gets, obj, curform, regxp) { //价格的验证
                        var re = /^[1-9]*[1-9][0-9]*$/;
                        if (re.test(gets))
                            return true;
                        return false;
                    },
					"idcard": function(gets, obj, curform, regxp) { //身份证的验证
						/*
						var re = /^[1-9](\d{16}|\d{13})[0-9xX]$/;
						if (re.test(gets))
							return true;
						return false;
						*/
						var c = new clsIDCard(gets);
						if (c.IsValid()) {
							return true;
						}
						return false;
					},
                    "china": function(gets, obj, curform, regxp) { //这个必须是中文;
						var re = /^[\u4E00-\u9FA5][\u4E00-\u9FA5\w ]{1,50}$/i;
						if (re.test(gets))
							return true;
						return false;
					},
					"chinaer": function(gets, obj, curform, regxp) { //价格的验证
						var re = /^[\u4E00-\u9FA5][\u4E00-\u9FA5\w\s\(\)\（\）]{2,50}$/i;  //这个必须是中文;
						if (re.test(gets))
							return true;
						return false;
					},
                    "companyname": function(gets, obj, curform, regxp) { //这个必须是中文;
						var re = /^[\u4E00-\u9FA5\w\ \.,()&]{4,100}$/i;  
						if (re.test(gets))
							return true;
						return false;
					},
                    "addresses": function(gets, obj, curform, regxp) { //这个必须是中文;
						var re = /^[\u4E00-\u9FA5\w\ \.,()&-]{4,100}$/i;  
						if (re.test(gets))
							return true;
						return false;
					},
                    "Mname": function(gets, obj, curform, regxp) { //真实姓名
                        var re = /^[\u4E00-\u9FA5\a-z\s]{1,20}$/i;  //这个必须是中文;
                        if (re.test(gets))
                            return true;
                        return false;
                    },
					"date": function(gets, obj, curform, regxp) { //时间格式的验证
						var re = /^\d{4}-(0?[1-9]|1[0-2])-(0?[1-9]|[1-2]\d|3[0-1])$/;
						if (re.test(gets))
							return true;
						return false;
					},
					search: function(gets, obj, curform, regxp) {
						var val = gets.replace("请填写关键字", "");
						obj.val(val);
						var stype = jQuery("#stype").val();

						if (stype == "" && val != "") {
							jQuery("#stype :selected").val(Math.pow(2, jQuery("#stype option").length - 1) - 1);
						} else {
							if (stype != "" && val == "")
								jQuery(stype).val("");
						}

						return  (val == "" || /^[\u4E00-\u9FA5\uf900-\ufa2d\w\.\s]{2,16}$/.test(val)) ? true : false;
					},
					join: function(gets, obj, curform, regxp) {//关联验证
						var before = $(curform[0]).attr("datainput");
						if ($("input[name='"+before+"']").val()) {
							return gets ? true : false;
						}
						return true;
					}
				},
				tiptype: 3,
				ignoreHidden: false,
				postonce: false,
				beforeCheck: function(curform) {
                    // submit initiation
                    if (etu6.hasOwnProperty("sbinit") && typeof etu6.sbinit == 'function') {
                        return etu6.sbinit.call(etu6, curform);
                    }                    
				},
				beforeSubmit: function(curform) {
					this.tiptype = function() {
						return false;
					}
					return true;
				},
				callback: function(o) {
					if (this.ajaxPost) {                       
                        var cb = $(element).attr("callback");
                        
                        if (cb != null && cb != undefined) {
                            etu6[cb].call(etu6, o);
                        } else if (etu6.hasOwnProperty("callback") && typeof etu6.callback == 'function') {
                            etu6.callback.call(etu6, o);
                        } else if (etu6.hasOwnProperty(func) && typeof etu6[func] == 'function') {
							etu6[func].call(etu6, o);
						}
					}
				},
				ajaxPost: isajax,
				showAllError: true
			});
		}
	}
})(jQuery);

//设为首页
function setHomepage(){
    if (document.all){
        document.body.style.behavior='url(#default#homepage)';
          document.body.setHomePage(window.location.href);
    }else if (window.sidebar){
        if(window.netscape){
            try{
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }catch (e){
                alert( "该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true" );
            }
        }
        var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components. interfaces.nsIPrefBranch);
        prefs.setCharPref('browser.startup.homepage',window.location.href);
    }else{
        alert('您的浏览器不支持自动自动设置首页, 请使用浏览器菜单手动设置!');
    }
}

//加入抽藏
function addFavorite(){  
    if (document.all){  
        try{  
            window.external.addFavorite(window.location.href,document.title);  
        }catch(e){  
            alert( "加入收藏失败，请使用Ctrl+D进行添加" );  
        }  
          
    }else if (window.sidebar){  
        window.sidebar.addPanel(document.title, window.location.href, "");  
     }else{  
        alert( "加入收藏失败，请使用Ctrl+D进行添加" );  
    }  
}  