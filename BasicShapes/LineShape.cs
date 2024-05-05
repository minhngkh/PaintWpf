using Paint.WpfCommon;
using System.Windows;
using System.Windows.Shapes;

namespace BasicShapes
{
    public class LineShape : IShape
    {
        public string Name => "Line";

        public bool IsFillable => false;

        public UIElement Create(ShapeSpecifications specs)
        {
            var line = new Line();
            ApplySpecsToShape(line, specs);

            return line;
        }

        public void Transform(UIElement shape, ShapeSpecifications spces)
        {
            if (shape is not Line line)
            {
                throw new ArgumentException("shape is not a Line");
            }

            ApplySpecsToShape(line, spces);
        }

        private void ApplySpecsToShape(Line line, ShapeSpecifications specs)
        {
            line.X1 = specs.StartPoint.X;
            line.Y1 = specs.StartPoint.Y;
            line.X2 = specs.EndPoint.X;
            line.Y2 = specs.EndPoint.Y;
            line.Stroke = specs.StrokeBrush;
            line.StrokeThickness = specs.StrokeThickness;
            line.StrokeDashArray = specs.StrokeDashArray;
        }
    }
}
