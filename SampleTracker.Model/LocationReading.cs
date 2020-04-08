using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTracker.Model
{
    public class LocationReading
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Timestamp { get; set; }
    }
}
