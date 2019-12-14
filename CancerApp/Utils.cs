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
            string[] codeName = { "02", "04", "06", "08", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30", "32" };

            int i;
            for (i = 0; i < regionNames.Length; i++)
            {
                if (codeName[i].Equals(name))
                    return regionNames[i];
            }

            return name;

        }
        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}
