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

namespace CancerApp
{
    public class CheckedListItem
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public bool IsChecked { get; set; }
     
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<CheckedListItem> CheckedCancerList = new List<CheckedListItem>();
        public List<CheckedListItem> CheckedAgeList = new List<CheckedListItem>();

        DataWindow dataWnd;

        private bool isInitialised = false;
        private string defaultCondition = "Wszystkie";

        public MainWindow()
        {
            

            InitializeComponent();
            //dataWnd = new DataWindow();
            //dataWnd.Show();


            updateFilterParamComps();

            colorizeMap();

        }
        private IEnumerable<CheckedListItem> GetCheckedListItems(string[] array)
        {
            for(int i=0;i<array.Length;i++)
            {
                CheckedListItem item = new CheckedListItem();
                item.Id = 0;
                item.Name = array[i];
                item.IsChecked = true;

                yield return item;
            }
        }
        private void updateFilterParamComps(bool restart = true)
        {

            if (restart)
            {

                // listbox age ranges

                string[] ages = Global.Instance.ListOfData.Select(x => x.Age)
                    .Distinct().ToArray();

                CheckedAgeList.Clear();

                CheckedAgeList.AddRange(GetCheckedListItems(ages));

                listBoxAge.ItemsSource = CheckedAgeList;

                // listbox cancer type

                Dictionary<string, string> mapOfCancerT = Global.Instance.CancerTypesMap;
                List<string> cancers = new List<string>();
                foreach (var item in mapOfCancerT)
                {
                    cancers.Add(item.Key + " - " + item.Value);
                }

                CheckedCancerList.Clear();

                CheckedCancerList.AddRange(GetCheckedListItems(cancers.ToArray()));

                listBoxCancer.ItemsSource = CheckedCancerList;
                /*
                string[] cancers = Global.Instance.ListOfData.Select(x => x.Cancer)
                    .Distinct().ToArray();

                CheckedCancerList.Clear();

                CheckedCancerList.AddRange(GetCheckedListItems(cancers));

                listBoxCancer.ItemsSource = CheckedCancerList;*/



                // comboBox year
                int[] years = Global.Instance.ListOfData.Select(x => x.Year)
                    .Distinct().ToArray();
                comboBoxYearFrom.ItemsSource = years;
                comboBoxYearFrom.SelectedIndex = 0;
                comboBoxYearTo.ItemsSource = years;
                comboBoxYearTo.SelectedIndex = years.Length - 1;

               
                // combobox gender
                string[] genders = Global.Instance.ListOfData.Select(x => x.Gender)
                    .Distinct().ToArray();
                Array.Resize(ref genders, genders.Length + 1);
                genders[genders.Length - 1] = defaultCondition;
                comboBoxGender.ItemsSource = genders;
                comboBoxGender.SelectedIndex = genders.Length - 1;

                
          
                isInitialised = true;

            }
            else
            {

            }

        }
        private void colorizeMap()
        {
            List<Path> listPaths = new List<Path>();
            getLogicalChildCollection(this,listPaths);

            Dictionary<string, int> tempMap = new Dictionary<string, int>();

            foreach (Path p in listPaths)
            {
                string name = p.Name.Remove(0,3);
                List<Data> subList = Global.Instance.ListOfData.Where(x => x.Region.Equals(Utils.MapRegionName(name)) 
                                                                        && x.Year >= (int)comboBoxYearFrom.SelectedItem 
                                                                        && x.Year<= (int) comboBoxYearTo.SelectedItem
                                                                        && (x.Gender.Equals(comboBoxGender.SelectedItem) || comboBoxGender.SelectedItem.ToString()==defaultCondition)
                                                                        && CheckedCancerList.Select((y) => y.IsChecked && x.Cancer.Equals(y.Name.Substring(0,3))).Contains(true)
                                                                        && CheckedAgeList.Select((y) => y.IsChecked && x.Age.Equals(y.Name)).Contains(true)).ToList();

                int sum = subList.Sum(x => x.Number);
                tempMap.Add(p.Name, sum);
            }

            float max = tempMap.Sum(x=>x.Value);

            foreach(Path p in listPaths)
            {
                float sum = tempMap.Where(x => x.Key.Equals(p.Name)).FirstOrDefault().Value;
                float perc = (sum / max);

               // perc = (perc != 0f) ? perc : 0.05f;
                perc = (float)Math.Pow(1f - perc, 10);

                p.Fill = new SolidColorBrush(new Color() { ScR = 0.9f, ScG = perc, ScB = perc, ScA = 1f });

                //p.Fill = new SolidColorBrush(new Color() { ScR = perc, ScG = perc, ScB = 1f,  ScA =1f } );
            }

        }

        private static void getLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            var children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    getLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            colorizeMap();
        }

  

        private void comboBoxGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void btnSelectAllCancers_Click(object sender, RoutedEventArgs e)
        {
            if(CheckedCancerList.Select((x) => x.IsChecked).Contains(false))
            {
                CheckedCancerList.ForEach((x) => x.IsChecked = true);
            }
            else
            {
                CheckedCancerList.ForEach((x) => x.IsChecked = false);
            }

            listBoxCancer.Items.Refresh();
        }

        private void btnSelectAllAges_Click(object sender, RoutedEventArgs e)
        {
            if (CheckedAgeList.Select((x) => x.IsChecked).Contains(false))
            {
                CheckedAgeList.ForEach((x) => x.IsChecked = true);
            }
            else
            {
                CheckedAgeList.ForEach((x) => x.IsChecked = false);
            }

            listBoxAge.Items.Refresh();
        }

        private void comboBoxYearFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialised)
                return;

            if ((int)comboBoxYearTo.SelectedItem < (int)comboBoxYearFrom.SelectedItem)
                comboBoxYearTo.SelectedIndex = comboBoxYearFrom.SelectedIndex;
        }

        private void comboBoxYearTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialised)
                return;

            if ((int)comboBoxYearFrom.SelectedItem > (int)comboBoxYearTo.SelectedItem)
                comboBoxYearFrom.SelectedIndex = comboBoxYearTo.SelectedIndex;
        }

        private void btnLoadAllData_Click(object sender, RoutedEventArgs e)
        {
            string[] regions = Global.Instance.ListOfData.Select((x) => x.Region).Distinct().ToArray();

            StatisticsWindow wnd = Application.Current.Windows.OfType<StatisticsWindow>().FirstOrDefault();

            if (wnd != null)
            {
                wnd.Close();
            }
            wnd = new StatisticsWindow();

            foreach(string s in regions)
                wnd.AddDataFilter(s);

            wnd.UpdateData();
            wnd.Show();
        }
    }
}
