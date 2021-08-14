using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MaxMind.Db;
using Newtonsoft.Json;

namespace Oldsu.Utils.Location
{
    public class Geolocation
    {
        // storing ip as string maybe not the best idea
        private static readonly ConcurrentDictionary<string, GeoLocSerialize> GeoLocCache = new();
        private static readonly Reader IpLookupDatabase = new("GeoLite2-City.mmdb", FileAccessMode.MemoryMapped);

        public static async Task<(float, float)> GetGeolocationAsync(string ip)
        {
            if (ip == "127.0.0.1")
                return (0, 0);
            
            var data = IpLookupDatabase.Find<Dictionary<string, object>>(IPAddress.Parse(ip));
            
            if (data != null)
            {
                var location = (Dictionary<string, object>)data["location"];
                return ((float)location["latitude"], (float)location["longitude"]);
            }

            if (GeoLocCache.TryGetValue(ip, out var geoLoc))
                return (geoLoc.Lat, geoLoc.Lon);

            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetAsync($"http://ip-api.com/json/{ip}");
                geoLoc = JsonConvert.DeserializeObject<GeoLocSerialize>(await json.Content.ReadAsStringAsync());
            }

            GeoLocCache.TryAdd(ip, geoLoc!);
            return (geoLoc!.Lat, geoLoc!.Lon);
        }
    }
}