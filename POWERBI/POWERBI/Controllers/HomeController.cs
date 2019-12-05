using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using POWERBI.Models;

namespace POWERBI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        SqlConnection con = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        SqlDataReader dr;
        public ActionResult Index()
        {
            return View();
        }
        void conexionString()
        {
            con.ConnectionString = "data source=DESKTOP-KKF85CV; database=bi;Integrated Security=True";
        }
        public ActionResult Reportes()
        {
            String conexion = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con.ConnectionString = conexion;
            con.Open();
            comando.Connection = con;
            comando.CommandText = "Select ru.id_reporte,r.nombre from usuario_reporte ru " +
                "inner join Reporte r on r.Repor_id = ru.id_reporte" +
                " where ru.id_usuario=" + HttpContext.Session["id"];
            dr = comando.ExecuteReader();
            List<reporte> reporte = new List<reporte>();
            while (dr.Read())
            {
                reporte.Add(new Models.reporte(dr.GetString(0), dr.GetString(1)));
            }

            //ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData();
            return View(reporte);

        }
        public async Task<ActionResult> Report(String reportId) {

            
            ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData(reportId);
          return View(embeddingData);
            
        }
        public async Task<ActionResult> Dashboard() { 
            DashboardEmbeddingData embeddingData = await PbiEmbeddedManager.GetDashboardEmbeddingData();
            return View(embeddingData); 
        }
        public async Task<ActionResult> Qna() {
            QnaEmbeddingData embeddingData = await PbiEmbeddedManager.GetQnaEmbeddingData(); 
            return View(embeddingData);
        }
        public async Task<ActionResult> NewReport() { 
            NewReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetNewReportEmbeddingData(); 
            return View(embeddingData); 
        }
        public async Task<ActionResult> Reports(string reportId) { 
            ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetEmbeddingDataForReport(reportId); 
            return View(embeddingData); 
        }
    }
}