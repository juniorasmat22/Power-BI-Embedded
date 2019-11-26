using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POWERBI.Models;
namespace POWERBI.Controllers
{
  
    public class LoginController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        SqlDataReader dr;
       // GET: Login
       [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void conexionString()
        {
            con.ConnectionString = "data source=DESKTOP-KKF85CV; database=bi;Integrated Security=True";
        }
        [HttpPost]
        public ActionResult Validar(Login login)
        {
            conexionString();
            con.Open();
            comando.Connection = con;
            comando.CommandText = "Select * from Usuario where usuario='"+login.user+ "' and pass='" + login.pass + "'";
            dr = comando.ExecuteReader();
            if (dr.Read()){
                Session["id"] = dr.GetInt32(0);
                Session["user"] = dr.GetString(1);
                Session["pass"] = dr.GetString(2);
                con.Close();
                //Response.Redirect("~/AdmonPrincipal.aspx");
                return View();
            }
            else
            {
                con.Close();
                return View("Login");
            }
           
            
        }
        public ActionResult Cerrar()
        {
            Session["user"] = null;
            Session["pas"] = null;
            return View("Login");
        }
    }
}