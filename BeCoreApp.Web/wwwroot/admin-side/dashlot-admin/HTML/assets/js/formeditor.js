$(function(e) {
	$('.content').richText();
	$('.content2').richText();
	$('.select2').select2({
		minimumResultsForSearch: Infinity
	});

	// Colored Hover
	$('#select2').select2({
		dropdownCssClass: 'hover-success',
		minimumResultsForSearch: Infinity // disabling search
	});
});