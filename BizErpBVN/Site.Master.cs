using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BizErpBVN.Models;

namespace BizErpBVN
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string sUsr = UserName.Text.Trim();
            string sPwd = Pass.Text.Trim();

            if (String.IsNullOrEmpty(sUsr) || String.IsNullOrEmpty(sPwd))
            {
                return;
            }

            sUsr = sUsr.ToUpper();
            sPwd = SysUtils.GetComputeHash(sPwd);
            if (!DBSys.GetConnectDB())
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('ไม่สามารถเชื่อมต่อฐานข้อมูลระบบได้');", true);
                return;
            }

            string ip = SysUtils.GetIpAddr();
            if (!DBSys.GetLogin(sUsr, sPwd, ip))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('ไม่สามารถเข้าสู่ระบบได้เนื่องจากผู้ใช้และรหัสผ่านผิดพลาด');", true);
                return;
            }

            if (!DBSys.GetLogin(sUsr, sPwd, ip))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('ไม่สามารถเข้าสู่ระบบได้เนื่องจากผู้ใช้และรหัสผ่านผิดพลาด');", true);
                return;
            }

            if (DBCompany.GetConnectDB())
            {
                DBCompany.GetLoginSaleRep();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('ไม่สามารถเชื่อมต่อฐานข้อมูลบริษัทได้');", true);
                return;
            }
            DBSys.GetUsrLogin();

            //ฐานข้อมูลบริษัท
            if (DBCompany.GetConnectDB())
            {
                DBCompany.GetLoginSaleRep();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('ไม่สามารถเชื่อมต่อฐานข้อมูลบริษัทได้');", true);
                return;
            }

            Response.Redirect("~/Menu/Home.aspx");
        }
    }
}