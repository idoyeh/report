﻿using System;
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
    public partial class ReportTable : Window
    {
        testLINQDataContext db = new testLINQDataContext(Properties.Settings.Default.testConnectionString);

        public queryClass queryReportTable { get; set; }
        public List<DataClass> dataClass { get; set; }

        public ReportTable()
        {
            InitializeComponent();
            //dataClass = List<DataClass>();
            //dataClass = SearchDataClass();
            //MakeDataTables();
        }

        public ReportTable(queryClass query)
        {
            InitializeComponent();
            queryReportTable = query;
            //dataClass = SearchDataClass();
            //MakeDataTables();
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

        //    public static List<DataClass> SearchDataClass()
        //    {
        //        // Database Connection
        //        ReportTable query1 = new ReportTable();
        //        List<DataClass> dataClass1 = new List<DataClass>();

        //        try
        //        {
        //            dataClass1 = query1.db.DATAs.Where(d => d.Date_Time >= queryReportTable.FirstDate && d.Date_Time <= queryReportTable.EndDate).ToList();

        //            for (var i = 0; i < strOperator.Count; i++)
        //            {
        //                Console.Write("strOperator -> ");
        //                Console.WriteLine(strOperator);
        //            }
        //            Console.WriteLine("-----------------------");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        finally
        //        {
        //        }
        //        return strOperator;

        //    }

        //    private void MakeDataTables()
        //    {
        //        // Run all of the functions.
        //        MakeParentTable();
        //        MakeChildTable();
        //        MakeDataRelation();
        //        BindToDataGrid();
        //    }

        //    private void MakeParentTable()
        //    {
        //        // Create a new DataTable.
        //        System.Data.DataTable table = new DataTable("ParentTable");
        //        // Declare variables for DataColumn and DataRow objects.
        //        DataColumn column;
        //        DataRow row;

        //        // Create new DataColumn, set DataType,
        //        // ColumnName and add to DataTable.
        //        column = new DataColumn();
        //        column.DataType = System.Type.GetType("System.DateTime");
        //        column.ColumnName = "Date_Time";
        //        column.ReadOnly = true;
        //        column.Unique = true;
        //        // Add the Column to the DataColumnCollection.
        //        table.Columns.Add(column);

        //        for (int i = 1; i <= queryReportTable.NomOfClmn; i++)
        //        {
        //            // Create second column.
        //            column = new DataColumn();
        //            column.DataType = System.Type.GetType("System.Decimal");
        //            column.ColumnName = "Value" + i;
        //            column.AutoIncrement = false;
        //            column.Caption = "ParentItem";
        //            column.ReadOnly = false;
        //            column.Unique = false;
        //            // Add the column to the table.
        //            table.Columns.Add(column);
        //        }

        //        // Make the ID column the primary key column.
        //        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        //        PrimaryKeyColumns[0] = table.Columns["Date_Time"];
        //        table.PrimaryKey = PrimaryKeyColumns;

        //        // Instantiate the DataSet variable.
        //        dataSet = new DataSet();
        //        // Add the new DataTable to the DataSet.
        //        dataSet.Tables.Add(table);

        //        // Create three new DataRow objects and add
        //        // them to the DataTable
        //        for (int i = 0; i <= 2; i++) // replace with range date
        //        {
        //            row = table.NewRow();
        //            row["Date_Time"] = i;
        //            row["ParentItem"] = "ParentItem " + i;
        //            table.Rows.Add(row);
        //        }
        //    }

        //    private void MakeChildTable()
        //    {
        //        // Create a new DataTable.
        //        DataTable table = new DataTable("childTable");
        //        DataColumn column;
        //        DataRow row;

        //        // Create first column and add to the DataTable.
        //        column = new DataColumn();
        //        column.DataType = System.Type.GetType("System.Int32");
        //        column.ColumnName = "ChildID";
        //        column.AutoIncrement = true;
        //        column.Caption = "ID";
        //        column.ReadOnly = true;
        //        column.Unique = true;

        //        // Add the column to the DataColumnCollection.
        //        table.Columns.Add(column);

        //        // Create second column.
        //        column = new DataColumn();
        //        column.DataType = System.Type.GetType("System.String");
        //        column.ColumnName = "ChildItem";
        //        column.AutoIncrement = false;
        //        column.Caption = "ChildItem";
        //        column.ReadOnly = false;
        //        column.Unique = false;
        //        table.Columns.Add(column);

        //        // Create third column.
        //        column = new DataColumn();
        //        column.DataType = System.Type.GetType("System.Int32");
        //        column.ColumnName = "ParentID";
        //        column.AutoIncrement = false;
        //        column.Caption = "ParentID";
        //        column.ReadOnly = false;
        //        column.Unique = false;
        //        table.Columns.Add(column);

        //        dataSet.Tables.Add(table);

        //        // Create three sets of DataRow objects,
        //        // five rows each, and add to DataTable.
        //        for (int i = 0; i <= 4; i++)
        //        {
        //            row = table.NewRow();
        //            row["childID"] = i;
        //            row["ChildItem"] = "Item " + i;
        //            row["ParentID"] = 0;
        //            table.Rows.Add(row);
        //        }
        //        for (int i = 0; i <= 4; i++)
        //        {
        //            row = table.NewRow();
        //            row["childID"] = i + 5;
        //            row["ChildItem"] = "Item " + i;
        //            row["ParentID"] = 1;
        //            table.Rows.Add(row);
        //        }
        //        for (int i = 0; i <= 4; i++)
        //        {
        //            row = table.NewRow();
        //            row["childID"] = i + 10;
        //            row["ChildItem"] = "Item " + i;
        //            row["ParentID"] = 2;
        //            table.Rows.Add(row);
        //        }
        //    }

        //    private void MakeDataRelation()
        //    {
        //        // DataRelation requires two DataColumn
        //        // (parent and child) and a name.
        //        DataColumn parentColumn =
        //            dataSet.Tables["ParentTable"].Columns["id"];
        //        DataColumn childColumn =
        //            dataSet.Tables["ChildTable"].Columns["ParentID"];
        //        DataRelation relation = new
        //            DataRelation("parent2Child", parentColumn, childColumn);
        //        dataSet.Tables["ChildTable"].ParentRelations.Add(relation);
        //    }

        //    private void BindToDataGrid()
        //    {
        //        // Instruct the DataGrid to bind to the DataSet, with the
        //        // ParentTable as the topmost DataTable.
        //        dataGrid1.SetDataBinding(dataSet, "ParentTable");
        //    }

        //}

    }
}