using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancerApp
{
    class DataMap : ClassMap<Data>
    {
        public DataMap()
        {
            Map(m => m.Year).Index(0);
            Map(m => m.Region).Index(1);
            Map(m => m.Gender).Index(2);
            Map(m => m.Cancer).Index(3);
            Map(m => m.Age).Index(4);
            Map(m => m.Number).Index(5);
        }
    }
}
