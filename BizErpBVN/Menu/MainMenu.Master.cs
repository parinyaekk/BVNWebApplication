using BizErpBVN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizErpBVN.Menu
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(DBCompany.gSaleRepOid))
            {
                Response.Redirect("../");
            }
            agotoHome.ServerClick += new EventHandler(gotoHome);
            agotoApprove.ServerClick += new EventHandler(gotoApprove);
            agotoOrder.ServerClick += new EventHandler(gotoOrder);
            agotoConfirm.ServerClick += new EventHandler(gotoConfirm);
        }

        protected void gotoApprove(Object sender, EventArgs e)
        {
            Response.Redirect("Approve_Purchase.aspx");
        }
        protected void gotoOrder(Object sender, EventArgs e)
        {
            Response.Redirect("Order.aspx");
        }
        protected void gotoConfirm(Object sender, EventArgs e)
        {
            Response.Redirect("Confirm_Purchase.aspx");
        }
        protected void gotoHome(Object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}