using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Webgroup.Model.UserModels;

namespace WebGroup.Service.UserApplicationService
{
    public class UserApplicationServices
    {
        private string con = "Data Source=BINHBCNB138102;Initial Catalog=GroupWeb;Integrated Security=True";
        public int CreateAccount(string firstname, string lastname, string email, string username, string password)
        {
            int res = 2;
            var objData = new SqlConnection(con);
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Create", objData);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirtsName", firstname);
                cmd.Parameters.AddWithValue("@LastName", lastname);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@PassWord", password);
                // nhận giá trị trả về
                var returnParameter = cmd.Parameters.Add("@Res", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                // chạy store
                objData.Open();
                cmd.ExecuteNonQuery();
                res = Convert.ToInt32(returnParameter.Value);
            }
            catch (Exception objEx)
            {
                throw objEx;
            }
            finally
            {
                objData.Close();
            }
            return res;
        }
        public UserApplication GetInforUserByUserName(string userName)
        {
            UserApplication item = new UserApplication();
            var objData = new SqlConnection(con);
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Select_By_UserName", objData);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usreName", userName);
                objData.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int roleid = 0;
                        int facultyid = 0;
                        if (reader["RoleId"] != DBNull.Value)
                        {
                            roleid = Convert.ToInt32(reader["RoleId"]);
                        }
                        if (reader["FacultyId"] != DBNull.Value)
                        {
                            facultyid = Convert.ToInt32(reader["FacultyId"]);
                        }
                        item = new UserApplication();
                        item.UserName = reader["UserName"].ToString();
                        item.FirstName = reader["FirstName"].ToString();
                        item.LastName = reader["LastName"].ToString();
                        item.Password = reader["Password"].ToString();
                        item.Email = reader["Email"].ToString();
                        item.FacultyId = facultyid;
                        item.RoleId = roleid;
                        item.UserId = Convert.ToInt32(reader["UserId"]);
                    }
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
