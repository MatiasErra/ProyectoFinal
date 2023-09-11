using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Paginas
{
    public partial class frmInicio : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
      // System.Web.HttpContext.Current.Session["ClienteIniciado"] = "1481022565";

            if (System.Web.HttpContext.Current.Session["ClienteIniciado"] != null)
            {
                this.MasterPageFile = "~/Master/MCliente.Master";

            }
            else
            {
                this.MasterPageFile = "~/Master/AGlobal.Master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}