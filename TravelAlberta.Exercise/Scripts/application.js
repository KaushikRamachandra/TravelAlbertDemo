// require: jquery

/*!
 * Global behaviours and helper functions for the entire application.
 *
 */

(function ($) {
    "use strict";

    /* KENDO BINDERS
     ========================= */
    kendo.data.binders.index = kendo.data.Binder.extend({
        refresh: function () {
            var $el = $(this.element);
            var index = this.bindings.index.get();

            var name = $el.attr("name").replace(/\[(\d+)\]/, "[" + index + "]");
            $el.attr("name", name);
        }
    });

    $.cache = {
        store: function (key, value) {
            localStorage.setItem(key, value);
            return;
        },

        get: function (key) {
            return localStorage.getItem(key);
        },

        clear: function () {
            localStorage.clear();
            return;
        }
    };

    $.fn.displayText = function (text) {
        this.each(function () {
            $(this).text(text);
        });
    };

})(jQuery);