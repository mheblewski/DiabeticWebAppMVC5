using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiInfrastructure.Models
{
    public class LoginApiModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public IDictionary<string, string> AsDictionary()
        {
            return this
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(this).ToString());
        }
    }
}
