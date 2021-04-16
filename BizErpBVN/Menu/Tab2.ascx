<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tap2.ascx.cs" Inherits="BizErpBVN.Menu.WebUserControl1" %>
<%@ Register Src="~/Menu/Tab2_SelectCustomer.ascx" TagPrefix="uc1" TagName="Tab2_SelectCustomer" %>




<!DOCTYPE html>
<head>
    <title></title>
</head>

    <script>
        $(document).ready(function () {
            $('#tab2_page2').hide();
            $('#tab2_page1').show();

                    $('#buttonEdit').click(function () {
                        $('#tab2_page2').show();
                        $('#tab2_page1').hide();
                    });

                    $('#buttonBack').click(function () {
                        $('#tab2_page2').hide();
                        $('#tab2_page1').show();
                    });

        });



    </script>

<form id="form2">
    
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
            <hr />
  
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
                            <asp:DropDownList ID="mt_custgrp" runat="server" class="form-control"></asp:DropDownList>
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
                <label for="description" class="col-md-2 control-label">ที่อยู่</label>
                <div class="col-md-4">
                    <textarea data-ng-model="tutorial.description" rows="2"
                        name="description" class="form-control">
                        </textarea>
                </div>
                <label for="title" class="col-md-2 control-label">การส่งสินค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="en_saledelry_type" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
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
                <label for="description" class="col-md-2 control-label">ที่อยู่จัดส่ง</label>
                <div class="col-md-4">
                    <textarea data-ng-model="tutorial.description" rows="2"
                        name="description" class="form-control">
                        </textarea>
                </div>
                <label for="title" class="col-md-2 control-label">พนักงานขาย</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="mt_emp" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="true" ShowHeader="true"
                ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView6_PageIndexChanged">
                <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="txn_date" HeaderText="รายการสินค้า" />
                    <asp:BoundField DataField="" HeaderText="รายละเอียดสินค้า" />
                    <asp:BoundField DataField="txn_num" HeaderText="ราคาต่อหน่วย" />
                    <asp:BoundField DataField="cust_oid" HeaderText="ส่วนลดต่อหน่วย" />
                    <asp:BoundField DataField="txn_num" HeaderText="ส่วนลดต่อหน่วย - กรณีลูกค้ารับสินค้าเอง " />
                    <asp:BoundField DataField="sodepos_type" HeaderText="หน่วยนับ" />
                    <asp:BoundField DataField="sodepos_amt" HeaderText="รวมมูลค่า" />
                    <asp:BoundField DataField="depos_amt" HeaderText="อธิบายเพิ่มเติม" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
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
                    <textarea data-ng-model="tutorial.description" rows="2" cols="50"
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
                <label />
                <label />
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ยอดเงินที่รับชำระแล้ว</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control"
                        required name="price" min="0" value="0.00" step="0.01" />
                </div>
                <label />
                <label />
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
    <div id="tab2_page2" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="form-group" style="padding-left: 10%">
                <div class="col-md-12">
                    <button id="buttonBack"  type="button" class="btn btn-labeled btn-primary">
                        <span class="btn-label"><i class="glyphicon glyphicon-circle-arrow-left"></i></span>กลับหน้าหลัก
                    </button>
                    <button type="button" class="btn btn-labeled btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                    </button>
                </div>
            </div>
        <uc1:Tab2_SelectCustomer runat="server" id="Tab2_SelectCustomer" />
    </div>
</form>
