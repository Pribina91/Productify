
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Meninx.Productify.Web.Products" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-striped">
                <tr >
                    <th width="25"></th>
                    <th >Name</th>
                    <th >Code</th>
                    <th >Price</th>
                    <th width="100"></th>
                </tr>
                <asp:Repeater id="productList" runat="server">
                    <ItemTemplate>
                        <tr onclick="openProductDetail(event)" data-product-id="<%# Eval("Id")%>">
                            <td><span class="fa fa-plus"></span></td>
                            
                            <td><%# Eval("Name")%></td>
                            <td><%# Eval("Code")%></td>
                            <td><%# Eval("Price")%></td>
                            <td>
                                <a title="Edit" class="link">
                                    <span class="fa fa-pencil-square-o fa-lg" aria-hidden="true" style="padding-right: 5px"></span>
                                </a>
                                <a title="Delete" class="link">
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
        function openProductDetail(event) {
            if (event == null || event.sender == null) {
                return;
            }

            var sender = event.sender;

            var productId = $(sender).data("product-id");
            $.ajax({
                type: "POST",
                url: "Products.aspx/GetProductDetail",
                data: productId,
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
            if (event == null || event.sender == null) {
                return;
            }

            var sender = event.sender;

            var productId = $(sender).data("product-id");
            $.ajax({
                type: "POST",
                url: "Products.aspx/GetProductDetail",
                data: productId,
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
            if (event == null || event.sender == null) {
                return;
            }

            var sender = event.sender;

            var productId = $(sender).data("product-id");
            $.ajax({
                type: "POST",
                url: "Products.aspx/GetProductDetail",
                data: productId,
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
    </script>
</asp:Content>

