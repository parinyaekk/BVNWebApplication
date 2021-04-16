<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab1_Add.ascx.cs" Inherits="BizErpBVN.Menu.Tap1_Add" %>

<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>


<!DOCTYPE html>

<head>
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
    </style>
    <script type="text/javascript">
</script>
</head>
<br />

<form id="form2">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <div id="tabT11" class="col-md-12 text-center" style="padding-left: 10%">
        <div class="container">
                        <div class="col-md-12" style="text-align:center">
                <div class="col-md-12">
                                   <button type="button" class="btn btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-floppy-save"></i></span>บันทึก
                    </button>
                    &nbsp;
                    <button id="btnBack"  type="button" class="btn btn-primary">
                        <span class="btn-label"><i class="glyphicon glyphicon-circle-arrow-left"></i></span>กลับหน้าหลัก
                    </button>
     
                </div>
           
            </div>
            <div class="col-md-12 text-center">
                <div class="form-group">
                    <label for="description" class="col-md-2 control-label">รหัสลูกค้า</label>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMtCode" runat="server" class="form-control"></asp:TextBox>
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
            <hr />
            <h2>รายละเอียด</h2>
            <hr />
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
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <label for="title" class="col-md-2 control-label">วงเงินบัตรเครดิต</label>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control"></asp:DropDownList>
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
            <hr />
            <h2>ที่อยู่หลัก</h2>
            <hr />
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
                                    <asp:DropDownList ID="cbbSubDistrict" runat="server" class="form-control"></asp:DropDownList>
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
                    <asp:TextBox ID="txtZipcode" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <label for="title" class="col-md-2 control-label"></label>
                <div class="col-md-4">
     <label for="title" class="col-md-2 control-label" style="display:none"></label>
                </div>
            </div>
                        <br />
            <hr />
            <h2>ที่อยู่อื่นๆ</h2>
            <hr />
                        <br />
             <asp:GridView ID="Gridview2" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" ViewStateMode="Inherit">
            <Columns>

                <asp:TemplateField HeaderText="ชื่อผู้ติดต่อ">
                    <ItemTemplate>
                        <asp:TextBox TextMode="multiline" Columns="80" Rows="1" ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ที่อยู่ ">
                    <ItemTemplate>
                        <asp:TextBox TextMode="multiline" Columns="80" Rows="3" ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="โทรศัพท์">
                    <ItemTemplate>
                        <asp:TextBox  ID="TextBox3" runat="server" class="form-control" Width="180px" TextMode="Number" MaxLength="10"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fax">
                    <ItemTemplate>
                        <asp:TextBox TextMode="multiline" Columns="80" Rows="1" ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="E-Mail">
                    <ItemTemplate>
                        <asp:TextBox TextMode="multiline" Columns="80" Rows="1" ID="TextBox5" runat="server" class="form-control"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <FooterTemplate>
                                <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-success form-control" Text="เพิ่ม" OnClick="ButtonAdd_Click" Style="height: 38px; width: 120px"></asp:Button>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            <br />
        </div>

    </div>
</form>



