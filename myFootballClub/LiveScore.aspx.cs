using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class LiveScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadScores();
            }
        }

        private void LoadScores()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT Team1, Team2, MatchDate, Stadium, Status, HomeScore, AwayScore FROM Fixtures ORDER BY MatchDate";
                using (SqlCommand cmd = new SqlCommand(query, con))
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
                    rptLiveScores.DataSource = dt;
                    rptLiveScores.DataBind();
                }
            }
        }
    }
}
