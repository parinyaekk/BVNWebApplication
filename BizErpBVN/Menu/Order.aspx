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
    <script>
        $(document).ready(function () {
            $('#buttonEdit').click(function () {
                window.location.href = "HistoryOrder.aspx";
            });
            SearchText();
        });

        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        };

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="tab2_page1" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="container">
            <div class="form-group" style="padding-left: 10%">
                <div class="col-md-12">
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="SaveData" class="btn btn-labeled btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="gotoOrder" class="btn btn-labeled alert-info">
                        <span class="btn-label"><i class="glyphicon glyphicon-plus-sign"></i></span>สร้างใหม่
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="gotoHistory" class="btn btn-labeled btn-warning">
                        <span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>แก้ไข
                    </asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เลขที่เอกสาร</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txn_num" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
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
                            <input type="date" class="form-control" id="txtDate" runat="server">
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">เลขประจำตัวผู้เสียภาษี</label>
                <div class="col-md-4">
                    <asp:TextBox ID="tax_num" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
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
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <textarea data-ng-model="tutorial.description" rows="2" runat="server" id="txt_Addr2"
                                name="description" class="form-control">
                        </textarea>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                    <asp:DropDownList ID="cbbItem" runat="server" class="form-control" OnSelectedIndexChanged="cbbItem_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <label for="title" class="col-md-2 control-label">รายละเอียดสินค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <textarea data-ng-model="tutorial.description" rows="1" id="txtItem_dest" runat="server" style="text-align: left"
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
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control text-right" TextMode="Number" ReadOnly="true"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label">ส่วนลดต่อหน่วย</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDisc1_price" runat="server" CssClass="form-control text-right" TextMode="Number" AutoPostBack="true" OnTextChanged="line_qty_Change"></asp:TextBox>
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
                            <asp:TextBox ID="txtline_qty" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="line_qty_Change" CssClass="form-control text-right"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label">รวมมูลค่า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtNetprice_amt" runat="server" CssClass="form-control text-right" TextMode="Number" ReadOnly="true"></asp:TextBox>
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
                            <asp:LinkButton ID="AddItem" runat="server" OnClick="ButtonAdd_Click" CssClass="btn btn-info form-control" Style="height: 37px; width: 100px;">
                          <i class="glyphicon glyphicon-plus"></i>&nbsp;เพิ่ม</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="false" ShowHeader="true"
                ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView6_PageIndexChanged">
                <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="mt_name" HeaderText="รายการสินค้า" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="line_item_dest" HeaderText="รายละเอียดสินค้า" />
                    <asp:BoundField DataField="line_price" HeaderText="ราคาต่อหน่วย" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="line_disc1_price" HeaderText="ส่วนลดต่อหน่วย" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="line_disc2_price" HeaderText="ส่วนลดต่อหน่วย - กรณีลูกค้ารับสินค้าเอง " DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="line_qty" HeaderText="หน่วยนับ" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="line_netprice_amt" HeaderText="รวมมูลค่า" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="line_memo" HeaderText="อธิบายเพิ่มเติม" />
                    <asp:TemplateField ItemStyle-Width="24%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" class="btn btn-labeled btn-warning"  Width="90px" Height="38px"><span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>
                        แก้ไข
                    </asp:LinkButton>
                            <asp:LinkButton ID="lnkSelect" runat="server" class="btn btn-labeled btn-danger" Width="90px" Height="38px"><span class="btn-label"><i class="glyphicon glyphicon-trash"></i></span>
                        ลบ
                    </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                            <asp:DropDownList ID="en_sodepos_type" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">อธิบายรายการ</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txn_memo" runat="server" TextMode="multiline" Columns="50" Rows="2" class="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">จำนวนเงินมัดจำ</label>
                <div class="col-md-4">
                    <asp:TextBox ID="sodepos_amt" Style="text-align: right" runat="server" class="form-control" type="number"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ยอดเงินที่รับชำระแล้ว</label>
                <div class="col-md-4">
                    <asp:TextBox ID="depos_amt" Style="text-align: right" runat="server" class="form-control" type="number"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ส่วนลดท้ายบิล</label>
                <div class="col-md-4">

                    <asp:TextBox ID="disc2_amt" runat="server" Style="text-align: right" class="form-control" AutoPostBack="true" OnTextChanged="dist_price_Change"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ส่วนลด</label>
                <div class="col-md-4">
                    <asp:TextBox ID="disc1_amt" Style="text-align: right" runat="server" class="form-control" ReadOnly="true" type="number"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">อัตราภาษี</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control" id="tax_rate"
                        name="price" min="0" value="7" step="0.01" readonly />
                </div>
                <label for="title" class="col-md-2 control-label">มูลค่าภาษี</label>
                <div class="col-md-4">
                    <asp:TextBox ID="tax_amt" Style="text-align: right" runat="server" class="form-control" ReadOnly="true" type="number"></asp:TextBox>
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
                    <asp:TextBox ID="txn_total" Style="text-align: right" runat="server" class="form-control" ReadOnly="true" type="number"></asp:TextBox>
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
                                    <asp:RadioButton ID="RadioButton1" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" ToolTip='<%# Eval("oid")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="addr_text" HeaderText="ที่อยู่" />
                            <asp:TemplateField ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" class="btn btn-labeled btn-warning" Width="100px" Height="38px"><span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>
                        แก้ไข
                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkSelect" runat="server" class="btn btn-labeled btn-danger" Width="100px" Height="38px"><span class="btn-label"><i class="glyphicon glyphicon-trash"></i></span>
                        ลบ
                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
                    </asp:GridView>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="Button1" runat="server" Text="ตกลง" CssClass="btn btn-primary" OnClick="Button1_Click" Width="100px" Height="38px" />
                    &nbsp;
                    <button type="button" class="btn btn-danger" data-dismiss="modal" style="height: 38px; width: 100px">ปิด</button>
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
                                    <asp:RadioButton ID="RadioButton2" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" ToolTip='<%# Eval("oid") %>' />
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
                    <asp:Button ID="Button2" runat="server" Text="ตกลง" CssClass="btn btn-primary" OnClick="Button2_Click" Width="100px" Height="38px" />
                    &nbsp;
                    <button type="button" class="btn btn-danger" data-dismiss="modal" style="height: 38px; width: 100px">ปิด</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
