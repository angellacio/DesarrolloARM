jQuery.fn.selText = function () {
    try {
        var range, selection;
        if (document.body.createTextRange) {
            range = document.body.createTextRange();
            range.moveToElementText(this[0]);
            range.select();
        } else if (window.getSelection) {
            selection = window.getSelection();
            range = document.createRange();
            range.selectNodeContents(this[0]);
            selection.removeAllRanges();
            selection.addRange(range);
        }

       
    } catch (err) {
        alert(err);
    }
    return this;
}