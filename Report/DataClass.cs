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
        public DateTime Date1 { get; set; }
        public List<double> Values { get; set; }
        public List<int> Status { get; set; }

        public static int SearchCountData()
        {
            // Database Connection
            SqlConnection sqlCon = new SqlConnection(@"data source=LAPTOP-NSUOOFBL\SQLEXPRESS1; initial catalog=test; integrated security=True;");
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

                Console.WriteLine("-----------------------");

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

        //public static int SearchFirstAndEndDate()
        //{
        //    // Database Connection
        //    SqlConnection sqlCon = new SqlConnection("your connection string");
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if (sqlCon.State == System.Data.ConnectionState.Closed)
        //        {
        //            sqlCon.Open();
        //        }

        //        String query = "select Date_Time from information_schema.columns where table_name = 'DATA' ";

        //        // Creating sqlCmd using query and sqlCon
        //        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

        //        Console.WriteLine("-----------------------");

        //        // Creating SQL DataAdapter using sqlCmd
        //        SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
        //        adapter.Fill(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        sqlCon.Close();
        //    }
        //    return Convert.ToInt32(dt.Rows[0][0]);
        //}
    }
}
