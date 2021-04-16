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
    public partial class Tap1_Add : System.Web.UI.UserControl
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        public int Zipcode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDepartMent();
                LoadTransport();
                LoadCustomer();
                LoadSale();
                LoadPymt();
                LoadProvince();
                SetInitialRow();

            }
        }
        protected void LoadDepartMent()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from en_org_type", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbbDepartMent.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbDepartMent.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbDepartMent.DataSource = ds.Tables[0];
            cbbDepartMent.DataBind();
            cbbDepartMent.Items.Insert(0, new ListItem("----------เลือก----------"));

        }

        protected void LoadTransport()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from en_saledelry_type", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbbTransport.DataTextField = ds.Tables[0].Columns["en_name"].ToString();
            cbbTransport.DataValueField = ds.Tables[0].Columns["en_code"].ToString();
            cbbTransport.DataSource = ds.Tables[0];
            cbbTransport.DataBind();
            cbbTransport.Items.Insert(0, new ListItem("----------เลือก----------"));
        }


        protected void LoadCustomer()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from mt_custgrp", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbbCustomer.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbCustomer.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbCustomer.DataSource = ds.Tables[0];
            cbbCustomer.DataBind();
            cbbCustomer.Items.Insert(0, new ListItem("----------เลือก----------"));
        }

        protected void LoadSale()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from mt_emp", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbbSale.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbSale.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbSale.DataSource = ds.Tables[0];
            cbbSale.DataBind();
            cbbSale.Items.Insert(0, new ListItem("----------เลือก----------"));
        }

        protected void LoadPymt()
        {
            NpgsqlCommand com = new NpgsqlCommand("select *from mt_pymt", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds);
            cbbPymt.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbPymt.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbPymt.DataSource = ds.Tables[0];
            cbbPymt.DataBind();
            cbbPymt.Items.Insert(0, new ListItem("----------เลือก----------"));
        }

        public void LoadProvince()
        {
            try
            {

                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT sys_prov_code,prov_name FROM qsys_addr_locat ORDER BY sys_prov_code", conn);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();

                da.Fill(ds);
                cbbProvinceTh.DataTextField = ds.Tables[0].Columns["prov_name"].ToString();
                cbbProvinceTh.DataValueField = ds.Tables[0].Columns["sys_prov_code"].ToString();
                cbbProvinceTh.DataSource = ds.Tables[0];
                cbbProvinceTh.DataBind();
                cbbProvinceTh.Items.Insert(0, new ListItem("----------เลือก----------"));

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void LoadDistrict()
        {
            try
            {

                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT sys_amphur_code,amphur_name FROM qsys_addr_locat WHERE sys_prov_code = @sys_amphur_code ORDER BY sys_amphur_code", conn);
                sqCommand.Parameters.AddWithValue("@sys_amphur_code", cbbProvinceTh.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();


                da.Fill(ds);
                cbbAmperr.DataTextField = ds.Tables[0].Columns["amphur_name"].ToString();
                cbbAmperr.DataValueField = ds.Tables[0].Columns["sys_amphur_code"].ToString();
                cbbAmperr.DataSource = ds.Tables[0];
                cbbAmperr.DataBind();
                cbbAmperr.Items.Insert(0, new ListItem("----------เลือก----------"));

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void cbbProvinceTh_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadDistrict();

        }


        public void LoadSubDistrict()
        {
            try
            {

                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT sys_code,locat_name,zipcode FROM qsys_addr_locat WHERE sys_amphur_code = @sys_code ORDER BY sys_code", conn);
                sqCommand.Parameters.AddWithValue("@sys_code", cbbAmperr.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();


                da.Fill(ds);
                cbbSubDistrict.DataTextField = ds.Tables[0].Columns["locat_name"].ToString();
                cbbSubDistrict.DataValueField = ds.Tables[0].Columns["sys_code"].ToString();


                cbbSubDistrict.DataSource = ds.Tables[0];
                cbbSubDistrict.DataBind();
                cbbSubDistrict.Items.Insert(0, new ListItem("----------เลือก----------"));

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void cbbAmperr_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadSubDistrict();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }


        private void SetInitialRow()

        {

            DataTable dt = new DataTable();

            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Column1", typeof(string)));

            dt.Columns.Add(new DataColumn("Column2", typeof(string)));

            dt.Columns.Add(new DataColumn("Column3", typeof(string)));

            dt.Columns.Add(new DataColumn("Column4", typeof(string)));

            dt.Columns.Add(new DataColumn("Column5", typeof(string)));


            dr = dt.NewRow();

            dr["Column1"] = string.Empty;

            dr["Column2"] = string.Empty;

            dr["Column3"] = string.Empty;

            dr["Column4"] = string.Empty;

            dr["Column5"] = string.Empty;

            dt.Rows.Add(dr);

            //dr = dt.NewRow();



            //Store the DataTable in ViewState

            ViewState["CurrentTable"] = dt;

            Gridview2.DataSource = dt;

            Gridview2.DataBind();
        }

        private void AddNewRowToGrid()

        {
            ViewState["ButtonAdd"] = Session["ButtonAdd"];

            int rowIndex = 0;



            if (ViewState["CurrentTable"] != null)

            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)

                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox4");
                        TextBox box5 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox5");
                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;

                        //rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    Gridview2.DataSource = dtCurrentTable;
                    Gridview2.DataBind();

                }
            }

            else

            {
                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks
            SetPreviousData();
        }


        private void SetPreviousData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox1");

                        TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox2");

                        TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("TextBox4");

                        TextBox box5 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("TextBox5");


                        box1.Text = dt.Rows[i]["Column1"].ToString();

                        box2.Text = dt.Rows[i]["Column2"].ToString();

                        box3.Text = dt.Rows[i]["Column3"].ToString();


                        box4.Text = dt.Rows[i]["Column4"].ToString();

                        box5.Text = dt.Rows[i]["Column5"].ToString();

                        rowIndex++;

                    }

                }

            }

        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                AddNewRowToGrid();

            }

        }
    }
}