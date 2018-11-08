
(function ($) {
    $.fn.datepickerCalendar = function (params,custom) {
        var datepicker_opts = {
            changeMonth: false,
            numberOfMonths: 1,
            showToday: true,
            prevText: "上月",
            nextText: "下月",
            dateFormat: 'yy-mm-dd',
            defaultDate: 1,
            beforeShow: function (input, obj) {},
            onChangeMonthYear: function (year, month, inst) { this.custom.onChange(this,year,month);},
            onSelect: function (dateText, inst) {   },
            beforeShowDay: function (date) {return [false, "c" + date.format(), ""];} 
        };
        var custom_opts = {
            format: function (el,item) {$(".c"+item.day,el).append($('<a href="'+item.href+'" target="_blank"><em class="orange">&yen;'+item.price+'<\/em><b class="green">'+item.state+"<\/b><\/a>"));},
            param: function (y, m) {return{y:y,m:m};},
            callback: function () { },
            url: "",
            onChange: function (el/* 输入框 */, y /* int 年 */, m /* int 月 */) { setTimeout(function(){$.post(el.custom.url,el.custom.param.call(el,y,m),function(n){if(parseInt(n.Status)!=1)return;el.custom.render(el,n,y,m);el.custom.callback.call(el,n,y,m)},"json")},1);},
            render: function (el, result, y, m) { var dataformat=el.custom.dataFormat(result),prevBtn=$(".ui-datepicker-prev",el),nextBtn=$(".ui-datepicker-next",el);dataformat.prev?prevBtn.show():prevBtn.hide();dataformat.next?nextBtn.show():nextBtn.hide();$.each(dataformat.data,function(){el.custom.format(el,this)});},
            dataFormat: function (result) {return{prev:result.prev,next:result.next,data:result.Data};}
        }
        var params = $.extend({}, datepicker_opts, params || {});
        var custom = $.extend({}, custom_opts, custom || {});
        this.each(function () {
           if($(this).datepicker(params).hasClass("hasDatepicker")){this.custom=custom;this.params=params;custom.onChange(this,$(this).datepicker("getDate")?$(this).datepicker("getDate").getFullYear():(new Date).getFullYear(),$(this).datepicker("getDate")?$(this).datepicker("getDate").getMonth()+1:(new Date).getMonth()+1)}
        });
    }
})(jQuery);
