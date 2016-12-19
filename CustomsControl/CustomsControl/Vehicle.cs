using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomsControl
{
    public enum vehicleType{ car, truck, motorcycle}
    public class Vehicle
    {
        public int Weight { get; set; }
        public vehicleType Type { get; set; }
        public bool Environmental { get; set; }

        public Vehicle(int weight, vehicleType type, bool environmental)
        {
            Weight = weight;
            Type = type;
            Environmental = environmental;
        }
    }
}
