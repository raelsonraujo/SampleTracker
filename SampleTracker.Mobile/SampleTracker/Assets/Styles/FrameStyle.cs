using SampleTracker.Assets.Theme;
using Xamarin.Forms;

namespace SampleTracker.Assets.Styles
{
    public static class FrameStyle
    {
        public static TFrame Standard<TFrame>(this TFrame frame) where TFrame : Frame
        {
            frame.BackgroundColor = Colors.Accent;
            frame.Padding = new Thickness(5);
            frame.CornerRadius = 6;

            return frame;
        }
    }
}
