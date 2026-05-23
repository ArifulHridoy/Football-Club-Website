using System;
using System.Data.SqlClient;
using System.Web;

namespace myFootballClub
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["AuthUserId"] != null && Request.Cookies["AuthRole"] != null)
                {
                    Session["UserId"] = Request.Cookies["AuthUserId"].Value;
                    Session["Role"] = Request.Cookies["AuthRole"].Value;
                    Session["Name"] = Request.Cookies["AuthName"]?.Value;

                    if (Session["Role"].ToString() == "Admin")
                    {
                        Response.Redirect("Admin/AdminDashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }

                if (Request.Cookies["Email"] != null)
                {
                    txtEmail.Text = Request.Cookies["Email"].Value;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Session["UserId"] = dr["UserId"].ToString();
                            Session["Name"] = dr["Name"].ToString();
                            Session["Role"] = dr["Role"].ToString();

                            if (chkRemember.Checked)
                            {
                                var emailCookie = new HttpCookie("Email", txtEmail.Text.Trim())
                                {
                                    Expires = DateTime.Now.AddDays(7),
                                    HttpOnly = true
                                };
                                var userCookie = new HttpCookie("AuthUserId", dr["UserId"].ToString())
                                {
                                    Expires = DateTime.Now.AddDays(7),
                                    HttpOnly = true
                                };
                                var roleCookie = new HttpCookie("AuthRole", dr["Role"].ToString())
                                {
                                    Expires = DateTime.Now.AddDays(7),
                                    HttpOnly = true
                                };
                                var nameCookie = new HttpCookie("AuthName", dr["Name"].ToString())
                                {
                                    Expires = DateTime.Now.AddDays(7),
                                    HttpOnly = true
                                };

                                Response.Cookies.Add(emailCookie);
                                Response.Cookies.Add(userCookie);
                                Response.Cookies.Add(roleCookie);
                                Response.Cookies.Add(nameCookie);
                            }
                            else
                            {
                                Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["AuthUserId"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["AuthRole"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["AuthName"].Expires = DateTime.Now.AddDays(-1);
                            }

                            if (dr["Role"].ToString() == "Admin")
                            {
                                Response.Redirect("Admin/AdminDashboard.aspx");
                            }
                            else
                            {
                                Response.Redirect("Default.aspx");
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Invalid email or password.";
                        }
                    }
                }
            }
        }
    }
}
