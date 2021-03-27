using System;
using System.Collections.Generic;
using System.Text;

namespace Webgroup.Model.ReportModel
{
    public class ReportModels
    {
        public int ReportId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { set; get; }
        public DateTime SubmitDate { set; get; }
    }

}