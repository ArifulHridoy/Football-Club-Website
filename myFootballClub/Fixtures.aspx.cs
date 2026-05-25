using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class Fixtures : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFixtures(null);
            }
        }

        protected void btnFixtureSearch_Click(object sender, EventArgs e)
        {
            LoadFixtures(txtFixtureSearch.Text.Trim());
        }

        protected void btnFixtureClear_Click(object sender, EventArgs e)
        {
            txtFixtureSearch.Text = string.Empty;
            LoadFixtures(null);
        }

        private void LoadFixtures(string search)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT MatchDate, Team1, Team2, Stadium, Result, Status, HomeScore, AwayScore FROM Fixtures";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query += " WHERE Team1 LIKE @Search OR Team2 LIKE @Search OR Stadium LIKE @Search";
                }
                query += " ORDER BY MatchDate";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (!dt.Columns.Contains("HomeScore"))
                        {
                            dt.Columns.Add("HomeScore", typeof(int));
                        }
                        if (!dt.Columns.Contains("AwayScore"))
                        {
                            dt.Columns.Add("AwayScore", typeof(int));
                        }
                        rptFixtures.DataSource = dt;
                        rptFixtures.DataBind();
                    }
                }
            }
        }
    }
}
