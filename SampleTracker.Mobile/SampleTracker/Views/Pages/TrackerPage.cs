using CSharpForMarkup;
using SampleTracker.Assets.Styles;
using SampleTracker.Assets.Theme;
using SampleTracker.Converters;
using SampleTracker.ViewModel;
using SampleTracker.Views.DataTemplates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace SampleTracker.Views.Pages
{
    public class TrackerPage : TabbedPage
    {
        enum Col { Location, Accelerometer}

        public TrackerPage() => Build();

        private void Build()
        {
            var app = App.Current;
            var trackerVM = app.trackerViewModel;

            NavigationPage.SetHasNavigationBar(this, false);
            BarBackgroundColor = Colors.Primary;
            BackgroundColor = Colors.TextIcons;

            Children.Add(new BasePage<TrackerViewModel>()
            {
                ViewModel = trackerVM,
                Title = "GPS",
                BackgroundColor = Colors.TextIcons,
                Content = new Grid()
                {
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                    Children =
                    {
                        new Button { Text = "Start" } .Standard()
                            .Center()
                            .Bind(Button.TextProperty, nameof(trackerVM.IsTracking), converter: new BoolTOString("Stop", "Start"))
                            .Bind(nameof(trackerVM.TrackingTrigger))
                    }
                }
            });

            Children.Add(new BasePage<TrackerViewModel>()
            {
                ViewModel = trackerVM,
                Title = "Dados",
                BackgroundColor = Colors.TextIcons,
                Content = new Grid()
                {
                    ColumnDefinitions = Columns.Define(
                        (Col.Location, GridLength.Star),
                        (Col.Accelerometer, GridLength.Star)),

                    RowSpacing = 0,
                    ColumnSpacing = 5,
                    Padding = new Thickness(5),
                    Children =
                    {
                        new CollectionView { ItemTemplate = LocationTemplate.New(), } .VerticalListStyle()
                        .Col(Col.Location)
                        .Bind(CollectionView.ItemsSourceProperty, nameof(trackerVM.LocationReadings)),

                        new CollectionView { ItemTemplate = AccelerometerTemplate.New(), } .VerticalListStyle()
                        .Col(Col.Accelerometer)
                        .Bind(CollectionView.ItemsSourceProperty, nameof(trackerVM.AccelerometerReadings)),
                    }
                }
            });
        }
    }
}
