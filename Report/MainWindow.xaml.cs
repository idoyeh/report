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
        OperatorClass op = new OperatorClass();
        DataClass dataClass = new DataClass();

        public MainWindow()
        {
            InitializeComponent();
            init();

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

        private void init()
        {
            fillOperatorField(op);
            fillValueDataField(dataClass);

            @startDate.SelectedDate = null;
            @stopDate.SelectedDate = null;

            @time1.Text = "00:00:00";
            @time2.Text = "00:00:00";
            
            // clear value field
            @value.Text = "";
            // clear error message
            @errorMsg.Content = "";
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

        private void removeOperatorField(OperatorClass op)
        {
            DataTable dt = OperatorClass.SearchOperator();

            for (int a = 0; a < dt.Rows.Count; a++)
            {
                for (int b = 0; b < dt.Columns.Count; b++)
                {
                    Console.WriteLine(dt.Rows[a].ItemArray[b]);
                    @operator.Items.Remove(dt.Rows[a].ItemArray[b]);
                }
                Console.WriteLine("--");
            }
        }

        private void fillValueDataField(DataClass dataClass)
        {
            int countCulmn = DataClass.SearchCountData();
            for (int a = 1; a <= countCulmn; a++)
            {
                @field.Items.Add("Value" + a);
            }
        }

        private void removeValueDataField(DataClass dataClass)
        {
            int countCulmn = DataClass.SearchCountData();
            for (int a = 1; a <= countCulmn; a++)
            {
                @field.Items.Remove("Value" + a);
            }
        }
        
        private bool IsFloatnumber(string str)
        {
            float number = 0;
            if (!(float.TryParse(str, out number)))
            {
                @errorMsg.Content = "* שדה ערך צריך להיות מספר!";
                return false;
            }
            else
            {
                @errorMsg.Content = "";
                return true;
            }
        }

        private bool stringTimeDistance(string first, string second)
        {
            string[] time1 = first.Split(':');
            string[] time2 = second.Split(':');
            
            if (Int32.Parse(time1[0]) > Int32.Parse(time2[0]))
            {
                return false;
            }
            else if ((Int32.Parse(time1[0]) == Int32.Parse(time2[0])) && Int32.Parse(time1[1]) > Int32.Parse(time2[1]))
            {
                return false;
            }
            else if ((Int32.Parse(time1[0]) == Int32.Parse(time2[0])) && (Int32.Parse(time1[1]) == Int32.Parse(time2[1])) && (float.Parse(time1[2]) > float.Parse(time2[2])))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            bool isFloat = IsFloatnumber(@value.Text);

            string[] t1 = time1.Text.Split(':');
            DateTime dateTime1 = Convert.ToDateTime(@startDate.Text) + new TimeSpan(Int32.Parse(t1[0]), Int32.Parse(t1[1]), Int32.Parse(t1[2]));
            string[] t2 = time2.Text.Split(':');
            DateTime dateTime2 = Convert.ToDateTime(@stopDate.Text) + new TimeSpan(Int32.Parse(t2[0]), Int32.Parse(t2[1]), Int32.Parse(t2[2]));
            
            #region B
            //Console.WriteLine(@operator.SelectedValue);
            //Console.WriteLine(@field.SelectedValue);
            //Console.WriteLine(@value.Text);
            //Console.WriteLine(isFloat);
            //Console.WriteLine("-----");
            //Console.WriteLine(@startDate.Text);
            //Console.WriteLine(@stopDate.Text);

            //Console.WriteLine((Convert.ToDateTime(@stopDate.Text) - Convert.ToDateTime(@startDate.Text)).TotalDays < 0);
            //Console.WriteLine((Convert.ToDateTime(@stopDate.Text) - Convert.ToDateTime(@startDate.Text)).TotalDays);
            //Console.WriteLine("-----");

            //Console.WriteLine(@time1.Text);
            //Console.WriteLine(@time2.Text);
            //Console.WriteLine(stringTimeDistance(@time1.Text, @time2.Text));
            //Console.WriteLine("-----");

            //Console.WriteLine(dateTime1);
            //Console.WriteLine(dateTime2);

            #endregion

            if (@startDate.SelectedDate == null || @stopDate.SelectedDate == null)
            {
                // Do what you have to do
                // read all table - [first date : end date]
                //SearchFirstAndEndDate();
            }
            else if ((Convert.ToDateTime(@stopDate.Text) - Convert.ToDateTime(@startDate.Text)).TotalDays < 0)
            {
                @errorMsg.Content = "* תקנו - תאריך התחלה צריך לבוא לפני תאריך סיום!";
            }
            else if(!stringTimeDistance(@time1.Text, @time2.Text))
            {
                @errorMsg.Content = "* תקנו - זמן התחלה צריך לבוא לפני זמן סיום!";
            }

            if (isFloat && isSelected())
            {
                queryClass query = new queryClass(dateTime1, dateTime2, @field.SelectedValue.ToString(), @operator.SelectedValue.ToString(), float.Parse(@value.Text));
            }
        }

        private bool isSelected()
        {
            if (@field.SelectedValue == null)
            {
                @errorMsg.Content = "* בחרו שדה!";
                return false;
            }

            if (@operator.SelectedValue == null)
            {
                @errorMsg.Content = "* בחרו אופרטור!";
                return false;
            }

            if (@operator.SelectedValue != null && @field.SelectedValue != null)
            {
                return true;
            }

            return false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            removeOperatorField(op);
            removeValueDataField(dataClass);
            init();
        }
    }
    
}
