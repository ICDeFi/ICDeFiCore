class Gallery {

  constructor(options) {
    Object.assign(this, {
            nav: ["<span> < </span>", "<span> > </span>"],
            close: "<span>X</span>",
            showNav: true,
            showNavIfOneItem: false,
            aspectRatio: "auto",
            showDots: false,
            showDotsIfOneItem: false,
            arrows: false,
            fullScreen: true,
            loop: true,
            autoplay: false,
            autoplayDelay: 2000,
            transition: "fade",
            transitionTime: 500,
        }, options);

    this.itemCount = this.gallery.find('img').length;
    this.agi = 0;
    let t=this;

    this.renderGallery();

    jQuery('.g_s-img img').eq(0).on("load", function () {
        t.calcGalleryHeight();
    });

    jQuery(window).on("load", function () {
        t.countMoveRight();
    });

    jQuery(window).on('resize', function () {
		t.countMoveRight();
		t.setLeftSmallImages();
        t.calcGalleryHeight();
	});

    jQuery(this.gallery).on('change', function(event, param) {
        t.changeImg(param);
    });

    jQuery(this.gallery).on('click', '.g_s-img img', function(e) {
        t.changeImg(e);
    });

    jQuery(this.gallery).on('click', '.prev', function() {
        t.changeImg('prev');
    });

    jQuery(this.gallery).on('click', '.next', function() {
        t.changeImg('next');
    });

    if (this.fullScreen) {
        jQuery(this.gallery).on('click', '.g_img-cont', function() {
            t.showFullScreen();
        });

        jQuery(this.gallery).on('click', '.g_f-s-image', function() {
            t.closeFullScreen();
        });
    }

    jQuery(this.gallery).on('click', '.dot', function(e) {
        e.stopPropagation();
        t.changeImg(parseInt(jQuery(this).attr('data-item')));
    });

    jQuery(this.gallery).on('click', '.big-arrows > *:first-child', function(e) {
        e.stopPropagation();
        t.changeImg("prev");
    });

    jQuery(this.gallery).on('click', '.big-arrows > *:last-child', function(e) {
        e.stopPropagation();
        t.changeImg("next");
    });

    this.xDown = null;
    this.yDown = null;

    this.gallery.on('touchstart', function(e) {
        t.xDown = e.originalEvent.touches[0].clientX;
        t.yDown = e.originalEvent.touches[0].clientY;
    });

    this.gallery.on('touchmove', function(e) {
        t.handleTouchMove(e);
    });

    if (this.autoplay) {
        var interval = setInterval(function() {
            t.changeImg('next');
        }, t.autoplayDelay);

        this.gallery.on('mouseenter', function() {
            clearInterval(interval);
        });

        this.gallery.on('mouseleave', function() {
            interval = setInterval(function() {
                t.changeImg('next');
            }, t.autoplayDelay);
        });
    }
  }

  calcGalleryHeight() {
    var galleryWidth = this.gallery.find('.g_img-cont').width();

    if (this.aspectRatio != "auto" && jQuery.isNumeric(this.aspectRatio)) {
      this.gallery.find('.g_img-cont').css('height', (galleryWidth/this.aspectRatio));
     }
  }

  countMoveRight() {
      if (this.itemCount > 1 ) {
          this.g_navDivWidth = this.gallery.find(".g_nav")[0].clientWidth;
          this.fullSmallWidth = 0;
          this.item = 0;

          for (var i = 0; i < this.itemCount; i++) {
              this.item = this.gallery.find('.g_s-img').get(i).clientWidth;
              this.fullSmallWidth += this.item + parseInt(this.gallery.find(".g_s-img").css('marginRight')) + parseInt(this.gallery.find(".g_s-img").css('marginLeft'));
          }

          this.moveRight = (this.fullSmallWidth - this.g_navDivWidth) / (this.gallery.find('.g_s-img').length - 1);
    }
  }

  setLeftSmallImages() {
      this.gallery.find(".g_s-img").css("left", "-" + this.agi * this.moveRight + "px");
  }

  loadActiveImg(cb) {
      let t=this;
      t.bigImageSrc = t.gallery.find(".g_s-img img").eq(t.agi).attr('src');
      this.gallery.find('.agi').removeClass('agi');

      if(cb) {
          cb(t);
      }

      t.gallery.find(".g_s-img").eq(t.agi).addClass("agi");
      if (this.transition == "fade" ) {
          if (t.gallery.find(".g_m-img").attr('src') !== "#") {
              t.gallery.find(".g_img-cont .g_m-img").not('.cloned').clone().appendTo(t.gallery.find(".g_img-cont .g_m-img").parent()).css({position: "absolute", height: "100%", width:"100%"}).addClass('cloned');
              t.gallery.find(".g_f-s .g_m-img").not('.cloned').clone().appendTo(t.gallery.find(".g_f-s .g_m-img").parent()).css({position: "absolute"}).addClass('cloned');
          }

          t.gallery.find(".g_m-img").not('.cloned').attr('src', t.bigImageSrc).hide().fadeIn(t.transitionTime/2);

          t.gallery.find(".g_m-img.cloned").fadeOut(t.transitionTime, function() {
              jQuery(this).remove();
          });

      } else if (this.transition == "slide") {
          if (!t.slideDirection) {
            t.gallery.find(".g_m-img").not('.cloned').attr('src', t.bigImageSrc);
          } else {
              t.gallery.find(".g_img-cont .g_m-img").not('.cloned').clone().appendTo(t.gallery.find(".g_img-cont .g_m-img").parent()).css({position: "absolute", height: "100%", width:"100%"}).addClass('cloned');
              t.gallery.find(".g_f-s .g_m-img").not('.cloned').clone().appendTo(t.gallery.find(".g_f-s .g_m-img").parent()).css({position: "absolute"}).addClass('cloned');
              if (this.slideDirection === "left") {
                   t.gallery.find(".g_m-img").not('.cloned').css({left: "100%"}).attr('src', t.bigImageSrc);
                   t.gallery.find(".g_m-img").not('.cloned').animate({left: "0"}, t.transitionTime);
                   t.gallery.find(".g_m-img.cloned").animate({left: "-100%"}, t.transitionTime, function() {jQuery(this).remove();});
              } else {
                    t.gallery.find(".g_m-img").not('.cloned').css({left: "-100%"}).attr('src', t.bigImageSrc);
                    t.gallery.find(".g_m-img").not('.cloned').css({left: "-100%"}).attr('src', t.bigImageSrc);
                    t.gallery.find(".g_m-img").not('.cloned').animate({left: "0"}, t.transitionTime);
                    t.gallery.find(".g_m-img.cloned").animate({left: "100%"}, t.transitionTime, function() {jQuery(this).remove();});
              }
          }

      } else {
          t.gallery.find(".g_m-img").not('.cloned').attr('src', t.bigImageSrc);
      }

      t.gallery.find(".dot").removeClass('dot-active');
      t.gallery.find(".dot[data-item='"+t.agi+"']").addClass('dot-active');
  }

  changeImg(e) {
      this.changed=false;
      if (this.itemCount>1) {
          if (e === 'prev') {
              if (this.agi > 0) {
                  this.agi--;
                  this.changed=true;
              } else if (this.agi === 0 && this.loop) {
                  this.agi = this.gallery.find(".g_s-img").length - 1;
              }
              if (this.transition === "slide") {
                  this.slideDirection = "left";
              }
          } else if (e === 'next') {
              if (this.agi < this.gallery.find(".g_s-img").length - 1) {
                  this.agi++;
                  this.changed=true;
              } else if (this.agi === this.gallery.find(".g_s-img").length - 1 && this.loop) {
                  this.agi = 0;
              }
              if (this.transition ==="slide") {
                 this.slideDirection = "right";
              }
          } else if (typeof e === "number") {
              if (e>=0 && e<this.itemCount) {
                  if (e > this.agi && this.transition ==="slide") {
                      this.slideDirection = "right";
                  } else {
                      this.slideDirection = "left";
                  }
                 this.changed=true;
                 this.agi=e;
              }
          } else {
              this.images = jQuery.map(this.gallery.find(".g_s-img img"), function (i) {
                  return i.src;
              });
              this.imgsrc = jQuery(e.target)[0].src;
              this.changed=true;

              if (this.images.indexOf(this.imgsrc) > this.agi && this.transition ==="slide") {
                  this.slideDirection = "right";
              } else {
                  this.slideDirection = "left";
              }
              this.agi = this.images.indexOf(this.imgsrc);
          }
          if (this.changed || this.loop) {
              this.loadActiveImg();
              this.setLeftSmallImages();
          }

      }
  }

  showFullScreen() {
      this.gallery.css('z-index', '99999');
      this.gallery.find(".g_f-s-hidden").fadeIn(400);
  }

  closeFullScreen() {
    this.gallery.find(".g_f-s-hidden").fadeOut(400, function() {
        this.gallery.removeAttr('style');
    }.bind(this));
  }

  renderGallery() {

      this.gallery.find('img').wrap('<div class="g_s-img"></div>');
      this.gallery.find('.g_s-img').wrapAll('<div class="g_nav" />');
      this.gallery.children().wrapAll('<div class="g_nav-cont" />');
      jQuery(`<div class="arrow-div prev">
                      `+ this.nav[0]+`
                  </div>`).prependTo(this.gallery.find('.g_nav-cont'));
      if (!this.showNav) {
          this.gallery.find('.g_nav-cont').hide();
      }
      jQuery(`<div class="arrow-div next">
                      `+ this.nav[1]+`
                  </div>`).appendTo(this.gallery.find('.g_nav-cont'));
      jQuery(`<div class="g_img-cont">
                  <div class="g_b-img">
                      <img class="g_m-img" src="#">
                  </div>
              </div>`).appendTo(this.gallery);

      if(this.arrows && this.itemCount>1) {
          jQuery(`<div class="big-arrows"><div>`+ this.nav[0]+`</div><div>`+ this.nav[1] + `</div></div>`).appendTo(this.gallery.find('.g_img-cont'));
      }

      if(this.showDots && this.itemCount>1 || this.showDotsIfOneItem) {
          jQuery(`<div class="dots"></div>`).appendTo(this.gallery.find('.g_img-cont'));
          for ( let i=0; i<this.itemCount; i++) {
              jQuery(`<div class="dot" data-item="`+i+`"></div>`).appendTo(this.gallery.find('.dots'));
          }
      }

      this.gallery.children().wrapAll('<div class="gallery-row" />');
      if (this.fullScreen) {
          jQuery(`<div class="g_f-s-hidden">
                      <div class="g_f-s">
                          <div class="g_f-s-arrow g_f-s-arrow-left prev">
                             `+ this.nav[0]+`
                          </div>
                          <div class="g_f-s-image">
                              <img class="g_m-img" src="#">
                              <div class="close-button">`+ this.close +`</div>
                          </div>
                          <div class="g_f-s-arrow g_f-s-arrow-right next">
                             `+ this.nav[1]+`
                          </div>
                      </div>
                  </div>`).appendTo(this.gallery);
        } else {
            this.gallery.find('.g_img-cont').css("cursor", "auto");
        }

        this.loadActiveImg(function(t) {
            if (t.itemCount < 2 && !t.showNavIfOneItem) {
                t.gallery.find('.g_nav-cont, .g_f-s-arrow').remove();
            }
        });
    }

    handleTouchMove(e) {
        if ( ! this.xDown || ! this.yDown ) {
            return;
        }

        var xUp = e.originalEvent.touches[0].clientX;
        var yUp = e.originalEvent.touches[0].clientY;

        this.xDiff = this.xDown - xUp;
        this.yDiff = this.yDown - yUp;

        if ( Math.abs( this.xDiff ) > Math.abs( this.yDiff ) ) {
            if ( this.xDiff > 0 ) {
                this.changeImg('prev');
            } else {
                this.changeImg('next');
            }
        }

        this.xDown = null;
        this.yDown = null;
    }
}

jQuery.fn.initGallery = function(options) {
      if(!options) {
          options={};
      }
      var galleries=[];
      for (let i=0; i<this.length; i++) {
          options.gallery = this.eq(i);
          galleries[i] = new Gallery(options);
      }
 };
