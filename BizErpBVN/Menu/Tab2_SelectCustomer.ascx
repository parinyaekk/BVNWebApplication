<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab2_SelectCustomer.ascx.cs" Inherits="BizErpBVN.Menu.WebUserControl2" %>

<!DOCTYPE html>
<head >
    <title></title>
</head>
<script type="text/javascript">


    function ConfirmDelete() {
        var x = confirm("คุณต้องการลบข้อมูลใช่หรือไม่?");
        if (x)
            return true;
        else
            return false;
    }

</script>

<br />
<br />
<body>
<form id="formSelectCustomer">

<div class="col-md-12 text-center" >
    <table>
        <tr>
            <td>
                <asp:TextBox ID="txtSearh" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>
            <td></td>
            <td>
                <asp:LinkButton ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn btn-primary" Height="38px" OnClick="btnSearch_Click">
                <i class="glyphicon glyphicon-search"></i> &nbsp;ค้นหา

                </asp:LinkButton>
            </td>
            <td></td>
            <td>
                <asp:LinkButton ID="ButtonClear" runat="server" Text="เคลียร์" CssClass="btn btn-warning"  Height="40px" OnClick="ButtonClear_Click">
                  <i class="glyphicon glyphicon-refresh" style="padding-bottom:5px"></i> &nbsp;เคลียร์
                </asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="true" ShowHeader="true"
        ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView1_PageIndexChanged">
        <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
        <Columns>
             <asp:TemplateField>
                   <ItemTemplate>
                       <asp:CheckBox ID="ckBox" runat="server"/>
                   </ItemTemplate>
               </asp:TemplateField>
            <asp:BoundField DataField="txn_date" HeaderText="วันที่เอกสาร" ReadOnly="true" />
            <asp:BoundField DataField="txn_num" HeaderText="เลขที่เอกสาร" />
             <asp:BoundField DataField="cust_name" HeaderText="ลูกค้า" />
            <asp:BoundField DataField="txn_total" HeaderText="ยอดรวมทั้งสิ้น" />
            <asp:BoundField DataField="txn_status_name" HeaderText="สถานะ" /> 
            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDeleted" runat="server" CommandName="RowDeleting " Text="ลบ" CssClass="btn btn-danger" Height="40px" Width="80px">
                      <i class="glyphicon glyphicon-trash"></i>&nbsp;ลบ
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
 
</div>
</form>
</body>
