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
            con.Close();
            //ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData();
            return View(reporte);

        }
        public ActionResult Dataset()
        {
            String conexion = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con.ConnectionString = conexion;
            con.Open();
            comando.Connection = con;
            comando.CommandText = "Select d.dataset_codigo,d.nombre,d.dataset_id from dataset_usuario du " +
                "inner join Dataset d on d.dataset_codigo = du.dataset_codigo" +
                " where du.id_usuario=" + HttpContext.Session["id"];
            dr = comando.ExecuteReader();
            List<dataSet> reporte = new List<dataSet>();
            while (dr.Read())
            {
                reporte.Add(new Models.dataSet(dr.GetInt32(0), dr.GetString(2), dr.GetString(1)));
            }
            con.Close();
            //ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData();
            return View(reporte);

        }
        public ActionResult lista_Dashboard()
        {
            String conexion = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con.ConnectionString = conexion;
            con.Open();
            comando.Connection = con;
            comando.CommandText = "Select d.das_id ,d.nombre from usuario_dashboard ud " +
                "inner join Dashboard d on d.das_id = ud.id_das" +
                " where ud.id_usuario=" + HttpContext.Session["id"];
            dr = comando.ExecuteReader();
            List<dashboard> reporte = new List<dashboard>();
            while (dr.Read())
            {
                reporte.Add(new Models.dashboard( dr.GetString(0), dr.GetString(1)));
            }
            con.Close();
            //ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData();
            return View(reporte);

        }
        public async Task<ActionResult> Report(String reportId) {

            
            ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData(reportId);
          return View(embeddingData);
            
        }
        public async Task<ActionResult> Dashboard(string DashboardID) { 
            DashboardEmbeddingData embeddingData = await PbiEmbeddedManager.GetDashboardEmbeddingData(DashboardID);
            return View(embeddingData); 
        }
       /* public async Task<ActionResult> Qna() {
            QnaEmbeddingData embeddingData = await PbiEmbeddedManager.GetQnaEmbeddingData(); 
            return View(embeddingData);
        }*/

        public async Task<ActionResult> NewReport(string datasetId) { 
            NewReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetNewReportEmbeddingData(datasetId); 
            return View(embeddingData); 
        }
        public async Task<ActionResult> Reports(string reportId) { 
            ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetEmbeddingDataForReport(reportId);
            String conexion = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con.ConnectionString = conexion;
            con.Open();
            comando.Connection = con;
            comando.CommandText = "Insert into Reporte values ('" + embeddingData.reportId + "','" + embeddingData.reportName + "')";
            comando.ExecuteNonQuery();
            comando.CommandText = "Insert into usuario_reporte values (" + HttpContext.Session["id"] + ",'" + embeddingData.reportId + "')";
            comando.ExecuteNonQuery();
            return View(embeddingData); 
        }
    }
}