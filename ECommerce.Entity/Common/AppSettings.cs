using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using Microsoft.AspNetCore.Hosting;


namespace ECommerce.Entity.Common
{
    public class AppSettings
    {
        public static string API_URL
        {
            get
            {
                return "https://localhost:7143";
                //return MyConvert.ToString(Startup.Configuration["AppSettings:Application:URL:API_URL"]);
            }
        }
    }
}
