(function ($) {
    $.fn.CascadingDropDown = function (source, actionPath, settings) {
        if (typeof source === 'undefined') { throw "please set source"; }
        if (typeof actionPath == 'undefined') { throw "please set actionPath"; }
        var optionTag = '<option></option>';
        var config = $.extend({}, $.fn.CascadingDropDown.defaults, settings);
        return this.each(function () {
            var $this = $(this);
            (function () {
                var methods = {
                    clearItems: function () {
                        $this.empty();
                        if (!$this.attr("disabled")) {
                            $this.attr("disabled", "disabled");
                        }
                    },
                    reset: function () {
                        methods.clearItems();
                        $this.append($(optionTag).attr("value", "").text(config.promptText));
                        $this.trigger('change');
                    },
                    initialize: function () {
                        if ($this.children().size() == 0) {
                            methods.reset();
                        }
                    },
                    showLoading: function () {
                        methods.clearItems();
                        $this.append($(optionTag).attr("value", "").text(config.loadingText));
                    },
                    loaded: function () {
                        $this.removeAttr("disabled");
                        $this.trigger('change');
                    },
                    showError: function () {
                        methods.clearItems();
                        $this.append($(optionTag).attr("value", "").text(config.errorText));
                    },
                    post: function () {
                        methods.showLoading();
                        $.isFunction(config.onLoading) && config.onLoading.call($this);
                        $.ajax({
                            url: actionPath,
                            type: 'POST',
                            dataType: 'json',
                            data: ((typeof config.postData == "function") ? config.postData() : config.postData) || $(source).serialize(),
                            success: function (data) {
                                methods.reset();
                                $.each(data.data, function (i, k) {
                                    $this.append($(optionTag).attr("value", eval("k." + config.valuefiled)).text(eval("k." + config.textfield)));
                                });
                                methods.loaded();
                                $.isFunction(config.onLoaded) && config.onLoaded.call($this);
                            },
                            error: function () {
                                methods.showError();
                            }
                        });
                    }
                };

                $(source).change(function () {
                    var parentSelect = $(source);
                    if (parentSelect.val() != '') {
                        methods.post();
                    }
                    else {
                        methods.reset();
                    }
                });
                methods.initialize();
            })();
        });
    }

    $.fn.CascadingDropDown.defaults = {
        promptText: '-- please choose --',
        loadingText: 'loading',
        errorText: 'error',
        postData: null,
        onLoading: null,
        onLoaded: null,
        textfield: 'text',
        valuefiled: 'value'
    }
})(jQuery);