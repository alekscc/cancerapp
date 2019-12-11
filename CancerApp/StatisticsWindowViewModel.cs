using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
        private PlotModel plotModel2;

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

        public PlotModel PlotModel2
        {
            get
            {
                return plotModel2;
            }
            set
            {
                plotModel2 = value; OnPropertyChanged("PlotModel2");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
        private void SetupScatterPlot()
        {
            //PlotModel.LegendTitle = "Legend";
            //PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            //PlotModel.LegendPosition = LegendPosition.TopRight;
            //PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            //PlotModel.LegendBorder = OxyColors.Black;

            //// AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm"
            ////var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            ////dateAxis.Title = "Rok";

            //var yearAxis = new LinearAxis() { Position = AxisPosition.Bottom, Title = "Rok", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

            //var valueAxis = new LinearAxis() { Position = AxisPosition.Left, Title = "Liczba", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

            //var collection = DataList.Select(x => x.Year).Distinct().ToList();
        }
        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            
            // AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm"
            //var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            //dateAxis.Title = "Rok";

            var yearAxis = new LinearAxis() { Position = AxisPosition.Bottom, Title = "Rok", MajorGridlineStyle=LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot};
        
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, Title="Liczba", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

            var collection = DataList.Select(x => x.Year).Distinct().ToList();



            //foreach (var item in collection)
            //{
            //    var lineSeries = new LineSeries
            //    {
            //        StrokeThickness = 2,
            //        MarkerSize = 1,
            //        LineStyle = LineStyle.Solid,
            //        LineJoin = LineJoin.Round,
            //        MarkerType = MarkerType.Circle,
            //        MarkerStroke = OxyColor.Parse("255,255,125,255"),

            //    };

            //    //collection.ForEach(x => lineSeries.Points.Add(new DataPoint(LinearAxis.ToDouble(item), LinearAxis.ToDouble(25))));
            //    lineSeries.Points.Add(new DataPoint(item, DataList.Where(x => x.Year.Equals(item)).Select(x => x.Number).Sum()));
            //    plotModel.Series.Add(lineSeries);
            //}

            var lineSeriesYearNumber = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 1,
                Color = OxyColors.Red,
                RenderInLegend = true,
            };

            foreach (var item in collection)
            {
                lineSeriesYearNumber.Points.Add(new DataPoint(LinearAxis.ToDouble(item), DataList.Where(x => x.Year.Equals(item)).Select(x => x.Number).Sum()));
            }


            
            PlotModel.Series.Add(lineSeriesYearNumber);
            //dateAxis.StringFormat = "dd/MM/yy HH:mm";

            
            PlotModel.Axes.Add(yearAxis);
            PlotModel.Axes.Add(valueAxis);




            // PLOT MODEL 2

            var collectionAge = DataList.Select(x => x.Age).Distinct().ToList();
            var lineSeriesAges = new LineSeries[collectionAge.Count];


            foreach (var item in collectionAge)
            {
                var series = new LineSeries();

                foreach (var item2 in collection)
                {
                    series.Points.Add(new DataPoint(LinearAxis.ToDouble(item2), LinearAxis.ToDouble(DataList.Where(x => x.Year.Equals(item2) && x.Age.Equals(item)).Select(x => x.Number).Sum())));

                }

                //plotModel2.Axes.Add(valueAxis);
                //plotModel2.Axes.Add(yearAxis);
                PlotModel2.Series.Add(series);
            }


        }


        public StatisticsWindowViewModel()
        {
            DataList = new List<Data>();

            PlotModel = new PlotModel();
            PlotModel2 = new PlotModel();
            //SetUpModel();

        }
        public void SetUpModel2(List<Data> data)
        {
            DataList = data;
            SetUpModel();
        }
    }
}
