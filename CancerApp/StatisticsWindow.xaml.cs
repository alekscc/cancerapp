using System;
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
        //private List<Data> dataList;

        public List<string> DataFilters { get; private set; }

        private StatisticsWindowViewModel viewModel;

        public StatisticsWindow(List<Data> data)
        {

            
            InitializeComponent();
            DataFilters = new List<string>();

      
        
        }
        public StatisticsWindow()
        {
            viewModel = new StatisticsWindowViewModel();
            DataContext = viewModel;

            InitializeComponent();
            DataFilters = new List<string>();

            
            //Plot1.DataContext = viewModel;
            //viewModel = (StatisticsWindowViewModel) DataContext;
            // viewModel = new StatisticsWindowViewModel();
            //DataContext = viewModel;



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

            viewModel.SetupModel(Global.Instance.ListOfData.Where(x => DataFilters.Select(y => y.Equals(x.Region)).OrderBy(z => z).LastOrDefault()).ToList());
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = viewModel.DataList;

            //int max = viewModel.DataList.Max(x => x.Number);
            //string icd = viewModel.DataList.Where(x => x.Number == max).First().Cancer;

            //MessageBox.Show("icd converted = " + Global.Instance.ConvertCancerType(icd));
          
        }
    }
}
