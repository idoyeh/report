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
    public class DataClass
    {
        testLINQDataContext db = new testLINQDataContext(Properties.Settings.Default.testConnectionString);

        public static int SearchCountData()
        {
            // Database Connection
            SqlConnection sqlCon = new SqlConnection(GlobalVariableClass.sql_Connection);
            DataTable dt = new DataTable();

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                String query = "select (count(*)/2) from information_schema.columns where table_name = 'DATA' ";

                // Creating sqlCmd using query and sqlCon
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                // Creating SQL DataAdapter using sqlCmd
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        public static void oneRow()
        {
            // Database Connection
            SqlConnection sqlCon = new SqlConnection(GlobalVariableClass.sql_Connection);
            DataTable dt = new DataTable();

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                String query = "select (count(*)/2) from information_schema.columns where table_name = 'DATA' ";

                // Creating sqlCmd using query and sqlCon
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                // Creating SQL DataAdapter using sqlCmd
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public static DateTime SearchFirstDate()
        {
            // Database Connection
            DataClass query1 = new DataClass();
            DateTime date = default(DateTime);

            try
            {
                date = query1.db.DATAs.Select(s => s.Date_Time).First();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return date;
        }

        public static DateTime SearchEndDate()
        {
            // Database Connection
            DataClass query1 = new DataClass();
            DateTime date = default(DateTime);

            try
            {
                date = query1.db.DATAs.OrderByDescending(o => o.Date_Time).Select(s => s.Date_Time).First();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            return date;
        }
    }
}
