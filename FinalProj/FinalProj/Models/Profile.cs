using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Content_Upload_Model.Models.DAL;

namespace Content_Upload_Model.Models
{
    public class Profile
    {
        
        string mail;
        string password;

        public Profile() { }

        public Profile(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }

        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }

        public string Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertProfile(this);
        }
    }
}