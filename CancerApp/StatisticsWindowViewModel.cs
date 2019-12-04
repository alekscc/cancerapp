using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace CancerApp
{
    class StatisticsWindowViewModel : INotifyPropertyChanged
    {
        public List<Data> DataList { get; set; } // dane


        private PlotModel plotModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlotModel PlotModel
        {
            get
            {
                return plotModel;
            }
            set
            {
                plotModel = value; OnPropertyChanged("PlotModel");
            }
        }

       
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            // AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm"
            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            dateAxis.Title = "Date";

            dateAxis.StringFormat = "dd/MM/yy HH:mm";
            PlotModel.Axes.Add(dateAxis);


        
        }

        public StatisticsWindowViewModel()
        {
            DataList = new List<Data>();

            PlotModel = new PlotModel();
            SetUpModel();
        }
    }
}
