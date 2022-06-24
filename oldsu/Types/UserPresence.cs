using System;
using System.Text.Json.Serialization;
using Oldsu.Enums;
using Action = Oldsu.Enums.Action;

namespace Oldsu.Types;

[Serializable]
public class UserPresence
{
   [JsonPropertyName("privilege")] public Privileges Privilege;

   [JsonPropertyName("utcOffset")] public int UtcOffset;

   [JsonPropertyName("country")] public int Country;

   [JsonPropertyName("longitude")] public int Longitude;
   
   [JsonPropertyName("latitude")] public int Latitude;

}