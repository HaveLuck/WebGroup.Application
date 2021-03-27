using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using Webgroup.Model.ResponseMessageModel;
using Webgroup.Model.UserModels;
using WebGroup.Service.UserApplicationService;


namespace WebGroup.Application.Controllers
{
    public class UserApplicationController : ApiController
    {
        ResponseMessageModel response = new ResponseMessageModel();
        UserApplicationServices bus = new UserApplicationServices();

        [Route("api/newAccount")]
        [HttpPost]
        public IHttpActionResult CreateAccount(string lastname, string firstname, string email, string username, string password)
        {
            string hashPassword = HashPassword(password);
            var res = bus.CreateAccount(lastname, firstname, email, username, hashPassword);
            if (res == 1 )
            {
                response.ResponseCode = res;
                response.ResponseMessage = "UserName was existed, Please input new Username";
                return Json(response);
            }
            else if(res == 0)
            {
                response.ResponseCode = res;
                response.ResponseMessage = "Create User Successful";
                return Json(response);
            }
            else
            {
                response.ResponseCode = res;
                response.ResponseMessage = "Cannot create User, Please check again";
                return Json(response);
            }
        }
        [Route("api/getUser")]
        [HttpPost]
        public IHttpActionResult GetInforUser(string username)
        {
            var res = bus.GetInforUserByUserName(username);
            return Json(res);
        }
        private static string HashPassword(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        private static bool CheckPassword(string passwordLogin, string passwordSystem)
        {
            string passWordLogin =  HashPassword(passwordLogin);
            return passWordLogin == passwordSystem;
        }
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login(string username, string passWord)
        {
            var res = bus.GetInforUserByUserName(username);
            if (res == null)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = "UserName or Password is incorrect";
                return Json(response);
            }
            bool islogin = CheckPassword(passWord, res.Password);
            if (islogin)
            {
                var issuccess = new
                {
                   
                    data = res,
                    ResponseCode = 1
                };
                return Json(issuccess);
            }
            response.ResponseCode = -1;
            response.ResponseMessage = "UserName or Password is incorrect";
            return Json(response);
        }
    }
}