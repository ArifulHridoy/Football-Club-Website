using System;
using System.Data.SqlClient;

namespace myFootballClub.Admin
{
    public partial class AddPlayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                lblMessage.Text = "Please fill in required fields.";
                return;
            }

            int goals;
            int.TryParse(txtGoals.Text, out goals);
            int assists;
            int.TryParse(txtAssists.Text, out assists);
            int appearances;
            int.TryParse(txtAppearances.Text, out appearances);

            string photoPath = txtPhotoUrl.Text.Trim();
            string fitnessStatus = txtFitnessStatus.Text.Trim();
            string injuryStatus = txtInjuryStatus.Text.Trim();

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO Players(Name,Position,Goals,Assists,Appearances,Photo,FitnessStatus,InjuryStatus) VALUES(@Name,@Position,@Goals,@Assists,@Appearances,@Photo,@FitnessStatus,@InjuryStatus)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text.Trim());
                    cmd.Parameters.AddWithValue("@Goals", goals);
                    cmd.Parameters.AddWithValue("@Assists", assists);
                    cmd.Parameters.AddWithValue("@Appearances", appearances);
                    cmd.Parameters.AddWithValue("@Photo", photoPath);
                    cmd.Parameters.AddWithValue("@FitnessStatus", string.IsNullOrWhiteSpace(fitnessStatus) ? (object)DBNull.Value : fitnessStatus);
                    cmd.Parameters.AddWithValue("@InjuryStatus", string.IsNullOrWhiteSpace(injuryStatus) ? (object)DBNull.Value : injuryStatus);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtName.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtGoals.Text = string.Empty;
            txtAssists.Text = string.Empty;
            txtAppearances.Text = string.Empty;
            txtFitnessStatus.Text = string.Empty;
            txtInjuryStatus.Text = string.Empty;
            txtPhotoUrl.Text = string.Empty;
            lblMessage.Text = "Player added successfully.";
        }
    }
}
