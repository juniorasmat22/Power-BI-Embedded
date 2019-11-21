using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Report() { 
            ReportEmbeddingData embeddingData = await PbiEmbeddedManager.GetReportEmbeddingData(); 
            return View(embeddingData); 
        }
    }
}