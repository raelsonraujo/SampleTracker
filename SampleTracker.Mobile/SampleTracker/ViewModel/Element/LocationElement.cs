using SampleTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTracker.ViewModel.Element
{
    public class LocationElement : BaseElement<LocationReading>
    {
        public LocationElement() { }

        public LocationElement(LocationReading model) : base(model)
        {
            this.Latitude = model.Latitude.ToString();
            this.Longitude = model.Longitude.ToString();
            this.Timestamp = model.Timestamp;
        }

        #region Bindable Properties

        public string Latitude
        {
            get => _latitude;
            set { _latitude = value; NotifyPropertyChanged(); }
        }
        private string _latitude;

        public string Longitude
        {
            get => _longitude;
            set { _longitude = value; NotifyPropertyChanged(); }
        }
        private string _longitude;

        public string Timestamp
        {
            get => _timestamp;
            set { _timestamp = value; NotifyPropertyChanged(); }
        }
        private string _timestamp;

        #endregion
    }
}
