using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.Utils;

public class OnlineUserService : IOnlineUserService
{
    public async Task<List<OnlineUser?>> GetOnlineUsers()
    {
        List<OnlineUser> Users = null;
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri($"https://oldsu.ayyeve.xyz");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var serverEndpoint = Environment.GetEnvironmentVariable("OLDSU_BANCHO_ENDPOINT");
        HttpResponseMessage response = 
            await client.GetAsync($"http://${serverEndpoint}/api/users").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            OnlineUser[]? users = JsonSerializer.Deserialize<OnlineUser[]>(await response.Content.ReadAsStringAsync());
            if (users != null)
            {
                Users = users.ToList();
            }
        }
        return Users;
    }
}