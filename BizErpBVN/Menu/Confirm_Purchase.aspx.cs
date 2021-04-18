using BizErpBVN.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace BizErpBVN.Menu
{
    public partial class Confirm_Purchase : System.Web.UI.Page
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        Guid ggcustid = Guid.Parse(DBCompany.gSaleRepOid.ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");

            }

            if (!Page.IsPostBack)
            {

                this.refreshGrid5();
                this.LoadAcctCashin();

            }
        }

        protected void refreshGrid5()
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT *, mac.mt_name FROM txn_so_depos td inner join mt_acct_cashin mac on td.acct_cashin_oid = mac.oid WHERE txn_status = 'SOF_SUBMIT' and parent_oid = @so_oid order by txn_date ", conn);
                cmd.Parameters.AddWithValue("@so_oid", ggcustid);
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
        protected void GridView5_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView5.PageIndex = e.NewPageIndex;
            refreshGrid5();
        }


        protected void LoadAcctCashin()
        {
            NpgsqlCommand com = new NpgsqlCommand("select mt_code, mt_name from mt_acct_cashin", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds);
            cbbAcct_cashin.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbAcct_cashin.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbAcct_cashin.DataSource = ds.Tables[0];
            cbbAcct_cashin.DataBind();
            cbbAcct_cashin.Items.Insert(0, new ListItem("----------เลือก----------"));
        }

        protected void btnSaves_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            var oid = btn.CommandArgument.ToString();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT TO_CHAR(txn_date, 'YYYY-MM-DD') as txn_date,td.txn_memo,td.depos_amt, mac.mt_name,mac.mt_code FROM txn_so_depos td inner join mt_acct_cashin mac on td.acct_cashin_oid = mac.oid WHERE td.oid = '" + oid + "' order by txn_date", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            txtDate.Value = ds.Tables[0].Rows[0]["txn_date"].ToString();
            txtTxn_memo.Value = ds.Tables[0].Rows[0]["txn_memo"].ToString();
            txtDepos_amt.Text = ds.Tables[0].Rows[0]["depos_amt"].ToString();
            cbbAcct_cashin.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbAcct_cashin.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbAcct_cashin.DataSource = ds.Tables[0];
            cbbAcct_cashin.DataBind();
        }
    }
}