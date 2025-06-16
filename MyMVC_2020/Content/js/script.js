function reloadGrid() {
    $(".grid[data-role='grid']").each(function () {
        var id = $(this).prop("id");
        if ($("#" + id).length > 0) {
            //console.log("#" + $id + " refreshing...");
            var grid = $("#" + id).data("kendoGrid");
            grid.dataSource.read();
            grid.refresh();
        }
    });
}

function applyTicketFilter(filterGrid, filterValue) {
    if ($("#" + filterGrid).length > 0) {
        var grid = $("#" + filterGrid).data("kendoGrid");
        //console.log("filterValue:" + filterValue);
        if (filterValue != "") {
            grid.dataSource.filter([
                {
                    field: "Tickets",
                    //operator: "gte",
                    operator: function (item) {
                        //console.log("item:" + item);
                        return $.isNumeric(item) ? parseInt(item) >= filterValue : false;
                    }/*,
                    value: filterValue*/
                }
            ]);
        }
    }
}

function applySearchFilter(filterGrid, filterValue) {
    if ($("#" + filterGrid).length > 0) {
        var grid = $("#" + filterGrid).data("kendoGrid");
        //console.log("filterValue:" + filterValue);
        if (filterValue != "") {
            grid.dataSource.filter([
                {
                    field: "UserId",
                    operator: "contains",
                    value: filterValue
                }
            ]);
        }
    }
}

function clearFilters(filterGrid) {
    if ($("#" + filterGrid).length > 0) {
        var grid = $("#" + filterGrid).data("kendoGrid");
        grid.dataSource.filter({});
    }
}

function _GridErrorHandler(args) {
    if (args.status == "customerror") {
        alert(args.errors);
    }
    else {
        alert('error.');
    }
};

$(function () {

    // Captcha
    $(".newCaptcha").click(function (e) {
        var cmd = $(this);
        var url = cmd.find("img").first().attr("src");
        url = url.replace(/(.*?)\?r=\d+\.\d+$/, "$1")
        //console.log(url);
        var img = new Image();
        $(img).load(function () {
            cmd.empty().append(img);
        });
        img.src = url + "?r=" + Math.random();
        e.preventDefault();
    });

    // Custom Checkbox
    $('.button-checkbox').each(function () {
        // Settings
        var $widget = $(this),
            $button = $widget.find('button'),
            $checkbox = $widget.find('input:radio'),
            settings = {
                on: {
                    icon: 'glyphicon glyphicon-ok'
                },
                off: {
                    icon: 'glyphicon glyphicon-unchecked'
                }
            };

        // Event Handlers
        $button.on('click', function () {
            $checkbox.prop('checked', true);
            $checkbox.triggerHandler('change');
            updateDisplay();
        });

        // Actions
        function updateDisplay() {
            $('.button-checkbox').each(function () {
                var isChecked = $(this).find('input:radio').is(':checked');
                var button = $(this).find('button');
                // Set the button's state
                button.data('state', (isChecked) ? "on" : "off");
                // Set the button's icon
                button.find('.state-icon')
                    .removeClass()
                    .addClass('state-icon ' + settings[button.data('state')].icon);
                // Update the button's color
                var color = button.data('color');
                if (isChecked) {
                    button
                        .removeClass('btn-default')
                        .addClass('btn-' + color + ' active');
                } else {
                    button
                        .removeClass('btn-' + color + ' active')
                        .addClass('btn-default');
                }
            })
        }
        // Initialization
        function init() {
            updateDisplay();
            // Inject the icon if applicable
            if ($button.find('.state-icon').length == 0) {
                $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
            }
        }
        init();
    });

    // Win
    $("input[name='bet-check']").on("change", function () {
        var value = $("input[name='bet-check']:checked").val();
        var grid;
        if ($("#grid-WinPlace-Bet-pending").length) grid = $("#grid-WinPlace-Bet-pending").data("kendoGrid");
        if ($("#grid-WinPlace-Bet-transacted").length) grid = $("#grid-WinPlace-Bet-transacted").data("kendoGrid");
        if (grid) {
            if (value.indexOf("W") >= 0) {
                grid.showColumn("WinTickets");
                $(".section-winplace-bet-pending .grid-detail,.section-winplace-bet-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.showColumn(1);
                });
            } else {
                grid.hideColumn("WinTickets");
                $(".section-winplace-bet-pending .grid-detail,.section-winplace-bet-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.hideColumn(1);
                });
            }
            if (value.indexOf("P") >= 0) {
                grid.showColumn("PlaceTickets");
                $(".section-winplace-bet-pending .grid-detail,.section-winplace-bet-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.showColumn(1);
                });
            } else {
                grid.hideColumn("PlaceTickets");
                $(".section-winplace-bet-pending .grid-detail,.section-winplace-bet-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.hideColumn(1);
                });
            }
        }
    });

    // Plc
    $("input[name='eat-check']").on("change", function () {
        var value = $("input[name='eat-check']:checked").val();
        var grid;
        if ($("#grid-WinPlace-Eat-pending").length) grid = $("#grid-WinPlace-Eat-pending").data("kendoGrid");
        if ($("#grid-WinPlace-Eat-transacted").length) grid = $("#grid-WinPlace-Eat-transacted").data("kendoGrid");
        if (grid) {
            if (value.indexOf("W") >= 0) {
                grid.showColumn("WinTickets");
                $(".section-winplace-eat-pending .grid-detail,.section-winplace-eat-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.showColumn(1);
                });
            } else {
                grid.hideColumn("WinTickets");
                $(".section-winplace-eat-pending .grid-detail,.section-winplace-eat-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.hideColumn(1);
                });
            }
            if (value.indexOf("P") >= 0) {
                grid.showColumn("PlaceTickets");
                $(".section-winplace-eat-pending .grid-detail,.section-winplace-eat-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.showColumn(2);
                });
            } else {
                grid.hideColumn("PlaceTickets");
                $(".section-winplace-eat-pending .grid-detail,.section-winplace-eat-transacted .grid-detail").each(function () {
                    var gridDetail = $(this).data("kendoGrid");
                    gridDetail.hideColumn(2);
                });
            }
        }
    });

    // Ticket Filter
    $(".TicketFilter").on("input", function () {
        var grid = $(this).data("grid").split(",");
        var filter = $(this).val();
        //console.log("filter=" + filter);
        if (grid.length > 0) {
            if (filter == "") {
                $.each(grid, function (index, value) {
                    clearFilters(value);
                });
            } else {
                $.each(grid, function (index, value) {
                    applyTicketFilter(value, filter);
                });
            }
        }
    });

    // Show All / Favorites Only
    $("div#filter > a").on("click", function () {
        if (!$(this).hasClass("active")) {
            $(this).addClass("active")
                .parent().find('a').not(this).removeClass("active");

            // Filter
            var filter = $(this).data("filter");
            $(".grid[data-role='grid']").each(function () {
                var id = $(this).prop("id");
                if ($("#" + id).length > 0) {
                    var grid = $("#" + id).data("kendoGrid");
                    if (filter) {
                        grid.dataSource.filter([
                        {
                            field: "IsFavorite",
                            operator: "eq",
                            value: true
                        }
                        ]);
                    } else {
                        grid.dataSource.filter({});
                    }
                }
            });
        }
    });

    // Search Textbox
    $("input[name='search']").on("input", function () {
        var value = $(this).val();

        $(".grid[data-role='grid']").each(function () {
            var id = $(this).prop("id");
            if ($("#" + id).length > 0) {
                if (value == "") {
                    clearFilters(id);
                } else {
                    applySearchFilter(id, value);
                }
            }
        });
    });

});