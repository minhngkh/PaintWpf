using System.Windows;
using System.Windows.Media;

namespace Paint.WpfCommon
{
    public class ShapeSpecifications(
        Brush strokeBrush, Brush fillBrush, double strokeThickness,
        DoubleCollection strokeDashArray
    )
    {
        private Point _startPoint;
        public Point StartPoint { get => _startPoint; set { _startPoint = value; } }
        public Point EndPoint { get; set; }
        public Brush StrokeBrush { get; set; } = strokeBrush;
        public Brush FillBrush { get; set; } = fillBrush;
        public double StrokeThickness { get; set; } = strokeThickness;
        public DoubleCollection StrokeDashArray { get; set; } = strokeDashArray;
        public bool IsConstrained { get; set; }
    }
}
