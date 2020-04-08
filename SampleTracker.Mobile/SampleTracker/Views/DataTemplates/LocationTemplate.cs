using CSharpForMarkup;
using SampleTracker.Assets.Styles;
using SampleTracker.ViewModel.Element;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace SampleTracker.Views.DataTemplates
{
    public class LocationTemplate
    {
        enum Row { Latitude, Longitude, Timestamp }

        public static DataTemplate New() => new DataTemplate(() =>
        {
            var grid = new Grid
            {
                Padding = 2,
                Children =
                {
                    new Frame
                    {
                        Content = new Grid()
                        {
                            RowDefinitions = Rows.Define(
                            (Row.Latitude, GridLength.Auto),
                            (Row.Longitude, GridLength.Auto),
                            (Row.Timestamp, GridLength.Auto)),

                            RowSpacing = 5,
                            ColumnSpacing = 0,

                            Children =
                            {
                                new StackLayout
                                {
                                    Children =
                                    {
                                        new Label { Text = "Latitude:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(LocationElement.Latitude)),

                                        new Label { Text = "Longitude:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(LocationElement.Longitude)),

                                        new Label { Text = "Timestamp:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(LocationElement.Timestamp)),
                                    },
                                },
                            },
                        }
                    } .Standard()
                }
            };
            return grid;
        });
    }
}
