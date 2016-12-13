var index = 0;
var menu_array = new Array();
var temp_array = new Array();
menu_array = [];
temp_array = [];
function addTab(title, url) {
    index++;
    var ex = exists(title);
    if (ex) {
        var tab = $('#tt').tabs('select', title);
        return;
    }
    menu_array.push(title)
    $('#tt').tabs('add', {
        title: title,
        content: buildFram(index, url),
        iconCls: 'icon-save',
        closable: true,
        tools: [{
            iconCls: 'icon-mini-refresh',
            handler: function () {
                alert('refresh');
            }
        }]
    });
    reinitIframe(index);
}
function getSelected() {
    var tab = $('#tt').tabs('getSelected');
    alert('Selected: ' + tab.panel('options').title);
}
function update() {
    index++;
    var tab = $('#tt').tabs('getSelected');
    $('#tt').tabs('update', {
        tab: tab,
        options: {
            title: 'new title' + index,
            iconCls: 'icon-save'
        }
    });
}

function exists(title) {
    for (var i = 0; i < menu_array.length; i++) {
        if (menu_array[i] == title) {
            return true;
        }
    }
    return false;
}



function buildFram(index, uri) {
    return "<iframe src='" + uri + "'  border='0' id='frame_content_" + index + "' name='frame_content_" + index + "' frameborder='0' framespacing='0' marginwidth='0'  marginheight='0' scrolling='yes' width='100%'></iframe>";
}

function reinitIframe(index) {
    var iframe = document.getElementById("frame_content_" + index + "");
    var bHeight = document.body.scrollHeight;
    var dHeight = document.documentElement.scrollHeight;
    var height = Math.max(bHeight, dHeight);
    iframe.height = height - 32;
}

function deleteMenu(title) {
    temp_array = [];
    for (var i = 0; i < menu_array.length; i++) {
        if (menu_array[i] == title) {
            continue;
        }
        temp_array.push(menu_array[i]);
    }

    menu_array = temp_array;
}