namespace Figures
{
    public class Rectangle
    {
        protected int _height;
        protected int _width;

        public void SetHeight(int height)
        {
            _height = height;
        }

        public void SetWidth(int width)
        {
            _width = width;
        }

        public float GetArea()
        {
            return new Area().GetReactangleArea(_height, _width);
        }
    }

    public class Area
    {
        public float GetReactangleArea(float height, float width)
        {
            return height * width;
        }
    }
}