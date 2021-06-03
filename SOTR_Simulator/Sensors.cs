using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTR_Simulator
{
    class Sensors
    {
        public Sensors(double SensorsCurrency, int SensorStatus)
        {
            this.SensorsCurrency = SensorsCurrency;
            
            switch (SensorStatus)
            {
                case (int)Status_of_Sensors.Workable:
                    {
                        this.SensorStatus = "Workable";
                        break;
                    }
                case (int)Status_of_Sensors.Postpone:
                    {
                        this.SensorStatus = "Postpone";
                        break;
                    }
                case  (int)Status_of_Sensors.NotWorkable:
                    {
                        this.SensorStatus = "NotWorkable";
                        break;
                    }
                default:
                    break;
            }
        }
        public double SensorsCurrency { get; set; }
        public string SensorStatus { get; set; }
    }
    enum Status_of_Sensors
    {
        Workable = 1,
        Postpone,
        NotWorkable
    }
}
