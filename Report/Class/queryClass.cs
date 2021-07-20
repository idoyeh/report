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
    public class queryClass
    {
        public DateTime FirstDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ValueField { get; set; }
        public string Operator { get; set; }
        public double Value { get; set; }

        public queryClass(DateTime firstDate, DateTime endDate, string valueField, string operator1, double value)
        {
            this.FirstDate = firstDate;
            this.EndDate = endDate;
            this.ValueField = valueField;
            this.Operator = operator1;
            this.Value = value;

        }
    }
}
