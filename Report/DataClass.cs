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
        public double Value { get; set; }
        public int Status { get; set; }
    }
}
