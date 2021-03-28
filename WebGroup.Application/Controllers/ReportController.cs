using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
      
        //lưu ảnh lên server
        [Route("api/UploadFile")]
        [HttpPost]
        public string UploadFile()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/uploads"),
                    fileName
                );
                
                file.SaveAs(path);
                
            }
             string res = file != null ? "/uploads/" + file.FileName : null;
            return res;

        }

    }
}