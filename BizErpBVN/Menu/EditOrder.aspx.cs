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
                this.LoadDepartMent();
                this.CustomerGroup();
                this.Transportation();
                this.Employee();
                this.LoadTaxcalc();
                this.refreshdataT2();
                this.LoadStatus();
                getEditData(GGID);
            }
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
                }
            }
            catch (Exception ex)
            {

            }
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
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Action", string.Format("alert('{1}', '{0}');", "Cancel", Title), true);
        }
        protected void Submit(Object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Action", string.Format("alert('{1}', '{0}');", "Confirm", Title), true);
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

        public void GetItem()
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand("select * from  txn_so_line tl inner join mt_item mi on tl.line_item_oid = mi.oid   where mi.mt_code = @mt_code", conn);
                cmd.Parameters.AddWithValue("@mt_code", cbbItem.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                txtItem_dest.Value = ds.Tables[0].Rows[0]["line_item_dest"].ToString();
                txtPrice.Text = ds.Tables[0].Rows[0]["line_price"].ToString();
                txtDisc1_price.Text = ds.Tables[0].Rows[0]["line_disc1_price"].ToString();
                txtUnt_oid.Text = ds.Tables[0].Columns["line_unt_oid"].ToString();
                txtNetprice_amt.Text = ds.Tables[0].Columns["mt_code"].ToString();
                txtMemo.Value = ds.Tables[0].Columns["line_memo"].ToString();

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
    }
}