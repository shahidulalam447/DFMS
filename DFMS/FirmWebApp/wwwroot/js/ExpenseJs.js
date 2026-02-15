
$(document).ready(function () {
    var rowCount = parseInt($("#total").val());

});
function adding() {
    let z = 0;
    if (z == 0) {
        $('#tableDiv').css("display", "table");
        z++;
    }

    var rowCount = parseInt($("#total").val());
    var expenseitem = $('#ExpenseItem').val().trim();
    var amount = $('#amount').val().trim();
    var expenseid = $('#expenseId').val();

    if (expenseitem != "" && amount != "") {


/*        $('table tbody').html("")*/;
        $('.odd').hide()
        $('#tablebody').append(`
                            <tr id="inputRow${rowCount}" >
                                        

                              <td>
                                 ${expenseitem}
                              </td>
                              <td>
                               ${amount}
                              </td>  
                              <td>
                              <a id="Edit${rowCount}" onclick="Edit(${rowCount})" class="btn btn-info btn-sm"  value='${rowCount}'>
                                                <i class="fas fa-pencil-alt">
                                                </i>
                                                Edit
                                            </a>
                                <button  class="btn btn-danger btn-sm "  onclick="DeleteParts(${rowCount})" >Remove
                              </td>
                             </tr>`);

        $('#ExpenseItem').val('');
        $('#').val('');
        rowCount++;
        $("#total").val(rowCount);

    }
};


function Edit(row) {

    var editId = '#Edit'.concat(row)
    var row = $(editId).closest('tr');
    var expensitem = row.find('td:eq(0)').text().trim();
    var amount = row.find('td:eq(1)').text().trim();
    var Item = $('#ExpenseItem').val(expenseitem);
    console.log(amount);
    var amount = $('#Amount').val(amount);
    console.log(amount);
    alert('okk');

    // Get the data from the row cells
    var name = dataTable.row(row).data()[0]; // Assuming "Name" is in the first column
    console.log(name);
    var email = dataTable.row(row).data()[1];
    console.log(email);
}

function DeleteParts(rowNumber) {
    var rowCount = parseInt($("#total").val());


    var row = '#inputRow'.concat(rowNumber);
    var hiddenExpenseItem = '#ExpenseItem$'.concat(rowNumber);
    var hiddenAmount = '#Amount$'.concat(rowNumber);
    console.log(row);
    console.log(row);
    $(row).remove();
    
    rowCount--;

    $("#total").val(rowCount);
    if (rowCount == 0) {
        $('#tableDiv').css("display", "none");
    }
};
function submit() {
    alert('okkk')

    var rowDataArray = [];

    // Iterate through each table row in the tbody
    $('#table tbody tr').each(function () {
        // Get the data from the current row cells
        var expenseitem = $(this).find('td:eq(0)').text().trim();
        var amount = $(this).find('td:eq(1)').text().trim();

        var rowData = {
            Name: partsName,
            Price: amount
        };
        console.log(rowData);
        // Push the rowData object to the array
        rowDataArray.push(rowData);
    });
    console.log(rowDataArray);

    var expenseitem = $('#ExpenseItem').val();
    var amount = $('#Amount').val();
    var Description = $('textarea[name="Description"]').val();
    var status = $('select[name="Status"]').val();
    //var serviceBill = $('input[name="WorkshopBill"]').val();
    //var serviceDate = $('input[name="ServiceDate"]').val();
    //var TripId = $('input[name="TripId"]').val();
    //var vehicleId = $('input[name="VehicleId"]').val();


    $.ajax({
        url: '/ExpenseApproval/Create', // Replace with your controller and action method
        type: 'POST',
        data: {
            ExpenseItem: expenseitem,
            Amount: amount,
            Description: Description,
            status: status,
            //serviceBill: serviceBill,
            //serviceDate: serviceDate,
            //TripId: TripId, 
            //vehicleId: vehicleId,
            RowDataArray: rowDataArray
        },
        success: function (response) {
            // Handle the server response here
            console.log("okk");
        },
        error: function (error) {
            // Handle errors here
            console.error(error);
        }
    });
}
