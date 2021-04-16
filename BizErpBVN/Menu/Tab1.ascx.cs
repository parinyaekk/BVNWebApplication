using BizErpBVN.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizErpBVN
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                this.refreshdata1();
            }

        }

        protected void refreshdata1()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT oid, mt_code, mt_name FROM mt_cust where salerep_oid::text = @salerep_oid", conn);
            cmd.Parameters.AddWithValue("@salerep_oid", DBCompany.gSaleRepOid);
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            if (ds != null)
            {
                sda.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }

            return;

        }

    
        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            this.refreshdata1();
        }


       
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            txtSearh.Text = "";
        }

        //ค้นหาข้อมูล
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            NpgsqlCommand cmdSearch = new NpgsqlCommand("Select oid, mt_code, mt_name  from mt_cust where mt_code like'%" + txtSearh.Text.Trim() + "%' or mt_name like'%" + txtSearh.Text.Trim() + "%' ", conn);
            DataTable dt = new DataTable();
            using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmdSearch))
            {
                if (txtSearh.Text != "")
                {
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                }
                else {

                    refreshdata1();

                }

            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {

                using (var cmd = conn.CreateCommand())
                {

                }
            }

            catch (Exception ex)
            {
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;
            }
        }
    }
}