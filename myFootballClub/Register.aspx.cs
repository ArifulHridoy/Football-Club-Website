using System;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO Users(Name,Email,Password,Role) VALUES(@Name,@Email,@Password,'User')";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("Login.aspx");
        }
    }
}
