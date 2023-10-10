namespace Vehicles
{
    public class Coche : Vehiculo
    {
        private string _matricula;

        public Coche(string matricula)
        {
            _matricula = matricula;
        }
        public string ObtenerMatricula()
        {
            return _matricula;
        }
    }
}
