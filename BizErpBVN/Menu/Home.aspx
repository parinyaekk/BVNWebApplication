<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BizErpBVN.Menu.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
<script>
    $(document).ready(function () {
        $('#tab1_page2').hide();
        $('#tab1_page1').show();

        $('#btnAdd1').click(function () {
            window.location.href = "AddCustomer.aspx";
        });

        $('#btnBack').click(function () {
            $('#tab1_page1').show();
            $('#tab1_page2').hide();
        });

        $('#btnEdit').click(function () {
            $('#tab1_page2').show();
            $('#tab1_page1').hide();
        });

    });
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
<div id="tab1_page1" class="col-md-12 text-center" style="padding-left: 10%">
    <table >
        <tr>
            <td>
                <asp:TextBox ID="txtSearh" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>
            <td></td>
              <td></td>
            <td style="padding-top:10px">
                <asp:LinkButton ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn btn-primary" Height="38px" OnClick="btnSearch_Click">
                <i class="glyphicon glyphicon-search"></i> &nbsp;ค้นหา

                </asp:LinkButton>
            </td>
            <td></td>
            <td style="padding-top:10px">
                <asp:LinkButton ID="ButtonClear" runat="server" Text="เคลียร์" CssClass="btn btn-info"  Height="40px" OnClick="ButtonClear_Click">
                  <i class="glyphicon glyphicon-refresh" style="padding-bottom:5px"></i> &nbsp;เคลียร์
                </asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <button id="btnAdd1" type="button" class="btn btn-labeled btn-success">
                        <span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>เพิ่ม
                </button>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped" Width="100%" ShowFooter="true" ShowHeader="true"
        ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView1_PageIndexChanged" OnRowCommand="GridView1_RowCommand">
        <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="mt_code" HeaderText="รหัสลูกค้า" ReadOnly="true" />
            <asp:BoundField DataField="mt_name" HeaderText="ชื่อลูกค้า" />
            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" class="btn btn-labeled btn-warning" CommandArgument='<%# "edit;" + Eval("oid") %>'><span class="btn-label"><i class="glyphicon glyphicon-wrench"></i></span>
                        แก้ไข
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkSelect" runat="server" class="btn btn-labeled btn-danger" CommandArgument='<%# "delete;" + Eval("oid") %>' OnClientClick="ConfirmCancel()"><span class="btn-label"><i class="glyphicon glyphicon-trash"></i></span>
                        ลบ
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</div>
</asp:Content>
