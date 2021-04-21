using BizErpBVN.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizErpBVN.Menu
{
    public partial class Order : System.Web.UI.Page
    {
        DataTable dt;
        NpgsqlConnection conn = DBCompany.gCnnObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");
            }

            if (!Page.IsPostBack)
            {
                this.LoadDepartMent();
                this.CustomerGroup();
                this.Transportation();
                this.Employee();
                this.LoadTaxcalc();
                this.refreshdataT2();
                this.LoadStatus();
                this.LoadItem();
                this.Loadsodepos();
                createDataTable();
                DataRow drNew = dt.NewRow();
                dt.Rows.Add(drNew);
                Session["dttableline"] = dt;
                ClearOtherField();
                Session["btn_addr1"] = "";
                Session["btn_addr2"] = "";
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
        protected void line_qty_Change(object sender, EventArgs e)
        {
            txtNetprice_amt.Text = ((Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) ? "0" : txtPrice.Text) - Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) || txtDisc1_price.Text == "&nbsp;" ? "0" : txtDisc1_price.Text)) * Convert.ToDouble(String.IsNullOrEmpty(txtline_qty.Text) || txtline_qty.Text == "&nbsp;" ? "0" : txtline_qty.Text)).ToString();
        }
        protected void gotoHistory(Object sender, EventArgs e)
        {
            Response.Redirect("HistoryOrder.aspx");
        }
        protected void gotoOrder(Object sender, EventArgs e)
        {
            Response.Redirect("Order.aspx");
        }

        protected void SaveData(Object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                //call sp_getrun('SOF', '04-21-2021', true, 'test')
                string valmt_pymt = mt_pymt.Text; //oid
                string valtxn_date = txtDate.Value; //oid

                if (String.IsNullOrEmpty(txn_num.Text))
                {
                    NpgsqlCommand com0 = new NpgsqlCommand("call sp_getrun(@p_runcode, Date(@p_rundate), true, @p_string)", conn);
                    com0.Parameters.AddWithValue("@p_runcode", "SOF");
                    com0.Parameters.AddWithValue("@p_rundate", valtxn_date);
                    com0.Parameters.AddWithValue("@p_string", "test");
                    NpgsqlDataAdapter da0 = new NpgsqlDataAdapter(com0);
                    DataSet ds0 = new DataSet();
                    da0.Fill(ds0);
                    if (ds0.Tables[0].Rows.Count > 0)
                    {
                        txn_num.Text = ds0.Tables[0].Rows[0][1].ToString();
                    }
                }

                Guid ggpymtid = new Guid();
                NpgsqlCommand com2 = new NpgsqlCommand("select oid from mt_pymt where mt_code = @mt_code", conn);
                com2.Parameters.AddWithValue("@mt_code", valmt_pymt);
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(com2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ggpymtid = Guid.Parse(ds2.Tables[0].Rows[0]["oid"].ToString());
                }
                string valtax_num = tax_num.Text;
                string valcbbCustgrp = cbbCustgrp.Text;
                string valen_saledelry_type = en_saledelry_type.Text;
                var valmt_emp = Guid.Parse(DBCompany.gSaleRepOid.ToString());
                string valcbbTaxcalc = cbbTaxcalc.Text;
                string valaddr_text = txt_Addr1.Value;
                string valship_addr_text = txt_Addr2.Value;

                List<TempModel> lstModel = new List<TempModel>();
                foreach (GridViewRow row in GridView6.Rows)
                {
                    //mt_name
                    //line_item_dest
                    //line_price
                    //line_disc1_price
                    //line_disc2_price
                    //line_qty
                    //line_netprice_amt
                    //line_memo
                    var temp = new TempModel();
                    temp.mt_name = row.Cells[0].Text;
                    temp.line_item_dest = row.Cells[1].Text;
                    temp.line_price = row.Cells[2].Text;
                    temp.line_disc1_price = row.Cells[3].Text;
                    temp.line_disc2_price = row.Cells[4].Text;
                    temp.line_qty = row.Cells[5].Text;
                    temp.line_netprice_amt = row.Cells[6].Text;
                    temp.line_memo = row.Cells[7].Text;
                    lstModel.Add(temp);
                }

                string valen_sodepos_type = en_sodepos_type.Text;
                string valtxn_memo = txn_memo.Text;
                string valsodepos_amt = sodepos_amt.Text;
                string valdepos_amt = depos_amt.Text;
                string valdisc2_amt = disc2_amt.Text;
                string valdisc1_amt = disc1_amt.Text; 
                var valtax_rate = 7;
                string valtax_amt = tax_amt.Text;
                string valtxn_total = txn_total.Text;

                NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO txn_so 
                                                        (
                                                        txn_num, --1
                                                        txn_date, --2
                                                        txn_type, --3
                                                        txn_status, --4
                                                        itemcatgy_oid, --5
                                                        cust_oid, --6
                                                        addr_oid, --7 
                                                        addr_text, --8
                                                        addr_phn, --9
                                                        addr_fax, --10
                                                        ship_addr_oid, --11
                                                        ship_addr_text, --12
                                                        ship_addr_phn, --13
                                                        ship_addr_fax, --14
                                                        tax_num, --15
                                                        curr_oid, --16
                                                        pymt_oid, --17
                                                        saledelry_type, --18
                                                        taxcalc, --19
                                                        salerep_oid, --20
                                                        price_amt, --21
                                                        disc1_amt, --22
                                                        disc2_amt, --23
                                                        netprice_amt, --24
                                                        tax_rate, --25
                                                        tax_amt, --26
                                                        netprice_nontax, --27
                                                        txn_total, --28
                                                        sodepos_type, --29
                                                        sodepos_amt, --30
                                                        depos_amt, --31
                                                        outstn_amt, --32
                                                        --comt1_amt, --33
                                                        --comt2_amt, --34
                                                        txn_memo --35
                                                        )
                                                        VALUES
                                                        (
                                                        @txn_num, --1
                                                        @txn_date, --2
                                                        @txn_type, --3
                                                        @txn_status, --4
                                                        @itemcatgy_oid, --5
                                                        @cust_oid, --6
                                                        @addr_oid, --7 
                                                        @addr_text, --8
                                                        @addr_phn, --9
                                                        @addr_fax, --10
                                                        @ship_addr_oid, --11
                                                        @ship_addr_text, --12
                                                        @ship_addr_phn, --13
                                                        @ship_addr_fax, --14
                                                        @tax_num, --15
                                                        @curr_oid, --16
                                                        @pymt_oid, --17
                                                        @saledelry_type, --18
                                                        @taxcalc, --19
                                                        @salerep_oid, --20
                                                        @price_amt, --21
                                                        @disc1_amt, --22
                                                        @disc2_amt, --23
                                                        @netprice_amt, --24
                                                        @tax_rate, --25
                                                        @tax_amt, --26
                                                        @netprice_nontax, --27
                                                        @txn_total, --28
                                                        @sodepos_type, --29
                                                        @sodepos_amt, --30
                                                        @depos_amt, --31
                                                        @outstn_amt, --32
                                                        --@comt1_amt, --33
                                                        --@comt2_amt, --34
                                                        @txn_memo --35
                                                        )", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@txn_num", txn_num.Text); //1
                cmd.Parameters.AddWithValue("@txn_date", Convert.ToDateTime(valtxn_date)); //2
                cmd.Parameters.AddWithValue("@txn_type", "SOF"); //3
                cmd.Parameters.AddWithValue("@txn_status", "NEW"); //4
                cmd.Parameters.AddWithValue("@itemcatgy_oid", Guid.Parse("0a290710-4021-4277-b25d-853e9463cee7")); //5
                cmd.Parameters.AddWithValue("@cust_oid", Guid.Parse(valcbbCustgrp)); //6 
                cmd.Parameters.AddWithValue("@addr_oid", Guid.Parse(Session["btn_addr1"].ToString())); //7
                cmd.Parameters.AddWithValue("@addr_text", valaddr_text); //8
                cmd.Parameters.AddWithValue("@addr_phn", ""); //9
                cmd.Parameters.AddWithValue("@addr_fax", ""); //10
                cmd.Parameters.AddWithValue("@ship_addr_oid", Guid.Parse(Session["btn_addr2"].ToString())); //11
                cmd.Parameters.AddWithValue("@ship_addr_text", valship_addr_text); //12
                cmd.Parameters.AddWithValue("@ship_addr_phn", ""); //13
                cmd.Parameters.AddWithValue("@ship_addr_fax", ""); //14
                cmd.Parameters.AddWithValue("@tax_num", valtax_num); //15
                cmd.Parameters.AddWithValue("@curr_oid", Guid.Parse("281262b0-61e3-43bc-8c17-26184c7072fe")); //16
                cmd.Parameters.AddWithValue("@pymt_oid", ggpymtid); //17
                cmd.Parameters.AddWithValue("@saledelry_type", valen_saledelry_type == "----------เลือก----------" ? "" : valen_saledelry_type); //18
                cmd.Parameters.AddWithValue("@taxcalc", "TAXINC"); //19
                cmd.Parameters.AddWithValue("@salerep_oid", Guid.Parse(DBCompany.gSaleRepOid.ToString())); //20
                cmd.Parameters.AddWithValue("@price_amt", Convert.ToDouble(valtxn_total) + Convert.ToDouble(valdisc1_amt) + Convert.ToDouble(valdisc2_amt)); //21
                cmd.Parameters.AddWithValue("@disc1_amt", Convert.ToDouble(valdisc1_amt)); //22
                cmd.Parameters.AddWithValue("@disc2_amt", Convert.ToDouble(valdisc2_amt)); //23
                cmd.Parameters.AddWithValue("@netprice_amt", Convert.ToDouble(valtxn_total)); //24
                cmd.Parameters.AddWithValue("@tax_rate", 7); //25
                cmd.Parameters.AddWithValue("@tax_amt", Convert.ToDouble(valtax_amt)); //26
                cmd.Parameters.AddWithValue("@netprice_nontax", Convert.ToDouble(valtxn_total) - Convert.ToDouble(valtax_amt)); //27
                cmd.Parameters.AddWithValue("@txn_total", Convert.ToDouble(valtxn_total)); //28
                cmd.Parameters.AddWithValue("@sodepos_type", valen_sodepos_type); //29
                cmd.Parameters.AddWithValue("@sodepos_amt", Convert.ToDouble(valsodepos_amt)); //30
                cmd.Parameters.AddWithValue("@depos_amt", Convert.ToDouble(valdepos_amt)); //31
                cmd.Parameters.AddWithValue("@outstn_amt", Convert.ToDouble(valtxn_total) - Convert.ToDouble(valdepos_amt)); //32
                //cmd.Parameters.AddWithValue("@comt1_amt", Zipcode); //33
                //cmd.Parameters.AddWithValue("@comt2_amt", Zipcode); //34
                cmd.Parameters.AddWithValue("@txn_memo", valtxn_memo); //35
                //cmd.Parameters.AddWithValue("@add_oid", DBCompany.gSaleRepOid);
                cmd.ExecuteNonQuery();
                conn.Close();

                
                Guid ggtxn_soid = new Guid();
                NpgsqlCommand com3 = new NpgsqlCommand("select oid from txn_so where txn_num = @txn_num", conn);
                com3.Parameters.AddWithValue("@txn_num", txn_num.Text);
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(com3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    ggtxn_soid = Guid.Parse(ds3.Tables[0].Rows[0]["oid"].ToString());
                }
                int cnt = 1;
                foreach (var items in lstModel)
                {
                    Guid ggitemid = new Guid();
                    Guid ggunitid = new Guid();
                    NpgsqlCommand com4 = new NpgsqlCommand("select * from mt_item where mt_name = @mt_name", conn);
                    com4.Parameters.AddWithValue("@mt_name", items.mt_name);
                    NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(com4);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4);
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        ggitemid = Guid.Parse(ds4.Tables[0].Rows[0]["oid"].ToString());
                        ggunitid = Guid.Parse(ds4.Tables[0].Rows[0]["unt_oid"].ToString());
                    }


                    NpgsqlCommand cmd2 = new NpgsqlCommand(@"INSERT INTO txn_so_line 
                                                        (
                                                        parent_oid, --1
                                                        line_seq, --2
                                                        line_item_oid, --3
                                                        line_item_dest, --4
                                                        line_unt_oid, --5
                                                        line_price, --6
                                                        line_disc1_price, --7 
                                                        line_disc2_price, --8
                                                        line_disc_price, --9
                                                        line_qty, --10
                                                        line_price_amt, --11
                                                        line_disc_amt, --12
                                                        line_netprice_amt, --13
                                                        line_memo --14
                                                        ) 
                                                        VALUES
                                                        (
                                                        @parent_oid, --1
                                                        @line_seq, --2
                                                        @line_item_oid, --3
                                                        @line_item_dest, --4
                                                        @line_unt_oid, --5
                                                        @line_price, --6
                                                        @line_disc1_price, --7 
                                                        @line_disc2_price, --8
                                                        @line_disc_price, --9
                                                        @line_qty, --10
                                                        @line_price_amt, --11
                                                        @line_disc_amt, --12
                                                        @line_netprice_amt, --13
                                                        @line_memo --14
                                                        )", conn);
                    conn.Open();
                    cmd2.Parameters.AddWithValue("@parent_oid", ggtxn_soid); //1
                    cmd2.Parameters.AddWithValue("@line_seq", cnt); //2
                    cmd2.Parameters.AddWithValue("@line_item_oid", ggitemid); //3
                    cmd2.Parameters.AddWithValue("@line_item_dest", items.line_item_dest); //4
                    cmd2.Parameters.AddWithValue("@line_unt_oid", ggunitid); //5
                    cmd2.Parameters.AddWithValue("@line_price", Convert.ToDouble(items.line_price)); //6
                    cmd2.Parameters.AddWithValue("@line_disc1_price", Convert.ToDouble(String.IsNullOrEmpty(items.line_disc1_price) || items.line_disc1_price == "&nbsp;" ? "0" : items.line_disc1_price)); //7 
                    cmd2.Parameters.AddWithValue("@line_disc2_price", Convert.ToDouble(String.IsNullOrEmpty(items.line_disc2_price) || items.line_disc2_price == "&nbsp;" ? "0" : items.line_disc2_price)); //8
                    cmd2.Parameters.AddWithValue("@line_disc_price", Convert.ToDouble(String.IsNullOrEmpty(items.line_price) || items.line_price == "&nbsp;" ? "0" : items.line_price) - (Convert.ToDouble(String.IsNullOrEmpty(items.line_disc1_price) || items.line_disc1_price == "&nbsp;" ? "0" : items.line_disc1_price) + Convert.ToDouble(String.IsNullOrEmpty(items.line_disc2_price) || items.line_disc2_price == "&nbsp;" ? "0" : items.line_disc2_price))); //9
                    cmd2.Parameters.AddWithValue("@line_qty", Convert.ToInt32(String.IsNullOrEmpty(items.line_qty) || items.line_qty == "&nbsp;" ? "0" : items.line_qty)); //10
                    cmd2.Parameters.AddWithValue("@line_price_amt", Convert.ToDouble(String.IsNullOrEmpty(items.line_price) || items.line_price == "&nbsp;" ? "0" : items.line_price) * Convert.ToDouble(String.IsNullOrEmpty(items.line_qty) || items.line_qty == "&nbsp;" ? "0" : items.line_qty)); //11
                    cmd2.Parameters.AddWithValue("@line_disc_amt", Convert.ToDouble(String.IsNullOrEmpty(items.line_price) || items.line_price == "&nbsp;" ? "0" : items.line_price) * Convert.ToDouble(String.IsNullOrEmpty(items.line_qty) || items.line_qty == "&nbsp;" ? "0" : items.line_qty)); //12
                    cmd2.Parameters.AddWithValue("@line_netprice_amt", Convert.ToDouble(String.IsNullOrEmpty(items.line_netprice_amt) || items.line_netprice_amt == "&nbsp;" ? "0" : items.line_netprice_amt)); //13
                    cmd2.Parameters.AddWithValue("@line_memo", items.line_memo); //14
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    cnt++;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        protected void dist_price_Change(object sender, EventArgs e)
        {
            double discnt = Convert.ToDouble(String.IsNullOrEmpty(disc2_amt.Text) ? "0" : disc2_amt.Text);
            double tax = Convert.ToDouble(String.IsNullOrEmpty(tax_amt.Text) ? "0" : tax_amt.Text);
            double sum = Convert.ToDouble(String.IsNullOrEmpty(txn_total.Text) ? "0" : txn_total.Text);
            sum = sum - discnt;
            tax_amt.Text = ((sum * 7) / 107).ToString("F2");
            txn_total.Text = (sum).ToString();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (cbbItem.SelectedIndex > 0)
            {
                DataTable table = Session["dttableline"] as DataTable;
                DataRow drNew = table.NewRow();
                foreach (GridViewRow gvr in GvOrder.Rows)
                {
                    RadioButton rd = (RadioButton)gvr.FindControl("RadioButton1");
                    if (rd.Checked)
                    {
                        drNew["mt_name"] = rd.ToolTip;
                    }
                }

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
                Summary();
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
                valdisc1_amt += (Convert.ToDouble(String.IsNullOrEmpty(row.Cells[3].Text) || row.Cells[3].Text == "&nbsp;" ? "0" : row.Cells[3].Text) + Convert.ToDouble(String.IsNullOrEmpty(row.Cells[4].Text) ? "0" : row.Cells[4].Text)) * Convert.ToDouble(String.IsNullOrEmpty(row.Cells[5].Text) ? "0" : row.Cells[5].Text);
                valsum += Convert.ToDouble(String.IsNullOrEmpty(row.Cells[6].Text) ? "0" : row.Cells[6].Text);
            }

            disc1_amt.Text = valdisc1_amt.ToString();
            txn_total.Text = valsum.ToString();
            tax_amt.Text = ((valsum * 7)/ 107).ToString("F2");
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

        protected void LoadDepartMent()
        {
            NpgsqlCommand com = new NpgsqlCommand("select mt_name , mt_code from mt_pymt", conn);
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
            NpgsqlCommand com = new NpgsqlCommand("select mt_name,oid from mt_cust where mt_name <>'' order by mt_name", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbCustgrp.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbCustgrp.DataValueField = ds.Tables[0].Columns["oid"].ToString();
            cbbCustgrp.DataSource = ds.Tables[0];
            cbbCustgrp.DataBind();
            cbbCustgrp.Items.Insert(0, "----------เลือก----------");
        }

        protected void Transportation()
        {
            NpgsqlCommand com = new NpgsqlCommand("select en_name , en_code from en_saledelry_type", conn);
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
            NpgsqlCommand com = new NpgsqlCommand("select mt_name,mt_code from mt_emp where oid = @oid", conn);
            com.Parameters.AddWithValue("@oid", Guid.Parse(DBCompany.gSaleRepOid.ToString()));
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            mt_emp.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            mt_emp.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            mt_emp.DataSource = ds.Tables[0];
            mt_emp.DataBind();
            mt_emp.SelectedIndex = 0;
        }

        protected void LoadTaxcalc()
        {
            NpgsqlCommand com = new NpgsqlCommand("select en_name ,en_code from en_taxcalc", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbTaxcalc.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbTaxcalc.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbTaxcalc.DataSource = ds.Tables[0];
            cbbTaxcalc.DataBind();
            cbbTaxcalc.SelectedIndex = 0;
            cbbTaxcalc.Enabled = false;
            cbbTaxcalc.CssClass = "form-control";
        }

        protected void LoadStatus()
        {
            NpgsqlCommand com = new NpgsqlCommand("select en_name, en_code from en_txn_status", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbStatus.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbStatus.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbStatus.DataSource = ds.Tables[0];
            cbbStatus.DataBind();
            cbbStatus.Items.Insert(0, "----------เลือก----------");
            cbbStatus.SelectedIndex = 2;
            cbbStatus.Enabled = false;
            cbbStatus.CssClass = "form-control";
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

        public void LoadItem()
        {
            try
            {
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT mt_name,oid FROM mt_item ORDER BY mt_name", conn);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();

                da.Fill(ds);
                cbbItem.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
                cbbItem.DataValueField = ds.Tables[0].Columns["oid"].ToString();

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

                //NpgsqlCommand cmd = new NpgsqlCommand("select * from  txn_so_line tl inner join mt_item mi on tl.line_item_oid = mi.oid   where tl.parent_oid::text = @oid", conn);
                //cmd.Parameters.AddWithValue("@oid", cbbItem.SelectedValue);
                //NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //txtItem_dest.Value = ds.Tables[0].Rows[0]["line_item_dest"].ToString();
                //txtPrice.Text = ds.Tables[0].Rows[0]["line_price"].ToString();
                //txtDisc1_price.Text = ds.Tables[0].Rows[0]["line_disc1_price"].ToString();
                //txtUnt_oid.Text = ds.Tables[0].Columns["line_unt_oid"].ToString();
                //txtNetprice_amt.Text = ds.Tables[0].Columns["mt_code"].ToString();
                //txtMemo.Value = ds.Tables[0].Columns["line_memo"].ToString();





                NpgsqlCommand cmd = new NpgsqlCommand(@"select mi.mt_name, cast(mi.saleprice1 as decimal(10,2)) AS saleprice1 ,cast(mi.disc1 as decimal(10,2)) AS disc1 
                                                        from mt_item mi
                                                        where mi.oid::Text = @oid", conn);
                cmd.Parameters.AddWithValue("@oid", cbbItem.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtItem_dest.Value = ds.Tables[0].Rows[0]["line_item_dest"].ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["saleprice1"].ToString();
                    Session["Custdiscount"] = ds.Tables[0].Rows[0]["disc1"].ToString();
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


        protected void cbbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();

        }



        protected void LoadAddress()
        {
            try { 

            NpgsqlCommand cmd1 = new NpgsqlCommand("select addr_text,oid from mt_nameaddr where name_oid::text = @oid order by seq", conn);
            cmd1.Parameters.AddWithValue("@oid", cbbCustgrp.SelectedValue);
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();

                sda.Fill(ds1);
                GvOrder.DataSource = ds1;
                GvOrder.DataBind();
                //txt_Addr1.Value = ds1.Tables[0].Rows[0]["addr_text"].ToString();


            NpgsqlCommand cmd2 = new NpgsqlCommand("select addr_text,oid from mt_nameaddr where name_oid::text = @oid and seq<> 0 order by seq", conn);
            cmd2.Parameters.AddWithValue("@oid", cbbCustgrp.SelectedValue);
            NpgsqlDataAdapter sda1 = new NpgsqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();

                sda.Fill(ds2);
                GvOrder1.DataSource = ds2;
                GvOrder1.DataBind();

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void cbbCustgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbbCustgrp.SelectedValue
            NpgsqlCommand cmd = new NpgsqlCommand("select * from mt_cust where oid = @oid", conn);
            cmd.Parameters.AddWithValue("@oid", Guid.Parse(cbbCustgrp.SelectedValue));
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tax_num.Text = ds.Tables[0].Rows[0]["tax_num"].ToString();

                NpgsqlCommand com2 = new NpgsqlCommand("select mt_code from mt_pymt where oid::text = @oid", conn);
                com2.Parameters.AddWithValue("@oid", ds.Tables[0].Rows[0]["pymt_oid"]?.ToString());
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(com2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mt_pymt.Text = ds2.Tables[0].Rows[0]["mt_code"].ToString();
                }

                en_saledelry_type.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["saledelry_type"]?.ToString()) ? null : ds.Tables[0].Rows[0]["saledelry_type"].ToString(); 
            }


            LoadAddress();

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
        //[WebMethod]
        //public List<string> GetCustomerName(string en_name)
        //{

        //    List<string> CustomerNames = new List<string>();
        //    {
        //        NpgsqlConnection conn = DBCompany.gCnnObj;
        //        NpgsqlCommand cmd = new NpgsqlCommand("select en_name, en_code from en_txn_status like'%" + en_name + "%'", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        conn.Open();
        //        NpgsqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            CustomerNames.Add(rdr["en_name"].ToString());
        //        }
        //        return CustomerNames;


        //    }
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
  
                foreach (GridViewRow gvr in GvOrder.Rows)
                {
                    RadioButton rd = (RadioButton)gvr.FindControl("RadioButton1");
                    if (rd.Checked)
                    {

                        NpgsqlCommand cmd1 = new NpgsqlCommand("select addr_text, oid , phn1 , fax1 from mt_nameaddr where name_oid::text = @name_oid and oid::text = @oid", conn);
                       cmd1.Parameters.AddWithValue("@name_oid", cbbCustgrp.SelectedValue);
                       cmd1.Parameters.AddWithValue("@oid", rd.ToolTip);
                        NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();

                        sda.Fill(ds1);
                        if(ds1.Tables[0].Rows.Count > 0)
                        {
                            txt_Addr1.Value = ds1.Tables[0].Rows[0]["addr_text"].ToString();
                            Session["btn_addr1"] = rd.ToolTip;
                        }

                    }
    
                }

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow gvr in GvOrder1.Rows)
                {
                    RadioButton rd2 = (RadioButton)gvr.FindControl("RadioButton2");
                    if (rd2.Checked)
                    {

                        NpgsqlCommand cmd1 = new NpgsqlCommand("select addr_text,oid, phn1 , fax1 from mt_nameaddr where name_oid::text = @name_oid and seq<> 0 and oid::text = @oid order by seq", conn);
                        cmd1.Parameters.AddWithValue("@name_oid", cbbCustgrp.SelectedValue);
                        cmd1.Parameters.AddWithValue("@oid", rd2.ToolTip);
                        NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd1);
                        DataSet ds2 = new DataSet();

                        sda.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            txt_Addr2.Value = ds2.Tables[0].Rows[0]["addr_text"].ToString();
                            Session["btn_addr2"] = rd2.ToolTip;
                        }

                    }

                }

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }

        }
    }
}