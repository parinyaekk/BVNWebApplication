<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab5.ascx.cs" Inherits="BizErpBVN.Menu.Tab5" %>

<!DOCTYPE html>
<head>
    <title></title>
</head>
<style type="text/css">
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
<form id="formtab3">

<div class="col-md-12 text-center" style="padding-left: 10%">
 <table>
        <tr>
            <td>
                <asp:TextBox ID="txtSearh" runat="server" CssClass="form-control" Width="300px" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
            <td></td>
            <td>
                <asp:LinkButton ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn btn-primary" Height="38px">
                <i class="glyphicon glyphicon-search"></i> &nbsp;ค้นหา

                </asp:LinkButton>
            </td>
            <td></td>
            <td>
                <asp:LinkButton ID="ButtonClear" runat="server" Text="เคลียร์" CssClass="btn btn-warning" OnClientClick="Clear()" Height="40px">
                  <i class="glyphicon glyphicon-refresh" style="padding-bottom:5px"></i> &nbsp;เคลียร์
                </asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                     <asp:LinkButton ID="btnAdd" runat="server" Text="เพิ่ม" CssClass="btn btn-success" OnClientClick="fncOpenPopup_Trans(); return false;" Height="40px">
                  <i class="glyphicon glyphicon-plus" style="padding-bottom:5px"></i> &nbsp;เพิ่ม
                </asp:LinkButton>


            </td>
        </tr>
    </table>

    <br />
    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="true" ShowHeader="true"
        ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView5_SelectedIndexChanging">
        <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="ลำดับที่" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:Label ID="lblRunNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขทะเบียนรถ" ItemStyle-Width="30%">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("mt_code") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายละเอียดรถ" ItemStyle-Width="40%">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("mt_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                <ItemTemplate>
                    <asp:LinkButton ID="ButtonEdit" runat="server" CommandName="Edit" Text="แก้ไข" CssClass="btn btn-info" Height="40px" Width="80px">
                      <i class="glyphicon glyphicon-wrench"></i>&nbsp;แก้ไข
                    </asp:LinkButton>
                    <asp:LinkButton ID="ButtonDelete" runat="server" CommandName="Delete" Text="ลบ" CssClass="btn btn-danger" OnClientClick="ConfirmDelete()" Height="40px" Width="80px">
                      <i class="glyphicon glyphicon-trash"></i>&nbsp;ลบ

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
</div>
</form>
</body>
