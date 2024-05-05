
using System.Windows;
using System.Windows.Media;

namespace Shapes
{
    public interface IShape
    {
        void Draw();

        void SetStartPoint(Point point);

        void SetEndPoint(Point point);

        void SetOutline(Brush brush);
    }

}
