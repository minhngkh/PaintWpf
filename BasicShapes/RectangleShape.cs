
using Paint.WpfCommon;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BasicShapes
{
    public class RectangleShape : IShape, IFillable
    {
        public string Name => "Rectangle";

        public bool IsFillable => true;

        public UIElement Create(ShapeSpecifications specs)
        {
            var rectangle = new Rectangle();
            ApplySpecsToShape(rectangle, specs);

            return rectangle;
        }

        public void Transform(UIElement shape, ShapeSpecifications specifications)
        {
            if (shape is not Rectangle rectangle)
            {
                throw new ArgumentException("shape is not a Rectangle");
            }

            ApplySpecsToShape(rectangle, specifications);
        }

        private void ApplySpecsToShape(Rectangle rectangle, ShapeSpecifications specs)
        {
            var width = Math.Abs(specs.StartPoint.X - specs.EndPoint.X);
            var height = Math.Abs(specs.StartPoint.Y - specs.EndPoint.Y);

            Point trueEndPoint;
            if (specs.IsConstrained)
            {
                // a square
                width = height = Math.Min(width, height);
                trueEndPoint = new Point(
                    specs.StartPoint.X + width * Math.Sign(specs.EndPoint.X - specs.StartPoint.X),
                    specs.StartPoint.Y + width * Math.Sign(specs.EndPoint.Y - specs.StartPoint.Y)
                );
            }
            else
            {
                // a rectangle
                trueEndPoint = specs.EndPoint;
            }

            var x = Math.Min(specs.StartPoint.X, trueEndPoint.X);
            var y = Math.Min(specs.StartPoint.Y, trueEndPoint.Y);

            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Stroke = specs.StrokeBrush;
            rectangle.Fill = specs.FillBrush;
            rectangle.StrokeThickness = specs.StrokeThickness;
            rectangle.StrokeDashArray = specs.StrokeDashArray;

            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
        }
    }
}
