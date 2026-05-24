using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myFootballClub
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContactName.Text) || string.IsNullOrWhiteSpace(txtContactEmail.Text) || string.IsNullOrWhiteSpace(txtContactMessage.Text))
            {
                lblContactMessage.Text = "Please fill in the required fields.";
                return;
            }

            using (var con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO ContactMessages(Name,Email,Subject,Message,CreatedAt) VALUES(@Name,@Email,@Subject,@Message,GETDATE())";
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", txtContactName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtContactEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Subject", txtContactSubject.Text.Trim());
                    cmd.Parameters.AddWithValue("@Message", txtContactMessage.Text.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtContactName.Text = string.Empty;
            txtContactEmail.Text = string.Empty;
            txtContactSubject.Text = string.Empty;
            txtContactMessage.Text = string.Empty;
            lblContactMessage.Text = "Message sent successfully.";
        }
    }
}