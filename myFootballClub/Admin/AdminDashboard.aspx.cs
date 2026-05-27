using System;

namespace myFootballClub.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadCounts();
            }
        }

        private void LoadCounts()
        {
            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Players", con))
                {
                    lblPlayerCount.Text = cmd.ExecuteScalar().ToString();
                }

                using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM News", con))
                {
                    lblNewsCount.Text = cmd.ExecuteScalar().ToString();
                }
            }
        }
    }
}
