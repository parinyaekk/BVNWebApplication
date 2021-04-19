<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="Approve_Purchase.aspx.cs" Inherits="BizErpBVN.Menu.Approve_Purchase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to reject data?")) {
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
                     <asp:LinkButton ID="btnApprove" runat="server" CommandArgument='<%# Eval("oid")%>' OnClick="Approve" Text="อนุมัติ" CssClass="btn btn-success" >
                      <i class="glyphicon glyphicon-ok"></i>&nbsp;อนุมัติ
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" runat="server" CommandArgument='<%# Eval("oid")%>' OnClick="Reject" Text="ยกเลิก" CssClass="btn btn-danger" OnClientClick="ConfirmCancel()" >
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
</asp:Content>
