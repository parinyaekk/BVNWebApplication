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
    public partial class Confirm_Purchase : System.Web.UI.Page
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        Guid ggcustid = Guid.Parse(DBCompany.gSaleRepOid.ToString());
        public string myvar;

        public void SetMyVar(string i)
        {
            myvar = i;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");

            }

            if (!Page.IsPostBack)
            {
                Session["oid"] = "";
                Session["parent_oid"] = "";
                Session["parent_oid"] = Guid.Parse(Request.QueryString["parent_oid"]);
                this.refreshGrid5();
                this.LoadAcctCashin();

            }
        }

        protected void refreshGrid5()
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT *, mac.mt_name FROM txn_so_depos td inner join mt_acct_cashin mac on td.acct_cashin_oid = mac.oid WHERE parent_oid = @parent_oid order by txn_date ", conn);
                cmd.Parameters.AddWithValue("@parent_oid", Guid.Parse(Session["parent_oid"].ToString()));
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            var oid = btn.CommandArgument.ToString();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT td.oid,img_value,TO_CHAR(txn_date, 'YYYY-MM-DD hh12:mi:ss AM') as txn_date,td.txn_memo,cast(td.depos_amt as decimal(10,2)) AS depos_amt, mac.mt_name,mac.mt_code FROM txn_so_depos td inner join mt_acct_cashin mac on td.acct_cashin_oid = mac.oid WHERE td.oid = '" + oid + "' order by txn_date", conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            txtDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["txn_date"].ToString()).ToString("yyyy-MM-ddThh:mm");
            txtTxn_memo.Value = ds.Tables[0].Rows[0]["txn_memo"].ToString();
            txtDepos_amt.Text = ds.Tables[0].Rows[0]["depos_amt"].ToString();
            cbbAcct_cashin.DataTextField = ds.Tables[0].Columns["mt_name"].ToString();
            cbbAcct_cashin.DataValueField = ds.Tables[0].Columns["mt_code"].ToString();
            cbbAcct_cashin.DataSource = ds.Tables[0];
            cbbAcct_cashin.DataBind();
            Session["oid"] = oid;
        }

        protected void btnBack(object sender, EventArgs e)
        {
            Session["parent_oid"] = "";
            Response.Redirect("HistoryOrder.aspx");
        }


        protected void ClearData()
        {
            txtDate.Value = DateTime.Now.ToString("MM/dd/yyyy");
            txtTxn_memo.Value = "";
            txtDepos_amt.Text = "0";
            Session["oid"] = "";
            fileupload.Dispose();
            this.refreshGrid5();
        }



        protected void AddData(object sender, EventArgs e)
        {
            //260af79f-fcf8-4264-a3b8-d307036d60ac oid ของ txn_so
            conn.Close(); 
            string txn_so = "";
            string oid = Convert.ToString(Session["oid"]);
            string parent_oid = Convert.ToString(Session["parent_oid"]);
            Stream fs = fileupload.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            var Date = txtDate.Value;
            var Txn_memo = txtTxn_memo.Value;
            var Depos_amt = txtDepos_amt.Text;
            var Acct_cashin = cbbAcct_cashin.Text;
            Guid acct_cashin_oid = new Guid();
            NpgsqlCommand com = new NpgsqlCommand("select oid from mt_acct_cashin where mt_code = @mt_code", conn);
            com.Parameters.AddWithValue("@mt_code", Acct_cashin);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                acct_cashin_oid = Guid.Parse(ds.Tables[0].Rows[0]["oid"].ToString());
            }

            //NpgsqlCommand com2 = new NpgsqlCommand("select * from txn_so where oid = @oid", conn);
            //com2.Parameters.AddWithValue("@oid", Guid.Parse("260af79f-fcf8-4264-a3b8-d307036d60ac"));
            //NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(com2);
            //DataSet ds2 = new DataSet();
            //da2.Fill(ds2);
            //if (ds2.Tables[0].Rows.Count > 0)
            //{
            //    txn_so = ds2.Tables[0].Rows[0]["txn_num"].ToString();
            //}

            if (String.IsNullOrEmpty(oid)) //Add
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO txn_so_depos 
                                                    (
                                                    --txn_so, --0
                                                    txn_date, --1
                                                    txn_type, --2
                                                    txn_status, --3
                                                    parent_oid, --4
                                                    pymet, --5
                                                    depos_amt, --6
                                                    txn_memo, --7
                                                    img_value, --8
                                                    edit_seq, --9
                                                    acct_cashin_oid --10
                                                    ) VALUES 
                                                    (
                                                    --@txn_so, --0
                                                    @txn_date, --1
                                                    @txn_type, --2
                                                    @txn_status, --3
                                                    @parent_oid, --4
                                                    @pymet, --5
                                                    @depos_amt::DECIMAL, --6
                                                    @txn_memo, --7
                                                    @img_value, --8
                                                    @edit_seq, --9
                                                    @acct_cashin_oid --10
                                                    )", conn);
                //('SOFD','SOF_SUBMIT','','CASH',"
                conn.Open();
                //cmd.Parameters.AddWithValue("@txn_so", txn_so); //0
                cmd.Parameters.AddWithValue("@txn_date", Convert.ToDateTime(Date)); //1
                cmd.Parameters.AddWithValue("@txn_type", "SOFD"); //2
                cmd.Parameters.AddWithValue("@txn_status", "NEW"); //3
                cmd.Parameters.AddWithValue("@parent_oid", Guid.Parse(parent_oid)); //4
                cmd.Parameters.AddWithValue("@pymet", "CASH"); //5
                cmd.Parameters.AddWithValue("@depos_amt", Depos_amt); //6
                cmd.Parameters.AddWithValue("@txn_memo", Txn_memo); //7
                cmd.Parameters.AddWithValue("@img_value", bytes); //8
                cmd.Parameters.AddWithValue("@edit_seq", 0); //9
                cmd.Parameters.AddWithValue("@acct_cashin_oid", acct_cashin_oid); //10
                cmd.ExecuteNonQuery();
                conn.Close();

                ClearData();
            }
            else //Update
            {
                string query = @"UPDATE txn_so_depos SET --txn_so = @txn_so, --0
                                                    txn_date = @txn_date,--1
                                                    txn_type = @txn_type,  --2
                                                    txn_status = @txn_status, --3
                                                    parent_oid = @parent_oid, --4
                                                    pymet = @pymet, --5
                                                    depos_amt = @depos_amt::DECIMAL, --6
                                                    txn_memo = @txn_memo, 
                                                    ";
                                                    
                if (bytes.Length > 0)
                {
                    query += @"img_value = @img_value, --8 
                                ";
                }
                query += @"edit_seq = @edit_seq, --9
                            acct_cashin_oid = @acct_cashin_oid--10
                            WHERE oid = @oid";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                //('SOFD','SOF_SUBMIT','','CASH',"
                conn.Open();
                cmd.Parameters.AddWithValue("@oid", Guid.Parse(oid)); //0
                //cmd.Parameters.AddWithValue("@txn_so", txn_so); //0
                cmd.Parameters.AddWithValue("@txn_date", Convert.ToDateTime(Date)); //1
                cmd.Parameters.AddWithValue("@txn_type", "SOFD"); //2
                cmd.Parameters.AddWithValue("@txn_status", "NEW"); //3
                cmd.Parameters.AddWithValue("@parent_oid", Guid.Parse(parent_oid)); //4
                cmd.Parameters.AddWithValue("@pymet", "CASH"); //5
                cmd.Parameters.AddWithValue("@depos_amt", Depos_amt); //6
                cmd.Parameters.AddWithValue("@txn_memo", Txn_memo); //7
                cmd.Parameters.AddWithValue("@img_value", bytes); //8
                cmd.Parameters.AddWithValue("@edit_seq", 0); //9
                cmd.Parameters.AddWithValue("@acct_cashin_oid", acct_cashin_oid); //10
                cmd.ExecuteNonQuery();
                conn.Close();

                ClearData();
            }    
        }
    }
}