using System;
using System.Text.Json.Serialization;

namespace Oldsu.Types;

[Serializable]
public class OnlineUser
{
    [JsonPropertyName("username")] public string Username;

    [JsonPropertyName("activity")] public UserActivity Activity;

    [JsonPropertyName("userID")] public int UserID;

    [JsonPropertyName("presence")] public UserPresence Presence;
}