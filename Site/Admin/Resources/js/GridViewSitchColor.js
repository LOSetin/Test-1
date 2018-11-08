function GridViewSwitchColor(GridViewId, NormalColor, HoverColor) {
    if (document.getElementById(GridViewId) == null)
        return;
    var AllRows = document.getElementById(GridViewId).getElementsByTagName("tr");
    if (AllRows == null)
        return;
    for (var i = 1; i < AllRows.length; i++) {
        if (HoverColor != "") {
            AllRows[i].onmouseover = function () {
                if (!this.selected) this.style.background = HoverColor;
            }
            AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = NormalColor; }
        }
    }
}
try { GridViewSwitchColor("gvData", "#FFFFFF", "#e4f3de"); } catch (e) { }