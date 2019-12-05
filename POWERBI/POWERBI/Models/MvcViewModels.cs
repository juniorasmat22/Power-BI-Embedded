using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POWERBI.Models
{
    public class Login
    {
        public string user { set; get; }
        public string pass { set; get; }

    }
    public class reporte
    {
        public string reportId { set; get; }
        public string reportName { set; get; }

        public reporte(string reportId, string reportName)
        {
            this.reportId = reportId;
            this.reportName = reportName;
        }
    }
    public class dataSet
    {
        public int codigo { set; get; }
        public string id { set; get; }
        public string nombre { set; get; }
        public dataSet(int codigo, string id,string nombre)
        {
            this.codigo = codigo;
            this.id = id;
            this.nombre = nombre;
        }


    }
    public class dashboard
    {
        public string id { set; get; }
        public string nombre { set; get; }
        public dashboard(string id, string nombre)
        {
             this.id = id;
            this.nombre = nombre;
        }
    }
    // data required for embedding a report
    public class ReportEmbeddingData {
        public string reportId; 
        public string reportName;
        public string embedUrl;
        public string accessToken; 
    } 
    // data required for embedding a new report
    public class NewReportEmbeddingData { 
        public string workspaceId;
        public string datasetId; 
        public string embedUrl; 
        public string accessToken; 
    }
    // data required for embedding a dashboard 
    public class DashboardEmbeddingData { 
        public string dashboardId; 
        public string dashboardName; 
        public string embedUrl; 
        public string accessToken; 
    } 
    // data required for embedding a dashboard 
    public class QnaEmbeddingData {
        public string datasetId; 
        public string embedUrl;
        public string accessToken; 
    }
}