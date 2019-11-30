using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancerApp
{
    public static class Utils
    {
        public static string MapRegionName(string name)
        {
            string[] regionNames = { "warmińsko-mazurskie", "pomorskie", "dolnośląskie", "zachodnio-pomorskie", "lubuskie", "wielkopolskie", "kujawsko-pomorskie", "śląskie", "łódzkie", "mazowieckie", "świętokrzyskie", "podlaskie", "lubelskie", "podkarpackie", "opolskie", "małopolskie" };
            //string[] codeName = { "POL02", "POL04", "POL06", "POL08", "POL10", "POL12", "POL14", "POL16", "POL18", "POL20", "POL22", "POL24", "POL26", "POL28", "POL30", "POL32" };
            string[] codeName = { "02", "04", "06", "08", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30", "32" };

            int i;
            for (i = 0; i < regionNames.Length; i++)
            {
                if (codeName[i].Equals(name))
                    return regionNames[i];
            }

            return name;

        }
    }
}
