<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="BizErpBVN.Menu.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="jquery-ui.css" rel="stylesheet" type="text/css" />  
    <script src="jquery.min.js" type="text/javascript"></script>  
    <script src="jquery-ui.min.js" type="text/javascript"></script> 
    <style type="text/css">
        hr {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
            border-top: 1px dotted;
        }

        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">  
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#txtEmpName").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Order.aspx/GetCustomerName",
                        data: "{ 'en_name': '" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("No Match");
                        }
                    });
                }
            });
        }
    </script> 
    <script>
        $(document).ready(function () {
            $('#buttonEdit').click(function () {
                window.location.href = "HistoryOrder.aspx";
            });
        });

        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="tab2_page1" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="container">
            <div class="form-group" style="padding-left: 10%">
                <div class="col-md-12">
                    <button type="button" class="btn btn-labeled btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                    </button>
                    <button type="button" class="btn btn-labeled alert-info">
                        <span class="btn-label"><i class="glyphicon glyphicon-plus-sign"></i></span>สร้างใหม่
                    </button>
                    <button id="buttonEdit" type="button" class="btn btn-labeled btn-warning">
                        <span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>แก้ไข
                    </button>
                    <button id="ButtonCancel" type="button" class="btn btn-labeled btn-danger">
                        <span class="btn-label"><i class="glyphicon glyphicon-remove"></i></span>ยกเลิกรายการ
                    </button>
                    <button type="button" class="btn btn-labeled btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-ok"></i></span>ยืนยันรายการ
                    </button>
                </div>
            </div>
            <%-- <table  style="border: solid 15px Green; background-color: SkyBlue;" >  
        <tr>  
            <td>  
                <span style="color: Red; font-weight: bold; font-size: 18pt;">Enter Employee Name:</span>   
                <asp:TextBox ID="txtEmpName" runat="server" Width="160px" />  
            </td>  
        </tr>  
    </table> --%>
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เลขที่เอกสาร</label>
                <div class="col-md-4">
                    <input type="text" data-ng-model="tutorial.title"
                        name="title" class="form-control" />
                </div>
                <label for="title" class="col-md-2 control-label">สถานะ</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbStatus" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">วันที่เอกสาร</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <input type="date" class="form-control">
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">เลขประจำตัวผู้เสียภาษี</label>
                <div class="col-md-4">
                    <input type="text" data-ng-model="tutorial.title"
                        name="title" class="form-control" />
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ลูกค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbCustgrp" runat="server" class="form-control" OnSelectedIndexChanged="cbbCustgrp_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">เงื่อนไขการชำระเงิน</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="mt_pymt" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">การส่งสินค้า</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="en_saledelry_type" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <label for="title" class="col-md-2 control-label">การคำนวนภาษี</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbTaxcalc" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">พนักงานขาย</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="mt_emp" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">ที่อยู่</label>
                <div class="col-md-4">

                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <textarea data-ng-model="tutorial.description" rows="2" runat="server" id="txt_Addr1"
                                name="description" class="form-control">
                        </textarea>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <img src="../Images/edit.png" style="width: 25px; height: 25px" data-toggle="modal" data-target="#exampleModalCenter1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">ที่อยู่จัดส่ง</label>
                <div class="col-md-4">
                    <textarea data-ng-model="tutorial.description" rows="2" runat="server" id="txt_Addr2"
                        name="description" class="form-control">
                        </textarea>
                </div>
                <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <img src="../Images/edit.png" style="width: 25px; height: 25px" data-toggle="modal" data-target="#exampleModalCenter" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <hr />
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">รายการสินค้า</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbbItem" runat="server" class="form-control" OnSelectedIndexChanged="cbbItem_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label for="title" class="col-md-2 control-label">รายละเอียดสินค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <textarea data-ng-model="tutorial.description" rows="1" id="txtItem_dest" runat="server"
                                        name="description" class="form-control">
                        </textarea>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">ราคาต่อหน่วย</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label">ส่วนลดต่อหน่วย</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDisc1_price" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">หน่วยนับ</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtUnt_oid" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label">รวมมูลค่า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtNetprice_amt" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">อธิบายเพิ่มเติม</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <textarea data-ng-model="tutorial.description" rows="2" id="txtMemo" runat="server"
                                name="description" class="form-control">
                        </textarea>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info form-control" Style="height: 38px; width: 120px;">
                          <i class="glyphicon glyphicon-plus"></i>&nbsp;เพิ่ม</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="true" ShowHeader="true"
                ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView6_PageIndexChanged">
                <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="line_item_oid" HeaderText="รายการสินค้า" />
                    <asp:BoundField DataField="line_item_dest" HeaderText="รายละเอียดสินค้า" />
                    <asp:BoundField DataField="line_price" HeaderText="ราคาต่อหน่วย" />
                    <asp:BoundField DataField="line_disc1_price" HeaderText="ส่วนลดต่อหน่วย" />
                    <asp:BoundField DataField="line_disc2_price" HeaderText="ส่วนลดต่อหน่วย - กรณีลูกค้ารับสินค้าเอง " />
                    <asp:BoundField DataField="line_unt_oid" HeaderText="หน่วยนับ" />
                    <asp:BoundField DataField="line_netprice_amt" HeaderText="รวมมูลค่า" />
                    <asp:BoundField DataField="line_memo" HeaderText="อธิบายเพิ่มเติม" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <br />
            <hr />
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">การชำระเงินมัดจำ</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-12">
                            <input type="number" style="text-align: right" class="form-control"
                                required name="price" min="0" value="0.00" step="0.01" />

                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">อธิบายรายการ</label>
                <div class="col-md-4">
                    <textarea data-ng-model="tutorial.description" rows="1" cols="50"
                        name="description" class="form-control">
                        </textarea>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">จำนวนเงินมัดจำ</label>
                <div class="col-md-4">
                    <input type="text" data-ng-model="tutorial.title"
                        name="title" class="form-control" />
                </div>
                <label for="title" class="col-md-2 control-label">ยอดเงินที่รับชำระแล้ว</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
            </div>

            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ส่วนลดท้ายบิล</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
                <label for="title" class="col-md-2 control-label">ส่วนลด</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">อัตราภาษี</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
                <label for="title" class="col-md-2 control-label">มูลค่าภาษี</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                </div>
                <label for="title" class="col-md-2 control-label">รวมทั้งสิ้น</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>


    <div class="modal fade" id="exampleModalCenter1" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle1">ที่อยู่</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GvOrder" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped text-center" Width="110%" ShowHeader="true"
                        ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4">
                        <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButton1" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="addr_text" HeaderText="ที่อยู่" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
                    </asp:GridView>
                </div>
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-primary">ตกลง</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">ที่อยู่จัดส่ง</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GvOrder1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped text-center" Width="110%" ShowHeader="true"
                        ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4">
                        <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButton2" runat="server" Checked="true" OnClick="javascript:SelectSingleRadiobutton(this.id)"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="addr_text" HeaderText="ที่อยู่" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
                    </asp:GridView>
                </div>
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-primary">ตกลง</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
