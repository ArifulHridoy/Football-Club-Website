using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class FixturesAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadFixtures();
            }
        }

        protected void btnAddFixture_Click(object sender, EventArgs e)
        {
            DateTime matchDate;
            if (!DateTime.TryParse(txtMatchDate.Text, out matchDate))
            {
                lblMessage.Text = "Please enter a valid date.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTeam1.Text) || string.IsNullOrWhiteSpace(txtTeam2.Text) || string.IsNullOrWhiteSpace(txtStadium.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            string statusText = string.IsNullOrWhiteSpace(txtStatus.Text) ? "Upcoming" : txtStatus.Text.Trim();
            string resultText = string.IsNullOrWhiteSpace(txtResult.Text) ? "0-0" : txtResult.Text.Trim();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO Fixtures(Team1,Team2,MatchDate,Stadium,Result,Status,HomeScore,AwayScore) VALUES(@Team1,@Team2,@MatchDate,@Stadium,@Result,@Status,@HomeScore,@AwayScore)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Team1", txtTeam1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Team2", txtTeam2.Text.Trim());
                    cmd.Parameters.AddWithValue("@MatchDate", matchDate);
                    cmd.Parameters.AddWithValue("@Stadium", txtStadium.Text.Trim());
                    cmd.Parameters.AddWithValue("@Result", resultText);
                    cmd.Parameters.AddWithValue("@Status", statusText);
                    cmd.Parameters.AddWithValue("@HomeScore", 0);
                    cmd.Parameters.AddWithValue("@AwayScore", 0);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtMatchDate.Text = string.Empty;
            txtTeam1.Text = string.Empty;
            txtTeam2.Text = string.Empty;
            txtStadium.Text = string.Empty;
            txtResult.Text = string.Empty;
            txtStatus.Text = string.Empty;
            lblMessage.Text = "Fixture added.";
            LoadFixtures();
        }

        private void LoadFixtures()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT FixtureId, Team1, Team2, MatchDate, Stadium, Result, Status FROM Fixtures ORDER BY MatchDate";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvFixtures.DataSource = dt;
                    gvFixtures.DataBind();
                }
            }
        }

        protected void gvFixtures_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvFixtures.EditIndex = e.NewEditIndex;
            LoadFixtures();
        }

        protected void gvFixtures_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvFixtures.EditIndex = -1;
            LoadFixtures();
        }

        protected void gvFixtures_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int fixtureId = Convert.ToInt32(gvFixtures.DataKeys[e.RowIndex].Value);
            string dateText = ((System.Web.UI.WebControls.TextBox)gvFixtures.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string team1 = ((System.Web.UI.WebControls.TextBox)gvFixtures.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string team2 = ((System.Web.UI.WebControls.TextBox)gvFixtures.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string stadium = ((System.Web.UI.WebControls.TextBox)gvFixtures.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string result = ((System.Web.UI.WebControls.TextBox)gvFixtures.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string status = ((System.Web.UI.WebControls.TextBox)gvFixtures.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            DateTime matchDate;
            DateTime.TryParse(dateText, out matchDate);

            string statusText = string.IsNullOrWhiteSpace(status) ? "Upcoming" : status.Trim();
            string resultText = string.IsNullOrWhiteSpace(result) ? "0-0" : result.Trim();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "UPDATE Fixtures SET Team1=@Team1, Team2=@Team2, MatchDate=@MatchDate, Stadium=@Stadium, Result=@Result, Status=@Status WHERE FixtureId=@FixtureId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Team1", team1.Trim());
                    cmd.Parameters.AddWithValue("@Team2", team2.Trim());
                    cmd.Parameters.AddWithValue("@MatchDate", matchDate);
                    cmd.Parameters.AddWithValue("@Stadium", stadium.Trim());
                    cmd.Parameters.AddWithValue("@Result", resultText);
                    cmd.Parameters.AddWithValue("@Status", statusText);
                    cmd.Parameters.AddWithValue("@FixtureId", fixtureId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            gvFixtures.EditIndex = -1;
            LoadFixtures();
        }

        protected void gvFixtures_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int fixtureId = Convert.ToInt32(gvFixtures.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "DELETE FROM Fixtures WHERE FixtureId=@FixtureId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadFixtures();
        }
    }
}
