using System;

namespace restlessmedia.Module
{
  public class Marker
  {
    public Marker() { }

    public Marker(double latitude, double longitude)
    {
      Latitude = latitude;
      Longitude = longitude;
    }

    public Marker(double latitude, double longitude, string title)
      : this(latitude, longitude)
    {
      Title = title;
    }

    public string Title;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    /// <summary>
    /// Rounds the coords to a specific number of digits
    /// </summary>
    /// <param name="digits"></param>
    public void Round(int digits)
    {
      if (Longitude.HasValue)
      {
        Math.Round(Longitude.Value, digits);
      }

      if (Latitude.HasValue)
      {
        Math.Round(Latitude.Value, digits);
      }
    }

    public bool IsValid
    {
      get
      {
        return Latitude != 0 && Longitude != 0;
      }
    }

    /// <summary>
    /// Returns the distance in miles between coords
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="long1"></param>
    /// <param name="lat2"></param>
    /// <param name="long2"></param>
    /// <returns></returns>
    public static double MilesBetween(double lat1, double long1, double lat2, double long2)
    {
      double fromLatRad = lat1 * D2r;
      double fromLongRad = long1 * D2r;
      double toLatRad = lat2 * D2r;
      double toLongRad = long2 * D2r;

      double longitude = toLongRad - fromLongRad;
      double latitude = toLatRad - fromLatRad;

      // Intermediate result a.
      double a = Math.Pow(Math.Sin(latitude / 2.0), 2.0) + Math.Cos(fromLatRad)
               * Math.Cos(@toLatRad)
               * Math.Pow(Math.Sin(longitude / 2.0), 2.0);

      // Intermediate result c (great circle distance in Radians).
      double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

      return EQuatorialEarthRadiusInMiles * c;
    }

    [NonSerialized]
    public const double EQuatorialEarthRadiusInMiles = 3956.0D;

    [NonSerialized]
    public const double EQuatorialEarthRadiusInKm = 6376.5D;

    /// <summary>
    /// Math.PI / 180D
    /// </summary>
    [NonSerialized]
    public const double D2r = 0.0174532925199433;

    public override string ToString()
    {
      return $"{Latitude.GetValueOrDefault(0)},{Longitude.GetValueOrDefault(0)}";
    }
  }
}