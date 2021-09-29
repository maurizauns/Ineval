namespace Ineval.Dto
{
    public static class Routes
    {
        public static class GpMap
        {
            //CYCLING
            public const string Cycling = "https://api.mapbox.com/directions/v5/mapbox/cycling/<P1>;<P2>?access_token=<P3>";

            //DRIVING
            public const string Driving = "https://api.mapbox.com/directions/v5/mapbox/driving/<P1>;<P2>?access_token=<P3>";

            //POSICION GEOGRAFICA
            public const string PosicionGeografica = "https://api.mapbox.com/geocoding/v5/mapbox.places/<P1>.json?limit=2&access_token=<P3>";
        }
    }
}