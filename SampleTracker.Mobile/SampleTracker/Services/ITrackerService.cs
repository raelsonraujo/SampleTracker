using SampleTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTracker.Services
{
    // This is a platform specific service. Please search for TrackerServiceDroid on the Android project and TrackerServiceIOS on iOS project
    public interface ITrackerService
    {
        /// <summary>
        /// Starts the tracking service and send GPS and Accelerometer data to the cloud. 
        /// This call takes a method as parameter for the parent implementation of what 
        /// happens then the service stops
        /// </summary>
        /// <param name="locationCallback">Method to send back the location reading</param>
        /// <param name="accelerometerCallback">Method to send back the accelerometer reading</param>
        /// <param name="stopCallback">Method call when the service stops</param>
        void StartListening(Action<LocationReading> locationCallback, Action<AccelerometerReading> accelerometerCallback, Action stopCallback);
        /// <summary>
        /// Stops the tracking service
        /// </summary>
        void StopListening();
        /// <summary>
        /// This method send the location reading to the listener
        /// </summary>
        /// <param name="reading">Reading data of GPS</param>
        void SendLocation(LocationReading reading);
        /// <summary>
        /// This method send the accelerometer reading to the listener
        /// </summary>
        /// <param name="reading">Reading data of accelerometer</param>
        void SendAccelerometer(AccelerometerReading reading);
    }
}
