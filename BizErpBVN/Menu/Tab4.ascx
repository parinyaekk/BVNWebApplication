<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab4.ascx.cs" Inherits="BizErpBVN.Menu.Tab4" %>




<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>

<!DOCTYPE html>
<head>
    <title></title>
</head>
<style type="text/css">

    table {
        max-width: none;
        background-color: transparent;
        border-collapse: collapse;
        border-spacing: 0;
    }

    .table {
        width: auto;
        height: auto;
        margin-bottom: 20px;
    }

        .table th, .table td {
            width: auto;
            height: auto;
            padding: 8px;
            line-height: 20px;
            text-align: center;
            vertical-align: top;
            border-top: 1px solid #dddddd;
        }

        .table th {
            width: auto;
            height: auto;
            font-weight: bold;
        }

        .table thead th {
            vertical-align: bottom;
        }


</style>
<script type="text/javascript">  
    function ConfirmCancel() {
    
            var selectedvalue = confirm("คุณต้องการอนุมัติรายการใช่หรือไม่ ?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
              } else {
                alert("xxxx");
              }

    }
 </script>  
<br />
<br />
<body>
    <form id="formtab4">
        <div class="col-md-12 text-center" style="padding-left: 10%">
            <asp:GridView ID="GridViewT4" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" CssClass="table table-bordered table-striped" Width="110%" ShowFooter="true" ShowHeader="true"
                ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridViewT4_SelectedIndexChanging"  HorizontalAlign="Center">
                <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ShowHeader="true" HeaderText="สถานะรายการ" HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("sodepos_type_name")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="วันที่" HeaderStyle-Width="110px" >
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("txn_date","{0:d MMM yyyy}")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="เลขรายการ" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("txn_num")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="ลูกค้า" HeaderStyle-Width="180px">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("cust_name")%>' Style="word-wrap: normal; word-break: break-all;" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="รวมทั้งสิ้น" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# string.Format("{0:#,###0}", Eval("txn_total"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="การชำระเงินมัดจำ" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# string.Format("{0:#,###0}", Eval("sodepos_type"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="ยอดที่ต้องชำระ" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# string.Format("{0:#,###0}", Eval("sodepos_amt"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="ยอดเงินที่ได้รับแล้ว" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# string.Format("{0:#,###0}", Eval("depos_amt"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="บันทึกเพิ่มเติม" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("txn_memo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="18%">
                        <ItemTemplate>
                     <asp:LinkButton ID="btnApprove" runat="server" CommandArgument='<%# Eval("oid")%>' OnClick="Save"  Text="อนุมัติ" CssClass="btn btn-success" >
                      <i class="glyphicon glyphicon-ok"></i>&nbsp;อนุมัติ
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" runat="server"  OnClick="test"   Text="ยกเลิก" CssClass="btn btn-danger">
                      <i class="glyphicon glyphicon-remove"></i>&nbsp;ยกเลิก

                    </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px"  HorizontalAlign="Center"/>
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    <asp:HiddenField ID="txtconformmessageValue" runat="server" />
    </form>
</body>