window.onload = function () {
    document.addEventListener("contextmenu", function (e) {
        e.preventDefault();
    }, false);
    document.addEventListener("keydown", function (e) {
        // "I" key
        if (e.ctrlKey && e.shiftKey && e.keyCode == 73) {
            disabledEvent(e);
        }
        // "J" key
        if (e.ctrlKey && e.shiftKey && e.keyCode == 74) {
            disabledEvent(e);
        }
        // "M" key
        if (e.ctrlKey && e.shiftKey && e.keyCode == 77) {
            disabledEvent(e);
        }
        // "K" key
        if (e.ctrlKey && e.shiftKey && e.keyCode == 75) {
            disabledEvent(e);
        }
        // "Z" key
        if (e.ctrlKey && e.shiftKey && e.keyCode == 90) {
            disabledEvent(e);
        }
        // "E" key
        if (e.ctrlKey && e.shiftKey && e.keyCode == 69) {
            disabledEvent(e);
        }
        // "F7" key
        if (e.shiftKey && e.keyCode == 118) {
            disabledEvent(e);
        }
        // "F5" key
        if (e.shiftKey && e.keyCode == 116) {
            disabledEvent(e);
        }
        // "F9" key
        if (e.shiftKey && e.keyCode == 120) {
            disabledEvent(e);
        }
        // "F12" key
        if (e.shiftKey && e.keyCode == 123) {
            disabledEvent(e);
        }
        // "S" key + macOS
        if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) {
            disabledEvent(e);
        }
        // "U" key
        if (e.ctrlKey && e.keyCode == 85) {
            disabledEvent(e);
        }
        // "F12" key
        if (event.keyCode == 123) {
            disabledEvent(e);
        }
    }, false);
    function disabledEvent(e) {
        if (e.stopPropagation) {
            e.stopPropagation();
        } else if (window.event) {
            window.event.cancelBubble = true;
        }
        e.preventDefault();
        return false;
    }
};