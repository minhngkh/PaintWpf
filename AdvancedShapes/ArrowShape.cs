using Paint.WpfCommon;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdvancedShapes
{
    public class ArrowShape : IShape
    {
        public string Name => "Arrow";

        public bool IsFillable => true;

        public UIElement Create(ShapeSpecifications specifications)
        {
            var path = new Polygon();
            ApplySpecsToShape(path, specifications);

            return path;
        }

        public void Transform(UIElement shape, ShapeSpecifications specifications)
        {
            if (shape is not Polygon polygon)
            {
                throw new ArgumentException("shape is not an arrow");
            }

            ApplySpecsToShape(polygon, specifications);
        }

        private void ApplySpecsToShape(Polygon polygon, ShapeSpecifications specs)
        {
            polygon.Stroke = specs.StrokeBrush;
            polygon.Fill = specs.FillBrush;
            polygon.StrokeThickness = specs.StrokeThickness;
            polygon.StrokeDashArray = specs.StrokeDashArray;

            polygon.Points = DrawBlockRightArrow(specs.StartPoint, specs.EndPoint);
        }

        private PointCollection DrawBlockRightArrow(Point startPoint, Point endPoint)
        {
            double width = endPoint.X - startPoint.X;
            double height = endPoint.Y - startPoint.Y;
            double d = height / 3;
            double r = width / 3;

            PointCollection points =
            [
                new Point(startPoint.X, startPoint.Y + d),
                new Point(endPoint.X - r, startPoint.Y + d),
                new Point(endPoint.X - r, startPoint.Y),
                new Point(endPoint.X, startPoint.Y + height / 2),
                new Point(endPoint.X - r, endPoint.Y),
                new Point(endPoint.X - r, endPoint.Y - d),
                new Point(startPoint.X, endPoint.Y - d)
            ];

            return points;
        }
    }
}
