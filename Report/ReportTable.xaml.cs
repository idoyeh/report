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
    /// Interaction logic for ReportTable.xaml
    /// </summary>
    public partial class ReportTable : Window
    {
        testLINQDataContext db = new testLINQDataContext(Properties.Settings.Default.testConnectionString);

        public queryClass queryReportTable { get; set; }
        public int selectedValueNumber { get; set; }

        public ReportTable()
        {
            InitializeComponent();
        }

        public ReportTable(queryClass query)
        {
            InitializeComponent();

            OperatorClass query1 = new OperatorClass();
            List<string> strOperator = new List<string>();

            strOperator = OperatorClass.SearchOperator();
            queryReportTable = query;
            selectedValueNumber = Convert.ToInt32(queryReportTable.ValueField.Substring(5, queryReportTable.ValueField.Length - 6 + 1));

            DataTable dt = queryRow();
            DataTable dt1 = FillRowValues(dt);
            displayDetails.ItemsSource = dt1.DefaultView;
        }

        DataTable dt_new;
        DataRow dr;

        private void Window_Loaded()
        {
            var columnNames = db.Mapping.MappingSource
                                  .GetModel(typeof(ReportTable))
                                  .GetMetaType(typeof(DATA))
                                  .DataMembers;

            dt_new = new DataTable("emp");

            DataColumn dc0 = new DataColumn(columnNames[0].Name, typeof(string));
            DataColumn dc1 = new DataColumn(columnNames[selectedValueNumber * 2 - 1].Name, typeof(string));
            dt_new.Columns.Add(dc0);
            dt_new.Columns.Add(dc1);
            int countColumn = 1;

            for (int i = 1; i < columnNames.Count - 1; i = i + 2)
            {
                if (countColumn != selectedValueNumber)
                {
                    dc1 = new DataColumn(columnNames[i].Name, typeof(string));
                    dt_new.Columns.Add(dc1);
                }
                countColumn++;
            }

            displayDetails.ItemsSource = dt_new.DefaultView;
        }

        private DataTable queryRow()
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

                string queryHelpTable = @"IF (EXISTS (SELECT * 
                                             FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'HelpTable'))
                                             BEGIN
                                                 DROP TABLE HelpTable
                                             END

                                             create table HelpTable(
                                             Date_Time datetime,
                                             Valuex real)";

                string queryEx = @"exec('INSERT INTO HelpTable select Date_Time, ' + @nstrValue_ + ' from DATA where ' + @nstrValue_ + ' ' + @op_ + ' ' + @numValue_)";

                string query01 = @"select *
                                    from DATA as D inner join HelpTable as H on D.Date_Time = H.Date_Time
                                    where (H.Date_Time between @Date1 and @Date2)";

                // Creating sqlCmd using query and sqlCon
                SqlCommand sqlCmd = new SqlCommand(queryHelpTable, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCmd = new SqlCommand(queryEx, sqlCon);
                sqlCmd.Parameters.AddWithValue("@numValue_", queryReportTable.Value);
                sqlCmd.Parameters.AddWithValue("@nstrValue_", queryReportTable.ValueField);
                sqlCmd.Parameters.AddWithValue("@op_", queryReportTable.Operator);
                sqlCmd.ExecuteNonQuery();

                // Creating sqlCmd using query and sqlCon
                sqlCmd = new SqlCommand(query01, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Date1", queryReportTable.FirstDate);
                sqlCmd.Parameters.AddWithValue("@Date2", queryReportTable.EndDate);
                sqlCmd.ExecuteNonQuery();

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

        private DataTable FillRowValues(DataTable dt)
        {
            // Database Connection
            SqlConnection sqlCon = new SqlConnection(GlobalVariableClass.sql_Connection);

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                Window_Loaded();

                string queryCreateFunc1 = @"IF (EXISTS (SELECT * 
                                             FROM sys.objects 
                                             WHERE object_id = OBJECT_ID('statusValue')))
                                             begin
                                                 DROP FUNCTION statusValue
                                             end";

                string queryCreateFunc2 = @"CREATE FUNCTION dbo.statusValue(@id smallint, @val real)
                                            RETURNS NVARCHAR(1000) as
                                            begin
                                            	declare @valueOrName nvarchar(1000)
                                            	if((select Valid
                                            			from STATUS as S
                                            			where S.Id = @id) = 'True')
                                            		begin
                                            			set @valueOrName = CONVERT(nvarchar(1000), @val);
                                            		end
                                            	else
                                            		begin
                                            			set @valueOrName = (select Name
                                            			from STATUS as S
                                            			where S.Id = @id)
                                            		end
                                            	RETURN @valueOrName;
                                            end";

                string queryExValue = @"select dbo.statusValue(@insertStatus_, @insertValue_)";

                // Creating sqlCmd using query and sqlCon
                SqlCommand sqlCmd = new SqlCommand(queryCreateFunc1, sqlCon);
                sqlCmd.ExecuteNonQuery();

                // Creating sqlCmd using query and sqlCon
                sqlCmd = new SqlCommand(queryCreateFunc2, sqlCon);
                sqlCmd.ExecuteNonQuery();

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    dr = dt_new.NewRow();
                    dr[0] = dt.Rows[a].ItemArray[0].ToString();

                    // Creating sqlCmd using query and sqlCon
                    sqlCmd = new SqlCommand(queryExValue, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@insertStatus_", dt.Rows[a].ItemArray[selectedValueNumber * 2]);
                    sqlCmd.Parameters.AddWithValue("@insertValue_", dt.Rows[a].ItemArray[selectedValueNumber * 2 - 1]);

                    var res = sqlCmd.ExecuteScalar();
                    dr[1] = res;
                    int num = 1, numColumn = 2;

                    for (int b = 1; b < dt.Columns.Count - 2; b = b + 2)
                    {
                        if (num != selectedValueNumber)
                        {
                            sqlCmd = new SqlCommand(queryExValue, sqlCon);
                            sqlCmd.Parameters.AddWithValue("@insertStatus_", dt.Rows[a].ItemArray[b + 1]);
                            sqlCmd.Parameters.AddWithValue("@insertValue_", dt.Rows[a].ItemArray[b]);
                            res = sqlCmd.ExecuteScalar();
                            dr[numColumn++] = res;
                        }
                        num++;
                    }
                    dt_new.Rows.Add(dr);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            return dt_new;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
            }
            finally
            {
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
        }

    }
}
