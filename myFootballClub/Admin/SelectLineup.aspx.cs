using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace myFootballClub.Admin
{
    public partial class SelectLineup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }

            ddlFixture.AutoPostBack = true;
            ddlFixture.SelectedIndexChanged += ddlFixture_SelectedIndexChanged;

            if (!IsPostBack)
            {
                LoadFixtures();
                LoadPlayers();
                LoadLineup();
            }
        }

        private void LoadFixtures()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT FixtureId, Team1, Team2, MatchDate FROM Fixtures ORDER BY MatchDate", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlFixture.DataSource = dt;
                dt.Columns.Add("FixtureLabel", typeof(string), "Team1 + ' vs ' + Team2");
                ddlFixture.DataTextField = "FixtureLabel";
                ddlFixture.DataValueField = "FixtureId";
                ddlFixture.DataBind();
                ddlFixture.Items.Insert(0, new ListItem("Select fixture", string.Empty));
            }
        }

        private void LoadPlayers()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                bool hasFitness = false;
                bool hasInjury = false;
                using (SqlCommand schemaCmd = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Players' AND COLUMN_NAME IN ('FitnessStatus','InjuryStatus')", con))
                using (SqlDataReader reader = schemaCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string columnName = reader.GetString(0);
                        if (columnName.Equals("FitnessStatus", StringComparison.OrdinalIgnoreCase))
                        {
                            hasFitness = true;
                        }
                        if (columnName.Equals("InjuryStatus", StringComparison.OrdinalIgnoreCase))
                        {
                            hasInjury = true;
                        }
                    }
                }

                string query = "SELECT PlayerId, Name FROM Players";
                if (hasFitness && hasInjury)
                {
                    query += " WHERE (FitnessStatus IS NULL OR FitnessStatus = '' OR FitnessStatus = 'Fit') AND (InjuryStatus IS NULL OR InjuryStatus = '' OR InjuryStatus <> 'Injured')";
                }
                query += " ORDER BY Name";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlPlayer.DataSource = dt;
                    ddlPlayer.DataTextField = "Name";
                    ddlPlayer.DataValueField = "PlayerId";
                    ddlPlayer.DataBind();
                    ddlPlayer.Items.Insert(0, new ListItem("Select player", string.Empty));
                }
            }
        }

        private void LoadLineup()
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                gvLineup.DataSource = null;
                gvLineup.DataBind();
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT l.LineupId, p.Name AS PlayerName, l.Role FROM FixtureLineups l INNER JOIN Players p ON l.PlayerId = p.PlayerId WHERE l.FixtureId = @FixtureId ORDER BY l.Role, p.Name", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvLineup.DataSource = dt;
                    gvLineup.DataBind();
                }
            }
        }

        protected void btnAddLineup_Click(object sender, EventArgs e)
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                lblMessage.Text = "Select a fixture.";
                return;
            }

            int playerId;
            if (!int.TryParse(ddlPlayer.SelectedValue, out playerId))
            {
                lblMessage.Text = "Select a player.";
                return;
            }

            int startingCount = GetLineupCount(fixtureId, "Starting");
            int substituteCount = GetLineupCount(fixtureId, "Substitute");
            if (ddlRole.SelectedValue == "Starting" && startingCount >= 11)
            {
                lblMessage.Text = "Starting XI already has 11 players.";
                return;
            }
            if (ddlRole.SelectedValue == "Substitute" && substituteCount >= 10)
            {
                lblMessage.Text = "Substitute bench already has 10 players.";
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("INSERT INTO FixtureLineups(FixtureId, PlayerId, Role) VALUES(@FixtureId,@PlayerId,@Role)", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                cmd.Parameters.AddWithValue("@PlayerId", playerId);
                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            int appearances = ddlRole.SelectedValue == "Starting" ? 1 : 0;
            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("IF EXISTS (SELECT 1 FROM PlayerMatchStats WHERE FixtureId=@FixtureId AND PlayerId=@PlayerId) UPDATE PlayerMatchStats SET Appearances = CASE WHEN Appearances < @Appearances THEN @Appearances ELSE Appearances END WHERE FixtureId=@FixtureId AND PlayerId=@PlayerId ELSE INSERT INTO PlayerMatchStats(FixtureId, PlayerId, Appearances) VALUES(@FixtureId,@PlayerId,@Appearances)", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                cmd.Parameters.AddWithValue("@PlayerId", playerId);
                cmd.Parameters.AddWithValue("@Appearances", appearances);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Lineup updated.";
            LoadLineup();
        }

        protected void ddlFixture_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLineup();
        }

        protected void gvLineup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int lineupId = Convert.ToInt32(gvLineup.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("DELETE FROM FixtureLineups WHERE LineupId=@LineupId", con))
            {
                cmd.Parameters.AddWithValue("@LineupId", lineupId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadLineup();
        }

        private int GetLineupCount(int fixtureId, string role)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM FixtureLineups WHERE FixtureId=@FixtureId AND Role=@Role", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                cmd.Parameters.AddWithValue("@Role", role);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
