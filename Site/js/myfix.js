function AddFav(memberName, lineID) {
    if (confirm("确定收藏该线路吗？")) {
        $.post("FavHandler.ashx?act=add&id=" + lineID + "&memberName=" + memberName, function (data, status) {
            if (data == "1") {
                //添加成功
                $("#a" + lineID).html("<a class='collect' href=\"javascript:RemoveFav('" + memberName + "','" + lineID + "')\"><i class='iconfont fav'></i><br/>已收藏</a>");
            } else if (data == "0") {
                window.location.href = "MemberLogin.aspx?ReturnUrl="+escape(window.location.href);
            }
            else {
                //否则，弹出错误信息
                alert(data);
            }
        });
    }
}

function RemoveFav(memberName, lineID) {
    if (confirm("确定取消收藏该线路吗？")) {
        $.post("FavHandler.ashx?act=remove&id=" + lineID + "&memberName=" + memberName, function (data, status) {
            if (data == "1") {
                $("#a" + lineID).html("<a class='collect' href=\"javascript:AddFav('" + memberName + "','" + lineID + "')\"><i class='iconfont'></i><br/>收藏</a>");
            }
            else if (data == "0") {
                window.location.href = "MemberLogin.aspx?ReturnUrl="+ escape(window.location.href);
            }
            else {
                alert(data);
            }

        });
    }
}

function DelFav(memberName, lineID) {
    if (confirm("确定删除该线路收藏吗？")) {
        $.post("FavHandler.ashx?act=remove&id=" + lineID + "&memberName=" + memberName, function (data, status) {
            if (data == "1") {
                window.location.reload(true);
            }
            else {
                alert(data);
            }

        });
    }
}