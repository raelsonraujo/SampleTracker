using SampleTracker.Services;
using SampleTracker.ViewModel.Element;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampleTracker.ViewModel
{
    public class TrackerViewModel : BaseViewModel
    {
        #region Service Container

        private readonly ITrackerService trackerService = App.Current.trackerService;
        private readonly ILogService logService = App.Current.logService;
        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        #region Bindable Properties

        public bool IsTracking
        {
            get => _isTracking;
            set { _isTracking = value; NotifyPropertyChanged(); }
        }
        private bool _isTracking;

        public ObservableCollection<LocationElement> LocationReadings { get; } = new ObservableCollection<LocationElement>();

        public ObservableCollection<AccelerometerElement> AccelerometerReadings { get; } = new ObservableCollection<AccelerometerElement>();

        #endregion

        #region Commanding

        public ICommand TrackingTrigger => new Command(() => TrackingTriggerExecute());
        private async void TrackingTriggerExecute()
        {
            if (await RequestLocationPermission())
            {
                var log = logService.LogAction(() =>
                {
                    if (!IsTracking)
                    {
                        IsTracking = true;
                        trackerService.StartListening(
                            (reading) => InvokeMainThread(() => LocationReadings.Add(new LocationElement(reading))),
                            (reading) => InvokeMainThread(() => AccelerometerReadings.Add(new AccelerometerElement(reading))),
                            () => IsTracking = false);
                    }
                    else
                        trackerService.StopListening();
                });
                if (log != null) toastService.ShortAlert(log);
            }
            else
                await DisplayAlert("Permission", "You need to enable location permission in order to use this feature", "OK");
        }

        #endregion

        #region Helpers

        

        public async Task<bool> RequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if (status != PermissionStatus.Granted)
                status = await Permissions.RequestAsync<Permissions.LocationAlways>();

            return status == PermissionStatus.Granted;
        }

        #endregion

        #region Initializers

        public override void Initialize()
        {

        }

        public override void Leave()
        {

        }
        
        #endregion
    }
}
