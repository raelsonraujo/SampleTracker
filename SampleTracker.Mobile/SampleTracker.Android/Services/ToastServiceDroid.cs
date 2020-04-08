using Android.Widget;
using SampleTracker.Droid.Services;
using SampleTracker.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastServiceDroid))]
namespace SampleTracker.Droid.Services
{
    public class ToastServiceDroid : IToastService
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}