using SampleTracker.Assets.Theme;
using Xamarin.Forms;

namespace SampleTracker.Assets.Styles
{
    public static class ButtonStyle
    {
        public static TButton Standard<TButton>(this TButton button) where TButton : Button
        {
            button.BackgroundColor = Colors.Accent;
            button.TextColor = Colors.TextIcons;
            button.Padding = new Thickness(5);
            button.CornerRadius = 6;

            return button;
        }
    }
}
