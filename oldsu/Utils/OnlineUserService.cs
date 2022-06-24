using System;
using System.Collections.Generic;
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
        List<OnlineUser?> users = new List<OnlineUser?>();
        var url = $"https://oldsu.ayyeve.xyz";
       
 
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var serverEndpoint = Environment.GetEnvironmentVariable("OLDSU_BANCHO_ENDPOINT");
        HttpResponseMessage response = 
            await client.GetAsync($"http://${serverEndpoint}/api/users").ConfigureAwait(false);
 
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            OnlineUser? user = JsonSerializer.Deserialize<OnlineUser>(jsonString);
 
            if (user != null)
            {
                users.Add(user);
            }
        }
 
        return users;
    }
}