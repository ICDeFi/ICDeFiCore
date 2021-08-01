(function ( $, window, document ) {
	
    $.fn.acc = function ( options ) {

		// Default settings
        var settings = $.extend({}, $.fn.acc.settings, options);

        return this.each(function () {
            var container = $(this);
            $(container).append('<div class="acc-small-screen-check"></div>');
            var accordionItemContent = $(container).find('.accordion-item-content');
            var smallScreen;
            var accordionItemContainer =  $(container).find('.accordion-item-container');
            var numOfElem = accordionItemContainer.length;
            
            screenSizeChk();

            if ( smallScreen ) {
                // Set accordion and navigation for small screens
                smallAccordionReset();
                smallAccordionNav();
            } else {
                // Set accordion desktop screens
                accordionContentWidth();
                desktopAccordionReset();
            }

            var elemWidth = accordionItemContainer.width();
            var containerWidth = $('.accordion-container').width();
            var elemSmallWidth = (containerWidth - elemWidth) / (numOfElem - 1);
            var elemMargin;

            // Open/Slide accordion desktop screens
            $('.accordion').on("click", ".accordion-item-container", function () {         
                var $that = $(this);
                var allItems = $('.accordion-item-container');
                if (!$(allItems).is(':animated') && !$that.hasClass("active")) {

                    //desktopAccordionReset();

                    var totPrevElem = $that.prevAll().length;
                    var pos = $that.index();
                    var zIndex = $that.css("z-index");

                    $that.css({
                        "z-index": 999999
                    });

                    $that.siblings().removeClass("active");
                    $that.stop().animate({
                        width: elemWidth,
                        left: elemSmallWidth * pos
                    }, 300, function () { $that.addClass("active") });

                    //var prevElems = $that.prevAll();
                    var totPrevElem = $that.prevAll().length;
                    // var nextElems = $that.nextAll();
                    // var totNextElem = $that.nextAll().length;

                    $that.siblings().animate({
                        width: elemSmallWidth
                    }, 300);

                    for (var i = 0; i < totPrevElem; i++) {
                        $(accordionItemContainer).eq(i).animate({
                            left: elemSmallWidth * i
                        }, 300);
                    }
                }                  
            })

            // Accordion item close button
            $('.accordion .close').on("click", function (e) {
                screenSizeChk();
                
                if (smallScreen) {
                    e.stopPropagation();
                    var $this = $(this),
                        parentBox = $this.closest('.accordion-item-content');
                    parentBox.parent().removeClass('active');
                    parentBox.animate({
                        left: 0
                    });
                    
                } else {
                    e.stopPropagation();
                    desktopAccordionReset();    
                }
            });

            // Slide item small screens
            $('.accordion-item-image').on('click touchstart touchmove', function () {
                screenSizeChk();
                if (smallScreen) {
                    var windowWidth = $(window).width();
                    var $this = $(this),
                        parentBox = $this.closest('.accordion-item-content');
                        parentBox.parent().addClass('active');
                        parentBox.animate({
                            left: "-" + windowWidth
                        });
                };
            });

            // Slider dots navigation small screens
            $('.accordion-container').on('click', '.acc-xs-nav > li', function () {
                screenSizeChk();
                if (smallScreen) {
                    // Close any open item
                    $('.accordion-item-container').removeClass('active');
                    $('.accordion-item-content').animate({left: 0});

                    // Open selected item
                    var windowWidth = $(window).width(),
                        $this = $(this),
                        itemPos = $this.attr('class');

                    $('.acc-xs-nav > li').removeClass('active');
                    $this.addClass('active');

                    $('.accordion').animate({
                        left: "-" + (itemPos * windowWidth)
                    });
                };
            });

            // Reset accordion on screen resize
            var resizeTimer;
            $(window).resize(function () {
                clearTimeout(resizeTimer);
                resizeTimer = setTimeout(function () {
                    screenSizeChk();
                    if (smallScreen) {
                        // Set accordion and navigation for small screens
                        smallAccordionReset();
                        smallAccordionNav();
                    } else {
                        accordionContentWidth();
                        desktopAccordionReset();
                        elemWidth = accordionItemContainer.width();
                        containerWidth = $('.accordion-container').width();
                        elemSmallWidth = (containerWidth - elemWidth) / (numOfElem - 1);
                    }
                }, 100);
            });

            /* ==== Functions ==== */
            
            // Check screen size          
            function screenSizeChk() {
                if ($('.acc-small-screen-check').css("display") === "block" ) {
                    smallScreen = true;
                } else {
                    smallScreen = false;
                }
                return smallScreen;
            }

            // Resize margins and images size on window resize 
            function desktopAccordionReset() {
                $('.accordion').css("left", "0");
                $('.accordion-item-container').removeClass("active");
                containerWidth = $('.accordion-container').width();
                //numOfElem = $('.accordion-item-container').length;
                elemMargin = containerWidth / numOfElem;
                elemSmallWidth = (containerWidth - elemWidth) / (numOfElem - 1);
                
                
                $('.accordion-item-container').removeClass('active');
                accordionItemContent.css({ left: "0" });

                $('.accordion-item-container').css({
                    float: "left"
                });

                for (var i = 0; i < numOfElem; i++) {
                    // Reset margin
                    if (i == 0) {
                        $('.accordion-item-container').eq(i).animate({
                            left: 0,
                            "z-index": 9999
                        }, { duration: 300, queue: false });
                    } else {
                        var newMargin = elemMargin * i;

                        $('.accordion-item-container').eq(i).animate({
                            left: newMargin,
                            "z-index": 9999 + i
                        }, { duration: 300, queue: false });
                    }

                    // Reset width (same as margin)
                    $('.accordion-item-container').eq(i).animate({
                        width: elemMargin
                    }, { duration: 300, queue: false })
                }
            }

            // Set accordion container and items widths for big screens
            function accordionContentWidth() {
                $(container).css({
                    width: settings.containerWidth
                });
                $(accordionItemContent).css({
                    "max-width": $(window).width(),
                    "width": settings.itemWidth
                });
                $('.accordion-item-content > div').removeAttr("style");
                $('.accordion-item-container').removeAttr("style");
            }

            // Set accordion style for small screens
            function smallAccordionReset() {
                // Close if any item is open
                $('.accordion-item-container').removeClass('active');

                // Set small screen container and elements sizes
                $('.accordion-item-content').css({
                    "max-width": "200%",
                    "width": ($(window).width()* 2)
                });
                $('.accordion-item-content > div:last-of-type, .accordion-item-container, .accordion-item-content > div:first-of-type').css({
                    "max-width":    "100%",
                    "width":        $(window).width(),
                    "float":        'left'
                });

                // Append touch icon element
                if( $('.touch-icon').length === 0) {
                    $(container).append('<div class="touch-icon icon-swipe-left"></div>');
                }
            };

            // Append small screens navigation if needed
            function smallAccordionNav() {
                var totalItems = $('.accordion-item-container').length;
                if (totalItems > 1 && $('.acc-xs-nav').length === 0 && settings.smallScreenDotsNav) {
                    $(container).append('<ul class="acc-xs-nav clearfix"></ul>');
                    for (var i = 0; i < totalItems ; i++) {
                        $('.acc-xs-nav').append('<li class="' + i + '"><a href="#"></a></li>');
                    };
                    $('.acc-xs-nav > li:first-child').addClass('active');
                };
            }
		});
	};


    // Default settings
    $.fn.acc.settings = {
        accordionWidth: '100%', 
        itemWidth: '75%',
        smallScreenDotsNav: true
    };

	

})( jQuery, window, document);

$(function () {
    $('.accordion-container').acc({
    	containerWidth: '1000px',
    	itemWidth: '750px',
    	smallScreenDotsNav: true
    });
});