﻿

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
                    result.push("<tr data-product-id='" + object.Id + "' >");

                    result.push("<td class=\"tb-name\">" +
                        "<div class='read-mode'>" + object.Name + "</div>" +
                        "<input name='Name' id='inName' value='" + object.Name + "' class='edit-mode form-control' /></td>");
                    result.push("<td class=\"tb-code\">" +
                        "<div class='read-mode'>" + object.Code + "</div>" +
                        "<input name='Code' id='inCode' value='" + object.Code + "' class='edit-mode form-control' /></td>");
                    result.push("<td class=\"tb-price\">" +
                        "<div class='read-mode'>" + object.Price + "</div>" +
                        "<input name='Price' id='inPrice' value='" + object.Price + "'type='text' pattern='\\d*' class='edit-mode form-control' style='display:none'/>" +
                        "</td>");
                    result.push("<td><div class='attribute-list'>");
                    var attributes = object.Attributes;
                    for (var j = 0; j < attributes.length; j++) {
                        result.push(getAttributeHtml(attributes[j].AttributeTypeId, attributes[j].AttributeTypeName, attributes[j].Value, attributes[j].Id));
                    }
                    
                    result.push("</div><div class='dropdownHolder' ><div>");
                    result.push("</td>");
                    result.push("<td>"
                        + "<button title='Delete' type='button' class='btn btn-danger pull-right' onclick='deleteProduct(event)' >Delete</button>" 
                        + "<button title='Edit' id='btnEdit' type='button' class='btn btn-default pull-right read-mode' onclick='openProductDetail(event)' >Edit</button>" +
                        "<div class='btn-group'>"
                        + "<button title='Save' id='btnSave'  type='button' class='btn btn-primary edit-mode' onclick='updateProduct(event)' >Save</button>"
                        + "<button title='Cancel' id='btnCancel' type='button' class='btn btn-default edit-mode' onclick='cancelProductDetail(event)' >Cancel</button>" +
                        "</div>" +
                        "</td></tr>");

                }
                $("#productTable tbody").html(result.join(""));
                $(".edit-mode").hide();
            }
        }
    });
}

function getAttributeHtml(typeId,typeName,value,id) {
    return "<div class=\"tb-attribute-item\">" + typeName + ":" +
        "<span class='read-mode'>" + value + "</span>" +
        "<input style='width: 50%;' class='form-control attribute-edit edit-mode' data-type-id='" + typeId + "' data-id='" + id + "' data-attribute-name='" + typeName + "' value='" + value + "'/>" +
        "</div>"
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

        $.ajax({
            type: "Post",
            url: "Products.aspx/PostFilter",
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
            }});
    }
}

function filterItems(itemSelector, filterValue) {
    if (!filterValue) {
        return;
    }
    $(itemSelector).each(function (index, element) {
        var jElement = $(element)

        if (jElement.text().toUpperCase().indexOf(filterValue.toUpperCase()) <= -1) {
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

function cancelProductDetail(event) {
    if (event == null || event.target == null) {
        return;
    }
    var sender = event.target;
    $(".edit-mode").hide();

    var lineOfInterest = $(sender).closest("tr");
    lineOfInterest.find(".read-mode").show();
}
function addAttribute(event) {
    if (event == null || event.target == null) {
        return;
    }
    var sender = event.target;
    var attributeTypeId = $(sender).data("attribute-type-id");
    var attributeName = $(sender).data("attribute-name");
    $(sender).closest("tr").find(".attribute-list").append(getAttributeHtml(attributeTypeId, attributeName, ""));
    $(sender).parent().hide();
}

function openProductDetail(event) {
    if (event == null || event.target == null) {
        return;
    }
    var sender = event.target;
    $(".edit-mode").hide();
    var lineOfInterest = $(sender).closest("tr");
    lineOfInterest.find(".dropdownHolder").html($("#ddAttributeTypeList").html());

    //hide attributes already added

    lineOfInterest.find(".dropdownHolder li").show();
    lineOfInterest.find(".attribute-edit").each(function (index, element) {
        var existingType = $(element).data("type-id");
        lineOfInterest.find(".dropdownHolder a[data-attribute-type-id='" + existingType + "']").parent().hide();
    });
    lineOfInterest.find(".read-mode").hide();
    lineOfInterest.find(".edit-mode").show();

    event.stopPropagation()
}
function updateProduct(event) {
    if (event == null || event.target == null) {
        return;
    }

    var sender = event.target;
    var lineOfinterest = $(sender).closest("tr");
    var attributesArray = lineOfinterest.find(".attribute-edit")
        .map(function (index, element) {
            var id = $(element).data("id");
            if (id == 'undefined') {
                id = null;
            }
            debugger;
            return { "AttributeTypeId": $(element).data("type-id"), "Value": $(element).val(), "Id": id }
    }).toArray();
    debugger;
    $.ajax({
        type: "POST",
        url: "Products.aspx/UpdateProduct",
        data: "{'product':" + JSON.stringify({ Id: lineOfinterest.data("product-id"), Name: lineOfinterest.find("#inName").val(), Code: lineOfinterest.find("#inCode").val(), Price: lineOfinterest.find("#inPrice").val(), Attributes: attributesArray }) + "}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            toastr.success("Updated");
            loadList();
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