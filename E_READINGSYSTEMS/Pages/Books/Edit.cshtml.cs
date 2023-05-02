using E_READINGSYSTEMS.Pages.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace E_READINGSYSTEMS.Pages.Books
{
    public class EditModel : PageModel
    {
        public BooksInfo BookInfo = new BooksInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {  
            String BookID = Request.Query["BookID"];

            try
            {
                String conString = "Data Source=DESKTOP-AV52V94\\SQLEXPRESS02;Initial Catalog=EreadingOSystem;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Book WHERE BookID = @BookID";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                BookInfo.BookID = "" + reader.GetInt32(0);
                                BookInfo.ArticleName = reader.GetString(1);
                                BookInfo.ArticleDescription = reader.GetString(2);
                                BookInfo.ArticlePicture = reader.GetString(3);
                                BookInfo.ArticleCategory = reader.GetString(4);
                                BookInfo.ArticleContent = reader.GetString(5);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
          

        }
        public void OnPost()
        {
            BookInfo.BookID = Request.Form["BookID"];
            BookInfo.ArticleName = Request.Form["ArticleName"];
            BookInfo.ArticleDescription = Request.Form["ArticleDescription"];
            BookInfo.ArticlePicture = Request.Form["ArticlePicture"];
            BookInfo.ArticleCategory = Request.Form["ArticleCategory"];
            BookInfo.ArticleContent = Request.Form["ArticleContent"];

           try
            {
                String conString = "Data Source=KERON\\SQLEXPRESS;Initial Catalog=bk;Integrated Security=True";
               // String conString = "Data Source=DESKTOP-AV52V94\\SQLEXPRESS02;Initial Catalog=EreadingOSystem;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();
                    String sqlQuery = "UPDATE Book SET ArticleName=@ArticleName, ArticleDescription=@ArticleDescription, ArticlePicture=@ArticlePicture, ArticleCategory=@ArticleCategory, ArticleContent=@ArticleContent WHERE BookID=@BookID";


                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookInfo.BookID);
                        cmd.Parameters.AddWithValue("@ArticleName", BookInfo.ArticleName);
                        cmd.Parameters.AddWithValue("@ArticleDescription", BookInfo.ArticleDescription);
                        cmd.Parameters.AddWithValue("@ArticlePicture", BookInfo.ArticlePicture);
                        cmd.Parameters.AddWithValue("@ArticleCategory", BookInfo.ArticleCategory);
                        cmd.Parameters.AddWithValue("@ArticleContent", BookInfo.ArticleContent);

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Books/Index");

        }
    }

        
    }

