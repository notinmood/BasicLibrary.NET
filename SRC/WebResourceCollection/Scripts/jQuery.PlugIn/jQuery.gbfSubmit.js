function submitAndRedirect(formSelector, targetUrl) {
    $(formSelector).ajaxSubmit({
        beforeSend: function () {
            var result = $(formSelector).valid();
            return result;
        },
        success: function (responseInfo) {
            if (responseInfo.IsSuccessful == false) {
                alert(responseInfo.Message);
            }
            else {
                if (targetUrl == null || targetUrl == "currenturl") {
                    window.location.reload();
                }
                else {
                    window.location = targetUrl;
                }
            }
        },
        data: { "isAjaxDeal": "true" }
    });
}

function submitAndLoad(formSelector, loadContainer, targetUrl) {
    $(formSelector).ajaxSubmit({
        success: function (responseInfo) {
            if (responseInfo.IsSuccessful == false) {
                alert(responseInfo.statusMessage);
            }
            else {
                $(loadContainer).load(targetUrl);
            }
        },
        data: { "isAjaxDeal": "true" }
    });
}