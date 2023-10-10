namespace Vehicles
{
    public class Impuestos
    {
        public void CalcularImpuesto(Coche vehiculo)
        {

            var identificador = vehiculo.ObtenerMatricula();
            ServicioCalculoImpuestos(identificador, vehiculo.Cilindrada);
        }

        public void CalcularImpuesto(Ciclomotor vehiculo)
        {
            var identificador = vehiculo.ObtenerNumLicencia();
            ServicioCalculoImpuestos(identificador, vehiculo.Cilindrada);
        }

        private void ServicioCalculoImpuestos(string identificador, int cilindrada)
        {
            throw new System.NotImplementedException();
        }
    }
}


