$(function(){
   'use strict'
	
	 $('.dropify').dropify({
		messages: {
			'default': 'Drag and drop a file here or click',
			'replace': 'Drag and drop or click to replace',
			'remove': 'Remove',
			'error': 'Ooops, something wrong appended.'
		},
		error: {
			'fileSize': 'The file size is too big (2M max).'
		}
	});
  

   // Time Picker
   $('#tpBasic').timepicker();
   $('#tp2').timepicker({'scrollDefault': 'now'});
   $('#tp3').timepicker();

   $('#setTimeButton').on('click', function (){
	 $('#tp3').timepicker('setTime', new Date());
   });
	
	//Colorpicker
	$('.my-colorpicker1').colorpicker()
	
	//color picker with addon
	$('.my-colorpicker2').colorpicker()
	
	//Timepicker
	$('.timepicker').timepicker({
		showInputs: false
	})

});