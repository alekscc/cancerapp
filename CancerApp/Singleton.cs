using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancerApp
{
    public sealed class Global
    {
        private static Global instance = null;
        private static readonly object padlock = new object();
        private static List<Data> listOfData;
        private static Dictionary<string, string> mapOfCancerTypes;

        Global()
        {
            listOfData = new List<Data>();

            using (var streamReader = File.OpenText("cancer.csv"))
            {
                var reader = new CsvReader(streamReader);
                reader.Configuration.RegisterClassMap<DataMap>();
                listOfData = reader.GetRecords<Data>().Where((x) => x.Number > 0 && !x.Cancer.Equals("c17") && !x.Cancer.Equals("c53") && !x.Cancer.Equals("c83")).ToList();

                loadCancerTypesMap();
            }

        }
        private void loadCancerTypesMap()
        {

            string[] data = File.ReadAllLines("cancer_types.csv",Encoding.Default).ToArray();

            mapOfCancerTypes = new Dictionary<string, string>();

            foreach (string line in data)
            {
                string[] vals = line.Split(';');
                mapOfCancerTypes.Add(vals[0], vals[1]);
            }
        }
        public List<Data> ListOfData
        {
            get
            {
                return listOfData;
            }
        }
        public Dictionary<string, string> CancerTypesMap
        {
            get
            {
                return mapOfCancerTypes;
            }
        }
        public string ConvertCancerType(string cancerType)
        {
            return mapOfCancerTypes.Where(x => x.Key.Equals(cancerType)).FirstOrDefault().Value;
        }
        public static Global Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Global();
                    }
                    return instance;
                }
            }
        }
    }
}
