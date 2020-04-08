using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTracker.Model
{
    public class AccelerometerReading
    {
        public string Id { get; set; }
        public double AccelerometerX { get; set; }
        public double AccelerometerY { get; set; }
        public double AccelerometerZ { get; set; }
        public string Timestamp { get; set; }
    }
}
