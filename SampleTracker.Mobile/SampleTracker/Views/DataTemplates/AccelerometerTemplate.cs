using CSharpForMarkup;
using SampleTracker.Assets.Styles;
using SampleTracker.ViewModel.Element;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace SampleTracker.Views.DataTemplates
{
    public class AccelerometerTemplate
    {
        enum Row { X, Y, Z, Timestamp }

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
                        (Row.X, GridLength.Auto),
                        (Row.Y, GridLength.Auto),
                        (Row.Z, GridLength.Auto),
                        (Row.Timestamp, GridLength.Auto)),

                            RowSpacing = 5,
                            ColumnSpacing = 0,

                            Children =
                            {
                                new StackLayout
                                {
                                    Children =
                                    {
                                        new Label { Text = "X:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(AccelerometerElement.AccelerometerX)),

                                        new Label { Text = "Y:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(AccelerometerElement.AccelerometerY)),

                                        new Label { Text = "Z:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(AccelerometerElement.AccelerometerZ)),

                                        new Label { Text = "Timestamp:" } .HeaderStyle(),
                                        new Label { } .BodyStyle()
                                            .Bind(nameof(AccelerometerElement.Timestamp)),
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
