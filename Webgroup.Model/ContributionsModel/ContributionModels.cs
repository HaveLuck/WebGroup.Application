using System;
using System.Collections.Generic;
using System.Text;

namespace Webgroup.Model.ContributionsModel
{
    public class ContributionModels
    {
        public int ContributionId { get; set; }
        public string Content { get; set; }
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public DateTime SubmitDate { get; set; }
       

    }
}
