using SampleTracker.Services;
using SampleTracker.ViewModel;
using SampleTracker.Views.Pages;
using Xamarin.Forms;

namespace SampleTracker
{
    public partial class App : Application
    {
        public static new App Current => (App)Application.Current;

        #region Services

        public IToastService toastService = DependencyService.Get<IToastService>();
        public ITrackerService trackerService = DependencyService.Get<ITrackerService>();

        public ILogService logService = new LogService();

        #endregion

        #region ViewModels

        public TrackerViewModel trackerViewModel;

        #endregion

        public App()
        {
            InitializeComponent();

            trackerViewModel = new TrackerViewModel();

            MainPage = new TrackerPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
