(function($) {
    "use strict";	
	
	// live handler
	   lc_lightbox('.elem', {
		   wrap_class: 'lcl_fade_oc',
		   gallery : true, 
		   thumb_attr: 'data-lcl-thumb', 
		   
		   skin: 'minimal',
		   radius: 0,
		   padding : 0,
		   border_w: 0,
	   }); 
	   $(".shuffle-me").shuffleImages({
		target: ".images > img"
	  });

})(jQuery);