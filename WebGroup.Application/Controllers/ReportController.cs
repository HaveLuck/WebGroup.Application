using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebGroup.Service.ReportService;

namespace WebGroup.Application.Controllers
{
    public class ReportController : ApiController
    {
        ReportService bus = new ReportService();
        // GET: Report
       
        [Route("api/addreport")]
        [HttpPost]
        public IHttpActionResult AddNewReport(string tittle, string content, string pathimage, int userid)
        {
            var res = bus.CreateReport(tittle, content, userid, pathimage);
            return Json(res);
        }
        [Route("api/listreport")]
        [HttpGet]
        public IHttpActionResult GetListReport()
        {
            var res = bus.GetListReport();
            return Json(res);
        }
    }
}