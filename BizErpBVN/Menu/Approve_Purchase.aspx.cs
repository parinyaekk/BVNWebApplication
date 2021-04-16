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
    public partial class Approve_Purchase : System.Web.UI.Page
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                refreshGridT4();
            }
        }

        protected void refreshGridT4()
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT t1.*, t2.mt_name AS cust_name, e1.en_name AS sodepos_type_name FROM txn_so t1 LEFT JOIN mt_cust t2 ON t1.cust_oid = t2.oid LEFT JOIN en_sodepos_type e1 ON t1.sodepos_type = e1.en_code WHERE t1.txn_type = 'SOF' AND t1.txn_status = 'SOF_APPV02' AND t1.depos_amt >= sodepos_amt ORDER BY t1.txn_date, t1.txn_num, t1.add_time;", conn);
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();


                sda.Fill(ds);
                GridViewT4.DataSource = ds;
                GridViewT4.DataBind();


            }
            catch (Exception ex)
            {
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;

            }

            return;
        }

        protected void GridViewT4_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewT4.PageIndex = e.NewPageIndex;
            refreshGridT4();
        }

        protected void Approve(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                string oid = Convert.ToString(btn.CommandArgument);
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = @txn_status  where oid::text = @oid", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@txn_status", "SOF_APPV99");
                cmd.Parameters.AddWithValue("@oid", oid);
                cmd.ExecuteNonQuery();

                conn.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Approve Successfully')", true);

                refreshGridT4();

            }
            catch (Exception ex)
            {
                conn.Close();
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;
            }
        }

        protected void Reject(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    string oid = Convert.ToString(btn.CommandArgument);
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = @txn_status  where oid::text = @oid", conn);

                    conn.Open();
                    cmd.Parameters.AddWithValue("@txn_status", "CANCEL");
                    cmd.Parameters.AddWithValue("@oid", oid);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Reject Successfully')", true);

                    refreshGridT4();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;
            }
        }
    }
}