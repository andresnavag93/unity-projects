namespace Figures
{
    public class Square
    {
        private int _side;
        public void SetSide(int side)
        {
            _side = side;
        }

        public float GetArea()
        {
            return new Area().GetReactangleArea(_side, _side);
        }
    }

}