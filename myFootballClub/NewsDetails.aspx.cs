using System;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class NewsDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNews();
            }
        }

        private void LoadNews()
        {
            int newsId;
            if (!int.TryParse(Request.QueryString["id"], out newsId))
            {
                pnlNotFound.Visible = true;
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT Title, Description, Image, PublishDate FROM News WHERE NewsId=@NewsId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NewsId", newsId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pnlNews.Visible = true;
                            pnlComments.Visible = true;
                            lblTitle.InnerText = reader["Title"].ToString();
                            lblDescription.InnerText = reader["Description"].ToString();
                            lblDate.InnerText = Convert.ToDateTime(reader["PublishDate"]).ToString("dd MMM yyyy");
                            imgNews.Src = reader["Image"].ToString();
                            imgNews.Alt = reader["Title"].ToString();
                        }
                        else
                        {
                            pnlNotFound.Visible = true;
                        }
                    }
                }
            }

            LoadComments(newsId);
        }

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            int newsId;
            if (!int.TryParse(Request.QueryString["id"], out newsId))
            {
                lblCommentMessage.Text = "Unable to post comment.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCommentName.Text) || string.IsNullOrWhiteSpace(txtComment.Text))
            {
                lblCommentMessage.Text = "Please enter your name and comment.";
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO NewsComments(NewsId,UserName,Email,CommentText,CreatedAt) VALUES(@NewsId,@UserName,@Email,@CommentText,GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NewsId", newsId);
                    cmd.Parameters.AddWithValue("@UserName", txtCommentName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtCommentEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@CommentText", txtComment.Text.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtComment.Text = string.Empty;
            lblCommentMessage.Text = "Comment posted.";
            LoadComments(newsId);
        }

        private void LoadComments(int newsId)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT UserName, CommentText, CreatedAt FROM NewsComments WHERE NewsId=@NewsId ORDER BY CreatedAt DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NewsId", newsId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        var dt = new System.Data.DataTable();
                        da.Fill(dt);
                        rptComments.DataSource = dt;
                        rptComments.DataBind();
                    }
                }
            }
        }
    }
}
