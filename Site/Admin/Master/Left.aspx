<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Master_Left" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="Right" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        body { margin: 0; padding: 0; overflow-x:hidden; background: url(../Resources/img/black-Linen.png) repeat-y #000000;  }
        #Left { vertical-align: top; width: 200px; overflow-x:hidden;  }
        
        img { border: 0; }
    </style>
    <link href="../Resources/css/LeftMenu.css" rel="stylesheet" />
    <script type="text/javascript" src="../Resources/js/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../Resources/js/slider.js"></script>  
    <script type="text/javascript">
        $(function () {
            //导航切换
            $('a').click(function () {
                $('a.item_active').removeClass("item_active");
                $(this).addClass("item_active");
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Left">
        <div>
            <dl class="sliderbox" id="slider2">
                <% if (CurrentAdmin.IsSuper)
                    { %>
                <dt><span class="title">系统管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Sys/AdminEdit.aspx">添加管理员</a></div>
                        <div class="item"><a href="../Sys/AdminManager.aspx">管理员管理</a></div>
                             <div class="item"><a href="../Sys/BanQuan.aspx">版权设置</a></div>
                        </div>
                </dd>
                <%} %>

                <dt><span class="title">积分商城</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Goods/GoodsCat.aspx">商品分类</a></div>
                        <div class="item"><a href="../Goods/GoodsManager.aspx">商品管理</a></div>
                    </div>
                </dd>

                 <dt><span class="title">照片墙管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Photo/PhotoUpload.aspx">上传照片</a></div>
                        <div class="item"><a href="../Photo/PhotoManager.aspx">照片管理</a></div>
                    </div>
                </dd>

                 <dt><span class="title">幻灯管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Slider/SliderEdit.aspx">添加幻灯</a></div>
                        <div class="item"><a href="../Slider/SliderManager.aspx">幻灯管理</a></div>
                    </div>
                </dd>

                 <dt><span class="title">热词设置</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../SiteSetting/HotWords.aspx">热词设置</a></div>
                    </div>
                </dd>

                   <dt><span class="title">促销管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Promotion/PromotionEdit.aspx">添加促销</a></div>
                        <div class="item"><a href="../Promotion/PromotionManager.aspx">促销管理</a></div>

                    </div>
                </dd>

                  <dt><span class="title">线路管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Line/LineCat.aspx">线路分类</a></div>
                        <div class="item"><a href="../Line/LineManager.aspx">线路管理</a></div>

                    </div>
                </dd>

                  <dt><span class="title">会员管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Mem/MemberManager.aspx">会员查询</a></div>
                    </div>
                </dd>

                    <dt><span class="title">兑换管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Exchange/WaitingSend.aspx">等待发货</a></div>
                        <div class="item"><a href="../Exchange/WaitingReceive.aspx">确认收货</a></div>
                        <div class="item"><a href="../Exchange/AllRecords.aspx">兑换记录</a></div>
                    </div>
                </dd>

                 <dt><span class="title">订单管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../Order/UnConfirmOrder.aspx">未确认订单</a></div>
                         <div class="item"><a href="../Order/OrderManager.aspx">订单管理</a></div>
                    </div>
                </dd>

                  <dt><span class="title">问答管理</span></dt>
                <dd>
                    <div class="text">
                        <div class="item"><a href="../QA/QAManager.aspx">问答管理</a></div>
                    </div>
                </dd>


            </dl>
<script type="text/javascript">
    var slider2 = new accordion.slider("slider2");
    slider2.init("slider2", 0, "open");
	</script>
        </div>
    </div>
    </form>
</body>
</html>
