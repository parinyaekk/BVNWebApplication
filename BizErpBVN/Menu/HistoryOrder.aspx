<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MainMenu.Master" AutoEventWireup="true" CodeBehind="HistoryOrder.aspx.cs" Inherits="BizErpBVN.Menu.HistoryOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView1_PageIndexChanged"
        OnRowCommand="GridView1_RowCommand">
        <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="txn_date" HeaderText="วันที่เอกสาร" ReadOnly="true" />
            <asp:BoundField DataField="txn_num" HeaderText="เลขที่เอกสาร" />
             <asp:BoundField DataField="cust_name" HeaderText="ลูกค้า" />
            <asp:BoundField DataField="txn_total" HeaderText="ยอดรวมทั้งสิ้น" />
            <asp:BoundField DataField="txn_status_name" HeaderText="สถานะ" /> 
            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Names="Tahoma">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="RowEdit" Text="ลบ" CssClass="btn btn-warning" Height="40px" Width="80px" CommandArgument='<%# "edit;" + Eval("oid") %>'>
                      <i class="glyphicon glyphicon-wrench"></i>&nbsp;แก้ไข
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
