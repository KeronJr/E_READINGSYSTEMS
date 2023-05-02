using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace E_READINGSYSTEMS.Pages.Books
{
    public class signupModel : PageModel

    {
        public signupInfo signupInfos = new signupInfo();
        public void OnGet()
        {
        }

		public void OnPost()
		{
            signupInfos.fnames= Request.Form["fname"];
			signupInfos.lname = Request.Form["lname"];
			signupInfos.userName = Request.Form["userName"];
			signupInfos.password = Request.Form["password"];
			signupInfos.confirmpassword = Request.Form["confirmpassword"];
            try
            {
                String conString = "Data Source=KERON\\SQLEXPRESS;Initial Catalog=bk;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    if (signupInfos.password == signupInfos.confirmpassword)
                    {



                        string pass = signupInfos.password;
                        using (MD5 md5 = MD5.Create())
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(pass);
                            byte[] hash = md5.ComputeHash(bytes);
                            StringBuilder builder = new StringBuilder();
                            for (int i = 0; i < hash.Length; i++)
                            {
                                builder.Append(hash[i].ToString("X2"));
                            }

                            string hardPassword = builder.ToString();
                            string query = "insert into signup values('" + signupInfos.fnames + "','" + signupInfos.lname + "','" + signupInfos.userName + "','" + hardPassword + "');";
                            SqlDataAdapter sda = new SqlDataAdapter(query, con);
                            sda.SelectCommand.ExecuteNonQuery();

							//MessageBox.Show("Record inserted successfully");

							//AdminLogin loginForm = new AdminLogin();
							//loginForm.Show();
							//this.Hide();
							Response.Redirect("/Signup/login");
						}

                    }
                    else
                    {
						//MessageBox.Show("Mismatch password!!!");
						signupInfos.fnames = "";
						signupInfos.lname = "";
						signupInfos.userName = "";
						signupInfos.password = "";
						signupInfos.confirmpassword = "";

					}
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class signupInfo
    {
        public string fnames;
        public string lname;
        public string userName;
        public string password;
        public string confirmpassword;
       
    }
    
    
}
