using BizErpBVN.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizErpBVN.Menu
{
    public partial class MainMenu : System.Web.UI.Page
    {

        NpgsqlConnection conn = DBCompany.gCnnObj;

        protected void Page_Load(object sender, EventArgs e)

        {

            if (!Page.IsPostBack)
            {
                //refreshdata();
            }
        }

    }
}

      

