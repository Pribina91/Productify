
<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Meninx.Productify.Web.Products" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-striped" id="productTable">
                <thead>
                    <tr >
                        <th >Name</th>
                        <th >Code</th>
                        <th >Price</th> <th >Attributes</th>
                        <th width="230">
                            <div class="btn-group">
                                <asp:Button runat="server" CssClass="btn btn-default " OnClick="OnJsonClick" Text="Json"/>
                                <asp:Button runat="server" CssClass="btn btn-default " OnClick="OnXmlClick" Text="Xml"/>
                            </div>
                            
                            <button type="button" title="Reload" onclick="loadList()" class="btn btn-primary pull-right">
                                <i class="fa fa-refresh"></i>
                            </button>
                        </th>
                    </tr>                
                    <tr >
                        <td >
                            <div class="filter">
                                <input class="form-control" id="nameFilter" onkeyup="filterChanged(event)"/>
                            </div>
                        </td>
                        <td >  
                            <div class="filter">
                            <input class="form-control" id="codeFilter" onkeyup="filterChanged(event)" />
                        </div>
                        </td>
                        
                        <td > <div class="filter">
                            <input class="form-control" id="priceFilter" onkeyup="filterChanged(event)" type="text" pattern="\d*" title="Price value" />
                        </div></td><td ></td>
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
  <div style="display:none">
      <ul>
          <asp:Repeater runat="server">
              <ItemTemplate>
                  <li><input type="checkbox" value=""/></li>
              </ItemTemplate>
          </asp:Repeater>
      </ul>
      
  </div>
<%--    <!-- Modal -->
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
                          <form>
                        <div class="row">
                            <label class="col-sm-4 control-label">Name</label>
                            <div class="col-sm-8">
                                <input name="name" type="text" />
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>--%>
    <script>
        $(document).ready(function () {
            loadList();
            $(".edit-mode").hide();
        });
    </script>
</asp:Content>

