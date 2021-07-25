using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Configuration;

namespace Report
{
    public static class GlobalVariableClass
    {
        // Put "dbContext" name in the App.config
        public static string sql_Connection = ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString;

    }
}
