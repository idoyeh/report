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
        }

        private void init()
        {
            fillOperatorField(op);
            fillValueDataField(dataClass);

            @startDate.SelectedDate = null;
            @stopDate.SelectedDate = null;

            @time1.Text = "00:00:00";
            @time2.Text = "00:00:00";

            @value.Text = "";
            @errorMsg.Content = "";
        }

        private void fillOperatorField(OperatorClass op)
        {
            List<string> dt = OperatorClass.SearchOperator();

            for (int a = 0; a < dt.Count; a++)
            {
                @operator.Items.Add(dt[a]);
            }
        }

        private void removeOperatorField(OperatorClass op)
        {
            List<string> dt = OperatorClass.SearchOperator();

            for (int a = 0; a < dt.Count; a++)
            {
                @operator.Items.Remove(dt[a]);
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

        private bool checkValidTime(string first, string second)
        {
            string[] time1 = first.Split(':');
            string[] time2 = second.Split(':');

            TimeSpan dummyOutput;

            if (TimeSpan.TryParse(first, out dummyOutput) && TimeSpan.TryParse(second, out dummyOutput))
            {
                if ((Int32.Parse(time1[0]) < 0 || Int32.Parse(time1[0]) >= 24)
                || (Int32.Parse(time1[1]) < 0 || Int32.Parse(time1[1]) > 59)
                || (Int32.Parse(time1[2]) < 0 || Int32.Parse(time1[2]) > 59))
                {
                    return false;
                }

                if ((Int32.Parse(time2[0]) < 0 || Int32.Parse(time2[0]) >= 24)
                    || (Int32.Parse(time2[1]) < 0 || Int32.Parse(time2[1]) > 59)
                    || (Int32.Parse(time2[2]) < 0 || Int32.Parse(time2[2]) > 59))
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            bool isFloat = IsFloatnumber(@value.Text);
            bool isValidDate = false;
            bool isValidTime = true;
            DateTime dateTime1 = default(DateTime), dateTime2 = default(DateTime);
            double totalDay = 0;

            if (@startDate.SelectedDate != null)
            {
                string[] t1 = time1.Text.Split(':');
                dateTime1 = Convert.ToDateTime(@startDate.Text) + new TimeSpan(Int32.Parse(t1[0]), Int32.Parse(t1[1]), Int32.Parse(t1[2]));
            }

            if (@stopDate.SelectedDate != null)
            {
                string[] t2 = time2.Text.Split(':');
                dateTime2 = Convert.ToDateTime(@stopDate.Text) + new TimeSpan(Int32.Parse(t2[0]), Int32.Parse(t2[1]), Int32.Parse(t2[2]));
            }

            if (@startDate.SelectedDate == null || @stopDate.SelectedDate == null)
            {
                dateTime1 = DataClass.SearchFirstDate();
                dateTime2 = DataClass.SearchEndDate();
                isValidDate = true;
            }
            totalDay = (dateTime2 - dateTime1).TotalDays;

            if (totalDay <= -1)
            {
                @errorMsg.Content = "* תקנו - תאריך התחלה צריך לבוא לפני תאריך סיום!";
                isValidDate = false;
            }
            else if (totalDay > -1 && totalDay < 0)
            {
                @errorMsg.Content = "* תקנו - זמן התחלה צריך לבוא לפני זמן סיום!";
                isValidDate = false;
            }
            else
            {
                isValidDate = true;
            }

            if (!checkValidTime(@time1.Text, @time2.Text))
            {
                @errorMsg.Content = "* תקנו - זמן לא תקין!";
                isValidTime = false;
            }
            else
            {
                isValidTime = true;
            }

            if (isFloat && isSelected() && isValidDate && isValidTime)
            {
                queryClass query = new queryClass(dateTime1, dateTime2, @field.SelectedValue.ToString(), @operator.SelectedValue.ToString(), float.Parse(@value.Text));
                ReportTable reportTable = new ReportTable(query);
                reportTable.Show();
                this.Close();
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
