﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="BizErpBVN.Menu.EditOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hidden {
            display: none;
        }
    </style>
    <script>
        function ConfirmCancel() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการยกเลิกรายการใช่หรือไม่ ?")) {
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
            <div class="form-group">
                <div class="col-md-12">
                    <asp:LinkButton ID="ButtonAdd1" runat="server" OnClick="gotoOrder" class="btn btn-labeled alert-info">
                        <span class="btn-label"><i class="glyphicon glyphicon-plus-sign"></i></span>สร้างใหม่
                    </asp:LinkButton>
                    <div style="display: none">
                        <asp:LinkButton ID="ButtonEdit1" runat="server" OnClick="gotoHistory" class="btn btn-labeled btn-warning">
                        <span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>แก้ไข
                        </asp:LinkButton>
                    </div>
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="gotoHistory" class="btn btn-labeled btn-primary">
                    <span class="btn-label"><i class="glyphicon glyphicon-circle-arrow-left"></i></span>กลับหน้าหลัก
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="SaveData" class="btn btn-labeled btn-success">
                    <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonCancel" runat="server" OnClick="Cancel" OnClientClick="ConfirmCancel()" class="btn btn-labeled btn-danger">
                        <span class="btn-label"><i class="glyphicon glyphicon-remove"></i></span>ยกเลิกรายการ
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonSubmit" runat="server" OnClick="Submit" class="btn btn-labeled btn-info">
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
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">วันที่เอกสาร</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txn_date" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
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
                            <asp:DropDownList ID="en_saledelry_type" runat="server" class="form-control" OnSelectedIndexChanged="en_saledelry_type_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                            <asp:DropDownList ID="mt_emp" runat="server" class="form-control" readonly="true"></asp:DropDownList>
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
                    <asp:TextBox ID="addr_text" runat="server" TextMode="multiline" Columns="50" Rows="2" class="form-control"></asp:TextBox>
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
                    <asp:TextBox ID="ship_addr_text" runat="server" TextMode="multiline" Columns="50" Rows="2" class="form-control"></asp:TextBox>
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
                                    <textarea data-ng-model="tutorial.description" rows="1" id="txtItem_dest" runat="server" style="text-align: left" readonly="readonly"
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
                                    <asp:TextBox ID="txtDisc1_price" runat="server" CssClass="form-control text-right" TextMode="Number" AutoPostBack="true" OnTextChanged="line_qty_Change" ReadOnly="true"></asp:TextBox>
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
                <label for="description" class="col-md-2 control-label">จำนวน</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="line_qty_Change" CssClass="form-control text-right"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                <label for="description" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                </div>
                  <label for="description" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:LinkButton ID="AddItem" runat="server" OnClick="ButtonAdd_Click" CssClass="btn btn-info form-control" Style="height: 37px; width: 100px;">
                          <i class="glyphicon glyphicon-plus"></i>&nbsp;เพิ่ม</asp:LinkButton>
                            <asp:LinkButton ID="UpdateItem" runat="server" OnClick="ButtonUpdate_Click" CssClass="btn btn-warning form-control" Style="height: 37px; width: 100px;">
                          <i class="glyphicon glyphicon-plus"></i>&nbsp;แก้ไขข้อมูล</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="false" ShowHeader="true"
                ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView6_PageIndexChanged" OnRowCommand="GridView6_RowCommand">
                <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="itemoid" HeaderText="idสินค้า" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="lineoid" HeaderText="idline" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="mt_name" HeaderText="รายการสินค้า" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_item_dest" HeaderText="รายละเอียดสินค้า" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_price" HeaderText="ราคาต่อหน่วย" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_disc1_price" HeaderText="ส่วนลดต่อหน่วย" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_disc2_price" HeaderText="ส่วนลดต่อหน่วย - กรณีลูกค้ารับสินค้าเอง " ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_qty" HeaderText="หน่วยนับ" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_netprice_amt" HeaderText="รวมมูลค่า" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:BoundField DataField="line_memo" HeaderText="อธิบายเพิ่มเติม" ItemStyle-Wrap="true" HeaderStyle-Wrap="true" />
                    <asp:TemplateField ItemStyle-Width="24%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" class="btn btn-labeled btn-warning" Width="95px" Height="38px" CommandArgument='<%# Eval("itemoid") %>' CommandName="edititem"><span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>
                                แก้ไข
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkSelect" runat="server" class="btn btn-labeled btn-danger" Width="90px" Height="38px" OnClientClick="ConfirmCancel()" CommandArgument='<%# Eval("lineoid") %>' CommandName="deleteitem"><span class="btn-label"><i class="glyphicon glyphicon-trash"></i></span>
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
            <hr />
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
                    <asp:TextBox ID="txn_memo" runat="server" TextMode="multiline" Columns="50" Rows="1" class="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">จำนวนเงินมัดจำ</label>
                <div class="col-md-4">
                    <asp:TextBox ID="sodepos_amt" Style="text-align: right" runat="server" class="form-control"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ยอดเงินที่รับชำระแล้ว</label>
                <div class="col-md-4">
                    <asp:TextBox ID="depos_amt" Style="text-align: right" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
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
                    <asp:TextBox ID="disc1_amt" Style="text-align: right" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
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
                    <asp:TextBox ID="tax_amt" Style="text-align: right" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
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
                    <asp:TextBox ID="txn_total" Style="text-align: right" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
        </div>
    </div>
    <div id="tab2_page2" class="col-md-12 text-center" style="padding-left: 10%; display: none">
        <div class="form-group" style="padding-left: 10%">
            <div class="col-md-12">
                <asp:LinkButton ID="buttonBack" runat="server" OnClick="gotoHistory" class="btn btn-labeled btn-primary">
                    <span class="btn-label"><i class="glyphicon glyphicon-circle-arrow-left"></i></span>กลับหน้าหลัก
                </asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="SaveData" class="btn btn-labeled btn-success">
                    <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
