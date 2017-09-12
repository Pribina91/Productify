
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Meninx.Productify.Web.Products" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-striped" id="productTable">
                <thead>
                    <tr >
                        <th >Name</th>
                        <th >Code</th>
                        <th >Price</th>
                        <th width="150">
                            <button type="button" title="Reload" onclick="loadList()" class="btn btn-primary pull-right">
                            <i class="fa fa-refresh"></i>
                        </button>
                        </th>
                    </tr>                
                    <tr >
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
                        <td> <button type="button" id="addButton" disabled="disabled" title="Add" onclick="createNewProduct(event)" class="btn btn-primary pull-right">
                           Add
                        </button></td>
                    </tr>
                </thead>
                <tbody>
               
                </tbody>
            </table>
        </div>
    </div>
  
    <!-- Modal -->
    <div class="modal fade" id="detailModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <%--   <a title="Edit" class="link" onclick="editProduct(event)">
                                        <span class="fa fa-pencil-square-o fa-lg" aria-hidden="true" style="padding-right: 5px"></span>
                                    </a>--%>
                    <form></form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            loadList();
        });
    </script>
</asp:Content>

