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
    public partial class EditCustomer : System.Web.UI.Page
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        DataTable dt;
        public int Zipcode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");
            }

            if (!Page.IsPostBack)
            {
                Guid GGID = Guid.Parse(Request.QueryString["ggid"]);
                Session["GGID"] = GGID;
                LoadDepartMent();
                LoadTransport();
                LoadCustomer();
                LoadSale();
                LoadPymt();
                LoadProvince();
                refreshdataT1_Addr();
                LoadProvince2();
                createDataTable();
                DataRow drNew = dt.NewRow();
                dt.Rows.Add(drNew);
                Session["dttable"] = dt;
                getEditData(GGID);
                getEditSubData(GGID);
            }
        }

        private void createDataTable()
        {
            dt = new DataTable();
            DataColumn dc1 = new DataColumn("contname");
            DataColumn dc2 = new DataColumn("street");
            DataColumn dc3 = new DataColumn("phn");
            DataColumn dc4 = new DataColumn("prov_code");
            DataColumn dc5 = new DataColumn("prov_txt");
            DataColumn dc6 = new DataColumn("amphur_code");
            DataColumn dc7 = new DataColumn("amphur_txt");
            DataColumn dc8 = new DataColumn("locat_code");
            DataColumn dc9 = new DataColumn("locat_txt");
            DataColumn dc10 = new DataColumn("zipcode");
            DataColumn dc11 = new DataColumn("email");
            DataColumn dc12 = new DataColumn("fax");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            dt.Columns.Add(dc10);
            dt.Columns.Add(dc11);
            dt.Columns.Add(dc12);
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

        public void GetZipCode()
        {
            try
            {
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT zipcode FROM sys_addr_locat WHERE sys_code = @sys_code ORDER BY zipcode", conn);
                sqCommand.Parameters.AddWithValue("@sys_code", cbbSubDistrict.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();


                da.Fill(ds);
                txtZipcode.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void cbbLocate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetZipCode();
        }
        public class TempModel
        {
            public string contname { get; set; }
            public string street { get; set; }
            public string phn { get; set; }
            public string prov_code { get; set; }
            public string prov_txt { get; set; }
            public string amphur_code { get; set; }
            public string amphur_txt { get; set; }
            public string locat_code { get; set; }
            public string locat_txt { get; set; }
            public string zipcode { get; set; }
            public string email { get; set; }
            public string fax { get; set; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string MtCode = txtMtCode.Text;
                string MtName = txtMtName.Text;
                string DepartMent = (cbbDepartMent.Text == null || cbbDepartMent.Text == "----------เลือก----------") ? "" : cbbDepartMent.Text;
                string Transport = (cbbTransport.Text == null || cbbTransport.Text == "----------เลือก----------") ? "" : cbbTransport.Text;
                string Customer = (cbbCustomer.Text == null || cbbCustomer.Text == "----------เลือก----------") ? "" : cbbCustomer.Text;
                string Sale = (cbbSale.Text == null || cbbSale.Text == "----------เลือก----------") ? "" : cbbSale.Text;
                string Reg = txtReg.Text;
                string Pymt = (cbbPymt.Text == null || cbbPymt.Text == "----------เลือก----------") ? "" : cbbPymt.Text;
                string TaxNum = txtTaxNum.Text;
                string conCrlimit = txtCrLimit.Text ?? "";
                double CrLimit = Convert.ToDouble(conCrlimit);
                string BrnNum = txtBrnNum.Text;
                string BrnName = txtBrnName.Text;
                string ContName = txtContName.Text;
                string Phn1 = txtPhn1.Text;
                string AddrText = txtAddr1.Text;
                string ProvinceTh = (cbbProvinceTh.Text == null || cbbProvinceTh.Text == "----------เลือก----------") ? "" : cbbProvinceTh.Text;
                string txtProvinceTh = (cbbProvinceTh.SelectedItem == null || cbbProvinceTh.SelectedItem.ToString() == "----------เลือก----------") ? "" : cbbProvinceTh.SelectedItem.Text;
                string Amperr = (cbbAmperr.Text == null || cbbAmperr.Text == "----------เลือก----------") ? "" : cbbAmperr.Text;
                string txtAmperr = (cbbAmperr.SelectedItem == null || cbbAmperr.SelectedItem.ToString() == "----------เลือก----------") ? "" : cbbAmperr.SelectedItem.Text;
                string SubDistrict = (cbbSubDistrict.Text == null || cbbSubDistrict.Text == "----------เลือก----------") ? "" : cbbSubDistrict.Text;
                string txtSubDistrict = (cbbSubDistrict.SelectedItem == null || cbbSubDistrict.SelectedItem.ToString() == "----------เลือก----------") ? "" : cbbSubDistrict.SelectedItem.Text;
                string Zipcode = txtZipcode.Text;
                conn.Close();

                List<TempModel> lstModel = new List<TempModel>();
                foreach(GridViewRow row in GridViewTdd1.Rows)
                {
                    
                    var temp = new TempModel();
                    temp.contname = row.Cells[0].Text;
                    temp.street = row.Cells[1].Text;
                    temp.phn = row.Cells[2].Text;
                    temp.prov_code = row.Cells[3].Text;
                    temp.prov_txt = row.Cells[4].Text;
                    temp.amphur_code = row.Cells[5].Text;
                    temp.amphur_txt = row.Cells[6].Text;
                    temp.locat_code = row.Cells[7].Text;
                    temp.locat_txt = row.Cells[8].Text;
                    temp.zipcode = row.Cells[9].Text;
                    temp.email = row.Cells[10].Text;
                    temp.fax = row.Cells[11].Text;
                    lstModel.Add(temp);
                }
                Guid ggpymtid = new Guid();
                Guid ggempid = new Guid();
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT oid FROM mt_pymt WHERE mt_code = @mt_code", conn);
                sqCommand.Parameters.AddWithValue("@mt_code", Pymt);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ggpymtid = Guid.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

                NpgsqlCommand sqCommand2 = new NpgsqlCommand("SELECT oid FROM mt_emp WHERE mt_code = @mt_code", conn);
                sqCommand2.Parameters.AddWithValue("@mt_code", Sale);
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sqCommand2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ggempid = Guid.Parse(ds2.Tables[0].Rows[0][0].ToString());
                }

                NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE mt_cust SET
                                                        mt_name = @mt_name, --2
                                                        org_type = @org_type, --3
                                                        saledelry_type = @saledelry_type, --4
                                                        cust_type = @cust_type, --5
                                                        salerep_oid = @salerep_oid, --6
                                                        reg_num = @reg_num, --7 
                                                        pymt_oid = @pymt_oid, --8
                                                        tax_num = @tax_num, --9
                                                        crlimit = @crlimit, --10
                                                        brn_num = @brn_num, --11
                                                        brn_name = @brn_name, --12
                                                        contname = @contname, --13
                                                        phn1 = @phn1, --14
                                                        street = @street, --15
                                                        prov_code = @prov_code, --16
                                                        amphur_code = @amphur_code, --17
                                                        locat_code = @locat_code, --18
                                                        zipcode = @zipcode, --19
                                                        prov_name = @prov_name, --20
                                                        amphur_name = @amphur_name, --21
                                                        locat_name = @locat_name --22
                                                        WHERE oid = @oid", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@oid", Guid.Parse(Session["GGID"].ToString())); //0
                //cmd.Parameters.AddWithValue("@mt_code", MtCode); //1
                cmd.Parameters.AddWithValue("@mt_name", MtName); //2
                cmd.Parameters.AddWithValue("@org_type", DepartMent); //3
                cmd.Parameters.AddWithValue("@saledelry_type", Transport); //4
                cmd.Parameters.AddWithValue("@cust_type", Customer); //5
                cmd.Parameters.AddWithValue("@salerep_oid", ggempid); //6 
                cmd.Parameters.AddWithValue("@reg_num", Reg); //7
                cmd.Parameters.AddWithValue("@pymt_oid", ggpymtid); //8
                cmd.Parameters.AddWithValue("@tax_num", TaxNum); //9
                cmd.Parameters.AddWithValue("@crlimit", CrLimit); //10
                cmd.Parameters.AddWithValue("@brn_num", BrnNum); //11
                cmd.Parameters.AddWithValue("@brn_name", BrnName); //12
                cmd.Parameters.AddWithValue("@contname", ContName); //13
                cmd.Parameters.AddWithValue("@phn1", Phn1); //14
                cmd.Parameters.AddWithValue("@street", AddrText); //15
                cmd.Parameters.AddWithValue("@prov_code", ProvinceTh ?? ""); //16
                cmd.Parameters.AddWithValue("@prov_name", txtProvinceTh); //17
                cmd.Parameters.AddWithValue("@amphur_code", Amperr); //18
                cmd.Parameters.AddWithValue("@amphur_name", txtAmperr); //19
                cmd.Parameters.AddWithValue("@locat_code", SubDistrict); //20
                cmd.Parameters.AddWithValue("@locat_name", txtSubDistrict); //21
                cmd.Parameters.AddWithValue("@zipcode", Zipcode); //22
                //cmd.Parameters.AddWithValue("@add_oid", DBCompany.gSaleRepOid);
                cmd.ExecuteNonQuery();
                conn.Close();

                Guid ggcustid = new Guid();
                NpgsqlCommand sqCommand3 = new NpgsqlCommand("SELECT oid FROM mt_cust WHERE mt_code = @mt_code", conn);
                sqCommand3.Parameters.AddWithValue("@mt_code", MtCode);
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sqCommand3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    ggcustid = Guid.Parse(ds3.Tables[0].Rows[0][0].ToString());
                }
                int cntData = 1;
                NpgsqlCommand cmd01 = new NpgsqlCommand(@"DELETE FROM mt_nameaddr WHERE name_oid = @oid", conn);
                conn.Open();
                cmd01.Parameters.AddWithValue("@oid", Guid.Parse(Session["GGID"].ToString()));
                cmd01.ExecuteNonQuery();
                conn.Close();
                foreach (var items in lstModel)
                {
                    NpgsqlCommand cmd2 = new NpgsqlCommand(@"INSERT INTO mt_nameaddr 
                                                        (name_oid, --1
                                                        seq, --2
                                                        contname, --3
                                                        addr1, --4
                                                        phn1, --5
                                                        fax1, --6
                                                        email, --7 
                                                        addr_text, --8
                                                        prov_code, --9
                                                        amphur_code, --10
                                                        locat_code, --11
                                                        zipcode, --12
                                                        prov_name, --13
                                                        amphur_name, --14
                                                        locat_name, --15
                                                        street) --16
                                                        VALUES
                                                        (@name_oid, --1
                                                        @seq, --2
                                                        @contname, --3
                                                        @addr1, --4
                                                        @phn1, --5
                                                        @fax1, --6
                                                        @email, --7 
                                                        @addr_text, --8
                                                        @prov_code, --9
                                                        @amphur_code, --10
                                                        @locat_code, --11
                                                        @zipcode, --12
                                                        @prov_name, --13
                                                        @amphur_name, --14
                                                        @locat_name, --15
                                                        @street --16
                                                        )", conn);
                    conn.Open();
                    cmd2.Parameters.AddWithValue("@name_oid", ggcustid); //1
                    cmd2.Parameters.AddWithValue("@seq", cntData); //2
                    cmd2.Parameters.AddWithValue("@contname", items.contname); //3
                    cmd2.Parameters.AddWithValue("@addr1", items.street); //4
                    cmd2.Parameters.AddWithValue("@phn1", items.phn); //5
                    cmd2.Parameters.AddWithValue("@fax1", items.fax); //6
                    cmd2.Parameters.AddWithValue("@email", items.email); //7 
                    cmd2.Parameters.AddWithValue("@addr_text", items.street); //8
                    cmd2.Parameters.AddWithValue("@prov_code", items.prov_code); //9
                    cmd2.Parameters.AddWithValue("@amphur_code", items.amphur_code); //10
                    cmd2.Parameters.AddWithValue("@locat_code", items.locat_code); //11
                    cmd2.Parameters.AddWithValue("@zipcode", items.zipcode); //12
                    cmd2.Parameters.AddWithValue("@prov_name", items.prov_txt); //13
                    cmd2.Parameters.AddWithValue("@amphur_name", items.amphur_txt); //14
                    cmd2.Parameters.AddWithValue("@locat_name", items.locat_txt); //15
                    cmd2.Parameters.AddWithValue("@street", items.street); //16
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    cntData++;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Record Customer Successfully.');window.location ='Home.aspx';", true);
            }
            catch (Exception ex)
            {
                conn.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error:Message", "alert('" + ex + "')", true);
            }
        }

        public void LoadProvince2()
        {
            try
            {

                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT sys_prov_code,prov_name FROM qsys_addr_locat ORDER BY sys_prov_code", conn);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();

                da.Fill(ds);
                cbbProvinceTh2.DataTextField = ds.Tables[0].Columns["prov_name"].ToString();
                cbbProvinceTh2.DataValueField = ds.Tables[0].Columns["sys_prov_code"].ToString();
                cbbProvinceTh2.DataSource = ds.Tables[0];
                cbbProvinceTh2.DataBind();
                cbbProvinceTh2.Items.Insert(0, new ListItem("----------เลือก----------"));

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void LoadDistrict2()
        {
            try
            {

                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT sys_amphur_code,amphur_name FROM qsys_addr_locat WHERE sys_prov_code = @sys_amphur_code ORDER BY sys_amphur_code", conn);
                sqCommand.Parameters.AddWithValue("@sys_amphur_code", cbbProvinceTh2.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();


                da.Fill(ds);
                cbbAmperr2.DataTextField = ds.Tables[0].Columns["amphur_name"].ToString();
                cbbAmperr2.DataValueField = ds.Tables[0].Columns["sys_amphur_code"].ToString();
                cbbAmperr2.DataSource = ds.Tables[0];
                cbbAmperr2.DataBind();
                cbbAmperr2.Items.Insert(0, new ListItem("----------เลือก----------"));

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void LoadSubDistrict2()
        {
            try
            {

                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT sys_code,locat_name,zipcode FROM qsys_addr_locat WHERE sys_amphur_code = @sys_code ORDER BY sys_code", conn);
                sqCommand.Parameters.AddWithValue("@sys_code", cbbAmperr2.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();


                da.Fill(ds);
                cbbSubDistrict2.DataTextField = ds.Tables[0].Columns["locat_name"].ToString();
                cbbSubDistrict2.DataValueField = ds.Tables[0].Columns["sys_code"].ToString();
                cbbSubDistrict2.DataSource = ds.Tables[0];
                cbbSubDistrict2.DataBind();
                cbbSubDistrict2.Items.Insert(0, new ListItem("----------เลือก----------"));

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void cbbProvinceTh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDistrict2();
        }

        protected void cbbAmperr2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubDistrict2();
        }

        public void GetZipCode2()
        {
            try
            {
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT DISTINCT zipcode FROM sys_addr_locat WHERE sys_code = @sys_code ORDER BY zipcode", conn);
                sqCommand.Parameters.AddWithValue("@sys_code", cbbSubDistrict2.SelectedValue);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();


                da.Fill(ds);
                txtZipCode2.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void cbbLocate2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetZipCode2();
        }

        protected void getEditData(Guid ggid)
        {
            try
            {
                
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT * FROM mt_cust WHERE oid = @oid", conn);
                sqCommand.Parameters.AddWithValue("@oid", ggid);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                txtMtCode.Text = ds.Tables[0].Rows[0]["mt_code"].ToString();
                txtMtName.Text = ds.Tables[0].Rows[0]["mt_name"].ToString();
                txtReg.Text = ds.Tables[0].Rows[0]["reg_num"].ToString();
                txtTaxNum.Text = ds.Tables[0].Rows[0]["tax_num"].ToString();
                txtCrLimit.Text = ds.Tables[0].Rows[0]["crlimit"].ToString();
                txtBrnNum.Text = ds.Tables[0].Rows[0]["brn_num"].ToString();
                txtBrnName.Text = ds.Tables[0].Rows[0]["brn_name"].ToString();
                txtContName.Text = ds.Tables[0].Rows[0]["contname"].ToString();
                txtPhn1.Text = ds.Tables[0].Rows[0]["phn1"].ToString();
                txtAddr1.Text = ds.Tables[0].Rows[0]["street"].ToString();
                txtZipcode.Text = ds.Tables[0].Rows[0]["zipcode"].ToString();
                cbbDepartMent.Text = ds.Tables[0].Rows[0]["org_type"].ToString();
                cbbTransport.Text = ds.Tables[0].Rows[0]["saledelry_type"].ToString();
                cbbCustomer.Text = ds.Tables[0].Rows[0]["cust_type"].ToString();

                NpgsqlCommand sqCommand2 = new NpgsqlCommand("SELECT mt_code FROM mt_emp WHERE oid::text = @oid", conn);
                sqCommand2.Parameters.AddWithValue("@oid", ds.Tables[0].Rows[0]["salerep_oid"].ToString());
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sqCommand2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
         
                    cbbSale.Text = ds2.Tables[0].Rows[0]["mt_code"].ToString();
                }

                NpgsqlCommand sqCommand3 = new NpgsqlCommand("SELECT mt_code FROM mt_pymt WHERE oid::text = @oid", conn);
                sqCommand3.Parameters.AddWithValue("@oid", ds.Tables[0].Rows[0]["pymt_oid"].ToString());
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sqCommand3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    cbbPymt.Text = ds3.Tables[0].Rows[0]["mt_code"].ToString(); 
                }

                cbbProvinceTh.Text = ds.Tables[0].Rows[0]["prov_code"].ToString();

                NpgsqlCommand sqCommand4 = new NpgsqlCommand("SELECT DISTINCT sys_amphur_code,amphur_name FROM qsys_addr_locat WHERE sys_prov_code = @sys_amphur_code ORDER BY sys_amphur_code", conn);
                sqCommand4.Parameters.AddWithValue("@sys_amphur_code", cbbProvinceTh.Text);
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sqCommand4);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4);
                cbbAmperr.DataTextField = ds4.Tables[0].Columns["amphur_name"].ToString();
                cbbAmperr.DataValueField = ds4.Tables[0].Columns["sys_amphur_code"].ToString();
                cbbAmperr.DataSource = ds4.Tables[0];
                cbbAmperr.DataBind();
                cbbAmperr.Items.Insert(0, new ListItem("----------เลือก----------"));

                cbbAmperr.Text = ds.Tables[0].Rows[0]["amphur_code"].ToString();

                NpgsqlCommand sqCommand5 = new NpgsqlCommand("SELECT DISTINCT sys_code,locat_name,zipcode FROM qsys_addr_locat WHERE sys_amphur_code = @sys_code ORDER BY sys_code", conn);
                sqCommand5.Parameters.AddWithValue("@sys_code", cbbAmperr.Text);
                NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sqCommand5);
                DataSet ds5 = new DataSet();

                da5.Fill(ds5);
                cbbSubDistrict.DataTextField = ds5.Tables[0].Columns["locat_name"].ToString();
                cbbSubDistrict.DataValueField = ds5.Tables[0].Columns["sys_code"].ToString();

                cbbSubDistrict.DataSource = ds5.Tables[0];
                cbbSubDistrict.DataBind();
                cbbSubDistrict.Items.Insert(0, new ListItem("----------เลือก----------"));

                cbbSubDistrict.Text = ds.Tables[0].Rows[0]["locat_code"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void getEditSubData(Guid ggid)
        {
            try
            {
                NpgsqlCommand sqCommand = new NpgsqlCommand("SELECT * FROM mt_nameaddr WHERE seq != 0 and name_oid = @oid order by seq", conn);
                sqCommand.Parameters.AddWithValue("@oid", ggid);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    DataTable table = Session["dttable"] as DataTable;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        DataRow drNew = table.NewRow();
                        drNew["contname"] = row.ItemArray[3].ToString();
                        drNew["street"] = row.ItemArray[21].ToString();
                        drNew["phn"] = row.ItemArray[8].ToString();
                        drNew["prov_txt"] = row.ItemArray[18].ToString();
                        drNew["prov_code"] = row.ItemArray[14].ToString();
                        drNew["amphur_txt"] = row.ItemArray[19].ToString();
                        drNew["amphur_code"] = row.ItemArray[15].ToString();
                        drNew["locat_txt"] = row.ItemArray[20].ToString();
                        drNew["locat_code"] = row.ItemArray[16].ToString();
                        drNew["zipcode"] = row.ItemArray[17].ToString();
                        drNew["email"] = row.ItemArray[12].ToString();
                        drNew["fax"] = row.ItemArray[10].ToString();
                        table.Rows.Add(drNew);
                    }
                    RemoveNullColumnFromDataTable(table);
                    Session["dttable"] = table;

                    GridViewTdd1.DataSource = table;
                    GridViewTdd1.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void refreshdataT1_Addr()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM mt_nameaddr where oid::text = @oid", conn);
            cmd.Parameters.AddWithValue("@oid", DBSys.gUsrOid);
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridViewTdd1.DataSource = ds;
            GridViewTdd1.DataBind();
        }

        protected void GridViewTdd1_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridViewTdd1.PageIndex = e.NewPageIndex;
            this.refreshdataT1_Addr();
        }

        public static void RemoveNullColumnFromDataTable(DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][0] == DBNull.Value && dt.Rows[i][1] == DBNull.Value && dt.Rows[i][2] == DBNull.Value && dt.Rows[i][9] == DBNull.Value && dt.Rows[i][10] == DBNull.Value && dt.Rows[i][11] == DBNull.Value)
                    dt.Rows[i].Delete();
            }
            dt.AcceptChanges();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextBox2.Text) && !String.IsNullOrEmpty(txtAddress.Text))
            {
                //contname 0
                //street 1
                //addr1 2
                //phn1 3
                //phn2 4
                //prov_code 5
                //amphur_code 6
                //locat_code 7
                //zipcode 8
                //email 9
                //fax1 10
                //fax2 11
                DataTable table = Session["dttable"] as DataTable;
                //foreach (GridViewRow gvr in GridViewTdd1.Rows)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["contname"] = gvr.Cells[0].Text;
                //    dr["street"] = gvr.Cells[1].Text;
                //    dr["addr1"] = gvr.Cells[2].Text;
                //    dr["phn1"] = gvr.Cells[3].Text;
                //    dr["phn2"] = gvr.Cells[4].Text;
                //    dr["prov_code"] = gvr.Cells[5].Text;
                //    dr["amphur_code"] = gvr.Cells[6].Text;
                //    dr["locat_code"] = gvr.Cells[7].Text;
                //    dr["zipcode"] = gvr.Cells[8].Text;
                //    dr["email"] = gvr.Cells[9].Text;
                //    dr["fax1"] = gvr.Cells[10].Text;
                //    dr["fax2"] = gvr.Cells[11].Text;
                //    dt.Rows.Add(dr);
                //}

                DataRow drNew = table.NewRow();
                drNew["contname"] = TextBox2.Text;
                drNew["street"] = txtAddress.Text;
                drNew["phn"] = TextBox3.Text;
                drNew["prov_txt"] = cbbProvinceTh2.SelectedItem == null ? null : cbbProvinceTh2.SelectedIndex > 0 ? cbbProvinceTh2.SelectedItem.ToString() : null;
                drNew["prov_code"] = cbbProvinceTh2.SelectedItem == null ? null : cbbProvinceTh2.SelectedIndex > 0 ?  cbbProvinceTh2.SelectedItem.Value : null;
                drNew["amphur_txt"] = cbbAmperr2.SelectedItem == null ? null : cbbAmperr2.SelectedIndex > 0 ? cbbAmperr2.SelectedItem.ToString() : null;
                drNew["amphur_code"] = cbbAmperr2.SelectedItem == null ? null : cbbAmperr2.SelectedIndex > 0 ? cbbAmperr2.SelectedItem.Value : null;
                drNew["locat_txt"] = cbbSubDistrict2.SelectedItem == null ? null : cbbSubDistrict2.SelectedIndex > 0 ? cbbSubDistrict2.SelectedItem.ToString() : null;
                drNew["locat_code"] = cbbSubDistrict2.SelectedItem == null ? null : cbbSubDistrict2.SelectedIndex > 0 ? cbbSubDistrict2.SelectedItem.Value : null;
                drNew["zipcode"] = txtZipCode2.Text;
                drNew["email"] = txtMail2.Text;
                drNew["fax"] = txtFax.Text;
                table.Rows.Add(drNew);

                RemoveNullColumnFromDataTable(table);
                Session["dttable"] = table;

                GridViewTdd1.DataSource = table;
                GridViewTdd1.DataBind();

                ClearOtherField();
            }
        }

        public void ClearOtherField()
        {
            TextBox2.Text = null;
            txtAddress.Text = null;
            TextBox3.Text = null;
            txtZipCode2.Text = null;
            txtMail2.Text = null;
            txtFax.Text = null;
            LoadProvince2();
            cbbAmperr2.DataSource = null;
            cbbAmperr2.DataBind();
            cbbSubDistrict2.DataSource = null;
            cbbSubDistrict2.DataBind();
        }
    }
}