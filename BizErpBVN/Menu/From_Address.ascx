<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="From_Address.ascx.cs" Inherits="BizErpBVN.Menu.From_Address" %>

<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">



<!DOCTYPE html>

<head>
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style type="text/css">

body {
  font-family: 'Open Sans', sans-serif;
  font-weight: 300;
}
.tabs {
  max-width: 750px;
  margin: 0 auto;
  padding-left: 0px;
  padding-right:0px;
  text-align:center;
}
#tab-button {
  display: table;
  table-layout: fixed;
  width: 100%;
  margin: 0;
  padding: 0;
  list-style: none;
}
#tab-button li {
  display: table-cell;
  width: 20%;
}
#tab-button li a {
  display: block;
  padding: .5em;
  background: #D1D0CE;
  border: 1px solid #ddd;
  text-align: center;
  color: #000;
  text-decoration: none;
}
#tab-button li:not(:first-child) a {
  border-left: none;
}
#tab-button li a:hover,
#tab-button .is-active a {
  border-bottom-color: transparent;
  background: #fff;
}
.tab-contents {
  padding: .5em 2em 1em;
  border: 1px solid #ddd;
  width: 800px;
  padding-left:0px;
  padding-right:0px;
}



.tab-button-outer {
  display: none;
  height: 30px;
  width: 800px;
}
.tab-contents {
  margin-top: 20px;
}
@media screen and (min-width: 800px) {
  .tab-button-outer {
    position: relative;
    z-index: 2;
    display: block;
  }
  .tab-select-outer {
    display: none;
  }
  .tab-contents {
    position: relative;
    top: -1px;
    margin-top: 0;
  }
}
.modal-lg {
    max-width: 100%;
}
table
{
    max-width:none;
    background-color:transparent;
    border-collapse:collapse;
    border-spacing:0;
}

.table
{
    width:auto;
    height:auto;
    margin-bottom:20px;
}

.table th,.table td
{
    width:auto;
    height:auto;
    padding:8px;
    line-height:20px;
    text-align:left;
    vertical-align:top;
    border-top:1px solid #dddddd;
}

.table th
{
    width:auto;
    height:auto;
    font-weight:bold;
}

.table thead th
{
    vertical-align:bottom;
}

    </style>
    <script type="text/javascript">

        $(function () {
            var $tabButtonItem = $('#tab-button li'),
                $tabSelect = $('#tab-select'),
                $tabContents = $('.tab-contents'),
                activeClass = 'is-active';

            $tabButtonItem.first().addClass(activeClass);
            $tabContents.not(':first').hide();

            $tabButtonItem.find('a').on('click', function (e) {
                var target = $(this).attr('href');

                $tabButtonItem.removeClass(activeClass);
                $(this).parent().addClass(activeClass);
                $tabSelect.val(target);
                $tabContents.hide();
                $(target).show();
                e.preventDefault();
            });

            $tabSelect.on('change', function () {
                var target = $(this).val(),
                    targetSelectNum = $(this).prop('selectedIndex');

                $tabButtonItem.removeClass(activeClass);
                $tabButtonItem.eq(targetSelectNum).addClass(activeClass);
                $tabContents.hide();
                $(target).show();
            });
        });

        function closeWin() {
            window.close();
        }


    </script>
</head>
<br />
<body>
    <form style="padding-left:0px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
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
            <br />
            <br />
             <div class="col-md-12 text-center">


        <div class="tabs" style="padding-right:900px">
            <asp:Label runat="server" for="tab" id="checkTab"></asp:Label>
            <div class="tab-button-outer" id="tab" style="width:900px">
                <ul id="tab-button">
                    <li><a  href="#tab01" onclick="$get('tab').value = '0';" tabindex="1">รายละเอียด</a></li>
                    <li><a  href="#tab02" onclick="$get('tab').value = '1';" tabindex="2">ที่อยู่หลัก</a></li>
                    <li><a  href="#tab03" onclick="$get('tab').value = '2';" tabindex="3">ที่อยู่อื่นๆ</a></li>
                </ul>
            </div>
            <div id="tab01" class="tab-contents"  style="width:900px; text-align:center">
                        <br />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="ui-grid-col-2,ui-grid-col-4">
                        <label for="title" class="col-md-2">ประเภทองค์กร</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="cbbDepartMent" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="ui-grid-col-2,ui-grid-col-4">
                        <label for="title" class="col-md-2">การขนส่งสินค้า</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="cbbTransport" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="ui-grid-col-2,ui-grid-col-4">
                        <label for="title" class="col-md-2">กลุ่มลูกค้า</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="cbbCustomer" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="ui-grid-col-2,ui-grid-col-4">
                        <label for="title" class="col-md-2">พนักงานขาย</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="cbbSale" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="ui-grid-col-2,ui-grid-col-4">
                        <label for="title" class="col-md-2" style="word-wrap: break-word">เลขนิติบุคคล/เลขบัตรประชาชน</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtReg" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="ui-grid-col-2,ui-grid-col-4">
                        <label for="title" class="col-md-2">เงื่อนไขการชำระเงิน</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="cbbPymt" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">เลขประจำตัวผู้เสียภาษี</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtNum" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">วงเงินบัตรเครดิต</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCrlimit" runat="server" class="form-control"  TextMode="Number">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">เลขที่สาขา</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBrnNum" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">ชื่อสาขา</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBrnName" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="tab02" class="tab-contents" style="width:900px; text-align:center">
        <br />
            <br />
        <div class="row">
            <div class="col-md-12">
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">ชื่อผู้ติดต่อ</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtContName" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">เบอร์โทรศัพท์</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtPhn1" runat="server" class="form-control"  TextMode="Number" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">ที่อยู่</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtAddr1" TextMode="multiline" Columns="40" Rows="3" runat="server" class="form-control" />
                    </div>
                </div>
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">จังหวัด</label>
                    <div class="col-md-4">
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
        <div class="row">
            <div class="col-md-12">
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">อำเภอ</label>
                    <div class="col-md-4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbAmperr" runat="server" class="form-control" OnSelectedIndexChanged="cbbAmperr_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">ตำบล</label>
                    <div class="col-md-4">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbSubDistrict" runat="server" class="form-control"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="ui-grid-col-2,ui-grid-col-4">
                    <label for="title" class="col-md-2">รหัสไปรษณีย์</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtZipcode" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="tab03" class="tab-contents" style="width:900px; text-align:center">
        <br />
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
    </div>
                </div>
            <br />
            <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btnSaves" runat="server" CssClass="btn btn-success form-control" Style="height: 38px; width: 120px">
                                <i class="glyphicon glyphicon-floppy-saved"></i> บันทึก</asp:LinkButton>
                &nbsp;
                    <button id="btnClose" type="button" class="btn btn-danger" data-dismiss="modal" style="height: 38px; width: 120px" onclick="closeWin()">
                        <span class="btn-label"><i class="glyphicon glyphicon-remove"></i></span>&nbsp;ยกเลิกรายการ</button>
           
            </div>
        </div>
    </form>
</body>

