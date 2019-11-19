using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CancerApp
{
    class MainWindowViewModel
    {
        private ICommand _leftButtonDownCommand;

        public ICommand LeftMouseButtonDown
        {
            get
            {
                return _leftButtonDownCommand ?? (_leftButtonDownCommand = new RelayCommand(
                   x =>
                   {
                       CreateStatisticsWindow(x);
                   }));
            }
        }

        private static void CreateStatisticsWindow(object p)
        {


            StatisticsWindow wnd = Application.Current.Windows.OfType<StatisticsWindow>().FirstOrDefault();

            if (wnd == null)
            {
                wnd = new StatisticsWindow();
            }

            //List<Data> query = Singleton.Instance.ListOfData.Where(x => x.Region.Equals(p.ToString())).ToList();
            

            wnd.AddDataFilter(p.ToString());
            wnd.UpdateData();
            wnd.Show();
           
        }
    }
}
