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
                getEditData(GGID);
                this.LoadItem();
                mt_emp.Enabled = false;
                mt_emp.CssClass = "form-control";
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
            DataColumn dc9 = new DataColumn("itemoid");
            DataColumn dc10 = new DataColumn("lineoid");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            dt.Columns.Add(dc10);
        }

        protected void getEditData(Guid ggid)
        {
            try
            {
                createDataTable();
                DataRow drNew1 = dt.NewRow();
                dt.Rows.Add(drNew1);
                Session["dttableline"] = dt;

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
                    if(cbbStatus.Text == "NEW")
                    {
                        en_saledelry_type.Enabled = true;
                        en_saledelry_type.CssClass = "form-control";
                        GridView6.Columns[9].Visible = true;
                        AddItem.Visible = true;
                        UpdateItem.Visible = false;
                    }
                    else
                    {
                        GridView6.Columns[9].Visible = false;
                        en_saledelry_type.Enabled = false;
                        en_saledelry_type.CssClass = "form-control";
                        AddItem.Visible = false;
                        UpdateItem.Visible = false;
                    }
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


                    en_saledelry_type.SelectedValue = ds.Tables[0].Rows[0]["saledelry_type"].ToString();

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

                    NpgsqlCommand com5 = new NpgsqlCommand(@"select mi.mt_name,line_item_dest,line_price,line_disc1_price,line_disc2_price,line_qty,line_netprice_amt,line_memo,mi.oid as itemoid, tl.oid as lineoid
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
                            drNew["itemoid"] = row.ItemArray[8].ToString();
                            drNew["lineoid"] = row.ItemArray[9].ToString();
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
                    Session["Total"] = txn_total.Text;

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
                if (en_saledelry_type.SelectedIndex > 0)
                {

                    conn.Close();

                    string valmt_pymt = mt_pymt.Text; //oid
                    string valaddr_text = addr_text.Text;
                    string valship_addr_text = ship_addr_text.Text;
                    string valen_sodepos_type = en_sodepos_type.Text;
                    string valtxn_memo = txn_memo.Text;
                    string valsodepos_amt = sodepos_amt.Text;
                    string valdepos_amt = depos_amt.Text;
                    string valdisc1_amt = disc1_amt.Text;
                    string valdisc2_amt = disc2_amt.Text;
                    string valtax_amt = tax_amt.Text;
                    string valtxn_total = txn_total.Text;

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
                                        sodepos_amt = @sodepos_amt, --6 

                                        price_amt = @price_amt, --7
                                        disc1_amt = @disc1_amt, --8
                                        disc2_amt = @disc2_amt, --9
                                        netprice_amt = @netprice_amt, --10
                                        tax_rate = @tax_rate, --11
                                        tax_amt = @tax_amt, --12
                                        netprice_nontax = @netprice_nontax, --13
                                        txn_total = @txn_total, --14
                                        depos_amt = @depos_amt, --15
                                        outstn_amt = @outstn_amt --16

                                        WHERE oid = @oid";

                    //string valdepos_amt = depos_amt.Text;
                    //string valdisc1_amt = disc1_amt.Text;
                    //string valdisc2_amt = disc2_amt.Text;
                    //string valtax_amt = tax_amt.Text;
                    //string valtxn_total = txn_total.Text;
                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@oid", Guid.Parse(Session["parent_oid"].ToString())); //0
                    cmd.Parameters.AddWithValue("@pymt_oid", ggpymtid); //1
                    cmd.Parameters.AddWithValue("@addr_text", valaddr_text); //2
                    cmd.Parameters.AddWithValue("@ship_addr_text", valship_addr_text); //3
                    cmd.Parameters.AddWithValue("@sodepos_type", valen_sodepos_type); //4
                    cmd.Parameters.AddWithValue("@txn_memo", valtxn_memo); //5 
                    cmd.Parameters.AddWithValue("@sodepos_amt", Convert.ToDouble(valsodepos_amt)); //6

                    cmd.Parameters.AddWithValue("@price_amt", Convert.ToDouble(String.IsNullOrEmpty(valtxn_total) || valtxn_total == "&nbsp;" ? "0" : valtxn_total) + Convert.ToDouble(String.IsNullOrEmpty(valdisc1_amt) || valdisc1_amt == "&nbsp;" ? "0" : valdisc1_amt) + Convert.ToDouble(String.IsNullOrEmpty(valdisc2_amt) || valdisc2_amt == "&nbsp;" ? "0" : valdisc2_amt)); //7
                    cmd.Parameters.AddWithValue("@disc1_amt", Convert.ToDouble(String.IsNullOrEmpty(valdisc1_amt) || valdisc1_amt == "&nbsp;" ? "0" : valdisc1_amt)); //8
                    cmd.Parameters.AddWithValue("@disc2_amt", Convert.ToDouble(String.IsNullOrEmpty(valdisc2_amt) || valdisc2_amt == "&nbsp;" ? "0" : valdisc2_amt)); //9
                    cmd.Parameters.AddWithValue("@netprice_amt", Convert.ToDouble(String.IsNullOrEmpty(valtxn_total) || valtxn_total == "&nbsp;" ? "0" : valtxn_total)); //10
                    cmd.Parameters.AddWithValue("@tax_rate", 7); //11
                    cmd.Parameters.AddWithValue("@tax_amt", Convert.ToDouble(String.IsNullOrEmpty(valtax_amt) || valtax_amt == "&nbsp;" ? "0" : valtax_amt)); //12
                    cmd.Parameters.AddWithValue("@netprice_nontax", Convert.ToDouble(String.IsNullOrEmpty(valtxn_total) || valtxn_total == "&nbsp;" ? "0" : valtxn_total) - Convert.ToDouble(String.IsNullOrEmpty(valtax_amt) || valtax_amt == "&nbsp;" ? "0" : valtax_amt)); //13
                    cmd.Parameters.AddWithValue("@txn_total", Convert.ToDouble(String.IsNullOrEmpty(valtxn_total) || valtxn_total == "&nbsp;" ? "0" : valtxn_total)); //14
                    cmd.Parameters.AddWithValue("@depos_amt", Convert.ToDouble(String.IsNullOrEmpty(valdepos_amt) || valdepos_amt == "&nbsp;" ? "0" : valdepos_amt)); //15
                    cmd.Parameters.AddWithValue("@outstn_amt", Convert.ToDouble(String.IsNullOrEmpty(valtxn_total) || valtxn_total == "&nbsp;" ? "0" : valtxn_total) - Convert.ToDouble(String.IsNullOrEmpty(valdepos_amt)
                        || valdepos_amt == "&nbsp;" ? "0" : valdepos_amt)); //16
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Update ข้อมูลสำเร็จ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกประเภทการส่งสินค้า')", true);
                }
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
        protected void en_saledelry_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                if (en_saledelry_type.SelectedIndex > 0)
                { 

                    NpgsqlCommand cmd2 = new NpgsqlCommand(@"UPDATE txn_so SET
                                                saledelry_type = @saledelry_type 
                                                WHERE oid = @oid
                                                ", conn);
                    conn.Open();
                    cmd2.Parameters.AddWithValue("@oid", Guid.Parse(Request.QueryString["ggid"].ToString()));
                    cmd2.Parameters.AddWithValue("@saledelry_type", en_saledelry_type.Text);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    getEditData(Guid.Parse(Request.QueryString["ggid"].ToString()));

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกประเภทการส่งสินค้า')", true);
                }
            }
            catch(Exception ex)
            {

            }
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
            try
            {
                conn.Close();
                if (cbbItem.SelectedIndex > 0)
                {
                    Guid ggitemid = new Guid();
                    Guid ggunitid = new Guid();

                    NpgsqlCommand com4 = new NpgsqlCommand("select * from mt_item where mt_name = @mt_name", conn);
                    com4.Parameters.AddWithValue("@mt_name", cbbItem.SelectedItem.ToString());
                    NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(com4);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4);
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        ggitemid = Guid.Parse(ds4.Tables[0].Rows[0]["oid"].ToString());
                        ggunitid = Guid.Parse(ds4.Tables[0].Rows[0]["unt_oid"].ToString());
                    }

                    NpgsqlCommand cmd2 = new NpgsqlCommand(@"insert into txn_so_line 
                                                    (
                                                    line_item_oid, --1
                                                    line_item_dest, --2
                                                    line_unt_oid, --3
                                                    line_price, --4
                                                    line_disc1_price, --5
                                                    line_disc2_price, --6
                                                    line_disc_price, --7
                                                    line_qty, --8
                                                    line_price_amt, --9
                                                    line_disc_amt, --10
                                                    line_netprice_amt, --11
                                                    line_memo, --12
                                                    parent_oid --13
                                                    )
                                                    VALUES
                                                    (
                                                    @line_item_oid, --1
                                                    @line_item_dest, --2
                                                    @line_unt_oid, --3
                                                    @line_price, --4
                                                    @line_disc1_price, --5
                                                    @line_disc2_price, --6
                                                    @line_disc_price, --7
                                                    @line_qty, --8
                                                    @line_price_amt, --9
                                                    @line_disc_amt, --10
                                                    @line_netprice_amt, --11
                                                    @line_memo, --12
                                                    @parent_oid --13
                                                    )
                                                    ", conn);
                    conn.Open();
                    cmd2.Parameters.AddWithValue("@line_item_oid", ggitemid); //1
                    cmd2.Parameters.AddWithValue("@line_item_dest", txtItem_dest.Value.Replace("&nbsp;", "")); //2
                    cmd2.Parameters.AddWithValue("@line_unt_oid", ggunitid); //3
                    cmd2.Parameters.AddWithValue("@line_price", Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) || txtPrice.Text == "&nbsp;" ? "0" : txtPrice.Text)); //4
                    cmd2.Parameters.AddWithValue("@line_disc1_price", Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)); //5
                    cmd2.Parameters.AddWithValue("@line_disc2_price", Convert.ToDouble(en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0")); //6
                    cmd2.Parameters.AddWithValue("@line_disc_price", Convert.ToDouble(en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0") + Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)); //7
                    cmd2.Parameters.AddWithValue("@line_qty", Convert.ToInt32(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text.Split('.').ElementAt(0))); //8
                    cmd2.Parameters.AddWithValue("@line_price_amt", Convert.ToInt32(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text.Split('.').ElementAt(0)) * Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) || txtPrice.Text == "&nbsp;" ? "0" : txtPrice.Text)); //9
                    cmd2.Parameters.AddWithValue("@line_disc_amt", (Convert.ToDouble(en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0") + Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)) * Convert.ToInt32(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text.Split('.').ElementAt(0))); //10
                    cmd2.Parameters.AddWithValue("@line_netprice_amt", Convert.ToDouble(String.IsNullOrEmpty(txtNetprice_amt.Text) || txtNetprice_amt.Text == "&nbsp;" ? "0" : txtNetprice_amt.Text)); //11
                    cmd2.Parameters.AddWithValue("@line_memo", txtMemo.Value.Replace("&nbsp;", "")); //12
                    cmd2.Parameters.AddWithValue("@parent_oid", Guid.Parse(Request.QueryString["ggid"].ToString())); //13
                    cmd2.ExecuteNonQuery();
                    conn.Close();

                    getEditData(Guid.Parse(Request.QueryString["ggid"].ToString()));

                    DataTable table = Session["dttableline"] as DataTable;

                    GridView6.DataSource = table;
                    GridView6.DataBind();
                    ClearOtherField();
                    Summary();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void line_qty_Change(object sender, EventArgs e)
        {
            txtNetprice_amt.Text = ((Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) || txtPrice.Text == "&nbsp;" ? "0" : txtPrice.Text) - Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)) * Convert.ToDouble(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text)).ToString();
        }

        protected void dist_price_Change(object sender, EventArgs e)
        {
            double? total = Convert.ToDouble(Session["Total"].ToString());
            double discnt = Convert.ToDouble(String.IsNullOrEmpty(disc2_amt.Text) ? "0" : disc2_amt.Text);
            double tax = Convert.ToDouble(String.IsNullOrEmpty(tax_amt.Text) ? "0" : tax_amt.Text);
            double price = Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) ? "0" : txtDisc1_price.Text);
            double line_qty = Convert.ToDouble(String.IsNullOrEmpty(txtline_qty.Text) ? "0" : txtline_qty.Text);

            double sum = Convert.ToDouble(total == null ? 0 : total);
            sum = sum - discnt;
            tax_amt.Text = ((sum * 7) / 107).ToString("F2");
            txn_total.Text = (sum).ToString();
            disc1_amt.Text = (price * line_qty).ToString("F2");

        }

        public void ClearOtherField()
        {
            cbbItem.SelectedIndex = 0;
            txtItem_dest.Value = "";
            txtPrice.Text = "";
            txtDisc1_price.Text = "";
            txtline_qty.Text = "";
            txtNetprice_amt.Text = "";
            txtMemo.Value = "";
        }

        public class TempModel
        {
            public string mt_name { get; set; }
            public string line_item_dest { get; set; }
            public string line_price { get; set; }
            public string line_disc1_price { get; set; }
            public string line_disc2_price { get; set; }
            public string line_qty { get; set; }
            public string line_netprice_amt { get; set; }
            public string line_memo { get; set; }
            public Guid itemoid { get; set; }
            public Guid lineoid { get; set; }
        }

        public void Summary()
        {
            var valdisc1_amt = 0.00;
            var valsum = 0.00;
            List<TempModel> lstModel = new List<TempModel>();
            foreach (GridViewRow row in GridView6.Rows)
            {
                //var temp = new TempModel();
                //temp.mt_name = row.Cells[0].Text;
                //temp.line_item_dest = row.Cells[1].Text;
                //temp.line_price = row.Cells[2].Text;
                //temp.line_disc1_price = row.Cells[3].Text;
                //temp.line_disc2_price = row.Cells[4].Text;
                //temp.line_qty = row.Cells[5].Text;
                //temp.line_netprice_amt = row.Cells[6].Text;
                //temp.line_memo = row.Cells[7].Text;
                //lstModel.Add(temp);
                valdisc1_amt += (Convert.ToDouble(String.IsNullOrEmpty(row.Cells[5].Text) || row.Cells[5].Text == "&nbsp;" ? "0" : row.Cells[5].Text) + Convert.ToDouble(String.IsNullOrEmpty(row.Cells[6].Text) || row.Cells[6].Text == "&nbsp;" ? "0" : row.Cells[6].Text)) * Convert.ToDouble(String.IsNullOrEmpty(row.Cells[7].Text) || row.Cells[7].Text == "&nbsp;" ? "0" : row.Cells[7].Text);
                valsum += Convert.ToDouble(String.IsNullOrEmpty(row.Cells[8].Text) || row.Cells[8].Text == "&nbsp;" ? "0" : row.Cells[8].Text);
                }

            disc1_amt.Text = valdisc1_amt.ToString();
            txn_total.Text = valsum.ToString();
            tax_amt.Text = ((valsum * 7) / 107).ToString("F2");
            Session["Total"] = txn_total.Text;
        }


        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                if (!String.IsNullOrEmpty(Session["itemoid"].ToString()))
                {
                    Guid itemoid = Guid.Parse(Session["itemoid"].ToString());
                    Guid ggitemid = new Guid();
                    Guid ggunitid = new Guid();

                    string query = @"UPDATE txn_so_line SET
                                                    line_item_oid = @line_item_oid, --3
                                                    line_item_dest = @line_item_dest, --4
                                                    ";

                    if (cbbItem.SelectedIndex > 0)
                    {
                        NpgsqlCommand com4 = new NpgsqlCommand("select * from mt_item where mt_name = @mt_name", conn);
                        com4.Parameters.AddWithValue("@mt_name", cbbItem.SelectedItem.ToString());
                        NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(com4);
                        DataSet ds4 = new DataSet();
                        da4.Fill(ds4);
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            ggitemid = Guid.Parse(ds4.Tables[0].Rows[0]["oid"].ToString());
                            ggunitid = Guid.Parse(ds4.Tables[0].Rows[0]["unt_oid"].ToString());
                            query += @"             line_unt_oid = @line_unt_oid, --5
                                                    ";
                        }
                    }
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand(@"select mi.mt_name,mi.saleprice1,mi.disc1
                                                        from mt_item mi
                                                        where mi.oid = @oid", conn);
                        cmd.Parameters.AddWithValue("@oid", itemoid);
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (en_saledelry_type.Text == "CUST")
                            {
                                Session["Custdiscount"] = ds.Tables[0].Rows[0]["disc1"].ToString();
                            }
                        }
                    }



                    query += @"                     line_price = @line_price, --6
                                                    line_disc1_price = @line_disc1_price, --7 
                                                    line_disc2_price = @line_disc2_price, --8
                                                    line_disc_price = @line_disc_price, --9
                                                    line_qty = @line_qty, --10
                                                    line_price_amt = @line_price_amt, --11
                                                    line_disc_amt = @line_disc_amt, --12
                                                    line_netprice_amt = @line_netprice_amt, --13
                                                    line_memo = @line_memo --14
                                                    WHERE parent_oid = @parent_oid and  line_item_oid = @fkline_item_oid
                                                    ";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query, conn);
                    conn.Open();
                    cmd2.Parameters.AddWithValue("@parent_oid", Guid.Parse(Request.QueryString["ggid"].ToString()));
                    cmd2.Parameters.AddWithValue("@fkline_item_oid", itemoid);
                    cmd2.Parameters.AddWithValue("@line_item_oid", cbbItem.SelectedIndex > 0 ? ggitemid : itemoid); //1
                    cmd2.Parameters.AddWithValue("@line_unt_oid", ggunitid); //1
                    cmd2.Parameters.AddWithValue("@line_item_dest", txtItem_dest.Value.Replace("&nbsp;", "")); //4
                    cmd2.Parameters.AddWithValue("@line_price", Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) || txtPrice.Text == "&nbsp;" ? "0" : txtPrice.Text)); //6
                    cmd2.Parameters.AddWithValue("@line_disc1_price", Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)); //7 
                    cmd2.Parameters.AddWithValue("@line_disc2_price", Convert.ToDouble(en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0")); //8
                    cmd2.Parameters.AddWithValue("@line_disc_price", Convert.ToDouble(en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0") + Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)); //9
                    cmd2.Parameters.AddWithValue("@line_qty", Convert.ToInt32(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text.Split('.').ElementAt(0))); //10
                    cmd2.Parameters.AddWithValue("@line_price_amt", Convert.ToInt32(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text.Split('.').ElementAt(0)) * Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) || txtPrice.Text == "&nbsp;" ? "0" : txtPrice.Text)); //11
                    cmd2.Parameters.AddWithValue("@line_disc_amt", (Convert.ToDouble(en_saledelry_type.Text == "CUST" ? Session["Custdiscount"].ToString() : "0") + Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)) * Convert.ToInt32(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text.Split('.').ElementAt(0))); //12
                    cmd2.Parameters.AddWithValue("@line_netprice_amt", Convert.ToDouble(String.IsNullOrEmpty(txtNetprice_amt.Text) || txtNetprice_amt.Text == "&nbsp;" ? "0" : txtNetprice_amt.Text)); //13
                    cmd2.Parameters.AddWithValue("@line_memo", txtMemo.Value.Replace("&nbsp;", "")); //14
                    cmd2.ExecuteNonQuery();
                    conn.Close();

                    getEditData(Guid.Parse(Request.QueryString["ggid"].ToString()));

                    DataTable table = Session["dttableline"] as DataTable;

                    GridView6.DataSource = table;
                    GridView6.DataBind();

                    ClearOtherField();
                    Summary();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                Guid itemoid = Guid.Parse(e.CommandArgument.ToString());
                conn.Close();
                if (e.CommandName == "deleteitem" || e.CommandName == "edititem")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    if (e.CommandName == "deleteitem")
                    {
                        string confirmValue = Request.Form["confirm_value"];
                        if (confirmValue == "Yes")
                        {
                            NpgsqlCommand cmd2 = new NpgsqlCommand(@"DELETE FROM txn_so_line WHERE oid = @oid", conn);
                            conn.Open();
                            cmd2.Parameters.AddWithValue("@oid", itemoid);
                            cmd2.ExecuteNonQuery();
                            conn.Close();

                            getEditData(Guid.Parse(Request.QueryString["ggid"].ToString()));

                            DataTable table = Session["dttableline"] as DataTable;

                            GridView6.DataSource = table;
                            GridView6.DataBind();

                            ClearOtherField();
                            Summary();
                        }
                    }
                    if (e.CommandName == "edititem")
                    {
                        Session["itemoid"] = itemoid;
                        NpgsqlCommand com = new NpgsqlCommand(@"select mi.mt_name,line_item_dest,line_price,line_disc1_price,line_disc2_price,line_qty,line_netprice_amt,line_memo,mi.oid as itemoid
                                                            from txn_so_line tl 
                                                            JOIN mt_item mi ON tl.line_item_oid = mi.oid
                                                            where tl.parent_oid = @parent_oid and tl.line_item_oid = @line_item_oid ORDER BY tl.line_seq LIMIT 1", conn);
                        com.Parameters.AddWithValue("@parent_oid", Guid.Parse(Request.QueryString["ggid"].ToString()));
                        com.Parameters.AddWithValue("@line_item_oid", itemoid);
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            cbbItem.SelectedItem.Text = ds.Tables[0].Rows[0]["mt_name"].ToString();
                            txtItem_dest.Value = ds.Tables[0].Rows[0]["line_item_dest"].ToString().Replace("&nbsp;", "");
                            txtPrice.Text = ds.Tables[0].Rows[0]["line_price"].ToString().Replace("&nbsp;", "0");
                            txtDisc1_price.Text = ds.Tables[0].Rows[0]["line_disc1_price"].ToString().Replace("&nbsp;", "0");
                            txtline_qty.Text = ds.Tables[0].Rows[0]["line_qty"].ToString().Replace("&nbsp;", "0");
                            txtNetprice_amt.Text = ds.Tables[0].Rows[0]["line_netprice_amt"].ToString().Replace("&nbsp;", "0");
                            txtMemo.Value = ds.Tables[0].Rows[0]["line_memo"].ToString().Replace("&nbsp;", "");
                            AddItem.Visible = false;
                            UpdateItem.Visible = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}