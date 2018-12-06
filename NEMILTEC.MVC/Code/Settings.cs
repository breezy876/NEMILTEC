using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEMILTEC.MVC.Code
{
    public static class Settings
    {
        public static string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString;
    }
}
