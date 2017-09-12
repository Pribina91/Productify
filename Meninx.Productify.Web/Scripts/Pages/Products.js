

function loadList() {
    $.ajax({
        type: "Post",
        url: 'Products.aspx/GetProducts',
        data: "{'productName':'', 'code':''}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error("Request: " +
                XMLHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (data) {
            if (data) {
                var products = JSON.parse(data.d);
                var result = [];
                for (var i = 0; i < products.length; i++) {
                    var object = products[i];
                    result.push("<tr data-product-id='" + object.Id + "' data-toggle='modal' data-target='#detailModal'>");

                    result.push("<td class=\"tb-name\">" + object.Name + "</td>");
                    result.push("<td class=\"tb-code\">" + object.Code + "</td>");
                    result.push("<td class=\"tb-price\">" + object.Price + "</td>");

                    result.push("<td>"
                        +"<button title='Detail' type='button' class='btn btn-default ' onclick='openProductDetail(event)' >Detail</button>"
                        +"<button title='Delete' type='button' class='btn btn-danger' onclick='deleteProduct(event)' >Delete</button>" +
                        "</td></tr>");
                }
                $("#productTable tbody").html(result.join(""));
            }
        }
    });
}
function filterChanged() {
    var name = $("#nameFilter").val().toUpperCase();
    var code = $("#codeFilter").val();
    var price = $("#priceFilter").val();
    if (name && code && price) {
        $("#addButton").attr("disabled", false);
    }
    if (name || code || price) {
        $("#productTable tbody tr").show();
        filterItems(".tb-name", name);
        filterItems(".tb-code", code);
        filterItems(".tb-price", price);
    }
}

function filterItems(itemSelector, filterValue) {
    if (!filterValue) {
        return;
    }
    $(itemSelector).each(function (index, element) {
        var jElement = $(element)

        if (jElement.text().toUpperCase().indexOf(filterValue) <= -1) {
            jElement.closest("tr").hide();
        }
    });
}
function createNewProduct() {
    var name = $("#nameFilter").val();
    var code = $("#codeFilter").val();
    var price = $("#priceFilter").val();
    if (name && code && price) {
        $.ajax({
            type: "Post",
            url: "Products.aspx/CreateProduct",
            data: "{'name': '" + name +
                "'," +
                "'code': '" + code
                + "'" +
                ",'price': '" + price
                + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Request: " +
                    XMLHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                toastr.success("Product created")
                loadList();
                clearFilter();
            }
        });
    }
}
function clearFilter() {
    $("#nameFilter").val("");
    $("#codeFilter").val("");
    $("#priceFilter").val("");
}
function openProductDetail(event) {
    debugger;
    if (event == null || event.target == null) {
        return;
    }
    var sender = event.target;

    var productId = $(sender).closest("tr").data("product-id");
    $.ajax({
        type: "Post",
        url: 'Products.aspx/GetProductDetail',
        data: "{'productId': '" + productId + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            //alert("We returned: " + result);
        }
    });

    event.stopPropagation()
}
function editProduct(event) {
    if (event == null || event.target == null) {
        return;
    }

    var sender = event.target;

    var productId = $(sender).closest("tr").data("product-id");
    $.ajax({
        type: "POST",
        url: "Products.aspx/UpdateProduct",
        data: "{'productId': '" + productId + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            //alert("We returned: " + result);
        }
    });

    event.stopPropagation()
}
function deleteProduct(event) {
    if (event == null || event.target == null) {
        return;
    }

    var sender = event.target;

    var productId = $(sender).closest("tr").data("product-id");
    $.ajax({
        type: "POST",
        url: "Products.aspx/DeleteProduct",
        data: "{'productId': '" + productId + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            toastr.warn("Product deleted")
            loadList();
        }
    });

    event.stopPropagation();
}