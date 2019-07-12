using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using PreFinalOp.App_Code;
using PreFinalOp.Models;

namespace PreFinalOp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        public bool IsExisting(string username)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT Username FROM Login WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    return cmd.ExecuteScalar() == null ? false : true;
                }
            }
        }

        [HttpPost]
        public ActionResult Register(Login login)
        {
            if (IsExisting(login.Username))
            {
                ViewBag.Message = "Username already Exists";
                return View(login);
            }
            else
            {
                using (SqlConnection con = new SqlConnection(Helper.GetCon()))
                {
                    con.Open();
                    string query = @"INSERT INTO Login(Username, Password) VALUES (@Username, @Password)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", login.Username);
                        cmd.Parameters.AddWithValue("@Password", login.Password);
                        cmd.ExecuteNonQuery();


                        string message = "eyo wassup " + login.Username;

                        return RedirectToAction("Login");
                    }
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login lll)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT LoginID, Username FROM Login WHERE Username=@Username AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", lll.Username);
                    cmd.Parameters.AddWithValue("@Password", lll.Password);

                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        if (data.HasRows)
                        {
                            while (data.Read())
                            {
                                Session["LoginID"] = data["LoginID"].ToString();
                                Session["Username"] = data["Username"].ToString();
                            }
                            return RedirectToAction("Profile");
                        }
                        else
                            ViewBag.Message = "Invalid Credentials";
                        return View();
                    }
                }
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            Helper.ValidateLogin();
            var record = new Login();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT LoginID, Username, Password FROM Login WHERE LoginID=@LoginID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@LoginID", Session["LoginID"].ToString());
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                record.ID = int.Parse(sdr["LoginID"].ToString());
                                record.Username = sdr["Username"].ToString();
                                record.Password = sdr["Password"].ToString();
                            }
                            return View(record);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Profile(Login login)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Login SET Username=@Username, Password=@Password WHERE LoginID=@LoginID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", login.Username);
                    cmd.Parameters.AddWithValue("@Password", login.Password);
                    cmd.Parameters.AddWithValue("@LoginID", Session["LoginID"].ToString());
                    cmd.ExecuteNonQuery();

                    return View(login);
                }
            }
        }
    }
}