using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Report
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OperatorClass op = new OperatorClass();
            fillOperatorField(op);

            #region A
            //DataTable dt = OperatorClass.SearchOperator();

            //for (int a = 0; a < dt.Rows.Count; a++)
            //{
            //    for (int b = 0; b < dt.Columns.Count; b++)
            //    {
            //        Console.WriteLine(dt.Rows[a].ItemArray[b]);
            //        @operator.Items.Add(dt.Rows[a].ItemArray[b]);
            //    }
            //}

            //SqlConnection sqlCon = new SqlConnection(@"data source=LAPTOP-NSUOOFBL\SQLEXPRESS1; initial catalog=test; integrated security=True;");
            //DataTable dt = new DataTable();

            //if (sqlCon.State == System.Data.ConnectionState.Closed)
            //{
            //    sqlCon.Open();
            //}

            ////String query = "select Name from OPERATOR where ID=4";
            //String query = "select Name from OPERATOR ";
            //// Creating sqlCmd using query and sqlCon
            //SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

            //Console.WriteLine("-----------------------");

            //// Creating SQL DataAdapter using sqlCmd
            //SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            //adapter.Fill(dt);

            //for (int a = 0; a < dt.Rows.Count; a++)
            //{
            //    for (int b = 0; b < dt.Columns.Count; b++)
            //    {
            //        Console.WriteLine(dt.Rows[a].ItemArray[b]);
            //        @operator.Items.Add(dt.Rows[a].ItemArray[b]);
            //    }
            //    Console.WriteLine("--");
            //}
            #endregion
        }

        private void fillOperatorField(OperatorClass op)
        {
            DataTable dt = OperatorClass.SearchOperator();

            for (int a = 0; a < dt.Rows.Count; a++)
            {
                for (int b = 0; b < dt.Columns.Count; b++)
                {
                    Console.WriteLine(dt.Rows[a].ItemArray[b]);
                    @operator.Items.Add(dt.Rows[a].ItemArray[b]);
                }
                Console.WriteLine("--");
            }
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(@operator.SelectedValue);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
