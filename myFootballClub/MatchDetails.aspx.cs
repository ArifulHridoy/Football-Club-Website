using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class MatchDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFixtures();
            }
        }

        protected void btnLoadDetails_Click(object sender, EventArgs e)
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                gvEvents.DataSource = null;
                gvEvents.DataBind();
                return;
            }

            LoadEvents(fixtureId);
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
                ddlFixture.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select fixture", string.Empty));
            }
        }

        private void LoadEvents(int fixtureId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"SELECT e.Minute,
       p.Name AS Scorer,
       ap.Name AS Assist,
       e.EventType
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
                    DataTable timelineTable = new DataTable();
                    timelineTable.Columns.Add("Minute", typeof(int));
                    timelineTable.Columns.Add("EventSummary", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string summary = BuildEventSummary(row);
                        DataRow timelineRow = timelineTable.NewRow();
                        timelineRow["Minute"] = row["Minute"];
                        timelineRow["EventSummary"] = summary;
                        timelineTable.Rows.Add(timelineRow);
                    }

                    rptTimeline.DataSource = timelineTable;
                    rptTimeline.DataBind();
                }
            }
        }

        private string BuildEventSummary(DataRow row)
        {
            string eventType = row["EventType"].ToString();
            string scorer = row["Scorer"].ToString();
            string assist = row["Assist"].ToString();

            switch (eventType)
            {
                case "Goal":
                    return string.IsNullOrWhiteSpace(assist)
                        ? string.Format("Goal - {0}", scorer)
                        : string.Format("Goal - {0} (Assist: {1})", scorer, assist);
                case "Assist":
                    return string.Format("Assist - {0}", scorer);
                case "YellowCard":
                    return string.Format("Yellow Card - {0}", scorer);
                case "RedCard":
                    return string.Format("Red Card - {0}", scorer);
                case "Substitution":
                    return string.IsNullOrWhiteSpace(assist)
                        ? string.Format("Substitution - {0}", scorer)
                        : string.Format("Substitution - {0} for {1}", scorer, assist);
                default:
                    return string.Format("{0} - {1}", eventType, scorer);
            }
        }
    }
}
