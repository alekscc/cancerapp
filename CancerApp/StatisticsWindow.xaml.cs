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
using System.Windows.Shapes;

namespace CancerApp
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private List<Data> listOfData;

        public List<string> DataFilters { get; private set; }

        public StatisticsWindow(List<Data> data)
        {
            InitializeComponent();
            DataFilters = new List<string>();


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
            //listOfData = Singleton.Instance.ListOfData.Where(x => DataFilters.Select(y => y.Equals(x.Region)).SkipWhile(z=>z ).FirstOrDefault() ).ToList();
            listOfData = Singleton.Instance.ListOfData.Where(x => DataFilters.Select(y => y.Equals(x.Region)).OrderBy(z => z).LastOrDefault()).ToList();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = listOfData;
        }
    }
}
