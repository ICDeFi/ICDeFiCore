(function($) {
	"use strict";
	
	var $TABLE = $('#table-1');
	var $BTN = $('#export-btn');
	var $EXPORT = $('#export');

	$('.table-add').on("click",function () {
		var $clone = $TABLE.find('tr.hide').clone(true).removeClass('hide table-line');
		$TABLE.find('table').append($clone);
	});

	$('.table-remove').on("click",function () {
		$(this).parents('tr').detach();
	});

	$('.table-up').on("click",function () {
		var $row = $(this).parents('tr');
		if ($row.index() === 1) return; // Don't go above the header
		$row.prev().before($row.get(0));
	});

	$('.table-down').on("click",function () {
		var $row = $(this).parents('tr');
		$row.next().after($row.get(0));
	});

	// A few jQuery helpers for exporting only
	jQuery.fn.pop = [].pop;
	jQuery.fn.shift = [].shift;

	$BTN.click(function () {
	var $rows = $TABLE.find('tr:not(:hidden)');
	var headers = [];
	var data = [];

	// Get the headers (add special header logic here)
	$($rows.shift()).find('th:not(:empty)').each(function () {
		headers.push($(this).text().toLowerCase());
	});

	// Turn all existing rows into a loopable array
	$rows.each(function () {
	var $td = $(this).find('td');
	var h = {};

	// Use the headers from earlier to name our hash keys
	headers.forEach(function (header, i) {
		h[header] = $td.eq(i).text();
	});

	data.push(h);
	});

	// Output the result
	$EXPORT.text(JSON.stringify(data));
	});
	
	
///////////////////////////////////table1////////////////////////////////////



var $TABLE1 = $('#table1');
	var $BTN = $('#export-btn');
	var $EXPORT = $('#export');

	$('.table-add').on("click",function () {
		var $clone = $TABLE1.find('tr.hide').clone(true).removeClass('hide table-line');
		$TABLE1.find('table').append($clone);
	});

	$('.table-remove').on("click",function () {
		$(this).parents('tr').detach();
	});

	$('.table-up').on("click",function () {
		var $row = $(this).parents('tr');
		if ($row.index() === 1) return; // Don't go above the header
		$row.prev().before($row.get(0));
	});

	$('.table-down').on("click",function () {
		var $row = $(this).parents('tr');
		$row.next().after($row.get(0));
	});

	// A few jQuery helpers for exporting only
	jQuery.fn.pop = [].pop;
	jQuery.fn.shift = [].shift;

	$BTN.click(function () {
	var $rows = $TABLE1.find('tr:not(:hidden)');
	var headers = [];
	var data = [];

	// Get the headers (add special header logic here)
	$($rows.shift()).find('th:not(:empty)').each(function () {
		headers.push($(this).text().toLowerCase());
	});

	// Turn all existing rows into a loopable array
	$rows.each(function () {
	var $td = $(this).find('td');
	var h = {};

	// Use the headers from earlier to name our hash keys
	headers.forEach(function (header, i) {
		h[header] = $td.eq(i).text();
	});

	data.push(h);
	});

	// Output the result
	$EXPORT.text(JSON.stringify(data));
	});
	
	
	///////////////////////////////////table2////////////////////////////////////



	var $TABLE2 = $('#table2');
	var $BTN = $('#export-btn');
	var $EXPORT = $('#export');

	$('.table-add').on("click",function () {
		var $clone = $TABLE2.find('tr.hide').clone(true).removeClass('hide table-line');
		$TABLE2.find('table').append($clone);
	});

	$('.table-remove').on("click",function () {
		$(this).parents('tr').detach();
	});

	$('.table-up').on("click",function () {
		var $row = $(this).parents('tr');
		if ($row.index() === 1) return; // Don't go above the header
		$row.prev().before($row.get(0));
	});

	$('.table-down').on("click",function () {
		var $row = $(this).parents('tr');
		$row.next().after($row.get(0));
	});

	// A few jQuery helpers for exporting only
	jQuery.fn.pop = [].pop;
	jQuery.fn.shift = [].shift;

	$BTN.click(function () {
	var $rows = $TABLE2.find('tr:not(:hidden)');
	var headers = [];
	var data = [];

	// Get the headers (add special header logic here)
	$($rows.shift()).find('th:not(:empty)').each(function () {
		headers.push($(this).text().toLowerCase());
	});

	// Turn all existing rows into a loopable array
	$rows.each(function () {
	var $td = $(this).find('td');
	var h = {};

	// Use the headers from earlier to name our hash keys
	headers.forEach(function (header, i) {
		h[header] = $td.eq(i).text();
	});

	data.push(h);
	});

	// Output the result
	$EXPORT.text(JSON.stringify(data));
	});
	
	///////////////////////////////////table3////////////////////////////////////



	var $TABLE3 = $('#table3');
	var $BTN = $('#export-btn');
	var $EXPORT = $('#export');

	$('.table-add').on("click",function () {
		var $clone = $TABLE3.find('tr.hide').clone(true).removeClass('hide table-line');
		$TABLE3.find('table').append($clone);
	});

	$('.table-remove').on("click",function () {
		$(this).parents('tr').detach();
	});

	$('.table-up').on("click",function () {
		var $row = $(this).parents('tr');
		if ($row.index() === 1) return; // Don't go above the header
		$row.prev().before($row.get(0));
	});

	$('.table-down').on("click",function () {
		var $row = $(this).parents('tr');
		$row.next().after($row.get(0));
	});

	// A few jQuery helpers for exporting only
	jQuery.fn.pop = [].pop;
	jQuery.fn.shift = [].shift;

	$BTN.click(function () {
	var $rows = $TABLE3.find('tr:not(:hidden)');
	var headers = [];
	var data = [];

	// Get the headers (add special header logic here)
	$($rows.shift()).find('th:not(:empty)').each(function () {
		headers.push($(this).text().toLowerCase());
	});

	// Turn all existing rows into a loopable array
	$rows.each(function () {
	var $td = $(this).find('td');
	var h = {};

	// Use the headers from earlier to name our hash keys
	headers.forEach(function (header, i) {
		h[header] = $td.eq(i).text();
	});

	data.push(h);
	});

	// Output the result
	$EXPORT.text(JSON.stringify(data));
	});
})(jQuery);