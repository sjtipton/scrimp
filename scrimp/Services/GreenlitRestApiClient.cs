using scrimp.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace scrimp.Services
{
    public class GreenlitRestApiClient : IRestApiClient<GreenlitUser>
    {
        public HttpClient Client { get; }

        public GreenlitRestApiClient(HttpClient client)
        {
            client.BaseAddress = new Uri(AppSettingsProvider.GetGreenlitApiUrl());
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<GreenlitUser> GetRestApiEntity(Guid id, string authToken)
        {
            // Update the client headers to send the authToken
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            try
            {
                var response = await Client.GetAsync($"users/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<GreenlitUser>();
            }
            catch (HttpRequestException ex)
            {
                throw new AppException(ex.Message);
            }
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
