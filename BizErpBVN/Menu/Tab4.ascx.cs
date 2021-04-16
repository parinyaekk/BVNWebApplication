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
    [ParseChildren(false)]
    [PersistChildren(true)]
    public partial class Tab4 : System.Web.UI.UserControl
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

        //ยืนยันรายการ
        protected void Save(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                var test = btn.CommandArgument.ToString();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = 'SOF_APPV99'  where oid = '" + test + "'", conn);
                conn.Open();
                //cmd.Parameters.AddWithValue("@txn_status", "SOF_APPV99");
                //cmd.Parameters.AddWithValue("@oid", "d0cb4611-9737-45bd-bc3a-0c09829dff87");
                cmd.ExecuteNonQuery();

                conn.Close();

                

            }
            catch (Exception ex)
            {
                conn.Close();
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;

            }

            return;
        }

        protected void test(object sender, EventArgs e)
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = 'CANCEL'  where oid = @oid ", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@txn_status", "CANCEL'");
                cmd.Parameters.AddWithValue("@oid", DBCompany.gSaleRepOid);
                cmd.ExecuteNonQuery();

                conn.Close();


            }
            catch (Exception ex)

            {

                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;

            }

            return;
        }


        //ยกเลิกรายการ
        protected void btnCancel_Click(object sender, EventArgs e)
        {


            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE txn_so SET txn_status = @txn_status  where oid = @oid ", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@txn_status", "CANCEL'");
            cmd.Parameters.AddWithValue("@oid", DBCompany.gSaleRepOid);
            cmd.ExecuteNonQuery();

            conn.Close();

            refreshGridT4();


        }

    }
}