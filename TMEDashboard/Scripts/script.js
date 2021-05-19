$(document).ready(function () {

    // Initialize Date Range
    $('.tme-task-date-range').daterangepicker(null, function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
    });

    $('#datatable-history').DataTable();
    $('#datatable-detail-year').DataTable({
        "columns": [
                    { "orderable": false },
                    { "orderable": false },
                    { "orderable": false },
                    { "orderable": false },
                    { "orderable": false },
                    { "orderable": false },
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // converting to interger to find total
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };

            // computing column Total of the complete result 
            var monTotal = api
                .column(1)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var tueTotal = api
                    .column(2)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

            var wedTotal = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var thuTotal = api
                   .column(4)
                   .data()
                   .reduce(function (a, b) {
                       return intVal(a) + intVal(b);
                   }, 0);

            var friTotal = api
                   .column(5)
                   .data()
                   .reduce(function (a, b) {
                       return intVal(a) + intVal(b);
                   }, 0);


            // Update footer by showing the total with the reference of the column index 
            $(api.column(0).footer()).html('Total');
            $(api.column(1).footer()).html(monTotal);
            $(api.column(2).footer()).html(tueTotal);
            $(api.column(3).footer()).html(wedTotal);
            $(api.column(4).footer()).html(thuTotal);
            $(api.column(5).footer()).html(friTotal);
        },
        //scrollY: "800px",
        scrollX: true,
        scrollCollapse: true,
        paging: false,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible',
                    modifier: {
                        //selected: true
                    }
                },

            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 5]
                }
            },
        ]
    });
    $('#datatable-task').DataTable({
        "autoWidth": false,
        "bAutoWidth": false,
    });
    $('#project-detail-table').DataTable({
        scrollY: "800px",
        scrollX: true,
        scrollCollapse: true,
        paging: true,
        fixedColumns: {
            leftColumns: 4
        },
        "columnDefs": [{
            "targets": -1,
            "data": null,
            "defaultContent": "<span class='btn btn-danger btn-sm detail-delete-button'>x</span>"
        }]

    });
    //
    // Rupiah
    $("input[data-type='currency']").on({
        keyup: function () {
            formatCurrency($(this));
        },
        blur: function () {
            formatCurrency($(this), "blur");
        }
    });


    function formatNumber(n) {
        // format number 1000000 to 1,234,567
        return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
    }


    function formatCurrency(input, blur) {
        // appends $ to value, validates decimal side
        // and puts cursor back in right position.

        // get input value
        var input_val = input.val();

        // don't validate empty input
        if (input_val === "") { return; }

        // original length
        var original_len = input_val.length;

        // initial caret position 
        var caret_pos = input.prop("selectionStart");

        // check for decimal
        if (input_val.indexOf(".") >= 0) {

            // get position of first decimal
            // this prevents multiple decimals from
            // being entered
            var decimal_pos = input_val.indexOf(".");

            // split number by decimal point
            var left_side = input_val.substring(0, decimal_pos);
            var right_side = input_val.substring(decimal_pos);

            // add commas to left side of number
            left_side = formatNumber(left_side);

            // validate right side
            right_side = formatNumber(right_side);

            // On blur make sure 2 numbers after decimal
            if (blur === "blur") {
                right_side += "00";
            }

            // Limit decimal to only 2 digits
            right_side = right_side.substring(0, 2);

            // join number by .
            input_val = "$" + left_side + "." + right_side;

        } else {
            // no decimal entered
            // add commas to number
            // remove all non-digits
            input_val = formatNumber(input_val);
            //input_val = "Rp" + input_val;
            input_val = input_val;

            // final formatting
            if (blur === "blur") {
                //input_val += ".00";
            }
        }

        // send updated string to input
        input.val(input_val);

        // put caret back in the right position
        var updated_len = input_val.length;
        caret_pos = updated_len - original_len + caret_pos;
        input[0].setSelectionRange(caret_pos, caret_pos);
    }




    //



    $('[data-toggle="tooltip"]').tooltip();

    $('[data-toggle="tooltip"]').tooltip();
    var table = $('#example1').DataTable({
        scrollY: "800px",
        scrollX: true,
        scrollCollapse: true,
        paging: true,
        fixedColumns: {
            leftColumns: 4
        }
    });

    
    //Update proposal legal=============================================================
    $(document).on('click', '.btn-update-legal', function () {

        var id_activation = $(this).attr("data-activation");
        var brand = $(this).attr("data-brand");
        var drug = $(this).attr("data-drug");
        var class_id = $(this).attr("data-class");
        var date = $(this).attr("data-date");
        var target = $(this).attr("data-launch");
        var trade = $(this).attr("data-trade");
        var note = $(this).attr("data-note");
        var desc = $(this).attr("data-desc");
      

        $('#edit-id-activation').val(id_activation);
        $('#edit-brand-name').val(brand);
        $('#edit-drug-category').val(drug);
        $('#edit-class-category').val(class_id);
        $('#edit-activation-start').val(date);
        $('#edit-activation-end').val(target);
        $('#edit-trademark-name').val(trade);
        $('#edit-activation-note').val(note);
        $('#edit-activation-desc').val(desc);
    });
    


    //// Muslim ==============================================================
    //$(document).on('click', '.btn-update-phone', function () {

    //    var id = $(this).attr("data-id");
    //    var job = $(this).attr("data-job");
    //    var name = $(this).attr("data-name");
    //    var ext = $(this).attr("data-ext");
    //    var status = $(this).attr("data-status");
       

    //    $('#edit-id-phone').val(id);
    //    $('#edit-job-name').val(job);
    //    $('#edit-name').val(name);
    //    $('#edit-ext').val(ext);
    //    $('#edit-status').val(status);
      
    //});

    //=====================================================================
    //tabel
    $('#datatablelamp_backup').DataTable({

        dom: 'Bfrtip',

        buttons: [

            'excel', 'pdf', 'print'

        ]

    });
    //tabel
    $('#datatablelamp').DataTable({

        dom: 'Bfrtip',

        buttons: [

            'excel', 'pdf', 'print'

        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // converting to interger to find total
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // computing column Total of the complete result 
            var budget = api
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            var budget_terpakai = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            var budget_tersedia = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
          

            $(api.column(0).footer()).html('Total');
            $(api.column(3).footer()).html(budget);
            $(api.column(4).footer()).html(budget_terpakai);
            $(api.column(5).footer()).html(budget_tersedia);
        },

    });



    $(".bdz-datepicker-add").datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        startDate: '+1d',
        autoclose: true,
    })
    $(".ms-datepicker-end").datepicker({
        
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        startDate: '+1d',
        endDate: moment(moment().add(3, 'M')).endOf('month').toDate(),
        autoclose: true,
    })

    $('#insert-activation-end').on('change', function () {
    var from = $("#insert-activation-start").val();
        var to = $("#insert-activation-end").val();
        //alert(from);
        //alert(to);
    $('.msdate').hide(0)/*.delay(3000).hide(0)*/;
        if (from > to)
        {
            //alert("invalid");
                $('#insert-activation-end').datepicker('update', from);
                $('#insert-activation-end').val(from);
                //alert("Invalid End Date");
                $('.msdate').show(0)/*.delay(3000).hide(0)*/;
        }


        else
         {
                 //alert("Ok");
          }



    });



    $('#print_proposal').on('click', function () {

        window.print();
    });



    $('#datatable-colvis').DataTable({
        scrollY: "800px",
        scrollX: true,
        scrollCollapse: true,
        paging: true,
        fixedColumns: {
            leftColumns: 4
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible',
                    modifier: {
                        selected: true
                    }
                },

            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 5]
                }
            },
            'colvis'
        ],
        select: true

    });


  




    
});










