using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class TeamLineup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFixtures();
            }
        }

        protected void btnLoadLineup_Click(object sender, EventArgs e)
        {
            int fixtureId;
            if (!int.TryParse(ddlFixture.SelectedValue, out fixtureId))
            {
                rptStarting.DataSource = null;
                rptStarting.DataBind();
                rptSubstitutes.DataSource = null;
                rptSubstitutes.DataBind();
                return;
            }

            LoadLineup(fixtureId);
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

        private void LoadLineup(int fixtureId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT p.Name, p.Position, l.Role FROM FixtureLineups l INNER JOIN Players p ON l.PlayerId = p.PlayerId WHERE l.FixtureId = @FixtureId ORDER BY l.Role, p.Name", con))
            {
                cmd.Parameters.AddWithValue("@FixtureId", fixtureId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataView starting = new DataView(dt);
                    starting.RowFilter = "Role = 'Starting'";
                    rptStarting.DataSource = starting;
                    rptStarting.DataBind();

                    DataView substitutes = new DataView(dt);
                    substitutes.RowFilter = "Role = 'Substitute'";
                    rptSubstitutes.DataSource = substitutes;
                    rptSubstitutes.DataBind();
                }
            }
        }
    }
}
