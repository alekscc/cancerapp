using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CancerApp
{
    public class WplViewModel
    {
        private ICommand _leftButtonDownCommand;


        public ICommand LeftMouseButtonDown
        {
            get
            {
                return _leftButtonDownCommand ?? (_leftButtonDownCommand = new RelayCommand(
                   x =>
                   {
                       DoStuff(x);
                   }));
            }
        }

        private static void DoStuff(object p)
        {
            MessageBox.Show("Responding to left mouse button click event..." + p.ToString());
            List<Data> query = Singleton.Instance.ListOfData.Where(x => x.Region.Equals(p.ToString())).ToList();
            StatisticsWindow wnd = new StatisticsWindow(query);
            wnd.Show();
        }
    }
}
