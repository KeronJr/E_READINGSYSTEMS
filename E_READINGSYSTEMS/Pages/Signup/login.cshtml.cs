using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using E_READINGSYSTEMS.Pages.Books;
using static E_READINGSYSTEMS.Pages.Signup.loginModel;

namespace E_READINGSYSTEMS.Pages.Signup
{
	public class loginModel : PageModel
	{
		public loginInfo loginInfos = new loginInfo();
		public void OnPost()
		{
			loginInfos.userName = Request.Form["userName"];
			loginInfos.hardPassword = Request.Form["hardPassword"];
			try
			{
				String conString = "Data Source=KERON\\SQLEXPRESS;Initial Catalog=bk;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(conString))
				{
					string username, password;
					username = loginInfos.userName;
					password = loginInfos.hardPassword;

					using (MD5 md5 = MD5.Create())
					{
						byte[] bytes = Encoding.UTF8.GetBytes(password);
						byte[] hash = md5.ComputeHash(bytes);
						StringBuilder builder = new StringBuilder();
						for (int i = 0; i < hash.Length; i++)
						{
							builder.Append(hash[i].ToString("X2"));
						}

						string hardPassword = builder.ToString();

						string querry = "select count (*) from signup where userName ='" + loginInfos.userName + "' and hardPassword = '" + hardPassword + "'";
						SqlDataAdapter sda = new SqlDataAdapter(querry, con);
						DataTable dtable = new DataTable();
						sda.Fill(dtable);

						if (dtable.Rows[0][0].ToString() == "1")
						{
							// page that need to be load next

							Response.Redirect("/Books/Index");
						}
						else
						{
							// MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							loginInfos.userName = "";
							loginInfos.hardPassword = "";

							//loginInfos.hardpassword.Focus();
							//messageLabel.Text = "Invalid username or password";
							//errorbox.Text = "Invalid username or password";
						}
					}


				}
			}
			catch (Exception ex)
			{

			}
		}
				//public void OnPost() { }

		public class loginInfo
		{

			public string userName;
			public string hardPassword;


		}
	} }

