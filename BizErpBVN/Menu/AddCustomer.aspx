<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="BizErpBVN.Menu.AddCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            $('#btnBack').click(function () {
                window.location.href = "Home.aspx";
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="tabT11" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="container">
            <div class="col-md-12" style="text-align: center">
                <div class="col-md-12">
                    <asp:LinkButton type="button" class="btn btn-success" runat="server" OnClick="btnSave_Click">
                        <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                    </asp:LinkButton>
                    &nbsp;
                   
                    <button id="btnBack" type="button" class="btn btn-primary">
                        <span class="btn-label"><i class="glyphicon glyphicon-circle-arrow-left"></i></span>กลับหน้าหลัก
                   
                    </button>

                </div>

            </div>
            <div class="col-md-12 text-center">
                <div class="form-group">
                    <label for="description" class="col-md-2 control-label">รหัสลูกค้า</label>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMtCode" runat="server" class="form-control" Enabled="true"></asp:TextBox>
                    </div>
                </div>
                <br />
                <br />
                <div class="form-group">
                    <label for="description" class="col-md-2 control-label">ชื่อลูกค้า</label>
                    <div class="col-md-8">

                        <asp:TextBox ID="txtMtName" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <hr />
            <h2>รายละเอียด</h2>
            <hr />
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ประเภทองค์กร</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbbDepartMent" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <label for="title" class="col-md-2 control-label">การขนส่งสินค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbTransport" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">กลุ่มลูกค้า</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbCustomer" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">พนักงานขาย</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbbSale" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เลขนิติบุคคล/เลขบัตรประชาชน</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtReg" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">เงื่อนไขการชำระเงิน</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="cbbPymt" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เลขประจำตัวผู้เสียภาษี</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtTaxNum" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">วงเงินบัตรเครดิต</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtCrLimit" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เลขที่สาขา</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtBrnNum" runat="server" class="form-control"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ชื่อสาขา</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtBrnName" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <hr />
            <h2>ที่อยู่หลัก</h2>
            <hr />
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ชื่อผู้ติดต่อ</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtContName" runat="server" class="form-control"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">เบอร์โทรศัพท์</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtPhn1" runat="server" class="form-control" TextMode="Number" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />

            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ที่อยู่</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtAddr1" TextMode="multiline" Columns="40" Rows="3" runat="server" class="form-control" />
                </div>
                <label for="title" class="col-md-2 control-label">จังหวัด</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbbProvinceTh" runat="server" class="form-control" OnSelectedIndexChanged="cbbProvinceTh_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">อำเภอ</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbAmperr" runat="server" class="form-control" OnSelectedIndexChanged="cbbAmperr_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label">ตำบล</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbbSubDistrict" runat="server" class="form-control" OnSelectedIndexChanged="cbbLocate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">รหัสไปรษณีย์</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtZipcode" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <label for="title" class="col-md-2 control-label" style="display: none"></label>
                </div>
            </div>
            <br />
            <br />
            <hr />
            <h2>ที่อยู่อื่นๆ</h2>
            <hr />
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ชื่อผู้ติดต่อ</label>
                <div class="col-md-4">
                    <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">ที่อยู่</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">จังหวัด</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbbProvinceTh2" runat="server" class="form-control" OnSelectedIndexChanged="cbbProvinceTh2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">อำเภอ</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbbAmperr2" runat="server" class="form-control" OnSelectedIndexChanged="cbbAmperr2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">ตำบล</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbSubDistrict2" runat="server" class="form-control" OnSelectedIndexChanged="cbbLocate2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label for="title" class="col-md-2 control-label">รหัสไปรษณีย์</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtZipCode2" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">เบอร์โทรศัพท์</label>
                <div class="col-md-4">
                    <asp:TextBox ID="TextBox3" runat="server" class="form-control" TextMode="Number" MaxLength="10"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label">E-Mail</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtMail2" runat="server" class="form-control"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="form-group">
                <label for="title" class="col-md-2 control-label">Fax</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtFax" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
                    <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-success form-control" OnClick="ButtonAdd_Click" Text="เพิ่มที่อยู่" Style="height: 38px; width: 120px"></asp:Button>
                </div>
            </div>
            <br />
            <br />
            <asp:GridView ID="GridViewTdd1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped text-center" Width="110%" ShowHeader="true"
                ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridViewTdd1_PageIndexChanged">
                <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="contname" HeaderText="ผู้ติดต่อ" />
                    <asp:BoundField DataField="street" HeaderText="ที่อยู่" />
                    <asp:BoundField DataField="phn" HeaderText="โทรศัพท์" />
                    <asp:BoundField DataField="prov_code" HeaderText="จังหวัด" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="prov_txt" HeaderText="ชื่อจังหวัด" />
                    <asp:BoundField DataField="amphur_code" HeaderText="อำเภอ" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="amphur_txt" HeaderText="ชื่ออำเภอ" />
                    <asp:BoundField DataField="locat_code" HeaderText="ตำบล" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="locat_txt" HeaderText="ชื่อตำบล" />
                    <asp:BoundField DataField="zipcode" HeaderText="รหัสไปรษณีย์" />
                    <asp:BoundField DataField="email" HeaderText="E-Mail" />
                    <asp:BoundField DataField="fax" HeaderText="Fax" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
            </asp:GridView>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
