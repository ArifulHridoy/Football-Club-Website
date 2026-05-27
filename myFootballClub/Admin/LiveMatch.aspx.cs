using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace myFootballClub.Admin
{
    public partial class LiveMatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadFixtures();
                LoadPlayers();
                LoadLineup();
                LoadGoalEvents();
            }
        }

        private void LoadMatchState()
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                txtHomeScore.Text = string.Empty;
                txtAwayScore.Text = string.Empty;
                ddlStatus.SelectedIndex = 0;
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT Status, HomeScore, AwayScore FROM Fixtures WHERE FixtureId=@FixtureId", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtHomeScore.Text = reader["HomeScore"].ToString();
                        txtAwayScore.Text = reader["AwayScore"].ToString();
                        string status = reader["Status"].ToString();
                        ListItem item = ddlStatus.Items.FindByValue(status);
                        if (item != null)
                        {
                            ddlStatus.ClearSelection();
                            item.Selected = true;
                        }
                    }
                }
            }
        }

        private void LoadGoalEvents()
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                gvGoalEvents.DataSource = null;
                gvGoalEvents.DataBind();
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"SELECT e.Minute, p.Name AS PlayerName, ap.Name AS AssistName
FROM MatchEvents e
INNER JOIN Players p ON e.PlayerId = p.PlayerId
LEFT JOIN Players ap ON e.AssistPlayerId = ap.PlayerId
WHERE e.FixtureId = @FixtureId AND e.EventType = 'Goal'
ORDER BY e.Minute", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvGoalEvents.DataSource = dt;
                    gvGoalEvents.DataBind();
                }
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
                    query += " WHERE (FitnessStatus IS NULL OR FitnessStatus = 'Fit') AND (InjuryStatus IS NULL OR InjuryStatus = 'Available')";
                }
                query += " ORDER BY Name";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlScorer.DataSource = dt;
                    ddlScorer.DataTextField = "Name";
                    ddlScorer.DataValueField = "PlayerId";
                    ddlScorer.DataBind();
                    ddlScorer.Items.Insert(0, new ListItem("Select scorer", string.Empty));

                    ddlAssist.DataSource = dt;
                    ddlAssist.DataTextField = "Name";
                    ddlAssist.DataValueField = "PlayerId";
                    ddlAssist.DataBind();
                    ddlAssist.Items.Insert(0, new ListItem("Select assist", string.Empty));

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

        private void UpdateFixtureScore(int fixtureId, string scoreSide)
        {
            string scoreColumn = scoreSide == "Away" ? "AwayScore" : "HomeScore";

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand($"UPDATE Fixtures SET {scoreColumn} = {scoreColumn} + 1, Result = CAST(HomeScore AS NVARCHAR(10)) + '-' + CAST(AwayScore AS NVARCHAR(10)) WHERE FixtureId=@FixtureId", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnUpdateMatch_Click(object sender, EventArgs e)
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                lblMatchMessage.Text = "Select a fixture.";
                return;
            }

            int homeScore;
            int awayScore;
            int.TryParse(txtHomeScore.Text, out homeScore);
            int.TryParse(txtAwayScore.Text, out awayScore);

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("UPDATE Fixtures SET Status=@Status, HomeScore=@HomeScore, AwayScore=@AwayScore, Result=@Result WHERE FixtureId=@FixtureId", con))
            {
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@HomeScore", homeScore);
                cmd.Parameters.AddWithValue("@AwayScore", awayScore);
                cmd.Parameters.AddWithValue("@Result", string.Format("{0}-{1}", homeScore, awayScore));
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMatchMessage.Text = "Match updated.";
        }

        protected void btnAddGoal_Click(object sender, EventArgs e)
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                lblGoalMessage.Text = "Select a fixture.";
                return;
            }

            int scorerId;
            if (!int.TryParse(ddlScorer.SelectedValue, out scorerId))
            {
                lblGoalMessage.Text = "Select a scorer.";
                return;
            }

            int assistId;
            int.TryParse(ddlAssist.SelectedValue, out assistId);

            int minute;
            int.TryParse(txtMinute.Text, out minute);

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO MatchEvents(FixtureId, PlayerId, AssistPlayerId, Minute, EventType) VALUES(@FixtureId,@PlayerId,@AssistPlayerId,@Minute,'Goal')", con))
                {
                    cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                    cmd.Parameters.AddWithValue("@PlayerId", scorerId);
                    cmd.Parameters.AddWithValue("@AssistPlayerId", assistId == 0 ? (object)DBNull.Value : assistId);
                    cmd.Parameters.AddWithValue("@Minute", minute);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE PlayerMatchStats SET Goals = Goals + 1, Appearances = CASE WHEN Appearances < 1 THEN 1 ELSE Appearances END WHERE FixtureId=@FixtureId AND PlayerId=@PlayerId", con))
                {
                    cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                    cmd.Parameters.AddWithValue("@PlayerId", scorerId);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        cmd.CommandText = "INSERT INTO PlayerMatchStats(FixtureId, PlayerId, Goals, Appearances) VALUES(@FixtureId,@PlayerId,1,1)";
                        cmd.ExecuteNonQuery();
                    }
                }

                if (assistId > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE PlayerMatchStats SET Assists = Assists + 1, Appearances = CASE WHEN Appearances < 1 THEN 1 ELSE Appearances END WHERE FixtureId=@FixtureId AND PlayerId=@PlayerId", con))
                    {
                        cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                        cmd.Parameters.AddWithValue("@PlayerId", assistId);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText = "INSERT INTO PlayerMatchStats(FixtureId, PlayerId, Assists, Appearances) VALUES(@FixtureId,@PlayerId,1,1)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            UpdateFixtureScore(fixtureId, ddlScoreSide.SelectedValue);

            lblGoalMessage.Text = "Goal saved.";
            txtMinute.Text = string.Empty;

            UpdatePlayerTotalsForFixture(fixtureId);
            LoadGoalEvents();
        }

        protected void ddlFixture_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchState();
            LoadLineup();
            LoadGoalEvents();
        }

        protected void btnAddLineup_Click(object sender, EventArgs e)
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                lblLineupMessage.Text = "Select a fixture.";
                return;
            }

            int playerId;
            if (!int.TryParse(ddlPlayer.SelectedValue, out playerId))
            {
                lblLineupMessage.Text = "Select a player.";
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

            lblLineupMessage.Text = "Lineup updated.";
            LoadLineup();

            UpdatePlayerTotalsForFixture(fixtureId);
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

        private void UpdatePlayerTotalsForFixture(int fixtureId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"UPDATE p
SET p.Goals = ISNULL(s.Goals, 0),
    p.Assists = ISNULL(s.Assists, 0),
    p.Appearances = ISNULL(s.Appearances, 0)
FROM Players p
LEFT JOIN (
    SELECT PlayerId,
           SUM(Goals) AS Goals,
           SUM(Assists) AS Assists,
           SUM(Appearances) AS Appearances
    FROM PlayerMatchStats
    GROUP BY PlayerId
) s ON p.PlayerId = s.PlayerId
WHERE p.PlayerId IN (SELECT PlayerId FROM PlayerMatchStats WHERE FixtureId = @FixtureId)", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
