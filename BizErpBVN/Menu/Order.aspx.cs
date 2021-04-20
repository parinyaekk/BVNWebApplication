using BizErpBVN.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizErpBVN.Menu
{
    public partial class Order : System.Web.UI.Page
    {
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
                this.LoadAddress();

            }
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
            NpgsqlCommand com = new NpgsqlCommand("select mt_name,mt_code from mt_emp", conn);
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
            NpgsqlCommand com = new NpgsqlCommand("select en_name ,en_code from en_taxcalc", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  

            cbbTaxcalc.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbTaxcalc.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbTaxcalc.DataSource = ds.Tables[0];
            cbbTaxcalc.DataBind();
            cbbTaxcalc.Items.Insert(0, "----------เลือก----------");
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


        protected void cbbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();
        }



        protected void LoadAddress()
        {
            try { 

            NpgsqlCommand cmd1 = new NpgsqlCommand("select addr_text from mt_nameaddr where name_oid::text = @oid and seq<> 0 order by seq", conn);
            cmd1.Parameters.AddWithValue("@oid", cbbCustgrp.SelectedValue);
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();

            sda.Fill(ds1);
            GvOrder.DataSource = ds1;
            GvOrder.DataBind();


            NpgsqlCommand cmd2 = new NpgsqlCommand("select addr_text from mt_nameaddr where name_oid::text = @oid and seq<> 0 order by seq", conn);
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
    }
}