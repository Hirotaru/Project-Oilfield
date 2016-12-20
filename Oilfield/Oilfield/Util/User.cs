using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class User
    {
        public bool Admin
        {
            get;
            private set;
        }

        public bool Authorized
        {
            get;
            private set;
        }

        private string login;

        public string Login
        {
            get { return login; }
        }

        private string password;

        public string Password
        {
            get { return password; }
        }

        private Dictionary<string, string> auth = new Dictionary<string, string>()
        {
            {"admin", "admin" }
        };

        public User()
        {
            Authorized = false;
        }

        public bool Auth(string login, string password)
        {
            if (!auth.ContainsKey(login)) return false;

            if (auth[login] != password) return false;

            this.login = login;
            this.password = password;

            if (login == "admin") Admin = true;
            else Admin = false;

            Authorized = true;

            return true;
        }

    }
}
