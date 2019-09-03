(function ($) {

    /**
     * @classdesc 
     * Places To Stay kendo data model.
     */
    var PlacesToStay = kendo.data.Model.define({
        id: "id",
        fields: {
            LocationDescription: { type: "string" },
            HealthPoints: { type: "number" },
            Name: { type: "string", required: true },
            Status: { type: "string", required: true },
            City: { type: "string" },
            Region: { type: "string" }
        }
    });

    /**
     *
     * @constructor
     * @param {Element} element - HTML DOM or jQuery Element to initialize
     * @param {object} options - Options array for this widget
     * @see $.fn.journey.defaults
     */
    var PlacesToStayViewModel = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, $.fn.placestostay.defaults, this.$element.data(), options);
        this._create();
    };

    PlacesToStayViewModel.prototype = {
        /**
         * Create the PlacesToStayViewModel widget, constructing MVVM view models and adding the required
         * controls and interactions to the widget.
         */
        _create: function () {
            var self = this;
            var options = this.options;

            /**
             * kendo view model
             * @type {kendo.observable}
             */
            this.viewModel = new kendo.observable({

                searchOn: '',

                hasPlacesToStay: function () {
                    var list = this.get('placesToStayList');
                    return list && list.total() && (list.total() > 0);
                },

                isNumber: function (n) {
                    return !isNaN(parseFloat(n)) && isFinite(n);
                },

                filterOnChange: function () {
                    console.log('blah!');
                    
                    var textToSearchFor = this.get('searchOn');

                    if (textToSearchFor && (textToSearchFor.length !== 0) && (textToSearchFor.trim().length !== 0)) {
                        var filter = { logic: 'or', filters: [] };

                        filter.filters.push({
                            field: 'Name',
                            operator: 'contains',
                            value: textToSearchFor
                        });

                        filter.filters.push({
                            field: 'LocationDescription',
                            operator: 'contains',
                            value: textToSearchFor
                        });

                        if (this.isNumber(textToSearchFor)) {
                            filter.filters.push({
                                field: 'HealthPoints',
                                operator: 'eq',
                                value: textToSearchFor
                            });

                            filter.filters.push({
                                field: 'Id',
                                operator: 'eq',
                                value: textToSearchFor
                            });
                        }

                        this.placesToStayList.filter(filter);
                    }
                    else {
                        this.placesToStayList.filter([]);
                    }
                },

                filteredPlacesToStayList: function () {
                    console.log('blah blah!');
                    var list = this.get('placesToStayList');

                    var textToSearchFor = this.get('searchOn');

                    if (textToSearchFor && (textToSearchFor.length !== 0) && (textToSearchFor.trim().length !== 0)) {
                        var filter = { logic: 'or', filters: [] };

                        filter.filters.push({
                            field: 'Name',
                            operator: 'contains',
                            value: textToSearchFor
                        });

                        filter.filters.push({
                            field: 'LocationDescription',
                            operator: 'contains',
                            value: textToSearchFor
                        });

                        if (this.isNumber(textToSearchFor)) {
                            filter.filters.push({
                                field: 'HealthPoints',
                                operator: 'eq',
                                value: textToSearchFor
                            });

                            filter.filters.push({
                                field: 'Id',
                                operator: 'eq',
                                value: textToSearchFor
                            });
                        }

                        list.filter(filter);
                    }
                    else {
                        list.filter([]);
                    }

                    return list.view();
                },

                placesToStayList: new kendo.data.DataSource({
                    serverFiltering: false,
                    transport: {
                        read: {
                            url: '/list',
                            contentType: 'application/json'
                        },
                        type: 'json'
                    },
                    schema: {
                        model: PlacesToStay
                    }
                })
            });

            //Force a fetch so that the table data is loaded before the page is bound.
            this.viewModel.placesToStayList.fetch();

            //The line before this and after this are hacks. In a very general case scenario, both are not needed.
            kendo.bind(this.$element, this.viewModel);

            //Now display the root element
            this.$element.show();
        }
    };


    /* PLUGIN DEFINITION
     =============================== */

    $.fn.placestostay = function (option) {
        var args = Array.prototype.slice.call(arguments, 1);

        return this.each(function () {
            var $this = $(this),
                data = $this.data("placestostay"),
                options = typeof option === "object" && option;

            if (!data) $this.data("placestostay", (data = new PlacesToStayViewModel(this, options)));
            if (typeof option === "string") data[option].apply(data, args);
        });
    };

    //Defaults if nay..
    $.fn.placestostay.defaults = {
        
    };


    $.fn.placestostay.Constructor = PlacesToStayViewModel;

   /**
   * jQuery namespace $.placestostay
   *
   * Global methods for configuring and interating with widget instances
   */
    $.placestostay = {
        /**
         * Sets global configuration options for the widget.
         *
         * @param {object} options, options to set
         */
        config: function (options) {
            $.extend($scope, options);
        }
    };

    // Initialization by HTML5 data-role
    $(window).on("load", function () {
        $("[data-role=placestostay]").each(function () {
            $(this).placestostay();
        });
    });

})(jQuery);