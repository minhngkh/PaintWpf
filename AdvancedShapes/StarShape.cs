using Paint.WpfCommon;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdvancedShapes
{
    public class StarShape : IShape
    {
        public string Name => "Star";

        public bool IsFillable => true;

        public UIElement Create(ShapeSpecifications specs)
        {
            var star = new Path();
            ApplySpecsToShape(star, specs);
            return star;
        }

        public void Transform(UIElement shape, ShapeSpecifications specifications)
        {
            if (shape is not Path star)
            {
                throw new ArgumentException("shape is not a star");
            }

            ApplySpecsToShape(star, specifications);
        }

        private void ApplySpecsToShape(Path path, ShapeSpecifications specs)
        {
            Point trueEndPoint;
            if (specs.IsConstrained)
            {
                // fit in square
                double min = Math.Min(
                    Math.Abs(specs.StartPoint.X - specs.EndPoint.X),
                    Math.Abs(specs.StartPoint.Y - specs.EndPoint.Y)
                );
                trueEndPoint = new Point(
                    specs.StartPoint.X + min * Math.Sign(specs.EndPoint.X - specs.StartPoint.X),
                    specs.StartPoint.Y + min * Math.Sign(specs.EndPoint.Y - specs.StartPoint.Y)
                );
            }
            else
            {
                // fit in rectangle
                trueEndPoint = specs.EndPoint;
            }

            path.Stroke = specs.StrokeBrush;
            path.Fill = specs.FillBrush;
            path.StrokeThickness = specs.StrokeThickness;
            path.StrokeDashArray = specs.StrokeDashArray;

            path.Data = CreateStarGeometry(specs.StartPoint, trueEndPoint);
        }

        private Geometry CreateStarGeometry(Point start, Point end)
        {
            double width = end.X - start.X;
            double height = end.Y - start.Y;
            double centerX = start.X + width / 2;
            double centerY = start.Y + height / 2;
            double outerRadius = Math.Min(width, height) / 2;

            // Adjust this to change the look of the star
            double innerRadius = outerRadius * 0.5;
            int points = 5;

            double angleStep = Math.PI / points;
            double startAngle = -Math.PI / 2;
            PointCollection collection = [];

            for (int i = 0; i < 2 * points; i++)
            {
                double r = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = startAngle + i * angleStep;

                double x = centerX + r * Math.Cos(angle);
                double y = centerY + r * Math.Sin(angle);
                collection.Add(new Point(x, y));
            }

            var figure = new PathFigure()
            {
                StartPoint = collection[0],
                IsClosed = true
            };

            foreach (var point in collection)
                figure.Segments.Add(new LineSegment(point, true));

            var geometry = new PathGeometry();

            geometry.Figures.Add(figure);

            Rect rect = geometry.Bounds;
            geometry.Transform = new ScaleTransform(
                Math.Abs(end.X - start.X) / rect.Width,
                Math.Abs(end.Y - start.Y) / rect.Height,
                centerX,
                centerY
            );

            return geometry;
        }

        private void _ApplySpecsToShape(Polygon polygon, ShapeSpecifications specs)
        {
            double width = specs.EndPoint.X - specs.StartPoint.X;
            double height = specs.EndPoint.Y - specs.StartPoint.Y;
            double centerX = specs.StartPoint.X + width / 2;
            double centerY = specs.StartPoint.Y + height / 2;
            double outerRadius = Math.Min(width, height) / 2;

            // Adjust this to change the look of the star
            double innerRadius = outerRadius * 0.4;
            int points = 5;

            //// stretch 
            //double factor = Math.Sin(Math.PI / points) / (1 + Math.Sin(Math.PI / points));
            //outerRadius = outerRadius / factor;
            //innerRadius = outerRadius * 0.4;

            polygon.Stroke = Brushes.Black;
            polygon.Fill = Brushes.Yellow;
            polygon.StrokeThickness = 2;


            double angleStep = Math.PI / points;
            double startAngle = -Math.PI / 2;
            PointCollection collection = new PointCollection();

            for (int i = 0; i < 2 * points; i++)
            {
                double r = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = startAngle + i * angleStep;

                double x = centerX + r * Math.Cos(angle);
                double y = centerY + r * Math.Sin(angle);
                collection.Add(new Point(x, y));
            }

            polygon.Points = collection;
        }
    }
}
