﻿using ChartServerConfiguration.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace testChartJs
{
    public partial class TimesheetHoursData : System.Web.UI.Page
    {
        protected string chartConfigString;
        private DataSet GetData()
        {
            DataTable table1 = new DataTable("timesheetHours");
            table1.Columns.Add("billableDur", typeof(decimal));
            table1.Columns.Add("nonbillableDur", typeof(decimal));
            table1.Columns.Add("date", typeof(DateTime));
            table1.Rows.Add(8.24, 0.70, new DateTime(2020, 1, 1));
            table1.Rows.Add(1.24, 3.70, new DateTime(2020, 1, 2));
            table1.Rows.Add(7.24, 0, new DateTime(2020, 1, 3));
            table1.Rows.Add(5, 3, new DateTime(2020, 1, 4));
            var ds = new DataSet();
            ds.Tables.Add(table1);
            return ds;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var chartConfig = CreateServerConfiguration();
            chartConfigString = chartConfig.MakeChart();
        }

        private ChartConfiguration CreateServerConfiguration()
        {
            var unit = "hrs";
            var dataSets = new List<DataSetItem>();

            DataSetItem dataSetItem = new DataSetItem()
            {
                Label = "Billable",
                Data = new List<int?> { 8, 1, 7, 5 },
                BackgroundColor = new List<string>() {"red", "red" , "red" , "red" }
            };
            dataSets.Add(dataSetItem);
            dataSetItem = new DataSetItem()
            {
                Label = "Non-Billable",
                Data = new List<int?> { 1, 3, 1, 5 },
                BackgroundColor = new List<string>() { "green", "green", "green", "green" }
            };
            dataSets.Add(dataSetItem);

            var chartConfig = new ChartConfiguration
            {
                Type = ChartType.bar.GetChartType(),
                Data =
                {
                    Labels = new List<string> { "Jan 1", "Jan 2", "Jan 3", "Jan 4" },
                    Datasets = dataSets
                },
                Options =
                {
                    Title =
                    {
                        Text = "My Test Chart"
                    },
                    Scales = new Scales()
                    {
                        XAxes = new List<AxesItem>()
                        {
                           new AxesItem(){stacked = true }
                        },
                        YAxes = new List<AxesItem>()
                        {
                           new AxesItem(){stacked = true }
                        }
                    }
                }
            };
            return chartConfig;
        }

    }
}