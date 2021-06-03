using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTR_Simulator
{
    class CarcassB:SotrElement
    {
        new public string Name { get; set; }
        public CarcassB(string Name,double[] SensorCurrency,int[] SensorStatus,int k,double[] AverageTempGroup)
            : base(Name, SensorCurrency, SensorStatus, k)
        {
            for (int i = 0; i < 3; i++)
            {
                this.AverageTempGroup[i] = AverageTempGroup[i];
            }
        }
        double[] AverageTempGroup = new double[3];
        public double MaxPanelTempreture { get; set; }
        public double MinPanelTempreture { get; set; }
        public double AverageTempPanel { get; set; }
        public int QuantityOfNotWorking { get; set; }
    }
}
