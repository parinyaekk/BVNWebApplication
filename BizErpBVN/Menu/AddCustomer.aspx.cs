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
    public partial class AddCustomer : System.Web.UI.Page
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        public int Zipcode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");
            }

            if (!Page.IsPostBack)
            {
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
                //drNew["contname"] = null;
                //drNew["street"] = null;
                //drNew["phn"] = null;
                //drNew["prov_code"] = null;
                //drNew["prov_txt"] = null;
                //drNew["amphur_code"] = null;
                //drNew["amphur_txt"] = null;
                //drNew["locat_code"] = null;
                //drNew["locat_txt"] = null;
                //drNew["zipcode"] = null;
                //drNew["email"] = null;
                //drNew["fax"] = null;
                dt.Rows.Add(drNew);
                //this.GridViewTdd1.DataSource = dt;
                //GridViewTdd1.DataBind();
                Session["dttable"] = dt;
                //DTable.Columns.Add("contname", System.Type.GetType("System.String"));
                //DTable.Columns.Add("street", System.Type.GetType("System.String"));
                //DTable.Columns.Add("addr1", System.Type.GetType("System.String"));
                //DTable.Columns.Add("phn1", System.Type.GetType("System.String"));
                //DTable.Columns.Add("phn2", System.Type.GetType("System.String"));
                //DTable.Columns.Add("prov_code", System.Type.GetType("System.String"));
                //DTable.Columns.Add("amphur_code", System.Type.GetType("System.String"));
                //DTable.Columns.Add("locat_code", System.Type.GetType("System.String"));
                //DTable.Columns.Add("zipcode", System.Type.GetType("System.String"));
                //DTable.Columns.Add("email", System.Type.GetType("System.String"));
                //DTable.Columns.Add("fax1", System.Type.GetType("System.String"));
                //DTable.Columns.Add("fax2", System.Type.GetType("System.String"));
            }
        }

        DataTable dt;
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

                NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO mt_cust 
                                                        (mt_code, --1
                                                        mt_name, --2
                                                        org_type, --3
                                                        saledelry_type, --4
                                                        cust_type, --5
                                                        salerep_oid, --6
                                                        reg_num, --7 
                                                        pymt_oid, --8
                                                        tax_num, --9
                                                        crlimit, --10
                                                        brn_num, --11
                                                        brn_name, --12
                                                        contname, --13
                                                        phn1, --14
                                                        street, --15
                                                        prov_code, --16
                                                        amphur_code, --17
                                                        locat_code, --18
                                                        zipcode, --19
                                                        prov_name, --20
                                                        amphur_name, --21
                                                        locat_name) --22
                                                        VALUES
                                                        (@mt_code, --1
                                                        @mt_name, --2
                                                        @org_type, --3
                                                        @saledelry_type, --4
                                                        @cust_type, --5
                                                        @salerep_oid, --6
                                                        @reg_num, --7 
                                                        @pymt_oid, --8
                                                        @tax_num, --9
                                                        @crlimit, --10
                                                        @brn_num, --11
                                                        @brn_name, --12
                                                        @contname, --13
                                                        @phn1, --14
                                                        @street, --15
                                                        @prov_code, --16
                                                        @amphur_code, --17
                                                        @locat_code, --18
                                                        @zipcode, --19
                                                        @prov_name, --20
                                                        @amphur_name, --21
                                                        @locat_name --22
                                                        )", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@mt_code", MtCode); //1
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
                foreach(var items in lstModel)
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('บันทึกข้อมูลสำเร็จ')", true);
                Response.Redirect("Home.aspx");
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
            drNew["prov_txt"] = cbbProvinceTh2.SelectedItem == null ? null : cbbProvinceTh2.SelectedItem.ToString();
            drNew["prov_code"] = cbbProvinceTh2.SelectedItem == null ? null : cbbProvinceTh2.SelectedItem.Value;
            drNew["amphur_txt"] = cbbAmperr2.SelectedItem == null ? null : cbbAmperr2.SelectedItem.ToString();
            drNew["amphur_code"] = cbbAmperr2.SelectedItem == null ? null : cbbAmperr2.SelectedItem.Value;
            drNew["locat_txt"] = cbbSubDistrict2.SelectedItem == null ? null : cbbSubDistrict2.SelectedItem.ToString();
            drNew["locat_code"] = cbbSubDistrict2.SelectedItem == null ? null : cbbSubDistrict2.SelectedItem.Value;
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


        private void AddNewRowToGrid()

        {
            ViewState["ButtonAdd"] = Session["ButtonAdd"];

            int rowIndex = 0;



            //if (ViewState["CurrentTable"] != null)
            //{
                
            //DataRow row = DTable.NewRow();
            //row["Column1"] = "test1";
            //row["Column2"] = "test2";
            //row["Column3"] = "test3";
            //DTable.Rows.Add(row);
            //GridViewTdd1.DataSource = DTable;
            //GridViewTdd1.DataBind();
            //if (dtCurrentTable.Rows.Count > 0)

            //{
            //    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

            //    {
            //        //extract the TextBox values
            //        TextBox box1 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
            //        TextBox box2 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
            //        TextBox box3 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
            //        TextBox box4 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[1].FindControl("TextBox4");
            //        TextBox box5 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[2].FindControl("TextBox5");
            //        drCurrentRow = dtCurrentTable.NewRow();

            //        //drCurrentRow["RowNumber"] = i + 1;
            //        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
            //        dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
            //        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
            //        dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
            //        dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;

            //        //rowIndex++;
            //    }

            //    dtCurrentTable.Rows.Add(drCurrentRow);
            //    ViewState["CurrentTable"] = dtCurrentTable;
            //    GridViewTdd1.DataSource = dtCurrentTable;
            //    GridViewTdd1.DataBind();

            //}
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //Set Previous Data on Postbacks
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
                        TextBox box1 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[1].FindControl("TextBox1");

                        TextBox box2 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[2].FindControl("TextBox2");

                        TextBox box3 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        TextBox box4 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[1].FindControl("TextBox4");

                        TextBox box5 = (TextBox)GridViewTdd1.Rows[rowIndex].Cells[2].FindControl("TextBox5");


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


    }
}