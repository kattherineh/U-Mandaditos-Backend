using Aplication.Interfaces.Helpers;

namespace Infrastructure.Services;

public class GeolocationService : IGeolocationService
{
    private const double EarthRadius = 6371;
    
    /*
     * Esta funcion recibe las coordenadas de dos puntos geográficos y devuelve la distancia en kilómetros
     * Utilizando la formula de Haversine.
     */
    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double dLat = DegreeToRadian(lat2 - lat1);
        double dLon = DegreeToRadian(lon2 - lon1);
        
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(DegreeToRadian(lat1)) * Math.Cos(DegreeToRadian(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadius * c;
    }
    
    /*
     * Función que convierte grados a radianes
     */
    private static double DegreeToRadian(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}