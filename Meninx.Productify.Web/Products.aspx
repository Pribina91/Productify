
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Meninx.Productify.Web.Products" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-striped">
                <thead>
                    <tr >
                        <th width="25"></th>
                        <th >Name</th>
                        <th >Code</th>
                        <th >Price</th>
                        <th width="100"></th>
                    </tr>                
                    <tr >
                        <td ></td>
                        <td >
                            <div class="filter">
                                <input class="form-control" id="nameFilter" onkeyup="filterChanged()"/>
                            </div>
                        </td>
                        <td >  
                            <div class="filter">
                            <input class="form-control" id="codeFilter" onkeyup="filterChanged()"/>
                        </div>
                        </td>

                        <td > <div class="filter">
                            <input class="form-control" id="priceFilter" onkeyup="filterChanged()"/>
                        </div></td>
                        <td> <button type="button" id="addButton" disabled="disabled" title="Add" onclick="createNewProduct(event)" class="btn btn-primary ">
                           Add
                        </button></td>
                    </tr>
                </thead>
                <asp:Repeater id="productList" runat="server">
                    <ItemTemplate>
                        <tr onclick="openProductDetail(event)" data-product-id="<%# Eval("Id")%>">
                            <td><span class="fa fa-plus"></span></td>
                            
                            <td><%# Eval("Name")%></td>
                            <td><%# Eval("Code")%></td>
                            <td><%# Eval("Price")%></td>
                            <td>
                                <a title="Edit" class="link" onclick="editProduct(event)">
                                    <span class="fa fa-pencil-square-o fa-lg" aria-hidden="true" style="padding-right: 5px"></span>
                                </a>
                                <a title="Delete" class="link" onclick="deleteProduct(event)">
                                    <span class="fa fa-times-circle fa-lg" aria-hidden="true" style="padding-right: 5px"></span>
                                </a>
                            </td>
                        </tr>
                        
                            
                        
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

    <script>
        function filterChanged() {
            var name = $("#nameFilter").val();
            var code = $("#codeFilter").val();
            var price = $("#priceFilter").val();
            if (name && code && price) {
                $("#addButton").attr("disabled",false);
            }

            //todo filter
        }
        function createNewProduct() {
            var name = $("#nameFilter").val();
            var code = $("#codeFilter").val();
            var price = $("#priceFilter").val();
            if (name && code && price) {
                $.ajax({
                    type: "Post",
                    url: '<%= ResolveUrl("Products.aspx/CreateProduct") %>',
                    data: "{'name': '" + name +
                        "'," +
                        "'code': '" + code
                        + "'" +
                        ",'price': '" + price
                        + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        toastr.error("Request: " +
                            XMLHttpRequest.toString() +
                            "\n\nStatus: " +
                            textStatus +
                            "\n\nError: " +
                            errorThrown);
                    },
                    success: function(result) {
                        alert("We returned: " + result);
                    }
                });
            }
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
                url: '<%= ResolveUrl("Products.aspx/GetProductDetail") %>',
                data: "{'productId': '" + productId + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (result) {
                    alert("We returned: " + result);
                }
            });
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
                    alert("We returned: " + result);
                }
            });
        }
        function deleteProduct(event) {
            if (event == null || event.target == null) {
                return;
            }

            var sender = event.target;

            var productId = $(sender).data("product-id");
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
                    window.reload;
                }
            });

            event.preventDefault();
        }
    </script>
</asp:Content>

