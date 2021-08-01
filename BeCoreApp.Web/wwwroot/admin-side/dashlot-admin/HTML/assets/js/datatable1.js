$(function(e) {
	var editor; // use a global for the submit and return data rendering in the examples
 
$(document).ready(function() {
    editor = new $.fn.dataTable.Editor( {
        
        table: "#edit-table",
        fields: [ {
                label: "First name:",
                name: "first_name"
            }, {
                label: "Last name:",
                name: "last_name"
            }, {
                label: "Position:",
                name: "position"
            }, {
                label: "Office:",
                name: "office"
            }, {
                label: "Extension:",
                name: "extn"
            }, {
                label: "Start date:",
                name: "start_date",
                type: 'datetime'
            }, {
                label: "Salary:",
                name: "salary"
            }
        ]
    } );
 
    var table = $('#edit-table').DataTable( {
        lengthChange: false,
        ajax: "../php/staff.php",
        columns: [
            { data: null, render: function ( data, type, row ) {
                // Combine the first and last names into a single table field
                return data.first_name+' '+data.last_name;
            } },
            { data: "position" },
            { data: "office" },
            { data: "extn" },
            { data: "start_date" },
            { data: "salary", render: $.fn.dataTable.render.number( ',', '.', 0, '$' ) }
        ],
        select: true
    } );
 
    // Display the buttons
    new $.fn.dataTable.Buttons( table, [
        { extend: "create", editor: editor },
        { extend: "edit",   editor: editor },
        { extend: "remove", editor: editor }
    ] );
 
    table.buttons().container()
        .appendTo( $('.col-md-6:eq(0)', table.table().container() ) );
} );
} );
