using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SampleTracker.Droid.Services
{
    public class BaseForegroundService
    {
        public const string ACTION_START_SERVICE = "SampleTracker.action.START_SERVICE";
        public const string ACTION_STOP_SERVICE = "SampleTracker.action.STOP_SERVICE";
        public const string ACTION_MAIN_ACTIVITY = "SampleTracker.action.MAIN_ACTIVITY";
        public const string SERVICE_STARTED_KEY = "has_service_been_started";

        private Intent startServiceIntent;
        private Intent stopServiceIntent;

        protected void RegisterService<T>(Bundle args = null)
        {
            var context = Android.App.Application.Context;

            if (startServiceIntent == null)
            {
                startServiceIntent = new Intent(context, typeof(T));
                startServiceIntent.SetAction(ACTION_START_SERVICE);
            }

            if (stopServiceIntent == null)
            {
                stopServiceIntent = new Intent(context, typeof(T));
                stopServiceIntent.SetAction(ACTION_STOP_SERVICE);
            }

            if (args != null)
            {
                startServiceIntent.PutExtras(args);
            }

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                context.StartForegroundService(startServiceIntent);
            }
            else
            {
                context.StartService(startServiceIntent);
            }
        }

        protected void StopService<T>()
        {
            var context = Android.App.Application.Context;

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                context.StartForegroundService(stopServiceIntent);
            }
            else
            {
                context.StartService(stopServiceIntent);
            }
        }
    }
}