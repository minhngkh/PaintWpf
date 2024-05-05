using System.Windows.Media;

namespace Paint.WpfCommon
{
    public class Strokes
    {
        public static DoubleCollection Solid { get; } = [];
        public static DoubleCollection Dot { get; } = [1, 2];

        public static DoubleCollection Dash { get; } = [4, 1];

        public static DoubleCollection DashDotDot { get; } = [4, 1, 1, 1, 1, 1];
    }
}
