using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class Players : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPlayers(null);
            }
        }

        protected void btnPlayerSearch_Click(object sender, EventArgs e)
        {
            LoadPlayers(txtPlayerSearch.Text.Trim());
        }

        protected void btnPlayerClear_Click(object sender, EventArgs e)
        {
            txtPlayerSearch.Text = string.Empty;
            LoadPlayers(null);
        }

        private void LoadPlayers(string search)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT Name, Position, Goals, Assists, Appearances, Photo, FitnessStatus, InjuryStatus FROM Players";
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query += " WHERE Name LIKE @Search OR Position LIKE @Search";
                }
                query += " ORDER BY PlayerId DESC";

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
                        if (!dt.Columns.Contains("FitnessStatus"))
                        {
                            dt.Columns.Add("FitnessStatus", typeof(string));
                        }
                        if (!dt.Columns.Contains("InjuryStatus"))
                        {
                            dt.Columns.Add("InjuryStatus", typeof(string));
                        }
                        rptPlayers.DataSource = dt;
                        rptPlayers.DataBind();
                    }
                }
            }
        }
    }
}
