
using System.Windows;

namespace Paint.WpfCommon
{
    public interface IShape
    {
        string Name { get; }
        bool IsFillable { get; }

        UIElement Create(ShapeSpecifications specifications);

        void Transform(UIElement shape, ShapeSpecifications specifications);
    }

}
