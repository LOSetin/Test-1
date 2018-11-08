


$.fn.extend({
     /* 时分秒选择 */
    timepickerselect: function (opts/* 参数 */, mode /* 时分秒选择模式 */) {
        if (mode == 1) {
            /* opts 参照插件timepicker */
            $(this).timepicker($.extend({
                minuteStep: 5,
                secondStep: 30,
                showInputs: false,
                showSeconds: true,
                showMeridian: false
            }, opts));
        }
        else {
            /* opts
             *  top: 0,//弹窗上偏移量
             *  left: 0,//弹窗左偏移量
             *  format: function (i) {
             *      if (i < 10) {
             *          return "0" + i;
             *      } else {
             *          return "" + i;
             *      }
             *  },//输出时间格式化
             *  unit:[true,true,true],//时分秒显示状态，请连续true选择
             *  callback: function () { },//关闭时间选择回调事件
             *  select: function (time) {  },//点击选择时间回调事件
             *  max: false,//[22,30,0] //最大选择时间
             *  min: false,//[7,30,0] //最小选择时间
             *  ranges: [[0,23], [0,59], [0,59]] //时分秒输出区间
             */
            $(this).click(function () {
                $(this).timeselect($.extend({ unit: [true, true, false] },opts));
            });
        }
    },
    linedatepickercalendar: function (opts, custom, isClick, params) {
        if (isClick) {
            var calendar, element;
            $(this).click(function () {
                element = $(this);
                $(".date-picker-dialog").hide();
                calendar = $('<div class="calendar_custom_v2 date-picker-dialog"></div>');
                if (!!$(document).find(calendar).length && calendar.is(":hidden")) { calendar.show(); return; }
                calendar.appendTo($(document.body));
                var param = { defaultDate: 1 };
                if (params != null) {
                    $.each(params, function (index, value) {
                        var temp = {};
                        temp[value] = element.attr("data-" + value);
                        param = $.extend(param, temp);
                    });
                }

                calendar.datepickerCalendar($.extend(opts, { defaultDate: param.defaultDate }), $.extend(custom, { callback: function () { refreshPosition(element, calendar); }, param: function (y, m) { return $.extend({ y: y, m: m }, param || {}); } }));
                refreshPosition(element, calendar);
                //input 的 blur 事件
                $(document).bind("click.datepicker", function (e) {
                    if (!calendar.is(":hidden") && $(e.target).closest(".calendar_custom_v2.date-picker-dialog,.ui-datepicker-header").size() == 0 && $(e.target).closest(element).size() == 0) {
                        calendar && calendar.remove();
                    }
                });
            });
        }
        else {
            $(this).datepickerCalendar(opts, custom);
        }
        //刷新坐标位置
        function refreshPosition(element, calendar) {
            var pX = getPosition(element, calendar.height());
            calendar.css({ "top": pX, left: element.offset().left });
        }
        //获取坐标位置
        function getPosition(element, height) {
            var top = element.offset().top;
            var bottom = element.offset().top + element.outerHeight();
            var bodyHeight = $(document.body).height();
            var windowHeight = $(window).height();
            var scrollTop = $(document).scrollTop();
            var down = windowHeight - (bottom - scrollTop);
            var up = top - scrollTop;
            if (down > up && down > height) {
                return bottom;
            }
            else if (down < up && up > height) {
                return top - height;
            }
            else {
                return bottom;
            }
        }
    },
    datepickercalendar: function (opts, custom, isClick) {
        if (isClick) {
            var calendar, element;
            $(this).click(function () {
                element = $(this);
                $(".date-picker-dialog").hide();
                calendar = $('<div class="calendar_custom_v2 date-picker-dialog"></div>');
                if (!!$(document).find(calendar).length && calendar.is(":hidden")) { calendar.show(); return; }
                calendar.appendTo($(document.body));
                calendar.datepickerCalendar(opts, custom);
                refreshPosition(element, calendar);
                //input 的 blur 事件
                $(document).bind("click.datepicker", function (e) {
                    if (!calendar.is(":hidden") && $(e.target).closest(".calendar_custom_v2.date-picker-dialog,.ui-datepicker-header").size() == 0 && $(e.target).closest(element).size() == 0) {
                        calendar && calendar.remove();
                    }
                });
            });
        }
        else {
            $(this).datepickerCalendar(opts, custom);
        }
        //刷新坐标位置
        function refreshPosition(element, calendar) {
            var pX = getPosition(element, calendar.height());
            calendar.css({ "top": pX, left: element.offset().left });
        }
        //获取坐标位置
        function getPosition(element, height) {
            var top = element.offset().top;
            var bottom = element.offset().top + element.outerHeight();
            var bodyHeight = $(document.body).height();
            var windowHeight = $(window).height();
            var scrollTop = $(document).scrollTop();
            var down = windowHeight - (bottom - scrollTop);
            var up = top - scrollTop;
            if (down > up && down > height) {
                return bottom;
            }
            else if (down < up && up > height) {
                return top - height;
            }
            else {
                return bottom;
            }
        }
    },
    //日期选择区间
    daterangeselect: function () {
        $(this).daterangepicker({
            opens: 'right',
            format: 'yyyy-MM-dd',
            separator: '至',
            startDate: Date.today().add({
                days: 0
            }),
            endDate: Date.today(),
            minDate: null,
            maxDate: null,
            locale: {
                applyLabel: '确认',
                clearLabel: "取消",
                fromLabel: ' ',
                toLabel: '-',
                customRangeLabel: '日期选择',
                daysOfWeek: ['一', '二', '三', '四', '五', '六', '日'],
                monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
                firstDay: 1
            },
            buttonClasses: ['btn-danger']
        },
        function (start, end) {
            $('.report_range span').html(start.toString('MMMM d, yyyy') + ' - ' + end.toString('MMMM d, yyyy'));
        });
         
    },
    dateselect: function (opts) {
        var base_opts = {
            changeMonth: false,
            numberOfMonths: 1,
            showToday: true,
            prevText: "上月",
            nextText: "下月",
            dateFormat: 'yy-mm-dd',
            defaultDate: 1,
            inline: true,
            constrainInput: true,
            beforeShow: function (input, obj) { $(obj.dpDiv).wrap($('<div class="calendar_custom_v2" style="top:0;left:0;">')); },
            onClose: function (date, obj) { var calendar = $(obj.dpDiv).closest(".calendar_custom_v2"); $(obj.dpDiv).hide().appendTo($(document.body)); calendar.remove(); }
        };
        $(this).datepicker($.extend(base_opts, opts || {}));
    },
    formatDate: function (params) {
        var opts = {
            format: function (d) {return d },
            attr: "data-countdown",
            finish: function () { }
        }
        var options = $.extend(opts, params || {});
        $(this).each(function () {
            var self = this;
            this.second = parseInt($(this).attr(options.attr)) || 0;
            this.countdown = function () {
                if (self.second <= 0) { clearInterval(self.delay); options.finish.call(self, this.second); return; }
                var date = getDateTimeFormat(self.second);
                $(self).html(options.format.call(self, dateformat(date, true)));
                self.second--;
            }
            this.delay = setInterval(this.countdown, 1000);
        });
        //返回日时分秒
        function getDateTimeFormat(second) {
            var d = Math.floor(second / 60 / 60 / 24);
            var h = Math.floor(second / 60 / 60 % 24);
            var m = Math.floor(second / 60 % 60);
            var s = Math.floor(second % 60);
            return {
                d: d,
                h: h,
                m: m,
                s: s
            };
        }
        //格式化
        function dateformat(date, flag) {
            if (flag) {
                if (date.d > 0) {
                    return date.d + "天" + lenWithZero(date.h) + "时" + lenWithZero(date.m) + "分";
                } else {
                    return lenWithZero(date.h) + "时" + lenWithZero(date.m) + "分" + lenWithZero(date.s) + "秒";
                }
            }
            else {
                if (date.d > 0) return date.d + "天" + lenWithZero(date.h) + "时" + lenWithZero(date.m) + "分" + lenWithZero(date.s) + "秒";
                if (date.h > 0) return lenWithZero(date.h) + "时" + lenWithZero(date.m) + "分" + lenWithZero(date.s) + "秒";
                if (date.m > 0) return lenWithZero(date.m) + "分" + lenWithZero(date.s) + "秒";
                if (date.s > 0) return lenWithZero(date.s) + "秒";
            }
        }
        //左对齐
        function lenWithZero(number) {
            return number.LenWithZero(2);
        }
    }
})