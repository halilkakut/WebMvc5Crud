using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplicationProductModule.Models;

namespace WebApplicationProductModule.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(Login login)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand sqlcom = new SqlCommand("[dbo].[UserLogin]");
            sqlconn.Open();
            sqlcom.Connection = sqlconn;
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue("@Email", login.Email);
            sqlcom.Parameters.AddWithValue("@Password", login.Password);
            SqlDataReader sdr = sqlcom.ExecuteReader();

            if (sdr.Read())
            {
                FormsAuthentication.SetAuthCookie(login.Email, true);
                Session["username"] = login.Email.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["message"] = "Giriş İşlemi Başarısız.";
            }

            sqlconn.Close();


            return View();
        }
    }
}