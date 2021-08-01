/* The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

"use strict";
$(document).ready(function () {

    (function ($) {
        $.fn.freeSimpleSlider = function (options) {

            //==== SETTINGS ====
            var settings = $.extend({
                time: 5000,
                animation: "slide",
                arrows: true,
                dots: true
            }, options);


            // ==== "Each" refers to each slider on the page  ====
            return this.each(function () {
                var self = $(this);
                var slideLength = self.children('li').length;
                var timer = parseInt(settings.time, 10);


                //====  adds new element to HTML's DOM tree ====
                self.addClass('slider-box').wrap("<div class='free-simple-slider'></div>");
                self.children('li:first').addClass('current');
                if (settings.animation === "slide") {
                    self.addClass('slide-animation');
                }
                if (settings.animation === "fade") {
                    self.addClass('slide-fade');
                }


                // ==== ADD ARROWS ==== 
                if (settings.arrows === true) {
                    self.after('<a class="arrows prev-arrow" href="#"><i class="fa fa-angle-left"></i></a><a class="arrows next-arrow" href="#"><i class="fa fa-angle-right"></i></a>');
                }
                // ==== ADD DOTS ====
                if (settings.dots === true) {
                    self.after('<ul class="slider-dots"></ul>');
                    var dotsContainer = self.parent('.free-simple-slider').children('.slider-dots');
                    for (var i = 0; i < slideLength; i++) {
                        dotsContainer.append('<li></li>');
                    }
                    dotsContainer.children('li').eq(0).addClass('current');
                }

                // ==== img src attribute change to background ====
                self.children('li').children('img').each(function () {
                    var imgUrl = $(this).attr('src');
                    $(this).parent('li').css('background-image', 'url(' + imgUrl + ')');
                });



                // ==== CHANGE SLIDES FUNCTION ==== function with slider animation
                var changeSlides = (function (normalDirection) {

                    var current = self.children('li.current');
                    var currentIndex = self.children('li.current').index();
                    var nextDot;
                    var currentDot = dotsContainer.children('li.current');

                    //==== Direction of changing slides ====
                    var next;
                    if (normalDirection === false) {
                        next = self.children('li').eq(currentIndex - 1);

                        if (currentIndex - 1 < 0) {
                            next = self.children('li:last');
                        }

                    } else {
                        next = self.children('li').eq(currentIndex + 1);
                        if (currentIndex + 1 === slideLength) {
                            next = self.children('li:first');
                        }
                    }


                    // ==== ANIMATION BASIC ====
                    if (settings.animation === "basic") {
                        next.addClass('current');
                        current.removeClass('current');
                    }

                    // ==== ANIMATION FADE IN/OUT ====
                    if (settings.animation === "fade") {
                        next.addClass('current');
                        current.removeClass('current').addClass('prev');
                        setTimeout(function () {
                            self.children('li.prev').removeClass('prev');
                        }, 500);
                    }

                    // ==== ANIMATION SLIDE ====
                    if (settings.animation === "slide") {
                        next.addClass('current');
                        current.removeClass('current').addClass('prev');
                        setTimeout(function () {
                            self.children('li.prev').removeClass('prev');
                        }, 250);
                    }



                    //==== DOTS CHANGE ====
                    if (settings.dots === true) {
                        nextDot = dotsContainer.children('li').eq(next.index());// next.index() <- allows to use the same logic as in slides change
                        currentDot.removeClass('current');
                        nextDot.addClass('current');
                    }

                });

                // ==== FIRE FUNCTION INTERWAL startChangeSlides()==== fires "changeSlides" function after time which is set in timer variable
                var interval;
                function startChangeSlides() {
                    interval = setInterval(function () {
                        changeSlides(true);
                    }, timer);
                }
                startChangeSlides();

                // ==== DOT NAVIGATION ==== changing slide with choose animation after click on dot navigation
                if (settings.dots === true) {
                    dotsContainer.children('li').on("click", function () {

                        clearInterval(interval);
                        dotsContainer.children('li').removeClass('current');

                        if (settings.animation === "basic") {
                            self.children('li').removeClass('current');
                        }

                        if (settings.animation === "fade") {
                            self.children('li.current').addClass('prev').removeClass('current');
                            setTimeout(function () {
                                self.children('li.prev').removeClass('prev');
                            }, 500);
                        }

                        if (settings.animation === "slide") {
                            self.children('li.current').addClass('prev').removeClass('current');
                            setTimeout(function () {
                                self.children('li.prev').removeClass('prev');
                            }, 250);
                        }

                        $(this).addClass('current');
                        var clickedDotIndex = $(this).index();
                        self.children('li').eq(clickedDotIndex).addClass('current');
                        startChangeSlides();
                    });
                }

                // ==== ARROW NAVIGATION ==== Changing slides after click on arrow animation
                if (settings.arrows === true) {
                    self.parent('.free-simple-slider').children('.prev-arrow').on('click', function (event) {
                        event.preventDefault();
                        clearInterval(interval);
                        changeSlides(false);
                        startChangeSlides();
                    });

                    self.parent('.free-simple-slider').children('.next-arrow').on('click', function (event) {
                        event.preventDefault();
                        clearInterval(interval);
                        changeSlides(true);
                        startChangeSlides();
                    });
                }


                //===== HAMMER PLUGIN WITH SWIPE EVENT =====
                if (typeof Hammer !== "undefined") {
                    var hammertime = new Hammer(self[0]);
                    hammertime.on('swipeleft', function () {
                        clearInterval(interval);
                        changeSlides(true);
                        startChangeSlides();
                    });
                    hammertime.on('swiperight', function () {
                        clearInterval(interval);
                        changeSlides(false);
                        startChangeSlides();
                    });
                }

            });
        };

    }(jQuery));
});



