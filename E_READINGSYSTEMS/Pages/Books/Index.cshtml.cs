using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;

namespace E_READINGSYSTEMS.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<BooksInfo> ListBooks = new List<BooksInfo>();
        public void OnGet()
        {
            ListBooks.Clear();
            try
            {
                String conString = "Data Source=KERON\\SQLEXPRESS;Initial Catalog=bk;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Book";
                    using(SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BooksInfo booksInfo = new BooksInfo();
                                booksInfo.BookID = "" + reader.GetInt32(0);
                                booksInfo.ArticleName = reader.GetString(1);
                                booksInfo.ArticleDescription= reader.GetString(2);
                                booksInfo.ArticlePicture= reader.GetString(3);
                                booksInfo.ArticleCategory=reader.GetString(4);
                                booksInfo.ArticleContent=reader.GetString(5);
                                
                                

                                ListBooks.Add(booksInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }
    }
    public class BooksInfo
        {
        public string BookID;
        public string ArticleName;
        public string ArticleDescription;
        public string ArticlePicture;
        public string ArticleCategory;
        public string ArticleContent;
        
    
        

    }
}
