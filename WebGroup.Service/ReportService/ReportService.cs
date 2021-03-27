using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Webgroup.Model.ReportModel;

namespace WebGroup.Service.ReportService
{
    public class ReportService
    {
        private string con = "Data Source=BINHBCNB138102;Initial Catalog=GroupWeb;Integrated Security=True";
        public int CreateReport(string tittle, string content, int userid, string imageUrl)
        {
            int res = 0;
            var objData = new SqlConnection(con);
            try
            {
                objData.Open();
                SqlCommand cmd = new SqlCommand("Reports_Add", objData);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", tittle);
                cmd.Parameters.AddWithValue("@Content", content);
                cmd.Parameters.AddWithValue ("@UserId", userid);
                cmd.Parameters.AddWithValue ("@ImageUrl", imageUrl);
                res = cmd.ExecuteNonQuery(); //1 success
                return res;
            }
            catch (Exception objEx)
            {
                throw objEx;
            }
            finally
            {
                objData.Close();
            }
        }

        public List<ReportModels> GetListReport()
        {
            List<ReportModels> item = new List<ReportModels>();
            var objData = new SqlConnection(con);
            try
            {
                SqlCommand cmd = new SqlCommand("Report_Get", objData);
                cmd.CommandType = CommandType.StoredProcedure;
                objData.Open();
                cmd.ExecuteNonQuery();
                DataTable tbl = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tbl);
                foreach (DataRow row in tbl.Rows)
                {
                    ReportModels obj = new ReportModels();
                    obj.ReportId = (int)row["ReportId"];
                    obj.Title = row["Title"].ToString();
                    obj.Content = row["Content"].ToString();
                    obj.SubmitDate = Convert.ToDateTime(row["SubmitDate"]);
                    obj.UserId = (int)row["UserId"];
                    obj.ImageUrl = row["ImageUrl"].ToString();
                    item.Add(obj);
                }
                
            }
            catch (Exception objex)
            {
                throw objex;
            }
            finally
            {
                objData.Close();
            }
            return item;
        }
    }
}
