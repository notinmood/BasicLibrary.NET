/*用来遍历指定对象所有的属性名称和值（此方法移到jQuery.hiland.general.js内实现）
obj 需要遍历的对象 
author: Jet Mah */
//Object.prototype.getAllProperty = function () {
//    var target = this;
//    // 用来保存所有的属性名称和值 
//    var props = "";
//    // 开始遍历 
//    for (var p in target) {
//        // 方法 
//        if (typeof (target[p]) == " function ") {
//            target[p]();
//        } else {
//            // p 为属性名称，target[p]为对应属性的值 
//            props += p + " = " + target[p] + " \t ";
//        }
//    }
//    // 最后显示所有的属性 <br>alert ( props ) ; 
//    return props;
//}

/*
层次深度嵌套的JSON数据对象的取值
是否方法参见：Test\Javascript对象内部取值.html
http://www.cnblogs.com/l_nh/archive/2012/08/11/2633264.html
*/
Object.prototype.tryGetValue = function (exp, defaultValue) {
    var value = this,
    regex = /(\w+)[\.\[\]]?/g,
    result;
    if (typeof exp !== 'string') {
        throw new Error("路径表达式必须是字符串！");
    }
    while ((result = regex.exec(exp)) !== null) {
        if (value == undefined) {
            value = defaultValue;
            break;
        }
        value = value[RegExp.$1];
    }

    return value || defaultValue;
}


/*说明：使用本js文件，需要首先引入jQuery原始文件*/
            
//(function ($) {
//    window.HlObject = function () {
//        return new HlObject();
//    };

//    HlObject.prototype.addBookmark = function () {

//    };

//    HlObject.prototype.hello = function () {
//        alert("hello world.");
//    };
//})(jQuery);

            

//            //加入收藏
//            function  addBookmark(site, url) {
//                if (this.checkBrowser().isIE== true) {
//                    window.external.addFavorite(url, site)
//                } else if (this.checkBrowser().isOpera== true) {
//                    alert("请使用Ctrl+T将本页加入收藏夹");
//                } else {
//                    alert("请使用Ctrl+D将本页加入收藏夹");
//                }
//            }


//　         // 设为首页
//           function setHomepage () {
//                var weburl= location.href;
//                if (document.all) {
//                    document.body.style.behavior = "url(#default#homepage)";
//                    document.body.setHomePage(weburl);　　　　　　　　　　　 
//                }
//                else if (window.sidebar) {
//                    if (window.netscape) {
//                        try {
//                            netscape.security.PrivilegeManager.enablePrivilege(“UniversalXPConnect”);
//                        }
//                        catch (e) {
//                            alert("设为首页操作被浏览器拒绝，假如想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true");
//                        }
//                    }
//                    var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
//                    prefs.setCharPref(‘browser.startup.homepage’,weburl);
//                }　　　　　　　
//            }
