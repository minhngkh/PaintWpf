using Paint.WpfCommon;

namespace Paint.App.Models
{
    class Item
    {
        Type Shape { get; set; }
        ShapeSpecifications Specifications { get; set; }

        public Item(Type shape, ShapeSpecifications specifications)
        {
            if (!shape.IsAssignableTo(typeof(IShape)))
            {
                throw new ArgumentException("shape is not an IShape");
            }

            Shape = shape;
            Specifications = specifications;
        }
    }
}
