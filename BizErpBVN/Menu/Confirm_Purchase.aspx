<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="Confirm_Purchase.aspx.cs" Inherits="BizErpBVN.Menu.Confirm_Purchase" %>
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
        text-align: left;
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
<script>
    function closeWin() {
        window.close();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 text-center" style="padding-left: 15%">
        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped text-center" Width="110%" ShowFooter="true" ShowHeader="true"
            ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView5_SelectedIndexChanging">
            <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="txn_status" HeaderText="สถานะรายการ"/>
                <asp:BoundField DataField="txn_date" HeaderText="วันที่" />
                <asp:BoundField DataField="txn_num" HeaderText="เลขรายการ"/>
                <asp:BoundField DataField="cust_oid" HeaderText="ลูกค้า" />
                <asp:BoundField DataField="txn_tatol" HeaderText="รวมทั้งสิ้น"/>
                <asp:BoundField DataField="sodepos_type" HeaderText="การชำระเงินมัดจำ" />
                <asp:BoundField DataField="sodepos_amt" HeaderText="ยอดที่ต้องชำระ"/>
                <asp:BoundField DataField="depos_amt" HeaderText="ยอดเงินที่ได้รับแล้ว" />
                <asp:BoundField DataField="txn_memo" HeaderText="บันทึกเพิ่มเติม" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <hr />
            <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="true" ShowHeader="true"
            ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView8_PageIndexChanging">
            <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="txn_status" HeaderText="ลำดับที่"/>
                <asp:BoundField DataField="txn_date" HeaderText="รหัสินค้า" />
                <asp:BoundField DataField="txn_num" HeaderText="ชื่อสินค้า"/>
                <asp:BoundField DataField="cust_oid" HeaderText="ยอดคงเหลือ" />
                <asp:BoundField DataField="txn_tatol" HeaderText="จำนวนสั่งขาย"/>
                <asp:BoundField DataField="sodepos_type" HeaderText="หน่วยนับ" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <div class="col-md-12 text-center">
                <asp:LinkButton ID="btnSaves" runat="server" CssClass="btn btn-success form-control" style="height: 38px; width: 120px;">
                            <i class="glyphicon glyphicon-floppy-saved"></i> บันทึก</asp:LinkButton>
            &nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger" OnClientClick="closeWin()" style="height: 38px; width: 120px;">
                            <i class="glyphicon glyphicon-remove"></i> ยกเลิกรายการ</asp:LinkButton>
        </div>
    </div>
</asp:Content>
