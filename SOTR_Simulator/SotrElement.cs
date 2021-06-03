using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTR_Simulator
{
    abstract class SotrElement
    {
        public string Name { get; set; }
        public Dictionary<int, Sensors> packOfSensors = new Dictionary<int, Sensors>();
        public SotrElement(string Name,double[] SensorCurrency,int[] StatusSensors,int k )
        {
            this.Name = Name;
            for (int i = 0; i < k; i++)
            {
                Sensors sensor = new Sensors(SensorCurrency[i], StatusSensors[i]);
                packOfSensors.Add(i, sensor);
            }
        }
    }
}
