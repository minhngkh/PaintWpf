using Paint.WpfCommon;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BasicShapes
{
    public class EllipseShape : IShape
    {
        public string Name => "Ellipse";

        public bool IsFillable => true;

        public UIElement Create(ShapeSpecifications specs)
        {
            var ellipse = new Ellipse();
            ApplySpecsToShape(ellipse, specs);

            return ellipse;
        }

        public void Transform(UIElement shape, ShapeSpecifications specifications)
        {
            if (shape is not Ellipse ellipse)
            {
                throw new ArgumentException("shape is not a Rectangle");
            }

            ApplySpecsToShape(ellipse, specifications);
        }

        private void ApplySpecsToShape(Ellipse ellipse, ShapeSpecifications specs)
        {
            var width = Math.Abs(specs.StartPoint.X - specs.EndPoint.X);
            var height = Math.Abs(specs.StartPoint.Y - specs.EndPoint.Y);

            Point trueEndPoint;
            if (specs.IsConstrained)
            {
                // a circle
                width = height = Math.Min(width, height);
                trueEndPoint = new Point(
                    specs.StartPoint.X + width * Math.Sign(specs.EndPoint.X - specs.StartPoint.X),
                    specs.StartPoint.Y + width * Math.Sign(specs.EndPoint.Y - specs.StartPoint.Y)
                );
            }
            else
            {
                // an ellipse
                trueEndPoint = specs.EndPoint;
            }

            var x = Math.Min(specs.StartPoint.X, trueEndPoint.X);
            var y = Math.Min(specs.StartPoint.Y, trueEndPoint.Y);

            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.Stroke = specs.StrokeBrush;
            ellipse.Fill = specs.FillBrush;
            ellipse.StrokeThickness = specs.StrokeThickness;
            ellipse.StrokeDashArray = specs.StrokeDashArray;

            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
        }
    }
}
