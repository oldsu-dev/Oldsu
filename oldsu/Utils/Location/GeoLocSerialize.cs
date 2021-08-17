using Newtonsoft.Json;

namespace Oldsu.Utils.Location
{
    public class GeoLocSerialize
    {
        [JsonProperty("lat")]
        public float Lat { get; set; }
        
        [JsonProperty("lon")]
        public float Lon { get; set; }
        
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}