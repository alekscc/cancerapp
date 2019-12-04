using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancerApp
{
    public class OxyPlotModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private OxyPlot.PlotModel plotModel;

        public OxyPlot.PlotModel PlotModel
        {
            get
            {
                return plotModel;
            }
            set
            {
                plotModel = value;
                OnPropertyChanged("PlotModel");
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }

            throw new NotImplementedException();
        }
    }
}
