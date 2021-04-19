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
        <div id="tab2_page1" class="col-md-12 text-center" style="padding-left: 10%">

        <div class="form-group">
            <label for="title" class="col-md-2 control-label">วันที่</label>
            <div class="col-md-4">
                <input type="date" class="form-control" id="txtDate" runat="server">
            </div>
            <label for="title" class="col-md-2 control-label">จำนวนเงิน</label>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtDepos_amt" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="form-group">
            <label for="title" class="col-md-2 control-label">บัญชีโอนเงิน</label>
            <div class="col-md-4">
                <asp:DropDownList ID="cbbAcct_cashin" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
            <label for="title" class="col-md-2 control-label">คำอธิบายรายการ</label>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-sm-12">
                        <textarea data-ng-model="tutorial.description" rows="2" runat="server"
                            name="description" class="form-control" id="txtTxn_memo">
                        </textarea>

                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
             <br />
        <div class="form-group">
            <label for="title" class="col-md-2 control-label">อัพโหลด Slip</label>
            <div class="col-md-4">
                <asp:FileUpload ID="fileupload" runat="server" />
                   <asp:Panel ID="frmConfirmation" Visible="False" Runat="server">
            <asp:Label id="lblUploadResult" Runat="server"></asp:Label>
         </asp:Panel>
            </div>
            <label for="title" class="col-md-2 control-label"></label>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="AddData" CssClass="btn btn-info form-control" Style="height: 38px; width: 120px;">
             <i class="glyphicon glyphicon-plus"></i> Update ข้อมูล</asp:LinkButton>
                        </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
        <br />
    <div class="col-md-12 text-center" style="padding-left: 15%">
        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" HeaderStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped text-center" Width="110%" ShowFooter="true" ShowHeader="true"
            ShowHeaderWhenEmpty="true" GridLines="None" CellPadding="4" OnPageIndexChanging="GridView5_SelectedIndexChanging">
            <EmptyDataTemplate>ไม่พบข้อมูล</EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="txn_date" HeaderText="วันที่" />
                <asp:BoundField DataField="depos_amt" HeaderText="จำนวนเงิน" />
                <asp:BoundField DataField="mt_name" HeaderText="บัญชีโอนเงิน" />
                <asp:BoundField DataField="txn_memo" HeaderText="คำอธิบายรายการ" />
               <%--<asp:ImageField  DataImageUrlField="img_file" HeaderText="Image"></asp:ImageField>--%> 
                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="130px" HeaderText="รูปภาพ">
                    <ItemTemplate>
                            <img src='data:image/jpg;base64,<%# Eval("img_value") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("img_value")) : null %>' alt="image" height="100" width="200"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSaves" runat="server" CssClass="btn btn-labeled btn-warning" Style="height: 45px; width: 100px;" CommandArgument='<%#Eval("oid") %>' OnClick="btnSaves_Click">
                                <i class="glyphicon glyphicon-wrench" style="padding-top:8px"></i> แก้ไข</asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <PagerStyle BackColor="#D9DABF" ForeColor="#3B3B37" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Salmon" Font-Bold="True" ForeColor="#3B3B37" Width="400px" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <div class="col-md-12 text-center">
            <asp:LinkButton ID="btnSaves" runat="server" CssClass="btn btn-success form-control" Style="height: 38px; width: 120px;">
             <i class="glyphicon glyphicon-floppy-saved"></i> บันทึก</asp:LinkButton>
            &nbsp;
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger form-control" Style="height: 38px; width: 120px;">
             <i class="glyphicon glyphicon-remove"></i> ยกเลิก</asp:LinkButton>
        </div>
    </div>
</asp:Content>
