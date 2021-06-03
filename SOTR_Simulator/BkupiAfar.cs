using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTR_Simulator
{
    class BkupiAfar:SotrElement
    {
        new public string Name { get; set; }
        public BkupiAfar(string Name, double[] SensorCurrency, int[] SensorStatus, int k, double[] AverageTempGroup)
            :base(Name,SensorCurrency,SensorStatus,k)
        {
            Flag = k;

        }
        public int Flag { get; set; }
        public int QuantityOfIterations { get; set; }
        public int QuantityOfNotWorkingSensors { get; set; }
    }
}
