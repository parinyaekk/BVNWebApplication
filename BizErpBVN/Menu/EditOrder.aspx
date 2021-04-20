<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="BizErpBVN.Menu.EditOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ConfirmCancel() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="tab2_page1" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="container">
            <div class="form-group" style="">
                <div class="col-md-12">
                    <asp:LinkButton ID="ButtonAdd1" runat="server" OnClick="gotoOrder" class="btn btn-labeled alert-info">
                        <span class="btn-label"><i class="glyphicon glyphicon-plus-sign"></i></span>สร้างใหม่
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonEdit1" runat="server" OnClick="gotoHistory" class="btn btn-labeled btn-warning">
                        <span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>แก้ไข
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonCancel" runat="server" OnClick="Cancel" OnClientClick="ConfirmCancel()" class="btn btn-labeled btn-danger">
                        <span class="btn-label"><i class="glyphicon glyphicon-remove"></i></span>ยกเลิกรายการ
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonSubmit" runat="server" OnClick="Submit" class="btn btn-labeled btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-ok"></i></span>ยืนยันรายการ
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonConfirm" runat="server" OnClick="Confirm" class="btn btn-labeled btn-primary">
                        <span class="btn-label"><i class="glyphicon glyphicon-usd"></i></span>รับชำระ
                    </asp:LinkButton>
                </div>
            </div>
            <hr />

            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เลขที่เอกสาร</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txn_num" runat="server" class="form-control" Readonly="true"></asp:TextBox>
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
                            <asp:TextBox ID="txn_date" runat="server" class="form-control" Readonly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">เลขประจำตัวผู้เสียภาษี</label>
                <div class="col-md-4">
                    <asp:TextBox ID="tax_num" runat="server" class="form-control" Readonly="true"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ลูกค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbCustgrp" runat="server" class="form-control"></asp:DropDownList>
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
                <label for="title" class="col-md-2 control-label">การส่งสินค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="en_saledelry_type" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="mt_emp" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
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
                    <asp:TextBox ID="addr_text" runat="server" TextMode="multiline" Columns="50" Rows="2" class="form-control" Readonly="true"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <img src="../Images/edit.png" style="width: 25px; height: 20px" />
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
                    <asp:TextBox ID="ship_addr_text" runat="server" TextMode="multiline" Columns="50" Rows="2" class="form-control" Readonly="true"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <img src="../Images/edit.png" style="width: 25px; height: 20px" />
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
                    <asp:TextBox ID="txtPrice" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ส่วนลดต่อหน่วย</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtDisc1_price" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">หน่วยนับ</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtUnt_oid" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">รวมมูลค่า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtNetprice_amt" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="description" class="col-md-2 control-label">อธิบายเพิ่มเติม</label>
                <div class="col-md-4">
                    <textarea data-ng-model="tutorial.description" rows="2" id="txtMemo" runat="server"
                        name="description" class="form-control">
                        </textarea>
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
                    <asp:TextBox ID="txn_memo" runat="server" TextMode="multiline" Columns="50" Rows="2" class="form-control" Readonly="true"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">จำนวนเงินมัดจำ</label>
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
                <label for="title" class="col-md-2 control-label">ยอดเงินที่รับชำระแล้ว</label>
                <div class="col-md-4">
                    <asp:TextBox ID="depos_amt" runat="server" class="form-control" Readonly="true"></asp:TextBox>
                </div>
                <label />
                <label />
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ส่วนลดท้ายบิล</label>
                <div class="col-md-4">
                    <asp:TextBox ID="disc2_amt" runat="server" class="form-control" Readonly="true"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ส่วนลด</label>
                <div class="col-md-4">
                    <input type="number" style="text-align: right" class="form-control" id="disc1_amt"
                        required name="price" min="0" value="0.00" step="0.01" readonly />
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
                    <input type="number" style="text-align: right" class="form-control" id="tax_amt"
                        required name="price" min="0" value="0.00" step="0.01" readonly />
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
                    <input type="number" style="text-align: right" class="form-control" id="txn_total"
                        required name="price" min="0" value="0.00" step="0.01" readonly />
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>
    <div id="tab2_page2" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="form-group" style="padding-left: 10%">
            <div class="col-md-12">
                <button id="buttonBack" type="button" class="btn btn-labeled btn-primary">
                    <span class="btn-label"><i class="glyphicon glyphicon-circle-arrow-left"></i></span>กลับหน้าหลัก
                </button>
                <button type="button" class="btn btn-labeled btn-success">
                    <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                </button>
            </div>
        </div>
    </div>
</asp:Content>
