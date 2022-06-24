using System;
using System.Text.Json.Serialization;
using Oldsu.Enums;
using Action = System.Action;

namespace Oldsu.Types;

[Serializable]
public class UserActivity
{
    [JsonPropertyName("map")] public string Map;

    [JsonPropertyName("mapMD5")] public string MapMD5;

    [JsonPropertyName("mods")] public int Mods;

    [JsonPropertyName("gameMode")] public Mode Mode;

    [JsonPropertyName("mapID")] public int MapID;

    [JsonPropertyName("action")] public Action Action;
}