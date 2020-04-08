using Newtonsoft.Json;
using SampleTracker.ViewModel;
using System;
using Xamarin.Forms;

namespace SampleTracker.Views.Pages
{
    public class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.Initialize();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.Leave();
        }
    }

    [QueryProperty(nameof(Model), nameof(Model))]
    public class BasePage<TViewModel, TModel> : ContentPage
        where TViewModel : BaseViewModel<TModel>
        where TModel : class
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.Initialize(ModelInstance);
        }

        public TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public TModel ModelInstance
        {
            get => _model;
            set { _model = value; }
        }
        private TModel _model;

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.Leave();
        }

        #region Navigation Properties

        public string Model
        {
            set
            {
                string jsonStr = Uri.UnescapeDataString(value);
                var model = JsonConvert.DeserializeObject<TModel>(jsonStr);

                ModelInstance = model;
            }
        }

        #endregion
    }
}
