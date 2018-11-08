(function($, undef){
    $.fn.region = function(options) {
        var obj = this,
            data = $.fn.region.data,
            config = $.extend({}, $.fn.region.defaults, options);
        config.defopt = "<option value=''>"+config.deftext+"</option>"; // 默认第一项 option
        config.selector = obj.selector;
        var urlvar = buildVar(config.script);
        var scripturl = config.script + (config.script.indexOf('?') === -1 ? '?' : '&') + 'var=' + '$.fn.region.data.' + urlvar;

        obj.empty();
        for(var count=1; count < config.depth; count++)
            obj.append($("<select>").html(config.defopt).prop("disabled", config.defdisable)); // 插入 depth-1个 select

        if (data[urlvar] === undef){
            if (! $.fn.region.loaddatacount) {
                $.fn.region.loaddatacount++;
                $.ajax({dataType:"script", cache:true, url:scripturl}).done(function(){ // ajax 载入js数据
                    createSelect(config, data[urlvar]);
                });
            } else {
                var loader = setInterval(function(){
                    if (data[urlvar] !== undef) {
                        clearInterval(loader);
                        createSelect(config, data[urlvar]);
                    }
                }, 100);
            }
        } else {
            createSelect(config, data[urlvar]);
        }

        if(typeof config.loaded === "function") config.loaded.call(null);
        return obj;
    };

    $.fn.region.defaults = {
        script : '/skin/web.v1/js/area.js?v=07151435', // 地区数据js文件，服务器端生成
        names : ['provinceid', 'cityid', 'areaid', 'zoneid'], // 各select name
        values : [], // 默认选中值，如： [123,456]
        attrs : [], // 各 select 的属性，如: [{datatype:'*'}]
        filters : [], // 各 select 白名单过滤，如：['浙江,江苏,', '杭州,南京'] , 如果 usetext 不为true,这里用id
        depth : 3, // 3 表示三级联动
        usetext: false, // 使用文字而非id
        loaded: null, // $.region() 载入完成后的回调
        change : null, // change 回调
        deftext: '请选择', // 初始化option文字
        defdisable : true // 初始化option是否disabled
    };

    $.fn.region.data = {};
    $.fn.region.loaddatacount = 0;

    function buildVar(url, params, parentid, ids){ // 后端仅处理 parentid 和 ids参数
        if (url.indexOf('?') !== -1){
            params = url.match(/\?.*/)[0];
            if (ids = params.match(/[?&]ids=(\d+(,\d+)*)/))
                return 'ids'+ids[1].replace(/,/g, '_');
            if (parentid = params.match(/[?&]parentid=(\d+)/))
                return 'parentid'+parentid[1];
        }
        return 'ALLREGIONS';
    }

    function createSelect(config, data, target, select, callee, index, i) {
        callee = arguments.callee;
        index = $(target).attr("depth") >> 0; // 当前创建的select，从0开始，共 config.depth 个
        select = $("<select>").attr($.extend({}, config.attrs[index], {
            name : config.names[index],
            depth : index + 1 // 记录当前选中框的深度，从 1 开始
        }));
        createOptions(config, select, index, data); // 生成 select 的 options 选项
        select.bind("change", function(e, t) {                                  // 绑定当前select的change事件处理函数
            t = e.target;
            if (typeof config.change === "function")config.change.call(null,e); // 调用自定义change回调
            if ($(t).attr("depth") >= config.depth) return;                     // 当前是最后一个select，不需要创建下一个select
            $(t).nextAll("select").removeAttr("name")
                .html(config.defopt).prop("disabled", config.defdisable);       // 初始化当前select之后的兄弟节点
            if (! $(t).prop("selectedIndex")) return;                           // 选中第一项时，不需要创建下一个select，终止
            var curid = $(t).find(":selected").data("id") || t.value;           // 获取当前选择项的id，可能使用文字作为value，尝试取 data-id 的属性值
            for(i=0; i<data.length && data[i][0] != curid; i++);                // 遍历出当前select对应的子data数据的下标
            if (typeof data[i][2] !== "undefined") {                            // 仅当有子数据的时候才创建下一个select
                $(t).parent().find("select:last").remove();                     // 插入一个select之前，先删除最后一个select
                callee(config, data[i][2], t);                                  // 递归创建下一个select
            }
        });
        target ? select.insertAfter(target) : select.prependTo(config.selector);// 插入创建的select
        select.val(config.values[index]).trigger("change");                     // 设置选中值，触发change事件
    }

    function createOptions(config, select, index, data, options){
        options = select.prop("options");
        options[0] = new Option(config.deftext, ""); // 初始化第一个option
        var filterstr = typeof config.filters[index] === "string" ? ","+config.filters[index]+"," : false; // 如果设置了过滤字符，仅创建包含过滤字符的option
        if(config.usetext){ // 使用文字，非数字id
            for(i=0; i<data.length; i++){ // 没有设置过滤 或 当前选项在过滤字符之列，就创建option
                if (! filterstr || filterstr.indexOf(data[i][1]) !== -1)
                    select.append($("<option>").data("id", data[i][0]).attr({value:data[i][1]}).text(data[i][1])); // value非数字id时，将id记录到 data-id 属性
            }
        } else {
            for(i=0; i<data.length; i++){
                if (! filterstr || filterstr.indexOf(","+data[i][0]+",") !== -1)
                    options[options.length] = new Option(data[i][1], data[i][0]);
            }
        }
    }
})(jQuery);
