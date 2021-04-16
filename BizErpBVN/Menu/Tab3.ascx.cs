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
    public partial class Tab3 : System.Web.UI.UserControl
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                this.refreshGrid5();
                this.refreshGrid8();
            }
        }

        protected void refreshGrid5()
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM txn_so WHERE txn_type = 'SOF' AND txn_status = 'SOF_APPV01' AND depos_amt >= sodepos_amt ORDER BY txn_date,txn_num,add_time", conn);
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                sda.Fill(ds);
                GridView5.DataSource = ds;
                GridView5.DataBind();

            }
            catch (Exception ex)
            {
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;

            }

            return;
        }

        protected void refreshGrid8()
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM txn_so WHERE txn_type = 'SOF' AND txn_status = 'SOF_APPV01' AND depos_amt >= sodepos_amt ORDER BY txn_date,txn_num,add_time", conn);
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                sda.Fill(ds);
                GridView8.DataSource = ds;
                GridView8.DataBind();

            }
            catch (Exception ex)
            {
                DBSys.gErrorResult = ex.HResult;
                DBSys.gErrorMsg = ex.Message;

            }

            return;
        }

        protected void GridView5_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView5.PageIndex = e.NewPageIndex;
            refreshGrid5();
        }

        protected void GridView8_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView8.PageIndex = e.NewPageIndex;
            refreshGrid8();
        }
    }
}