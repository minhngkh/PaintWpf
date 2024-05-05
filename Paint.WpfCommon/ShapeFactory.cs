using System.Windows;

namespace Paint.WpfCommon
{
    public class ShapeFactory
    {
        public IShape shape { private get; set; }

        public UIElement Create(ShapeSpecifications specifications)
        {
            return shape.Create(specifications);
        }

        public UIElement Transform(UIElement shape, ShapeSpecifications specifications)
        {
            return shape;
        }
    }
}