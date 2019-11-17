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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataWindow dataWnd;
        public MainWindow()
        {
            InitializeComponent();
            dataWnd = new DataWindow();
            dataWnd.Show();

            ColorizeMap();
        }
        private void ColorizeMap()
        {
            List<Path> listPaths = new List<Path>();
            GetLogicalChildCollection(this,listPaths);

            Dictionary<string, int> tempMap = new Dictionary<string, int>();

            foreach (Path p in listPaths)
            {
                string name = p.Name.Remove(0,3);
                List<Data> subList = Singleton.Instance.ListOfData.Where(x => x.Region.Equals(name)).ToList();
                int sum = subList.Sum(x => x.Number);
                tempMap.Add(p.Name, sum);
            }

            float max = tempMap.Sum(x=>x.Value);

            foreach(Path p in listPaths)
            {
                float sum = tempMap.Where(x => x.Key.Equals(p.Name)).FirstOrDefault().Value;
                p.Fill = new SolidColorBrush(new Color() { ScR = sum / max, ScG = sum / max, ScB = sum / max,  ScA =1f } );
            }

        }
        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
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
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

    }
}
