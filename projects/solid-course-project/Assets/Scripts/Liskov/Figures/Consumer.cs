namespace Figures
{
    public class Consumer
    {
        public void Example1()
        {
            var rectangle = new Rectangle();

            rectangle.SetHeight(2);
            rectangle.SetWidth(5);

            rectangle.GetArea();
        }

        public void Example2()
        {
            Square square = new Square();

            square.SetSide(2);

            square.GetArea();

        }
    }
}