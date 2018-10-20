using scrimp.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace scrimp.Services
{
    public class GreenlitRestApiClient
    {
        public HttpClient Client { get; }

        public GreenlitRestApiClient(HttpClient client)
        {
            client.BaseAddress = new Uri(AppSettingsProvider.GetGreenlitApiUrl());
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<GreenlitUser> GetGreenlitRestApiUser(Guid id)
        {
            var response = await Client.GetAsync($"users/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<GreenlitUser>();
        }
    }

    public class GreenlitUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
