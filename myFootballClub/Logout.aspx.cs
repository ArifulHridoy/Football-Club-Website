using System;

namespace myFootballClub
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["Email"] != null)
            {
                Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Request.Cookies["AuthUserId"] != null)
            {
                Response.Cookies["AuthUserId"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Request.Cookies["AuthRole"] != null)
            {
                Response.Cookies["AuthRole"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Request.Cookies["AuthName"] != null)
            {
                Response.Cookies["AuthName"].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Redirect("Default.aspx");
        }
    }
}
