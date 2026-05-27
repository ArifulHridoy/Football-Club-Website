using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace myFootballClub.Admin
{
    public partial class MatchEvents : System.Web.UI.Page
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
                LoadEvents();
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
                    ddlPlayer.DataSource = dt;
                    ddlPlayer.DataTextField = "Name";
                    ddlPlayer.DataValueField = "PlayerId";
                    ddlPlayer.DataBind();
                    ddlPlayer.Items.Insert(0, new ListItem("Select player", string.Empty));

                    ddlAssist.DataSource = dt;
                    ddlAssist.DataTextField = "Name";
                    ddlAssist.DataValueField = "PlayerId";
                    ddlAssist.DataBind();
                    ddlAssist.Items.Insert(0, new ListItem("Select assist", string.Empty));
                }
            }
        }

        private void LoadEvents()
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                gvEvents.DataSource = null;
                gvEvents.DataBind();
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"SELECT e.EventId, e.Minute, e.EventType, p.Name AS PlayerName, ap.Name AS AssistName
FROM MatchEvents e
INNER JOIN Players p ON e.PlayerId = p.PlayerId
LEFT JOIN Players ap ON e.AssistPlayerId = ap.PlayerId
WHERE e.FixtureId = @FixtureId
ORDER BY e.Minute", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvEvents.DataSource = dt;
                    gvEvents.DataBind();
                }
            }
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
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

            int assistId;
            int.TryParse(ddlAssist.SelectedValue, out assistId);

            int minute;
            int.TryParse(txtMinute.Text, out minute);

            string eventType = ddlEventType.SelectedValue;

            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO MatchEvents(FixtureId, PlayerId, AssistPlayerId, Minute, EventType) VALUES(@FixtureId,@PlayerId,@AssistPlayerId,@Minute,@EventType)", con))
                {
                    cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                    cmd.Parameters.AddWithValue("@PlayerId", playerId);
                    cmd.Parameters.AddWithValue("@AssistPlayerId", assistId == 0 ? (object)DBNull.Value : assistId);
                    cmd.Parameters.AddWithValue("@Minute", minute);
                    cmd.Parameters.AddWithValue("@EventType", eventType);
                    cmd.ExecuteNonQuery();
                }

                if (eventType == "Goal")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE PlayerMatchStats SET Goals = Goals + 1, Appearances = CASE WHEN Appearances < 1 THEN 1 ELSE Appearances END WHERE FixtureId=@FixtureId AND PlayerId=@PlayerId", con))
                    {
                        cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                        cmd.Parameters.AddWithValue("@PlayerId", playerId);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText = "INSERT INTO PlayerMatchStats(FixtureId, PlayerId, Goals, Appearances) VALUES(@FixtureId,@PlayerId,1,1)";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    UpdateFixtureScore(fixtureId, ddlScoreSide.SelectedValue);
                }

                if (eventType == "Assist" && assistId > 0)
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

            UpdatePlayerTotalsForFixture(fixtureId);
            lblMessage.Text = "Event added.";
            txtMinute.Text = string.Empty;
            LoadEvents();
        }

        protected void ddlFixture_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEvents();
        }

        protected void gvEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eventId = Convert.ToInt32(gvEvents.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("DELETE FROM MatchEvents WHERE EventId=@EventId", con))
            {
                cmd.Parameters.AddWithValue("@EventId", eventId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEvents();
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
