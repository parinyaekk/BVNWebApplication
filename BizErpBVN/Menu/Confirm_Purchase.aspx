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
                    <asp:TemplateField ShowHeader="true" HeaderText="วันที่" HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <input type="date" class="form-control">
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="จำนวนเงิน" HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="บัญชีโอนเงิน" HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" HeaderText="คำอธิบายรายการ" HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upload" HeaderStyle-Width="130px">
                        <ItemTemplate>
                            <asp:FileUpload runat="server" EnableViewState="true" />
                            <asp:Button ID="saveBtn" runat="server"
                                CommandArgument="<%# Container.DataItemIndex%>" CommandName="save"
                                Text="OK" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <hr />
            <div class="col-md-12 text-center">
                <asp:LinkButton ID="btnSaves" runat="server" CssClass="btn btn-success form-control" Style="height: 38px; width: 120px;">
                                <i class="glyphicon glyphicon-floppy-saved"></i> บันทึก</asp:LinkButton>
                &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger" OnClientClick="closeWin()" Style="height: 38px; width: 120px;">
                                <i class="glyphicon glyphicon-remove"></i> ยกเลิกรายการ</asp:LinkButton>
            </div>
        </div>
</asp:Content>
