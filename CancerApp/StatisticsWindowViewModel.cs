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
        private PlotModel plotModel3;
        private PlotModel plotModel4;


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

        public PlotModel PlotModel3
        {
            get
            {
                return plotModel3;
            }
            set
            {
                plotModel3 = value; OnPropertyChanged("PlotModel3");
            }
        }

        public PlotModel PlotModel4
        {
            get
            {
                return plotModel4;
            }
            set
            {
                plotModel4 = value; OnPropertyChanged("PlotModel4");
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
        private void SetupPlotModel2()
        {
            PlotModel2.Series.Clear();
            PlotModel2.Axes.Clear();



            PlotModel2.LegendTitle = "Legenda";
            PlotModel2.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel2.LegendPosition = LegendPosition.TopRight;
            PlotModel2.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel2.LegendBorder = OxyColors.Black;

            var yearAxis = new LinearAxis() { Position = AxisPosition.Bottom, Title = "Rok", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, Title = "Liczba zachorowań", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

            var collectionYear = DataList.Select(x => x.Year).Distinct().ToList();
            var collectionAge = DataList.Select(x => x.Age).Distinct().ToList();
            var lineSeriesAges = new LineSeries[collectionAge.Count];


            foreach (var age in collectionAge)
            {
                var series = new LineSeries()
                {
                    Title = age,
                };

                foreach (var year in collectionYear)
                {
                    series.Points.Add(new DataPoint(LinearAxis.ToDouble(year), LinearAxis.ToDouble(DataList.Where(x => x.Year.Equals(year) && x.Age.Equals(age)).Select(x => x.Number).Sum())));

                }
                //plotModel2.Axes.Add(valueAxis);
                //plotModel2.Axes.Add(yearAxis);
                PlotModel2.Series.Add(series);

            }

            PlotModel2.Axes.Add(yearAxis);
            PlotModel2.Axes.Add(valueAxis);
        }
        private void SetupPlotModel()
        {
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();

            

            PlotModel.LegendTitle = "Legenda";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var yearAxis = new LinearAxis() { Position = AxisPosition.Bottom, Title = "Rok", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, Title = "Liczba zachorowań", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };

            var collectionYear = DataList.Select(x => x.Year).Distinct().ToList();

            var lineSeriesYearNumber = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 1,
                Color = OxyColors.Red,
                RenderInLegend = true,
                Title="Ilość zachorowań"
            };

            foreach (var item in collectionYear)
            {
                lineSeriesYearNumber.Points.Add(new DataPoint(LinearAxis.ToDouble(item), DataList.Where(x => x.Year.Equals(item)).Select(x => x.Number).Sum()));
            }

            PlotModel.Series.Add(lineSeriesYearNumber);
            //dateAxis.StringFormat = "dd/MM/yy HH:mm";


            PlotModel.Axes.Add(yearAxis);
            PlotModel.Axes.Add(valueAxis);

        }
        //private void SetupModel()
        //{


        //    // AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm"
        //    //var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
        //    //dateAxis.Title = "Rok";





        //    //foreach (var item in collection)
        //    //{
        //    //    var lineSeries = new LineSeries
        //    //    {
        //    //        StrokeThickness = 2,
        //    //        MarkerSize = 1,
        //    //        LineStyle = LineStyle.Solid,
        //    //        LineJoin = LineJoin.Round,
        //    //        MarkerType = MarkerType.Circle,
        //    //        MarkerStroke = OxyColor.Parse("255,255,125,255"),

        //    //    };

        //    //    //collection.ForEach(x => lineSeries.Points.Add(new DataPoint(LinearAxis.ToDouble(item), LinearAxis.ToDouble(25))));
        //    //    lineSeries.Points.Add(new DataPoint(item, DataList.Where(x => x.Year.Equals(item)).Select(x => x.Number).Sum()));
        //    //    plotModel.Series.Add(lineSeries);
        //    //}









        //    // PLOT MODEL 2

        //    var collectionAge = DataList.Select(x => x.Age).Distinct().ToList();
        //    var lineSeriesAges = new LineSeries[collectionAge.Count];


        //    foreach (var item in collectionAge)
        //    {
        //        var series = new LineSeries();

        //        foreach (var item2 in collection)
        //        {
        //            series.Points.Add(new DataPoint(LinearAxis.ToDouble(item2), LinearAxis.ToDouble(DataList.Where(x => x.Year.Equals(item2) && x.Age.Equals(item)).Select(x => x.Number).Sum())));

        //        }

        //        //plotModel2.Axes.Add(valueAxis);
        //        //plotModel2.Axes.Add(yearAxis);
        //        PlotModel2.Series.Add(series);
        //    }


        //}



        private void SetupPlotModel3()
        {

            PlotModel3.Series.Clear();
            PlotModel3.Axes.Clear();

            var collectionGender = DataList.Select(x => x.Gender).Distinct().ToList();

            PlotModel3.Title = "Liczba zachorowań ze względu na płeć"; 

            dynamic seriesP1 = new PieSeries {StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            foreach (var gender in collectionGender)
            {
                seriesP1.Slices.Add(new PieSlice(gender.ToString(), DataList.Where(x => x.Gender.Equals(gender)).Select(x => x.Number).Sum()) { IsExploded = true });
            }

            PlotModel3.Series.Add(seriesP1);
        }



        private void SetupPlotModel4()
        {
            PlotModel4.Series.Clear();
            PlotModel4.Axes.Clear();

            PlotModel4.Title = "Ilość zachorowań ze wzgledu na płeć i wiek";
            //PlotModel4.Background = OxyColors.Gray;

            PlotModel4.LegendTitle = "Legenda";
            PlotModel4.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel4.LegendPosition = LegendPosition.TopRight;
            PlotModel4.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel4.LegendBorder = OxyColors.Black;
           
            CategoryAxis xaxis = new CategoryAxis { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            LinearAxis yaxis = new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Dot };

            ColumnSeries s1 = new ColumnSeries { IsStacked = true};
            ColumnSeries s2 = new ColumnSeries { IsStacked = true};


            var collectionAge = DataList.Select(x => x.Age).Distinct().ToList();

            foreach (var item in collectionAge)
            {
                xaxis.Labels.Add(item);

                s1.Items.Add(new ColumnItem(DataList.Where(x => x.Age.Equals(item) && x.Gender.Equals("K")).Select(x => x.Number).Sum()));
                s2.Items.Add(new ColumnItem(DataList.Where(x => x.Age.Equals(item) && x.Gender.Equals("M")).Select(x => x.Number).Sum()));
            }

            PlotModel4.Axes.Add(xaxis);
            PlotModel4.Axes.Add(yaxis);
            PlotModel4.Series.Add(s1);
            PlotModel4.Series.Add(s2);
        }

        public StatisticsWindowViewModel()
        {
            DataList = new List<Data>();

            PlotModel = new PlotModel();
            PlotModel2 = new PlotModel();
            PlotModel3 = new PlotModel();
            PlotModel4 = new PlotModel();
            //SetUpModel();

        }
        public void SetupModel(List<Data> data)
        {
            DataList = data;

            SetupPlotModel();
            SetupPlotModel2();
            SetupPlotModel3();
            SetupPlotModel4();

        }
    }
}
