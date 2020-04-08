using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using SampleTracker.Core;
using SampleTracker.Droid.Services;
using SampleTracker.Model;
using SampleTracker.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(TrackerServiceDroid))]
namespace SampleTracker.Droid.Services
{
    public class TrackerServiceDroid : BaseForegroundService, ITrackerService
    {
        public Action<LocationReading> locationCallback;
        public Action<AccelerometerReading> accelerometerCallback;
        public Action stopCallback;

        public void StartListening(Action<LocationReading> locationCallback, Action<AccelerometerReading> accelerometerCallback, Action stopCallback)
        {
            this.locationCallback = locationCallback;
            this.accelerometerCallback = accelerometerCallback;
            this.stopCallback = stopCallback;

            RegisterService<TrackerWorker>();
        }

        public void StopListening()
        {
            stopCallback?.Invoke();
            stopCallback = null;
            locationCallback = null;
            accelerometerCallback = null;

            StopService<TrackerWorker>();
        }

        public void SendLocation(LocationReading reading)
        {
            locationCallback?.Invoke(reading);
        }

        public void SendAccelerometer(AccelerometerReading reading)
        {
            accelerometerCallback?.Invoke(reading);
        }
    }

    [Service]
    public class TrackerWorker : Service
    {
        #region Service Container

        private readonly TrackerServiceDroid trackerService = (TrackerServiceDroid)App.Current.trackerService;

        #endregion

        // General fields
        bool isRunning = false;

        // Reading related fields
        private Timer updateLocationTimer = new Timer();
        private Stopwatch updateAccelerometerTimer;
        private object queueAccelerometerTimer = new object();

        // Notification Channel related fields
        private const int SERVICE_RUNNING_NOTIFICATION_ID = 696969;
        readonly string channelId = "sampletracker.channel";

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action.Equals(BaseForegroundService.ACTION_START_SERVICE))
            {
                if (!isRunning)
                {
                    var notification = new NotificationCompat.Builder(this, channelId)
                        .SetContentTitle("Sample Tracker")
                        .SetContentText("Your device is now feeding the app with GPS and Accelerometer data!")
                        .SetSmallIcon(Resource.Drawable.icon)
                        //.SetContentIntent(BuildIntentToShowMainActivity())
                        .SetOngoing(true)
                        .AddAction(BuildStopServiceAction())
                        .Build();

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                        CreateNotification();

                    // Start with a cache location reading and send to the listener
                    UpdateCacheLocationReading();

                    // Starts the foreground service for readings at each 10 second
                    StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
                    isRunning = true;
                    updateLocationTimer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
                    updateLocationTimer.Elapsed += UpdateLocationReading;
                    updateLocationTimer.Start();

                    updateAccelerometerTimer = Stopwatch.StartNew();
                    Accelerometer.ReadingChanged += UpdateAccelerometerReading;
                    Accelerometer.Start(SensorSpeed.UI);
                }
            }
            else if (intent.Action.Equals(BaseForegroundService.ACTION_STOP_SERVICE))
            {
                StopForeground(true);
                StopSelf();
                isRunning = false;

                updateLocationTimer.Elapsed -= UpdateLocationReading;
                updateLocationTimer.Stop();

                updateAccelerometerTimer?.Stop();
                Accelerometer.ReadingChanged -= UpdateAccelerometerReading;
                Accelerometer.Stop();

                trackerService.stopCallback?.Invoke();
                trackerService.stopCallback = null;
                trackerService.locationCallback = null;
                trackerService.accelerometerCallback = null;
            }

            return StartCommandResult.Sticky;
        }

        #region Notification Methods

        private void CreateNotification()
        {
            using (var notificationManager = NotificationManager.FromContext(ApplicationContext))
            {
                var channelName = channelId;
                NotificationChannel channel = null;
#if !DEBUG
                channel = notificationManager.GetNotificationChannel(channelName);
#endif
                channel = new NotificationChannel(channelName, channelName, NotificationImportance.High);
                channel.SetShowBadge(true);
                notificationManager.CreateNotificationChannel(channel);
                channel.Dispose();
            }
        }

        PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.SetAction(BaseForegroundService.ACTION_MAIN_ACTIVITY);
            notificationIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);
            notificationIntent.PutExtra(BaseForegroundService.SERVICE_STARTED_KEY, true);

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }

        NotificationCompat.Action BuildStopServiceAction()
        {
            var stopServiceIntent = new Intent(this, GetType());
            stopServiceIntent.SetAction(BaseForegroundService.ACTION_STOP_SERVICE);
            var stopServicePendingIntent = PendingIntent.GetService(this, 0, stopServiceIntent, 0);

            var builder = new NotificationCompat.Action.Builder(Android.Resource.Drawable.IcMediaPause, "Stop", stopServicePendingIntent);
            return builder.Build();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        #endregion


        #region Listener Methods

        private void UpdateCacheLocationReading()
        {
            Task.Run(async () =>
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                trackerService?.SendLocation(new LocationReading()
                {
                    Latitude = location?.Latitude ?? 0D,
                    Longitude = location?.Longitude ?? 0D,
                    Timestamp = DateTime.Now.ToString(Constants.DateTimeFormat),
                });
            });
        }

        private void UpdateLocationReading(object sender, ElapsedEventArgs args)
        {
            updateLocationTimer.Stop();
            Task.Run(async () =>
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest { DesiredAccuracy = GeolocationAccuracy.High, Timeout = TimeSpan.FromSeconds(9) });

                trackerService?.SendLocation(new LocationReading()
                {
                    Latitude = location?.Latitude ?? 0D,
                    Longitude = location?.Longitude ?? 0D,
                    Timestamp = DateTime.Now.ToString(Constants.DateTimeFormat),
                });

                updateLocationTimer.Start();
            });
        }

        private void UpdateAccelerometerReading(object sender, AccelerometerChangedEventArgs args)
        {
            lock (queueAccelerometerTimer)
            {
                if (updateAccelerometerTimer.ElapsedMilliseconds >= 1000)
                {
                    var accelerometer = new AccelerometerReading()
                    {
                        AccelerometerX = args.Reading.Acceleration.X,
                        AccelerometerY = args.Reading.Acceleration.Y,
                        AccelerometerZ = args.Reading.Acceleration.Z,
                        Timestamp = DateTime.Now.ToString(Constants.DateTimeFormat),
                    };

                    trackerService?.SendAccelerometer(accelerometer);
                    updateAccelerometerTimer.Restart();
                }
            }
        }

        #endregion
    }
}