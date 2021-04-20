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
                createDataTable();
                DataRow drNew = dt.NewRow();
                dt.Rows.Add(drNew);
                Session["dttableline"] = dt;
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
            txtNetprice_amt.Text = ((Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) ? "0" : txtPrice.Text) - Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) ? "0" : txtDisc1_price.Text)) * Convert.ToDouble(String.IsNullOrEmpty(txtline_qty.Text) ? "0" : txtline_qty.Text)).ToString();
        }

        protected void dist_price_Change(object sender, EventArgs e)
        {
            txtNetprice_amt.Text = (Convert.ToDouble(String.IsNullOrEmpty(txtPrice.Text) ? "0" : txtPrice.Text) - Convert.ToDouble(String.IsNullOrEmpty(txtDisc1_price.Text) ? "0" : txtDisc1_price.Text)).ToString();
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
        public static void RemoveNullColumnFromDataTable(DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][0] == DBNull.Value && dt.Rows[i][1] == DBNull.Value && dt.Rows[i][2] == DBNull.Value && dt.Rows[i][3] == DBNull.Value && dt.Rows[i][4] == DBNull.Value && dt.Rows[i][5] == DBNull.Value)
                    dt.Rows[i].Delete();
            }
            dt.AcceptChanges();
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
                NpgsqlCommand cmd = new NpgsqlCommand(@"select mi.mt_name,mi.saleprice1,mi.disc1
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
                txt_Addr1.Value = ds1.Tables[0].Rows[0]["addr_text"].ToString();


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
            LoadAddress();
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

                        NpgsqlCommand cmd1 = new NpgsqlCommand("select addr_text, oid from mt_nameaddr where name_oid::text = @name_oid and oid::text = @oid", conn);
                       cmd1.Parameters.AddWithValue("@name_oid", cbbCustgrp.SelectedValue);
                       cmd1.Parameters.AddWithValue("@oid", rd.Text);
                        NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();

                        sda.Fill(ds1);
                        if(ds1.Tables[0].Rows.Count > 0)
                        {
                            txt_Addr1.Value = ds1.Tables[0].Rows[0]["addr_text"].ToString();

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