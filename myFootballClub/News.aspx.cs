using System;
using System.Data;
using System.Data.SqlClient;

namespace myFootballClub
{
    public partial class News : System.Web.UI.Page
    {
        private const int PageSize = 6;

        protected int CurrentPage
        {
            get => ViewState["NewsPage"] == null ? 1 : (int)ViewState["NewsPage"];
            set => ViewState["NewsPage"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNews();
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadNews();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            LoadNews();
        }

        private void LoadNews()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string countQuery = "SELECT COUNT(*) FROM News";
                string dataQuery = "SELECT NewsId, Title, Description, Image, PublishDate, LEFT(Description, 180) AS ShortDescription FROM News ORDER BY PublishDate DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                int totalCount = 0;
                using (SqlCommand countCmd = new SqlCommand(countQuery, con))
                {
                    con.Open();
                    totalCount = (int)countCmd.ExecuteScalar();
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand(dataQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Offset", (CurrentPage - 1) * PageSize);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        rptNews.DataSource = dt;
                        rptNews.DataBind();
                    }
                }

                int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                if (CurrentPage > totalPages && totalPages > 0)
                {
                    CurrentPage = totalPages;
                    LoadNews();
                    return;
                }

                lblPageInfo.Text = $"Page {CurrentPage} of {Math.Max(totalPages, 1)}";
                btnPrev.Enabled = CurrentPage > 1;
                btnNext.Enabled = CurrentPage < totalPages;
            }
        }
    }
}
