using SampleTracker.Assets.Theme;
using Xamarin.Forms;

namespace SampleTracker.Assets.Styles
{
    public static class LabelStyle
    {
        public static TLabel HeaderStyle<TLabel>(this TLabel label) where TLabel : Label
        {
            label.FontAttributes = FontAttributes.Bold;
            label.FontSize = 20;
            label.TextColor = Colors.TextIcons;

            return label;
        }

        public static TLabel BodyStyle<TLabel>(this TLabel label) where TLabel : Label
        {
            label.FontSize = 14;
            label.TextColor = Colors.TextIcons;

            return label;
        }
    }
}
