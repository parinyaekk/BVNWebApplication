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
    public partial class Tab5 : System.Web.UI.UserControl
    {
        NpgsqlConnection conn = DBCompany.gCnnObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                refreshGrid4();
            }
        }

        protected void refreshGrid4()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT oid,mt_code,mt_name FROM mt_car", conn);
            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            sda.Fill(ds);
            GridView5.DataSource = ds;
            GridView5.DataBind();

        }

        protected void GridView5_SelectedIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView5.PageIndex = e.NewPageIndex;
            refreshGrid4();
        }
    }
}
