if (!Function.prototype.bind) { Function.prototype.bind = function () { var self = this, args = [].slice.call(arguments), target = args.shift(); return function () { self.apply(target, args.concat([].slice.call(arguments))); } }; }

$.extend({
    //点击提交按钮时使用，可以阻止多次请求
    ajaxPost: function (url, data, callback, before, error) {
        var result = $.ajax({
            url: url,
            dataType: 'json',   //返回的数据类型                
            type: "post",
            data: data,//参数
            async: false,//false为同步，true为异步，同步的数据会使页面锁住并且之后的JS也不会执行，一直等待到数据加载完成才解锁,本次操作必须要同步得到数据才能去执行是否隐藏
            success: callback,
            error: before,
            beforeSend: error
        });
        var status = parseInt(result.status);
        if (status == 500) {
            alert("服务器异常，稍后重试！");
        }
        if (status == 404) {
            alert("找不到页面！");
        }
        return result;
    },
    //将serializeArray序列化成json格式, $.serializeArrayToJson($('#form1').serializeArray())
    serializeArrayToJson: function (data) {
        var o = {};
        $.each(data, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    },
    //缓存，只传key等于等于取值，key和value都传入等于赋值
    m: (function () {
        var cache = {};
        return function (key, value) {
            if (typeof value == "undefined") {
                return cache[key];
            } else {
                if (value) {
                    return cache[key] = value;
                } else {
                    delete cache[key];
                }
            }
        }
    })($),
    //随机数
    random: function () {
        return Math.floor(Math.random() * 0x100000).toString(16);
    },
    //获取地址栏参数
    getParams: function (qk, uri) {
        uri = uri || window.location.search;
        var re = /(\w*)\=([^\&\?]*)/ig;
        var retval = {};
        while ((arr = re.exec(uri)) != null)
            retval[arr[1]] = decodeURIComponent(arr[2]);
        if (qk)
            return retval[qk] ? retval[qk] : null;
        else
            return retval;
    },
    //全选
    checkALL: function (params) {
        var opts =
            {
                all: "",//参考#checkAll
                item: "",//参考.input-radio
                itemBox: ""//参考.list-item
            }
        this.params = $.extend({}, opts, params || {});
        var self = this.params;
        $(self.all).live("click", function () {
            $(self.itemBox).find(self.item).prop("checked", $(this).prop('checked'));
        });
        $(self.itemBox).live("click", self.item, function () {
            if ($(self.itemBox).find(self.item).filter(":checked").size() == $(self.itemBox).find(self.item).size()) {
                $(self.all).prop("checked", true);
            } else {
                $(self.all).prop("checked", false);
            }
        });
    },
    min: function (a, b) { return a < b ? a : b; },
    max: function (a, b) { return a > b ? a : b; },
    diffDate: function (date, diffDay) {
        function NewDate(str) {
          str=str.split("-");var date=new Date;return date.setUTCFullYear(str[0],str[1]-1,str[2]),date.setUTCHours(0,0,0,0),date;
        }
        var d = NewDate(date);
        return new Date(d.getTime() + 86400000*diffDay);
    }
   
});

$.fn.extend({
    getAttr: function (params) {
        var e = [];
        $.each(this, function () {
            var element = $(this), obj = {};
            if (params != null) {
                $.each(params, function (index, value) {
                    var temp = {};
                    temp[value] = element.attr("data-" + value);
                    obj = $.extend(obj, temp);
                });
                return e.push(obj);
            }
            else {
                //todo:1
            }
           
        })
        return e;
    }
})


//时间格式化
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1,  //month
        "d+": this.getDate(),     //day
        "h+": this.getHours(),    //hour
        "m+": this.getMinutes(),  //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (typeof this == "string") { return this; }
    if (format == null || format == "undefined") { format = "yyyy-MM-dd"; }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}


// 清除两边的空格  
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, '');
};
// 合并多个空白为一个空白  
String.prototype.ResetBlank = function () {
    var regEx = /\s+/g;
    return this.replace(regEx, ' ');
};

// 保留数字  
String.prototype.GetNum = function () {
    var regEx = /[^\d]/g;
    return this.replace(regEx, '');
};

// 保留中文  
String.prototype.GetCN = function () {
    var regEx = /[^\u4e00-\u9fa5\uf900-\ufa2d]/g;
    return this.replace(regEx, '');
};

// String转化为Number  
String.prototype.ToInt = function () {
    return isNaN(parseInt(this)) ? this.toString() : parseInt(this);
};

// 得到字节长度  
String.prototype.GetLen = function () {
    var regEx = /^[\u4e00-\u9fa5\uf900-\ufa2d]+$/;
    if (regEx.test(this)) {
        return this.length * 2;
    } else {
        var oMatches = this.match(/[\x00-\xff]/g);
        var oLength = this.length * 2 - oMatches.length;
        return oLength;
    }
};
// 数字补零  
Number.prototype.LenWithZero = function (oCount) {
    var strText = this.toString();
    while (strText.length < oCount) {
        strText = '0' + strText;
    }
    return strText;
};

// 数字数组由小到大排序  
Array.prototype.Sort = function () {
    for(var j,oValue,i=0;i<this.length;i++)for(j=0;j<=i;j++)this[i]<this[j]&&(oValue=this[i],this[i]=this[j],this[j]=oValue);return this;
};

// 获得数字数组中最大项  
Array.prototype.GetMax = function () {
    for(var oValue=0,i=0;i<this.length;i++)this[i]>oValue&&(oValue=this[i]);return oValue;
};

// 获得数字数组中最小项  
Array.prototype.GetMin = function () {
    for(var oValue=0,i=0;i<this.length;i++)this[i]<oValue&&(oValue=this[i]);return oValue;
};
//对比2个时间的偏移量
Date.prototype.Diff = function (interval, objDate) {
    //若参数不足或 objDate 不是日期类型則回传 undefined  
    if (arguments.length < 2 || objDate.constructor != Date) { return undefined; }
    switch (interval) {
        //计算秒差                                                          
        case 's': return parseInt((objDate - this) / 1000);
            //计算分差  
        case 'n': return parseInt((objDate - this) / 60000);
            //计算時差  
        case 'h': return parseInt((objDate - this) / 3600000);
            //计算日差  
        case 'd': return parseInt((objDate - this) / 86400000);
            //计算周差  
        case 'w': return parseInt((objDate - this) / (86400000 * 7));
            //计算月差  
        case 'm': return (objDate.getMonth() + 1) + ((objDate.getFullYear() - this.getFullYear()) * 12) - (this.getMonth() + 1);
            //计算年差  
        case 'y': return objDate.getFullYear() - this.getFullYear();
            //输入有误  
        default: return undefined;
    }
};

//检测是否为空  
String.prototype.IsNullOrEmpty = function () {
   var obj=this,flag=!1;return obj==null||obj==undefined||typeof obj=="undefined"||obj==""?flag=!0:typeof obj=="string"?(obj=obj.trim(),obj==""?flag=!0:(obj=obj.toUpperCase(),(obj=="NULL"||obj=="UNDEFINED"||obj=="{}")&&(flag=!0))):flag=!1,flag;
};