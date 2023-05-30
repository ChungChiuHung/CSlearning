using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateAndEvent_ex03
{
    public delegate void ValueChangeEventHandler(int value);


    public class DataProcessor
    {
        private int data;

        public event ValueChangeEventHandler ValueChange;

        public void StartProcessing()
        {
            Random random = new Random();

            while (true)
            {
                int newValue = random.Next(100);
                Console.WriteLine($"DataProcessor: Generated new value : {newValue}");

                if(data!=newValue)
                {
                    data = newValue;
                    OnValueChanged(data);
                }

                Thread.Sleep(2000);
            }

        }

        protected virtual void OnValueChanged(int newValue)
        {
            if(ValueChange!= null)
            {
                ValueChange.Invoke(newValue);
            }

            // ValueChange?.Invoke(newValue);
        }

        public int GetValues()
        {
            return data;
        }
    }

    public class DataViewer
    {
        public event ValueChangeEventHandler ValueChanged;

        public void StartViewing(DataProcessor dataProcessor)
        {
            dataProcessor.ValueChange += HandleValueChanged;
            Thread viewerThread = new Thread(() =>
            {
                MonitorData(dataProcessor);
            });

            viewerThread.Start();

        }

        private void MonitorData(DataProcessor dataProcessor)
        {
            while (true) 
            {
                int currentValue = dataProcessor.GetValues();
                Console.WriteLine($"DataViewer: currentValue value is {currentValue}");

                Thread.Sleep(3000);
            }
        }

        public void HandleValueChanged(int newValue)
        {
            ValueChanged?.Invoke(newValue);
        }
    }

    public class BufferThread
    {
        private DataProcessor dataProcessor;
        private DataViewer dataViewer;

        public BufferThread(DataProcessor dataProcessor, DataViewer viewer)
        {
            this.dataProcessor = dataProcessor;
            this.dataViewer = viewer;
        }

        public void StartBuffering()
        {
            Thread bufferThread = new Thread(() =>
            {
                BufferData();
            });

            bufferThread.Start();
        }

        private void BufferData()
        {
            while (true)
            {
                int currentValue = dataProcessor.GetValues();
                Console.WriteLine($"BufferThread: Buffering value: {currentValue}");

                // Perform some buffering or intermediate processing of the data

                // Simulate a delay in buffering
                Thread.Sleep(1000);

                // Pass the buffered value to the data viewer
                dataViewer.HandleValueChanged(currentValue);
            }
            
        }

        private void BufferValueChanged(int newValue)
        {
            Console.WriteLine($"BufferThread: Value changed to {newValue}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DataProcessor dataProcessor = new DataProcessor();
            DataViewer dataViewer = new DataViewer();
            BufferThread bufferThread = new BufferThread(
                dataProcessor, dataViewer);

            Thread processorThread = new Thread(dataProcessor.StartProcessing);
            Thread viewerThread = new Thread(() =>
            dataViewer.StartViewing(dataProcessor));

            processorThread.Start();
            viewerThread.Start();
            bufferThread.StartBuffering();

            
            // Wait for the thread to finish
            processorThread.Join();
            viewerThread.Join();

            Console.ReadLine();
        }
    }
}
