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
    public partial class Home : System.Web.UI.Page
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
                this.refreshdata1();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            var arr = e.CommandArgument.ToString().Split(';');
            conn.Close();

            if (arr[0] == "delete")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Guid ggcustid = Guid.Parse(arr[1].ToString());
                    NpgsqlCommand cmd = new NpgsqlCommand(@"DELETE FROM mt_cust WHERE oid = @oid", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@oid", ggcustid); //1
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Delete Record Customer Successfully.')", true);
                    //Response.Redirect("Home.aspx");
                    refreshdata1();
                }
            }
            if(arr[0] == "edit")
            {
                Response.Redirect("EditCustomer.aspx?ggid="+ arr[1].ToString());
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
                else
                {

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