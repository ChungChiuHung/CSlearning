using System;

namespace DelegateAndEvent_ex06
{

    public class Object1
    {
        public event EventHandler<WaterData> OnWaterData;

        protected virtual void OnWaterDataChanged(WaterData e)
        {
            OnWaterData?.Invoke(this, e);
        }

        public void UpdateWaterData(int temperature, int level)
        {
            WaterData data = new WaterData(temperature, level);
            OnWaterDataChanged(data);
        }
    }

    public class Object2
    {
        public Object2(Object1 obj)
        {
            obj.OnWaterData += Obj_OnWaterData;
        }

        private void Obj_OnWaterData(object sender, WaterData e)
        {
            Console.WriteLine("The Event is Raised From Object 1.");
        }
    }

    public class WaterLevelMonitor
    {
        public delegate void WaterDataChangedEventHandler(object sender, WaterDataChangedEventArgs e);

        public event WaterDataChangedEventHandler WaterDataChanged;

        private WaterData currentData;

        public void UpdateWaterData(int temperature, int level)
        {
            currentData = new WaterData(temperature, level);
            OnWaterDataChanged(new WaterDataChangedEventArgs(currentData));
        }

        protected virtual void OnWaterDataChanged(WaterDataChangedEventArgs e)
        {
            if (WaterDataChanged != null)
            {
                WaterDataChanged.Invoke(this, e);
            }
        }
        
    }

    public class WaterDataChangedEventArgs : EventArgs
    {
        public WaterData Data { get; }

        public WaterDataChangedEventArgs(WaterData data)
        {
            Data = data;
        }
    }

    public class WaterLevelDisplay
    {
        public WaterLevelDisplay(WaterLevelMonitor monitor)
        {
            monitor.WaterDataChanged += Monitor_WaterDataChanged;
        }

        private void Monitor_WaterDataChanged(object sender, WaterDataChangedEventArgs e)
        {
            WaterData data = e.Data;
            Console.WriteLine($"Water Level Display: {data.Level}");
            Console.WriteLine($"Water Temperature: {data.Temperature}");
        }
    }

    public class WaterData
    {
        public int Temperature { get; set; }
        public int Level { get; set; }

        public WaterData(int temperatrue, int level)
        {
            Temperature = temperatrue;
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
            monitor.UpdateWaterData(30, 60);

            Console.ReadLine();

            Object1 object1 = new Object1();
            Object2 object2 = new Object2(object1);

            WaterData waterData = new WaterData(25, 50); ;

            object1.UpdateWaterData(waterData.Temperature, waterData.Level);

            Console.ReadLine();

        }
    }
}
