using BizErpBVN.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizErpBVN.Menu
{
    public partial class EditOrder : System.Web.UI.Page
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        DataTable dt;
        public Guid GGID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");
            }

            if (!Page.IsPostBack)
            {
                GGID = Guid.Parse(Request.QueryString["ggid"]);
                Session["parent_oid"] = GGID;
                cbbStatus.Enabled = false;
                cbbStatus.CssClass = "form-control";
                this.Loadsodepos();
                this.LoadDepartMent();
                this.CustomerGroup();
                this.Transportation();
                this.Employee();
                this.LoadTaxcalc();
                this.refreshdataT2();
                this.LoadStatus();
                createDataTable();
                DataRow drNew = dt.NewRow();
                dt.Rows.Add(drNew);
                Session["dttableline"] = dt;
                getEditData(GGID);
                this.LoadItem();
                en_saledelry_type.Enabled = false;
                en_saledelry_type.CssClass = "form-control";
            }
        }
        private void createDataTable()
        {
            //mt_name
            //line_item_dest
            //line_price
            //line_disc1_price
            //line_disc2_price
            //line_qty
            //line_netprice_amt
            //line_memo
            dt = new DataTable();
            DataColumn dc1 = new DataColumn("mt_name");
            DataColumn dc2 = new DataColumn("line_item_dest");
            DataColumn dc3 = new DataColumn("line_price");
            DataColumn dc4 = new DataColumn("line_disc1_price");
            DataColumn dc5 = new DataColumn("line_disc2_price");
            DataColumn dc6 = new DataColumn("line_qty");
            DataColumn dc7 = new DataColumn("line_netprice_amt");
            DataColumn dc8 = new DataColumn("line_memo");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
        }

        protected void getEditData(Guid ggid)
        {
            try
            {
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT * FROM qtxn_so WHERE oid = @oid", conn);
                sqCommand.Parameters.AddWithValue("@oid", ggid);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txn_num.Text = ds.Tables[0].Rows[0]["txn_num"].ToString();
                    Session["sodepos_type"] = ds.Tables[0].Rows[0]["sodepos_type"].ToString();
                    cbbStatus.Text = ds.Tables[0].Rows[0]["txn_status"].ToString();
                    ButtonConfirm.Visible = cbbStatus.Text == "SOF_SUBMIT" ? true : false;
                    tax_num.Text = ds.Tables[0].Rows[0]["tax_num"].ToString();
                    txn_date.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["txn_date"].ToString()).ToString("MM/dd/yyyy");

                    NpgsqlCommand com = new NpgsqlCommand("select mt_code from mt_pymt where oid = @oid", conn);
                    com.Parameters.AddWithValue("@oid", Guid.Parse(ds.Tables[0].Rows[0]["pymt_oid"].ToString()));
                    NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(com);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        mt_pymt.Text = ds2.Tables[0].Rows[0]["mt_code"].ToString();
                    }

                    en_sodepos_type.Text = ds.Tables[0].Rows[0]["sodepos_type"].ToString();
                    sodepos_amt.Text = ds.Tables[0].Rows[0]["sodepos_amt"].ToString();

                    NpgsqlCommand com3 = new NpgsqlCommand("select mt_code from mt_cust where oid = @oid", conn);
                    com3.Parameters.AddWithValue("@oid", Guid.Parse(ds.Tables[0].Rows[0]["cust_oid"].ToString()));
                    NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(com3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        cbbCustgrp.Text = ds3.Tables[0].Rows[0]["mt_code"].ToString();
                    }

                    en_saledelry_type.Text = ds.Tables[0].Rows[0]["saledelry_type"].ToString();

                    NpgsqlCommand com4 = new NpgsqlCommand("select mt_code from mt_emp where oid = @oid", conn);
                    com4.Parameters.AddWithValue("@oid", Guid.Parse(ds.Tables[0].Rows[0]["salerep_oid"].ToString()));
                    NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(com4);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4);
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        mt_emp.Text = ds4.Tables[0].Rows[0]["mt_code"].ToString();
                    }

                    addr_text.Text = ds.Tables[0].Rows[0]["addr_text"].ToString();
                    ship_addr_text.Text = ds.Tables[0].Rows[0]["ship_addr_text"].ToString();

                    NpgsqlCommand com5 = new NpgsqlCommand(@"select mi.mt_name,line_item_dest,line_price,line_disc1_price,line_disc2_price,line_qty,line_netprice_amt,line_memo 
                                                            from txn_so_line tl 
                                                            JOIN mt_item mi ON tl.line_item_oid = mi.oid
                                                            where tl.parent_oid = @parent_oid ORDER BY tl.line_seq", conn);
                    com5.Parameters.AddWithValue("@parent_oid", ggid);
                    NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(com5);
                    DataSet ds5 = new DataSet();
                    da5.Fill(ds5);
                    if (ds5.Tables[0].Rows.Count > 0)
                    {
                        
                        DataTable table = Session["dttableline"] as DataTable;
                        foreach (DataRow row in ds5.Tables[0].Rows)
                        {
                            DataRow drNew = table.NewRow();
                            drNew["mt_name"] = row.ItemArray[0].ToString();
                            drNew["line_item_dest"] = row.ItemArray[1].ToString();
                            drNew["line_price"] = row.ItemArray[2].ToString();
                            drNew["line_disc1_price"] = row.ItemArray[3].ToString();
                            drNew["line_disc2_price"] = row.ItemArray[4].ToString();
                            drNew["line_qty"] = row.ItemArray[5].ToString();
                            drNew["line_netprice_amt"] = row.ItemArray[6].ToString();
                            drNew["line_memo"] = row.ItemArray[7].ToString();
                            table.Rows.Add(drNew);
                        }
                        RemoveNullColumnFromDataTable(table);
                        Session["dttableline"] = table;

                        GridView6.DataSource = table;
                        GridView6.DataBind();
                    }

                    txn_memo.Text = ds.Tables[0].Rows[0]["txn_memo"].ToString();
                    depos_amt.Text = ds.Tables[0].Rows[0]["depos_amt"].ToString();
                    disc2_amt.Text = ds.Tables[0].Rows[0]["disc2_amt"].ToString();
                    disc1_amt.Text = ds.Tables[0].Rows[0]["disc1_amt"].ToString();
                    tax_amt.Text = ds.Tables[0].Rows[0]["tax_amt"].ToString();
                    txn_total.Text = ds.Tables[0].Rows[0]["txn_total"].ToString();


                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void RemoveNullColumnFromDataTable(DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][0] == DBNull.Value && dt.Rows[i][1] == DBNull.Value && dt.Rows[i][2] == DBNull.Value && dt.Rows[i][3] == DBNull.Value && dt.Rows[i][4] == DBNull.Value && dt.Rows[i][5] == DBNull.Value)
                    dt.Rows[i].Delete();
            }
            dt.AcceptChanges();
        }

        protected void gotoHistory(Object sender, EventArgs e)
        {
            Response.Redirect("HistoryOrder.aspx");
        }
        protected void gotoOrder(Object sender, EventArgs e)
        {
            Response.Redirect("Order.aspx");
        }
        protected void Confirm(Object sender, EventArgs e)
        {
            Response.Redirect("Confirm_Purchase.aspx?parent_oid=" + Session["parent_oid"]);
        }
        protected void Cancel(Object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = @txn_status where oid = @oid", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@txn_status", "CANCEL");
                cmd.Parameters.AddWithValue("@oid", Guid.Parse(Session["parent_oid"].ToString()));
                cmd.ExecuteNonQuery();
                conn.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ยกเลิกรายการสำเร็จ')", true);
                
                Response.Redirect("HistoryOrder.aspx");
            }
        }
        protected void Submit(Object sender, EventArgs e)
        {
            try
            {
                string sodepos_type = en_sodepos_type.Text;//Convert.ToString(Session["sodepos_type"]);
                if (!String.IsNullOrEmpty(sodepos_type))
                {
                    string status = "";
                    if (sodepos_type == "NOTPAY")
                    {
                        status = "SOF_APPV01";
                    }
                    else
                    {
                        status = "SOF_SUBMIT";
                    }

                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = @txn_status where oid = @oid", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@txn_status", status);
                    cmd.Parameters.AddWithValue("@oid", Guid.Parse(Session["parent_oid"].ToString()));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ยืนยันรายการสำเร็จ')", true);

                    getEditData(Guid.Parse(Session["parent_oid"].ToString()));
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void SaveData(Object sender, EventArgs e)
        {
            try
            {
                conn.Close();

                string valmt_pymt = mt_pymt.Text; //oid
                string valaddr_text = addr_text.Text;
                string valship_addr_text = ship_addr_text.Text;
                string valen_sodepos_type = en_sodepos_type.Text;
                string valtxn_memo = txn_memo.Text;
                string valsodepos_amt = sodepos_amt.Text;

                Guid ggpymtid = new Guid();

                NpgsqlCommand com = new NpgsqlCommand("select oid from mt_pymt where mt_code = @mt_code", conn);
                com.Parameters.AddWithValue("@mt_code", valmt_pymt);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ggpymtid = Guid.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

                string query = @"UPDATE txn_so SET
                                    pymt_oid = @pymt_oid, --1
                                    addr_text = @addr_text, --2
                                    ship_addr_text = @ship_addr_text, --3
                                    sodepos_type = @sodepos_type, --4
                                    txn_memo = @txn_memo, --5
                                    sodepos_amt = @sodepos_amt --6 
                                    WHERE oid = @oid";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@oid", Guid.Parse(Session["parent_oid"].ToString())); //0
                cmd.Parameters.AddWithValue("@pymt_oid", ggpymtid); //1
                cmd.Parameters.AddWithValue("@addr_text", valaddr_text); //2
                cmd.Parameters.AddWithValue("@ship_addr_text", valship_addr_text); //3
                cmd.Parameters.AddWithValue("@sodepos_type", valen_sodepos_type); //4
                cmd.Parameters.AddWithValue("@txn_memo", valtxn_memo); //5 
                cmd.Parameters.AddWithValue("@sodepos_amt", Convert.ToDouble(valsodepos_amt)); //6
                cmd.ExecuteNonQuery();
                conn.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Update ข้อมูลสำเร็จ')", true);
            }
            catch (Exception ex)
            {

            }
        }
        protected void Loadsodepos()
        {
            NpgsqlCommand com = new NpgsqlCommand("select * from en_sodepos_type", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            en_sodepos_type.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            en_sodepos_type.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            en_sodepos_type.DataSource = ds.Tables[0];
            en_sodepos_type.DataBind();
            en_sodepos_type.Items.Insert(0, "----------เลือก----------");
        }

        protected void LoadDepartMent()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from mt_pymt", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            mt_pymt.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            mt_pymt.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            mt_pymt.DataSource = ds.Tables[0];
            mt_pymt.DataBind();
            mt_pymt.Items.Insert(0, "----------เลือก----------");
        }

        protected void CustomerGroup()
        {
            NpgsqlCommand com = new NpgsqlCommand("select mt_name,mt_code from mt_cust", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbCustgrp.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbCustgrp.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbCustgrp.DataSource = ds.Tables[0];
            cbbCustgrp.DataBind();
            cbbCustgrp.Items.Insert(0, "----------เลือก----------");
        }

        protected void Transportation()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from en_saledelry_type", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            en_saledelry_type.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            en_saledelry_type.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            en_saledelry_type.DataSource = ds.Tables[0];
            en_saledelry_type.DataBind();
            en_saledelry_type.Items.Insert(0, "----------เลือก----------");
        }

        protected void Employee()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from mt_emp", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            mt_emp.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            mt_emp.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            mt_emp.DataSource = ds.Tables[0];
            mt_emp.DataBind();
            mt_emp.Items.Insert(0, "----------เลือก----------");
        }

        protected void LoadTaxcalc()
        {
            NpgsqlCommand com = new NpgsqlCommand("select * from en_taxcalc", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbTaxcalc.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbTaxcalc.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbTaxcalc.DataSource = ds.Tables[0];
            cbbTaxcalc.DataBind();
            cbbTaxcalc.Items.Insert(0, "----------เลือก----------");
            cbbTaxcalc.SelectedValue = "TAXINC";
            cbbTaxcalc.Enabled = false;
            cbbTaxcalc.CssClass = "form-control";
        }

        protected void LoadStatus()
        {
            NpgsqlCommand com = new NpgsqlCommand("select * from en_txn_status", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbStatus.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbStatus.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbStatus.DataSource = ds.Tables[0];
            cbbStatus.DataBind();
            cbbStatus.Items.Insert(0, "----------เลือก----------");
        }

        protected void refreshdataT2()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM txn_so_line where parent_oid::text = @oid", conn);
            cmd.Parameters.AddWithValue("@oid", DBCompany.gSaleRepOid);
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            sda.Fill(ds);
            GridView6.DataSource = ds;
            GridView6.DataBind();
        }

        protected void GridView6_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView6.PageIndex = e.NewPageIndex;
            this.refreshdataT2();
        }
        protected void cbbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();
        }

        public void LoadItem()
        {
            try
            {
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT mt_name,mt_code FROM mt_item ORDER BY mt_name", conn);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();

                da.Fill(ds);
                cbbItem.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
                cbbItem.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();

                cbbItem.DataSource = ds.Tables[0];
                cbbItem.DataBind();
                cbbItem.Items.Insert(0, new ListItem("----------เลือก----------"));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void GetItem()
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(@"select mi.mt_name,mi.saleprice1,mi.disc1
                                                        from mt_item mi
                                                        where mi.mt_code = @mt_code", conn);
                cmd.Parameters.AddWithValue("@mt_code", cbbItem.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtItem_dest.Value = ds.Tables[0].Rows[0]["line_item_dest"].ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["saleprice1"].ToString();
                    if (en_saledelry_type.Text == "CUST")
                    {
                        Session["Custdiscount"] = ds.Tables[0].Rows[0]["disc1"].ToString();
                    }
                    //txtDisc1_price.Text
                    txtNetprice_amt.Text = ds.Tables[0].Rows[0]["saleprice1"].ToString();
                    //txtMemo.Value = ds.Tables[0].Columns["line_memo"].ToString();
                }

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (cbbItem.SelectedIndex > 0)
            {
                DataTable table = Session["dttableline"] as DataTable;
                DataRow drNew = table.NewRow();
                drNew["mt_name"] = cbbItem.SelectedItem;
                drNew["line_item_dest"] = txtItem_dest.Value;
                drNew["line_price"] = txtPrice.Text;
                drNew["line_disc1_price"] = txtDisc1_price.Text;
                drNew["line_disc2_price"] = en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0";
                drNew["line_qty"] = txtline_qty.Text;
                drNew["line_netprice_amt"] = txtNetprice_amt.Text;
                drNew["line_memo"] = txtMemo.Value;
                table.Rows.Add(drNew);

                RemoveNullColumnFromDataTable(table);
                Session["dttable"] = table;

                GridView6.DataSource = table;
                GridView6.DataBind();

                ClearOtherField();
            }
        }

        protected void line_qty_Change(object sender, EventArgs e)
        {
            txtNetprice_amt.Text = ((Convert.ToDouble(txtPrice.Text) - Convert.ToDouble(txtDisc1_price.Text)) * Convert.ToDouble(txtline_qty.Text)).ToString();
        }

        protected void dist_price_Change(object sender, EventArgs e)
        {
            txtNetprice_amt.Text = (Convert.ToDouble(txtPrice.Text) - Convert.ToDouble(txtDisc1_price.Text)).ToString();
        }

        public void ClearOtherField()
        {
            cbbItem.SelectedIndex = 0;
            txtItem_dest.Value = "";
            txtPrice.Text = "";
            txtDisc1_price.Text = "";
            en_saledelry_type.SelectedIndex = 0;
            txtline_qty.Text = "";
            txtNetprice_amt.Text = "";
            txtMemo.Value = "";
        }

    }
}