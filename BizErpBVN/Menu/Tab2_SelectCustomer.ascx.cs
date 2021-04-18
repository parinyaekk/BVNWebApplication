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
    public partial class WebUserControl2 : System.Web.UI.UserControl
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
            //NpgsqlCommand cmd = new NpgsqlCommand("SELECT oid, mt_code, mt_name FROM mt_cust where oid::text = @oid", conn);
            //cmd.Parameters.AddWithValue("@oid", DBSys.gUsrOid
            DateTime dte = DateTime.Now.AddYears(-3);
            //NpgsqlCommand cmd = new NpgsqlCommand("SELECT oid, txn_date, txn_num, mt_name as cust_name, txn_total, txn_status_name FROM qtxn_so WHERE txn_date BETWEEN @pDATE1 AND @pDATE2 ORDER BY txn_date DESC,txn_num DESC", conn);
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM qtxn_so", conn);
            cmd.Parameters.AddWithValue("@pDATE1", DateTime.Now.ToString("yyyy/MM/dd"));
            cmd.Parameters.AddWithValue("@pDATE2", dte.ToString("yyyy/MM/dd"));

            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            sda.Fill(ds);
            GridView7.DataSource = ds;
            GridView7.DataBind();

        }


        protected void GridView1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            GridView7.PageIndex = e.NewPageIndex;
            this.refreshdata1();
        }



        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            txtSearh.Text = "";
        }

        //ค้นหาข้อมูล
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            NpgsqlCommand cmdSearch = new NpgsqlCommand("Select oid, mt_code, mt_name  from mt_cust where mt_code='" + txtSearh.Text + "' or mt_name ='" + txtSearh.Text + "' ", conn);
            DataTable dt = new DataTable();
            using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmdSearch))
            {
                if (txtSearh.Text != "")
                {
                    sda.Fill(dt);
                    GridView7.DataSource = dt;
                    GridView7.DataBind();


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
                    //conn.Open();
                    //cmd.CommandText = "DELETE FROM excludes WHERE word = @word";
                    //cmd.Parameters.AddWithValue("@word", word);
                    //cmd.ExecuteNonQuery();
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