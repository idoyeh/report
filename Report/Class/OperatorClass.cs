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

namespace Report
{
    public class OperatorClass
    {
        testLINQDataContext db = new testLINQDataContext(Properties.Settings.Default.testConnectionString);

        public static List<string> SearchOperator()
        {
            // Database Connection
            OperatorClass query1 = new OperatorClass();
            List<string> strOperator = new List<string>();

            try
            {
                strOperator = query1.db.OPERATORs.Select(n => n.Name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return strOperator;
        }
    }
}
