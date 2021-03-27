using System;
using System.Collections.Generic;
using System.Text;

namespace Webgroup.Model.UserModels
{
    public class UserApplication
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public int? FacultyId { get; set; }
    }
}
