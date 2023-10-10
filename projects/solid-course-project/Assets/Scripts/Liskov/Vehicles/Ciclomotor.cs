namespace Vehicles
{
    public class Ciclomotor : Vehiculo
    {
        private string _numeroDeLicencia;

        public Ciclomotor(string numeroDeLicencia)
        {
            _numeroDeLicencia = numeroDeLicencia;
        }

        public string ObtenerNumLicencia()
        {
            return _numeroDeLicencia;
        }
    }
}
