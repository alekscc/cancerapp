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


            StatisticsWindow curWnd = Application.Current.Windows.OfType<StatisticsWindow>().FirstOrDefault();

            if (curWnd == null)
            {
                curWnd = new StatisticsWindow();
            }

            curWnd.AddDataFilter(Utils.MapRegionName(p.ToString()));
            curWnd.UpdateData();
            curWnd.Show();

        }
    }
}
