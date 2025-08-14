namespace WeatherApi.Models.Entities
{
    public class Current
    {
        public DateTime time { get; set; }
        public double temperature_2m { get; set; }
        public double wind_speed_10m { get; set; }

    }
    public class WeatherEntity
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public int elevation { get; set; }
        public Current current { get; set; }

    }
}