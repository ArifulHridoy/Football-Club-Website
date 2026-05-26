using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub.Admin
{
    public partial class ManagePlayers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadPlayers();
            }
        }

        private void LoadPlayers()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT PlayerId, Name, Position, Goals, Assists, Appearances, Photo";
                using (SqlCommand schemaCmd = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Players' AND COLUMN_NAME IN ('FitnessStatus','InjuryStatus','InjuryNotes','RecoveryDate')", con))
                {
                    con.Open();
                    using (SqlDataReader reader = schemaCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader.GetString(0);
                            query += ", " + columnName;
                        }
                    }
                    con.Close();
                }
                query += " FROM Players ORDER BY PlayerId DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (!dt.Columns.Contains("FitnessStatus"))
                    {
                        dt.Columns.Add("FitnessStatus", typeof(string));
                    }
                    if (!dt.Columns.Contains("InjuryStatus"))
                    {
                        dt.Columns.Add("InjuryStatus", typeof(string));
                    }
                    if (!dt.Columns.Contains("InjuryNotes"))
                    {
                        dt.Columns.Add("InjuryNotes", typeof(string));
                    }
                    if (!dt.Columns.Contains("RecoveryDate"))
                    {
                        dt.Columns.Add("RecoveryDate", typeof(DateTime));
                    }
                    gvPlayers.DataSource = dt;
                    gvPlayers.DataBind();
                }
            }
        }

        protected void gvPlayers_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvPlayers.EditIndex = e.NewEditIndex;
            LoadPlayers();
        }

        protected void gvPlayers_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvPlayers.EditIndex = -1;
            LoadPlayers();
        }

        protected void gvPlayers_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int playerId = Convert.ToInt32(gvPlayers.DataKeys[e.RowIndex].Value);
            string name = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string position = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string goalsText = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string assistsText = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string appearancesText = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string photo = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string fitnessStatus = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            string injuryStatus = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
            string injuryNotes = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[9].Controls[0]).Text;
            string recoveryDateText = ((System.Web.UI.WebControls.TextBox)gvPlayers.Rows[e.RowIndex].Cells[10].Controls[0]).Text;

            int goals;
            int.TryParse(goalsText, out goals);
            int assists;
            int.TryParse(assistsText, out assists);
            int appearances;
            int.TryParse(appearancesText, out appearances);

            DateTime? recoveryDate = null;
            DateTime parsedDate;
            if (DateTime.TryParse(recoveryDateText, out parsedDate))
            {
                recoveryDate = parsedDate;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "UPDATE Players SET Name=@Name, Position=@Position, Goals=@Goals, Assists=@Assists, Appearances=@Appearances, Photo=@Photo, FitnessStatus=@FitnessStatus, InjuryStatus=@InjuryStatus, InjuryNotes=@InjuryNotes, RecoveryDate=@RecoveryDate WHERE PlayerId=@PlayerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name.Trim());
                    cmd.Parameters.AddWithValue("@Position", position.Trim());
                    cmd.Parameters.AddWithValue("@Goals", goals);
                    cmd.Parameters.AddWithValue("@Assists", assists);
                    cmd.Parameters.AddWithValue("@Appearances", appearances);
                    cmd.Parameters.AddWithValue("@Photo", photo.Trim());
                    cmd.Parameters.AddWithValue("@FitnessStatus", fitnessStatus.Trim());
                    cmd.Parameters.AddWithValue("@InjuryStatus", injuryStatus.Trim());
                    cmd.Parameters.AddWithValue("@InjuryNotes", injuryNotes.Trim());
                    cmd.Parameters.AddWithValue("@RecoveryDate", (object)recoveryDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PlayerId", playerId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            gvPlayers.EditIndex = -1;
            LoadPlayers();
        }

        protected void gvPlayers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int playerId = Convert.ToInt32(gvPlayers.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "DELETE FROM Players WHERE PlayerId=@PlayerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PlayerId", playerId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadPlayers();
        }
    }
}
