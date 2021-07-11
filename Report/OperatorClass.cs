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
        public int ID { get; set; }
        public string Name { get; set; }

        //public static SearchOperator(OperatorClass o)
        public static DataTable SearchOperator()
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

                //String query = "select Name from OPERATOR where ID=4";
                String query = "select Name from OPERATOR ";
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
            return dt;
        }
    }
}
