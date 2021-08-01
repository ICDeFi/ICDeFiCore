$(".carousel").swipe({

  swipe: function(event, direction, distance, duration, fingerCount, fingerData) {

	if (direction == 'left') $(this).carousel('next');
	if (direction == 'right') $(this).carousel('prev');

  },
  allowPageScroll: "vertical"
});
$(".carousel-item").carousel({
	items: 1,
	autoPlay: 5000,
	itemsDesktop: [1199, 1],
	itemsDesktopSmall: [979, 1],
	itemsTablet: [768, 1]
});
	

 	