using SampleTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTracker.ViewModel.Element
{
    public class AccelerometerElement : BaseElement<AccelerometerReading>
    {
        public AccelerometerElement() { }

        public AccelerometerElement(AccelerometerReading model) : base(model)
        {
            this.AccelerometerX = model.AccelerometerX;
            this.AccelerometerY = model.AccelerometerY;
            this.AccelerometerZ = model.AccelerometerZ;
            this.Timestamp = model.Timestamp;
        }

        #region Bindable Properties

        public double AccelerometerX
        {
            get => _accelerometerX;
            set { _accelerometerX = value; NotifyPropertyChanged(); }
        }
        private double _accelerometerX;

        public double AccelerometerY
        {
            get => _accelerometerY;
            set { _accelerometerY = value; NotifyPropertyChanged(); }
        }
        private double _accelerometerY;

        public double AccelerometerZ
        {
            get => _accelerometerZ;
            set { _accelerometerZ = value; NotifyPropertyChanged(); }
        }
        private double _accelerometerZ;

        public string Timestamp
        {
            get => _timestamp;
            set { _timestamp = value; NotifyPropertyChanged(); }
        }
        private string _timestamp;

        #endregion
    }
}
