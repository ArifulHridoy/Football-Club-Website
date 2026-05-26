using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class NewsAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadNews();
            }
        }

        protected void btnAddNews_Click(object sender, EventArgs e)
        {
            DateTime publishDate;
            if (!DateTime.TryParse(txtPublishDate.Text, out publishDate))
            {
                lblMessage.Text = "Please enter a valid publish date.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            string imagePath = string.Empty;
            if (fuNewsImage.HasFile)
            {
                string fileName = System.IO.Path.GetFileName(fuNewsImage.FileName);
                string folder = Server.MapPath("~/Images/News/");
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }

                string savedPath = System.IO.Path.Combine(folder, fileName);
                fuNewsImage.SaveAs(savedPath);
                imagePath = "Images/News/" + fileName;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO News(Title,Description,Image,PublishDate) VALUES(@Title,@Description,@Image,@PublishDate)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Image", imagePath);
                    cmd.Parameters.AddWithValue("@PublishDate", publishDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPublishDate.Text = string.Empty;
            lblMessage.Text = "News added.";
            LoadNews();
        }

        private void LoadNews()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT NewsId, Title, Description, Image, PublishDate FROM News ORDER BY PublishDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvNews.DataSource = dt;
                    gvNews.DataBind();
                }
            }
        }

        protected void gvNews_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvNews.EditIndex = e.NewEditIndex;
            LoadNews();
        }

        protected void gvNews_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvNews.EditIndex = -1;
            LoadNews();
        }

        protected void gvNews_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int newsId = Convert.ToInt32(gvNews.DataKeys[e.RowIndex].Value);
            string title = ((System.Web.UI.WebControls.TextBox)gvNews.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string description = ((System.Web.UI.WebControls.TextBox)gvNews.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string image = ((System.Web.UI.WebControls.TextBox)gvNews.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string dateText = ((System.Web.UI.WebControls.TextBox)gvNews.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            DateTime publishDate;
            DateTime.TryParse(dateText, out publishDate);

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "UPDATE News SET Title=@Title, Description=@Description, Image=@Image, PublishDate=@PublishDate WHERE NewsId=@NewsId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", title.Trim());
                    cmd.Parameters.AddWithValue("@Description", description.Trim());
                    cmd.Parameters.AddWithValue("@Image", image.Trim());
                    cmd.Parameters.AddWithValue("@PublishDate", publishDate);
                    cmd.Parameters.AddWithValue("@NewsId", newsId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            gvNews.EditIndex = -1;
            LoadNews();
        }

        protected void gvNews_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int newsId = Convert.ToInt32(gvNews.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "DELETE FROM News WHERE NewsId=@NewsId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NewsId", newsId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadNews();
        }
    }
}
