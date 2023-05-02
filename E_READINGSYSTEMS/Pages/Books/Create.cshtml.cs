using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace E_READINGSYSTEMS.Pages.Books
{
    public class CreateModel : PageModel
    {
        public BooksInfo BookInfo = new BooksInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() {
        BookInfo.BookID = Request.Form["BookID"];
        BookInfo.ArticleName = Request.Form["ArticleName"];
        BookInfo.ArticleDescription = Request.Form["ArticleDescription"];
        BookInfo.ArticlePicture = Request.Form["ArticlePicture"];
        BookInfo.ArticleCategory = Request.Form["ArticleCategory"];
        BookInfo.ArticleContent = Request.Form["ArticleContent"];


            try
            {
                String conString = "Data Source=KERON\\SQLEXPRESS;Initial Catalog=bk;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    
                    con.Open();
                    String sqlQuery = "INSERT INTO Book(BookID, ArticleName, ArticleDescription, ArticlePicture, ArticleCategory, ArticleContent)VALUES (@BookID, @ArticleName, @ArticleDescription, @ArticlePicture, @ArticleCategory, @ArticleContent)";

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
            BookInfo.BookID = "";
            BookInfo.ArticlePicture = "";
            BookInfo.ArticleCategory = "";
            BookInfo.ArticleDescription = "";
            BookInfo.ArticleContent = "";
            BookInfo.ArticleName = "";
            successMessage = "Success";
           Response.Redirect("/Books/Index");

        }
    }
}
