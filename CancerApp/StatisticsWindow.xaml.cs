﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace CancerApp
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {

       

        private List<Data> dataList;


        public List<string> DataFilters { get; private set; }

        private StatisticsWindowViewModel viewModel;

        public StatisticsWindow(List<Data> data)
        {
            viewModel = new StatisticsWindowViewModel();
            DataContext = viewModel;
            
            InitializeComponent();
            DataFilters = new List<string>();

            //oxyPlotModel = new OxyPlotModel();
            //this.DataContext = oxyPlotModel;
        
        }
        public StatisticsWindow()
        {
            InitializeComponent();
            DataFilters = new List<string>();

        }

        public void AddDataFilter(string filter)
        {
            DataFilters.Add(filter);
            //UpdateData();
        }
        public void UpdateData()
        {
            Title = "";
            foreach (string filterName in DataFilters)
            {
                Title += filterName + ", ";
            }

            dataList = Singleton.Instance.ListOfData.Where(x => DataFilters.Select(y => y.Equals(x.Region)).OrderBy(z => z).LastOrDefault()).ToList();
            //MessageBox.Show("Found record: " + dataList.Count + " data filter [0]:" + DataFilters[0]);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = dataList;
        }
    }
}
