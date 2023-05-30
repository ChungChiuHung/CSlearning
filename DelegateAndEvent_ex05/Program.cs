using System;

namespace DelegateAndEvent_ex05
{

    //public delegate void WaterDataDelegate(WaterData data);

    public class WaterLevelMonitor
    {
        public event Action<WaterData> WaterDataChanged;

        //public WaterDataDelegate WaterDataChanged;

        private WaterData currentData;

        public void UpdateWaterData(int temperature, int level)
        {
            currentData = new WaterData(temperature, level);
            OnWaterDataChanged(currentData);
        }

        protected virtual void OnWaterDataChanged(WaterData data)
        {
            WaterDataChanged?.Invoke(currentData);
        }
    }

    public class WaterLevelDisplay
    {
        public WaterLevelDisplay(WaterLevelMonitor monitor) 
        {
            monitor.WaterDataChanged += Monitor_WaterDataChanged;
        }

        private void Monitor_WaterDataChanged(WaterData data)
        {
            Console.WriteLine($"Water Level Display:{ data.Level}");
            Console.WriteLine($"Water Temperature Display: {data.Temperature}");
        }
    }

    public class WaterData
    {
        public int Temperature { get; set; }

        public int Level { get; set; }

        public WaterData(int temperature, int level)
        {
            Temperature = temperature;
            Level = level;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            WaterLevelMonitor monitor = new WaterLevelMonitor();
            WaterLevelDisplay display = new WaterLevelDisplay(monitor);

            monitor.UpdateWaterData(25, 50);
            monitor.UpdateWaterData(30,60);

            Console.ReadLine();
        }
    }
}
