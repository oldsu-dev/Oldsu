using Newtonsoft.Json;

namespace Oldsu.Utils.Location
{
    internal class GeoLocSerialize
    {
        [JsonProperty("lat")]
        public float Lat { get; set; }
        
        [JsonProperty("lon")]
        public float Lon { get; set; }
        
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}