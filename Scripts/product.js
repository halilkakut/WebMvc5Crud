$(document).ready(function () {
    loadData();
    $("#SearchBtn").click(function () {
        var SearchValue = $("#Search").val();
        var SetData = $("#DataSearching");
        SetData.html("");
        $.ajax({
            type: "post",
            url: "/Home/GetSearchingData?SearchValue=" + SearchValue,
            contentType: "html",
            success: function (result) {
                if (result.length == 0) {
                    SetData.append('<tr style="color:red"><td colspan="3">No Match Data</td></tr>')
                } else {
                    $.each(result, function (index, value) {
                        var Data = "<tr>" +
                            "<td>" + value.ProductId + "</td>" +
                            "<td>" + value.ProductName + "</td>" +
                            "<td>" + value.ProductDescription + "</td>" +
                            '<td><a href="#" onclick="return getByID(' + value.ProductId + ')">Edit</a> | <a href="#" onclick="Delete(' + value.ProductId + ')">Delete</a></td>'+
                            "</tr>";
                        SetData.append(Data);
                    });
                }
            }
        });
    });
})





function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var html = '';
                $.each(result, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.ProductId + '</td>';
                    html += '<td>' + item.ProductName + '</td>';
                    html += '<td>' + item.ProductDescription + '</td>';
                    html += '<td><a href="#" onclick="return getByID(' + item.ProductId + ')">Edit</a> | <a href="#" onclick="Delete(' + item.ProductId + ')">Delete</a></td>';
                    html += '<tr>';
                })
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var productObj = {
        ProductId: $('#ProductId').val(),
        ProductName: $('#ProductName').val(),
        ProductDescription: $('#ProductDescription').val()
    };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getByID(ProductId) {
    $('#ProductName').css('border-color', 'lightgrey');
    $('#ProductDescription').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Home/GetById/" + ProductId,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ProductId').val(result.ProductId);
            $('#ProductName').val(result.ProductName);
            $('#ProductDescription').val(result.ProductDescription);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating product's record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var productObj = {
        ProductId: $('#ProductId').val(),
        ProductName: $('#ProductName').val(),
        ProductDescription: $('#ProductDescription').val(),
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ProductName').val("");
            $('#ProductDescription').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}  

//function for deleting product's record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ProductID').val("");
    $('#ProductName').val("");
    $('#ProductDescription').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#ProductName').css('border-color', 'lightgrey');
    $('#ProductDescription').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#ProductName').val().trim() == "") {
        $('#ProductName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProductName').css('border-color', 'lightgrey');
    }
    if ($('#ProductDescription').val().trim() == "") {
        $('#ProductDescription').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProductDescription').css('border-color', 'lightgrey');
    }
   
    return isValid;
}

