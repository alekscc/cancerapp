using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancerApp
{
    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly object padlock = new object();
        private static List<Data> listOfData;

        Singleton()
        {
            listOfData = new List<Data>();

            using (var streamReader = File.OpenText("cancer.csv"))
            {
                var reader = new CsvReader(streamReader);
                reader.Configuration.RegisterClassMap<DataMap>();
                listOfData = reader.GetRecords<Data>().ToList();
            }
      
        }
        public List<Data> ListOfData
        {
            get
            {
                return listOfData;
            }
        }
        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
    }
}
